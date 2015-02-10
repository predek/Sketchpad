using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchpad
{
    public class EditPanel
    {
        private MainWindow window;
        private SketchManager sketchManager;
        private EvaluationManager evaluationManager;
        private Selection selection;

        public EditPanel(MainWindow window, SketchManager sketchManager, EvaluationManager evaluationManager)
        {
            this.window = window;
            this.sketchManager = sketchManager;
            this.evaluationManager = evaluationManager;
        }

        public void select(Selection selection)
        {
            this.selection = selection;
            window.rectangle2.Visibility = Visibility.Visible;
            window.expressionTextBox.Text = selection.expression;
            window.valueLabel.Content = selection.value;
        }

        public void deselect()
        {
            selection = null;
            window.rectangle2.Visibility = Visibility.Hidden;
            window.expressionTextBox.Text = "";
            window.valueLabel.Content = "";
        }

        public void updateExpression(string changedText)
        {
            if(selection != null)
            {
                if(selection.IsNode)
                {
                    selection.expression = changedText;

                    if (selection.expression.StartsWith("="))
                    {
                        Dictionary<String, String> parametersDictionary = new Dictionary<string, string>();
                        List<Edge> feedingEdges = sketchManager.getFeedingEdges(selection.node);
                        foreach (Edge edge in feedingEdges)
                        {
                            if (edge.value != null)
                            {
                                String expressionParameterName;
                                String expressionParameterValue;

                                expressionParameterName = edge.value.ToString();     //variable name
                                expressionParameterValue = edge.originNode.value.ToString();     //variable value

                                parametersDictionary.Add(expressionParameterName, expressionParameterValue);
                            }
                        }

                        selection.value = evaluationManager.evaluate(changedText.Substring(1, changedText.Length - 1), parametersDictionary);
                    }
                    else
                    {
                        selection.value = changedText;
                    }

                    sketchManager.updateLabels();

                    window.expressionTextBox.Text = selection.expression;
                    window.valueLabel.Content = selection.value;
                }
                else
                {
                    selection.value = changedText;
                    selection.expression = changedText;
                    sketchManager.updateLabels();
                }
            }
        }
    }
}

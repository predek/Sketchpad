using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCalc;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sketchpad
{
    public class EvaluationManager
    {
        private MainWindow window;
        private List<TextBlock> textBlocks;

        public EvaluationManager(MainWindow window)
        {
            this.window = window;
            textBlocks = new List<TextBlock>();
        }

        public void updateLabels()
        {
            System.Diagnostics.Debug.WriteLine("EvaluationManager.update");

            window.labelsRectangle1.Children.Clear();

            foreach (TextBlock textBlock in textBlocks)
                window.labelsRectangle1.Children.Remove(textBlock);
        }

        public TextBlock createTextBlock()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "";
            String color = "#000000";
            textBlock.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            Canvas.SetLeft(textBlock, 0);
            Canvas.SetTop(textBlock, 0);

            return textBlock;
        }

        public String evaluate(String expressionString, Dictionary<String, String> parametersDictionary)
        {
            String value = "";

            if (expressionString == "")
            {
                return value;
            }

            try
            {
                NCalc.Expression expression = new Expression(expressionString);

                if (parametersDictionary.Count > 0)
                {
                    foreach (KeyValuePair<string, string> pair in parametersDictionary)
                    {
                        expression.Parameters[pair.Key] = Convert.ToDouble(pair.Value);
                    }
                }

                try
                {
                    value = expression.Evaluate().ToString();
                }
                catch (EvaluationException exception)
                {

                }

            }
            catch (ArgumentException exception)
            {
                return value;
            }
            catch (FormatException exception)
            {
                return value;
            }

            return value;
        }
    }
}

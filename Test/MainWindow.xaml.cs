using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sketchpad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SketchManager sketchManager;
        ToolsManager toolsManager;
        EditPanel editPanel;
        EvaluationManager evaluationManager;

        public MainWindow()
        {
            //InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            evaluationManager = new EvaluationManager(this);
            sketchManager = new SketchManager(this, evaluationManager);
            editPanel = new EditPanel(this, sketchManager, evaluationManager);
            toolsManager = new ToolsManager(sketchManager, editPanel);

            rectangle2.Visibility = Visibility.Hidden;
        }

        private void rectangle1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            toolsManager.MouseDown(e.GetPosition(rectangle1));
        }

        private void rectangle1_MouseMove(object sender, MouseEventArgs e)
        {
            bool mousePressed = e.LeftButton == MouseButtonState.Pressed;

            if (mousePressed)
                toolsManager.MouseMove(e.GetPosition(rectangle1));

        }

        private void rectangle1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            toolsManager.MouseUp(e.GetPosition(rectangle1));
        }

        private void rectangle1_MouseLeave(object sender, MouseEventArgs e)
        {
            bool mousePressed = e.LeftButton == MouseButtonState.Pressed;

            if (mousePressed)
                toolsManager.MouseLeave(e.GetPosition(rectangle1));
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            toolsManager.setTool("SelectTool");
            makeButtonPressed(SelectButton);

        }

        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            toolsManager.setTool("Pencil");
            makeButtonPressed(PencilButton);
        }

        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            toolsManager.setTool("Eraser");
            makeButtonPressed(EraserButton);
        }

        private void makeButtonPressed(Button buttonToPress)
        {
            List<Button> toolsetButtons = new List<Button>();
            toolsetButtons.Add(SelectButton);
            toolsetButtons.Add(PencilButton);
            toolsetButtons.Add(EraserButton);

            foreach (Button button in toolsetButtons)
            {
                button.BorderThickness = new Thickness(1);
            }

            buttonToPress.BorderThickness = new Thickness(4);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            sketchManager.Document = new Document();
            sketchManager.update();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML Files (.xml)|*.xml";
            openDialog.FilterIndex = 1;

            if (openDialog.ShowDialog().Value)
            {
                sketchManager.Document = FileManager.read(openDialog.FileName);
                sketchManager.update();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML Files (.xml)|*.xml";
            saveDialog.FilterIndex = 1;

            if (saveDialog.ShowDialog().Value)
            {
                FileManager.write(saveDialog.FileName, sketchManager.Document);
            }
        }

        private void expressionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            editPanel.updateExpression(expressionTextBox.Text);
        }
    }
}

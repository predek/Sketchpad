using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sketchpad
{
    public class SelectTool : Tool
    {
        SketchManager sketchManager;
        private EditPanel editPanel;

        public SelectTool(SketchManager sketchManager ,EditPanel editPanel)
        {
            this.sketchManager = sketchManager;
            this.editPanel = editPanel; 
        }

        public void MouseDown(Point p)
        {
            Selection selection;
            List<Node> nodes = sketchManager.getNodesUnder(p);
            System.Diagnostics.Debug.WriteLine("nodes.Count" + nodes.Count);
            if (nodes.Count == 1)
            {
                selection = new Selection(nodes.ElementAt(0));
                editPanel.select(selection);
            }
            else
            {
                List<Edge> edges = sketchManager.getEdgesUnder(p);
                System.Diagnostics.Debug.WriteLine("edges.Count" + edges.Count);
                if (edges.Count == 1)
                {
                    selection = new Selection(edges.ElementAt(0));
                    editPanel.select(selection);
                }
                else
                    editPanel.deselect();
            }
        }

        public void MouseMove(Point p)
        {

        }

        public void MouseUp(Point p)
        {

        }

        public void MouseLeave(Point p)
        {

        }

        public void closeTool()
        {
            editPanel.deselect();
        }
    }
}

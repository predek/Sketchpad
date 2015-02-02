using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchpad
{
    public class ToolsManager
    {
        private SketchManager sketchManager;
        private EditPanel editPanel;

        private Pencil pencil;
        private Eraser eraser;
        private SelectTool selectTool;

        private Tool currentTool;

        public ToolsManager(SketchManager sketchManager, EditPanel editPanel)
        {
            this.sketchManager = sketchManager;
            this.editPanel = editPanel;

            pencil = new Pencil(sketchManager);
            eraser = new Eraser(sketchManager);
            selectTool = new SelectTool(sketchManager, editPanel);

            setTool("Pencil");
        }

        public void setTool(String toolName)
        {
            if(toolName == "Pencil")
            {
                currentTool = pencil;
            }
            else if (toolName == "Eraser")
            {
                currentTool = eraser;
            }
            else if (toolName == "SelectTool")
            {
                currentTool = selectTool;
            }
        }

        public void MouseDown(Point p)
        {
            currentTool.MouseDown(p);
        }

        public void MouseMove(Point p)
        {
            currentTool.MouseMove(p);
        }

        public void MouseUp(Point p)
        {
            currentTool.MouseUp(p);
        }

        public void MouseLeave(Point p)
        {
            currentTool.MouseLeave(p);
        }
    }
}

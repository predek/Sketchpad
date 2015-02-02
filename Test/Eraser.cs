using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sketchpad
{
    public class Eraser : Tool
    {
        private SketchManager sketchManager;

        private bool isMouseMoving;
        private Point prevErasedPoint;

        public Eraser(SketchManager sketchManager)
        {
            this.sketchManager = sketchManager;
        }

        public void MouseDown(Point p)
        {
            isMouseMoving = true;
            prevErasedPoint = p;
            eraseNeighbourhood(p);
        }

        public void MouseMove(Point p)
        {
            if (isMouseMoving && distance(p, prevErasedPoint) > 10)
            {
                prevErasedPoint = p;
                eraseNeighbourhood(p);
            }
        }

        public void MouseUp(Point p)
        {
            if (isMouseMoving)
            {
                prevErasedPoint = p;
                isMouseMoving = false;
                eraseNeighbourhood(p);
            }
        }

        public void MouseLeave(Point p)
        {
            if (isMouseMoving)
            {
                prevErasedPoint = p;
                isMouseMoving = false;
                eraseNeighbourhood(p);
            }
        }

        private void eraseNeighbourhood(Point p)
        {
            sketchManager.eraseNeighbourhood(p);
        }

        public void closeTool()
        {
            if (isMouseMoving)
                isMouseMoving = false;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}

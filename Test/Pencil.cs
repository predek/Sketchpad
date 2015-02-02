using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchpad
{
    public class Pencil : Tool
    {
        SketchManager sketchManager;

        private Segment currentSegment;
        private bool isMouseMoving = false;
        private Point initPoint;

        public Pencil(SketchManager sketchManager)
        {
            this.sketchManager = sketchManager;
        }

        public void MouseDown(Point p)
        {
            initPoint = p;

            currentSegment = new Segment();
            currentSegment.p1 = p;
            currentSegment.p2 = p;

            sketchManager.SegmentsBuffer.push(currentSegment);

            isMouseMoving = true;
        }

        public void MouseMove(Point p)
        {
            if (isMouseMoving)
            {
                if (distance(p, currentSegment.p1) > 10)
                {
                    currentSegment.p2 = p;
                    currentSegment = new Segment();
                    currentSegment.p1 = p;
                    currentSegment.p2 = p;
                    sketchManager.SegmentsBuffer.push(currentSegment);
                    sketchManager.SegmentsBuffer.segmentUpdated();
                }
            }
        }

        public void MouseUp(Point p)
        {
            closeTool();
            if (isMouseMoving)
            {
                currentSegment.p2 = p;
                sketchManager.SegmentsBuffer.segmentUpdated();
                sketchManager.SegmentsBuffer.done();
            }

            isMouseMoving = false;
        }

        public void MouseLeave(Point p)
        {
            if (isMouseMoving)
            {
                currentSegment.p2 = p;
                sketchManager.SegmentsBuffer.done();
            }
            isMouseMoving = false;
        }

        public void closeTool()
        {
            isMouseMoving = false;
            sketchManager.SegmentsBuffer.done();
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

    }
}

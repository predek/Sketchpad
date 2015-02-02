using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchpad
{
    public class SegmentsBuffer
    {
        SketchManager sketchManager;
        public List<Segment> segments;

        public SegmentsBuffer(SketchManager sketchManager)
        {
            this.sketchManager = sketchManager;
            segments = new List<Segment>();
        }

        public void push(Segment segment)
        {
            segments.Add(segment);
        }

        public void segmentUpdated()
        {
            sketchManager.update();
        }

        public void done()
        {
            Node node = detectNode(segments);
            if(node != null)
            {
                sketchManager.pushNode(node);
            }
            else
            {
                Edge edge = detectEdge(segments);
                if (edge != null)
                {
                    sketchManager.pushEdge(edge);
                }
                else
                {
                    sketchManager.pushFreeSegments(segments);
                }
            }

            segments.Clear();
            sketchManager.update();
        }

        //private

        private Node detectNode(List<Segment> segments)
        {
            if (segments.Count > 0 && distance(segments.First().p1, segments.Last().p2) < 20)
            {
                List<Segment> cycleSegments = segments;
                List<Point> cyclePoints = cycleSegments.Select(s => s.p1).ToList<Point>();

                List<Point> orderedByX = cyclePoints.OrderBy(x => x.X).ToList<Point>();
                double minX = Math.Round(orderedByX.First<Point>().X);
                double maxX = Math.Round(orderedByX.Last<Point>().X);

                List<Point> orderedByY = cyclePoints.OrderBy(x => x.Y).ToList<Point>();
                double minY = Math.Round(orderedByY.First<Point>().Y);
                double maxY = Math.Round(orderedByY.Last<Point>().Y);

                double w = maxX - minX;
                double h = maxY - minY;

                //all cycle point boudary
                Rect bounds = new Rect(new Point(minX, minY), new Point(maxX, maxY));

                double f = 5;
                //small rect inside bounds to make sure shape reminds rectangle
                Rect excludedBounds = new Rect(new Point(minX + h / f, minY + h / f), new Point(maxX - h / f, maxY - h / f));

                bool anyInsideExcludedBouds = cyclePoints.Any(x => excludedBounds.Contains(x));

                double ratio = w / h;
                bool correctRatio = ratio > 0.25 && ratio < 4;

                bool isRectangle = !anyInsideExcludedBouds && correctRatio && w > 30 && h > 30;

                if (isRectangle)
                {
                    //redraw rectangle:
                    Segment s1 = new Segment();
                    s1.p1 = new Point(minX, minY);
                    s1.p2 = new Point(maxX, minY);

                    Segment s2 = new Segment();
                    s2.p1 = new Point(maxX, minY);
                    s2.p2 = new Point(maxX, maxY);

                    Segment s3 = new Segment();
                    s3.p1 = new Point(maxX, maxY);
                    s3.p2 = new Point(minX, maxY);

                    Segment s4 = new Segment();
                    s4.p1 = new Point(minX, maxY);
                    s4.p2 = new Point(minX, minY);

                    int limit = 20;
                    List<Segment> ls1 = divideSegment(s1, limit);   //divided top edge
                    List<Segment> ls2 = divideSegment(s2, limit);
                    List<Segment> ls3 = divideSegment(s3, limit);
                    List<Segment> ls4 = divideSegment(s4, limit);
                    
                    List<Segment> redrawnRectangle = new List<Segment>();
                    redrawnRectangle.AddRange(ls1);
                    redrawnRectangle.AddRange(ls2);
                    redrawnRectangle.AddRange(ls3);
                    redrawnRectangle.AddRange(ls4);

                    //shake it
                    Random random = new Random();
                    int range = 1;
                    for (int i = 0; i < redrawnRectangle.Count; i++)
                    {
                        Point movedPoint = new Point();
                        movedPoint.X = redrawnRectangle[i].p2.X + random.Next(-range, range);
                        movedPoint.Y = redrawnRectangle[i].p2.Y + random.Next(-range, range);

                        redrawnRectangle[i].p2 = redrawnRectangle[(i + 1) % redrawnRectangle.Count].p1 = movedPoint;
                    }

                    Node node = new Node(bounds, redrawnRectangle);
                    return node;
                }
            }

            return null;
        }

        private Edge detectEdge(List<Segment> segments)
        {
            bool longerThan40px = false;
            if(segments.Count > 0)
                longerThan40px = distance(segments.First().p1, segments.Last().p2) > 40;

            if (longerThan40px)
            {
                Segment straightLine = new Segment();
                straightLine.p1 = segments.First().p1;
                straightLine.p2 = segments.Last().p2;

                Node p1nearestNode = sketchManager.nearestNode(straightLine.p1);
                double d1 = -1;
                if (p1nearestNode != null)
                    d1 = distanceToRect(straightLine.p1, p1nearestNode.bounds);

                Node p2nearestNode = sketchManager.nearestNode(straightLine.p2);
                double d2 = -1;
                if (p1nearestNode != null)
                    d2 = distanceToRect(straightLine.p2, p2nearestNode.bounds);

                //System.Diagnostics.Debug.WriteLine("//");
                //System.Diagnostics.Debug.WriteLine("p1nearestNode = " + p1nearestNode);
                //System.Diagnostics.Debug.WriteLine("p2nearestNode = " + p2nearestNode);
                //System.Diagnostics.Debug.WriteLine("d1 = " + d1);
                //System.Diagnostics.Debug.WriteLine("d2 = " + d2);

                bool isEdge = d1 > 0 && d1 < 30 && d2 > 0 && d2 < 30; //line connects p1nearestNode and p2nearestNode == is edge                    
                if ((p1nearestNode != null) && (p2nearestNode != null) && isEdge)
                {
                    //double p1p2angle = calculateAngle(segments.First().p1, segments.Last().p2);
                    //System.Diagnostics.Debug.WriteLine("p1p2angle = " + p1p2angle);

                    //bool isLine = !freshSegments.Any(s => angleDifference(sumAngle, calculateAngle(s.p1, s.p2)) > 45);
                    //bool isStraight = !segments.Any(s => angleDistance(p1p2angle, calculateAngle(s.p1, s.p2)) > 45);
                    //System.Diagnostics.Debug.WriteLine("isStraight = " + isStraight);

                    //
                    double straightLineLength = distance(straightLine.p1, straightLine.p2);
                    double segmentsLength = 0;
                    foreach (Segment segment in segments)
                        segmentsLength += distance(segment.p1, segment.p2);

                    bool isStraight = (segmentsLength / straightLineLength) < 1.1;

                    if (isStraight)
                    {
                        //build straight line
                        int limit = 20;
                        List<Segment> redrawnLine = divideSegment(straightLine, limit);

                        //shake it
                        Random random = new Random();
                        int range = 1;
                        for (int i = 0; i < redrawnLine.Count - 1; i++)
                        {
                            Point movedPoint = new Point();
                            movedPoint.X = redrawnLine[i].p2.X + random.Next(-range, range);
                            movedPoint.Y = redrawnLine[i].p2.Y + random.Next(-range, range);

                            redrawnLine[i].p2 = redrawnLine[(i + 1) % redrawnLine.Count].p1 = movedPoint;
                        }

                        //build arrowhead
                        List<Segment> arrowDashes = drawArrowHead(straightLine);

                        //build edge data
                        List<Segment> edgeSegments = new List<Segment>();
                        edgeSegments.AddRange(redrawnLine);
                        edgeSegments.AddRange(arrowDashes);

                        Edge edge = new Edge(straightLine, edgeSegments, p1nearestNode, p2nearestNode);
                        return edge;
                    }
                    
                }
            }
            return null;
        }

        //misc

        private List<Segment> drawArrowHead(Segment segmentLine)
        {
            double angle = calculateAngle(segmentLine.p2, segmentLine.p1);
            //SketchpadApp.Instance.Diagnostics.Write("drawArrowHead");
            //SketchpadApp.Instance.Diagnostics.Write("angle = " + angle);

            Point arrowHead = new Point(segmentLine.p2.X, segmentLine.p2.Y);
            int dashLength = 15;

            double dashAngle = Math.PI / 180 * 40;

            System.Diagnostics.Debug.WriteLine("angle = " + angle);
            //while (angle < 0)
                //angle += 360;

            double a1 = angle - dashAngle / 2;
            Segment s1 = new Segment();
            s1.p1 = arrowHead;
            s1.p2 = new Point(arrowHead.X + dashLength * Math.Cos(a1), arrowHead.Y + dashLength * Math.Sin(a1));

            double a2 = angle + dashAngle / 2;
            Segment s2 = new Segment();
            s2.p1 = arrowHead;
            s2.p2 = new Point(arrowHead.X + dashLength * Math.Cos(a2), arrowHead.Y + dashLength * Math.Sin(a2));

            List<Segment> result = new List<Segment>();
            result.Add(s1);
            result.Add(s2);

            return result;
        }

        private List<Segment> divideSegment(Segment s, int limit)
        {
            List<Segment> result = new List<Segment>();
            if (distance(s.p1, s.p2) < limit)
            {
                result.Add(s);
            }
            else
            {
                Point midpoint = new Point((s.p1.X + s.p2.X) / 2, (s.p1.Y + s.p2.Y) / 2);

                Segment s1 = new Segment();
                s1.p1 = s.p1;
                s1.p2 = midpoint;
                result.AddRange(divideSegment(s1, limit));

                Segment s2 = new Segment();
                s2.p1 = midpoint;
                s2.p2 = s.p2;
                result.AddRange(divideSegment(s2, limit));
            }
            return result;
        }

        private static double calculateAngle(Point p1, Point p2)//degress!
        {
            //calculate angle created by 2 points
            return Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);// *180 / Math.PI;
        }

        private double angleDistance(double a1, double a2)
        {
            double d = Math.Abs(a1 - a2) % 360;
            double result = d > 180 ? 360 - d : d;
            return result;
        }

        private double distanceToRect(Point p, Rect bounds)
        {
            double cx = Math.Max(Math.Min(p.X, bounds.X + bounds.Width), bounds.X);
            double cy = Math.Max(Math.Min(p.Y, bounds.Y + bounds.Height), bounds.Y);
            return Math.Sqrt((p.X - cx) * (p.X - cx) + (p.Y - cy) * (p.Y - cy));
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}

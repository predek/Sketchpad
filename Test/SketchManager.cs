using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Sketchpad
{
    public class SketchManager
    {
        private MainWindow window;
        private SegmentsBuffer segmentsBuffer;
        private EvaluationManager evaluationManager;

        private List<Segment> freeSegments;
        private List<Node> nodes;
        private List<Edge> edges;

        public SketchManager(MainWindow window, EvaluationManager evaluationManager)
        {
            this.window = window;
            segmentsBuffer = new SegmentsBuffer(this);
            this.evaluationManager = evaluationManager;

            Document = new Document();
        }

        public SegmentsBuffer SegmentsBuffer
        {
            get
            {
                return segmentsBuffer;
            }
        }

        public Document Document
        {
            get
            {
                Document document = new Document();
                document.freeSegments = freeSegments;
                document.nodes = nodes;
                document.edges = edges;
                return document;
            }

            set
            {
                freeSegments = value.freeSegments;
                nodes = value.nodes;
                edges = value.edges;
                fixNodesReferencesInEdges();
                updateLabels();
            }
        }

        private void fixNodesReferencesInEdges()
        {
            foreach(Edge edge in edges)
                foreach (Node node in nodes)
                {
                    if (edge.originNode.bounds.Equals(node.bounds))
                        edge.originNode = node;
                    if (edge.targetNode.bounds.Equals(node.bounds))
                        edge.targetNode = node;
                }
        }

        public void pushFreeSegments(List<Segment> segments)
        {
            freeSegments.AddRange(segments);
        }

        public void pushNode(Node node)
        {
            nodes.Add(node);
            updateLabels();
        }

        public void pushEdge(Edge edge)
        {
            edges.Add(edge);
            updateLabels();
        }

        public void eraseNeighbourhood(Point p)
        {
            //erase freeSegments
            for (int i = 0; i < freeSegments.Count; i++)
            {
                Segment segment = freeSegments[i];
                if (distance(middlePoint(segment), p) < 15)
                    freeSegments.Remove(segment);
            }

            //erase nodes
            List<Node> deletedNodes = new List<Node>();

            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];
                bool repurposeSegments = false;

                for (int j = 0; j < node.segments.Count; j++)
                {
                    Segment segment = node.segments[j];
                    if (distance(middlePoint(segment), p) < 15)
                    {
                        node.segments.Remove(segment);
                        repurposeSegments = true;
                    }
                }

                if(repurposeSegments)
                {
                    freeSegments.AddRange(node.segments);
                    deletedNodes.Add(node);
                    nodes.Remove(node);
                }
            }

            //erase edges
            for (int i = 0; i < edges.Count; i++)
            {
                Edge edge = edges[i];
                bool repurposeSegments = false;

                for (int j = 0; j < edge.segments.Count; j++)
                {
                    Segment segment = edge.segments[j];
                    if (distance(middlePoint(segment), p) < 15)
                    {
                        edge.segments.Remove(segment);
                        repurposeSegments = true;
                    }
                }
                
                //repurpose edge if connected to deleted node:
                if (deletedNodes.Contains(edge.originNode) || deletedNodes.Contains(edge.targetNode))
                    repurposeSegments = true;

                if (repurposeSegments)
                {
                    freeSegments.AddRange(edge.segments);
                    edges.Remove(edge);
                }
            }

            update();
        }

        public void update()
        {
            redraw();
        }

        private void redraw(Selection selection = null)
        {
            window.rectangle1.Children.Clear();

            string sketchColor = "#0557CE";
            string shapeColor = "#000000";
            string stressColor = "#FF0000";

            foreach (Node node in nodes)
                foreach (Segment segment in node.segments)
                    drawLine(segment, shapeColor);

            foreach (Edge edge in edges)
                foreach (Segment segment in edge.segments)
                    drawLine(segment, shapeColor);

            foreach (Segment segment in freeSegments)
                drawLine(segment, sketchColor);

            foreach (Segment segment in segmentsBuffer.segments)
                drawLine(segment, sketchColor);
            
            if (selection != null)
            {
                if(selection.IsNode)
                {
                    foreach (Segment segment in selection.node.segments)
                        drawLine(segment, stressColor, 1);
                }
                else
                {
                    foreach (Segment segment in selection.edge.segments)
                        drawLine(segment, stressColor, 1);
                }
            }
        }

        private void drawLine(Segment segment, string color, double strokeThickness = 0.5)
        {
            Line line = new Line();
            line.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            line.X1 = segment.p1.X;
            line.X2 = segment.p2.X;
            line.Y1 = segment.p1.Y;
            line.Y2 = segment.p2.Y;

            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Center;
            line.StrokeThickness = strokeThickness;
            window.rectangle1.Children.Add(line);
        }

        public List<Node> getNodesUnder(Point p)
        {
            return nodes.FindAll(n => n.bounds.Contains(p)).ToList();
        }

        public List<Edge> getEdgesUnder(Point p)
        {
            return edges.FindAll(e => distanceToLine(p, e.line) < 10).ToList();
        }

        public List<Edge> getFeedingEdges(Node node)
        {
            return edges.FindAll(e => e.targetNode.bounds.Equals(node.bounds));
        }

        public void updateLabels()
        {
            window.labelsRectangle1.Children.Clear();

            foreach (Node node in nodes)
                drawTextBlock(node.value, node.expression, node.bounds.TopLeft, node.bounds);

            foreach (Edge edge in edges)
                drawTextBlock(edge.value, edge.expression, middlePoint(edge.line), new Rect(edge.line.p1, edge.line.p2));
        }

        public void drawTextBlock(String text, String toolTipText, Point position, Rect bounds)
        {
            int margin = 5;
            position.Offset(margin, margin);

            TextBlock textBlock = new TextBlock();
            textBlock.ToolTip = toolTipText;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Text = text;
            String color = "#000000";
            textBlock.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));

            Canvas.SetLeft(textBlock, position.X);
            Canvas.SetTop(textBlock, position.Y);

            textBlock.MaxWidth = Math.Round(bounds.Width - 2 * margin);
            textBlock.MaxHeight = Math.Round(bounds.Height - 2 * margin);

            window.labelsRectangle1.Children.Add(textBlock);
        }

        public void stress(Selection selection)
        {
            redraw(selection);        
        }

        public void unstress()
        {
            redraw();
        }

        //misc

        public Node nearestNode(Point p)
        {
            return nodes.OrderBy(node => distanceToRect(p, node.bounds)).FirstOrDefault();
        }

        private Point middlePoint(Segment segment)
        {
            Point result = new Point((segment.p1.X + segment.p2.X) / 2, (segment.p1.Y + segment.p2.Y) / 2);
            return result;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private double distanceToLine(Point point, Segment line)
        {
            Point l1 = line.p1;
            Point l2 = line.p2;

            return Math.Abs((l2.X - l1.X) * (l1.Y - point.Y) - (l1.X - point.X) * (l2.Y - l1.Y)) /
                    Math.Sqrt(Math.Pow(l2.X - l1.X, 2) + Math.Pow(l2.Y - l1.Y, 2));
        }
        
        private double distanceToRect(Point p, Rect bounds)
        {
            double cx = Math.Max(Math.Min(p.X, bounds.X + bounds.Width), bounds.X);
            double cy = Math.Max(Math.Min(p.Y, bounds.Y + bounds.Height), bounds.Y);
            return Math.Sqrt((p.X - cx) * (p.X - cx) + (p.Y - cy) * (p.Y - cy));
        }
    }
}

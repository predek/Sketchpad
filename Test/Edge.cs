using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Sketchpad
{
    public class Edge
    {
        public Segment line;
        public List<Segment> segments;

        public Node originNode;
        public Node targetNode;

        public string expression = "";
        public string value = "";

        public Edge()
        {

        }

        public Edge(Segment line, List<Segment> segments, Node originNode, Node targetNode)
        {
            this.line = line;
            this.segments = segments;
            this.originNode = originNode;
            this.targetNode = targetNode;
        }
    }
}

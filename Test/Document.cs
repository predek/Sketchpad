using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sketchpad
{
    public class Document
    {
        public List<Segment> freeSegments;
        public List<Node> nodes;
        public List<Edge> edges;

        public Document()
        {
            freeSegments = new List<Segment>();
            nodes = new List<Node>();
            edges = new List<Edge>();
        }
    }
}

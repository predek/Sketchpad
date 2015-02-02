using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sketchpad
{
    public class Node
    {
        public Rect bounds;
        public List<Segment> segments;

        public string expression = "";
        public string value = "";
        
        public Node()
        {

        }

        public Node(Rect bounds, List<Segment> segments)
        {
            this.bounds = bounds;
            this.segments = segments;
        }
    }
}

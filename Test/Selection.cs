using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpad
{
    public class Selection
    {
        public Node node;
        public Edge edge;

        private bool isNode;
	
        public Selection(Node node)
        {
            isNode = true;
            this.node = node;
        }

        public Selection(Edge edge)
        {
            isNode = false;
            this.edge = edge;
        }

        public bool IsNode
        {
            get
            {
                return isNode;
            }
        }

        public string expression
        {
            get
            {
                if (isNode)
                    return node.expression;
                else
                    return edge.expression;
            }
            set
            {
                if (isNode)
                    node.expression = value;
                else
                    edge.expression = value;
            }
        }

        public string value
        {
            get
            {
                if (isNode)
                    return node.value;
                else
                    return edge.value;
            }
            set
            {
                if (isNode)
                    node.value = value;
                else
                    edge.value = value;
            }
        }
    }
}

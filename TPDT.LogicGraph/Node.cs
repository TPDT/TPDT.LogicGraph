using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph
{
    public sealed class Node : GameElement
    {
        public Node(LogicGraph game, NodeBase data)
            : base(game)
        {
            NodeData = data;
        }
    
        public NodeBase NodeData { get; set; }
    }
}

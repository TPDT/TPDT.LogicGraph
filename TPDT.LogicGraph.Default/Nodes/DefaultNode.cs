using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph.Default
{
    public class DefaultNode:NodeBase
    {
        public DefaultNode(int id, NodeDefinition node, Tuple<float, float> position) :
            base(id, node, position)
        {

        }
    }
}

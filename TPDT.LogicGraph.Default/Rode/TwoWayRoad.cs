using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph.Default
{
    public class TwoWayRoad : RoadBase
    {
        static TwoWayRoad()
        {
            Name = "TwoWayRoad";
        }
        public TwoWayRoad(int id, NodeBase node1, NodeBase node2)
            : base(id, node1, node2)
        {
        }
        public override bool IsMoveable(Army.ArmyBase army)
        {
            return true;
        }
    }
}

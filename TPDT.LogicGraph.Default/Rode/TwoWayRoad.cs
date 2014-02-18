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
        private static int index = 0;
        static TwoWayRoad()
        {
            Name = "TwoWayRoad";
        }
        public TwoWayRoad()
        {
            this.Id = index++;
        }
        public override bool IsMoveable(Army.ArmyBase army)
        {
            return true;
        }
    }
}

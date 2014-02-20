using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPDT.LogicGraph.Army;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph.Default
{
    public class Soilder : ArmyBase
    {
        public Soilder(int id, ArmyDefinition army, NodeBase position, PlayerBase owner) :
            base(id, army, position, owner)
        {
        }

    }
}

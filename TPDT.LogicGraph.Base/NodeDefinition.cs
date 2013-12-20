using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using TPDT.LogicGraph.Army;
using System.IO;

namespace TPDT.LogicGraph.Base
{
    public class NodeDefinition : DataDefinion
    {
        public NodeDefinition()
            : base()
        {

        }

        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
        }

        public static NodeDefinition Load(BinaryReader reader)
        {
            return DataDefinion.Load<NodeDefinition>(reader);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public class CardDefinition : DataDefinion
    {
        public CardDefinition()
            : base()
        {
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            base.Save(writer);
        }

        public static CardDefinition Load(BinaryReader reader)
        {
            return DataDefinion.Load<CardDefinition>(reader);
        }
    }
}

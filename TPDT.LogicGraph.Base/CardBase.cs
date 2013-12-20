using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public abstract class CardBase
    {
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public CardBase(CardDefinition card)
        {
            Name = card.DefaultDescription;
            Description = card.DefaultDescription;
        }
        public virtual void WriteNode(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(this.GetType().FullName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public abstract class CardBase : GameEntity
    {
        public PlayerBase Owner { get; protected set; }
        public CardDefinition Definition { get; protected set; }
        private bool _used;

        public bool Used
        {
            get { return _used; }
            set
            {
                _used = value;
                if (value)
                    OnUsed(this, new EventArgs());
            }
        } 

        private static int index = 0;

        public event EventHandler OnStartUsing;

        public event EventHandler OnUsed;

        protected CardBase(int id,CardDefinition card, PlayerBase owner)
        {
            this.Id = id;
            Name = card.DefaultDescription;
            Owner = owner;
            Description = card.DefaultDescription;
            Used = false;
        }
        public virtual void WriteCard(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(this.GetType().FullName);
        }

        public static CardBase CreateCard(CardDefinition cardDefinition, PlayerBase owner)
        {
            CardBase card;

            card = ResourceManager.CurrentResouceManager.LoadedNodes[cardDefinition.EntityType].InvokeMember(
                null,
                System.Reflection.BindingFlags.CreateInstance,
                null,
                null,
                new object[] { index++, cardDefinition, owner }
                ) as CardBase;

            return card;
        }

        public virtual bool Use()
        {
            OnStartUsing(this, new EventArgs());
            return true;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

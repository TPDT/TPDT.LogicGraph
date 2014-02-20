using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPDT.LogicGraph.Army;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph.Default
{
    public class Spirit : CardBase, ISelectable, IRound
    {
        public int Span { get; private set; }
        public int AttackIncrement { get; private set; }
        public NodeBase SelectNode { get;  set; }
        public ArmyBase TargetArmy { get; private set; }
        public int Round { get;  set; }
        public Spirit(int id, CardDefinition card, PlayerBase owner)
            : base(id, card, owner)
        {
            AttackIncrement = Convert.ToInt32(card.Tags["AttackIncrement"]);
            Span = Convert.ToInt32(card.Tags["Span"]);
        }

        public event EventHandler OnNodeSelected;
        public event EventHandler OnNodeNotSelected;
        public event EventHandler OnStarted;
        public event EventHandler OnToggled;
        public event EventHandler OnEnded;

        public void Select(NodeBase node)
        {
            if (SelectTest(node))
            {
                SelectNode = node;
                TargetArmy = node.Army;
                OnNodeSelected(this, new EventArgs());
            }
            else
                OnNodeNotSelected(this, new EventArgs());
        }

        public bool SelectTest(NodeBase node)
        {
            return (node.Army != null);
        }

        public void Start()
        {
            if(TargetArmy!=null)
            {
                Round = Span;
                TargetArmy.Attack += AttackIncrement;
                OnStarted(this, new EventArgs());
            }
        }

        public void Toggle()
        {
            if (Round > 0)
            {
                Round--;
                OnToggled(this, new EventArgs());
            }
            else if (Span == 0)
            {
                TargetArmy.Attack -= AttackIncrement;
                Used = true;
                OnEnded(this, new EventArgs());
            }
        }

        public override bool Use()
        {
            if (!Used && TargetArmy != null)
            {
                Start();
                return base.Use();
            }
            return false;
        }
    }
}

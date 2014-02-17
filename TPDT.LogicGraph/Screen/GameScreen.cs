using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPDT.LogicGraph.Base;
using TPDT.LogicGraph.UI;

namespace TPDT.LogicGraph
{
    class GameScreen : Screen
    {
        public ResourceManager Resource { get; set; }
        private Button btnnode, btnroad;
        private Map map;
        private Texture2D ntex;
        private bool addnode, addroad;
        public GameScreen(LogicGraph game)
            : base(game)
        {
            Resource = new ResourceManager();
            btnnode = new Button(game, "Button.png", "Add Node", Vector2.Zero, new Size2(110, 30), null, Color.Blue);
            btnroad = new Button(game, "Button.png", "Add Road", new Vector2(120, 0), new Size2(110, 30), null, Color.Blue);
            btnnode.Click += btnnode_Click;
            btnroad.Click += btnroad_Click;

            map = new Map();
        }

        void btnroad_Click(object sender, EventArgs e)
        {
            addroad = !addroad;
        }

        void btnnode_Click(object sender, EventArgs e)
        {
            addnode = !addnode;
        }
        public override void Initialize()
        {
            Resource.Load(@"default.def");
            this.Components.Add(btnnode);
            this.Components.Add(btnroad);

            addnode = addroad = false;

            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            foreach(NodeBase node in map.Nodes)
            {
                Game.SpriteBatch.Draw(ntex, new Vector2(node.Position.Item1, node.Position.Item2), Color.White);
            }
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            btnnode.HoverBackground = Content.Load<Texture2D>("ButtonHover.png");
            btnroad.HoverBackground = Content.Load<Texture2D>("ButtonHover.png");
            ntex = Content.Load<Texture2D>("Node_0.png");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (Game.MouseHelper.CurrentState.Left == SharpDX.Toolkit.Input.ButtonState.Pressed)
            {
                if (addnode)
                {
                    map.Nodes.Add(NodeBase.CreateNode(Resource.NodeDefinitions[0],
                        new Tuple<float, float>(Game.MouseHelper.Position.X - 20, Game.MouseHelper.Position.Y - 20)));
                }
                addnode = addroad = false;
            }
            base.Update(gameTime);
        }
    }
}

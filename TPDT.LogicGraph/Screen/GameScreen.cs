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
        private bool addnode, addroad;
        private NodeBase selectNode;
        public GameScreen(LogicGraph game)
            : base(game)
        {
            Resource = new ResourceManager();
            btnnode = new Button(game, "Button.png", "Add Node", Vector2.Zero, new Size2(110, 30), null, Color.Blue);
            btnroad = new Button(game, "Button.png", "Add Road", new Vector2(120, 0), new Size2(110, 30), null, Color.Blue);
            btnnode.Click += btnnode_Click;
            btnroad.Click += btnroad_Click;

            this.OnButtonUp += GameScreen_OnButtonUp;

            map = new Map();
        }

        void GameScreen_OnButtonUp(object sender, EventArgs e)
        {
            if (addnode)
            {
                var node = NodeBase.CreateNode(Resource.NodeDefinitions[0],
                    new Tuple<float, float>(Game.MouseHelper.Position.X, Game.MouseHelper.Position.Y));
                map.Nodes.Add(node);
                var comp = new Node(Game, node);
                this.Components.Add(comp);
                comp.OnButtonUp += comp_OnButtonUp;
                this.addnode = false;
            }
        }

        void comp_OnButtonUp(object sender, EventArgs e)
        {
            if (selectNode == null)
            {
                selectNode = ((Node)sender).NodeData;
            }
            else
            {
                if (selectNode != ((Node)sender).NodeData)
                {
                    var rode = RoadBase.CreateRode(0, selectNode, ((Node)sender).NodeData);
                    map.Roads.Add(rode);
                    var rodecomp = new Road(Game, rode);
                    this.Components.Add(rodecomp);
                }
                selectNode = null;
                addroad = false;
            }

        }

        void btnroad_Click(object sender, EventArgs e)
        {
            addroad = !addroad;
        }

        void btnnode_Click(object sender, EventArgs e)
        {
            addnode = !addnode;
            selectNode = null;
        }
        public override void Initialize()
        {
            Resource.Load(@"default.def");
            this.Components.Add(btnnode);
            this.Components.Add(btnroad);

            addnode = addroad = false;

            this.Position = Vector2.Zero;
            this.Size = new Size2(Game.GraphicsDevice.BackBuffer.Width, Game.GraphicsDevice.BackBuffer.Width);
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            if (addnode)
                Game.SpriteBatch.DrawString(Game.BasicFont, "addnode", Vector2.UnitY * 25, Color.White);
            if (addroad)
                Game.SpriteBatch.DrawString(Game.BasicFont, "addroad", Vector2.UnitY * 25, Color.White);
            if (selectNode != null)
                Game.SpriteBatch.DrawString(Game.BasicFont, selectNode.Id.ToString(), Vector2.UnitY * 50, Color.White);
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            btnnode.HoverBackground = Content.Load<Texture2D>("ButtonHover.png");
            btnroad.HoverBackground = Content.Load<Texture2D>("ButtonHover.png");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

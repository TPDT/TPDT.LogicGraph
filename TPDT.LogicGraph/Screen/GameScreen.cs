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
        private Button btnnode, btnroad, btnrenode, btnrerode;
        private Map map;
        private int mode;
        private NodeBase selectNode;
        private Texture2D line;
        private RoadDisplayMode roadmode = RoadDisplayMode.Direct;
        public GameScreen(LogicGraph game)
            : base(game)
        {
            Resource = new ResourceManager();
            btnnode = new Button(game, "Button110.png", "Add Node", Vector2.Zero, new Size2(110, 30), null, Color.White);
            btnroad = new Button(game, "Button110.png", "Add Road", new Vector2(110, 0), new Size2(110, 30), null, Color.White);
            btnrenode = new Button(game, "Button150.png", "Remove Node", new Vector2(220, 0), new Size2(150, 30), null, Color.White);
            btnrerode = new Button(game, "Button150.png", "Remove Road", new Vector2(370, 0), new Size2(150, 30), null, Color.White);
            btnnode.Click += btnnode_Click;
            btnroad.Click += btnroad_Click;
            btnrerode.Click += btnrerode_Click;

            this.OnButtonUp += GameScreen_OnButtonUp;

            map = new Map();
        }

        void btnrerode_Click(object sender, EventArgs e)
        {
            mode = 3;
        }

        void GameScreen_OnButtonUp(object sender, EventArgs e)
        {
            if (mode==1)
            {
                var node = NodeBase.CreateNode(Resource.NodeDefinitions[0],
                    new Tuple<float, float>(Game.MouseHelper.Position.X, Game.MouseHelper.Position.Y));
                map.Nodes.Add(node);
                var comp = new Node(Game, node);
                this.Components.Add(comp);
                comp.OnButtonUp += comp_OnButtonUp;
                this.mode = 0;
            }
        }

        void comp_OnButtonUp(object sender, EventArgs e)
        {
            //如果目前没有选择任何节点则选中此节点
            if (selectNode == null)
            {
                selectNode = ((Node)sender).NodeData;
            }
            else
            {
                //否则先寻找两节点之间是否有路
                Node node = ((Node)sender);
                bool flag = true;
                RoadBase rode = null;
                foreach(var r in selectNode.Roads)
                {
                    if (r.Node1 == node.NodeData || r.Node2 == node.NodeData)
                    {
                        flag = false;
                        rode = r;
                        break;
                    }
                }
                //如果此时是新建道路模式 节点间没路 且两节点不同则添加一个新的路
                if (mode == 2)
                {
                    if (selectNode != ((Node)sender).NodeData && flag)
                    {
                        rode = RoadBase.CreateRode(0, selectNode, node.NodeData);
                        rode.RoadDisplayMode = roadmode;
                        map.Roads.Add(rode);
                        var rodecomp = new Road(Game, rode);
                        this.Components.Add(rodecomp);
                    }
                }
                //否则判断是否为道路删除模式 节点间有路 且两节点不同则删除这个路
                else if (mode == 3)
                {
                    if (selectNode != ((Node)sender).NodeData && !flag)
                    {
                        Road res = null;
                         foreach(var com in this.Components)
                         {
                             if (com is Road)
                             {
                                 Road r = com as Road;
                                 if (r.RodeData == rode)
                                 {
                                     res = r;
                                 }
                             }
                         }
                        if(res!=null)
                        {
                            this.Components.Remove(res);
                            map.Roads.Remove(res.RodeData);
                            res.Dispose();
                        }
                    }
                }
                selectNode = null;
                mode = 0;
            }

        }

        void btnroad_Click(object sender, EventArgs e)
        {
            mode = 2;
        }

        void btnnode_Click(object sender, EventArgs e)
        {
            mode = 1;
            selectNode = null;
        }
        public override void Initialize()
        {
            Resource.Load(@"default.def");
            this.Components.Add(btnnode);
            this.Components.Add(btnroad);
            this.Components.Add(btnrenode);
            this.Components.Add(btnrerode);

            mode = 0;

            this.Position = Vector2.Zero;
            this.Size = new Size2(Game.GraphicsDevice.BackBuffer.Width, Game.GraphicsDevice.BackBuffer.Width);
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            if (mode==1)
                Game.SpriteBatch.DrawString(Game.BasicFont, "addnode", Vector2.UnitY * 25, Color.White);
            if (mode==2)
            {
                if (selectNode != null)
                {
                    Vector2 p = new Vector2(selectNode.Position.Item1, selectNode.Position.Item2);
                    Road.DrawLine(p, Game.MouseHelper.Position, line, Game, Color.Green, roadmode);
                }
                Game.SpriteBatch.DrawString(Game.BasicFont, roadmode.ToString(), Vector2.UnitY * 25, Color.White);
            }
            if (selectNode != null)
                Game.SpriteBatch.DrawString(Game.BasicFont, selectNode.Id.ToString(), Vector2.UnitY * 50, Color.White);
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            btnnode.HoverBackground = Content.Load<Texture2D>("Button110_Hover.png");
            btnroad.HoverBackground = Content.Load<Texture2D>("Button110_Hover.png");
            btnrenode.HoverBackground = Content.Load<Texture2D>("Button150_Hover.png");
            btnrerode.HoverBackground = Content.Load<Texture2D>("Button150_Hover.png");
            line = Content.Load<Texture2D>("Line.png");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (mode == 2)
            {
                roadmode = (RoadDisplayMode)Math.Abs(Game.MouseHelper.CurrentState.WheelDelta / 100 % 5);
            }
            base.Update(gameTime);
        }
    }
}

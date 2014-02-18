using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph
{
    public sealed class Node : GameElement
    {
        private Texture2D tex;
        public Node(LogicGraph game, NodeBase data)
            : base(game)
        {
            NodeData = data;
            this.Position = new Vector2(data.Position.Item1, data.Position.Item2);
        }
        public NodeBase NodeData { get; set; }

        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            Game.SpriteBatch.Draw(tex, new SharpDX.Vector2(this.Position.X - 20, this.Position.Y - 20), Color.White);
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            tex = Content.Load<Texture2D>(string.Format("Node_{0}.png", NodeData.Definition.Id));
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(SharpDX.Toolkit.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

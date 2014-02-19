using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph
{
    public sealed class Road : GameElement
    {
        public RoadBase RodeData { get; set; }
        private Texture2D line;
        public Road(LogicGraph game, RoadBase data)
            : base(game)
        {
            RodeData = data;
            this.Position = new Vector2(data.Node1.Position.Item1
                + data.Node2.Position.Item1, data.Node1.Position.Item2 + data.Node2.Position.Item2) / 2;
            this.Size = new Size2();
            this.DrawOrder = 0;
        }
        protected override void LoadContent()
        {
            line = Content.Load<Texture2D>("Line.png");
            base.LoadContent();
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            Vector2 p1=new Vector2(RodeData.Node1.Position.Item1,RodeData.Node1.Position.Item2),
                p2=new Vector2(RodeData.Node2.Position.Item1,RodeData.Node2.Position.Item2);
            Game.SpriteBatch.Draw(line, p1, new Rectangle(0, 0, (int)((p2 - p1).Length()), 4)
                , Color.Green, getAngle(p1, p2), Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        private float getAngle(Vector2 p1, Vector2 p2)
        {
            Vector2 tmp = p2 - p1;
            tmp.Normalize();
            double result = Vector2.Dot(tmp, Vector2.UnitX);
            result = Math.Acos(result);
            if (p1.Y > p2.Y)
                result = -result;
            return (float)result;
        }
    }
}

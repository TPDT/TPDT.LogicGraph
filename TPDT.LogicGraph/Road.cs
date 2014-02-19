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
        private Buffer<VertexPositionColor> line;
        BasicEffect ef;
        public Road(LogicGraph game, RoadBase data)
            : base(game)
        {
            RodeData = data;
            this.Position = new Vector2(data.Node1.Position.Item1
                + data.Node2.Position.Item1, data.Node1.Position.Item2 + data.Node2.Position.Item2) / 2;
            this.Size = new Size2();
            this.DrawOrder = 11;
        }

        public override void Initialize()
        {
            VertexPositionColor[] data = new VertexPositionColor[] {
                new VertexPositionColor(new Vector3(RodeData.Node1.Position.Item1,RodeData.Node1.Position.Item2,0),Color.Red),
                new VertexPositionColor(new Vector3(RodeData.Node2.Position.Item1,RodeData.Node2.Position.Item2,0),Color.Red )};
           
            line = SharpDX.Toolkit.Graphics.Buffer.New<VertexPositionColor>(this.Game.GraphicsDevice, 2, BufferFlags.VertexBuffer);
            line.SetData(data);
            ef = new BasicEffect(this.Game.GraphicsDevice);
            base.Initialize();
        }

        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            this.Game.GraphicsDevice.SetVertexBuffer(line);
            ef = new BasicEffect(GraphicsDevice);
            ef.World = Matrix.Identity;
            ef.View = Matrix.Identity;
            ef.Projection = Matrix.OrthoOffCenterRH(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1);
            ef.VertexColorEnabled = true;
            foreach (EffectPass pass in ef.CurrentTechnique.Passes)
                pass.Apply();
            GraphicsDevice.DrawAuto(PrimitiveType.LineList);
        }
    }
}

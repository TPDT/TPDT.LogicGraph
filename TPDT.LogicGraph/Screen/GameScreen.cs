using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    class GameScreen : Screen
    {
        private Model army1; 
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private BoundingSphere modelBounds;
        public GameScreen(Game game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            // Draw the model
            army1.Draw(GraphicsDevice, world, view, projection);
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            army1 = Content.Load<Model>("Army1");
            BasicEffect.EnableDefaultLighting(army1, true);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            // Calculate the bounds of this model
            modelBounds = army1.CalculateBounds();
            
            // Calculates the world and the view based on the model size
            const float MaxModelSize = 10.0f;
            var scaling = MaxModelSize / modelBounds.Radius;
            view = Matrix.LookAtRH(new Vector3(0, 0, 2147483647), new Vector3(0, 0, 0), Vector3.Up);
            projection = Matrix.PerspectiveFovRH(0.9f, (float)GraphicsDevice.BackBuffer.Width / GraphicsDevice.BackBuffer.Height, 0.1f, MaxModelSize * 10.0f);
            world = Matrix.Identity;
            base.Update(gameTime);
        }
    }
}

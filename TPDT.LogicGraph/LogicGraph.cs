using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    class LogicGraph:Game
    {
        Texture2D tex;
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch sb;
        public LogicGraph()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void LoadContent()
        {
            tex = Content.Load<Texture2D>("Logo.jpg");
            //tex = Texture2D.Load(GraphicsDevice, "Logo.jpg");
            base.LoadContent();
        }
        protected override void Initialize()
        {
            Window.Title = "1213";
            sb = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }
        protected override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}

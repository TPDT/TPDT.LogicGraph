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
        SpriteFont sf;
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch sb;
        private ScreenManager sm;
        public LogicGraph()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            sm = new ScreenManager(this);
            this.GameSystems.Add(sm);
        }
        protected override void LoadContent()
        {
            tex = Content.Load<Texture2D>("Logo.jpg");
            //tex = Texture2D.Load(GraphicsDevice, "Logo.jpg");
            base.LoadContent();
        }
        protected override void Initialize()
        {
            Window.Title = "编程棋 Alpha 0.1";
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

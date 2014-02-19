using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    public class LogicGraph:Game
    {
        Texture2D tex,mouse;
        public SpriteBatch SpriteBatch { get; set; }
        public MouseHelper MouseHelper { get; set; }
        public SpriteFont BasicFont { get; set; }
        private GraphicsDeviceManager graphicsDeviceManager;
        private ScreenManager sm;
        private RenderForm form;
        public LogicGraph()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            sm = new ScreenManager(this);
            MouseHelper = new MouseHelper(this);
            Content.RootDirectory = "Content";
        }
        protected override void LoadContent()
        {
            tex = Content.Load<Texture2D>("Logo.jpg");
            mouse = Content.Load<Texture2D>("Mouse.png");
            BasicFont = this.Content.Load<SpriteFont>("wryh");
            base.LoadContent();
        }
        protected override void Initialize()
        {
            Window.Title = "编程棋 Alpha 0.1";
            this.IsMouseVisible = false;
            Window.AllowUserResizing = true;
            form = Window.NativeWindow as RenderForm;
            form.Icon = System.Drawing.Icon.ExtractAssociatedIcon("TPDT.ico");
            this.GameSystems.Add(MouseHelper);
            this.GameSystems.Add(sm);

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            sm.ToggleScreen(new GameScreen(this));
            base.Initialize();
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            this.SpriteBatch.Begin(SpriteSortMode.Deferred, GraphicsDevice.BlendStates.NonPremultiplied);
            base.Draw(gameTime);
            SpriteBatch.Draw(mouse, MouseHelper.Position, Color.White);
            this.SpriteBatch.End();
        }
    }
}

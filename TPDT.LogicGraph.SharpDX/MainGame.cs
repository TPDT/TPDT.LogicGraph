using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.SharpDXGame
{
    class MainGame : Game2DBase
    {
        public ScreenManager ScreenManager { get; private set; }
        public TextFormat TextFormat { get; private set; }
        public TextLayout TextLayout { get; private set; }
        public MainGame()
        {

        }
        protected override void Initialize(GameConfiguration gameConfiguration)
        {
            base.Initialize(gameConfiguration);            // Initialize a TextFormat

            Components.Add(ScreenManager = new ScreenManager(this));
            ScreenManager.ToggleScreen(new GameScreen(this));

            // Initialize a TextFormat
            TextFormat = new TextFormat(FactoryDWrite, "微软雅黑", 128) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };

            RenderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;

            // Initialize a TextLayout
            TextLayout = new TextLayout(FactoryDWrite, "编程棋 Alpha 0", TextFormat, gameConfiguration.Width, gameConfiguration.Height);

        }
        protected override void Draw(GameTime time)
        {
            // Draw the TextLayout
            base.Draw(time);
            //RenderTarget2D.DrawTextLayout(new Vector2(0, 0), TextLayout, new SolidColorBrush(RenderTarget2D, Color.White), DrawTextOptions.None);
            ScreenManager.Draw(time);
        }
        protected override void Update(GameTime time)
        {
            base.Update(time);
            ScreenManager.Update(time);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        protected override void KeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.KeyDown(e);
        }
        protected override void KeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            base.KeyUp(e);
        }
        protected override void MouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.MouseClick(e);
        }
    }
}

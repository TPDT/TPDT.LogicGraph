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
    class GameScreen : Screen
    {
        public GameScreen(Game2DBase game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            RenderTarget2D.DrawTextLayout(new Vector2(0, 0), new TextLayout(Game.FactoryDWrite, "编程棋 Alpha 0", new TextFormat(Game.FactoryDWrite, "微软雅黑", 128) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center }, Game.RenderTarget2D.Size.Width, Game.RenderTarget2D.Size.Height), new SolidColorBrush(RenderTarget2D, Color.White), DrawTextOptions.None);

        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

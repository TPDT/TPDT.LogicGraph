using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    class ScreenManager : DrawableGameComponent
    {
        public Screen CurrentScreen { get; set; }

        public void ToggleScreen(Screen screen)
        {
            CurrentScreen = screen;
            CurrentScreen.Initialize();
        }

        public ScreenManager(Game game)
            : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (CurrentScreen != null)
                CurrentScreen.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CurrentScreen != null)
                CurrentScreen.Update(gameTime);
        }
    }
}

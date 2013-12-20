using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.SharpDXGame
{
    public class DrawableGameComponent : GameComponent, IDrawable
    {
        // Fields
        public WindowRenderTarget RenderTarget2D { get; set; }   
        public int DrawOrder { get; set; }
        private bool initialized;
        public bool Visible { get; set; }

        // Methods
        public DrawableGameComponent(Game2DBase game)
            : base(game)
        {
            this.Visible = true;
            RenderTarget2D = Game.RenderTarget2D;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.UnloadContent();
            }
            base.Dispose(disposing);
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            if (!this.initialized)
            {
                if (RenderTarget2D != null)
                {
                    this.LoadContent();
                }
            }
            this.initialized = true;
        }

        protected virtual void LoadContent()
        {
        }



        protected virtual void UnloadContent()
        {
        }
    }

    public interface IDrawable
    {
        // Methods
        void Draw(GameTime gameTime);

        // Properties
        int DrawOrder { get; }
        bool Visible { get; }
    }
}

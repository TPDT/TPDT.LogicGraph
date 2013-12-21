using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    public class DrawableGameComponent : GameComponent, IDrawable
    {
        private IGraphicsDeviceService graphicsDeviceService;
        private bool visible;
        private int drawOrder;
        public DrawableGameComponent(Game game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            // Gets the graphics device service
            graphicsDeviceService = (IGraphicsDeviceService)base.Services.GetService(typeof(IGraphicsDeviceService)); 
            base.Initialize();
        }
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        /// <value>The graphics device.</value>
        protected GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDeviceService != null ? graphicsDeviceService.GraphicsDevice : null;
            }
        }
        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public virtual bool BeginDraw()
        {
            return true;
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void EndDraw()
        {
        }

        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }

        public int DrawOrder
        {
            get { return drawOrder; }
            set
            {
                if (drawOrder != value)
                {
                    drawOrder = value;
                    OnDrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        protected virtual void OnDrawOrderChanged(object source, EventArgs e)
        {
            EventHandler<EventArgs> handler = DrawOrderChanged;
            if (handler != null) handler(source, e);
        }

        private void OnVisibleChanged(EventArgs e)
        {
            EventHandler<EventArgs> handler = VisibleChanged;
            if (handler != null) handler(this, e);
        }
    }
}

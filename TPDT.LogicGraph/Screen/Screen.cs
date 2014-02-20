using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    public class Screen : ComponentContainer, IPosition
    {
        private bool laststate, lasthover;

        public Screen(LogicGraph game)
            : base(game)
        {
            this.Screen = null;
            this.Components.ItemAdded += Components_ItemAdded;
            this.Components.ItemRemoved += Components_ItemRemoved;
        }

        void Components_ItemRemoved(object sender, SharpDX.Collections.ObservableCollectionEventArgs<GameComponent> e)
        {
            e.Item.Screen = null;
        }

        void Components_ItemAdded(object sender, SharpDX.Collections.ObservableCollectionEventArgs<GameComponent> e)
        {
            e.Item.Screen = this;
        }

        public override void Update(SharpDX.Toolkit.GameTime gameTime)
        {
            if (this.AbsoluteRectangle.Contains(Game.MouseHelper.Position))
            {
                if (Game.MouseHelper.CurrentState.Left == ButtonState.Pressed)
                {
                    if (laststate)
                    {
                        if (OnPress != null)
                            this.OnPress(this, new EventArgs());
                    }
                    else
                    {
                        if (OnButtonDown != null)
                            this.OnButtonDown(this, new EventArgs());
                    }
                    this.laststate = true;
                }
                else
                {
                    if (laststate)
                    {
                        if (OnButtonUp != null)
                            this.OnButtonUp(this, new EventArgs());
                    }
                    else
                    {
                        if (OnHover != null)
                            this.OnHover(this, new EventArgs());
                    }

                    this.laststate = false;
                }
                lasthover = true;
            }
            else if (lasthover && OnLeave != null)
            {
                this.OnLeave(this, new EventArgs());
                lasthover = false;
            }
            base.Update(gameTime);
        }

        public SharpDX.Vector2 Position { get; set; }

        public SharpDX.Size2 Size { get; set; }


        public SharpDX.Rectangle AbsoluteRectangle
        {
            get
            {
                if (Screen == null)
                    return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
                else
                {
                    return new Rectangle((int)Position.X + Screen.AbsoluteRectangle.Location.X,
                        (int)Position.Y + Screen.AbsoluteRectangle.Location.Y
                        , Size.Width, Size.Height);
                }
            }
        }

        public event EventHandler OnButtonDown;

        public event EventHandler OnButtonUp;

        public event EventHandler OnHover;

        public event EventHandler OnPress;

        public event EventHandler OnLeave;
    }
}

using SharpDX;
using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    public class Screen : ComponentContainer, IPosition
    {
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

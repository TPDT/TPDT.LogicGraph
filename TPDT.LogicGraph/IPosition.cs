using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph
{
    public interface IPosition
    {
        event System.EventHandler OnButtonDown;

        event System.EventHandler OnButtonUp;

        event System.EventHandler OnHover;

        event System.EventHandler OnPress;

        event EventHandler OnLeave;
    
        SharpDX.Vector2 Position
        {
            get;
            set;
        }

        SharpDX.Size2 Size
        {
            get;
            set;
        }

        SharpDX.Rectangle AbsoluteRectangle
        {
            get;
        }
    }
}

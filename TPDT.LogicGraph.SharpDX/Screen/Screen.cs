using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.SharpDXGame
{  
    class Screen:ComponentContainer
    {
        public Screen (Game2DBase game):base(game)
        {
            this.Screen = this;
        }
    }
}

using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph
{
    class LogicGraph:Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        public LogicGraph()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            Window.Title = "1213";

            base.Initialize();
        }
    }
}

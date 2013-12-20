using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPDT.LogicGraph.SharpDXGame
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainGame game = new MainGame();
            game.Run(new GameConfiguration("编程棋 1.0"));
        }
    }
}

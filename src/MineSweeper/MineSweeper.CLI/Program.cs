using MineSweeper.CLI.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new GameController();
            controller.Start();
        }
    }
}

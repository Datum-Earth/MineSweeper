using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineSweeper.CLI.Implementation
{
    internal class MineSweeperView : TraceListener
    {
        public string Header { get; set; }
        private static readonly object _lockObj = new object();

        private string Body { get; set; }

        private void Refresh(string newBody)
        {
            lock (_lockObj)
            {
                this.Body = newBody;
            }

            DrawScreenStrategy();
        }

        public void DrawScreenStrategy()
        {
            Console.Clear();
            Console.WriteLine(this.Header);
            Console.WriteLine(this.Body);
        }

        public override void Write(string message)
        {
            Refresh(message);
        }

        public override void WriteLine(string message)
        {
            Refresh(message);
        }
    }
}

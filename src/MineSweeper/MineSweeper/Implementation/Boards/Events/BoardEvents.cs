using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Implementation.Boards.Events
{
    public delegate EventResult TileClicked(int x, int y);

    public class EventResult
    {
        public ITile[,] UpdatedBoard { get; set; } // TODO: replace this with an actual change tracking system, it's dirty as hell sending the whole board back each time
        public GameStatus Status { get; set; }
        public string Message { get; set; }
    }
}

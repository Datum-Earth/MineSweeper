using MineSweeper.Interfaces;
using System;

namespace MineSweeper.Implementation.Tiles
{
    public class MineTile : ITile
    {
        public bool IsHidden { get; set; }
        public bool IsEmpty { get; set; }
        public int AdjacentTileCount { get; set; }

        public MineTile()
        {
            this.IsHidden = true;
            this.IsEmpty = true;
        }
    }
}

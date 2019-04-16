using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Interfaces
{
    public interface ITile
    {
        bool IsHidden { get; set; }
        bool IsEmpty { get; set; }
        int AdjacentTileCount { get; set; }
    }
}

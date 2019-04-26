using MineSweeper.Implementation.Boards.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Interfaces
{
    public interface IBoard
    {
        EventResult OnClick(int x, int y);
    }
}

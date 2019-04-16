using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Interfaces
{
    public interface IBoard
    {
        void OnClick(int x, int y);
    }
}

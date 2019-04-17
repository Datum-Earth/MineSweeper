using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Implementation.Board
{
    internal class MinesweeperBoard : IBoard
    {
        ITile[,] BoardTiles { get; set; }

        public void OnClick(int x, int y)
        {
            var pos = this.BoardTiles[y, x];

            if (!pos.IsEmpty)
            {
                // game over
            } else if (pos.AdjacentTileCount == 0)
            {
                // fill (expose all blanks)
            } else
            {
                pos.IsHidden = false;
            }
        }

        void FloodFill(int startingY, int startingX)
        {

        }

        int GetTileCountFromBoardSize(BoardSize size)
        {
            switch (size)
            {
                case BoardSize.Small:
                    return (int)Math.Pow(4, 2);
                case BoardSize.Medium:
                    return (int)Math.Pow(5, 2);
                case BoardSize.Large:
                    return (int)Math.Pow(6, 2);
                default:
                    throw new Exception("Invalid board size");
            }
        }
    }
}

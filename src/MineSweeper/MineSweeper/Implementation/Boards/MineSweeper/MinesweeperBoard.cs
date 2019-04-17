using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;
using System.Diagnostics;
using MineSweeper.Implementation.Board.Generators;

namespace MineSweeper.Implementation.Boards
{
    internal class MineSweeperBoard : IBoard
    {
        public ITile[,] BoardTiles { get; private set; }
        MineSweeperBoardHelper BoardHelper { get; set; }

        public MineSweeperBoard()
        {
            this.BoardHelper = new MineSweeperBoardHelper();
        }

        public void Start(BoardSize size)
        {
            var gen = new MineSweeperBoardGenerator(GetTileCountFromBoardSize(size));
            this.BoardTiles = gen.GetBoard();
        }

        public void OnClick(int x, int y)
        {
            if (!this.BoardTiles.PositionExistsAt(y, x))
            {
                Trace.WriteLine("Invalid position.");
                return;
            }

            var pos = this.BoardTiles[y, x];

            if (!pos.IsEmpty)
            {
                Trace.WriteLine("Game over!"); // TODO: actually implement a game over
            } else if (pos.AdjacentTileCount == 0)
            {
                FloodFill(y, x);
            } else
            {
                pos.IsHidden = false;
            }
        }

        void FloodFill(int startingY, int startingX)
        {
            var adjacentTiles = this.BoardHelper.FindAdjacentTilePositions(this.BoardTiles, startingY, startingX);

            foreach (var pos in adjacentTiles)
            {
                var tile = this.BoardTiles[pos.Item1, pos.Item2];

                if (!tile.IsEmpty)
                    return;
                if (tile.AdjacentTileCount != 0)
                {
                    tile.IsHidden = false;
                    return;
                } else
                {
                    FloodFill(pos.Item1, pos.Item2);
                }
            }
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

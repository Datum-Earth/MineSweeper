using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;
using System.Diagnostics;
using MineSweeper.Implementation.Board.Generators;
using MineSweeper.Implementation.Boards.Events;
using System.Threading.Tasks;

namespace MineSweeper.Implementation.Boards
{
    public class MineSweeperBoard : IBoard
    {
        public ITile[,] BoardTiles { get; private set; }
        MineSweeperBoardHelper BoardHelper { get; set; }

        public MineSweeperBoard()
        {
            this.BoardHelper = new MineSweeperBoardHelper();
        }

        public void Start(BoardSize size)
        {
            var tileCount = GetTileCountFromBoardSize(size);
            var bombCount = tileCount / 6;
            var gen = new MineSweeperBoardGenerator(tileCount, bombCount);
            this.BoardTiles = gen.GetBoard();
        }

        public EventResult OnClick(int y, int x)
        {
            if (!this.BoardTiles.PositionExistsAt(y, x))
            {
                return new EventResult() { UpdatedBoard = this.BoardTiles, Status = GameStatus.InProgress, Message = "Invalid position." };
            }

            var pos = this.BoardTiles[y, x];

            if (!pos.IsEmpty)
            {
                ShowAllBombs();
                return new EventResult() { UpdatedBoard = this.BoardTiles, Status = GameStatus.Lost };
            } else
            {
                FloodFill(y, x);

                var onlyBombsLeftHidden = this.BoardTiles.Where(tile => tile.IsHidden && tile.IsEmpty).Count() == 0 ? true : false;

                if (onlyBombsLeftHidden)
                {
                    ShowAllBombs();
                    return new EventResult() { UpdatedBoard = this.BoardTiles, Status = GameStatus.Won };
                }

                return new EventResult() { UpdatedBoard = this.BoardTiles, Status = GameStatus.InProgress };
            }

        }

        void ShowAllBombs()
        {
            foreach (var tile in this.BoardTiles)
            {
                if (!tile.IsEmpty && tile.IsHidden)
                    tile.IsHidden = false;
            }
        }

        void FloodFill(int y, int x)
        {
            if (!this.BoardTiles.PositionExistsAt(y, x))
                return;
            
            var tile = this.BoardTiles[y, x];

            if (!tile.IsHidden)
                return;

            tile.IsHidden = false;
            if (BombPresent(y, x))
                return;

            FloodFill(y - 1, x - 1); //upper left
            FloodFill(y - 1, x); // upper middle
            FloodFill(y -1, x + 1); // upper right
            FloodFill(y, x - 1); // middle left
            FloodFill(y, x + 1); // middle right
            FloodFill(y + 1, x - 1); // bottom left
            FloodFill(y + 1, x); // bottom middle
            FloodFill(y + 1, x + 1); // bottom right
        }

        bool BombPresent(int y, int x)
        {
            var adjacentTiles = this.BoardHelper.FindAdjacentTilePositions(this.BoardTiles, y, x).ToArray();

            int bombCount = 0;
            foreach (var adjTile in adjacentTiles)
            {
                if (!this.BoardTiles[adjTile.Item1, adjTile.Item2].IsEmpty)
                    bombCount++;
            }

            if (bombCount != 0)
                return true;
            else
                return false;
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

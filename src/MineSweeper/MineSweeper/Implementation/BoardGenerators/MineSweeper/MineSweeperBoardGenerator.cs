using MineSweeper.Implementation.Enum;
using MineSweeper.Implementation.Tiles;
using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MineSweeper.Implementation.Board.Generators
{
    internal class MineSweeperBoardGenerator : BaseGenerator
    {
        int BombCount { get; set; }

        public MineSweeperBoardGenerator(int size) : base(size)
        {
            int bombCount = this.TileCount / 4;
            Initialize(size, bombCount);
        }

        public MineSweeperBoardGenerator(int size, int bombCount) : base(size)
        {
            Initialize(size, bombCount);
        }

        void Initialize(int size, int bombCount)
        {
            this.BombCount = bombCount;
            this.GenerateBoardTiles<MineTile>(size);
            PopulateBombs();
            SetAllAdjacentValues(this.Board);
        }

        public ITile[,] GetBoard()
        {
            return this.Board;
        }

        void SetAllAdjacentValues(ITile[,] boardTiles)
        {
            for (int y = 0; y < boardTiles.GetLength(0); y++)
            {
                for (int x = 0; x < boardTiles.GetLength(1); x++)
                {
                    SetAdjacentValues(y, x, boardTiles, tile => tile.IsEmpty);
                }
            }
        }

        void SetAdjacentValues(int tileY, int tileX, ITile[,] boardTiles, Func<ITile, bool> condition)
        {
            int relativeStartingY = tileY - 1;
            int relativeStartingX = tileX - 1;
            int adjacentCount = 0;

            for (int y = relativeStartingY; y < relativeStartingY + 3; y++)
            {
                for (int x = relativeStartingX; x < relativeStartingX + 3; x++)
                {
                    if (boardTiles.PositionExistsAt(y, x))
                        if (!condition(boardTiles[y, x]))
                            adjacentCount++;
                }
            }

            boardTiles[tileY, tileX].AdjacentTileCount = adjacentCount;
        }

        void PopulateBombs()
        {
            for (int i = 0; i < this.BombCount; i++)
            {
                var pos = GetRandomBombPosition();
                this.Board[pos.Item1, pos.Item2].IsEmpty = false;
            }
        }

        Tuple<int, int> GetRandomBombPosition()
        {
            var pos = this.GetRandomBoardPosition();
            int randX = pos.Item1;
            int randY = pos.Item2;

            if (IsBomb(randX, randY))
            {
                var newPos = GetRandomBombPosition();
                randX = newPos.Item1;
                randY = newPos.Item2;
            }

            return new Tuple<int, int>(randX, randY);
        }

        bool IsBomb(int x, int y)
        {
            return this.Board[x, y].IsEmpty ? false : true;
        }
    }
}
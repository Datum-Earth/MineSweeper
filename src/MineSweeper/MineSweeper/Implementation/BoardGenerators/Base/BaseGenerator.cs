using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper.Implementation.Board.Generators
{
    internal class BaseGenerator
    {
        protected ITile[,] Board { get; private set; }

        protected int TileCount { get; set; }

        // have to make as a property on the class because of issues using it with recursion.
        // evidently the default seed used is based on the system clock, so calling it recursively
        // if we have a dupe (which we will, since it's being called so quickly it's using the same seed
        // and being given the same params) leads to a stack overflow.
        private readonly Random BoardPosRandom; 

        public BaseGenerator(int boardSize)
        {
            this.TileCount = boardSize;
            this.BoardPosRandom = new Random();
        }

        private void ValidateBoardSize(int tileCount)
        {
            if (tileCount < 4)
                throw new ArgumentException("Board size cannot be less than four.");

            double root = Math.Sqrt(tileCount);
            bool isSquare = root == (int)root;

            if (!isSquare)
                throw new ArgumentException("Board shape must be a square.");
        }

        protected void GenerateBoardTiles<T>(int tileCount) where T : ITile, new()
        {
            ValidateBoardSize(tileCount);

            var length = (int)Math.Sqrt(tileCount);
            this.Board = new ITile[length, length];

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    this.Board[x, y] = new T();
                }
            }
        }

        protected virtual Tuple<int, int> GetRandomBoardPosition()
        {
            var randomX = BoardPosRandom.Next(0, (int)Math.Sqrt(this.TileCount));
            var randomY = BoardPosRandom.Next(0, (int)Math.Sqrt(this.TileCount));

            return new Tuple<int, int>(randomX, randomY);
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Implementation.Board.Generators;
using MineSweeper.Interfaces;

namespace MineSweeper.Test.BoardGenerator
{
    [TestClass]
    public class MineSweeperBoardGeneratorTests
    {
        [TestMethod]
        [TestProperty("TestType", "Automated")]
        public void BoardGeneratorConstructs()
        {
            var gen = new MineSweeperBoardGenerator(16);
        }

        [TestMethod]
        [TestProperty("TestType", "Automated")]
        public void BombValuesAreCorrect()
        {
            var random = new Random(); // creating here since creating randoms during iteration or recursion is a bad idea

            for (int i = 0; i < 20; i++)
            {
                int boardSize = GetRandomBoardSize(random);
                int mineCount = random.Next(0, boardSize);
                var boardGen = new MineSweeperBoardGenerator(boardSize, mineCount);
                var board = boardGen.GetBoard();

                var actualMineCount = board.Select(x => !x.IsEmpty).Count();

                if (actualMineCount != mineCount)
                {
                    Assert.Fail($"Mine count mismatch.\nExpected: {mineCount}\nActual: {actualMineCount}");
                }
                
            }
        }

        [TestMethod]
        [TestProperty("TestType", "Automated")]
        public void AdjacentValuesAreCorrect()
        {
            var smallBombGen = new MineSweeperBoardGenerator(36, 1);
            var smallBombBoard = smallBombGen.GetBoard();

            TraceBoard(smallBombBoard);

            foreach (var tile in smallBombBoard)
            {
                if (tile.AdjacentTileCount > 1)
                    Assert.Fail();
            }

            var manyBombGen = new MineSweeperBoardGenerator(4, 3);
            var manyBombBoard = manyBombGen.GetBoard();

            TraceBoard(manyBombBoard);

            foreach (var tile in manyBombBoard)
            {
                if (tile.IsEmpty && tile.AdjacentTileCount < 3)
                    Assert.Fail();
            }

        }

        [TestMethod]
        [TestProperty("TestType", "Manual")]
        public void TraceRandomBoard()
        {
            var gen = new MineSweeperBoardGenerator(36);
            var board = gen.GetBoard();

            TraceBoard(board);
        }

        void TraceBoard(ITile[,] board)
        {
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    var elementAtPos = board[y, x];

                    if (!elementAtPos.IsEmpty)
                        Trace.Write("X");
                    else if (elementAtPos.AdjacentTileCount != 0)
                        Trace.Write(elementAtPos.AdjacentTileCount);
                    else
                        Trace.Write("\u25A1");

                    Trace.Write("\t");
                }

                Trace.Write("\n");
            }

            Trace.WriteLine("");
        }

        // TODO: Make this less shitty
        int GetRandomBoardSize(Random rand)
        {
            var attemptVal = rand.Next(2, 30);
            var squared = Math.Pow(attemptVal, 2);

            bool isSquare = squared == (int)squared;

            if (!isSquare)
                return GetRandomBoardSize(rand);
            else
                return (int)squared;
        }
    }
}

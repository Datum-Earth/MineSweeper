using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Implementation.Board.Generators;
using MineSweeper.Implementation.Boards;
using MineSweeper.Interfaces;
using static MineSweeper.Test.Helpers.ResultHelper;

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

                var actualMineCount = board.Where(x => !x.IsEmpty).Count();

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
        [TestProperty("TestType", "Automated")]
        public void FindAdjacentDoesNotAllowNegativeStartingPoints()
        {
            var gen = new MineSweeperBoardGenerator(36, 6);
            var board = gen.GetBoard();
            var helper = new MineSweeperBoardHelper();

            var adjacentTileCount = helper.FindAdjacentTilePositions(board, -1, -1).Count();

            if (adjacentTileCount != 0)
                Assert.Fail();
        }

        [TestMethod]
        [TestProperty("TestType", "Manual")]
        public void TraceRandomBoard()
        {
            var gen = new MineSweeperBoardGenerator(36);
            var board = gen.GetBoard();

            TraceBoard(board);
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Implementation.Board.Generators;
using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;

namespace MineSweeper.Test.BoardGenerator
{
    [TestClass]
    public class BoardGeneratorTests
    {
        [TestMethod]
        [TestProperty("TestType", "Automated")]
        public void BoardGeneratorConstructs()
        {
            var smallGen = new TestBoardGenerator(16);
            var medGen = new TestBoardGenerator(25);
            var largeGen = new TestBoardGenerator(36);
        }

        [TestMethod]
        [TestProperty("TestType", "Automated")]
        public void BoardGeneratorOnlyAllowsSquares()
        {
            var gen = new TestBoardGenerator(0);

            // do not allow less than four
            if (gen.GenerateTilesByCount(0))
                Assert.Fail();
            if (gen.GenerateTilesByCount(17))
                Assert.Fail();
        }

        class TestBoardGenerator : BaseGenerator
        {
            public TestBoardGenerator(int size) : base(size) { }

            public bool GenerateTilesByCount(int tileCount)
            {
                try
                {
                    this.GenerateBoardTiles<TestTile>(tileCount);
                    return true;
                } catch (ArgumentException)
                {
                    return false;
                }
            }
        }

        class TestTile : ITile
        {
            public bool IsHidden { get; set; }
            public bool IsEmpty { get; set; }
            public int AdjacentTileCount { get; set; }
        }
    }
}

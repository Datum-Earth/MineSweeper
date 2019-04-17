using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Implementation.Boards;
using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;

namespace MineSweeper.Test.Boards
{
    [TestClass]
    public class MineSweeperBoardTests
    {
        [TestMethod]
        [TestCategory("Boards")]
        [TestProperty("TestType", "Automated")]
        public void BoardFloodFills()
        {
            var board = new MineSweeperBoard();
            board.Start(BoardSize.Medium);
            var blankTile = GetTilesWithPositions(board).Where(x => x.Tile.AdjacentTileCount == 0).First();

            board.OnClick(blankTile.Position.Item2, blankTile.Position.Item1);
        }

        IEnumerable<TileWithPosition> GetTilesWithPositions(MineSweeperBoard board)
        {
            for (int y = 0; y < board.BoardTiles.GetLength(0); y++)
            {
                for (int x = 0; x < board.BoardTiles.GetLength(1); x++)
                {
                    yield return new TileWithPosition() { Position = new Tuple<int, int>(y, x), Tile = board.BoardTiles[y, x] };
                }
            }
        }

        class TileWithPosition
        {
            public Tuple<int, int> Position { get; set; }
            public ITile Tile { get; set; }
        }
    }
}

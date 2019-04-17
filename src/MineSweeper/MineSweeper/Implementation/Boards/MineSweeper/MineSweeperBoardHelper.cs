using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Implementation.Boards
{
    internal class MineSweeperBoardHelper
    {
        public IEnumerable<Tuple<int, int>> FindAdjacentTilePositions(ITile[,] board, int tileY, int tileX)
        {
            if (tileY < 0 || tileX < 0)
                yield break;

            int relativeStartingY = tileY - 1;
            int relativeStartingX = tileX - 1;

            for (int y = relativeStartingY; y < relativeStartingY + 3; y++)
            {
                for (int x = relativeStartingX; x < relativeStartingX + 3; x++)
                {
                    if (board.PositionExistsAt(y, x))
                        yield return new Tuple<int, int>(y, x);
                }
            }
        }
    }
}

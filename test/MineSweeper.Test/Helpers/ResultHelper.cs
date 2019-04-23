using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Test.Helpers
{
    internal static class ResultHelper
    {
        public static void TraceBoard(ITile[,] board, bool showHidden = true)
        {
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    var elementAtPos = board[y, x];

                    if (showHidden)
                    {
                        if (!elementAtPos.IsEmpty)
                            Trace.Write("X");
                        else if (elementAtPos.AdjacentTileCount != 0)
                            Trace.Write(elementAtPos.AdjacentTileCount);
                        else
                            Trace.Write("_");
                    } else
                    {
                        if (!elementAtPos.IsHidden)
                        {
                            if (!elementAtPos.IsEmpty)
                                Trace.Write("X");
                            else if (elementAtPos.AdjacentTileCount != 0)
                                Trace.Write(elementAtPos.AdjacentTileCount);
                            else
                                Trace.Write("_");
                        } else
                        {
                            Trace.Write("\u25A1");
                        }
                    }

                    Trace.Write("\t");
                }

                Trace.Write("\n");
            }

            Trace.WriteLine("");
        }
    }
}

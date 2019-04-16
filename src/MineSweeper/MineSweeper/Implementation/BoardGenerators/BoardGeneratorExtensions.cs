using MineSweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    public static partial class Extensions
    {
        public static IEnumerable<ITile> Select(this ITile[,] matrix, Func<ITile, bool> equalityComparer)
        {
            foreach (var tile in matrix)
            {
                if (equalityComparer(tile))
                    yield return tile;
            }
        }

        public static bool PositionExistsAt<T>(this T[,] matrix, params int[] positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (positions[i] >= matrix.GetLength(i) || positions[i] < 0)
                    return false;
            }

            return true;
        }
    }
}

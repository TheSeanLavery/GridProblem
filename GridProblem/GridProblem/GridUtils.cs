using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GridProblem
{
    class GridUtils
    {
        /// <summary>
        /// Gets the grid size, assuming a square.
        /// Will throw an error if not square.
        /// </summary>
        /// <param name="pointCount"></param>
        /// <returns></returns>
        public static int GetGridSize(int pointCount)
        {
            double squareSize = Math.Sqrt(pointCount);
            //check if square is a whole number
            if (squareSize % 1 != 0)
            {
                throw new Exception("Grid must be square, " +
                    "please check the data file and make sure it is a square number");
            }
            return (int)squareSize;
        }
        public static void CreateRows(int squareSize, List<List<Vector2>> rows, List<List<Vector2>> columns)
        {
            for (int y = 0; y < squareSize; y++)
            {
                List<Vector2> row = new List<Vector2>();
                for (int x = 0; x < squareSize; x++)
                {
                    Vector2 point = columns[x][y];
                    row.Add(point);
                }
                rows.Add(row);
            }
        }
        /// <summary>
        /// Creates a list of a list of Vector2s representing the columns.
        /// Each list of Vector2s represents a column
        /// </summary>
        /// <param name="points"></param>
        /// <param name="squareSize"></param>
        /// <param name="columns"></param>
        public static void CreateColumns(List<Vector2> points, int squareSize, List<List<Vector2>> columns)
        {
            //Runs while there are still points in the list
            while (points.Count > 0)
            {
                List<Vector2> Column = GetColumn(points, squareSize);
                columns.Add(Column);
            }
        }

        static List<Vector2> GetColumn(List<Vector2> points, int count)
        {
            List<Vector2> result = new List<Vector2>();
            Vector2 startingPoint = MathUtils.FindTopLeftPoint(points);
            result.Add(startingPoint);
            points.Remove(startingPoint);
            for (int i = 0; i < count - 1; i++)
            {
                Vector2 closest = MathUtils.GetClosestPointWithVerticalBias(startingPoint, points);
                result.Add(closest);
                startingPoint = closest;
                points.Remove(closest);
            }
            return result;
        }

    }
}

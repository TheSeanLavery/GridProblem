using System;
using System.Collections.Generic;
using System.Numerics;

namespace GridProblem
{
    public class GridUtils
    {
        /// <summary>
        /// Gets the grid size, assuming a square.
        /// Will throw an error if not square.
        /// </summary>
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
        public static List<List<Vector2>> CreateRows(int squareSize, List<List<Vector2>> columns)
        {
            List<List<Vector2>> rows = new List<List<Vector2>>();
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
            return rows;
        }
        /// <summary>
        /// Creates a list of a list of Vector2s representing the columns.
        /// Each list of Vector2s represents a column
        /// </summary>
        public static List<List<Vector2>> CreateColumns(List<Vector2> points, int squareSize)
        {
            List<List<Vector2>> columns = new List<List<Vector2>>();

            //Runs while there are still points in the list
            while (points.Count > 0)
            {
                List<Vector2> Column = GetColumn(points, squareSize);
                columns.Add(Column);
            }
            return columns;
        }
        /// <summary>
        /// Gets a List of Vec2 for one column of data
        /// </summary>

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
        static bool IsClose(float f1,float f2,float error = 3)
        {
            if(Math.Abs(f1-f2)< error)
            {
                return true;
            }
            return false;
        }
        static Vector2 GetClosestPointAlongAngle(Vector2 point, List<Vector2> points, float angle)
        {
            Vector2 closestPoint = new Vector2();
            float distance;
            float closestDistance = float.MaxValue;
            foreach (Vector2 p in points)
            {
                float newAngle = (float)MathUtils.AngleBetween(point, p);

                if (!IsClose(newAngle, angle)) continue;

                if (p == point) continue;
                distance = Vector2.Distance(point, p);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = p;
                }
            }
            return closestPoint;
        }
    }
}

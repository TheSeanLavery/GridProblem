using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace GridProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            List<Vector2> points = FileUtils.ReadPointsFromFile(fileName);
            int squareSize = GridUtils.GetGridSize(points.Count());
            List<List<Vector2>> rows = new List<List<Vector2>>();
            List<List<Vector2>> columns = new List<List<Vector2>>();

            Vector2 topLeftPoint = FindTopLeftPoint(points);

            CreateColumns(points, squareSize, columns);
            CreateRows(squareSize, rows, columns);

            for (int i = 0; i < rows.Count; i++)
            {
                List<Vector2> row = rows[i];
                WriteRow(i, row);
            }
            for (int i = 0; i < columns.Count; i++)
            {
                List<Vector2> column = columns[i];
                WriteColumn(i, column);
            }
            Vector2 line1 = columns[1][0] - columns[0][0];
            Vector2 line2 = new Vector2(1, 0);
            Console.WriteLine("Alpha = " + AngleBetween(line1, line2) + " degrees");

            //Console.WriteLine(A)
            //This should work as 9 is a square number
            //int testSquareSize = GetGridSize(9); 

            //This should cause an exception as 20 is not a square number
            //int testSquareSize = GetGridSize(9); 

            Console.ReadLine();
        }

        private static void CreateRows(int squareSize, List<List<Vector2>> rows, List<List<Vector2>> columns)
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

        private static void CreateColumns(List<Vector2> points, int squareSize, List<List<Vector2>> columns)
        {
            while (points.Count > 0)
            {
                List<Vector2> Column = GetColumn(points, squareSize);
                columns.Add(Column);
            }
        }

        public static double AngleBetween(Vector2 vector1, Vector2 vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }
        static List<Vector2> GetColumn(List<Vector2> points, int count)
        {
            List<Vector2> result = new List<Vector2>();
            Vector2 startingPoint = MathUtils.FindTopLeftPoint(points);
            result.Add(startingPoint);
            points.Remove(startingPoint);
            for (int i = 0; i < count-1; i++)
            {
                Vector2 closest = GetClosestPointWithVerticalBias(startingPoint, points);
                result.Add(closest);
                startingPoint = closest;
                points.Remove(closest);
            }
            return result;
        }
        
        static Vector2 GetClosestPoint(Vector2 point, List<Vector2> points)
        {
            Vector2 closestPoint = new Vector2() ;
            float distance;
            float closestDistance = float.MaxValue;
            foreach(Vector2 p in points)
            {
                if (p == point) continue;
                distance = Vector2.Distance(point, p);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = p;
                }
            }
            return closestPoint;
        }
        static Vector2 GetClosestPointWithVerticalBias(Vector2 point, List<Vector2> points)
        {
            Vector2 closestPoint = new Vector2();
            List<Vector2> tempPoints = new List<Vector2>();
            tempPoints.AddRange(points);

            float stretchValue = 50;

            Vector2 stretchedOriginalPoint = new Vector2();
            stretchedOriginalPoint.X = point.X * stretchValue;
            stretchedOriginalPoint.Y = point.Y;

            float distance;
            float closestDistance = float.MaxValue;
            foreach (Vector2 p in tempPoints)
            {
                if (p == point) continue;
                Vector2 stretchedPoint = new Vector2();
                stretchedPoint.X = p.X * stretchValue;
                stretchedPoint.Y = p.Y;
                
                distance = Vector2.Distance(point, stretchedPoint);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = p;
                }
            }
            return closestPoint;
        }
        static void WriteColumn(int column, List<Vector2> points)
        {
            string value = String.Format("Col {0}: ",column);

            for(int i = 0; i< points.Count();i++)
            {
                Vector2 point = points[i];

                //if last point don't add hyphen
                if(i == points.Count()-1)
                {
                    value += point.X + "," + point.Y ;
                } else
                {
                    value += point.X + "," + point.Y + " - ";
                }
            }
            Console.WriteLine(value);
        }
        static void WriteRow(int row, List<Vector2> points)
        {
            string value = String.Format("Row {0}: ", row);
            for (int i = 0; i < points.Count(); i++)
            {
                Vector2 point = points[i];

                //if last point don't add hyphen
                if (i == points.Count() - 1)
                {
                    value += point.X + "," + point.Y;
                }
                else
                {
                    value += point.X + "," + point.Y + " - ";
                }
            }
            Console.WriteLine(value);
        }
    }
}

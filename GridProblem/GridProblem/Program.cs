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


            GridUtils.CreateColumns(points, squareSize, columns);
            GridUtils.CreateRows(squareSize, rows, columns);
            WriteRows(rows);
            WriteColumns(columns);
            WriteAlpha(columns);

            //Console.WriteLine(A)
            //This should work as 9 is a square number
            //int testSquareSize = GetGridSize(9); 

            //This should cause an exception as 20 is not a square number
            //int testSquareSize = GetGridSize(9); 

            Console.ReadLine();
        }

        private static void WriteAlpha(List<List<Vector2>> columns)
        {
            Vector2 line1 = columns[1][0] - columns[0][0];
            Vector2 line2 = new Vector2(1, 0);
            Console.WriteLine("Alpha = " + MathUtils.AngleBetween(line1, line2) + " degrees");
        }

        private static void WriteColumns(List<List<Vector2>> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                List<Vector2> column = columns[i];
                WriteColumn(i, column);
            }
        }

        private static void WriteRows(List<List<Vector2>> rows)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                List<Vector2> row = rows[i];
                WriteRow(i, row);
            }
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

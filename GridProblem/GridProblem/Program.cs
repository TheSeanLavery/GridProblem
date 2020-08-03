using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GridProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
            string fileName = args[0];

            List<Vector2> points = ReadPointsFromFile(fileName);

            //   int squareSize = GetGridSize(points.Count());


            //This should work as 9 is a square number
            //int testSquareSize = GetGridSize(9);

            //This should cause an exception as 20 is not a square number
            //int testSquareSize = GetGridSize(9); 

            Console.ReadLine();
        }
        static int GetGridSize(int pointCount)
        {
            double squareSize = Math.Sqrt(pointCount);

            //check if square is a whole number
            if(squareSize % 1 != 0)
            {
                throw new Exception("Grid must be square, " +
                    "please check the data file and make sure it is a square number");
            }
            return (int)squareSize;
        }
        static List<Vector2> ReadPointsFromFile(string path)
        {
            List<Vector2> points = new List<Vector2>();


            return points;

        }
        static void WriteColumn(int column, params Vector2[] points)
        {
            string value = String.Format("Row {0}: ",column);

            for(int i = 0; i< points.Count();i++)
            {
                Vector2 point = points[i];

                //if last point don't add hyphen
                if(i == points.Count()-1)
                {
                    value += point.X + "," + point.Y + " - ";
                } else
                {
                    value += point.X + "," + point.Y;
                }
            }

            Console.WriteLine(value);

        }
        static void WriteRow()
        {


        }
    }
}

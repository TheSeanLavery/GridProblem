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
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
            string fileName = args[0];

            List<Vector2> points = ReadPointsFromFile(fileName);

            int squareSize = GetGridSize(points.Count());

            List<List<Vector2>> rows = new List<List<Vector2>>();
            List<List<Vector2>> columns = new List<List<Vector2>>();

            foreach(Vector2 p in points)
            {
                _ = GetClosestPoint(p, points);
            }

            Vector2 topLeftPoint = FindTopLeftPoint(points);

            //This should work as 9 is a square number
            //int testSquareSize = GetGridSize(9);

            //This should cause an exception as 20 is not a square number
            //int testSquareSize = GetGridSize(9); 

            Console.ReadLine();
        }
        static Vector2 FindTopLeftPoint(List<Vector2> points)
        {
            Vector2 topLeftCommon = new Vector2(float.MaxValue,0);
            Vector2 topLeftPoint = new Vector2();

            foreach(Vector2 p in points)
            {
                if (p.X < topLeftCommon.X) topLeftCommon.X = p.X;
                if (p.Y > topLeftCommon.Y) topLeftCommon.Y = p.Y;
            }
            float distance = float.MaxValue;
            foreach(Vector2 p in points)
            {
                float newDistance = Vector2.Distance(topLeftCommon, p);
                if (newDistance < distance)
                {
                    topLeftPoint = p;
                    distance = newDistance;
                }
            }
            return topLeftPoint;

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

            string line;

            StreamReader file = new StreamReader(path);

            while((line = file.ReadLine()) != null)
            {
                Vector2 point = new Vector2();
                string[] values = line.Split(',');

                int count = 0;
                foreach(var v in values)
                {
                    Double output;
                    Double.TryParse(v, out output);
                    if (count == 0)
                    {
                        point.X = (float)output;
                    } else 
                    {
                        point.Y = (float)output;
                    }
                    count++;
                    
                    Console.WriteLine(v);
                }
                Console.WriteLine(line);
                points.Add(point);
            }

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

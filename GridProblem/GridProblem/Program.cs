using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;

namespace GridProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create empty lists for rows and columns
            List<List<Vector2>> rows = new List<List<Vector2>>();
            List<List<Vector2>> columns = new List<List<Vector2>>();

            //List<List<Vector2>> debugColumns = GenerateGrid(new GridParams());

            //List<Vector2> flatList = UnpackColumns(debugColumns);

            //Get the filename from the arguments
            string fileName = args[0];

            //Create list of points from the data file of tuples
            List<Vector2> points = FileUtils.ReadPointsFromFile(fileName);

            //Calculate the square size of the grid from the points count
            int squareSize = GridUtils.GetGridSize(points.Count());

            //Analyze data and extract columns
            columns = GridUtils.CreateColumns(points, squareSize);

            //Use column data and extract rows
            rows = GridUtils.CreateRows(squareSize, columns);

            //Write calculated rows to console
            OutputUtils.WriteRows(rows);

            //Write Columns to console
            OutputUtils.WriteColumns(columns);

            //Write the alpha angle to console
            OutputUtils.WriteAlpha(columns);

            Console.WriteLine(" ");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
       
        
        
    }
    public class GridParams
    {
        public int squareSize = 5;
        public int gridTicDistance = 5;
        public int seed = 1337;
        public int coordMin = -50;
        public int coordMax = 50;
        public int angleMin = 0;
        public int angleMax = 45;
    }
}

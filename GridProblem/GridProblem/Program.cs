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

            List<List<Vector2>> debugColumns = GenerateGrid(new GridParams());

            List<Vector2> flatList = UnpackColumns(debugColumns);

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
        public static List<Vector2> UnpackColumns(List<List<Vector2>> cols)
        {
            List<Vector2> vecs = new List<Vector2>();
            foreach(List<Vector2> v in cols)
            {
                vecs.AddRange(v);
            }
            return vecs;
        }
        public static List<List<Vector2>> GenerateGrid(GridParams grid)
        {
            List<Vector2> generated = new List<Vector2>();

            Matrix result = new Matrix();
            Random rand = new Random(grid.seed);

            Vector2 startingPoint = new Vector2();
            startingPoint.X = rand.Next(grid.coordMin, grid.coordMax);
            startingPoint.Y = rand.Next(grid.coordMin, grid.coordMax);

            Vector2 rightPoint = new Vector2();
            rightPoint.X = startingPoint.X + grid.gridTicDistance;
            rightPoint.Y = startingPoint.Y;

            float angle = rand.Next(grid.angleMin, grid.angleMax);
            rightPoint = RotateAboutOrigin(rightPoint, startingPoint, DegreesToRadians(angle));

            generated.Add(startingPoint);
            generated.Add(rightPoint);

            Vector2 bottomPoint = new Vector2();
            bottomPoint.X = rightPoint.X;
            bottomPoint.Y = rightPoint.Y;

            bottomPoint = RotateAboutOrigin(bottomPoint, startingPoint, DegreesToRadians(-90));
            generated.Add(bottomPoint);


            List<List<Vector2>> columns = new List<List<Vector2>>();

            //col.Add(startingPoint);
            //col.Add(bottomPoint);

            Vector2 columnAngle =  bottomPoint - startingPoint;
            Vector2 rowAngle = rightPoint - startingPoint;

            Vector2 nextVec = new Vector2();
            nextVec.X = startingPoint.X;
            nextVec.Y = startingPoint.Y;
            for(int x = 0; x < grid.squareSize; x++)
            {
                List<Vector2> col = new List<Vector2>();

                for (int y = 0; y < grid.squareSize; y++)
                {
                    col.Add(nextVec);
                    nextVec += columnAngle;
                }
                nextVec = startingPoint + (rowAngle * (x+1));
                columns.Add(col);
            }
            

            return columns;

            //result.RotateAt(angle, startingPoint);
            
            //generated.Add()
            
        }
        public static float DegreesToRadians(float degrees)
        {
            float radians = (float)((Math.PI / 180) * degrees);
            return (radians);
        }
        public static Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float rotation)
        {
            return Vector2.Transform(point - origin, Matrix3x2.CreateRotation(rotation)) + origin;
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

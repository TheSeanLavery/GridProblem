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
            List<List<Vector2>> rows = new List<List<Vector2>>();
            List<List<Vector2>> columns = new List<List<Vector2>>();

            string fileName = args[0];
            List<Vector2> points = FileUtils.ReadPointsFromFile(fileName);
            int squareSize = GridUtils.GetGridSize(points.Count());

            GridUtils.CreateColumns(points, squareSize, columns);
            GridUtils.CreateRows(squareSize, rows, columns);

            OutputUtils.WriteRows(rows);
            OutputUtils.WriteColumns(columns);
            OutputUtils.WriteAlpha(columns);

            Console.ReadLine();
        }
    }
}

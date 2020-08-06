using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace GridProblem
{
    class FileUtils
    {
        /// <summary>
        /// Reads tuples from file into Vec2 List
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Vector2> ReadPointsFromFile(string path)
        {
            List<Vector2> points = new List<Vector2>();
            string line;
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                Vector2 point = new Vector2();
                string[] values = line.Split(',');

                int count = 0;
                foreach (var v in values)
                {
                    double.TryParse(v, out double output);

                    //Only works with X and Y pair of numbers
                    if (count == 0)
                    {
                        point.X = (float)output;
                    }
                    else
                    {
                        point.Y = (float)output;
                    }
                    count++;
                }
                points.Add(point);
            }
            return points;
        }
    }
}

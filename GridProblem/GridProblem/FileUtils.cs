﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GridProblem
{
    class FileUtils
    {
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
                    Double output;
                    Double.TryParse(v, out output);
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
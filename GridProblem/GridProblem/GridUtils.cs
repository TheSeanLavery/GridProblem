﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

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

            float angle = AngleOfClosest(startingPoint, points);

            //Loop runs count -1 as the results already have the starting point added
            for (int i = 0; i < count - 1; i++)
            {
                Vector2 closest = GetClosestPointAlongAngle(startingPoint, points, angle);

                result.Add(closest);
                startingPoint = closest;
                points.Remove(closest);
            }
            return result;
        }
        /// <summary>
        /// Determins if two floats are within an error margin
        /// </summary>
        static bool IsClose(float f1,float f2,float error = 10)
        {
            if(Math.Abs(f1-f2)< error)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the nearest point if the calculated angle is close to the desired angle
        /// </summary>
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
        /// <summary>
        /// Finds the lowest point in a list of points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        static Vector2 GetLowestPoint(List<Vector2> points)
        {
            float height = float.MaxValue;
            Vector2 value = new Vector2();
            foreach(Vector2 p in points)
            {
                if (p.Y < height)
                {
                    height = p.Y;
                    value = p;
                }
            }
            return value;
        }
        /// <summary>
        /// Finds the closest two points. Used for determining the next right, and bottom point
        /// </summary>
        static List<Vector2> GetClosest2Points(Vector2 startingPoint, List<Vector2> points)
        { 
            List<Vector2> result = new List<Vector2>();
            var dictionary = new Dictionary<Vector2, float>();

            foreach(Vector2 p in points)
            {
                float distance = Vector2.Distance(startingPoint, p);
                dictionary.Add(p, distance);
            }
            var list = dictionary.ToList();
            list.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            result.Add(list[0].Key);
            result.Add(list[1].Key);

            return result;
        }
        /// <summary>
        /// Operates on the top left point to find the next right and down points
        /// </summary>
        static float AngleOfClosest(Vector2 startingPoint, List<Vector2> points)
        {
            List<Vector2> closestPoints = GetClosest2Points(startingPoint, points); 
            Vector2 bottomPoint = GetLowestPoint(closestPoints);
            float angle = (float)MathUtils.AngleBetween(startingPoint, bottomPoint);
            return angle;
        }
        /// <summary>
        /// Takes a list of columns and turns them into a single list of points.
        /// Used for Unit Testing
        /// </summary>
        public static List<Vector2> UnpackColumns(List<List<Vector2>> cols)
        {
            List<Vector2> vecs = new List<Vector2>();
            foreach (List<Vector2> v in cols)
            {
                vecs.AddRange(v);
            }
            return vecs;
        }
    }
}

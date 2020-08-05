using System;
using System.Collections.Generic;
using System.Numerics;

namespace GridProblem
{
    public class MathUtils
    {
        /// <summary>
        /// Finds the top left point in a list of 2D points
        /// </summary>
        public static Vector2 FindTopLeftPoint(List<Vector2> points)
        {
            Vector2 topLeftCommon = new Vector2(float.MaxValue, float.MinValue);
            Vector2 topLeftPoint = new Vector2(float.MaxValue, float.MinValue);
            foreach (Vector2 p in points)
            {
                if (p.X < topLeftCommon.X) topLeftCommon.X = p.X;
                if (p.Y > topLeftCommon.Y) topLeftCommon.Y = p.Y;
            }
            float distance = float.MaxValue;
            Vector2 stretched = new Vector2(50, 1);
            foreach (Vector2 p in points)
            {
                float newDistance = Vector2.Distance(topLeftCommon*stretched, p * stretched);
                if (newDistance < distance && p.X <= topLeftPoint.X)
                {
                    topLeftPoint = p;
                    distance = newDistance;
                }
            }
            return topLeftPoint;
        }
        /// <summary>
        /// Gets angle between two Vectors
        /// </summary>
        public static double AngleBetween(Vector2 vector1, Vector2 vector2)
        {
            float xDiff = vector2.X - vector1.X;
            float yDiff = vector2.Y - vector1.Y;
            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }
        /// <summary>
        /// Gets closest point from a list of points.
        /// Will not return overlapping points
        /// </summary>
        static Vector2 GetClosestPoint(Vector2 point, List<Vector2> points)
        {
            Vector2 closestPoint = new Vector2();
            float distance;
            float closestDistance = float.MaxValue;
            foreach (Vector2 p in points)
            {
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
        /// Gets closest points with a vertical bias.
        /// The X values are multiplied by 50 to make the graph seem wider than it is,
        /// causing any distances to be biased in the vertical direction
        /// </summary>
        public static Vector2 GetClosestPointWithVerticalBias(Vector2 point, List<Vector2> points)
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

                distance = Vector2.Distance(stretchedOriginalPoint, stretchedPoint);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = p;
                }
            }
            return closestPoint;
        }
    }
}

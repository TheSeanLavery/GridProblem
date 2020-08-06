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
            double value = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
            return value;
        }
        /// <summary>
        /// Calculates Angle Between two line segments
        /// </summary>
        public static double AngleBetweenLines(Vector2 vector1, Vector2 vector2)
        {
            double sin = vector1.X * vector2.Y - vector2.X * vector1.Y;
            double cos = vector1.X * vector2.X + vector1.Y * vector2.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }
        /// <summary>
        /// Converts Degrees to Radians
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float DegreesToRadians(float degrees)
        {
            float radians = (float)((Math.PI / 180) * degrees);
            return (radians);
        }/// <summary>
        /// Rotates a point around origin point by radians
        /// </summary>
        public static Vector2 RotateAboutOrigin(Vector2 point, Vector2 origin, float radians)
        {
            return Vector2.Transform(point - origin, Matrix3x2.CreateRotation(radians)) + origin;
        }
    }
}

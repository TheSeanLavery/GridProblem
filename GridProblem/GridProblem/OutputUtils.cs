using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GridProblem
{
    class OutputUtils
    {
        /// <summary>
        /// Calculates and writes to the Console the angle alpha
        /// between the tilted grid and a grid with no tilt
        /// </summary>
        /// <param name="columns"></param>
        public static void WriteAlpha(List<List<Vector2>> columns)
        {
            Vector2 line1 = columns[1][0] - columns[0][0];
            Vector2 line2 = new Vector2(1, 0);
            Console.WriteLine("Alpha = " + MathUtils.AngleBetweenLines(line1, line2) + " degrees");
        }
        /// <summary>
        /// Writes the Columns to to the console
        /// </summary>
        public static void WriteColumns(List<List<Vector2>> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                List<Vector2> column = columns[i];
                WriteColumn(i, column);
            }
        }
        /// <summary>
        /// Writes the Rows to the console
        /// </summary>
        /// <param name="rows"></param>
        public static void WriteRows(List<List<Vector2>> rows)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                List<Vector2> row = rows[i];
                WriteRow(i, row);
            }
        }
        /// <summary>
        /// Writes and individual column to the Console
        /// </summary>
        public static void WriteColumn(int column, List<Vector2> points)
        {
            string value = String.Format("Col {0}: ", column);

            for (int i = 0; i < points.Count(); i++)
            {
                Vector2 point = points[i];

                //if last point don't add hyphen
                if (i == points.Count() - 1)
                {
                    value += point.X + "," + point.Y;
                }
                else
                {
                    value += point.X + "," + point.Y + " - ";
                }
            }
            Console.WriteLine(value);
        }
        /// <summary>
        /// Writes invididual row to the Console
        /// </summary>
        public static void WriteRow(int row, List<Vector2> points)
        {
            string value = String.Format("Row {0}: ", row);
            for (int i = 0; i < points.Count(); i++)
            {
                Vector2 point = points[i];

                //if last point don't add hyphen
                if (i == points.Count() - 1)
                {
                    value += point.X + "," + point.Y;
                }
                else
                {
                    value += point.X + "," + point.Y + " - ";
                }
            }
            Console.WriteLine(value);
        }
    }
}

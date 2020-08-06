using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GridProblem
{
    public class TestingUtils
    {
        /// <summary>
        /// Generates a rotated Grid with seeded random number generation for unit testing
        /// </summary>
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
            rightPoint = MathUtils.RotateAboutOrigin(rightPoint, startingPoint, MathUtils.DegreesToRadians(angle));

            generated.Add(startingPoint);
            generated.Add(rightPoint);

            Vector2 bottomPoint = new Vector2();
            bottomPoint.X = rightPoint.X;
            bottomPoint.Y = rightPoint.Y;

            bottomPoint = MathUtils.RotateAboutOrigin(bottomPoint, startingPoint, MathUtils.DegreesToRadians(-90));
            generated.Add(bottomPoint);


            List<List<Vector2>> columns = new List<List<Vector2>>();

            //col.Add(startingPoint);
            //col.Add(bottomPoint);

            Vector2 columnAngle = bottomPoint - startingPoint;
            Vector2 rowAngle = rightPoint - startingPoint;

            Vector2 nextVec = new Vector2();
            nextVec.X = startingPoint.X;
            nextVec.Y = startingPoint.Y;
            for (int x = 0; x < grid.squareSize; x++)
            {
                List<Vector2> col = new List<Vector2>();

                for (int y = 0; y < grid.squareSize; y++)
                {
                    col.Add(nextVec);
                    nextVec += columnAngle;
                }
                nextVec = startingPoint + (rowAngle * (x + 1));
                columns.Add(col);
            }


            return columns;

            //result.RotateAt(angle, startingPoint);

            //generated.Add()

        }
    }

    /// <summary>
    /// Data structure class for generation of grids for Unit Test
    /// </summary>
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

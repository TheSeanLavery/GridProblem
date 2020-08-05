using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GridProblemUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGridGenerationAndValidate()
        {
            for (int i = 0; i < 200; i++)
            {
                TestGridParam(0, 45, i);
            }
        }
        void TestGridParam(int minAngle, int maxAngle, int? randomSeed=null)
        {
            Random rand = new Random();
            GridProblem.GridParams grid = new GridProblem.GridParams();
            grid.squareSize = rand.Next(3,10);
            grid.coordMin = 0;
            grid.coordMax = 100;

            if (randomSeed == null) grid.seed = rand.Next();

            List<List<Vector2>> columns = GridProblem.TestingUtils.GenerateGrid(grid);
            List<Vector2> unpackedList = GridProblem.GridUtils.UnpackColumns(columns);

            Vector2 topLeft = GridProblem.MathUtils.FindTopLeftPoint(unpackedList);

            Assert.AreEqual(topLeft, columns[0][0]);

            List<List<Vector2>> newColumns = GridProblem.GridUtils.CreateColumns(unpackedList, grid.squareSize);

            for (int x = 0; x < grid.squareSize; x++)
            {
                for (int y = 0; y < grid.squareSize; y++)
                {
                    Assert.AreEqual(newColumns[x][y], columns[x][y]);
                }
            }
            Console.WriteLine("test complete");
        }
    }
}
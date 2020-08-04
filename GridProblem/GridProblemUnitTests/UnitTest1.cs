using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System.Collections.Generic;

namespace GridProblemUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            for (int i = 0; i < 200; i++)
            {
                TestGridParam(0, 44, i);
            }
        }
        void TestGridParam(int minAngle, int maxAngle, int? randomSeed=null)
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                string[] args = { "grid_input.txt" };
                //GridProblem.Program.Main(args);

                Random rand = new Random();
                GridProblem.GridParams grid = new GridProblem.GridParams();
                grid.squareSize = 2;
                grid.coordMin = -50;
                grid.coordMax = 100;

                if (randomSeed == null) grid.seed = rand.Next();

                //var prog = GridProblem.Program;

                List<List<Vector2>> columns = GridProblem.Program.GenerateGrid(grid);
                List<Vector2> unpackedList = GridProblem.Program.UnpackColumns(columns);

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
}
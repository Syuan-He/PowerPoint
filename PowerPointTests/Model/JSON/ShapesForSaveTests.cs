using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class ShapesForSaveTests
    {
        Shape _shape;
        Shapes _shapes;
        const int X1 = 0;
        const int Y1 = 1;
        const int X2 = 2;
        const int Y2 = 3;
        Coordinate _point1 = new Coordinate(X1, Y1);
        Coordinate _point2 = new Coordinate(X2, Y2);

        // Test ShapesForSave
        [TestMethod()]
        public void TestShapesForSave()
        {
            _shape = new Line(_point1, _point2);
            _shapes = new Shapes(new Factory(new Random()));
            _shapes.CreateShape(_shape);
            ShapesForSave save = new ShapesForSave(_shapes);
            Assert.IsNotNull(save.ShapeList);
        }

        // Test ShapesForSave1
        [TestMethod()]
        public void TestShapesForSave1()
        {
            ShapesForSave save = new ShapesForSave();
        }

        // Test Turn2Shapes
        [TestMethod()]
        public void TestTurn2Shapes()
        {
            _shape = new Line(_point1, _point2);
            _shapes = new Shapes(new Factory(new Random()));
            _shapes.CreateShape(_shape);
            ShapesForSave save = new ShapesForSave(_shapes);
            Assert.AreEqual(save.Turn2Shapes().GetType(), _shapes.GetType());
        }
    }
}
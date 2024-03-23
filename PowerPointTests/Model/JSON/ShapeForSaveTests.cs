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
    public class ShapeForSaveTests
    {
        Shape _shape;
        const int X1 = 0;
        const int Y1 = 1;
        const int X2 = 2;
        const int Y2 = 3;
        Coordinate _point1 = new Coordinate(X1, Y1);
        Coordinate _point2 = new Coordinate(X2, Y2);

        // Test ShapeForSave
        [TestMethod()]
        public void TestShapeForSave()
        {
            _shape = new Line(_point1, _point2);
            ShapeForSave save = new ShapeForSave(_shape);
            Assert.AreEqual(X1, save.X1);
            Assert.AreEqual(Y1, save.Y1);
            Assert.AreEqual(X2, save.X2);
            Assert.AreEqual(Y2, save.Y2);
            Assert.AreEqual(save.Type, _shape.ShapeName);
        }

        // Test ShapeForSave1
        [TestMethod()]
        public void TestShapeForSave1()
        {
            ShapeForSave save = new ShapeForSave();
        }

        // Test Turn2Shape
        [TestMethod()]
        public void TestTurn2Shape()
        {
            _shape = new Line(_point1, _point2);
            ShapeForSave save = new ShapeForSave(_shape);
            Shape newShape = save.Turn2Shape();
            Assert.AreEqual(newShape.Information, _shape.Information);
            Assert.AreEqual(save.Type, _shape.ShapeName);
        }
    }
}
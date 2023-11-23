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
    public class RectangleTests
    {
        Coordinate _point1 = new Coordinate(1, 0);
        Coordinate _point2 = new Coordinate(0, 1);
        Rectangle _rectangle;
        PrivateObject _rectanglePrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _rectangle = new Rectangle(_point1, _point2);
            _rectanglePrivate = new PrivateObject(_rectangle);
        }

        // 測試建構式與 AdjustPoint()
        [TestMethod()]
        public void TestRectangle()
        {
            Assert.AreEqual(ShapeType.RECTANGLE, _rectangle.ShapeName);
            Assert.AreEqual("(0, 0), (1, 1)", _rectangle.Information);
        }

        // Test GetShapeName
        [TestMethod()]
        public void TestGetShapeName()
        {
            Assert.AreEqual(ShapeType.RECTANGLE, _rectangle.GetShapeName());
        }

        // Test GetInfo
        [TestMethod()]
        public void TestGetInfo()
        {
            Assert.AreEqual("(0, 0), (1, 1)", _rectangle.GetInfo());
        }

        // Test SetEndPoint
        [TestMethod()]
        public void TestSetEndPoint()
        {
            _rectangle.SetEndPoint(new Coordinate(2, 2));
            Assert.AreEqual("(0, 0), (2, 2)", _rectangle.GetInfo());
        }

        // Test SetMove
        [TestMethod()]
        public void TestSetMove()
        {
            _rectangle.SetMove(1, 1);
            Assert.AreEqual("(1, 1), (2, 2)", _rectangle.GetInfo());
        }

        // Test IsSelect
        [TestMethod()]
        public void TestIsSelect()
        {
            Assert.IsTrue(_rectangle.IsSelect(0, 0));
            Assert.IsFalse(_rectangle.IsSelect(-1, 0));
            Assert.IsFalse(_rectangle.IsSelect(0, -1));
            Assert.IsFalse(_rectangle.IsSelect(3, 3));
        }

        // Test IsInnerInX (private)
        [TestMethod()]
        public void TestIsInnerInX()
        {
            Assert.IsFalse((bool)_rectanglePrivate.Invoke("IsInnerInX", new object[] { -1 }));
            Assert.IsTrue((bool)_rectanglePrivate.Invoke("IsInnerInX", new object[] { 0 }));
            Assert.IsTrue((bool)_rectanglePrivate.Invoke("IsInnerInX", new object[] { 1 }));
            Assert.IsFalse((bool)_rectanglePrivate.Invoke("IsInnerInX", new object[] { 2 }));
        }

        // Test IsInnerInY (private)
        [TestMethod()]
        public void TestIsInnerInY()
        {
            Assert.IsFalse((bool)_rectanglePrivate.Invoke("IsInnerInY", new object[] { -1 }));
            Assert.IsTrue((bool)_rectanglePrivate.Invoke("IsInnerInY", new object[] { 0 }));
            Assert.IsTrue((bool)_rectanglePrivate.Invoke("IsInnerInY", new object[] { 1 }));
            Assert.IsFalse((bool)_rectanglePrivate.Invoke("IsInnerInY", new object[] { 2 }));
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockGraphics _graphics = new MockGraphics();
            _rectangle.Draw(_graphics);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_x1"), _graphics._x1);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_y1"), _graphics._y1);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_x2"), _graphics._x2);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_y2"), _graphics._y2);
        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            MockGraphics _graphics = new MockGraphics();
            _rectangle.DrawSelectFrame(_graphics);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_x1"), _graphics._x1);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_y1"), _graphics._y1);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_x2"), _graphics._x2);
            Assert.AreEqual(_rectanglePrivate.GetFieldOrProperty("_y2"), _graphics._y2);
        }
    }
}
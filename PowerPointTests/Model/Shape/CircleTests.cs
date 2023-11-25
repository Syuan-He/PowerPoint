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
    public class CircleTests
    {
        Coordinate _point1 = new Coordinate(1, 0);
        Coordinate _point2 = new Coordinate(0, 1);
        Circle _circle;
        PrivateObject _circlePrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _circle = new Circle(_point1, _point2);
            _circlePrivate = new PrivateObject(_circle);
        }

        // 測試建構式與 AdjustPoint()
        [TestMethod()]
        public void TestRectangle()
        {
            Assert.AreEqual(ShapeType.CIRCLE, _circle.ShapeName);
            Assert.AreEqual("(0, 0), (1, 1)", _circle.Information);
        }

        // Test GetShapeName
        [TestMethod()]
        public void TestGetShapeName()
        {
            Assert.AreEqual(ShapeType.CIRCLE, _circle.GetShapeName());
        }

        // Test GetInfo
        [TestMethod()]
        public void TestGetInfo()
        {
            Assert.AreEqual("(0, 0), (1, 1)", _circle.GetInfo());
        }

        // Test SetEndPoint
        [TestMethod()]
        public void TestSetEndPoint()
        {
            _circle.SetEndPoint(new Coordinate(2, 2));
            Assert.AreEqual("(0, 0), (2, 2)", _circle.GetInfo());
        }

        // Test SetMove
        [TestMethod()]
        public void TestSetMove()
        {
            _circle.SetMove(1, 1);
            Assert.AreEqual("(1, 1), (2, 2)", _circle.GetInfo());
        }

        // Test IsSelect
        [TestMethod()]
        public void TestIsSelect()
        {
            Assert.IsTrue(_circle.IsSelect(0, 0));
            Assert.IsFalse(_circle.IsSelect(-1, 0));
            Assert.IsFalse(_circle.IsSelect(0, -1));
            Assert.IsFalse(_circle.IsSelect(3, 3));
        }

        // Test IsInnerInX (private)
        [TestMethod()]
        public void TestIsInnerInX()
        {
            Assert.IsFalse((bool)_circlePrivate.Invoke("IsInnerInX", new object[] { -1 }));
            Assert.IsTrue((bool)_circlePrivate.Invoke("IsInnerInX", new object[] { 0 }));
            Assert.IsTrue((bool)_circlePrivate.Invoke("IsInnerInX", new object[] { 1 }));
            Assert.IsFalse((bool)_circlePrivate.Invoke("IsInnerInX", new object[] { 2 }));
        }

        // Test IsInnerInY (private)
        [TestMethod()]
        public void TestIsInnerInY()
        {
            Assert.IsFalse((bool)_circlePrivate.Invoke("IsInnerInY", new object[] { -1 }));
            Assert.IsTrue((bool)_circlePrivate.Invoke("IsInnerInY", new object[] { 0 }));
            Assert.IsTrue((bool)_circlePrivate.Invoke("IsInnerInY", new object[] { 1 }));
            Assert.IsFalse((bool)_circlePrivate.Invoke("IsInnerInY", new object[] { 2 }));
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics _graphics = new MockIGraphics();
            _circle.Draw(_graphics);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_x1"), _graphics._x1);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_y1"), _graphics._y1);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_x2"), _graphics._x2);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_y2"), _graphics._y2);
        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            MockIGraphics _graphics = new MockIGraphics();
            _circle.DrawSelectFrame(_graphics);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_x1"), _graphics._x1);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_y1"), _graphics._y1);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_x2"), _graphics._x2);
            Assert.AreEqual(_circlePrivate.GetFieldOrProperty("_y2"), _graphics._y2);
        }
    }
}
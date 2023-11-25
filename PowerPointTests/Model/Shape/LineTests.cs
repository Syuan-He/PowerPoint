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
    public class LineTests
    {
        Coordinate _point1 = new Coordinate(1, 0);
        Coordinate _point2 = new Coordinate(0, 1);
        Line _line;
        PrivateObject _linePrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _line = new Line(_point1, _point2);
            _linePrivate = new PrivateObject(_line);
        }

        // 測試建構式與 AdjustPoint()
        [TestMethod()]
        public void TestLine()
        {
            Assert.AreEqual(ShapeType.LINE, _line.ShapeName);
            Assert.AreEqual("(0, 1), (1, 0)", _line.Information);
        }

        // Test GetShapeName
        [TestMethod()]
        public void TestGetShapeName()
        {
            Assert.AreEqual(ShapeType.LINE, _line.GetShapeName());
        }

        // Test GetInfo
        [TestMethod()]
        public void TestGetInfo()
        {
            Assert.AreEqual("(0, 1), (1, 0)", _line.GetInfo());
        }

        // Test SetEndPoint
        [TestMethod()]
        public void TestSetEndPoint()
        {
            _line.SetEndPoint(new Coordinate(2, 2));
            Assert.AreEqual("(0, 1), (2, 2)", _line.GetInfo());
        }

        // Test SetMove
        [TestMethod()]
        public void TestSetMove()
        {
            _line.SetMove(1, 1);
            Assert.AreEqual("(1, 2), (2, 1)", _line.GetInfo());
        }

        // Test IsSelect
        [TestMethod()]
        public void TestIsSelect()
        {
            Assert.IsTrue(_line.IsSelect(0, 0));
            Assert.IsFalse(_line.IsSelect(-1, 0));
            Assert.IsFalse(_line.IsSelect(0, -1));
            Assert.IsFalse(_line.IsSelect(3, 3));
        }

        // Test IsInnerInX (private)
        [TestMethod()]
        public void TestIsInnerInX()
        {
            Assert.IsFalse((bool)_linePrivate.Invoke("IsInnerInX", new object[] { -1 }));
            Assert.IsTrue((bool)_linePrivate.Invoke("IsInnerInX", new object[] { 0 }));
            Assert.IsTrue((bool)_linePrivate.Invoke("IsInnerInX", new object[] { 1 }));
            Assert.IsFalse((bool)_linePrivate.Invoke("IsInnerInX", new object[] { 2 }));
        }

        // Test IsInnerInY (private)
        [TestMethod()]
        public void TestIsInnerInY()
        {
            Assert.IsFalse((bool)_linePrivate.Invoke("IsInnerInY", new object[]{ -1 }));
            Assert.IsTrue((bool)_linePrivate.Invoke("IsInnerInY", new object[] { 0 }));
            Assert.IsTrue((bool)_linePrivate.Invoke("IsInnerInY", new object[] { 1 }));
            Assert.IsFalse((bool)_linePrivate.Invoke("IsInnerInY", new object[] { 2 }));
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics _graphics = new MockIGraphics();
            _line.Draw(_graphics);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_x1"), _graphics._x1);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_y1"), _graphics._y1);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_x2"), _graphics._x2);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_y2"), _graphics._y2);
        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            MockIGraphics _graphics = new MockIGraphics();
            _line.DrawSelectFrame(_graphics);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_x1"), _graphics._x1);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_y1"), _graphics._y1);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_x2"), _graphics._x2);
            Assert.AreEqual(_linePrivate.GetFieldOrProperty("_y2"), _graphics._y2);
        }
    }
}
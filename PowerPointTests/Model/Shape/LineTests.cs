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
        private const int LARGE_NUMBER = 100;
        private const int MAX_Y = 40;
        private const int MAX_X = 20;
        Coordinate _point1 = new Coordinate(MAX_X, 0);
        Coordinate _point2 = new Coordinate(0, MAX_Y);
        Line _shape;
        PrivateObject _shapePrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _shape = new Line(_point1, _point2);
            _shapePrivate = new PrivateObject(_shape);
        }

        // 測試建構式
        [TestMethod()]
        public void TestLine()
        {
            Assert.AreEqual(ShapeType.LINE, _shape.ShapeName);
            Assert.AreEqual(
                String.Format(
                    "{0}, {1}",
                    _point2.ToString(),
                    _point1.ToString()),
                _shape.Information);
        }

        // Test AdjustPoint
        [TestMethod]
        public void TestAdjustPoint()
        {
            bool eventRaised = false;
            _shape.PropertyChanged += (sender, args) => eventRaised = true;

            _shapePrivate.SetField("_x1", LARGE_NUMBER);
            _shape.AdjustPoint();

            Assert.AreEqual(
                String.Format(
                    "({0}, {1}), ({2}, {3})",
                    _point1.X,
                    _point1.Y,
                    LARGE_NUMBER,
                    _point2.Y),
                _shape.Information);
            Assert.IsFalse((bool)_shapePrivate.GetField("_isReverse"));
            Assert.IsTrue(eventRaised);

            _shapePrivate.SetField("_x2", 0);
            _shape.AdjustPoint();

            Assert.AreEqual(
                String.Format(
                    "({0}, {1}), ({2}, {3})",
                    0,
                    _point2.Y,
                    _point1.X,
                    _point1.Y),
                _shape.Information);
            Assert.IsTrue((bool)_shapePrivate.GetField("_isReverse"));
        }

        // Test GetShapeName
        [TestMethod()]
        public void TestShapeName()
        {
            Assert.AreEqual(ShapeType.LINE, _shape.ShapeName);
        }

        // Test GetInfo
        [TestMethod()]
        public void TestInformation()
        {
            Assert.AreEqual(
                String.Format(
                    "({0}, {1}), ({2}, {3})",
                    _point2.X,
                    _point2.Y,
                    _point1.X,
                    _point1.Y),
                _shape.Information);
        }

        // Test SetEndPoint
        [TestMethod()]
        public void TestSetEndPoint()
        {
            _shape.SetEndPoint(20, 20);
            Assert.AreEqual(
                String.Format(
                    "({0}, {1}), ({2}, {3})",
                    0,
                    20,
                    20,
                    0),
                _shape.Information);

            _shapePrivate.SetField("_y2", 20);
            _shape.AdjustPoint();
            _shape.SetEndPoint(30, 30);
            Assert.AreEqual(
                String.Format(
                    "({0}, {1}), ({2}, {3})",
                    0,
                    20,
                    30,
                    30),
                _shape.Information);
        }

        // Test SetMove
        [TestMethod()]
        public void TestSetMove()
        {
            _shape.SetMove(20, 20);
            Assert.AreEqual("(20, 60), (40, 20)", _shape.Information);
        }

        // Test IsSelect
        [TestMethod()]
        public void TestIsSelect()
        {
            Assert.IsTrue(_shape.IsSelect(0, 0));
            Assert.IsFalse(_shape.IsSelect(-1, 0));
            Assert.IsFalse(_shape.IsSelect(0, -1));
            Assert.IsFalse(_shape.IsSelect(50, 50));
        }

        // Test IsInnerInX (private)
        [TestMethod()]
        public void TestIsInnerInX()
        {
            Assert.IsFalse((bool)_shapePrivate.Invoke("IsInnerInX", new object[] { -1 }));
            Assert.IsTrue((bool)_shapePrivate.Invoke("IsInnerInX", new object[] { 0 }));
            Assert.IsTrue((bool)_shapePrivate.Invoke("IsInnerInX", new object[] { 1 }));
            Assert.IsFalse((bool)_shapePrivate.Invoke("IsInnerInX", new object[] { 50 }));
        }

        // Test IsInnerInY (private)
        [TestMethod()]
        public void TestIsInnerInY()
        {
            Assert.IsFalse((bool)_shapePrivate.Invoke("IsInnerInY", new object[]{ -1 }));
            Assert.IsTrue((bool)_shapePrivate.Invoke("IsInnerInY", new object[] { 0 }));
            Assert.IsTrue((bool)_shapePrivate.Invoke("IsInnerInY", new object[] { 1 }));
            Assert.IsFalse((bool)_shapePrivate.Invoke("IsInnerInY", new object[] { 50 }));
        }

        // TestGetAtCorner
        [TestMethod()]
        [DataRow(20, 40, 8)]
        [DataRow(0, 0, -1)]
        [DataRow(15, 35, -1)]
        public void TestGetAtCorner(int x1, int y1, int ans)
        {
            Assert.AreEqual(ans, _shape.GetAtCorner(x1, y1));
        }

        // Test IsCornerInX (private)
        [TestMethod()]
        [DataRow(MAX_X + 5, false)]
        [DataRow(MAX_X + 4, true)]
        [DataRow(MAX_X - 4, true)]
        [DataRow(MAX_X - 5, false)]
        public void TestIsCornerInX(int x1, bool ans)
        {
            Assert.AreEqual(ans, (bool)_shapePrivate.Invoke("IsCornerInX", new object[] { x1 }));
        }

        // Test IsCornerInY (private)
        [TestMethod()]
        [DataRow(MAX_Y + 5, false)]
        [DataRow(MAX_Y + 4, true)]
        [DataRow(MAX_Y - 4, true)]
        [DataRow(MAX_Y - 5, false)]
        public void TestIsCornerInY(int y1, bool ans)
        {
            Assert.AreEqual(ans, (bool)_shapePrivate.Invoke("IsCornerInY", new object[] { y1 }));
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _shape.Draw(graphics);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_x1"), graphics._x1);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_y1"), graphics._y1);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_x2"), graphics._x2);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_y2"), graphics._y2);
            Assert.AreEqual(1, graphics._countDrawLine);
        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            MockIGraphics graphics = new MockIGraphics();
            _shape.DrawSelectFrame(graphics);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_x1"), graphics._x1);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_y1"), graphics._y1);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_x2"), graphics._x2);
            Assert.AreEqual(_shapePrivate.GetFieldOrProperty("_y2"), graphics._y2);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }
    }
}
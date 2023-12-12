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
                String.Format("({0}, {1}), ({2}, {3})", 0, 20, 20, 0),
                _shape.Information);

            _shapePrivate.SetField("_y2", 20);
            _shape.AdjustPoint();
            _shape.SetEndPoint(30, 30);
            Assert.AreEqual(
                String.Format("({0}, {1}), ({2}, {3})", 0, 20, 30, 30),
                _shape.Information);
        }

        // Test SetPoint
        [TestMethod()]
        [DataRow(0, 40, 20, 0, 4)]
        [DataRow(2, 40, 20, 3, 0)]
        [DataRow(0, 3, 2, 0, 8)]
        [DataRow(0, 40, 2, 3, 2)]
        public void TestSetPoint(int x1, int y1, int x2, int y2, int index)
        {
            _shape.SetPoint(2, 3, index);
            Assert.AreEqual(
                String.Format("({0}, {1}), ({2}, {3})", x1, y1, x2, y2),
                _shape.Information);
        }

        // Test SetY Not Reverse
        [TestMethod()]
        [DataRow(0, 40, 4)]
        [DataRow(3, 40, 0)]
        [DataRow(0, 3, 6)]
        public void TestSetYNotReverse(int y1, int y2, int index)
        {
            _shapePrivate.SetField("_y1", 0);
            _shapePrivate.SetField("_y2", 40);
            _shapePrivate.SetField("_isReverse", false);
            _shape.SetPoint(0, 3, index);
            Assert.AreEqual(
                String.Format("({0}, {1}), ({2}, {3})", 0, y1, 20, y2),
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
        [DataRow(-10, -10, -1)]
        [DataRow(16, 35, 8)]
        public void TestGetAtCorner(int x1, int y1, int ans)
        {
            Assert.AreEqual(ans, _shape.GetAtCorner(x1, y1));
        }

        // Test NearCoordinate (private)
        [TestMethod()]
        [DataRow(MAX_X + 6, -1)]
        [DataRow(MAX_X + 5, 2)]
        [DataRow(MAX_X - 5, 2)]
        [DataRow(MAX_X - 6, 1)]
        [DataRow(-6, -1)]
        [DataRow(0, 0)]
        [DataRow(4, 0)]
        [DataRow(5, 1)]
        public void TestNearCoordinate(int x1, int ans)
        {
            Assert.AreEqual(ans, (int)_shapePrivate.Invoke("NearCoordinate", new object[] { x1, 0, MAX_X }));
        }

        // Test NearCoordinate1 (private)
        [TestMethod()]
        [DataRow(-6, -1)]
        [DataRow(-5, 0)]
        [DataRow(5, 0)]
        [DataRow(6, -1)]
        [DataRow(14, -1)]
        [DataRow(15, 1)]
        public void TestNearCoordinate1(int value, int ans)
        {
            Assert.AreEqual(ans, (int)_shapePrivate.Invoke("NearCoordinate1", new object[] { value, 0, (0 + MAX_Y) / 2 }));
        }

        // Test NearCoordinate2 (private)
        [TestMethod()]
        [DataRow(20, 1)]
        [DataRow(25, 1)]
        [DataRow(26, -1)]
        [DataRow(34, -1)]
        [DataRow(35, 2)]
        [DataRow(45, 2)]
        [DataRow(46, -1)]
        public void TestNearCoordinate2(int value, int ans)
        {
            Assert.AreEqual(ans, (int)_shapePrivate.Invoke("NearCoordinate2", new object[] { value, MAX_Y, (0 + MAX_Y) / 2 }));
        }

        // Test NearCoordinate2 (private) Ex
        [TestMethod()]
        [DataRow(1, 2)]
        public void TestNearCoordinate2Ex(int value, int ans)
        {
            Assert.AreEqual(ans, (int)_shapePrivate.Invoke("NearCoordinate2", new object[] { value, 1, 1 }));
        }

        // Test NearCoordinate Ex
        [TestMethod()]
        [DataRow(1, 2)]
        public void TestNearCoordinateEx(int value, int ans)
        {
            Assert.AreEqual(ans, (int)_shapePrivate.Invoke("NearCoordinate", new object[] { value, 1, 1 }));
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

        // Test SetPosition
        [TestMethod]
        public void TestSetPosition()
        {
            bool eventRaised = false;
            _shape.PropertyChanged += (sender, args) => eventRaised = true;
            _shape.SetPosition(new Coordinate(MAX_X, MAX_Y));
            Assert.AreEqual(String.Format(
                    "({0}, {1}), ({2}, {3})",
                    MAX_X,
                    MAX_Y,
                    _point1.X + MAX_X - _point2.X,
                    _point1.Y + MAX_Y - _point2.Y),
                _shape.Information);
            Assert.AreEqual(true, eventRaised);
        }

        // Test GetPoint1
        [TestMethod]
        public void TestGetPoint1()
        {
            Assert.AreEqual(_point2.ToString(), _shape.GetPoint1().ToString());
        }
    }
}
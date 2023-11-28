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
    public class PointPointerTests
    {
        private const int X1 = 0;
        private const int Y1 = 1;
        private const int X2 = 2;
        private const int Y2 = 3;
        Shape _hint;

        PointPointer _point;
        PrivateObject _pointPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _hint = new Circle(new Coordinate(X1, Y1), new Coordinate(X1, Y1));
            _point = new PointPointer(null);
            _pointPrivate = new PrivateObject(_point);
        }

        // Test PressPointer 
        [TestMethod()]
        public void TestPressPointer()
        {
            _point.PressPointer(X1, Y1, _hint);
            Assert.AreEqual(X1, _pointPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(Y1, _pointPrivate.GetFieldOrProperty("_firstPointY"));
            Shape hint = (Shape)_pointPrivate.GetFieldOrProperty("_shape");
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X1, Y1), hint.Information);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            _point.MovePointer(X1, Y1);

            _point.PressPointer(X1, Y1, _hint);

            _point.MovePointer(X1 + 1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1 + 1, Y1, X1 + 1, Y1), _hint.Information);
            Assert.AreEqual(1, _pointPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(1, _pointPrivate.GetFieldOrProperty("_firstPointY"));

            _point.MovePointer(X2, Y2);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2 -X1, Y2 - Y1 + 1, X2 - X1, Y2 - Y1 + 1), _hint.Information);
            Assert.AreEqual(X2, _pointPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(Y2, _pointPrivate.GetFieldOrProperty("_firstPointY"));
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            Shape hint = (Shape)_pointPrivate.GetFieldOrProperty("_shape");
            Assert.IsNull(hint);
            _point.ReleasePointer(X2, Y2);

            _point.PressPointer(0, 0, _hint);
            _point.MovePointer(X2, Y2);
            _point.ReleasePointer(X2, Y2);

            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1 + X2, Y1 + Y2, X1 + X2, Y1 + Y2), _hint.Information);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _point.Draw(graphics);
            Assert.AreEqual(0, graphics._countDrawSelectFrame);

            _point.PressPointer(X1, Y1, _hint);
            _point.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }
    }
}
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
        MockModel _mockModel;
        PointPointer _point;
        PrivateObject _pointPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _mockModel = new MockModel();
            _point = new PointPointer(_mockModel);
            _pointPrivate = new PrivateObject(_point);
        }

        // Test PressPointer 
        [TestMethod()]
        public void TestPressPointer()
        {
            _point.PressPointer(0, 0);
            Assert.IsFalse(_mockModel._isFindSelect);

            _point.PressPointer(0, 1);
            Assert.IsTrue(_mockModel._isFindSelect);
            Assert.AreEqual(0, _pointPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(1, _pointPrivate.GetFieldOrProperty("_firstPointY"));
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            _point.PressPointer(X1, Y1);

            _point.MovePointer(1, 1);
            Assert.AreEqual("(1, 0)", _mockModel._point1.ToString());
            Assert.AreEqual(1, _pointPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(1, _pointPrivate.GetFieldOrProperty("_firstPointY"));

            _point.MovePointer(X2, Y2);
            Assert.AreEqual("(1, 2)", _mockModel._point1.ToString());
            Assert.AreEqual(X2, _pointPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(Y2, _pointPrivate.GetFieldOrProperty("_firstPointY"));
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            _point.PressPointer(0, 0);
            _point.MovePointer(X2, Y2);
            _point.ReleasePointer(X2, Y2);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _point.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }
    }
}
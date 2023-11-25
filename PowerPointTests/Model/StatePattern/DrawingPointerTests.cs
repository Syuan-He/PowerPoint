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
    public class DrawingPointerTests
    {
        private const int X1 = 0;
        private const int Y1 = 1;
        private const int X2 = 2;
        private const int Y2 = 3;
        MockModel _mockModel;
        DrawingPointer _drawing;
        PrivateObject _drawingPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _mockModel = new MockModel();
            _drawing = new DrawingPointer(_mockModel);
            _drawingPrivate = new PrivateObject(_drawing);
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _drawing.PressPointer(X1, Y1);
            Assert.AreEqual(X1, _drawingPrivate.GetFieldOrProperty("_firstPointX"));
            Assert.AreEqual(Y1, _drawingPrivate.GetFieldOrProperty("_firstPointY"));
            Shape hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X1, Y1), hint.Information);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            Shape hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.IsNull(hint);
            _drawing.MovePointer(X2, Y2);

            _drawing.PressPointer(X1, Y1);
            _drawing.MovePointer(X2, Y2);
            hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X2, Y2), hint.Information);
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            Shape hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.IsNull(hint);
            _drawing.ReleasePointer(X2, Y2);

            _drawing.PressPointer(X1, Y1);
            _drawing.MovePointer(X2, Y2);
            _drawing.ReleasePointer(X2, Y2);

            Assert.AreEqual(X1, _mockModel._point1.X);
            Assert.AreEqual(Y1, _mockModel._point1.Y);
            Assert.AreEqual(X2, _mockModel._point2.X);
            Assert.AreEqual(Y2, _mockModel._point2.Y);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _drawing.PressPointer(X1, Y1);
            _drawing.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawLine);
        }
    }
}
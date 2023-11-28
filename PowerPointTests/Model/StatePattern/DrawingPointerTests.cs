﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Shape _hint;

        DrawingPointer _drawing;
        PrivateObject _drawingPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _drawing = new DrawingPointer();
            _drawingPrivate = new PrivateObject(_drawing);
            _hint = new Line(new Coordinate(X2, Y2), new Coordinate(X2, Y2));
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _drawing.PressPointer(X1, Y1, _hint);
            Shape hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X2, Y2), hint.Information);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            Shape hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.IsNull(hint);
            _drawing.MovePointer(X2, Y2);

            _drawing.PressPointer(X2, Y2, _hint);
            _drawing.MovePointer(X1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X1, Y1), _hint.Information);
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            Shape hint = (Shape)_drawingPrivate.GetFieldOrProperty("_hint");
            Assert.IsNull(hint);
            _drawing.ReleasePointer(X2, Y2);

            _drawing.PressPointer(X2, Y2, _hint);
            _drawing.MovePointer(X1, Y2);
            _drawing.ReleasePointer(X1, Y1);

            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X2, Y2), _hint.Information);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _drawing.Draw(graphics);
            _drawing.PressPointer(X1, Y1, _hint);
            _drawing.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawLine);
        }
    }
}
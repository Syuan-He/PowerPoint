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
        Shape _hint;

        Model _model;
        PrivateObject _modelPrivate;
        IState _pointer;
        PrivateObject _pointerPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model(new Factory(new MockRandom()), new MockService());
            _modelPrivate = new PrivateObject(_model);
            _model.SetDrawing();
            _pointer = (IState)_modelPrivate.GetField("_pointer");
            _pointerPrivate = new PrivateObject(_pointer);
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _model.PressPointer(ShapeType.LINE, X2, Y2);
            _hint = (Shape)_pointerPrivate.GetFieldOrProperty("_hint");
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X2, Y2), _hint.Information);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            _pointer.MovePointer(X2, Y2);

            _model.PressPointer(ShapeType.LINE, X2, Y2);
            _hint = (Shape)_pointerPrivate.GetFieldOrProperty("_hint");
            _pointer.MovePointer(X1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X1, Y1), _hint.Information);
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            _pointer.ReleasePointer(X2, Y2);

            _model.PressPointer(ShapeType.LINE, X2, Y2);
            _hint = (Shape)_pointerPrivate.GetFieldOrProperty("_hint");
            _pointer.MovePointer(X1, Y2);
            _pointer.ReleasePointer(X1, Y1);

            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X2, Y2), _hint.Information);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _pointer.Draw(graphics);
            Assert.AreEqual(0, graphics._countDrawLine);
            _model.PressPointer(ShapeType.LINE, X2, Y2);
            _pointer.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawLine);
        }
    }
}
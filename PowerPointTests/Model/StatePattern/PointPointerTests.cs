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
        private const int X1 = 3;
        private const int Y1 = 6;
        private const int X2 = 54;
        private const int Y2 = 92;
        Shape _hint;

        Model _model;
        PrivateObject _modelPrivate;
        IState _pointer;
        PrivateObject _pointPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Model(new MockFactory());
            _modelPrivate = new PrivateObject(_model);
            _hint = new Circle(new Coordinate(X1, Y1), new Coordinate(X2, Y2));
            _model.CreateShapeCommand(_hint);
            _pointer = (IState)_modelPrivate.GetField("_pointer");
            _pointPrivate = new PrivateObject(_pointer);
        }

        // Test PressPointer 
        [TestMethod()]
        public void TestPressPointer()
        {
            _pointer.PressPointer(X1, Y1);
            Assert.AreEqual(X1, _pointPrivate.GetFieldOrProperty("_x1"));
            Assert.AreEqual(Y1, _pointPrivate.GetFieldOrProperty("_y1"));
            Shape hint = (Shape)_pointPrivate.GetFieldOrProperty("_shape");
            Assert.IsNotNull(hint);
            Assert.AreEqual(_hint, hint);
            Coordinate firstPoint = (Coordinate)_pointPrivate.GetFieldOrProperty("_firstPoint");
            Assert.IsNotNull(firstPoint);
            Assert.AreEqual(String.Format("({0}, {1})", X1, Y1), firstPoint.ToString());

            _pointer.PressPointer(-100, -100);
            Assert.IsNull(_pointPrivate.GetFieldOrProperty("_shape"));

            _pointer.PressPointer(X1, Y1);
            _pointer.PressPointer(X2, Y2);
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(ScalingPointer));
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            Shape hint = (Shape)_pointPrivate.GetFieldOrProperty("_shape");
            Assert.IsNull(hint);
            _pointer.MovePointer(X1, Y1);

            _pointer.PressPointer(X1, Y1);

            _pointer.MovePointer(X1 + 1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1 + 1, Y1, X2 + 1, Y2), _hint.Information);
            Assert.AreEqual(X1 + 1, _pointPrivate.GetFieldOrProperty("_x1"));
            Assert.AreEqual(Y1, _pointPrivate.GetFieldOrProperty("_y1"));

            _pointer.MovePointer(X2, Y2);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X2 + X2 - X1, Y2 + Y2 - Y1), _hint.Information);
            Assert.AreEqual(X2, _pointPrivate.GetFieldOrProperty("_x1"));
            Assert.AreEqual(Y2, _pointPrivate.GetFieldOrProperty("_y1"));
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            Shape hint = (Shape)_pointPrivate.GetFieldOrProperty("_shape");
            Assert.IsNull(hint);
            _pointer.ReleasePointer(X2, Y2);
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(PointPointer));
            
            _pointer.PressPointer(X1, Y1);
            _pointer.ReleasePointer(X2, Y2);
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(PointPointer));
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X2 + X2 - X1, Y2 + Y2 - Y1), _hint.Information);

            _pointer.PressPointer(X2, Y2);
            _pointer.ReleasePointer(X2, Y2);
            _model.PressUndo();
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(PointPointer));
            Assert.IsFalse(_model.IsUndoEnabled);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _pointer.Draw(graphics);
            Assert.AreEqual(0, graphics._countDrawSelectFrame);

            _pointer.PressPointer(X1, Y1);
            _pointer.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }
    }
}
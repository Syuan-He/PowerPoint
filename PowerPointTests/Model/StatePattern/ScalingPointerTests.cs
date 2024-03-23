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
    public class ScalingPointerTests
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
            _model = new Model(new MockFactory(), new MockService());
            _modelPrivate = new PrivateObject(_model);
            _hint = new Circle(new Coordinate(X1, Y1), new Coordinate(X2, Y2));
            _model.CreateShapeCommand(_hint, 0);
            _pointer = (IState)_modelPrivate.GetField("_pointer");
            _pointer.PressPointer(X1, Y1);
            _pointer.PressPointer(X2, Y2);
            _pointer = (IState)_modelPrivate.GetField("_pointer");
            _pointPrivate = new PrivateObject(_pointer);
        }

        // 測試建構式
        [TestMethod()]
        public void TestScalingPointer()
        {
            Assert.IsInstanceOfType(_pointer, typeof(ScalingPointer));
            Shape hint = (Shape)_pointPrivate.GetFieldOrProperty("_shape");
            Assert.AreEqual(_hint, hint);
            Assert.AreEqual(
                String.Format("({0}, {1})", X2, Y2),
                ((Coordinate)_pointPrivate.GetField("_firstPoint")).ToString());
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _pointer.PressPointer(X1, Y1);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            _pointer.MovePointer(X1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X1, Y1), _hint.Information);

            _pointPrivate.SetField("_shape", null);
            _pointer.MovePointer(X1, Y1);
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            _pointer.ReleasePointer(50, 87);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, 50, 87), _hint.Information);
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(PointPointer));

            _pointer.ReleasePointer(X2, Y2);
            _model.PressUndo();
            Assert.IsFalse(_model.IsUndoEnabled);

            _pointPrivate.SetField("_shape", null);
            _pointer.ReleasePointer(X1, Y1);
            Assert.IsFalse(_model.IsUndoEnabled);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _pointer.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);

            _modelPrivate.SetField("_selectedIndex", -1);
            _pointer.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }
    }
}
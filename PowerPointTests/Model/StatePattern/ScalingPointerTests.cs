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
        private const int X1 = 0;
        private const int Y1 = 1;
        private const int X2 = 2;
        private const int Y2 = 3;
        Shape _hint;

        ScalingPointer _scaling;
        PrivateObject _scalingPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _hint = new Line(new Coordinate(X2, Y2), new Coordinate(X2, Y2));
            _scaling = new ScalingPointer(_hint);
            _scalingPrivate = new PrivateObject(_scaling);
        }

        // 測試建構式
        [TestMethod()]
        public void TestScalingPointer()
        {
            Shape hint = (Shape)_scalingPrivate.GetFieldOrProperty("_shape");
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X2, Y2), hint.Information);
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _scaling.PressPointer(X1, Y1, _hint);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            _scaling.MovePointer(X1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X2, Y2, X1, Y1), _hint.Information);

            _scaling = new ScalingPointer(null);
            _scaling.MovePointer(X1, Y1);
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            _scaling.MovePointer(X1, Y1);
            _scaling.ReleasePointer(X1, Y1);
            Assert.AreEqual(String.Format("({0}, {1}), ({2}, {3})", X1, Y1, X2, Y2), _hint.Information);
            
            _scaling = new ScalingPointer(null);
            _scaling.ReleasePointer(X1, Y1);
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            MockIGraphics graphics = new MockIGraphics();
            _scaling.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);

            _scaling = new ScalingPointer(null);
            _scaling.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }
    }
}
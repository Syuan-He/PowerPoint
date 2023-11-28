using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class ShapeTests
    {
        Shape _shape = new Circle(new Coordinate(0, 0), new Coordinate(0, 0));

        // Test ShapeName get, set
        [TestMethod()]
        public void TestGetShapeName()
        {
            Assert.AreEqual(ShapeType.CIRCLE, _shape.ShapeName);
        }

        // Test Information get, set
        [TestMethod()]
        public void TestGetInfo()
        {
            Assert.AreEqual("(0, 0), (0, 0)", _shape.Information);
        }

        // Test PropertyChangedEvent
        [TestMethod()]
        public void TestPropertyChangedEvent()
        {
            bool eventRaised = false;
            _shape.PropertyChanged += (sender, args) => eventRaised = true;
            _shape.AdjustPoint();
            Assert.IsTrue(eventRaised);
        }
    }
}
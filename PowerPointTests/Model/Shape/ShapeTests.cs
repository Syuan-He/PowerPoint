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
        Shape MockShape = new MockShape(new Coordinate(0, 0), new Coordinate(0, 0));

        // Test ShapeName get, set
        [TestMethod()]
        public void TestGetShapeName()
        {
            Assert.AreEqual("testName", MockShape.ShapeName);
        }

        // Test Information get, set
        [TestMethod()]
        public void TestGetInfo()
        {
            Assert.AreEqual("(0, 0), (0, 0)", MockShape.Information);
        }

        // Test PropertyChangedEvent
        [TestMethod()]
        public void TestPropertyChangedEvent()
        {
            bool eventRaised = false;
            MockShape.PropertyChanged += (sender, args) => eventRaised = true;
            MockShape.SetEndPoint(new Coordinate(1, 1));
            Assert.IsTrue(eventRaised);
        }
    }
}
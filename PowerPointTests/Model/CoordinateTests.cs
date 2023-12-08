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
    public class CoordinateTests
    {
        private const int INITIAL_X = 65535;
        private const int INITIAL_Y = -65535;
        Coordinate _coordinate;
        PrivateObject _coordinatePrivate;

        // Test initialize
        [TestInitialize()]
        public void Initialize()
        {
            _coordinate = new Coordinate(INITIAL_X, INITIAL_Y);
            _coordinatePrivate = new PrivateObject(_coordinate); 
        }

        // 測試建構式
        [TestMethod()]
        public void TestCoordinate()
        {
            Assert.AreEqual(INITIAL_X, _coordinatePrivate.GetFieldOrProperty("X"));
            Assert.AreEqual(INITIAL_Y, _coordinatePrivate.GetFieldOrProperty("Y"));
        }

        // Test Set X
        [TestMethod()]
        public void TestSetX()
        {
            _coordinate.X = 34659;
            Assert.AreEqual(34659, _coordinatePrivate.GetFieldOrProperty("X"));
        }

        // Test Get X
        [TestMethod()]
        public void TestGetX()
        {
            _coordinate.X = 48349;
            Assert.AreEqual(_coordinatePrivate.GetFieldOrProperty("X"), _coordinate.X);
        }

        // Test Set Y
        [TestMethod()]
        public void TestSetY()
        {
            _coordinate.Y = 34659;
            Assert.AreEqual(34659, _coordinatePrivate.GetFieldOrProperty("Y"));
        }

        // Test Get Y
        [TestMethod()]
        public void TestGetY()
        {
            _coordinate.Y = 48349;
            Assert.AreEqual(_coordinatePrivate.GetFieldOrProperty("Y"), _coordinate.Y);
        }

        // Test ToString
        [TestMethod()]
        public void TestToString()
        {
            Assert.AreEqual(
                String.Format("({0}, {1})", INITIAL_X, INITIAL_Y),
                _coordinate.ToString());
        }

        // Test AreEqual
        [TestMethod]
        public void TestAreEqual()
        {
            Assert.AreEqual(true, _coordinate.AreEqual(_coordinate));
            Assert.AreEqual(true, _coordinate.AreEqual(new Coordinate(INITIAL_X, INITIAL_Y)));
            Assert.AreEqual(false, _coordinate.AreEqual(new Coordinate(1156, INITIAL_Y)));
        }
    }
}
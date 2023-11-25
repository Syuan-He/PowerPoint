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
    public class FactoryTests
    {
        private const int WIDTH = 123;
        private const int HEIGHT = 465;
        private const int MAX_VALUE = 3541564;
        Coordinate _point1 = new Coordinate(123, 456);
        Coordinate _point2 = new Coordinate(456, 79);

        Factory _factory;
        PrivateObject _factoryPrivate;
        MockRandom _mockRandom;
        PrivateObject _mockRandomPrivate;

        

        // Test Initialize
        [TestInitialize()]
        public void Initialize()
        {
            _mockRandom = new MockRandom();
            _mockRandomPrivate = new PrivateObject(_mockRandom);
            _factory = new Factory(_mockRandom);
            _factoryPrivate = new PrivateObject(_factory);
        }

        // Test Generate Shape With Coordinates
        [TestMethod()]
        public void TestGenerateShapeWithCoordinates()
        {
            Assert.IsNull(_factory.GenerateShape("", _point1, _point2));
            Assert.IsInstanceOfType(_factory.GenerateShape(ShapeType.LINE, _point1, _point2), typeof(Line));
            Assert.IsInstanceOfType(_factory.GenerateShape(ShapeType.RECTANGLE, _point1, _point2), typeof(Rectangle));
            Assert.IsInstanceOfType(_factory.GenerateShape(ShapeType.CIRCLE, _point1, _point2), typeof(Circle));
        }

        // Test Generate Shape With Panel Size
        [TestMethod()]
        public void TestGenerateShapeWithSize()
        {
            Assert.IsNull(_factory.GenerateShape("", WIDTH, HEIGHT));
            Assert.IsInstanceOfType(_factory.GenerateShape(ShapeType.LINE, WIDTH, HEIGHT), typeof(Line));
            Assert.IsInstanceOfType(_factory.GenerateShape(ShapeType.RECTANGLE, WIDTH, HEIGHT), typeof(Rectangle));
            Assert.IsInstanceOfType(_factory.GenerateShape(ShapeType.CIRCLE, WIDTH, HEIGHT), typeof(Circle));
        }

        // Test CreateRandomPoint
        [TestMethod()]
        public void TestCreateRandomPoint()
        {
            Coordinate point = (Coordinate)_factoryPrivate.Invoke("CreateRandomPoint", new Object[] { WIDTH, HEIGHT });
            Assert.AreEqual(0, point.X);
            Assert.AreEqual(1, point.Y);
        }

        //Test CreateRandomNumber
        [TestMethod()]
        public void TestCreateRandomNumber()
        {
            _factoryPrivate.Invoke("CreateRandomNumber", new Object[] { MAX_VALUE });
            Assert.AreEqual(MAX_VALUE, _mockRandomPrivate.GetFieldOrProperty("_maxValue"));
        }

    }
}
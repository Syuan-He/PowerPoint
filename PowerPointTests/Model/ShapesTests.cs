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
    public class ShapesTests
    {
        private const int WIDTH = 123;
        private const int HEIGHT = 465;
        Coordinate _point1 = new Coordinate(123, 456);
        Coordinate _point2 = new Coordinate(456, 79);

        MockIGraphics _mockGraphics;
        MockFactory _mockFactory;
        Shapes _shapes;
        PrivateObject _shapesPrivate;
        IList<Shape> _shapeList;

        // initial
        [TestInitialize()]
        public void Initialize()
        {
            _mockFactory = new MockFactory();
            _shapes = new Shapes(_mockFactory);
            _shapesPrivate = new PrivateObject(_shapes);
        }

        // 測試建構式
        [TestMethod()]
        public void TestShapes()
        {
            Assert.IsInstanceOfType(_shapesPrivate.GetFieldOrProperty("_factory"), typeof(MockFactory));
        }

        // Test ShapeList get
        [TestMethod()]
        public void TestShapeList()
        {
            Assert.AreEqual(_shapes.ShapeList, _shapesPrivate.GetFieldOrProperty("_shapeList"));
        }

        // Test CreateShape With Coordinates
        [TestMethod()]
        public void TestCreateShapeWithCoordinates()
        {
            _shapes.CreateShape(ShapeType.LINE, _point1, _point2);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");

            Assert.AreEqual(ShapeType.LINE, _shapeList[0].ShapeName);
            Assert.AreEqual(
                String.Format("({0}, {1}), ({2}, {3})", _point1.X, _point1.Y, _point2.X, _point2.Y),
                _shapeList[0].Information);
        }

        // Test CreateShape With Size
        [TestMethod()]
        public void TestCreateShapeWithSize()
        {
            _shapes.CreateShape(ShapeType.LINE, WIDTH, HEIGHT);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            
            Assert.AreEqual(_shapeList[0].ShapeName, ShapeType.LINE);
            Assert.AreEqual(WIDTH, _mockFactory._width);
            Assert.AreEqual(HEIGHT, _mockFactory._height);
        }

        // Test Remove
        [TestMethod()]
        public void TestRemove()
        {
            AddListElement();

            _shapes.Remove(6);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            Assert.AreEqual(6, _shapeList.Count());
            _shapes.Remove(5);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            Assert.AreEqual(5, _shapeList.Count());
            Assert.AreEqual("(16, 17), (18, 19)", _shapeList[4].Information);
            _shapes.Remove(-1);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            Assert.AreEqual(5, _shapeList.Count());
            _shapes.Remove(0);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            Assert.AreEqual(4, _shapeList.Count());
            Assert.AreEqual("(4, 5), (6, 7)", _shapeList[0].Information);
        }

        // 替 shapes 加點料
        void AddListElement()
        {
            _shapes.CreateShape(ShapeType.LINE, WIDTH, HEIGHT);
            _shapes.CreateShape(ShapeType.RECTANGLE, WIDTH, HEIGHT);
            _shapes.CreateShape(ShapeType.CIRCLE, WIDTH, HEIGHT);
            _shapes.CreateShape(ShapeType.RECTANGLE, WIDTH, HEIGHT);
            _shapes.CreateShape(ShapeType.CIRCLE, WIDTH, HEIGHT);
            _shapes.CreateShape(ShapeType.CIRCLE, WIDTH, HEIGHT);
        }

        // Test FindSelectItem
        [TestMethod()]
        public void TestFindSelectItem()
        {
            AddListElement();
            Assert.AreEqual(-1, _shapes.FindSelectItem(0, 0));
            Assert.AreEqual(0, _shapes.FindSelectItem(1, 1));
            Assert.AreEqual(5, _shapes.FindSelectItem(22, 21));
            Assert.AreEqual(-1, _shapes.FindSelectItem(23, 22));
        }

        // Test MoveSelectedShape
        [TestMethod()]
        public void TestMoveSelectedShape()
        {
            AddListElement();
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            _shapes.MoveSelectedShape(-1, 1, 1);
            _shapes.MoveSelectedShape(6, 1, 1);
            foreach (MockShape mockShape in _shapeList)
            {
                Assert.AreEqual(0, mockShape._countSetMove);
            }
            _shapes.MoveSelectedShape(0, 1, 1);
            Assert.AreEqual(1, ((MockShape)_shapeList[0])._countSetMove);
            _shapes.MoveSelectedShape(5, 1, 1);
            Assert.AreEqual(1, ((MockShape)_shapeList[5])._countSetMove);

        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            _mockGraphics = new MockIGraphics();
            AddListElement();
            _shapes.Draw(_mockGraphics);
            Assert.AreEqual(1, _mockGraphics._countDrawLine);
            Assert.AreEqual(2, _mockGraphics._countDrawRectangle);
            Assert.AreEqual(3, _mockGraphics._countDrawCircle);

        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            _mockGraphics = new MockIGraphics();
            AddListElement();
            _shapes.DrawSelectFrame(_mockGraphics, -1);
            Assert.AreEqual(0, _mockGraphics._countDrawSelectFrame);
            _shapes.DrawSelectFrame(_mockGraphics, 0);
            Assert.AreEqual(1, _mockGraphics._countDrawSelectFrame);
            Assert.AreEqual(0, _mockGraphics._x1);
            _shapes.DrawSelectFrame(_mockGraphics, 5);
            Assert.AreEqual(2, _mockGraphics._countDrawSelectFrame);
            Assert.AreEqual(20, _mockGraphics._x1);
            _shapes.DrawSelectFrame(_mockGraphics, 6);
            Assert.AreEqual(2, _mockGraphics._countDrawSelectFrame);
        }
    }
}
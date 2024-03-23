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
        Coordinate _point1 = new Coordinate(123, 456);
        Coordinate _point2 = new Coordinate(456, 796);

        MockIGraphics _graphics;
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

        // Test GetShapeListForSave
        [TestMethod()]
        public void TestGetShapeListForSave()
        {
            _shapes.CreateShape(new Line(_point1, _point2));
            Assert.IsInstanceOfType(_shapes.GetShapeListForSave(), typeof(List<ShapeForSave>));
            Assert.AreEqual(_shapes.ShapeList.Count, _shapes.GetShapeListForSave().Count);
        }

        // Test CreateShape
        [TestMethod()]
        public void TestCreateShape()
        {
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");

            Shape shape = null;
            _shapes.CreateShape(shape);
            Assert.AreEqual(0, _shapeList.Count());

            _shapes.CreateShape(new Line(_point1, _point2));
            Assert.AreEqual(1, _shapeList.Count());

            Assert.AreEqual(ShapeType.LINE, _shapeList[0].ShapeName);
            Assert.AreEqual(
                String.Format("{0}, {1}", _point1.ToString(), _point2.ToString()),
                _shapeList[0].Information);
        }

        // Test CreateShape With Coordinates
        [TestMethod()]
        public void TestCreateShapeWithCoordinates()
        {
            _shapes.CreateShape(ShapeType.LINE, _point1, _point2);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");

            Assert.AreEqual(ShapeType.LINE, _shapeList[0].ShapeName);
            Assert.AreEqual(
                String.Format("{0}, {1}", _point1.ToString(), _point2.ToString()),
                _shapeList[0].Information);
        }

        // Test CreateShape With Random
        [TestMethod()]
        public void TestCreateShapeWithRandom()
        {
            _shapes.CreateShape(ShapeType.LINE);
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            
            Assert.AreEqual(_shapeList[0].ShapeName, ShapeType.LINE);
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
            Shape one = new Line(new Coordinate(1, 2), new Coordinate(9, 10));
            _shapes.CreateShape(one);
            Shape another = _shapes.Remove(4);
            Assert.AreEqual(one, another);
        }
        
        // 替 shapes 加點料
        void AddListElement()
        {
            _shapes.CreateShape(ShapeType.LINE);
            _shapes.CreateShape(ShapeType.RECTANGLE);
            _shapes.CreateShape(ShapeType.CIRCLE);
            _shapes.CreateShape(ShapeType.RECTANGLE);
            _shapes.CreateShape(ShapeType.CIRCLE);
            _shapes.CreateShape(ShapeType.CIRCLE);
        }

        // Test RemoveLast
        [TestMethod()]
        public void TestRemoveLast()
        {
            _shapes.CreateShape(ShapeType.LINE);
            _shapes.CreateShape(ShapeType.RECTANGLE);
            _shapes.RemoveLast();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            Assert.AreEqual(ShapeType.LINE, _shapes.ShapeList[0].ShapeName);
            _shapes.RemoveLast();
            _shapes.RemoveLast();
        }

        //Test Insert
        [TestMethod]
        public void TestInsert()
        {
            AddListElement();
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            Shape shape = new Line(_point1, _point2);
            _shapes.Insert(shape, -1);
            Assert.AreEqual(6, _shapeList.Count);
            _shapes.Insert(shape, 7);
            Assert.AreEqual(6, _shapeList.Count);

            _shapes.Insert(shape, 0);
            Assert.AreEqual(7, _shapeList.Count);
            Assert.AreEqual(
                String.Format("{0}, {1}", _point1.ToString(), _point2.ToString()),
                _shapeList[0].Information);
            _shapes.Insert(shape, 7);
            Assert.AreEqual(8, _shapeList.Count);
            Assert.AreEqual(
                String.Format("{0}, {1}", _point1.ToString(), _point2.ToString()),
                _shapeList[7].Information);
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
            
            _shapes.MoveSelectedShape(0, 1, 1);
            Assert.AreEqual("(1, 2), (3, 4)", _shapeList[0].Information);
            _shapes.MoveSelectedShape(5, 1, 1);
            Assert.AreEqual("(21, 22), (23, 24)", _shapeList[5].Information);

        }

        // Test SetSelectedShapePosition
        [TestMethod()]
        public void TestSetSelectedShapePosition()
        {
            AddListElement();
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            _shapes.SetSelectedShapePosition(-1, new Coordinate(1, 1));
            _shapes.SetSelectedShapePosition(6, new Coordinate(1, 1));

            _shapes.SetSelectedShapePosition(0, new Coordinate(1, 1));
            Assert.AreEqual("(1, 1), (3, 3)", _shapeList[0].Information);
            _shapes.SetSelectedShapePosition(5, new Coordinate(1, 1));
            Assert.AreEqual("(1, 1), (3, 3)", _shapeList[5].Information);
        }

        // Test SetSelectedShapeEndPoint
        [TestMethod()]
        public void TestSetSelectedShapeEndPoint()
        {
            AddListElement();
            _shapeList = (IList<Shape>)_shapesPrivate.GetFieldOrProperty("_shapeList");
            _shapes.SetSelectedShapePoint(-1, new Coordinate(1, 1), 8);
            _shapes.SetSelectedShapePoint(6, new Coordinate(1, 1), 8);

            _shapes.SetSelectedShapePoint(0, new Coordinate(1, 1), 8);
            Assert.AreEqual("(0, 1), (1, 1)", _shapeList[0].Information);
            _shapes.SetSelectedShapePoint(5, new Coordinate(1, 1), 8);
            Assert.AreEqual("(1, 1), (20, 21)", _shapeList[5].Information);
        }

        // Test GetAtSelectedCorner
        [TestMethod]
        public void TestGetAtSelectedCorner()
        {
            AddListElement();
            Assert.AreEqual(-1, _shapes.GetAtSelectedCorner(-1, 2, 3));
            Assert.AreEqual(8, _shapes.GetAtSelectedCorner(0, 2, 3));
            Assert.AreEqual(-1, _shapes.GetAtSelectedCorner(1, 12, 5));
            Assert.AreEqual(-1, _shapes.GetAtSelectedCorner(9, 2, 3));
        }

        // Test GetShape
        [TestMethod]
        public void TestGetShape()
        {
            AddListElement();
            Assert.AreEqual(null, _shapes.GetShape(-1));
            Assert.AreEqual(_shapes.ShapeList[0], _shapes.GetShape(0));
            Assert.AreEqual(_shapes.ShapeList[5], _shapes.GetShape(5));
            Assert.AreEqual(null, _shapes.GetShape(6));
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            _graphics = new MockIGraphics();
            AddListElement();
            _shapes.Draw(_graphics);
            Assert.AreEqual(1, _graphics._countDrawLine);
            Assert.AreEqual(2, _graphics._countDrawRectangle);
            Assert.AreEqual(3, _graphics._countDrawCircle);

        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            _graphics = new MockIGraphics();
            AddListElement();
            _shapes.DrawSelectFrame(_graphics, -1);
            Assert.AreEqual(0, _graphics._countDrawSelectFrame);
            _shapes.DrawSelectFrame(_graphics, 0);
            Assert.AreEqual(1, _graphics._countDrawSelectFrame);
            Assert.AreEqual(0, _graphics._x1);
            _shapes.DrawSelectFrame(_graphics, 5);
            Assert.AreEqual(2, _graphics._countDrawSelectFrame);
            Assert.AreEqual(20, _graphics._x1);
            _shapes.DrawSelectFrame(_graphics, 6);
            Assert.AreEqual(2, _graphics._countDrawSelectFrame);
        }
    }
}
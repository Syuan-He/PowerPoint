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
    public class ModelTests
    {
        private const int X1 = 1;
        private const int Y1 = 20;
        Coordinate _point1 = new Coordinate(23, 16);
        Coordinate _point2 = new Coordinate(49, 33);
        MockState _mockState;

        MockFactory _mockFactory;
        Model _model;
        PrivateObject _modelPrivate;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _mockFactory = new MockFactory();
            _model = new Model(_mockFactory);
            _modelPrivate = new PrivateObject(_model);
        }

        // 測試建構式
        [TestMethod()]
        public void TestModel()
        {
            Assert.AreEqual(_mockFactory, _modelPrivate.GetFieldOrProperty("_factory"));
            Assert.IsNotNull(_modelPrivate.GetFieldOrProperty("_shapes"));
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty("_selectedIndex"));
            Assert.AreEqual(false, _modelPrivate.GetFieldOrProperty("_isPressed"));
            Assert.IsNotNull(_modelPrivate.GetFieldOrProperty("_pointer"));
        }

        // Test NotifyModelChanged
        [TestMethod()]
        public void TestNotifyModelChanged()
        {
            bool eventRaised = false;
            _model._modelChanged += () => eventRaised = true;
            _model.NotifyModelChanged();
            Assert.IsTrue(eventRaised);
        }

        // Test PressInfoAdd
        [TestMethod()]
        [DataRow(ShapeType.LINE)]
        [DataRow(ShapeType.RECTANGLE)]
        [DataRow(ShapeType.CIRCLE)]
        [DataRow("")]
        public void TestPressInfoAdd(string type)
        {
            bool eventRaised = false;
            _model._modelChanged += () => eventRaised = true;

            _model.PressInfoAdd(type);
            Assert.AreEqual(type, _mockFactory._shapeType);
            Assert.IsTrue(eventRaised);
        }

        // Test PressDelete
        [TestMethod()]
        [DataRow(1, 2, false, 3)]
        [DataRow(0, 2, true, 2)]
        [DataRow(0, -1, false, 3)]
        public void TestPressDelete(int column, int row, bool ans, int len)
        {
            AddShape();
            bool eventRaised = false;
            _model._modelChanged += () => eventRaised = true;
            _model.PressDelete(column, row);
            Shapes shapes = (Shapes)_modelPrivate.GetFieldOrProperty("_shapes");
            Assert.AreEqual(ans, eventRaised);
            Assert.AreEqual(len, shapes.ShapeList.Count());
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty("_selectedIndex"));
        }

        // 替 model 的 shapes 加料
        void AddShape()
        {
            _model.PressInfoAdd(ShapeType.LINE);
            _model.PressInfoAdd(ShapeType.RECTANGLE);
            _model.PressInfoAdd(ShapeType.CIRCLE);
        }

        // Test PressDeleteKey
        [TestMethod()]
        [DataRow(-1, false, 3)]
        [DataRow(0, true, 2)]
        [DataRow(2, true, 2)]
        [DataRow(3, true, 3)]
        public void TestPressDeleteKey(int row, bool ans, int len)
        {
            AddShape();
            bool eventRaised = false;
            _model._modelChanged += () => eventRaised = true;
            _modelPrivate.SetFieldOrProperty("_selectedIndex", row);
            _model.PressDeleteKey();
            Shapes shapes = (Shapes)_modelPrivate.GetFieldOrProperty("_shapes");
            Assert.AreEqual(ans, eventRaised);
            Assert.AreEqual(len, shapes.ShapeList.Count());
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty("_selectedIndex"));
        }

        // Test SetPoint
        [TestMethod()]
        public void TestSetPoint()
        {
            _model.SetPoint();
            Assert.IsInstanceOfType(_modelPrivate.GetFieldOrProperty("_pointer"), typeof(PointPointer));
        }

        // Test SetDrawing
        [TestMethod()]
        public void TestSetDrawing()
        {
            _model.SetDrawing();
            Assert.IsInstanceOfType(_modelPrivate.GetFieldOrProperty("_pointer"), typeof(DrawingPointer));
        }

        // Test CreateHint
        [TestMethod()]
        public void TestCreateHint()
        {
            _modelPrivate.SetFieldOrProperty("_shapeType", ShapeType.LINE);
            Shape shape = _model.CreateHint(X1, Y1);
            Assert.AreEqual(ShapeType.LINE, shape.ShapeName);
            Assert.AreEqual(String.Format("({0}, {1}), ({0}, {1})", X1, Y1), shape.Information);
        }

        // Test CreateShape
        [TestMethod()]
        public void TestCreateShape()
        {
            bool eventRaised = false;
            _model._modelChanged += () => eventRaised = true;

            _model.CreateShape(ShapeType.LINE, _point1, _point2);
            Assert.AreEqual(ShapeType.LINE, _mockFactory._shapeType);
            Assert.AreEqual(_point1, _mockFactory._point1);
            Assert.AreEqual(_point2, _mockFactory._point2);
            Assert.IsTrue(eventRaised);
        }

        // Test FindSelectShape
        [TestMethod()]
        [DataRow(0, 1, true)]
        [DataRow(0, 5, false)]
        [DataRow(4, 6, true)]
        [DataRow(11, 1, false)]
        public void TestFindSelectShape(int x1, int y1, bool ans)
        {
            AddShape();
            bool isFind = _model.FindSelectShape(x1, y1);
            Assert.AreEqual(ans, isFind);
        }

        // Test MoveShape
        [TestMethod()]
        public void TestMoveShape()
        {
            AddShape();
            _modelPrivate.SetFieldOrProperty("_selectedIndex", 2);
            _model.MoveShape(1, 1);
            Shapes shapes = (Shapes)_modelPrivate.GetFieldOrProperty("_shapes");
            Assert.AreEqual("(9, 10), (11, 12)", shapes.ShapeList[2].Information);
        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            AddShape();
            _modelPrivate.SetFieldOrProperty("_selectedIndex", 1);
            MockIGraphics graphics = new MockIGraphics();
            _model.DrawSelectFrame(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            SetMockState();
            _model.PressPointer("", 1, 1);
            Assert.IsTrue((bool) _modelPrivate.GetField("_isPressed"));
            Assert.AreEqual("", _modelPrivate.GetField("_shapeType"));
            Assert.AreEqual(1, _mockState._countPress);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());
        }

        // 替 model 植入 mockState
        void SetMockState()
        {
            _mockState = new MockState();
            _modelPrivate.SetFieldOrProperty("_pointer", _mockState);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            SetMockState();
            bool eventRaised = false;
            _model._modelChanged += () => eventRaised = true;

            _model.MovePointer(1, 1);
            Assert.IsFalse(eventRaised);
            Assert.AreEqual(0, _mockState._countMove);

            _model.PressPointer("", 0, 0);
            eventRaised = false;
            _model.MovePointer(1, 1);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(1, _mockState._countMove);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());
        }

        // TestReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            SetMockState();
            _model.ReleasePointer(1, 1);
            Assert.IsFalse((bool)_modelPrivate.GetField("_isPressed"));
            Assert.AreEqual(1, _mockState._countRelease);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());
        }

        // Test Draw
        [TestMethod()]
        [DataRow(true, false, 0)]
        [DataRow(true, true, 1)]
        [DataRow(false, true, 0)]
        public void TestDraw(bool panel, bool press, int ans)
        {
            AddShape();
            MockIGraphics graphics = new MockIGraphics();
            SetMockState();

            _modelPrivate.SetField("_isPressed", press);
            _model.Draw(graphics, panel);
            Assert.AreEqual(1, graphics._countClear);
            Assert.AreEqual(1, graphics._countDrawCircle);
            Assert.AreEqual(1, graphics._countDrawLine);
            Assert.AreEqual(1, graphics._countDrawRectangle);
            Assert.AreEqual(ans, _mockState._countDraw);
        }

        // Test GetInfoDataGridView
        [TestMethod()]
        public void TestGetInfoDataGridView()
        {
            Shapes shapes = (Shapes)_modelPrivate.GetField("_shapes");
            Assert.AreEqual(shapes.ShapeList, _model.GetInfoDataGridView());
        }
    }
}
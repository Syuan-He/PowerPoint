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
            _modelPrivate.Invoke("NotifyPanelChanged");
            Assert.IsFalse(eventRaised);
            _model._panelChanged += () => eventRaised = true;
            _modelPrivate.Invoke("NotifyPanelChanged");
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
            _model._panelChanged += () => eventRaised = true;

            _model.PressInfoAdd(type, 0, 0);
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
            _model._panelChanged += () => eventRaised = true;
            Shapes shapes = (Shapes)_modelPrivate.GetFieldOrProperty("_shapes");

            _model.PressDelete(column, row);

            Assert.AreEqual(ans, eventRaised);
            Assert.AreEqual(len, shapes.ShapeList.Count());
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty("_selectedIndex"));
            IState state = (IState)_modelPrivate.GetFieldOrProperty("_pointer");
            Assert.IsInstanceOfType(state, typeof(PointPointer));
        }

        // 替 model 的 shapes 加料
        void AddShape()
        {
            _model.PressInfoAdd(ShapeType.LINE, 0, 0);
            _model.PressInfoAdd(ShapeType.RECTANGLE, 0, 0);
            _model.PressInfoAdd(ShapeType.CIRCLE, 0, 0);
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
            _model._panelChanged += () => eventRaised = true;
            _modelPrivate.SetFieldOrProperty("_selectedIndex", row);
            Shapes shapes = (Shapes)_modelPrivate.GetFieldOrProperty("_shapes");

            _model.PressDeleteKey();

            Assert.AreEqual(ans, eventRaised);
            Assert.AreEqual(len, shapes.ShapeList.Count());
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty("_selectedIndex"));
            IState state = (IState)_modelPrivate.GetFieldOrProperty("_pointer");
            Assert.IsInstanceOfType(state, typeof(PointPointer));
        }

        // Test SetPoint
        [TestMethod()]
        public void TestSetPoint()
        {
            _model.PressInfoAdd(ShapeType.LINE, 0, 0);
            _modelPrivate.SetField("_selectedIndex", 0);
            _model.SetPoint();
            IState state = (IState)_modelPrivate.GetFieldOrProperty("_pointer");
            Assert.IsInstanceOfType(state, typeof(PointPointer));
            MockIGraphics graphics = new MockIGraphics();
            state.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }

        // Test SetDrawing
        [TestMethod()]
        public void TestSetDrawing()
        {
            _model.SetDrawing();
            Assert.IsInstanceOfType(_modelPrivate.GetFieldOrProperty("_pointer"), typeof(DrawingPointer));
            Assert.AreEqual(-1, _modelPrivate.GetField("_selectedIndex"));
        }

        // Test SetScaling
        [TestMethod()]
        public void TestSetScaling()
        {
            _model.PressInfoAdd(ShapeType.LINE, 0, 0);
            _modelPrivate.SetField("_selectedIndex", 0);
            _model.SetScaling();
            IState state = (IState)_modelPrivate.GetFieldOrProperty("_pointer");
            Assert.IsInstanceOfType(state, typeof(ScalingPointer));
            MockIGraphics graphics = new MockIGraphics();
            state.Draw(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }

        // Test IsHasSelected
        [TestMethod]
        [DataRow(false, -1)]
        [DataRow(true, 0)]
        public void TestIsHasSelected(bool ans, int index)
        {
            _modelPrivate.SetField("_selectedIndex", index);
            Assert.AreEqual(ans, _model.IsHasSelected());
        }

        // Test GetAtSelectedCorner
        [TestMethod]
        public void TestGetAtSelectedCorner()
        {
            _model.PressInfoAdd(ShapeType.LINE, 0, 0);
            _modelPrivate.SetField("_selectedIndex", 0);
            Assert.AreEqual(8, _model.GetAtSelectedCorner(2, 3));
            Assert.AreEqual(-1, _model.GetAtSelectedCorner(9,10));
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            AddShape();
            SetMockState();

            _model.PressPointer(null, 1, 1);
            Assert.IsTrue((bool) _modelPrivate.GetField("_isPressed"));
            Assert.AreEqual(null, _modelPrivate.GetField("_shapeType"));
            Assert.AreEqual(0, _modelPrivate.GetField("_selectedIndex"));
            Assert.AreEqual(1, _mockState._countPress);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());
            Assert.AreEqual(_model.GetInfoDataGridView()[0], _mockState._shape);

            _model.PressPointer(null, 1, 1);
            Assert.AreEqual(0, _modelPrivate.GetField("_selectedIndex"));
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(ScalingPointer));

            SetMockState();
            _model.PressPointer(null, 100, 100);
            Assert.IsInstanceOfType(_modelPrivate.GetField("_pointer"), typeof(MockState));

            _model.PressPointer(ShapeType.CIRCLE, 100, 100);
            Assert.AreEqual(_mockState._shape.ShapeName, _mockFactory._shapeType);
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
            _model._panelChanged += () => eventRaised = true;

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
            bool eventRaised = false;
            _model._panelChanged += () => eventRaised = true;
            SetMockState();
            _modelPrivate.SetField("_shapeType", null);
            _modelPrivate.SetField("_shape", new Circle(_point1, _point2));

            _model.ReleasePointer(1, 1);
            Assert.IsFalse((bool)_modelPrivate.GetField("_isPressed"));
            Assert.AreEqual(1, _mockState._countRelease);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());
            Assert.IsFalse(eventRaised);
            Assert.AreEqual(0, _model.GetInfoDataGridView().Count);

            _modelPrivate.SetField("_shapeType", ShapeType.CIRCLE);

            _model.ReleasePointer(1, 1);
            Assert.IsTrue(eventRaised);
            Assert.AreEqual(1, _model.GetInfoDataGridView().Count);
        }

        // Test Draw
        [TestMethod()]
        [DataRow(true, false, 1)]
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

        // Test NotifyPanelChanged
        [TestMethod]
        public void TestNotifyPanelChanged()
        {
            bool eventRaised = false;
            _model._panelChanged += () => eventRaised = true;

            _modelPrivate.Invoke("NotifyPanelChanged");
            Assert.IsTrue(eventRaised);
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
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private const string INDEX = "_selectedIndex";
        Coordinate _point1 = new Coordinate(23, 16);
        Coordinate _point2 = new Coordinate(49, 33);
        MockState _mockState;

        MockFactory _mockFactory;
        Model _model;
        PrivateObject _modelPrivate;
        Shapes _shapes;

        bool _eventRaised;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _mockFactory = new MockFactory();
            _model = new Model(_mockFactory, new MockService());
            _modelPrivate = new PrivateObject(_model);
        }

        // 測試建構式
        [TestMethod()]
        public void TestModel()
        {
            Assert.AreEqual(_mockFactory, _modelPrivate.GetFieldOrProperty("_factory"));
            Assert.IsNotNull(_modelPrivate.GetFieldOrProperty("CurrentShapes"));
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty(INDEX));
            Assert.AreEqual(false, _modelPrivate.GetFieldOrProperty("_isPressed"));
            Assert.IsNotNull(_modelPrivate.GetFieldOrProperty("_pointer"));
            Assert.IsNotNull(_modelPrivate.GetField("_commandManager"));
        }

        // Test NotifyModelChanged
        [TestMethod()]
        public void TestNotifyModelChanged()
        {
            _eventRaised = false;
            _modelPrivate.Invoke("NotifyPanelChanged");
            Assert.IsFalse(_eventRaised);
            _model._panelChanged += () => _eventRaised = true;
            _modelPrivate.Invoke("NotifyPanelChanged");
            Assert.IsTrue(_eventRaised);
        }

        // 設定測試用的 event
        void SetEvent()
        {
            _eventRaised = false;
            _model._panelChanged += () => _eventRaised = true;
        }

        // 取 model 內的 shapes
        void GetShapes()
        {
            _shapes = (Shapes)_modelPrivate.GetProperty("CurrentShapes");
        }

        // 替 model 的 shapes 加料
        void Add3Shapes()
        {
            _model.PressInfoAdd(ShapeType.LINE, new Coordinate(0, 1), new Coordinate(2, 3));
            _model.PressInfoAdd(ShapeType.RECTANGLE, new Coordinate(4, 5), new Coordinate(6, 7));
            _model.PressInfoAdd(ShapeType.CIRCLE, new Coordinate(8, 9), new Coordinate(10, 11));
        }

        // Test CreateShapeCommand
        [TestMethod]
        public void TestCreateShapeCommand()
        {
            GetShapes();
            SetEvent();
            Shape shape = new Line(_point1, _point2);
            _model.CreateShapeCommand(shape, 0);
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            Assert.AreEqual(shape, _shapes.ShapeList[0]);
        }

        // Test RemoveLast
        [TestMethod]
        public void TestRemoveLast()
        {
            GetShapes();
            _model.RemoveLast(0);
            Add3Shapes();
            _model.RemoveLast(0);
            Assert.AreEqual(2, _shapes.ShapeList.Count);
        }

        // Test CreateShape
        [TestMethod]
        public void TestCreateShape()
        {
            GetShapes();
            SetEvent();
            Shape shape = new Line(_point1, _point2);
            _model.CreateShape(shape);
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            Assert.AreEqual(shape, _shapes.ShapeList[0]);
        }

        // Test RemoveAt
        [TestMethod]
        public void TestRemoveAt()
        {
            Assert.IsNull(_model.RemoveAt(0, 0));
            Shape shape = new Line(_point1, _point2);
            _model.CreateShape(shape);
            SetEvent();
            Assert.AreEqual(shape, _model.RemoveAt(0, 0));
            Assert.AreEqual(-1, _modelPrivate.GetField(INDEX));
            Assert.IsTrue(_eventRaised);
        }

        // Test Insert
        [TestMethod]
        public void TestInsert()
        {
            SetEvent();
            _model.Insert(new Line(_point1, _point2), 0, 0);
            Assert.IsTrue(_eventRaised);
            
        }

        // Test InsertPage
        [TestMethod]
        public void TestInsertPage()
        {
            _model.InsertPage(new Shapes(_mockFactory), _model.PagesCount);
            bool slideEventRaised = false;
            Assert.AreEqual(2, _model.PagesCount);
            _model._slideInsert += (_) => slideEventRaised = true;
            _model.InsertPage(new Shapes(_mockFactory), _model.PagesCount);
            Assert.AreEqual(3, _model.PagesCount);
            Assert.IsTrue(slideEventRaised);
        }

        // Test RemovePageAt
        [TestMethod]
        public void TestRemovePageAt()
        {
            _model.InsertPage(new Shapes(_mockFactory),_model.PagesCount);
            _model.RemovePageAt(0);
            bool slideEventRaised = false;
            _model._slideRemoveAt += (_) => slideEventRaised = true;
            SetEvent();
            _model.InsertPage(new Shapes(_mockFactory), _model.PagesCount);
            _model.PageIndex = 1;
            _model.RemovePageAt(_model.PageIndex);
            Assert.AreEqual(0, _model.PageIndex);
            Assert.IsTrue(slideEventRaised);
        }

        // Test SetShapePosition
        [TestMethod]
        public void TestSetShapePosition()
        {
            SetEvent();
            GetShapes();
            Add3Shapes();
            _model.SetShapePosition(2, 0, new Coordinate(1, 1));
            Assert.AreEqual("(1, 1), (3, 3)", _shapes.ShapeList[2].Information);
            Assert.IsTrue(_eventRaised);
        }

        // TestSetShapeEndPoint
        [TestMethod]
        public void TestSetShapeEndPoint()
        {

            SetEvent();
            GetShapes();
            Add3Shapes();
            _model.SetShapeEndPoint(2, 0, new Coordinate(1, 1), 8);
            Assert.AreEqual("(1, 1), (8, 9)", _shapes.ShapeList[2].Information);
            Assert.IsTrue(_eventRaised);
        }

        // Test PressInfoAdd
        [TestMethod()]
        [DataRow(ShapeType.LINE, 1)]
        [DataRow(ShapeType.RECTANGLE, 1)]
        [DataRow(ShapeType.CIRCLE, 1)]
        [DataRow("", 0)]
        public void TestPressInfoAdd(string type, int ans)
        {
            _model.PressInfoAdd(type, new Coordinate(0, 1), new Coordinate(2, 3));
            Assert.AreEqual(ans, _model.GetInfoDataGridView().Count);
        }

        // Test PressDelete
        [TestMethod()]
        [DataRow(1, 2, false, 3)]
        [DataRow(0, 2, true, 2)]
        [DataRow(0, -1, false, 3)]
        public void TestPressDelete(int column, int row, bool ans, int len)
        {
            Add3Shapes();
            bool eventRaised = false;
            _model._panelChanged += () => eventRaised = true;
            GetShapes();

            _model.PressDelete(column, row);

            Assert.AreEqual(ans, eventRaised);
            Assert.AreEqual(len, _shapes.ShapeList.Count());
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty(INDEX));
            IState state = (IState)_modelPrivate.GetFieldOrProperty("_pointer");
            Assert.IsInstanceOfType(state, typeof(PointPointer));
        }

        // Test PressAddPage
        [TestMethod]
        public void TestPressAddPage()
        {
            _model.PressAddPage();
            Assert.AreEqual(_model.PageIndex, _model.PagesCount - 1);
        }

        // Test PressDeleteKey
        [TestMethod()]
        [DataRow(-1, false, 3)]
        [DataRow(0, true, 2)]
        [DataRow(2, true, 2)]
        [DataRow(3, true, 3)]
        public void TestPressDeleteKey(int row, bool ans1, int len)
        {
            Add3Shapes();
            SetEvent();
            _modelPrivate.SetFieldOrProperty(INDEX, row);
            GetShapes();

            _model.PressDeleteKey();

            Assert.AreEqual(ans1, _eventRaised);
            Assert.AreEqual(len, _shapes.ShapeList.Count());
            Assert.AreEqual(-1, _modelPrivate.GetFieldOrProperty(INDEX));
        }

        // Test PressDeleteKey for page
        [TestMethod()]
        public void TestPressDeleteKeyForPage()
        {
            bool slideEventRaised = false;
            _model._slideRemoveAt += (_) => slideEventRaised = true;
            _model.PressDeleteKey();
            Assert.AreEqual(false, slideEventRaised);
            Assert.AreEqual(1, _model.PagesCount);

            _model.PressAddPage();
            Assert.AreEqual(2, _model.PagesCount);
            _model.PressDeleteKey();
            Assert.AreEqual(true, slideEventRaised);
            Assert.AreEqual(1, _model.PagesCount);
        }

        // Test SetPoint
        [TestMethod()]
        public void TestSetPoint()
        {
            _model.SetPoint();
            IState state = (IState)_modelPrivate.GetFieldOrProperty("_pointer");
            Assert.IsInstanceOfType(state, typeof(PointPointer));
        }

        // Test SetDrawing
        [TestMethod()]
        public void TestSetDrawing()
        {
            _model.SetDrawing();
            Assert.IsInstanceOfType(_modelPrivate.GetFieldOrProperty("_pointer"), typeof(DrawingPointer));
            Assert.AreEqual(-1, _modelPrivate.GetField(INDEX));
        }

        // Test SetScaling
        [TestMethod()]
        public void TestSetScaling()
        {
            _model.PressInfoAdd(ShapeType.LINE, new Coordinate(0, 1), new Coordinate(2, 3));
            _modelPrivate.SetField(INDEX, 0);
            _model.SetScaling(new Coordinate(20, 64));
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
            _modelPrivate.SetField(INDEX, index);
            Assert.AreEqual(ans, _model.IsHasSelected());
        }

        // Test GetAtSelectedCorner
        [TestMethod]
        public void TestGetAtSelectedCorner()
        {
            _model.PressInfoAdd(ShapeType.LINE, new Coordinate(0, 1), new Coordinate(2, 3));
            _modelPrivate.SetField(INDEX, 0);
            Assert.AreEqual(8, _model.GetAtSelectedCorner(2, 3));
            Assert.AreEqual(-1, _model.GetAtSelectedCorner(9,10));
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            SetMockState();

            _model.PressPointer(null, 1, 1);
            Assert.AreEqual(true, (bool) _modelPrivate.GetField("_isPressed"));
            Assert.AreEqual(null, _modelPrivate.GetField("_shapeType"));
            Assert.AreEqual(1, _mockState._countPress);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());

            _model.PressPointer(ShapeType.CIRCLE, 100, 100);
            Assert.AreEqual(ShapeType.CIRCLE, _modelPrivate.GetField("_shapeType"));
            Assert.AreEqual("(100, 100)", _mockState._point.ToString());
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
            SetMockState();
            _modelPrivate.SetField("_isPressed", true);

            _model.ReleasePointer(1, 1);
            Assert.IsFalse((bool)_modelPrivate.GetField("_isPressed"));
            Assert.AreEqual(1, _mockState._countRelease);
            Assert.AreEqual("(1, 1)", _mockState._point.ToString());
        }

        // Test DrawSelectFrame
        [TestMethod()]
        public void TestDrawSelectFrame()
        {
            Add3Shapes();
            _modelPrivate.SetFieldOrProperty(INDEX, 1);
            MockIGraphics graphics = new MockIGraphics();
            _model.DrawSelectFrame(graphics);
            Assert.AreEqual(1, graphics._countDrawSelectFrame);
        }

        // Test Draw
        [TestMethod()]
        [DataRow(false, 1)]
        [DataRow(true, 1)]
        public void TestDraw(bool press, int ans)
        {
            Add3Shapes();
            MockIGraphics graphics = new MockIGraphics();
            SetMockState();

            _modelPrivate.SetField("_isPressed", press);
            _model.Draw(graphics);
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
            GetShapes();
            Assert.AreEqual(_shapes.ShapeList, _model.GetInfoDataGridView());
        }

        // Test FindSelected
        [TestMethod]
        public void TestFindSelected()
        {
            Add3Shapes();
            SetEvent();
            _model.FindSelected(4, 6);
            Assert.AreEqual(1, _modelPrivate.GetField(INDEX));
            Assert.IsTrue(_eventRaised);
        }

        // Test GetSelected
        [TestMethod]
        public void GetSelected()
        {
            Add3Shapes();
            GetShapes();
            _model.FindSelected(4, 6);
            Assert.AreEqual(_shapes.ShapeList[1], _model.GetSelected());
        }

        // Test MoveSelected
        [TestMethod()]
        public void TestMoveSelected()
        {
            Add3Shapes();
            _modelPrivate.SetFieldOrProperty("_selectedIndex", 2);
            _model.MoveSelected(new Coordinate(8, 9), new Coordinate(9, 10));
            GetShapes();
            Assert.AreEqual("(9, 10), (11, 12)", _shapes.ShapeList[2].Information);
        }

        // Test CreateHint
        [TestMethod]
        public void TestCreateHint()
        {
            _model.PressPointer(ShapeType.CIRCLE, 12, 34);
            Shape shape = _model.CreateHint(1, 2);
            Assert.AreEqual(String.Format("({0}, {1}), ({0}, {1})", 1, 2), shape.Information);
        }
        // Test PressUndo
        [TestMethod]
        public void PressUndo()
        {
            Add3Shapes();
            _model.PressUndo();
            GetShapes();
            Assert.AreEqual(2, _shapes.ShapeList.Count);
        }

        // Test PressUndo
        [TestMethod]
        public void PressRedo()
        {
            Add3Shapes();
            _model.PressUndo();
            _model.PressRedo();
            GetShapes();
            Assert.AreEqual(3, _shapes.ShapeList.Count);
        }

        // Test ScalingSelected
        [TestMethod]
        public void TestScalingSelected()
        {
            Add3Shapes();
            _modelPrivate.SetFieldOrProperty("_selectedIndex", 2);
            _model.ScalingSelected(new Coordinate(8, 9), new Coordinate(10, 11));
            GetShapes();
            Assert.AreEqual("(8, 9), (10, 11)", _shapes.ShapeList[2].Information);
            _model.PressUndo();
            Assert.AreEqual("(8, 9), (8, 9)", _shapes.ShapeList[2].Information);
            _model.PressRedo();
            Assert.AreEqual("(8, 9), (10, 11)", _shapes.ShapeList[2].Information);
        }

        // Test IsUndoEnabled
        [TestMethod]
        public void TestIsUndoEnabled()
        {
            Assert.IsFalse(_model.IsUndoEnabled);
            Add3Shapes();
            Assert.IsTrue(_model.IsUndoEnabled);
        }

        // Test IsRedoEnabled
        [TestMethod]
        public void TestIsRedoEnabled()
        {
            Assert.IsFalse(_model.IsRedoEnabled);
            Add3Shapes();
            Assert.IsFalse(_model.IsRedoEnabled);
            _model.PressUndo();
            Assert.IsTrue(_model.IsRedoEnabled);
            Add3Shapes();
            Assert.IsFalse(_model.IsRedoEnabled);
        }
    }
}
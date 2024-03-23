using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class PresentationModelTests
    {
        private const int WIDTH = 1920;
        MockModel _model;
        PresentationModel _pModel;
        PrivateObject _pModelPrivate;
        string type;

        // Initial
        [TestInitialize()]
        public void Initialize()
        {
            _model = new MockModel();
            _pModel = new PresentationModel(_model);
            _pModelPrivate = new PrivateObject(_pModel);
        }

        // Test IsLine get
        [TestMethod()]
        public void TestIsLine()
        {
            Assert.IsFalse(_pModel.IsLine);
            _pModel.PressLineButton();
            Assert.IsTrue(_pModel.IsLine);
        }

        // Test IsRectangle get
        [TestMethod()]
        public void TestIsRectangle()
        {
            Assert.IsFalse(_pModel.IsRectangle);
            _pModel.PressRectangleButton();
            Assert.IsTrue(_pModel.IsRectangle);
        }

        // Test IsCircle get
        [TestMethod()]
        public void TestIsCircle()
        {
            Assert.IsFalse(_pModel.IsCircle);
            _pModel.PressCircleButton();
            Assert.IsTrue(_pModel.IsCircle);
        }

        // Test IsPointer get
        [TestMethod()]
        public void TestIsPointer()
        {
            Assert.IsTrue(_pModel.IsPointer);
            _pModel.PressLineButton();
            Assert.IsFalse(_pModel.IsPointer);
        }

        // Test PressLineButton
        [TestMethod()]
        public void TestPressLineButton()
        {
            bool eventRaised = false;
            _pModel.PropertyChanged += (Object sender, PropertyChangedEventArgs e) => eventRaised = true;
            bool[] _boolList = (bool[])_pModelPrivate.GetField("_booleanToolStripList");
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[0]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);

            _pModel.PressLineButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsTrue(_boolList[0]);
            Assert.IsFalse(_boolList[3]);
            Assert.AreEqual(ShapeType.LINE, type);
            Assert.IsTrue(eventRaised);

            _pModel.PressLineButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[0]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);
        }

        // Test PressRectangleButton
        [TestMethod()]
        public void TestPressRectangleButton()
        {
            bool eventRaised = false;
            _pModel.PropertyChanged += (Object sender, PropertyChangedEventArgs e) => eventRaised = true;
            bool[] _boolList = (bool[])_pModelPrivate.GetField("_booleanToolStripList");
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[1]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);

            _pModel.PressRectangleButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsTrue(_boolList[1]);
            Assert.IsFalse(_boolList[3]);
            Assert.AreEqual(ShapeType.RECTANGLE, type);
            Assert.IsTrue(eventRaised);

            _pModel.PressRectangleButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[1]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);
        }

        // Test PressCircleButton
        [TestMethod()]
        public void TestPressCircleButton()
        {
            bool eventRaised = false;
            _pModel.PropertyChanged += (Object sender, PropertyChangedEventArgs e) => eventRaised = true;
            bool[] _boolList = (bool[])_pModelPrivate.GetField("_booleanToolStripList");
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[2]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);

            _pModel.PressCircleButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsTrue(_boolList[2]);
            Assert.IsFalse(_boolList[3]);
            Assert.AreEqual(ShapeType.CIRCLE, type);
            Assert.IsTrue(eventRaised);

            _pModel.PressCircleButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[2]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);
        }

        // Test PressPointerButton
        [TestMethod()]
        public void TestPressPointerButton()
        {
            bool eventRaised = false;
            _pModel.PropertyChanged += (Object sender, PropertyChangedEventArgs e) => eventRaised = true;
            bool[] _boolList = (bool[])_pModelPrivate.GetField("_booleanToolStripList");
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[0]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);

            _pModel.PressPointerButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[0]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);
            Assert.AreEqual(0, _model._stateNumber);
            Assert.IsTrue(eventRaised);

            _pModel.PressLineButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsTrue(_boolList[0]);
            Assert.IsFalse(_boolList[3]);
            Assert.AreEqual(ShapeType.LINE, type);

            _pModel.PressPointerButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            Assert.IsFalse(_boolList[0]);
            Assert.IsTrue(_boolList[3]);
            Assert.AreEqual(null, type);
        }

        // Test PressToolStrip
        [TestMethod]
        public void TestPressToolStrip()
        {
            _pModel.PressLineButton();
            Assert.IsTrue(_pModel.IsLine);
            Assert.IsFalse(_pModel.IsPointer);
            _pModel.PressLineButton();
            Assert.IsFalse(_pModel.IsLine);
            Assert.IsTrue(_pModel.IsPointer);
            _pModel.PressLineButton();
            Assert.IsTrue(_pModel.IsLine);
            _pModel.PressRectangleButton();
            Assert.IsTrue(_pModel.IsRectangle);
            Assert.IsFalse(_pModel.IsLine);
            _pModel.PressCircleButton();
            Assert.IsTrue(_pModel.IsCircle);
            Assert.IsFalse(_pModel.IsRectangle);
        }

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _pModel.PressCircleButton();
            _pModel.PressPointer(0, 1, WIDTH);
            Assert.AreEqual("(0, 1)", _model._point1.ToString());
            Assert.AreEqual(ShapeType.CIRCLE, _model._shapeType);
        }

        // Test MovePointer
        [TestMethod()]
        public void TestMovePointer()
        {
            _pModel.MovePointer(0, 1, WIDTH);
            Assert.AreEqual("(0, 1)", _model._point1.ToString());
        }

        // Test ReleasePointer
        [TestMethod()]
        public void TestReleasePointer()
        {
            _pModel.ReleasePointer(0, 1, WIDTH);
            Assert.AreEqual("(0, 1)", _model._point1.ToString());
        }

        // Test PressDelete
        [TestMethod()]
        public void TestPressDelete()
        {
            _pModel.PressDelete(Keys.Control);
            Assert.AreEqual(0, _model._countPressDeleteKey);
            _pModel.PressDelete(Keys.Delete);
            Assert.AreEqual(1, _model._countPressDeleteKey);
        }

        // Test GetPointerShape
        [TestMethod()]
        public void TestGetPointerShape()
        {
            type = (string)_pModelPrivate.GetField("_shapeType");
            Cursor cursor = _pModel.GetPointerShape();
            Assert.AreEqual(null, type);
            Assert.AreEqual(Cursors.Default, cursor);

            _pModel.PressCircleButton();
            type = (string)_pModelPrivate.GetField("_shapeType");
            cursor = _pModel.GetPointerShape();
            Assert.AreEqual(ShapeType.CIRCLE, type);
            Assert.AreEqual(Cursors.Cross, cursor);
        }

        // Test GetCornerCursor
        [TestMethod]
        public void TestGetCornerCursor()
        {
            Assert.AreEqual(Cursors.Default, _pModelPrivate.Invoke("GetCornerCursor", new object[] { -1 }));
            Assert.AreEqual(Cursors.Default, _pModelPrivate.Invoke("GetCornerCursor", new object[] { 0 }));
            Assert.AreEqual(Cursors.SizeNWSE, _pModelPrivate.Invoke("GetCornerCursor", new object[] { 8 }));
            Assert.AreEqual(Cursors.Default, _pModelPrivate.Invoke("GetCornerCursor", new object[] { 9 }));
        }

        // Test GetPointerShape
        [TestMethod]
        public void TestGetPointerShapeWithCoordinate()
        {
            _pModel.PressCircleButton();
            Assert.AreEqual(Cursors.Cross, _pModel.GetPointerShape(0, 0, WIDTH));
            _pModel.PressPointerButton();
            Assert.AreEqual(Cursors.Default, _pModel.GetPointerShape(0, 0, WIDTH));
            Assert.AreEqual(Cursors.SizeNWSE, _pModel.GetPointerShape(1, 1, WIDTH));
            _model._isHasSelected = false;
            Assert.AreEqual(Cursors.Default, _pModel.GetPointerShape(1, 1, WIDTH));
        }

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            Graphics graphics = null;
            _pModel.Draw(graphics, WIDTH);
            Assert.IsInstanceOfType(_model._graphics, typeof(WindowsFormsGraphicsAdaptor));
        }

        // Test DrawSlide
        [TestMethod()]
        public void TestDrawSlide()
        {
            Graphics graphics = null;
            _pModel.DrawSlide(graphics, 1, 0);
            Assert.IsInstanceOfType(_model._graphics, typeof(WindowsFormsGraphicsAdaptor));
        }

        // Test SetPanelSize
        [TestMethod]
        [DataRow(160, 54, 32, 0)]
        [DataRow(640, 1280, 0, 460)]
        public void TestSetPanelSize(int width, int height, int localX, int localY)
        {
            _pModel.SetPanelSize(width, height);
            Assert.AreEqual(_pModel.PanelWidth / _pModel.PanelHeight, 16 / 9);
            Assert.AreEqual(_pModel.PanelLocal.X, localX);
            Assert.AreEqual(_pModel.PanelLocal.Y, localY);
        }

        // Test GetPanelHeight
        [TestMethod]
        public void TestGetPanelHeight()
        {
            Assert.AreEqual(1280 / _pModel.GetPanelHeight(1280), 16 / 9);
        }

        // Test NotifyPropertyChanged
        [TestMethod()]
        public void TestNotifyPropertyChanged()
        {
            bool eventRaised = false;
            _pModel.PropertyChanged += (Object sender, PropertyChangedEventArgs e) => eventRaised = true;
            _pModelPrivate.Invoke("NotifyPropertyChanged");
            Assert.IsTrue(eventRaised);
        }
    }
}
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

        // Test PressPointer
        [TestMethod()]
        public void TestPressPointer()
        {
            _pModel.PressCircleButton();
            _pModel.PressPointer(0, 1);
            Assert.AreEqual("(0, 1)", _model._point1.ToString());
            Assert.AreEqual(ShapeType.CIRCLE, _model._shapeType);
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

        // Test Draw
        [TestMethod()]
        public void TestDraw()
        {
            Graphics graphics = null;
            _pModel.Draw(graphics);
            Assert.IsInstanceOfType(_model._graphics, typeof(WindowsFormsGraphicsAdaptor));
        }

        // Test DrawSlide
        [TestMethod()]
        public void TestDrawSlide()
        {
            Graphics graphics = null;
            _pModel.DrawSlide(graphics);
            Assert.IsInstanceOfType(_model._graphics, typeof(SlideAdaptor));
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
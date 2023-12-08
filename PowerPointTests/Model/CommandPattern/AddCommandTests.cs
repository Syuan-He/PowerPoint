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
    public class AddCommandTests
    {
        Coordinate _point1 = new Coordinate(23, 16);
        Coordinate _point2 = new Coordinate(49, 33);
        Model _model;
        Shape _shape;
        AddCommand _command;
        PrivateObject _commandPrivate;
        PrivateObject _modelPrivate;
        Shapes _shapes;

        //Initial
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model(new Factory(new Random()));
            _shape = new Line(_point1, _point2);
            _command = new AddCommand(_model, _shape);
            _commandPrivate = new PrivateObject(_command);
            _modelPrivate = new PrivateObject(_model);
            _shapes = (Shapes)_modelPrivate.GetField("_shapes");
        }

        // Test AddCommand
        [TestMethod()]
        public void TestAddCommand()
        {
            Assert.IsNotNull(_commandPrivate.GetField("_model"));
            Assert.IsNotNull(_commandPrivate.GetField("_shape"));
        }

        // Test Execute
        [TestMethod()]
        public void TestExecute()
        {
            _command.Execute();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            Assert.AreEqual(_shape, _shapes.ShapeList[0]);
        }

        // Test Undo
        [TestMethod()]
        public void TestUndo()
        {
            _command.Execute();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            _command.Undo();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
        }

        // Test Redo
        [TestMethod()]
        public void TestRedo()
        {
            _command.Execute();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            _command.Undo();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
            _command.Redo();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            Assert.AreEqual(_shape, _shapes.ShapeList[0]);
        }
    }
}
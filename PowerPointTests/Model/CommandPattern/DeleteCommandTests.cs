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
    public class DeleteCommandTests
    {
        Coordinate _point1 = new Coordinate(23, 16);
        Coordinate _point2 = new Coordinate(49, 33);
        Model _model;
        Shape _shape;
        DeleteCommand _command;
        PrivateObject _commandPrivate;
        PrivateObject _modelPrivate;
        Shapes _shapes;

        //Initial
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model(new Factory(new MockRandom()), new MockService());
            _shape = new Line(_point1, _point2);
            _model.CreateShapeCommand(_shape, 0);
            _command = new DeleteCommand(_model, 0, 0);
            _commandPrivate = new PrivateObject(_command);
            _modelPrivate = new PrivateObject(_model);
            _shapes = (Shapes)_modelPrivate.GetProperty("CurrentShapes");
        }

        // Test DeleteCommand
        [TestMethod()]
        public void TestDeleteCommand()
        {
            Assert.IsNotNull(_commandPrivate.GetField("_model"));
            Assert.IsNotNull(_commandPrivate.GetField("_index"));
        }

        // Test Execute
        [TestMethod()]
        public void TestExecute()
        {
            _command.Execute();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
            Assert.AreEqual(_shape, _commandPrivate.GetField("_shape"));
        }

        // Test Undo
        [TestMethod()]
        public void TestUndo()
        {
            _command.Execute();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
            _command.Undo();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
        }

        // Test Redo
        [TestMethod()]
        public void TestRedo()
        {
            _command.Execute();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
            _command.Undo();
            Assert.AreEqual(1, _shapes.ShapeList.Count);
            _command.Redo();
            Assert.AreEqual(0, _shapes.ShapeList.Count);
        }
    }
}
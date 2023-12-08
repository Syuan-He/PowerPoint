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
    public class ScalingCommandTests
    {
        Coordinate _point1 = new Coordinate(23, 16);
        Coordinate _point2 = new Coordinate(49, 33);
        Model _model;
        Shape _shape;
        ScalingCommand _command;
        PrivateObject _commandPrivate;
        PrivateObject _modelPrivate;
        Shapes _shapes;

        //Initial
        [TestInitialize]
        public void Initialize()
        {
            _model = new Model(new Factory(new Random()));
            _shape = new Line(_point1, _point2);
            _model.CreateShapeCommand(_shape);
            _command = new ScalingCommand(_model, 0, _point2, _point1);
            _commandPrivate = new PrivateObject(_command);
            _modelPrivate = new PrivateObject(_model);
            _shapes = (Shapes)_modelPrivate.GetField("_shapes");
        }

        // Test MoveCommand
        [TestMethod()]
        public void TestMoveCommand()
        {
            Assert.IsNotNull(_commandPrivate.GetField("_model"));
            Assert.IsNotNull(_commandPrivate.GetField("_startPoint"));
            Assert.IsNotNull(_commandPrivate.GetField("_endPoint"));
            Assert.IsNotNull(_commandPrivate.GetField("_index"));
        }

        // Test Execute
        [TestMethod()]
        public void TestExecute()
        {
            _command.Execute();
            Assert.AreEqual("(23, 16), (23, 16)", _shapes.ShapeList[0].Information);
        }

        // Test Undo
        [TestMethod()]
        public void TestUndo()
        {
            _command.Execute();
            Assert.AreEqual("(23, 16), (23, 16)", _shapes.ShapeList[0].Information);
            _command.Undo();
            Assert.AreEqual("(23, 16), (49, 33)", _shapes.ShapeList[0].Information);
        }

        // Test Redo
        [TestMethod()]
        public void TestRedo()
        {
            _command.Execute();
            Assert.AreEqual("(23, 16), (23, 16)", _shapes.ShapeList[0].Information);
            _command.Undo();
            Assert.AreEqual("(23, 16), (49, 33)", _shapes.ShapeList[0].Information);
            _command.Redo();
            Assert.AreEqual("(23, 16), (23, 16)", _shapes.ShapeList[0].Information);
        }
    }
}
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
    public class AddPageCommandTests
    {
        Factory _factory;
        Model _model;
        AddPageCommand _command;
        PrivateObject _commandPrivate;
        Shapes _shapes;

        // Initial
        [TestInitialize]
        public void Initialize()
        {
            _factory = new Factory(new Random());
            _model = new Model(_factory, new MockService());
            _shapes = new Shapes(_factory);
            _command = new AddPageCommand(_model, 0, _shapes);
            _commandPrivate = new PrivateObject(_command);
        }

        // Test AddPageCommand
        [TestMethod()]
        public void TestAddPageCommand()
        {
            Assert.AreEqual(_shapes, _commandPrivate.GetField("_shapes"));
        }

        // Test Execute
        [TestMethod()]
        public void TestExecute()
        {
            _command.Execute();
            Assert.AreEqual(2, _model.PagesCount);
        }

        // Test Undo
        [TestMethod()]
        public void TestUndo()
        {
            _command.Execute();
            _command.Undo();
            Assert.AreEqual(1, _model.PagesCount);
        }

        // Test Redo
        [TestMethod()]
        public void TestRedo()
        {
            _command.Execute();
            _command.Undo();
            _command.Redo();
            Assert.AreEqual(2, _model.PagesCount);
        }
    }
}
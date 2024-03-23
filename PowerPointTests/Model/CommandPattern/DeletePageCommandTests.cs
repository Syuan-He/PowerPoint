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
    public class DeletePageCommandTests
    {
        Factory _factory;
        Model _model;
        DeletePageCommand _command;
        PrivateObject _commandPrivate;
        Shapes _shapes;

        // Initial
        [TestInitialize]
        public void Initialize()
        {
            _factory = new Factory(new Random());
            _model = new Model(_factory, new MockService());
            _shapes = new Shapes(_factory);
            _command = new DeletePageCommand(_model, 0);
            _commandPrivate = new PrivateObject(_command);
            _model.PressAddPage();
        }

        // Test DeletePageCommand
        [TestMethod()]
        public void TestDeletePageCommand()
        {
            Assert.IsNull(_commandPrivate.GetField("_shapes"));
        }

        // Test Execute
        [TestMethod()]
        public void TestExecute()
        {
            _command.Execute();
            Assert.AreEqual(1, _model.PagesCount);
        }

        // Test Undo
        [TestMethod()]
        public void TestUndo()
        {
            _command.Execute();
            _command.Undo();
            Assert.AreEqual(2, _model.PagesCount);
        }

        // Test Redo
        [TestMethod()]
        public void TestRedo()
        {
            _command.Execute();
            _command.Undo();
            _command.Redo();
            Assert.AreEqual(1, _model.PagesCount);
        }
    }
}
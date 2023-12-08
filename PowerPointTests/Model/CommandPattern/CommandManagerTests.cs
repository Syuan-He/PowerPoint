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
    public class CommandManagerTests
    {
        CommandManager _commandManager;
        PrivateObject _commandManagerPrivate;
        MockCommand _mockCommand;

        // Initial
        [TestInitialize]
        public void TestInitial()
        {
            _commandManager = new CommandManager();
            _commandManagerPrivate = new PrivateObject(_commandManager);
            _mockCommand = new MockCommand();
        }

        // Test Execute
        [TestMethod()]
        public void TestExecute()
        {
            _commandManager.Execute(_mockCommand);
            Assert.AreEqual(1, _mockCommand.CountExecute);
            Assert.AreEqual(1, ((Stack<ICommand>)_commandManagerPrivate.GetField("_undo")).Count);
        }

        // Test Undo
        [TestMethod()]
        public void TestUndo()
        {
            _commandManager.Execute(_mockCommand);
            _commandManager.Execute(_mockCommand);
            _commandManager.Undo();
            Assert.AreEqual(1, ((Stack<ICommand>)_commandManagerPrivate.GetField("_undo")).Count);
            Assert.AreEqual(1, _mockCommand.CountUndo);
        }

        // Test Undo Throw Exception
        [TestMethod()]
        [ExpectedException(typeof(Exception), "Cannot Undo exception\n")]
        public void TestUndoThrowException()
        {
            _commandManager.Undo();
        }

        // Test Redo
        [TestMethod()]
        public void TestRedo()
        {
            _commandManager.Execute(_mockCommand);
            _commandManager.Execute(_mockCommand);
            _commandManager.Undo();
            _commandManager.Undo();
            _commandManager.Redo();
            Assert.AreEqual(1, ((Stack<ICommand>)_commandManagerPrivate.GetField("_redo")).Count);
            Assert.AreEqual(1, ((Stack<ICommand>)_commandManagerPrivate.GetField("_undo")).Count);
            Assert.AreEqual(1, _mockCommand.CountRedo);
        }

        // Test Undo Throw Exception
        [TestMethod()]
        [ExpectedException(typeof(Exception), "Cannot Undo exception\n")]
        public void TestRedoThrowException()
        {
            _commandManager.Redo();
        }

        // Test Execute With Undo
        [TestMethod()]
        public void TestExecuteWithUndo()
        {
            _commandManager.Execute(_mockCommand);
            _commandManager.Undo();
            _commandManager.Execute(_mockCommand);
            Assert.AreEqual(0, ((Stack<ICommand>)_commandManagerPrivate.GetField("_redo")).Count);
        }

        // Test IsUndoEnabled
        [TestMethod]
        public void TestIsUndoEnabled()
        {
            _commandManager.Execute(_mockCommand);
            Assert.AreEqual(true, _commandManager.IsUndoEnabled);
            _commandManager.Undo();
            Assert.AreEqual(false, _commandManager.IsUndoEnabled);
        }

        // Test IsRedoEnabled
        [TestMethod]
        public void TestIsRedoEnabled()
        {
            _commandManager.Execute(_mockCommand);
            Assert.AreEqual(false, _commandManager.IsRedoEnabled);
            _commandManager.Undo();
            Assert.AreEqual(true, _commandManager.IsRedoEnabled);
        }
    }
}
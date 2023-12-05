using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPoint
{
    class CommandManager
    {
        private const string UNDO_WARNING_MESSAGE = "Cannot Undo exception\n";
        private const string REDO_WARNING_MESSAGE = "Cannot Redo exception\n";
        Stack<ICommand> _undo = new Stack<ICommand>();
        Stack<ICommand> _redo = new Stack<ICommand>();

        // 執行傳入的 Command
        public void Execute(ICommand command)
        {
            command.Execute();
            // push command 進 undo stack
            _undo.Push(command);
            // 清除redo stack
            _redo.Clear();
        }

        // 解執行儲存的 Command
        public void Undo()
        {
            if (_undo.Count <= 0)
                throw new Exception(UNDO_WARNING_MESSAGE);
            ICommand command = _undo.Pop();
            _redo.Push(command);
            command.Undo();
        }

        // 執行被解執行的 Command
        public void Redo()
        {
            if (_redo.Count <= 0)
                throw new Exception(REDO_WARNING_MESSAGE);
            ICommand command = _redo.Pop();
            _undo.Push(command);
            command.Execute();
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _redo.Count != 0;
            }
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _undo.Count != 0;
            }
        }
    }
}

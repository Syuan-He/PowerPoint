using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPoint
{
    public interface ICommand
    {
        // Command 執行
        void Execute();

        // Command 解執行
        void Undo();

        // Command 回復執行
        void Redo();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPoint
{
    interface ICommand
    {
        // Command 執行
        void Execute();

        // Command 解執行
        void Undo();
    }
}

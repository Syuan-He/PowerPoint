using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    public class MockCommand : ICommand
    {
        public int CountExecute
        {
            get;
            set;
        }

        public int CountUndo
        {
            get;
            set;
        }

        public int CountRedo
        {
            get;
            set;
        }

        public MockCommand()
        {
            CountExecute = 0;
            CountUndo = 0;
        }

        // Command 執行
        public void Execute()
        {
            CountExecute++;
        }

        // Command 解執行
        public void Undo()
        {
            CountUndo++;
        }

        // Command 回復執行
        public void Redo()
        {
            CountRedo++;
        }
    }
}
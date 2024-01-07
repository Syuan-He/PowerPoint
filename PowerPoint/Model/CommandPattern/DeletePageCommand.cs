using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    class DeletePageCommand : ICommand
    {
        Model _model;
        int _index;
        Shapes _shapes;

        public DeletePageCommand(Model model, int pageIndex)
        {
            _model = model;
            _index = pageIndex;
        }

        // Command 執行
        public void Execute()
        {
            _shapes = _model.RemovePageAt(_index);
        }

        // Command 解執行
        public void Undo()
        {
            _model.InsertPage(_shapes, _index);
        }

        // Command 回復執行
        public void Redo()
        {
            _shapes = _model.RemovePageAt(_index);
        }
    }
}

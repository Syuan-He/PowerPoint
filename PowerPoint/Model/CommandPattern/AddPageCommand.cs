using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    class AddPageCommand : ICommand
    {
        Model _model;
        int _index;
        Shapes _shapes;

        public AddPageCommand(Model model, int pageIndex, Shapes shapes)
        {
            _model = model;
            _index = pageIndex;
            _shapes = shapes;
        }

        // Command 執行
        public void Execute()
        {
            _model.InsertPage(_shapes, _index);
        }

        // Command 解執行
        public void Undo()
        {
            _shapes = _model.RemovePageAt(_index);
        }

        // Command 回復執行
        public void Redo()
        {
            _model.InsertPage(_shapes, _index);
        }
    }
}

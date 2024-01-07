using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class DeleteCommand : ICommand
    {
        Model _model;
        int _index;
        int _pageIndex;
        Shape _shape;

        public DeleteCommand(Model model, int index, int pageIndex)
        {
            _model = model;
            _index = index;
            _pageIndex = pageIndex;
        }

        // Command 執行
        public void Execute()
        {
            _shape = _model.RemoveAt(_index, _pageIndex);
        }

        // Command 解執行
        public void Undo()
        {
            _model.Insert(_shape, _index, _pageIndex);
        }

        // Command 回復執行
        public void Redo()
        {
            _shape = _model.RemoveAt(_index, _pageIndex);
        }
    }
}

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
        Shape _shape;

        public DeleteCommand(Model model, int index)
        {
            _model = model;
            _index = index;
        }

        // Command 執行
        public void Execute()
        {
            _shape = _model.RemoveAt(_index);
        }

        // Command 解執行
        public void Undo()
        {
            _model.Insert(_shape, _index);
        }
    }
}

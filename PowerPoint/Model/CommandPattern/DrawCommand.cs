using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class DrawCommand : ICommand
    {
        Model _model;
        Shape _shape;
        int _pageIndex;

        public DrawCommand(Model model, Shape shape, int pageIndex)
        {
            _model = model;
            _shape = shape;
            _pageIndex = pageIndex;
        }

        // Command 執行
        public void Execute()
        {
            _model.CreateShapeCommand(_shape, _pageIndex);
        }

        // Command 解執行
        public void Undo()
        {
            _model.RemoveLast(_pageIndex);
        }

        // Command 回復執行
        public void Redo()
        {
            _model.CreateShapeCommand(_shape, _pageIndex);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    class DrawCommand : ICommand
    {
        Model _model;
        Shape _shape;

        public DrawCommand(Model model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        // Command 執行
        public void Execute()
        {
            _model.CreateShapeCommand(_shape);
        }

        // Command 解執行
        public void Undo()
        {
            _model.RemoveLast();
        }
    }
}

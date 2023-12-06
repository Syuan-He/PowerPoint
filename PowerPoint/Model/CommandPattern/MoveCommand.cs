using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class MoveCommand : ICommand
    {
        Model _model;
        int _offsetX;
        int _offsetY;

        public MoveCommand(Model model, int offsetX, int offsetY)
        {
            _model = model;
            _offsetX = offsetX;
            _offsetY = offsetY;
        }

        // Command 執行
        public void Execute()
        {
            _model.MoveShape(_offsetX, _offsetY);
        }

        // Command 解執行
        public void Undo()
        {
            _model.MoveShape(-_offsetX, -_offsetY);
        }
    }
}

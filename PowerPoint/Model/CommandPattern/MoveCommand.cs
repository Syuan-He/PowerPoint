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
        Coordinate _startPoint;
        Coordinate _endPoint;
        int _index;
        int _pageIndex;

        public MoveCommand(Model model, Coordinate indexes, Coordinate startPoint, Coordinate endPoint)
        {
            _model = model;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _index = indexes.X;
            _pageIndex = indexes.Y;
        }

        // Command 執行
        public void Execute()
        {
            _model.SetShapePosition(_index, _pageIndex, _endPoint);
        }

        // Command 解執行
        public void Undo()
        {
            _model.SetShapePosition(_index, _pageIndex, _startPoint);
        }

        // Command 回復執行
        public void Redo()
        {
            _model.SetShapePosition(_index, _pageIndex, _endPoint);
        }
    }
}

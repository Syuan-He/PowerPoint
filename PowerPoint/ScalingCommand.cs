using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class ScalingCommand : ICommand
    {
        Model _model;
        Coordinate _startPoint;
        Coordinate _endPoint;
        int _index;
        int _cornerIndex;

        public ScalingCommand(Model model, int index, Coordinate startPoint, Coordinate endPoint)
        {
            _model = model;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _index = index;
            _cornerIndex = _model.GetAtSelectedCorner(endPoint.X, endPoint.Y);
        }

        // Command 執行
        public void Execute()
        {
            _model.SetShapeEndPoint(_index, _endPoint, _cornerIndex);
        }

        // Command 解執行
        public void Undo()
        {
            _model.SetShapeEndPoint(_index, _startPoint, _cornerIndex);
            _cornerIndex = _model.GetAtSelectedCorner(_startPoint.X, _startPoint.Y);
        }

        // Command 回復執行
        public void Redo()
        {
            _model.SetShapeEndPoint(_index, _endPoint, _cornerIndex);
            _cornerIndex = _model.GetAtSelectedCorner(_endPoint.X, _endPoint.Y);
        }
    }
}

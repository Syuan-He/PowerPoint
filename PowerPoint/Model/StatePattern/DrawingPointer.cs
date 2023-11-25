using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class DrawingPointer : IState
    {
        IModel _model;
        int _firstPointX;
        int _firstPointY;
        Shape _hint;

        public DrawingPointer(IModel model)
        {
            _model = model;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1)
        {
            _firstPointX = x1;
            _firstPointY = y1;
            _hint = _model.CreateHint(x1, y1);
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_hint != null)
                _hint.SetEndPoint(new Coordinate(x2, y2));
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if (_hint != null)
                _model.CreateShape(_hint.GetShapeName(), new Coordinate(_firstPointX, _firstPointY), new Coordinate(x2, y2));
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            if (_hint != null)
                _hint.Draw(graphics);
        }
    }
}

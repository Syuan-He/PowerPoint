using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    class DrawingPointer : IState
    {
        Model _model;
        int _firstPointX;
        int _firstPointY;
        Shape _hint;
        bool _isPressed;

        public DrawingPointer(Model model)
        {
            _model = model;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(string shapeType, int x1, int y1)
        {
            if (shapeType != null)
            {
                if (x1 > 0 && y1 > 0)
                {
                    _firstPointX = x1;
                    _firstPointY = y1;
                    _hint = Factory.GenerateShape(shapeType, new Coordinate(x1, y1), new Coordinate(x1, y1));
                    _isPressed = true;
                    _model.NotifyModelChanged();
                }
            }
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_isPressed)
            {
                _hint.SetEndPoint(new Coordinate(x2, y2));
                _model.NotifyModelChanged();
            }
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            _isPressed = false;
            _model.CreateShape(_hint.GetShapeName(), new Coordinate(_firstPointX, _firstPointY), new Coordinate(x2, y2));
            _model.NotifyModelChanged();
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            if (_isPressed)
            {
                _hint.Draw(graphics);
            }
        }
    }
}

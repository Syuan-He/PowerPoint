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
        Model _model;
        Shape _hint;

        public DrawingPointer(Model model)
        {
            _model = model;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1, Shape shape)
        {
            _hint = shape;
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_hint != null)
                _hint.SetEndPoint(x2, y2);
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if (_hint != null)
            {
                _hint.SetEndPoint(x2, y2);
                _hint.AdjustPoint();
            }
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            if (_hint != null)
                _hint.Draw(graphics);
        }
    }
}

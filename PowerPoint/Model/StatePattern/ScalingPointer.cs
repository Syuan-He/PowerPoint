using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class ScalingPointer : IState
    {
        Shape _shape;

        public ScalingPointer(Shape shape)
        {
            _shape = shape;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1)
        {
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_shape != null)
                _shape.SetEndPoint(x2, y2);
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if (_shape != null)
                _shape.AdjustPoint();
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            if (_shape != null)
            {
                _shape.Draw(graphics);
                _shape.DrawSelectFrame(graphics);
            }
        }
    }
}

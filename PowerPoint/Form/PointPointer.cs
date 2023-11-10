using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class PointPointer : IState
    {
        Model _model;
        int _firstPointX;
        int _firstPointY;
        bool isSelect;

        public PointPointer(Model model)
        {
            _model = model;
        }
        // 按下滑鼠左鍵時
        public void PressPointer(string shapeType, int x1, int y1)
        {
            _firstPointX = x1;
            _firstPointY = y1;
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {

        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if(_firstPointX == x2 && _firstPointY == y2)
            {
                for (; ; )
                {

                }
            }
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {

        }
    }
}

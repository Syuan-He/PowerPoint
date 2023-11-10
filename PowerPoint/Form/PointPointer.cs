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

        public PointPointer(Model model)
        {
            model = _model;
        }
        // 按下滑鼠左鍵時
        public void PressPointer(string shapeType, int x1, int y1)
        {

        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {

        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {

        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {

        }
    }
}

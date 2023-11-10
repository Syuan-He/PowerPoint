using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    interface IState
    {
        // 按下滑鼠左鍵時
        void PressPointer(string shapeType, int x1, int y1);

        // 滑鼠移動
        void MovePointer(int x2, int y2);

        // 放掉滑鼠左鍵時
        void ReleasePointer(int x2, int y2);

        // 為鼠標繪製操作產生的圖形
        void Draw(IGraphics graphics);
    }
}

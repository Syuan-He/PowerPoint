using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }

        //清除畫面
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //畫直線
        public void DrawLine(Point point1, Point point2)
        {
            _graphics.DrawLine(Pens.Black, point1, point2);
        }

        //畫矩形
        public void DrawRectangle(int x1, int y1, int width, int height)
        {
            _graphics.DrawRectangle(Pens.Black, x1, y1, width, height);
        }

        //畫圓圈
        public void DrawEllipse(int x1, int y1, int width, int height)
        {
            _graphics.DrawEllipse(Pens.Black, x1, y1, width, height);
        }
    }
}
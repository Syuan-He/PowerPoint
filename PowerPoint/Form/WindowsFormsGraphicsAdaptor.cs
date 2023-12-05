using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
    {
        private const int HALF = 2;
        private const int DIAMETER = 10;
        private const int RADIUS = 5;
        private const int WIDTH = 1920;
        Graphics _graphics;
        float ratio;

        public WindowsFormsGraphicsAdaptor(Graphics graphics, int width)
        {
            this._graphics = graphics;
            ratio = (float)width / WIDTH;
        }

        //清除畫面
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //畫直線
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            _graphics.DrawLine(Pens.Black, (int)(x1 * ratio), (int)(y1 * ratio), (int)(x2 * ratio), (int)(y2 * ratio));
        }

        //畫矩形
        public void DrawRectangle(int x1, int y1, int width, int height)
        {
            _graphics.DrawRectangle(Pens.Black, x1 * ratio, y1 * ratio, width * ratio, height * ratio);
        }

        //畫圓圈
        public void DrawEllipse(int x1, int y1, int width, int height)
        {
            _graphics.DrawEllipse(Pens.Black, x1 * ratio, y1 * ratio, width * ratio, height * ratio);
        }

        // 繪製選取外框
        public void DrawSelectFrame(int x1, int y1, int x2, int y2)
        {
            float ratioX1 = x1 * ratio;
            float ratioY1 = y1 * ratio;
            float ratioX2 = x2 * ratio;
            float ratioY2 = y2 * ratio;
            _graphics.DrawRectangle(
                Pens.Black,
                Math.Min(ratioX1, ratioX2),
                Math.Min(ratioY1, ratioY2),
                Math.Abs(ratioX2 - ratioX1),
                Math.Abs(ratioY2 - ratioY1));

            _graphics.DrawEllipse(Pens.Black, ratioX1 - RADIUS, ratioY1 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, ratioX2 - RADIUS, ratioY1 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, ratioX1 - RADIUS, ratioY2 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, ratioX2 - RADIUS, ratioY2 - RADIUS, DIAMETER, DIAMETER);

            _graphics.DrawEllipse(Pens.Black, (ratioX1 + ratioX2) / HALF - RADIUS, ratioY1 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, (ratioX1 + ratioX2) / HALF - RADIUS, ratioY2 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, ratioX1 - RADIUS, (ratioY1 + ratioY2) / HALF - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, ratioX2 - RADIUS, (ratioY1 + ratioY2) / HALF - RADIUS, DIAMETER, DIAMETER);
        }
    }
}
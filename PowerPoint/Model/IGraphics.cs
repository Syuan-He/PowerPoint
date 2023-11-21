using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public interface IGraphics
    {
        //清理畫面
        void ClearAll();

        //畫線
        void DrawLine(Point point1, Point point2);

        //畫矩形
        void DrawRectangle(int x1, int y1, int width, int height);

        //畫圓圈
        void DrawEllipse(int x1, int y1, int width, int height);

        ////畫縮放的直線
        //void DrawResizingLine(Point point1, Point point2);

        ////畫縮放的矩形
        //void DrawResizingRectangle(int x1, int y1, int width, int height);

        ////畫縮放的圓圈
        //void DrawResizingEllipse(int x1, int y1, int width, int height);

        // 繪製選取外框
        void DrawSelectFrame(int x1, int y1, int x2, int y2);
    }
}

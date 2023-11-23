using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    class MockGraphics : IGraphics
    {
        public int _x1;
        public int _y1;
        public int _x2;
        public int _y2;
        public int _countDrawLine = 0;
        public int _countDrawRectangle = 0;
        public int _countDrawCircle = 0;
        public int _countDrawSelectFrame = 0;

        // 清理畫面
        public void ClearAll()
        {
        }

        // 畫圓圈
        public void DrawEllipse(int x1, int y1, int width, int height)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = width;
            _y2 = height;
            _countDrawCircle++;
        }

        // 畫線
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _countDrawLine++;
        }

        // 畫矩形
        public void DrawRectangle(int x1, int y1, int width, int height)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = width;
            _y2 = height;
            _countDrawRectangle++;
        }

        // 繪製選取外框
        public void DrawSelectFrame(int x1, int y1, int x2, int y2)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _countDrawSelectFrame++;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    class MockState : IState
    {
        public int _countPress;
        public int _countMove;
        public int _countRelease;
        public int _countDraw;
        public Coordinate _point;
        public IGraphics _graphics;

        public MockState()
        {
            _countDraw = 0;
            _countMove = 0;
            _countPress = 0;
            _countRelease = 0;
            _point = new Coordinate(0, 0);
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1)
        {
            _point.X = x1;
            _point.Y = y1;
            _countPress++;
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            _point.X = x2;
            _point.Y = y2;
            _countMove++;
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            _point.X = x2;
            _point.Y = y2;
            _countRelease++;
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            _graphics = graphics;
            _countDraw++;
        }
    }
}

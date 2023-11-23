using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    class MockShape : Shape
    {
        private const string INFO_FORMAT = "({0}, {1}), ({2}, {3})";
        private const string INFORMATION_PROPERTY = "Information";

        public int _countDraw;
        public int _countDrawSelectFrame;
        public int _countGetInfo;
        public int _countGetShapeName;
        public int _coountIsSelect;
        public int _countSetEndPoint;
        public int _countSetMove;

        public int _x1;
        public int _y1;
        public int _x2;
        public int _y2;

        public MockShape(Coordinate point1, Coordinate point2)
        {
            _countDraw = 0;
            _countDrawSelectFrame = 0;
            _countGetInfo = 0;
            _countGetShapeName = 0;
            _coountIsSelect = 0;
            _countSetEndPoint = 0;
            _countSetMove = 0;
            _x1 = point1.X;
            _y1 = point1.Y;
            _x2 = point2.X;
            _y2 = point2.Y;
            ShapeName = GetShapeName();
            Information = GetInfo();
        }

        // Draw
        public override void Draw(IGraphics graphics)
        {
            _countDraw++;
        }

        // DrawSelectFrame
        public override void DrawSelectFrame(IGraphics graphics)
        {
            _countDrawSelectFrame++;
        }

        // GetInfo
        public override string GetInfo()
        {
            _countGetInfo++;
            return string.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
        }

        // GetShapeName
        public override string GetShapeName()
        {
            _countGetShapeName++;
            return "testName";
        }

        // IsSelect
        public override bool IsSelect(int x1, int y1)
        {
            _coountIsSelect++;
            if (IsInnerInX(x1) && IsInnerInY(y1))
            {
                return true;
            }
            return false;
        }

        // 檢查是否X軸在範圍內
        bool IsInnerInX(int x1)
        {
            return Math.Max(_x1, _x2) >= x1 && Math.Min(_x1, _x2) <= x1;
        }

        // 檢查是否Y軸在範圍內
        bool IsInnerInY(int y1)
        {
            return Math.Max(_y1, _y2) >= y1 && Math.Min(_y1, _y2) <= y1;
        }

        // SetEndPoint
        public override void SetEndPoint(Coordinate endPoint)
        {
            _countSetEndPoint++;
            _x2 = endPoint.X;
            _y2 = endPoint.Y;
            Information = GetInfo();
            NotifyPropertyChanged(INFORMATION_PROPERTY);
        }

        // SetMove
        public override void SetMove(int offsetX, int offsetY)
        {
            _x1 += offsetX;
            _x2 += offsetX;
            _y1 += offsetY;
            _y2 += offsetY;
            _countSetMove++;
            Information = GetInfo();
            NotifyPropertyChanged(INFORMATION_PROPERTY);
        }
    }
}

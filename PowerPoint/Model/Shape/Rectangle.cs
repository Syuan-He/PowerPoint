using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class Rectangle : Shape
    {
        private const string INFO_FORMAT = "({0}, {1}), ({2}, {3})";
        private const string INFORMATION_PROPERTY = "Information";

        int _x1;
        int _y1;
        int _x2;
        int _y2;

        public Rectangle(Coordinate point1, Coordinate point2)
        {
            _x1 = point1.X;
            _y1 = point1.Y;
            _x2 = point2.X;
            _y2 = point2.Y;
            AdjustPoint();
            ShapeName = ShapeType.RECTANGLE;
        }

        //設定圖形終點
        public override void SetEndPoint(int x2, int y2)
        {
            _x2 = x2;
            _y2 = y2;
            Information = String.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
        }

        // 移動圖形
        public override void SetMove(int offsetX, int offsetY)
        {
            _x1 += offsetX;
            _x2 += offsetX;
            _y1 += offsetY;
            _y2 += offsetY;
            Information = String.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
        }

        // 檢查是否被選取
        public override bool IsSelect(int x1, int y1)
        {
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

        // 確認在哪個頂點上
        public override int GetAtCorner(int x1, int y1)
        {
            if (IsCornerInX(x1) && IsCornerInY(y1))
            {
                return ShapeInteger.TOTAL_CORNER;
            }
            return ShapeInteger.NOT_IN_LIST;
        }

        // 檢查X軸是否在角落的範圍內
        bool IsCornerInX(int x1)
        {
            return Math.Max(_x1, _x2) + ShapeInteger.RADIUS > x1 &&
                Math.Max(_x1, _x2) - ShapeInteger.RADIUS < x1;
        }

        // 檢查Y軸是否在角落的範圍內
        bool IsCornerInY(int y1)
        {
            return Math.Max(_y1, _y2) + ShapeInteger.RADIUS > y1 &&
                Math.Max(_y1, _y2) - ShapeInteger.RADIUS < y1;
        }

        //調整傳入的 point 的座標，使第一個 point 的座標在左上，第二個在右下
        public override void AdjustPoint()
        {
            int temp;
            if (_x1 > _x2)
            {
                temp = _x1;
                _x1 = _x2;
                _x2 = temp;
            }
            if (_y1 > _y2)
            {
                temp = _y1;
                _y1 = _y2;
                _y2 = temp;
            }
            Information = String.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
            NotifyPropertyChanged(INFORMATION_PROPERTY);
        }

        //繪製該圖形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(
                Math.Min(_x1, _x2),
                Math.Min(_y1, _y2), 
                Math.Abs(_x2 - _x1),
                Math.Abs(_y2 - _y1));
        }

        // 繪製選取外框
        public override void DrawSelectFrame(IGraphics graphics)
        {
            graphics.DrawSelectFrame(_x1, _y1, _x2, _y2);
        }
    }
}

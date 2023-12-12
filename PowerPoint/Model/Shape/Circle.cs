using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class Circle : Shape
    {
        private const string INFO_FORMAT = "({0}, {1}), ({2}, {3})";
        private const string INFORMATION_PROPERTY = "Information";

        int _x1;
        int _y1;
        int _x2;
        int _y2;

        public Circle(Coordinate point1, Coordinate point2)
        {
            _x1 = point1.X;
            _y1 = point1.Y;
            _x2 = point2.X;
            _y2 = point2.Y;
            AdjustPoint();
            ShapeName = ShapeType.CIRCLE;
        }

        // 設定圖形終點
        public override void SetEndPoint(int x2, int y2)
        {
            _x2 = x2;
            _y2 = y2;
            Information = String.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
        }

        // 依 index 設定圖形的點
        public override void SetPoint(int x1, int y1, int index)
        {
            int coordinateX = index % ShapeInteger.SPLIT_PART;
            if (coordinateX == ShapeInteger.X1)
                _x1 = x1;
            else if (coordinateX == ShapeInteger.X2)
                _x2 = x1;
            int coordinateY = index / ShapeInteger.SPLIT_PART;
            if (coordinateY == ShapeInteger.X1)
                _y1 = y1;
            else if (coordinateY == ShapeInteger.X2)
                _y2 = y1;

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

        // 設定位子(以左上角為準)
        public override void SetPosition(Coordinate point1)
        {
            _x2 += point1.X - _x1;
            _y2 += point1.Y - _y1;
            _x1 = point1.X;
            _y1 = point1.Y;
            Information = String.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
            NotifyPropertyChanged(INFORMATION_PROPERTY);
        }

        // 取得第一個點的座標
        public override Coordinate GetPoint1()
        {
            return new Coordinate(_x1, _y1);
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
            int nearX = NearCoordinate(x1, _x1, _x2);
            int nearY = NearCoordinate(y1, _y1, _y2);
            if (nearX >= 0 && nearY >= 0)
                return nearY * ShapeInteger.SPLIT_PART + nearX;
            return ShapeInteger.NOT_IN_LIST;
        }

        // 看靠近 X 軸或 Y 軸上的哪個點
        int NearCoordinate(int value, int x1, int x2)
        {
            int centerX = (x1 + x2) / ShapeInteger.HALF;
            
            if (centerX - value > 0)
            {
                return NearCoordinate1(value, x1, centerX);
            }
            else
            {
                return NearCoordinate2(value, x2, centerX);
            }
        }

        // 看是否靠近 X 或 Y 軸上的 x1
        int NearCoordinate1(int value, int x1, int centerX)
        {
            int index = 0;
            if (Math.Abs(x1 - value) < centerX - value)
            {
                if (Math.Abs(x1 - value) <= ShapeInteger.RADIUS)
                    return index;
            }
            else
            {
                index++;
                if (centerX - value <= ShapeInteger.RADIUS)
                    return index;
            }
            return ShapeInteger.NOT_IN_LIST;
        }

        // 看是否靠近 X 或 Y 軸上的 x2
        int NearCoordinate2(int value, int x2, int centerX)
        {
            int index = 1;
            if (Math.Abs(x2 - value) > value - centerX)
            {
                if (value - centerX <= ShapeInteger.RADIUS)
                    return index;
            }
            else
            {
                index++;
                if (Math.Abs(x2 - value) <= ShapeInteger.RADIUS)
                    return index;
            }
            return ShapeInteger.NOT_IN_LIST;
        }

        // 調整傳入的 point 的座標，使第一個 point 的座標在左上，第二個在右下
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

        // 繪製該圖形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(
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

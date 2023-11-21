using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    class Circle : Shape
    {
        private const string INFO_FORMAT = "({0}, {1}), ({2}, {3})";
        private const int HALF = 2;
        private const string INFORMATION_PROPERTY = "Information";
        int _x1;
        int _y1;
        int _x2;
        int _y2;

        public Circle(Point point1, Point point2)
        {
            _x1 = point1.X;
            _y1 = point1.Y;
            _x2 = point2.X;
            _y2 = point2.Y;
            AdjustPoint();
            ShapeName = GetShapeName();
            Information = GetInfo();
        }

        //取得圖形物件的型態資料
        public override string GetShapeName()
        {
            return ShapeType.CIRCLE;
        }

        //取得物件的頂點座標
        public override string GetInfo()
        {
            return string.Format(INFO_FORMAT, _x1, _y1, _x2, _y2);
        }

        //設定圖形終點
        public override void SetEndPoint(Point point2)
        {
            _x2 = point2.X;
            _y2 = point2.Y;
        }

        // 移動圖形
        public override void SetMove(int offsetX, int offsetY)
        {
            _x1 += offsetX;
            _x2 += offsetX;
            _y1 += offsetY;
            _y2 += offsetY;
            Information = GetInfo();
            NotifyPropertyChanged(INFORMATION_PROPERTY);
        }

        // 檢查是否被選取
        public override bool IsSelect(int x1, int y1)
        {
            if (
                Math.Abs((_x1 + _x2) / HALF - x1) < Math.Abs(_x1 - _x2) / HALF &&
                Math.Abs((_y1 + _y2) / HALF - y1) < Math.Abs(_y1 - _y2) / HALF)
            {
                return true;
            }
            return false;
        }

        //調整傳入的 point 的座標，使第一個 point 的座標在左上，第二個在右下
        private void AdjustPoint()
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
        }

        //繪製該圖形
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawEllipse(
                Math.Min(_x1, _x2),
                Math.Min(_y1, _y2),
                Math.Abs(_x2 - _x1),
                Math.Abs(_y2 - _y1));
        }

        // 繪製該圖形的縮圖
        public override void DrawSlide(IGraphics graphics, Size panelSize, Size slideSize)
        {
            graphics.DrawEllipse(
                Math.Min(_x1, _x2) * slideSize.Width / panelSize.Width,
                Math.Min(_y1, _y2) * slideSize.Height / panelSize.Height,
                Math.Abs(_x2 - _x1) * slideSize.Width / panelSize.Width,
                Math.Abs(_y2 - _y1) * slideSize.Height / panelSize.Height);
        }

        // 繪製選取外框
        public override void DrawSelectFrame(IGraphics graphics)
        {
            graphics.DrawSelectFrame(_x1, _y1, _x2, _y2);
        }
    }
}

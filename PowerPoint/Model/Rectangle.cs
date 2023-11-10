using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    class Rectangle : Shape
    {
        private const string INFO_FORMAT = "({0}, {1}), ({2}, {3})";

        int _x1;
        int _y1;
        int _x2;
        int _y2;

        public Rectangle(Point point1, Point point2)
        {
            _x1 = point1.X;
            _y1 = point1.Y;
            _x2 = point2.X;
            _y2 = point2.Y;
            AdjustPoint();
            _shapeType = GetShapeName();
            _info = GetInfo();
        }

        //取得圖形物件的型態資料
        public override string GetShapeName()
        {
            return ShapeType.RECTANGLE;
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
            graphics.DrawRectangle(
                Math.Min(_x1, _x2),
                Math.Min(_y1, _y2), 
                Math.Abs(_x2 - _x1),
                Math.Abs(_y2 - _y1));
        }
    }
}

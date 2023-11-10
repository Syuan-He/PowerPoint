using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace PowerPoint
{
    class Shapes
    {
        private const string DELETE_STRING = "刪除";
        BindingList<Shape> _shapes = new BindingList<Shape>();

        public BindingList<Shape> ShapeList
        {
            get
            {
                return _shapes;
            }
        }

        // 為 list 創建新的 shape
        public void CreateShape(string shapeType, Point point1, Point point2)
        {
            Shape shape = Factory.GenerateShape(shapeType, point1, point2);
            _shapes.Add(shape);
        }

        // 用多載為 list 創建隨機位子的新 shape
        public void CreateShape(string shapeType, int width, int height)
        {
            Shape shape = Factory.GenerateShape(shapeType, width, height);
            _shapes.Add(shape);
        }

        // 從 list 移除要刪除的物件
        public void Remove(int index)
        {
            if (index >= 0 && index < _shapes.Count())
                _shapes.RemoveAt(index);
        }

        // 繪製所有存在 list 的圖形
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aShape in _shapes)
                aShape.Draw(graphics);
        }
    }
}

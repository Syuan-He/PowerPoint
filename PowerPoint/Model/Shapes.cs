using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace PowerPoint
{
    public class Shapes
    {
        BindingList<Shape> _shapes;
        Factory _factory;

        public Shapes()
        {
            _shapes = new BindingList<Shape>();
            _factory = new Factory(new Random(Guid.NewGuid().GetHashCode()));
        }

        public BindingList<Shape> ShapeList
        {
            get
            {
                return _shapes;
            }
        }

        // 為 list 創建新的 shape
        public void CreateShape(string shapeType, Coordinate point1, Coordinate point2)
        {
            Shape shape = Factory.GenerateShape(shapeType, point1, point2);
            _shapes.Add(shape);
        }

        // 用多載為 list 創建隨機位子的新 shape
        public void CreateShape(string shapeType, int width, int height)
        {
            Shape shape = _factory.GenerateShape(shapeType, width, height);
            _shapes.Add(shape);
        }

        // 從 list 移除要刪除的物件
        public void Remove(int index)
        {
            if (index >= 0 && index < _shapes.Count())
                _shapes.RemoveAt(index);
        }

        // 尋找被選取的 shape
        public int FindSelectItem(int x1, int y1)
        {
            int index = _shapes.Count - 1;
            for (; index >= 0; index--)
            {
                if (_shapes[index].IsSelect(x1, y1))
                    return index;
            }
            return --index;
        }

        // 移動選取的圖形
        public void MoveSelectedShape(int index, int x1, int y1)
        {
            if (index >= 0 && index < _shapes.Count())
                _shapes[index].SetMove(x1, y1);
        }

        // 繪製所有存在 list 的圖形
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aShape in _shapes)
                aShape.Draw(graphics);
        }

        // 繪製選取外框
        public void DrawSelectFrame(IGraphics graphics, int index)
        {
            if (index >= 0 && index < _shapes.Count())
                _shapes[index].DrawSelectFrame(graphics);
        }
    }
}

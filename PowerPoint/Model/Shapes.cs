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
        BindingList<Shape> _shapeList;
        IFactory _factory;

        public Shapes(IFactory factory)
        {
            _shapeList = new BindingList<Shape>();
            _factory = factory;
        }

        public BindingList<Shape> ShapeList
        {
            get
            {
                return _shapeList;
            }
        }

        // 為 list 創建新的 shape
        public void CreateShape(string shapeType, Coordinate point1, Coordinate point2)
        {
            Shape shape = _factory.GenerateShape(shapeType, point1, point2);
            CreateShape(shape);
        }

        // 用多載為 list 創建隨機位子的新 shape
        public void CreateShape(string shapeType)
        {
            Shape shape = _factory.GenerateShape(shapeType);
            CreateShape(shape);
        }

        // 用多載讓 list 能直接加 shape
        public void CreateShape(Shape shape)
        {
            if (shape != null)
                _shapeList.Add(shape);
        }

        // 從 list 移除要刪除的物件
        public Shape Remove(int index)
        {
            Shape shape = null;
            if (index >= 0 && index < _shapeList.Count())
            {
                shape = _shapeList[index];
                _shapeList.RemoveAt(index);
            }
            return shape;
        }

        // 從 list 移除最後一個物件
        public void RemoveLast()
        {
            int last = _shapeList.Count;
            if (last > 0)
            {
                _shapeList.RemoveAt(last - 1);
            }
        }

        // 在 list 指定的位子插入 shape
        public void Insert(Shape shape, int index)
        {
            if (index >= 0 && index <= _shapeList.Count())
                _shapeList.Insert(index, shape);
        }

        // 尋找被點選的 shape
        public int FindSelectItem(int x1, int y1)
        {
            int index = _shapeList.Count - 1;
            for (; index >= 0; index--)
            {
                if (_shapeList[index].IsSelect(x1, y1))
                    return index;
            }
            return index;
        }

        // 移動選取的圖形
        public void MoveSelectedShape(int index, int x1, int y1)
        {
            if (index >= 0 && index < _shapeList.Count())
                _shapeList[index].SetMove(x1, y1);
        }

        // 設定選取 shape 的位子
        public void SetSelectedShapePosition(int index, Coordinate point1)
        {
            if (index >= 0 && index < _shapeList.Count())
                _shapeList[index].SetPosition(point1);
        }

        // 設定選取 shape 的指定 Corner
        public void SetSelectedShapePoint(int index, Coordinate point1, int cornerIndex)
        {
            if (index >= 0 && index < _shapeList.Count())
            {
                _shapeList[index].SetPoint(point1.X, point1.Y, cornerIndex);
                _shapeList[index].AdjustPoint();
            }
        }

        // 確認在哪個頂點上
        public int GetAtSelectedCorner(int index, int x1, int y1)
        {
            if (index >= 0 && index < _shapeList.Count)
                return _shapeList[index].GetAtCorner(x1, y1);
            return ShapeInteger.NOT_IN_LIST;
        }

        // 取得被選取的 shape
        public Shape GetShape(int index)
        {
            if (index >= 0 && index < _shapeList.Count)
                return _shapeList[index];
            return null;
        }

        // 繪製所有存在 list 的圖形
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aShape in _shapeList)
                aShape.Draw(graphics);
        }

        // 繪製選取外框
        public void DrawSelectFrame(IGraphics graphics, int index)
        {
            if (index >= 0 && index < _shapeList.Count())
                _shapeList[index].DrawSelectFrame(graphics);
        }
    }
}

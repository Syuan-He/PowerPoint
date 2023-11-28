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
        public void CreateShape(string shapeType, int width, int height)
        {
            Shape shape = _factory.GenerateShape(shapeType, width, height);
            CreateShape(shape);
        }

        // 用多載讓 list 能直接加 shape
        public void CreateShape(Shape shape)
        {
            if (shape != null)
                _shapeList.Add(shape);
        }

        // 從 list 移除要刪除的物件
        public void Remove(int index)
        {
            if (index >= 0 && index < _shapeList.Count())
                _shapeList.RemoveAt(index);
        }

        // 尋找被選取的 shape
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

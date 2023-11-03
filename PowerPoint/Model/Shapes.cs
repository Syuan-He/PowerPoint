using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    class Shapes
    {
        private const string DELETE_STRING = "刪除";
        private List<Shape> _shapes = new List<Shape>();

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
            _shapes.RemoveAt(index);
        }

        // 取得 list 的 GridView
        public List<InfoGridView> GetListGridView()
        {
            List<InfoGridView> gridViewList = _shapes.Select(item => new InfoGridView
            { 
                _delete = DELETE_STRING,
                _shape = item.GetShapeName(),
                _information = item.GetInfo() }).ToList();

            return gridViewList;
        }

        // 繪製所有存在 list 的圖形
        public void Draw(IGraphics graphics)
        {
            foreach (Shape aShape in _shapes)
                aShape.Draw(graphics);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class Model
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        public event ListChangedEventHandler _shapesChanged;
        public delegate void ListChangedEventHandler();

        const int PANEL_WIDTH = 710;
        const int PANEL_HEIGHT = 568;
        
        Shapes _shapes = new Shapes();
        bool _isPressed = false;
        Shape _hint;
        int _firstPointX;
        int _firstPointY;

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType)
        {
            if (shapeType != ShapeType.LINE && shapeType != ShapeType.RECTANGLE && shapeType != ShapeType.CIRCLE)
            {
                return;
            }
            _shapes.CreateShape(shapeType, PANEL_WIDTH, PANEL_HEIGHT);
            NotifyListChanged();
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 && rowIndex >= 0)
            {
                _shapes.Remove(rowIndex);
                NotifyListChanged();
            }
        }

        // 按下滑鼠左鍵
        public void PressPointer(string shapeType, int x1, int y1)
        {
            if (x1 > 0 && y1 > 0)
            {
                _firstPointX = x1;
                _firstPointY = y1;
                _hint = Factory.GenerateShape(shapeType, new Point(x1, y1), new Point(x1, y1));
                _isPressed = true;
            }
        }

        // 滑鼠移動時
        public void MovePointer(int x2, int y2)
        {
            if (_isPressed)
            {
                _hint.SetEndPoint(new Point(x2, y2));
                NotifyModelChanged();
            }
        }

        // 放開滑鼠左鍵
        public void ReleasePointer(int x2, int y2)
        {
            if (_isPressed)
            {
                _isPressed = false;
                //if (x2 > PANEL_WIDTH)
                //    x2 = PANEL_WIDTH;
                //else if (x2 < 0)
                //    x2 = 0;
                //else if (y2 > PANEL_HEIGHT)
                //    y2 = PANEL_HEIGHT;
                //else if (y2 < 0)
                //    y2 = 0;
                _shapes.CreateShape(_hint.GetShapeName(), new Point(_firstPointX, _firstPointY), new Point(x2, y2));
                NotifyListChanged();
            }
        }

        // 繪製圖形
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            _shapes.Draw(graphics);
            if (_isPressed)
                _hint.Draw(graphics);
        }

        // 通知 model 要重新繪製 panel
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        // 通知 model 要重新繪製 DataGridView
        void NotifyListChanged()
        {
            if (_shapesChanged != null)
                _shapesChanged();
        }

        //回傳 Shapes 裡 list 的資訊，給資訊顯示的 _infoDataGridView 用
        public List<InfoGridView> GetInfoDataGridView()
        {
            return _shapes.GetListGridView();
        }
    }
}

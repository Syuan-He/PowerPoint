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

        const int PANEL_WIDTH = 710;
        const int PANEL_HEIGHT = 568;

        private const int NOT_IN_LIST = -1;
        private const int SLIDE_WIDTH = 144;
        private const int SLIDE_HEIGHT = 108;
        Shapes _shapes = new Shapes();
        int _selectedIndex = NOT_IN_LIST;
        IState _pointer;

        public Model()
        {
            _pointer = new PointPointer(this);
        }

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType)
        {
            if (shapeType != ShapeType.LINE && shapeType != ShapeType.RECTANGLE && shapeType != ShapeType.CIRCLE)
            {
                return;
            }
            _shapes.CreateShape(shapeType, PANEL_WIDTH, PANEL_HEIGHT);
            NotifyModelChanged();
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 && rowIndex >= 0)
            {
                _shapes.Remove(rowIndex);
                if (_selectedIndex == rowIndex)
                    _selectedIndex = NOT_IN_LIST;
                NotifyModelChanged();
            }
        }

        // 按下鍵盤 delete 鍵
        public void PressDeleteKey()
        {
            _shapes.Remove(_selectedIndex);
            _selectedIndex = NOT_IN_LIST;
            NotifyModelChanged();
        }

        // pointer 進入 point 模式
        public void SetPoint()
        {
            _pointer = new PointPointer(this);
        }

        // pointer 進入 drawing 模式
        public void SetDrawing()
        {
            _pointer = new DrawingPointer(this);
            _selectedIndex = NOT_IN_LIST;
            NotifyModelChanged();
        }

        // 按下滑鼠左鍵
        public void PressPointer(string shapeType, int x1, int y1)
        {
            _pointer.PressPointer(shapeType, x1, y1);
        }

        // 滑鼠移動時
        public void MovePointer(int x2, int y2)
        {
            _pointer.MovePointer(x2, y2);
        }

        // 放開滑鼠左鍵
        public void ReleasePointer(int x2, int y2)
        {
            _pointer.ReleasePointer(x2, y2);
        }

        // DrawingPointer(state pattern) 創建新圖形要用的 function
        public void CreateShape(string shapeType, Point point1, Point point2)
        {
            _shapes.CreateShape(shapeType, point1, point2);
        }

        // PointPointer 搜尋重疊要用的 function
        public bool FindSelectShape(int x1, int y1)
        {
            _selectedIndex = _shapes.FindSelectItem(x1, y1);
            NotifyModelChanged();
            return _selectedIndex != NOT_IN_LIST;
        }

        // 移動選取的圖形
        public void MoveShape(int x1, int y1)
        {
            _shapes.MoveSelectedShape(_selectedIndex, x1, y1);
            NotifyModelChanged();
        }

        // 繪製選取外框
        public void DrawSelectFrame(IGraphics graphics)
        {
            _shapes.DrawSelectFrame(graphics, _selectedIndex);
        }
        
        // 繪製圖形
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            _shapes.Draw(graphics);
            _pointer.Draw(graphics);
        }

        // 繪製圖縮
        public void DrawSlide(IGraphics graphics)
        {
            graphics.ClearAll();
            _shapes.DrawSlide(graphics, new Size(PANEL_WIDTH, PANEL_HEIGHT), new Size(SLIDE_WIDTH, SLIDE_HEIGHT));
        }

        // 通知 model 要重新繪製 panel
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        //回傳 Shapes 裡 list 的資訊，給資訊顯示的 _infoDataGridView 用
        public System.ComponentModel.BindingList<Shape> GetInfoDataGridView()
        {
            return _shapes.ShapeList;
        }
    }
}

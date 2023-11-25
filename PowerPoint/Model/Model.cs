using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class Model : IModel
    {
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();

        const int PANEL_WIDTH = 710;
        const int PANEL_HEIGHT = 568;
        private const int NOT_IN_LIST = -1;

        Shapes _shapes;
        string _shapeType;

        IState _pointer;
        IFactory _factory;

        bool _isPressed;
        int _selectedIndex;

        public Model(IFactory factory)
        {
            _factory = factory;
            _shapes = new Shapes(factory);
            _selectedIndex = NOT_IN_LIST;
            _isPressed = false;
            _pointer = new PointPointer(this);
        }

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType)
        {
            _shapes.CreateShape(shapeType, PANEL_WIDTH, PANEL_HEIGHT);
            NotifyModelChanged();
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 && rowIndex >= 0)
            {
                _shapes.Remove(rowIndex);
                _selectedIndex = NOT_IN_LIST;
                NotifyModelChanged();
            }
        }

        // 按下鍵盤 delete 鍵
        public void PressDeleteKey()
        {
            if (_selectedIndex > NOT_IN_LIST)
            {
                _shapes.Remove(_selectedIndex);
                _selectedIndex = NOT_IN_LIST;
                NotifyModelChanged();
            }
        }

        // pointer 進入 point 模式
        public void SetPoint()
        {
            _pointer = new PointPointer(this);
            NotifyModelChanged();
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
            _isPressed = true;
            _shapeType = shapeType;
            _pointer.PressPointer(x1, y1);
        }

        // 滑鼠移動時
        public void MovePointer(int x2, int y2)
        {
            if (_isPressed)
            {
                _pointer.MovePointer(x2, y2);
                NotifyModelChanged();
            }
        }

        // 放開滑鼠左鍵
        public void ReleasePointer(int x2, int y2)
        {
            _isPressed = false;
            _pointer.ReleasePointer(x2, y2);
        }

        // 為 DrawingPointer 創建 hint
        public Shape CreateHint(int x1, int y1)
        {
            return _factory.GenerateShape(_shapeType, new Coordinate(x1, y1), new Coordinate(x1, y1));
        }

        // DrawingPointer(state pattern) 創建新圖形要用的 function
        public void CreateShape(string shapeType, Coordinate point1, Coordinate point2)
        {
            _shapes.CreateShape(shapeType, point1, point2);
            NotifyModelChanged();
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
        }

        // 繪製選取外框
        public void DrawSelectFrame(IGraphics graphics)
        {
            _shapes.DrawSelectFrame(graphics, _selectedIndex);
        }

        // 繪製圖形
        public void Draw(IGraphics graphics, bool isPanel)
        {
            graphics.ClearAll();
            _shapes.Draw(graphics);
            if (_isPressed && isPanel)
            {
                _pointer.Draw(graphics);
            }
        }

        // 通知 model 要重新繪製 panel
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        //回傳 Shapes 的 BindingList ，給資訊顯示的 _infoDataGridView 用
        public System.ComponentModel.BindingList<Shape> GetInfoDataGridView()
        {
            return _shapes.ShapeList;
        }
    }
}

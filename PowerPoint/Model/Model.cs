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
        public event ModelChangedEventHandler _panelChanged;
        public delegate void ModelChangedEventHandler();

        private const int NOT_IN_LIST = -1;

        Shapes _shapes;
        string _shapeType;

        IState _pointer;
        IFactory _factory;

        bool _isPressed;
        int _selectedIndex;
        Shape _shape;

        public Model(IFactory factory)
        {
            _factory = factory;
            _shapes = new Shapes(factory);
            _selectedIndex = NOT_IN_LIST;
            _isPressed = false;
            _pointer = new PointPointer(null);
        }

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType, int panelWidth, int panelHeight)
        {
            _shapes.CreateShape(shapeType, panelWidth, panelHeight);
            NotifyPanelChanged();
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 && rowIndex >= 0)
            {
                _shapes.Remove(rowIndex);
                _selectedIndex = NOT_IN_LIST;
                SetPoint();
                NotifyPanelChanged();
            }
        }

        // 按下鍵盤 delete 鍵
        public void PressDeleteKey()
        {
            if (_selectedIndex > NOT_IN_LIST)
            {
                _shapes.Remove(_selectedIndex);
                _selectedIndex = NOT_IN_LIST;
                SetPoint();
                NotifyPanelChanged();
            }
        }

        // pointer 進入 point 模式 (有選取的 shape 就傳進 pointer，GetShape會自動判斷)
        public void SetPoint()
        {
            _pointer = new PointPointer(_shapes.GetShape(_selectedIndex));
            NotifyPanelChanged();
        }

        // pointer 進入 drawing 模式
        public void SetDrawing()
        {
            _pointer = new DrawingPointer();
            _selectedIndex = NOT_IN_LIST;
            NotifyPanelChanged();
        }

        // pointer 進入 scaling 模式 (有選取的 shape 就傳進 pointer，GetShape會自動判斷)
        public void SetScaling()
        {
            _pointer = new ScalingPointer(_shapes.GetShape(_selectedIndex));
        }

        // 是否有被選取的 shape
        public bool IsHasSelected()
        {
            return _selectedIndex > NOT_IN_LIST;
        }

        // 判斷是不是位在頂點上
        public int GetAtSelectedCorner(int x1, int y1)
        {
            return _shapes.GetAtSelectedCorner(_selectedIndex, x1, y1);
        }

        // 按下滑鼠左鍵
        public void PressPointer(string shapeType, int x1, int y1)
        {
            _isPressed = true;
            _shapeType = shapeType;

            if (shapeType == null)
            {
                if (GetAtSelectedCorner(x1, y1) < 0)
                {
                    _selectedIndex = _shapes.FindSelectItem(x1, y1);
                    _shape = GetSelectShape(_selectedIndex);
                }
                else
                    SetScaling();
            }
            else
                _shape = _factory.GenerateShape(_shapeType, new Coordinate(x1, y1), new Coordinate(x1, y1));
            _pointer.PressPointer(x1, y1, _shape);
        }

        // 滑鼠移動時
        public void MovePointer(int x2, int y2)
        {
            if (_isPressed)
            {
                _pointer.MovePointer(x2, y2);
                NotifyPanelChanged();
            }
        }

        // 放開滑鼠左鍵
        public void ReleasePointer(int x2, int y2)
        {
            _isPressed = false;
            _pointer.ReleasePointer(x2, y2);
            if (_shapeType != null)
            {
                _shapes.CreateShape(_shape);
                _shape = null;
                NotifyPanelChanged();
            }
            _pointer = new PointPointer(_shapes.GetShape(_selectedIndex));
        }

        // 回傳指定位子的 shape
        Shape GetSelectShape(int index)
        {
            return _shapes.GetShape(index);
        }

        // 繪製圖形
        public void Draw(IGraphics graphics, bool isPanel)
        {
            graphics.ClearAll();
            _shapes.Draw(graphics);
            if (isPanel)
            {
                _pointer.Draw(graphics);
            }
        }

        // 通知 model 要重新繪製 panel
        void NotifyPanelChanged()
        {
            if (_panelChanged != null)
                _panelChanged();
        }

        //回傳 Shapes 的 BindingList ，給資訊顯示的 _infoDataGridView 用
        public System.ComponentModel.BindingList<Shape> GetInfoDataGridView()
        {
            return _shapes.ShapeList;
        }
    }
}

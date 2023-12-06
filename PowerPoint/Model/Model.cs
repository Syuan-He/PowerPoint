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
        IFactory _factory;
        IState _pointer;
        CommandManager _commandManager;

        string _shapeType;

        bool _isPressed;
        int _selectedIndex;

        public Model(IFactory factory)
        {
            _factory = factory;
            _shapes = new Shapes(factory);
            _selectedIndex = NOT_IN_LIST;
            _isPressed = false;
            _pointer = new PointPointer(this, null);
            _commandManager = new CommandManager();
        }

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType)
        {
            _commandManager.Execute(new AddCommand(this, _factory.GenerateShape(shapeType)));
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0)
            {
                _commandManager.Execute(new DeleteCommand(this, rowIndex));
            }
        }

        // 按下 Undo 按鍵
        public void PressUndo()
        {
            _commandManager.Undo();
        }

        // 按下 Redo 按鍵
        public void PressRedo()
        {
            _commandManager.Redo();
        }

        // 按下鍵盤 delete 鍵
        public void PressDeleteKey()
        {
            _commandManager.Execute(new DeleteCommand(this, _selectedIndex));
        }

        // pointer 進入 point 模式
        public void SetPoint()
        {
            _pointer = new PointPointer(this, GetSelectedDuplicate());
            NotifyPanelChanged();
        }

        // pointer 進入 drawing 模式
        public void SetDrawing()
        {
            _pointer = new DrawingPointer(this);
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
            _pointer.PressPointer(x1, y1);
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

        // PointPointer 取得指定位子的 shape
        public Shape GetSelectedDuplicate()
        {
            return _shapes.GetSelectDuplicate(_selectedIndex);
        }

        // PointPointer 找被點選的 shape
        public void FindSelected(int x1, int y1)
        {
            _selectedIndex = _shapes.FindSelectItem(x1, y1);
        }

        // PointPointer 移動圖形
        public void MoveSelected(int offsetX, int offsetY)
        {
            _commandManager.Execute(new MoveCommand(this, offsetX, offsetY));
        }

        // PointPointer 設隱藏的 shape 為可見
        public void SetSelectedVisible(bool visible)
        {
            _shapes.SetSelectShapeVisible(_selectedIndex, visible);
        }

        // DrawingPointer 創建 hint 要用的 function
        public Shape CreateHint(int x1, int y1)
        {
            return _factory.GenerateShape(_shapeType, new Coordinate(x1, y1), new Coordinate(x1, y1));
        }

        // DrawingPointer 創建新圖形要用的 function
        public void CreateShape(Shape shape)
        {
            _commandManager.Execute(new DrawCommand(this, shape));
        }

        // ICommand 創建新圖形要用的 function
        public void CreateShapeCommand(Shape shape)
        {
            _shapes.CreateShape(shape);
            NotifyPanelChanged();
        }

        // ICommand 從 list 移除最後一個物件
        public void RemoveLast()
        {
            _shapes.RemoveLast();
            NotifyPanelChanged();
        }

        // DeleteCommand 從 list 移除選定的物件
        public Shape RemoveAt(int index)
        {
            Shape shape = null;
            if (index > ShapeInteger.NOT_IN_LIST)
            {
                shape = _shapes.Remove(index);
                _selectedIndex = ShapeInteger.NOT_IN_LIST;
                SetPoint();
                NotifyPanelChanged();
            }
            return shape;
        }

        // DeleteCommand 在 shapes 插入 shape
        public void Insert(Shape shape, int index)
        {
            if (shape != null)
            {
                _shapes.Insert(shape, index);
                _selectedIndex = index;
                NotifyPanelChanged();
            }
        }

        // MoveCommand 移動 shape 用
        public void MoveShape(int offsetX, int offsetY)
        {
            _shapes.SetSelectShapeVisible(_selectedIndex, true);
            _shapes.MoveSelectedShape(_selectedIndex, offsetX, offsetY);
            NotifyPanelChanged();
        }
    }
}

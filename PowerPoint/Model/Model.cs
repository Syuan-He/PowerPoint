﻿using System;
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
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
            _isPressed = false;
            _pointer = new PointPointer(this);
            _commandManager = new CommandManager();
        }

        public bool IsUndoEnabled
        {
            get
            {
                return _commandManager.IsUndoEnabled;
            }
        }

        public bool IsRedoEnabled
        {
            get
            {
                return _commandManager.IsRedoEnabled;
            }
        }

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType)
        {
            _commandManager.Execute(new AddCommand(this, _factory.GenerateShape(shapeType)));
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 && rowIndex > ShapeInteger.NOT_IN_LIST)
            {
                _commandManager.Execute(new DeleteCommand(this, rowIndex));
                _selectedIndex = ShapeInteger.NOT_IN_LIST;
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
            if (_selectedIndex > ShapeInteger.NOT_IN_LIST)
                _commandManager.Execute(new DeleteCommand(this, _selectedIndex));
        }

        // pointer 進入 point 模式
        public void SetPoint()
        {
            _pointer = new PointPointer(this);
            NotifyPanelChanged();
        }

        // pointer 進入 drawing 模式
        public void SetDrawing()
        {
            _pointer = new DrawingPointer(this);
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
            NotifyPanelChanged();
        }

        // pointer 進入 scaling 模式 (有選取的 shape 就傳進 pointer，GetShape會自動判斷)
        public void SetScaling(Coordinate point1)
        {
            _pointer = new ScalingPointer(this, _shapes.GetShape(_selectedIndex), point1);
        }

        // 是否有被選取的 shape
        public bool IsHasSelected()
        {
            return _selectedIndex > ShapeInteger.NOT_IN_LIST;
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

        // PointPointer 找被點選的 shape
        public void FindSelected(int x1, int y1)
        {
            _selectedIndex = _shapes.FindSelectItem(x1, y1);
            NotifyPanelChanged();
        }

        // PointPointer 取得指定位子的 shape
        public Shape GetSelected()
        {
            return _shapes.GetShape(_selectedIndex);
        }

        // PointPointer 移動圖形
        public void MoveSelected(Coordinate startPoint, Coordinate endPoint)
        {
            _commandManager.Execute(new MoveCommand(this, _selectedIndex, startPoint, endPoint));
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

        // ScalingPointer 拉圖形
        public void ScalingSelected(Coordinate startPoint, Coordinate endPoint)
        {
            _commandManager.Execute(new ScalingCommand(this, _selectedIndex, startPoint, endPoint));
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
            Shape shape = _shapes.Remove(index);
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
            NotifyPanelChanged();
            return shape;
        }

        // DeleteCommand 在 shapes 插入 shape
        public void Insert(Shape shape, int index)
        {
            if (shape != null)
            {
                _shapes.Insert(shape, index);
                NotifyPanelChanged();
            }
        }

        // MoveCommand 移動 shape 用
        public void SetShapePosition(int index, Coordinate point1)
        {
            _shapes.SetSelectedShapePosition(index, point1);
            NotifyPanelChanged();
        }

        // ScalingCommand 拉 shape 用
        public void SetShapeEndPoint(int index, Coordinate point1, int cornerIndex)
        {
            _shapes.SetSelectedShapePoint(index, point1, cornerIndex);
            NotifyPanelChanged();
        }
    }
}
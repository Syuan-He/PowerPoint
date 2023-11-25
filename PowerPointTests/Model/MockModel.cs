using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PowerPoint.Tests
{
    class MockModel : IModel
    {
        public event Model.ModelChangedEventHandler _modelChanged;

        public string _shapeType;
        public Coordinate _point1;
        public Coordinate _point2;

        public int _countPressDeleteKey;
        public int _countPressDelete;
        public bool _isFindSelect;
        public int _stateNumber;
        public bool _isPanel;
        public IGraphics _graphics;

        public MockModel()
        {
            _countPressDeleteKey = 0;
            _countPressDelete = 0;
        }

        // 按下資訊顯示的新增按鍵
        public void PressInfoAdd(string shapeType)
        {
            _shapeType = shapeType;
        }

        // 按下鍵盤 delete 鍵
        public void PressDeleteKey()
        {
            _countPressDeleteKey++;
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            _point1 = new Coordinate(columnIndex, rowIndex);
            _countPressDelete++;
        }

        // pointer 進入 point 模式
        public void SetPoint()
        {
            _stateNumber = 0;
        }

        // pointer 進入 drawing 模式
        public void SetDrawing()
        {
            _stateNumber = 1;
        }

        // 按下滑鼠左鍵
        public void PressPointer(string shapeType, int x1, int y1)
        {
            _shapeType = shapeType;
            _point1 = new Coordinate(x1, y1);
        }

        // 滑鼠移動時
        public void MovePointer(int x2, int y2)
        {
            _point1 = new Coordinate(x2, y2);
        }

        // 放開滑鼠左鍵
        public void ReleasePointer(int x2, int y2)
        {
            _point1 = new Coordinate(x2, y2);
        }

        // 為 DrawingPointer 創建 hint
        public Shape CreateHint(int x1, int y1)
        {
            return new MockShape(ShapeType.LINE, new Coordinate(x1, y1), new Coordinate(x1, y1));
        }

        // DrawingPointer(state pattern) 創建新圖形要用的 function
        public void CreateShape(string shapeType, Coordinate point1, Coordinate point2)
        {
            _shapeType = shapeType;
            _point1 = point1;
            _point2 = point2;
        }

        // PointPointer 搜尋重疊要用的 function
        public bool FindSelectShape(int x1, int y1)
        {
            if (x1 == 0 && y1 == 1)
                _isFindSelect = true;
            else
                _isFindSelect = false;
            return _isFindSelect;
        }

        // 移動選取的圖形
        public void MoveShape(int x1, int y1)
        {
            _point1 = new Coordinate(x1, y1);
        }

        // 繪製選取外框
        public void DrawSelectFrame(IGraphics graphics)
        {
            graphics.DrawSelectFrame(0, 0, 0, 0);
        }

        // 繪製圖形
        public void Draw(IGraphics graphics, bool isPanel)
        {
            _graphics = graphics;
            _isPanel = isPanel;
        }

        // 通知 model 要重新繪製 panel
        public void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }

        //回傳 Shapes 的 BindingList ，給資訊顯示的 _infoDataGridView 用
        public BindingList<Shape> GetInfoDataGridView()
        {
            return new BindingList<Shape>();
        }
    }
}

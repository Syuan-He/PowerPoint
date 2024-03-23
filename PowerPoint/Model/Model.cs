using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PowerPoint
{
    public class Model : IModel
    {
        public event ModelChangedEventHandler _panelChanged;
        public delegate void ModelChangedEventHandler();
        public event SlideRemoveAtEventHandler _slideRemoveAt;
        public delegate void SlideRemoveAtEventHandler(int index);
        public event SlideInsertEventHandler _slideInsert;
        public delegate void SlideInsertEventHandler(int index);

        List<Shapes> _pages;
        IFactory _factory;
        IState _pointer;
        CommandManager _commandManager;
        string _shapeType;
        bool _isPressed;
        int _selectedIndex;
        IGoogleDriveService _service;

        public Model(IFactory factory, IGoogleDriveService service)
        {
            _factory = factory;
            _pages = new List<Shapes>();
            _pages.Add(new Shapes(factory));
            Initialize();
            _service = service;
        }

        // 初始化model
        void Initialize()
        {
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
            PageIndex = 0;
            _isPressed = false;
            _pointer = new PointPointer(this);
            _commandManager = new CommandManager();
        }

        Shapes CurrentShapes
        {
            get
            {
                return _pages[PageIndex];
            }
        }

        public int PageIndex
        {
            get; set;
        }

        public int PagesCount
        {
            get
            {
                return _pages.Count;
            }
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
        public void PressInfoAdd(string shapeType, Coordinate point1, Coordinate point2)
        {
            _commandManager.Execute(new AddCommand(this, _factory.GenerateShape(shapeType, point1, point2), PageIndex));
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDelete(int columnIndex, int rowIndex)
        {
            if (columnIndex == 0 && rowIndex > ShapeInteger.NOT_IN_LIST)
            {
                _commandManager.Execute(new DeleteCommand(this, rowIndex, PageIndex));
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

        // 按下 AddPage 按鍵
        public void PressAddPage()
        {
            _commandManager.Execute(new AddPageCommand(this, _pages.Count, new Shapes(_factory)));
            PageIndex = _pages.Count - 1;
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
        }

        // 按下鍵盤 delete 鍵
        public void PressDeleteKey()
        {
            Debug.Assert(_selectedIndex >= ShapeInteger.NOT_IN_LIST, ErrorMessage.NEGATIVE_INDEX_SHAPE);
            if (_selectedIndex == ShapeInteger.NOT_IN_LIST && _pages.Count > 1)
                _commandManager.Execute(new DeletePageCommand(this, PageIndex));
            else if (_selectedIndex > ShapeInteger.NOT_IN_LIST)
                _commandManager.Execute(new DeleteCommand(this, _selectedIndex, PageIndex));
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
            _pointer = new ScalingPointer(this, CurrentShapes.GetShape(_selectedIndex), point1);
        }

        // 是否有被選取的 shape
        public bool IsHasSelected()
        {
            return _selectedIndex > ShapeInteger.NOT_IN_LIST;
        }

        // 判斷是不是位在頂點上
        public int GetAtSelectedCorner(int x1, int y1)
        {
            return CurrentShapes.GetAtSelectedCorner(_selectedIndex, x1, y1);
        }

        // 判斷是不是位在頂點上(多載)
        public int GetAtSelectedCorner(int x1, int y1, int index)
        {
            return _pages[index].GetAtSelectedCorner(_selectedIndex, x1, y1);
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
            CurrentShapes.DrawSelectFrame(graphics, _selectedIndex);
        }

        // 繪製圖形
        public void Draw(IGraphics graphics)
        {
            graphics.ClearAll();
            CurrentShapes.Draw(graphics);
            _pointer.Draw(graphics);
        }

        // 繪製圖形
        public void DrawSlide(IGraphics graphics, int index)
        {
            graphics.ClearAll();
            if (index < _pages.Count)
                _pages[index].Draw(graphics);
        }

        // 通知 model 要重新繪製 panel
        void NotifyPanelChanged()
        {
            if (_panelChanged != null)
                _panelChanged();
        }

        // 回傳 Shapes 的 BindingList ，給資訊顯示的 _infoDataGridView 用
        public System.ComponentModel.BindingList<Shape> GetInfoDataGridView()
        {
            return CurrentShapes.ShapeList;
        }

        // 存檔
        public void Save(bool isSave)
        {
            if (isSave)
                FileManager.Save(new PagesForSave(_pages), _service);
        }

        // 讀檔
        public void Load(bool isLoad)
        {
            if (isLoad)
            {
                _pages = FileManager.Load(_service);
                Initialize();
            }
        }

        // PointPointer 找被點選的 shape
        public void FindSelected(int x1, int y1)
        {
            _selectedIndex = CurrentShapes.FindSelectItem(x1, y1);
            NotifyPanelChanged();
        }

        // PointPointer 取得指定位子的 shape
        public Shape GetSelected()
        {
            return CurrentShapes.GetShape(_selectedIndex);
        }

        // PointPointer 移動圖形
        public void MoveSelected(Coordinate startPoint, Coordinate endPoint)
        {
            _commandManager.Execute(new MoveCommand(this, new Coordinate(_selectedIndex, PageIndex), startPoint, endPoint));
        }

        // DrawingPointer 創建 hint 要用的 function
        public Shape CreateHint(int x1, int y1)
        {
            return _factory.GenerateShape(_shapeType, new Coordinate(x1, y1), new Coordinate(x1, y1));
        }

        // DrawingPointer 創建新圖形要用的 function
        public void CreateShape(Shape shape)
        {
            _commandManager.Execute(new DrawCommand(this, shape, PageIndex));
        }

        // ScalingPointer 拉圖形
        public void ScalingSelected(Coordinate startPoint, Coordinate endPoint)
        {
            _commandManager.Execute(new ScalingCommand(this, new Coordinate(_selectedIndex, PageIndex), startPoint, endPoint));
        }

        // ICommand 創建新圖形要用的 function
        public void CreateShapeCommand(Shape shape, int index)
        {
            _pages[index].CreateShape(shape);
            NotifyPanelChanged();
        }

        // ICommand 從 list 移除最後一個物件
        public void RemoveLast(int index)
        {
            _pages[index].RemoveLast();
            NotifyPanelChanged();
        }

        // DeleteCommand 從 list 移除選定的物件
        public Shape RemoveAt(int index, int pageIndex)
        {
            Shape shape = _pages[pageIndex].Remove(index);
            _selectedIndex = ShapeInteger.NOT_IN_LIST;
            NotifyPanelChanged();
            return shape;
        }

        // DeleteCommand 在 shapes 插入 shape
        public void Insert(Shape shape, int index, int pageIndex)
        {
            Debug.Assert(shape != null, ErrorMessage.INSERT_LOSS_SHAPE);
            _pages[pageIndex].Insert(shape, index);
            NotifyPanelChanged();
        }

        // DeletePageCommand 從 list 移除選定的物件
        public Shapes RemovePageAt(int index)
        {
            Shapes shapes = _pages[index];
            _pages.RemoveAt(index);
            if (PageIndex >= _pages.Count)
                PageIndex = _pages.Count - 1;
            if (_slideRemoveAt != null)
                _slideRemoveAt(index);
            NotifyPanelChanged();
            return shapes;
        }

        // DeletePageCommand 在 pages 插入 shapes
        public void InsertPage(Shapes shapes, int index)
        {
            Debug.Assert(shapes != null, ErrorMessage.INSERT_LOSS_SHAPES);
            _pages.Insert(index, shapes);
            if (_slideInsert != null)
                _slideInsert(index);
        }

        // MoveCommand 移動 shape 用
        public void SetShapePosition(int index, int pageIndex, Coordinate point1)
        {
            _pages[pageIndex].SetSelectedShapePosition(index, point1);
            NotifyPanelChanged();
        }

        // ScalingCommand 拉 shape 用
        public void SetShapeEndPoint(int index, int pageIndex, Coordinate point1, int cornerIndex)
        {
            _pages[pageIndex].SetSelectedShapePoint(index, point1, cornerIndex);
            NotifyPanelChanged();
        }
    }
}
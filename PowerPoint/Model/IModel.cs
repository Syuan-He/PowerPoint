using System.ComponentModel;

namespace PowerPoint
{
    public interface IModel
    {
        event Model.ModelChangedEventHandler _modelChanged;

        // 按下資訊顯示的新增按鍵
        void PressInfoAdd(string shapeType);

        // 按下鍵盤 delete 鍵
        void PressDeleteKey();

        // 按下資訊顯示的刪除按鍵
        void PressDelete(int columnIndex, int rowIndex);

        // pointer 進入 point 模式
        void SetPoint();

        // pointer 進入 drawing 模式
        void SetDrawing();

        // 按下滑鼠左鍵
        void PressPointer(string shapeType, int x1, int y1);

        // 滑鼠移動時
        void MovePointer(int x2, int y2);

        // 放開滑鼠左鍵
        void ReleasePointer(int x2, int y2);

        // 為 DrawingPointer 創建 hint
        Shape CreateHint(int x1, int y1);

        // DrawingPointer(state pattern) 創建新圖形要用的 function
        void CreateShape(string shapeType, Coordinate point1, Coordinate point2);

        // PointPointer 搜尋重疊要用的 function
        bool FindSelectShape(int x1, int y1);

        // 移動選取的圖形
        void MoveShape(int x1, int y1);

        // 繪製選取外框
        void DrawSelectFrame(IGraphics graphics);

        // 繪製圖形
        void Draw(IGraphics graphics, bool isPanel);

        // 通知 model 要重新繪製 panel
        void NotifyModelChanged();

        //回傳 Shapes 的 BindingList ，給資訊顯示的 _infoDataGridView 用
        BindingList<Shape> GetInfoDataGridView();
    }
}
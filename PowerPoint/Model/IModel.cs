using System.ComponentModel;

namespace PowerPoint
{
    public interface IModel
    {
        // 按下鍵盤 delete 鍵
        void PressDeleteKey();

        // 按下 Undo 按鍵
        void PressUndo();

        // 按下 Redo 按鍵
        void PressRedo();

        // pointer 進入 point 模式
        void SetPoint();

        // pointer 進入 drawing 模式
        void SetDrawing();

        // 是否有被選取的 shape
        bool IsHasSelected();

        // 判斷是不是位在頂點上
        int GetAtSelectedCorner(int x1, int y1);

        // 按下滑鼠左鍵
        void PressPointer(string shapeType, int x1, int y1);

        // 滑鼠移動時
        void MovePointer(int x2, int y2);

        // 放開滑鼠左鍵
        void ReleasePointer(int x2, int y2);

        // 繪製圖形
        void Draw(IGraphics graphics, bool isPanel);
    }
}
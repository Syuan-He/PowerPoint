namespace PowerPoint
{
    public class PointPointer : IState
    {
        Shape _shape;
        int _firstPointX;
        int _firstPointY;

        public PointPointer(Shape shape)
        {
            _shape = shape;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1, Shape shape)
        {
            _firstPointX = x1;
            _firstPointY = y1;
            _shape = shape;
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_shape != null)
                _shape.SetMove(x2 - _firstPointX, y2 - _firstPointY);
            _firstPointX = x2;
            _firstPointY = y2;
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if (_shape != null)
            {
                _shape.SetMove(x2 - _firstPointX, y2 - _firstPointY);
                _shape.AdjustPoint();
            }
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            if (_shape != null)
                _shape.DrawSelectFrame(graphics);
        }
    }
}

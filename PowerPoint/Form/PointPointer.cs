namespace PowerPoint
{
    public class PointPointer : IState
    {
        Model _model;
        int _firstPointX;
        int _firstPointY;
        bool _isPressed;

        public PointPointer(Model model)
        {
            _model = model;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(string shapeType, int x1, int y1)
        {
            
            if (_model.FindSelectShape(x1, y1))
            {
                _firstPointX = x1;
                _firstPointY = y1;
            }
            _isPressed = true;
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_isPressed)
            {
                _model.MoveShape(x2 - _firstPointX, y2 - _firstPointY);
                _firstPointX = x2;
                _firstPointY = y2;
            }
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            _isPressed = false;
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            _model.DrawSelectFrame(graphics);
        }
    }
}

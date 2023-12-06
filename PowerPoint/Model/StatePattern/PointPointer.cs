namespace PowerPoint
{
    public class PointPointer : IState
    {
        Model _model;
        Shape _shape;
        int _firstPointX;
        int _firstPointY;
        int _x1;
        int _y1;

        public PointPointer(Model model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1)
        {
            _firstPointX = x1;
            _firstPointY = y1;
            _x1 = x1;
            _y1 = y1;
            _shape = null;
            if (_model.GetAtSelectedCorner(x1, y1) < 0)
            {
                GetDuplicate(x1, y1);
            }
            else
                Change2Scaling();
        }

        // 取得被點選的 shape 的複製
        void GetDuplicate(int x1, int y1)
        {
            _model.FindSelected(x1, y1);
            _shape = _model.GetSelectedDuplicate();
            _model.SetSelectedVisible(false);
        }

        // 切換成拉縮 shape 模式
        void Change2Scaling()
        {
            _model.SetScaling();
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_shape != null)
            {
                _shape.SetMove(x2 - _x1, y2 - _y1);
                _x1 = x2;
                _y1 = y2;
            }
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if (x2 != _firstPointX && y2 != _firstPointY && _shape != null)
            {
                _model.MoveSelected(x2 - _firstPointX, y2 - _firstPointY);
            }
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            if (_shape != null)
            {
                _shape.Draw(graphics);
                _shape.DrawSelectFrame(graphics);
            }
        }
    }
}

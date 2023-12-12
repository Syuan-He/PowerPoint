namespace PowerPoint
{
    public class PointPointer : IState
    {
        Model _model;
        Shape _shape;
        Coordinate _firstPoint;
        int _x1;
        int _y1;

        public PointPointer(Model model)
        {
            _model = model;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1)
        {
            _x1 = x1;
            _y1 = y1;
            if (_model.GetAtSelectedCorner(x1, y1) != ShapeInteger.TOTAL_CORNER)
            {
                GetSelect(x1, y1);
                if (_shape != null)
                    _firstPoint = _shape.GetPoint1();
            }
            else
                Change2Scaling(x1, y1);
        }

        // 取得被點選的 shape 的複製
        void GetSelect(int x1, int y1)
        {
            _model.FindSelected(x1, y1);
            _shape = _model.GetSelected();
        }

        // 切換成拉縮 shape 模式
        void Change2Scaling(int x1, int y1)
        {
            _model.SetScaling(new Coordinate(x1, y1));
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
            if (_shape != null)
            {
                _shape.SetMove(x2 - _x1, y2 - _y1);
                Coordinate endPoint = _shape.GetPoint1();
                if (!_firstPoint.AreEqual(endPoint))
                {
                    _model.MoveSelected(_firstPoint, endPoint);
                }
            }
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            _model.DrawSelectFrame(graphics);
        }
    }
}

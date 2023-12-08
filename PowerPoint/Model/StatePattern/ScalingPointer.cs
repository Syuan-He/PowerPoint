﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class ScalingPointer : IState
    {
        Model _model;
        Shape _shape;
        Coordinate _firstPoint;

        public ScalingPointer(Model model, Shape shape, Coordinate point1)
        {
            _model = model;
            _shape = shape;
            _firstPoint = point1;
        }

        // 按下滑鼠左鍵時
        public void PressPointer(int x1, int y1)
        {
        }

        // 滑鼠移動
        public void MovePointer(int x2, int y2)
        {
            if (_shape != null)
                _shape.SetEndPoint(x2, y2);
        }

        // 放掉滑鼠左鍵時
        public void ReleasePointer(int x2, int y2)
        {
            if (_shape != null)
            {
                _shape.AdjustPoint();
                Coordinate endPoint = new Coordinate(x2, y2);
                if (!_firstPoint.AreEqual(endPoint))
                    _model.ScalingSelected(_firstPoint,endPoint);
                _model.SetPoint();
            }
        }

        // 為鼠標繪製操作產生的圖形
        public void Draw(IGraphics graphics)
        {
            _model.DrawSelectFrame(graphics);
        }
    }
}

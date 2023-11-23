﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    class SlideAdaptor : IGraphics
    {
        private const int HALF = 2;
        private const int DIAMETER = 10;
        private const int RADIUS = 5;
        Graphics _graphics;
        int _panelWidth;
        int _panelHeight;
        int _slideWidth;
        int _slideHeight;

        public SlideAdaptor(Graphics graphics, Coordinate panelSize, Coordinate slideSize)
        {
            this._graphics = graphics;
            _panelWidth = panelSize.X;
            _panelHeight = panelSize.Y;
            _slideWidth = slideSize.X;
            _slideHeight = slideSize.Y;
        }

        //清除畫面
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        //畫直線
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            _graphics.DrawLine(
                Pens.Black,
                x1 * _slideWidth / _panelWidth,
                y1 * _slideHeight / _panelHeight,
                x2 * _slideWidth / _panelWidth,
                y2 * _slideHeight / _panelHeight);
        }

        //畫矩形
        public void DrawRectangle(int x1, int y1, int width, int height)
        {
            _graphics.DrawRectangle(
                Pens.Black,
                x1 * _slideWidth / _panelWidth,
                y1 * _slideHeight / _panelHeight,
                width * _slideWidth / _panelWidth,
                height * _slideHeight / _panelHeight);
        }

        //畫圓圈
        public void DrawEllipse(int x1, int y1, int width, int height)
        {
            _graphics.DrawEllipse(
                Pens.Black,
                x1 * _slideWidth / _panelWidth,
                y1 * _slideHeight / _panelHeight,
                width * _slideWidth / _panelWidth,
                height * _slideHeight / _panelHeight);
        }

        // 繪製選取外框
        public void DrawSelectFrame(int x1, int y1, int x2, int y2)
        {
            _graphics.DrawRectangle(
                Pens.Black,
                Math.Min(x1, x2),
                Math.Min(y1, y2),
                Math.Abs(x2 - x1),
                Math.Abs(y2 - y1));

            _graphics.DrawEllipse(Pens.Black, x1 - RADIUS, y1 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, x2 - RADIUS, y1 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, x1 - RADIUS, y2 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, x2 - RADIUS, y2 - RADIUS, DIAMETER, DIAMETER);

            _graphics.DrawEllipse(Pens.Black, (x1 + x2) / HALF - RADIUS, y1 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, (x1 + x2) / HALF - RADIUS, y2 - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, x1 - RADIUS, (y1 + y2) / HALF - RADIUS, DIAMETER, DIAMETER);
            _graphics.DrawEllipse(Pens.Black, x2 - RADIUS, (y1 + y2) / HALF - RADIUS, DIAMETER, DIAMETER);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PowerPoint.Tests
{
    class MockModel : IModel
    {
        public string _shapeType;
        public Coordinate _point1;

        public int _countPressDeleteKey;
        public int _countPressDelete;
        public int _stateNumber;
        public IGraphics _graphics;
        public int _index;
        public bool _isHasSelected;

        public MockModel()
        {
            _countPressDeleteKey = 0;
            _countPressDelete = 0;
            _isHasSelected = true;
        }

        // 按下資訊顯示的刪除按鍵
        public void PressDeleteKey()
        {
            _countPressDeleteKey++;
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

        // 是否有被選取的 shape
        public bool IsHasSelected()
        {
            return _isHasSelected;
        }

        // 判斷是不是位在頂點上
        public int GetAtSelectedCorner(int x1, int y1)
        {
            if (x1 == 1 && y1 == 1)
            {
                return 8;
            }
            return -1;
        }

        // 按下滑鼠左鍵
        public void PressPointer(string shapeType, int x1, int y1)
        {
            _shapeType = shapeType;
            _point1 = new Coordinate(x1, y1);
        }

        // 移動滑鼠
        void IModel.MovePointer(int x2, int y2)
        {
            _point1 = new Coordinate(x2, y2);
        }

        // 放開滑鼠左鍵
        void IModel.ReleasePointer(int x2, int y2)
        {
            _point1 = new Coordinate(x2, y2);
        }

        // 繪製圖形
        public void Draw(IGraphics graphics)
        {
            _graphics = graphics;
        }

        // 繪製圖形
        public void DrawSlide(IGraphics graphics, int index)
        {
            _graphics = graphics;
            _index = index;
        }
    }
}
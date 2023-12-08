using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;

namespace PowerPoint
{
    public class PresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string LINE_PROPERTY = "IsLine";
        private const string RECTANGLE_PROPERTY = "IsRectangle";
        private const string CIRCLE_PROPERTY = "IsCircle";
        private const string POINTER_PROPERTY = "IsPointer";
        private const int LINE_NUMBER = 0;
        private const int RECTANGLE_NUMBER = 1;
        private const int CIRCLE_NUMBER = 2;
        private const int POINTER_NUMBER = 3;
        private const float WIDTH = 1920;
        string[] _stringToolStripList = { ShapeType.LINE, ShapeType.RECTANGLE, ShapeType.CIRCLE, null };
        Cursor[] _cornerCursors = { Cursors.SizeNWSE, Cursors.SizeNS, Cursors.SizeNESW, Cursors.SizeWE, Cursors.Default, Cursors.SizeNS, Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE };

        IModel _model;

        bool[] _booleanToolStripList = { false, false, false, true };
        string _shapeType;

        public PresentationModel(IModel model)
        {
            _shapeType = null;
            this._model = model;
        }

        public bool IsLine
        {
            get
            {
                return _booleanToolStripList[LINE_NUMBER];
            }
        }

        public bool IsRectangle
        {
            get
            {
                return _booleanToolStripList[RECTANGLE_NUMBER];
            }
        }

        public bool IsCircle
        {
            get
            {
                return _booleanToolStripList[CIRCLE_NUMBER];
            }
        }

        public bool IsPointer
        {
            get
            {
                return _booleanToolStripList[POINTER_NUMBER];
            }
        }

        // 按下 toolStrip 的 Line 按鍵
        public void PressLineButton()
        {
            PressToolStrip(LINE_NUMBER);
            NotifyPropertyChanged();
        }

        // 按下 toolStrip 的 Rectangle 按鍵
        public void PressRectangleButton()
        {
            PressToolStrip(RECTANGLE_NUMBER);
            NotifyPropertyChanged();
        }

        // 按下 toolStrip 的 Circle 按鍵
        public void PressCircleButton()
        {
            PressToolStrip(CIRCLE_NUMBER);
            NotifyPropertyChanged();
        }

        // 按下 toolStrip 的 Pointer 按鍵
        public void PressPointerButton()
        {
            if (!_booleanToolStripList[POINTER_NUMBER])
            {
                SetFalse();
                _booleanToolStripList[POINTER_NUMBER] = true;
                _model.SetPoint();
            }
            _shapeType = null;
            NotifyPropertyChanged();
        }

        // ToolStrip 按下繪製圖形按鍵，統一會用到的反應
        void PressToolStrip(int codeNumber)
        {
            if (_booleanToolStripList[codeNumber])
            {
                _shapeType = null;
                _booleanToolStripList[codeNumber] = false;
                _booleanToolStripList[POINTER_NUMBER] = true;
                _model.SetPoint();
            }
            else
            {
                _shapeType = _stringToolStripList[codeNumber];
                SetFalse();
                _booleanToolStripList[codeNumber] = true;
                _model.SetDrawing();
            }
        }

        // 按下滑鼠左鍵時，依據 ToolStrip 的選取狀況，決定要拉什麼圖
        public void PressPointer(int x1, int y1, int width)
        {
            float ratio = WIDTH / width;
            _model.PressPointer(_shapeType, (int)(x1 * ratio), (int)(y1 * ratio));
        }

        // 滑鼠移動時
        public void MovePointer(int x2, int y2, int width)
        {
            float ratio = WIDTH / width;
            _model.MovePointer((int)(x2 * ratio), (int)(y2 * ratio));
        }

        // 放開滑鼠左鍵
        public void ReleasePointer(int x2, int y2, int width)
        {
            float ratio = WIDTH / width;
            _model.ReleasePointer((int)(x2 * ratio), (int)(y2 * ratio));
            PressPointerButton();
        }

        // 按下刪除鍵
        public void PressDelete(Keys code)
        {
            if (code == Keys.Delete)
            {
                _model.PressDeleteKey();
            }
        }

        // 傳回鼠標當下應該要有的樣子(形狀)
        public Cursor GetPointerShape()
        {
            if (_shapeType != null)
            {
                return Cursors.Cross;
            }
            return Cursors.Default;
        }

        // 傳回鼠標當下應該要有的樣子(形狀)
        public Cursor GetPointerShape(int x1, int y1, int width)
        {
            float ratio = WIDTH / width;
            if (_shapeType != null)
            {
                return Cursors.Cross;
            }
            else if (_model.IsHasSelected())
            {
                return GetCornerCursor(_model.GetAtSelectedCorner((int)(x1 * ratio), (int)(y1 * ratio)));
            }
            return Cursors.Default;
        }

        // 取得選取邊緣的鼠標
        Cursor GetCornerCursor(int index)
        {
            if (index >= 0 && index <= ShapeInteger.TOTAL_CORNER)
            {
                return _cornerCursors[index];
            }
            return Cursors.Default;
        }

        // 讓 model 畫圖
        public void Draw(Graphics graphics, int width)
        {
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics, width), true);
        }

        // 讓 model 畫縮圖
        public void DrawSlide(Graphics graphics, int width)
        {
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics, width), false);
        }

        // 通知 ToolBar 相關 bool 變數改變
        void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // 統一呼叫 NotifyPropertyChanged 的多載
        void NotifyPropertyChanged()
        {
            NotifyPropertyChanged(LINE_PROPERTY);
            NotifyPropertyChanged(RECTANGLE_PROPERTY);
            NotifyPropertyChanged(CIRCLE_PROPERTY);
            NotifyPropertyChanged(POINTER_PROPERTY);
        }

        // 統一設 ToolBar 相關 bool 變數為 false
        void SetFalse()
        {
            for (int i = 0; i < _booleanToolStripList.Length; i++)
            {
                _booleanToolStripList[i] = false;
            }
        }
    }
}

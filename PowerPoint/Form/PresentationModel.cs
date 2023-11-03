using System.Windows.Forms;
using System.ComponentModel;

namespace PowerPoint
{
    public class PresentationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string LINE_PROPERTY = "IsLine";
        private const string RECTANGLE_PROPERTY = "IsRectangle";
        private const string CIRCLE_PROPERTY = "IsCircle";
        Model _model;

        bool _isLine = false;
        bool _isRectangle = false;
        bool _isCircle = false;
        string _shapeType = null;

        public PresentationModel(Model model)
        {
            this._model = model;
        }
        
        public bool IsLine
        {
            get
            {
                return _isLine;
            }
        }

        public bool IsRectangle
        {
            get
            {
                return _isRectangle;
            }
        }

        public bool IsCircle
        {
            get
            {
                return _isCircle;
            }
        }

        // 按下 toolStrip 的 Line 按鍵
        public void PressLineButton()
        {
            if (_shapeType != ShapeType.LINE)
            {
                _shapeType = ShapeType.LINE;
                SetFalse();
                _isLine = true;
            }
            else
            {
                _shapeType = null;
                _isLine = false;
            }
            NotifyPropertyChanged();
        }

        // 按下 toolStrip 的 Rectangle 按鍵
        public void PressRectangleButton()
        {
            if (_shapeType != ShapeType.RECTANGLE)
            {
                _shapeType = ShapeType.RECTANGLE;
                SetFalse();
                _isRectangle = true;
            }
            else
            {
                _shapeType = null;
                _isRectangle = false;
            }
            NotifyPropertyChanged();
        }

        // 按下 toolStrip 的 Line 按鍵
        public void PressCircleButton()
        {
            if (_shapeType != ShapeType.CIRCLE)
            {
                _shapeType = ShapeType.CIRCLE;
                SetFalse();
                _isCircle = true;
            }
            else
            {
                _shapeType = null;
                _isCircle = false;
            }
            NotifyPropertyChanged();
        }

        // 按下滑鼠左鍵時，依據 ToolStrip 的選取狀況，決定要拉什麼圖
        public void PressPointer(int x1, int y1)
        {
            if (_shapeType != null)
            {
                _model.PressPointer(_shapeType, x1, y1);
            }
        }

        // 放掉滑鼠左鍵時，設 shapeType 為 null (鼠標設回 Default )，ToolStrip 的選取因此皆為 false
        public void ReleasePointer()
        {
            _shapeType = null;
            NotifyPropertyChanged();
            SetFalse();
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

        // 確認是否為畫線模式
        public bool IsLineChecked()
        {
            return _isLine;
        }

        // 確認是否為畫矩形模式
        public bool IsRectangleChecked()
        {
            return _isRectangle;
        }

        // 確認是否為畫圓圈模式
        public bool IsCircleChecked()
        {
            return _isCircle;
        }

        // 讓 model 畫圖
        public void Draw(System.Drawing.Graphics graphics)
        {
            // graphics物件是Paint事件帶進來的，只能在當次Paint使用
            // 而Adaptor又直接使用graphics，這樣DoubleBuffer才能正確運作
            // 因此，Adaptor不能重複使用，每次都要重新new
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
        }

        // obviser
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
        }

        // 統一設 ToolBar 相關 bool 變數為 false
        void SetFalse()
        {
            _isLine = false;
            _isRectangle = false;
            _isCircle = false;
        }
    }
}

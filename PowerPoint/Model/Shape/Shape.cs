using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace PowerPoint
{
    public abstract class Shape : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string ShapeName
        {
            get;
            set;
        }

        public string Information
        {
            get;
            set;
        }

        // 設定圖形終點
        public abstract void SetEndPoint(int x2, int y2);

        // 移動圖形
        public abstract void SetMove(int offsetX, int offsetY);

        // 檢查是否被選取
        public abstract bool IsSelect(int x1, int y1);

        // 確認在哪個頂點上
        public abstract int GetAtCorner(int x1, int y1);

        //調整傳入的 point 的座標，使第一個 point 的座標在左上，第二個在右下
        public abstract void AdjustPoint();

        // 繪製該圖形
        public abstract void Draw(IGraphics graphics);

        // 繪製選取外框
        public abstract void DrawSelectFrame(IGraphics graphics);

        // observer
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

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

        // 取得圖形物件的型態資料
        public abstract string GetShapeName();

        // 取得物件的頂點座標
        public abstract string GetInfo();

        // 設定圖形終點
        public abstract void SetEndPoint(Point endPoint);

        // 移動圖形
        public abstract void SetMove(int offsetX, int offsetY);

        // 檢查是否被選取
        public abstract bool IsSelect(int x1, int y1);

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

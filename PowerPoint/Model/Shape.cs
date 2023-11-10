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
        protected string _shapeType;
        protected string _info;
        public string ShapeName
        {
            get
            {
                return _shapeType;
            }
        }

        public string Information
        {
            get
            {
                return _info;
            }
        }

        // 取得圖形物件的型態資料
        public abstract string GetShapeName();

        // 取得物件的頂點座標
        public abstract string GetInfo();

        // 設定圖形終點
        public abstract void SetEndPoint(Point endPoint);

        // 繪製該圖形
        public abstract void Draw(IGraphics graphics);

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

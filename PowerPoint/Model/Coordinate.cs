using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class Coordinate
    {
        private const string FORMAT = "({0}, {1})";

        public Coordinate(int x1, int y1)
        {
            X = x1;
            Y = y1;
        }

        public int X
        {
            get;
            set;
        }

        public int Y
        {
            get;
            set;
        }

        // 轉成字串顯示內容
        public override string ToString()
        {
            return String.Format(FORMAT, X, Y);
        }

        // 判斷是否相等
        public bool AreEqual(Coordinate coordinate)
        {
            return ToString() == coordinate.ToString();
        }
    }
}

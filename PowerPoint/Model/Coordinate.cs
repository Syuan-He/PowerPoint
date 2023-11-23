using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class Coordinate
    {
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
    }
}

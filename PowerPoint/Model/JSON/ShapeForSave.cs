using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class ShapeForSave
    {
        public ShapeForSave(Shape shape)
        {
            int[] point = shape.GetCoordinateList();
            X1 = point[ShapeInteger.X1];
            Y1 = point[ShapeInteger.Y1];
            X2 = point[ShapeInteger.X2];
            Y2 = point[ShapeInteger.Y2];
            Type = shape.ShapeName;
        }
        
        public ShapeForSave()
        {
        }

        public int X1
        {
            get;
            set;
        }

        public int Y1
        {
            get;
            set;
        }
        public int X2
        {
            get;
            set;
        }
        public int Y2
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        // 把該 class 轉回 shape
        public Shape Turn2Shape()
        {
            Factory factory = new Factory(new Random());
            return factory.GenerateShape(Type, new Coordinate(X1, Y1), new Coordinate(X2, Y2));
        }
    }
}

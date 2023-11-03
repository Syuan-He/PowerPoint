using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class Factory
    {
        //實作 Shape 的 Simple Factory
        public static Shape GenerateShape(string type, Point point1, Point point2)
        {
            switch (type)
            {
                case ShapeType.LINE:
                    return new Line(point1, point2);
                case ShapeType.RECTANGLE:
                    return new Rectangle(point1, point2);
                case ShapeType.CIRCLE:
                    return new Circle(point1, point2);
            }
            return null;
        }

        // 用多載實作能產生隨機位子的 Shape 的 Simple Factory
        public static Shape GenerateShape(string type, int width, int height)
        {
            return GenerateShape(type, CreateRandomPoint(width, height), CreateRandomPoint(width, height));
        }

        // 產生一個位子隨機的點
        public static Point CreateRandomPoint(int width, int height)
        {
            Random randomPosition = new Random(Guid.NewGuid().GetHashCode());
            Point point = new Point(randomPosition.Next(width), randomPosition.Next(height));
            return point;
        }
    }
}

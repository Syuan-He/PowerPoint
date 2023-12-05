using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint
{
    public class Factory : IFactory
    {
        Random _randomPosition;
        public Factory(Random random)
        {
            _randomPosition = random;
        }

        //實作 Shape 的 Simple Factory
        public Shape GenerateShape(string type, Coordinate point1, Coordinate point2)
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
        public Shape GenerateShape(string type, int width, int height)
        {
            return GenerateShape(type, CreateRandomPoint(width, height), CreateRandomPoint(width, height));
        }

        // 產生一個位子隨機的點
        Coordinate CreateRandomPoint(int width, int height)
        {
            return new Coordinate(CreateRandomNumber(width), CreateRandomNumber(height));
        }

        // 產生隨機數
        int CreateRandomNumber(int maxNumber)
        {
            return _randomPosition.Next(maxNumber);
        }
    }
}

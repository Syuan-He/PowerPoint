using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    class MockFactory : IFactory
    {
        public int _width;
        public int _height;
        public Coordinate _point1;
        public Coordinate _point2;
        public string _shapeType;
        MockRandom _mockRandom;

        public MockFactory()
        {
            _mockRandom = new MockRandom();
        }

        //實作 Shape 的 Simple Factory
        public Shape GenerateShape(string type, Coordinate point1, Coordinate point2)
        {

            _point1 = point1;
            _point2 = point2;
            _shapeType = type;
            switch (type)
            {
                case ShapeType.LINE:
                    return new MockShape(type, point1, point2);
                case ShapeType.RECTANGLE:
                    return new MockShape(type, point1, point2);
                case ShapeType.CIRCLE:
                    return new MockShape(type, point1, point2);
            }
            return null;
        }

        // 用多載實作能產生隨機位子的 Shape 的 Simple Factory
        public Shape GenerateShape(string type, int width, int height)
        {
            _width = width;
            _height = height;
            return GenerateShape(type, CreateRandomPoint(width, height), CreateRandomPoint(width, height));
        }

        // 產生一個位子隨機的點
        Coordinate CreateRandomPoint(int width, int height)
        {
            return new Coordinate(_mockRandom.Next(width), _mockRandom.Next(height));
        }
    }
}

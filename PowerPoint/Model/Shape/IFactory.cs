using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public interface IFactory
    {
        //實作 Shape 的 Simple Factory
        Shape GenerateShape(string type, Coordinate point1, Coordinate point2);

        // 用多載實作能產生隨機位子的 Shape 的 Simple Factory
        Shape GenerateShape(string type, int width, int height);
    }
}

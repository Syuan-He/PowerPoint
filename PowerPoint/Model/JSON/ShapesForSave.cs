using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class ShapesForSave
    {
        public ShapesForSave(Shapes shapes)
        {
            ShapeList = shapes.GetShapeListForSave();
        }

        public ShapesForSave()
        {
        }

        public List<ShapeForSave> ShapeList
        {
            get;
            set;
        }

        // // 把該 class 轉回 shapes
        public Shapes Turn2Shapes()
        {
            Shapes shapes = new Shapes(new Factory(new Random()));
            foreach (ShapeForSave item in ShapeList)
                shapes.CreateShape(item.Turn2Shape());
            return shapes;
        }
    }
}

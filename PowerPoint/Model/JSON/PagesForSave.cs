using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class PagesForSave
    {
        public PagesForSave(List<Shapes> pages)
        {
            PagesList = new List<ShapesForSave>();
            foreach (Shapes shapes in pages)
                PagesList.Add(new ShapesForSave(shapes));
        }

        public PagesForSave() 
        {        
        }

        public List<ShapesForSave> PagesList
        {
            get;
            set;
        }

        // // 把該 class 轉回 Pages
        public List<Shapes> Turn2Pages()
        {
            List<Shapes> shapes = new List<Shapes>();
            foreach (ShapesForSave item in PagesList)
                shapes.Add(item.Turn2Shapes());
            return shapes;
        }
    }
}

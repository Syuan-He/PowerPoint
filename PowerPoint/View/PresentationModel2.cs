using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public class PresentationModel2
    {
        // 確認輸入是否合法
        public bool IsValidInput(string x1, string y1, string x2, string y2)
        {
            return int.TryParse(x1, out _) && int.TryParse(y1, out _) && int.TryParse(x2, out _) && int.TryParse(y2, out _);
        }
    }
}

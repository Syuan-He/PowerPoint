using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint
{
    public static class ErrorMessage
    {
        public const string SHAPE = "Not Expect Shape";
        public const string INDEX = "Not Expect Integer Index";
        public const string NEGATIVE_INDEX_SHAPE = "shape 的選取 index 小於預期(-1)";
        public const string INSERT_LOSS_SHAPES = "在插入頁面時，遺失page資料(shapes)";
        public const string INSERT_LOSS_SHAPE = "在插入 shape 時，遺失 shape";
    }
}

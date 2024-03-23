using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PowerPoint
{
    public static class DataString
    {
        //public const string TEST_BASE = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
        public const string BASE_URL = "../../../PowerPoint/";
        public const string APPLICATION_NAME = "test Drive API";
        public const string CLIENT_SECRET = "PowerPoint/clientSecret.json";
        public const string FILE_NAME = "pptTemp.json";
        public const string CONTENT_TYPE = "application/json";
        public const string LINE_PROPERTY = "IsLine";
        public const string RECTANGLE_PROPERTY = "IsRectangle";
        public const string CIRCLE_PROPERTY = "IsCircle";
        public const string POINTER_PROPERTY = "IsPointer";
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class DoubleBufferedPanelTests
    {
        // 測試建構式
        [TestMethod]
        public void TestConstructor()
        {
            var panel = new DoubleBufferedPanel();

            // 使用反射來獲取 DoubleBuffered 屬性的值
            var doubleBufferedProperty = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            var doubleBufferedValue = (bool)doubleBufferedProperty.GetValue(panel);

            Assert.IsTrue(doubleBufferedValue);
        }
    }
}
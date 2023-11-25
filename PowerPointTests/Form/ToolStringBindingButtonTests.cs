using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace PowerPoint.Tests
{
    [TestClass]
    public class ToolStringBindingButtonTests
    {
        // Test BindingContext get, set
        [TestMethod]
        public void TestBindingContext()
        {
            ToolStripBindingButton button = new ToolStripBindingButton();
            BindingContext context = new BindingContext();

            button.BindingContext = context;

            Assert.AreEqual(context, button.BindingContext);
        }

        // Test DataBindings get
        [TestMethod]
        public void TestDataBindings()
        {
            ToolStripBindingButton button = new ToolStripBindingButton();

            Assert.IsNotNull(button.DataBindings);
        }
    }
}

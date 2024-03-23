using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint.Tests
{
    [TestClass()]
    public class PresentationModel2Tests
    {
        // Test IsValidInput
        [TestMethod()]
        [DataRow("", "", "", "", false)]
        [DataRow("1", "2", "3", "4", true)]
        [DataRow("1.", "2", "3", "4", false)]
        [DataRow("1", "2.", "4", "5", false)]
        [DataRow("1", "2", "3.", "4", false)]
        [DataRow("1", "2", "2", "3.", false)]
        public void TestIsValidInput(string x1, string y1, string x2, string y2, bool ans)
        {
            PresentationModel2 pModel = new PresentationModel2();
            Assert.AreEqual(ans, pModel.IsValidInput(x1, y1, x2, y2));
        }
    }
}
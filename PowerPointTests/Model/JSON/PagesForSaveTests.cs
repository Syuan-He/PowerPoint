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
    public class PagesForSaveTests
    {
        // Test PagesForSave
        [TestMethod()]
        public void TestPagesForSave()
        {
            List<Shapes> pages = new List<Shapes>();
            pages.Add(new Shapes(new Factory(new Random())));
            PagesForSave save = new PagesForSave(pages);
            Assert.IsNotNull(save.PagesList);
        }

        // Test PagesForSave1
        [TestMethod()]
        public void TestPagesForSave1()
        {
            PagesForSave save = new PagesForSave();
        }

        // Test Turn2Pages
        [TestMethod()]
        public void TestTurn2Pages()
        {
            List<Shapes> pages = new List<Shapes>();
            pages.Add(new Shapes(new Factory(new Random())));
            PagesForSave save = new PagesForSave(pages);
            Assert.AreEqual(save.Turn2Pages().Count, pages.Count);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;
using System.Windows.Automation;
using System.Windows;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Windows.Input;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;

namespace PowerPoint.UITest
{
    public class Robot
    {
        private WindowsDriver<WindowsElement> _driver;
        private Dictionary<string, string> _windowHandles;
        private string _root;
        private const string CONTROL_NOT_FOUND_EXCEPTION = "The specific control is not found!!";
        private const string WIN_APP_DRIVER_URI = "http://127.0.0.1:4723";
        private const float RELATIVE_WIDTH = 1920f;

        // constructor
        public Robot(string targetAppPath, string root)
        {
            this.Initialize(targetAppPath, root);
        }

        // initialize
        public void Initialize(string targetAppPath, string root)
        {
            _root = root;
            var options = new AppiumOptions();
            options.AddAdditionalCapability("app", targetAppPath);
            //options.AddAdditionalCapability("ms:waitForAppLaunch", $"{3}");
            //options.AddAdditionalCapability("ms:experimental-webdriver", false);
            options.AddAdditionalCapability("deviceName", "WindowsPC");

            _driver = new WindowsDriver<WindowsElement>(new Uri(WIN_APP_DRIVER_URI), options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _windowHandles = new Dictionary<string, string>
            {
                { _root, _driver.CurrentWindowHandle }
            };
        }

        // clean up
        public void CleanUp()
        {
            SwitchTo(_root);
            _driver.CloseApp();
            _driver.Dispose();
        }

        // test
        public void SwitchTo(string formName)
        {
            if (_windowHandles.ContainsKey(formName))
            {
                _driver.SwitchTo().Window(_windowHandles[formName]);
            }
            else
            {
                foreach (var windowHandle in _driver.WindowHandles)
                {
                    _driver.SwitchTo().Window(windowHandle);
                    try
                    {
                        _driver.FindElementByAccessibilityId(formName);
                        _windowHandles.Add(formName, windowHandle);
                        return;
                    }
                    catch
                    {

                    }
                }
            }
        }

        // 取得畫布的寬
        public int GetPanelWidth(string name)
        {
            return _driver.FindElementByAccessibilityId(name).Size.Width;
        }

        // 取得畫布的高
        public int GetPanelHeight(string name)
        {
            return _driver.FindElementByAccessibilityId(name).Size.Height;
        }

        // 取得 DataGridView 的資料
        public string GetGridViewData(string name, int rowIndex, string columnName)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            return dataGridView.FindElementByName(String.Format("{0} 資料列 {1}", columnName, rowIndex)).Text;
        }

        // test
        public void ClickButton(string name)
        {
            _driver.FindElementByName(name).Click();
        }

        // 滑鼠拖曳
        public void DragMouse(int x1, int y1, int x2, int y2)
        {
            Actions actions = new Actions(_driver);
            var element = _driver.FindElementByAccessibilityId("_panel");
            int tfX1 = x1;
            int tfY1 = y1;
            int tfX2 = x2 - tfX1;
            int tfY2 = y2 - tfY1;
            actions.MoveToElement(element).MoveByOffset(-(element.Size.Width / 2) + tfX1, -(element.Size.Height / 2) + tfY1).ClickAndHold().Perform();
            Thread.Sleep(100);
            actions.MoveByOffset(tfX2, tfY2).Perform();
            actions.Release().Perform();
        }

        // 拖曳 Spliter
        public void DragSpliter(string name, int x2)
        {
            Actions actions = new Actions(_driver);
            var element = _driver.FindElementByAccessibilityId(name);
            Assert.IsNotNull(element);
            actions.MoveToElement(element).MoveByOffset(2, 0).ClickAndHold().Perform();
            Thread.Sleep(300);
            actions.MoveByOffset(x2, 0).Perform();
            Thread.Sleep(300);
            actions.Release().Perform();
        }
        
        // test
        public void Click(int x1, int y1)
        {
            Actions actions = new Actions(_driver);
            var element = _driver.FindElementByAccessibilityId("_panel");
            actions.MoveToElement(element).MoveByOffset(-(element.Size.Width / 2) + x1, -(element.Size.Height / 2) + y1).Click().Perform();
        }

        // test
        public void CloseWindow()
        {
            SendKeys.SendWait("%{F4}");
        }

        // 按鍵盤
        public void SendInput(string key)
        {
            SendKeys.SendWait(key);
        }

        // 按鍵盤
        public void SendInput(int key)
        {
            SendKeys.SendWait(String.Format("{0}", key));
        }

        // test
        public void CloseMessageBox()
        {
            _driver.FindElementByName("確定").Click();
        }

        // test
        public void ClickDataGridViewCellBy(string name, int rowIndex, string columnName)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            _driver.FindElementByName(String.Format("{0} 資料列 {1}", columnName, rowIndex)).Click();
        }

        // test
        public void AssertEnable(string name, bool state)
        {
            WindowsElement element = _driver.FindElementByName(name);
            Assert.AreEqual(state, element.Enabled);
        }

        // test
        public void AssertText(string name, string text)
        {
            WindowsElement element = _driver.FindElementByAccessibilityId(name);
            Assert.AreEqual(text, element.Text);
        }

        // test
        public void AssertDataGridViewRowDataBy(string name, int rowIndex, string[] data)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            var rowDatas = dataGridView.FindElementByName(String.Format("資料列 {0}", rowIndex)).FindElementsByXPath("//*");

            // FindElementsByXPath("//*") 會把 "row" node 也抓出來，因此 i 要從 1 開始以跳過 "row" node
            for (int i = 1; i < rowDatas.Count; i++)
            {
                if (data[i-1] != null)
                    Assert.AreEqual(data[i - 1], rowDatas[i].Text.Replace("(null)", ""));
            }
        }

        // test
        public void AssertDataGridViewRowCountBy(string name, int rowCount)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            Point point = new Point(dataGridView.Location.X, dataGridView.Location.Y);
            AutomationElement element = AutomationElement.FromPoint(point);

            while (element != null && element.Current.LocalizedControlType.Contains("datagrid") == false)
            {
                element = TreeWalker.RawViewWalker.GetParent(element);
            }

            if (element != null)
            {
                GridPattern gridPattern = element.GetCurrentPattern(GridPattern.Pattern) as GridPattern;

                if (gridPattern != null)
                {
                    Assert.AreEqual(rowCount, gridPattern.Current.RowCount);
                }
            }
        }

        // test
        public void AssertDataGridViewHasTheRow(string name, int rowCount)
        {
            var dataGridView = _driver.FindElementByAccessibilityId(name);
            var rowDatas = dataGridView.FindElementByName(String.Format("資料列 {0}", rowCount - 1)).FindElementsByXPath("//*");
            Assert.IsNotNull(rowDatas);
        }

        // assert 長寬比
        public void AssertHeightWidthRate(string name)
        {
            var element = _driver.FindElementByAccessibilityId(name);
            Assert.IsTrue(Math.Abs(element.Size.Width / 16 - element.Size.Height / 9) < 1);
        }

        // assert 
        public void AssertElementsCount(string name, int count)
        {
            var element = _driver.FindElementsByAccessibilityId(name);
            Assert.AreEqual(element.Count, count);
        }
    }
}

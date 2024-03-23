using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace PowerPoint.UITest
{
    [TestClass]
    public class MainFormUITest
    {
        private const string MENU_FORM = "MainForm";
        private const float RELATIVE_WIDTH = 1920f;
        private const string GRID_VIEW_NAME = "_infoDataGridView";
        private const string COLUMN_DELETE = "刪除";
        private const string COLUMN_INFO = "資訊";
        private const string ADD_PAGE_BUTTON = "AddPage";
        private const string LINE_BUTTON = "DrawLine";
        private const string RECTANGLE_BUTTON = "DrawRectangle";
        private const string CIRCLE_BUTTON = "DrawCircle";
        private const string UNDO_BUTTON = "UndoButton";
        private const string SAVE_BUTTON = "save";
        private const string LOAD_BUTTON = "load";
        private const string PAGE = "_page";
        private const string LINE = "線";
        private const string CIRCLE = "圓圈";
        private const string RECTANGLE = "矩形";
        string solutionPath;
        Robot _robot;
        
        // initial
        [TestInitialize]
        public void Initialize()
        {
            var projectName = "PowerPoint";
            solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", projectName+".exe");
            _robot = new Robot(targetAppPath, MENU_FORM);
        }

        // Test CleanUp
        [TestCleanup]
        public void TestCleanUp()
        {
            _robot.CleanUp();
        }

        // Test DrawShape
        [TestMethod]
        [DataRow(LINE_BUTTON, 18, 18, 91, 91, 0)]
        [DataRow(RECTANGLE_BUTTON, 18, 18, 91, 91, 0)]
        [DataRow(CIRCLE_BUTTON, 18, 18, 91, 91, 0)]
        public void DrawShape(string buttonName, int x1, int y1, int x2, int y2, int index)
        {
            _robot.ClickButton(buttonName);
            _robot.AssertEnable(buttonName, true);
            _robot.DragMouse(x1, y1, x2, y2);
            string type;
            switch (buttonName)
            {
                case LINE_BUTTON:
                    type = LINE;
                    break;
                case RECTANGLE_BUTTON:
                    type = RECTANGLE;
                    break;
                case CIRCLE_BUTTON:
                    type = CIRCLE;
                    break;
                default:
                    type = LINE;
                    break;
            }
            string[] data = { COLUMN_DELETE, type, null };
            _robot.AssertDataGridViewRowDataBy(GRID_VIEW_NAME, index, data);
        }

        // Test DrawShape Undo and Redo
        [TestMethod]
        [DataRow(LINE_BUTTON, 18, 18, 91, 91)]
        [DataRow(RECTANGLE_BUTTON, 18, 18, 91, 91)]
        [DataRow(CIRCLE_BUTTON, 18, 18, 91, 91)]
        public void DrawShapeUndoAndRedo(string buttonName, int x1, int y1, int x2, int y2)
        {
            DrawShape(buttonName, x1, y1, x2, y2, 0);
            _robot.ClickButton(UNDO_BUTTON);
            _robot.AssertDataGridViewRowCountBy(GRID_VIEW_NAME, 0);
            _robot.ClickButton("RedoButton");
            _robot.AssertDataGridViewHasTheRow(GRID_VIEW_NAME, 1);
        }

        // Test ScaleShape Undo and Redo
        [TestMethod]
        public void ScaleShapeUndoAndRedo()
        {
            DrawShape(LINE_BUTTON, 90, 70, 100, 160, 0);
            string location1 = _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO);
            _robot.Click(95, 115);
            _robot.DragMouse(100, 160, 112, 120);
            string location2 = _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO);
            Assert.AreNotEqual(location1, location2);
            _robot.ClickButton(UNDO_BUTTON);
            Assert.AreEqual(location1, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
            _robot.ClickButton("RedoButton");
            Assert.AreEqual(location2, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
        }

        // Use Add Shape button
        void AddShape(string shapeName, int x1, int y1, int x2, int y2, int index)
        {
            _robot.ClickButton("_shapeComboBox");
            _robot.SendInput("%{DOWN}");
            _robot.ClickButton(shapeName);
            _robot.ClickButton("新增");
            Thread.Sleep(1000);
            _robot.SendInput(x1);
            _robot.SendInput("{TAB}");
            _robot.SendInput(y1);
            _robot.SendInput("{TAB}");
            _robot.SendInput(x2);
            _robot.SendInput("{TAB}");
            _robot.SendInput(y2);
            _robot.SendInput("{TAB}");
            _robot.SendInput("~");
            string[] data = { COLUMN_DELETE, shapeName, string.Format("({0}, {1}), ({2}, {3})", x1, y1, x2, y2) };
            _robot.AssertDataGridViewRowDataBy(GRID_VIEW_NAME, index, data);
        }
            
        // Test Add Shape button
        [TestMethod]
        public void TestAddShape()
        {
            AddShape(LINE, 100, 100, 500, 500, 0);
            AddShape(RECTANGLE, 100, 100, 500, 500, 1);
            AddShape(CIRCLE, 100, 100, 500, 500, 2);
        }

        // Test AddShape Undo and Redo
        [TestMethod]
        [DataRow(LINE, 100, 100, 500, 500)]
        [DataRow(RECTANGLE, 100, 100, 500, 500)]
        [DataRow(CIRCLE, 100, 100, 500, 500)]
        public void AddShapeUndoAndRedo(string buttonName, int x1, int y1, int x2, int y2)
        {
            AddShape(buttonName, x1, y1, x2, y2, 0);
            _robot.ClickButton(UNDO_BUTTON);
            _robot.AssertDataGridViewRowCountBy(GRID_VIEW_NAME, 0);
            _robot.ClickButton("RedoButton");
            _robot.AssertDataGridViewHasTheRow(GRID_VIEW_NAME, 1);
            _robot.ClickDataGridViewCellBy(GRID_VIEW_NAME, 0, COLUMN_DELETE);
            _robot.ClickButton(UNDO_BUTTON);
            _robot.AssertDataGridViewHasTheRow(GRID_VIEW_NAME, 1);
            _robot.ClickButton("RedoButton");
            _robot.AssertDataGridViewRowCountBy(GRID_VIEW_NAME, 0);
        }

        // use move shape
        void MoveShape(int index, int x1, int y1, int x2, int y2)
        {
            string location1 = _robot.GetGridViewData(GRID_VIEW_NAME, index, COLUMN_INFO);
            _robot.DragMouse(x1, y1, x2, y2);
            Assert.AreNotEqual(location1, _robot.GetGridViewData(GRID_VIEW_NAME, index, COLUMN_INFO));
        }

        // Test MoveShape
        [TestMethod]
        public void TestMoveShape()
        {
            DrawShape(LINE_BUTTON, 0, 0, 100, 100, 0);
            MoveShape(0, 50, 50, 100, 50);
        }

        // Test MoveShape Undo And Redo
        [TestMethod]
        public void MoveShapeUndoAndRedo()
        {
            TestMoveShape();
            string location1 = _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO);
            _robot.ClickButton(UNDO_BUTTON);
            Assert.AreNotEqual(location1, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
            _robot.ClickButton("RedoButton");
            Assert.AreEqual(location1, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
        }

        // Test MoveSpliter
        [TestMethod]
        [DataRow(-30)]
        public void MoveSpliter(int x1)
        {
            _robot.DragSpliter("_splitContainer2", x1);
            _robot.AssertHeightWidthRate("_panel");
            _robot.AssertHeightWidthRate(PAGE);
        }

        // Test AddPage
        [TestMethod]
        [DataRow(2)]
        public void AddPage(int count)
        {
            _robot.ClickButton(ADD_PAGE_BUTTON);
            _robot.AssertElementsCount(PAGE, count);
        }

        // Test AddPage Undo And Redo
        [TestMethod]
        public void AddPageUndoAndRedo()
        {
            AddPage(2);
            _robot.ClickButton(UNDO_BUTTON);
            _robot.AssertElementsCount(PAGE, 1);
            _robot.ClickButton("RedoButton");
            _robot.AssertElementsCount(PAGE, 2);
        }

        // Test Save and Load
        [TestMethod]
        public void SaveAndLoad()
        {
            DrawShape(LINE_BUTTON, 12, 70, 70, 15, 0);
            string info = _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO);
            string type = _robot.GetGridViewData(GRID_VIEW_NAME, 0, "形狀");
            _robot.ClickButton(SAVE_BUTTON);
            _robot.ClickButton("是(Y)");
            _robot.AssertEnable(SAVE_BUTTON, false);
            DrawShape(CIRCLE_BUTTON, 100, 100, 200, 200, 1);
            string info2 = _robot.GetGridViewData(GRID_VIEW_NAME, 1, COLUMN_INFO);
            Thread.Sleep(3000);
            _robot.AssertEnable(SAVE_BUTTON, true);
            _robot.ClickDataGridViewCellBy(GRID_VIEW_NAME, 0, COLUMN_DELETE);
            _robot.ClickButton(LOAD_BUTTON);
            _robot.ClickButton("是(Y)");
            _robot.DragMouse(145, 145, 200, 145);
            Assert.AreEqual(info2, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
            Thread.Sleep(3000);
            Assert.AreEqual(info, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
            Assert.AreEqual(type, _robot.GetGridViewData(GRID_VIEW_NAME, 0, "形狀"));
        }

        // 整合測試
        [TestMethod]
        public void TestIntegration()
        {
            SaveAndLoad();
            _robot.ClickDataGridViewCellBy(GRID_VIEW_NAME, 0, COLUMN_DELETE);
            DoYouWantToBuildASnowman();
            AddPageUndoAndRedo();
            AddPage(3);
            _robot.SendInput("{DELETE}");
            TestAddShape();
            AddPage(3);
            TestMoveShape();
            AddPage(4);
            ScaleShapeUndoAndRedo();
            AddPage(5);
            _robot.SendInput("{DELETE}");
            _robot.SendInput("{DELETE}");
            _robot.SendInput("{DELETE}");
            _robot.SendInput("{DELETE}");
            AddPage(2);
            DrawLandscape();
            MoveSpliter(-20);
            Thread.Sleep(1000);
        }

        // 用 robot 畫一隻雪人
        void DoYouWantToBuildASnowman()
        {
            DrawShapeUndoAndRedo(CIRCLE_BUTTON, 100, 20, 150, 70);
            MoveShape(0, 125, 45, 160, 45);
            DrawShape(CIRCLE_BUTTON, 100, 70, 200, 170, 1);
            MoveShape(1, 150, 135, 162, 135);
            DrawShape(RECTANGLE_BUTTON, 138, 5, 182, 36, 2);
            DrawShape(LINE_BUTTON, 130, 36, 190, 36, 3);
            MoveShape(2, 155, 20, 155, 18);
            string hatLocation = _robot.GetGridViewData(GRID_VIEW_NAME, 2, COLUMN_INFO);
            _robot.ClickButton(UNDO_BUTTON);
            Assert.AreNotEqual(hatLocation, _robot.GetGridViewData(GRID_VIEW_NAME, 0, COLUMN_INFO));
            DrawShape(LINE_BUTTON, 90, 70, 100, 160, 4);
            _robot.Click(95, 115);
            _robot.DragMouse(100, 160, 112, 120);
            DrawShape(LINE_BUTTON, 212, 120, 224, 70, 5);
        }

        // 畫山景
        void DrawLandscape()
        {
            AddShape(LINE, 320, 1080, 1280, 0, 0);
            AddShape(LINE, 1280, 0, 1800, 1080, 1);
            AddShape(CIRCLE, -120, -120, 240, 240, 2);
            AddShape(RECTANGLE, 780, 200, 1480, 540, 3);
            DrawShape(RECTANGLE_BUTTON, 60, 90, 180, 150, 4);
        }
    }
}

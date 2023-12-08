using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint
{
    public partial class Form1 : Form
    {
        private const string LINE_PROPERTY = "IsLine";
        private const string RECTANGLE_PROPERTY = "IsRectangle";
        private const string CIRCLE_PROPERTY = "IsCircle";
        private const string POINTER_PROPERTY = "IsPointer";
        private const string CHECKED_PROPERTY = "Checked";
        private const float ASPECT_RATIO = 0.5625f;
        Model _model;
        PresentationModel _presentationModel;

        public Form1(PresentationModel presentationModel, Model model)
        {
            _model = model;
            _presentationModel = presentationModel;
            InitializeComponent();
            _model._panelChanged += HandlePanelChanged;

            _panel.Width = _splitContainer2.Panel1.Width;
            _panel.Height = GetPanelHeight(_panel.Width);
            _slide1.Width = _splitContainer1.Panel1.Width;
            _slide1.Height = GetPanelHeight(_slide1.Width);
            _lineToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, LINE_PROPERTY);
            _rectangleToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, RECTANGLE_PROPERTY);
            _circleToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, CIRCLE_PROPERTY);
            _pointerToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, POINTER_PROPERTY);
            _infoDataGridView.DataSource = _model.GetInfoDataGridView();
            _infoDataGridView.Columns[1].HeaderText = "形狀";
            _infoDataGridView.Columns[2].HeaderText = "資訊";
            _slide1.Paint += PaintSlide;
        }

        // 資訊顯示的新增按鍵
        void ClickAddButton(object sender, EventArgs e)
        {
            _model.PressInfoAdd(_shapeComboBox.Text);
            RefreshCommandButton();
        }

        // 資訊顯示的刪除按鍵
        void ClickInfoDataGridViewCellContent(object sender, DataGridViewCellEventArgs e)
        {
            _model.PressDelete(e.ColumnIndex, e.RowIndex);
            RefreshCommandButton();
        }
        
        // ToolStrip 的 Line 按鈕
        void ClickLineToolStripButton(object sender, EventArgs e)
        {
            _presentationModel.PressLineButton();
        }

        // ToolStrip 的 Rectangle 按鈕
        void ClickRectangleToolStripButton(object sender, EventArgs e)
        {
            _presentationModel.PressRectangleButton();
        }

        // ToolStrip 的 Circle 按鈕
        void ClickCircleToolStripButton(object sender, EventArgs e)
        {
            _presentationModel.PressCircleButton();
        }

        // ToolStrip 的 Pointer 按鈕
        private void ClickPointerToolStripButton(object sender, EventArgs e)
        {
            _presentationModel.PressPointerButton();
        }

        // 畫出所有在 list 的圖形
        void PaintPanel(object sender, PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics, _panel.Width);
        }

        // 在小畫面畫出所有在 list 的縮圖
        void PaintSlide(object sender, PaintEventArgs e)
        {
            _presentationModel.DrawSlide(e.Graphics, _slide1.Width);
        }

        // 通知 panel modelChange (observer's function)
        public void HandlePanelChanged()
        {
            _panel.Invalidate(true);
            _slide1.Invalidate(true);
        }

        // 鼠標進入 panel
        public void HandleCanvasEnter(object sender, EventArgs e)
        {
            Cursor = _presentationModel.GetPointerShape();
        }

        // 鼠標離開 panel
        public void HandleCanvasLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        // 滑鼠在畫布上按住左鍵
        public void HandleCanvasPressed(object sender, MouseEventArgs e)
        {
            _presentationModel.PressPointer(e.X, e.Y, _panel.Width);
        }

        // 放掉滑鼠左鍵
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            _presentationModel.ReleasePointer(e.X, e.Y, _panel.Width);
            Cursor = _presentationModel.GetPointerShape();
            RefreshCommandButton();
        }

        // 滑鼠移動
        public void HandleCanvasMoved(object sender, MouseEventArgs e)
        {
            _presentationModel.MovePointer(e.X, e.Y, _panel.Width);
            Cursor = _presentationModel.GetPointerShape(e.X, e.Y, _panel.Width);
        }

        // 按下鍵盤
        private void PressKey(object sender, KeyEventArgs e)
        {
            _presentationModel.PressDelete(e.KeyCode);
            RefreshCommandButton();
        }

        // 移動大小畫面間的分割線
        private void MovedSplit1(object sender, SplitterEventArgs e)
        {
            _slide1.Width = e.X;
            _slide1.Height = GetPanelHeight(_slide1.Width);
            _panel.Invalidate(true);
            _slide1.Invalidate(true);
        }

        // 移動畫面與 DataGrideView 間的分割線
        private void MovedSplit2(object sender, SplitterEventArgs e)
        {
            _panel.Width = e.X;
            _panel.Height = GetPanelHeight(_panel.Width);
            _panel.Invalidate(true);
        }

        // 按下 ToolStrip 的 undo 按鈕
        private void ClickUndoToolStripButton(object sender, EventArgs e)
        {
            _model.PressUndo();
            RefreshCommandButton();
        }

        // 按下 ToolStrip 的 undo 按鈕
        private void ClickRedoToolStripButton(object sender, EventArgs e)
        {
            _model.PressRedo();
            RefreshCommandButton();
        }

        // 更新redo與undo是否為enabled
        void RefreshCommandButton()
        {
            _redoToolStripButton.Enabled = _model.IsRedoEnabled;
            _undoToolStripButton.Enabled = _model.IsUndoEnabled;
            Invalidate();
        }

        // 換算寬度
        int GetPanelHeight(int width)
        {
            return (int)(width * ASPECT_RATIO);
        }
    }
}

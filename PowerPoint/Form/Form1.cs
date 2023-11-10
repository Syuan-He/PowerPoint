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
        Model _model;
        PresentationModel _presentationModel;
        Panel _panel = new DoubleBufferedPanel();

        public Form1(PresentationModel presentationModel, Model model)
        {
            InitializeComponent();
            _model = model;
            _presentationModel = presentationModel;
            _model._modelChanged += HandlePanelChanged;
            _model._shapesChanged += HandleGridViewChanged;

            _panel.BackColor = System.Drawing.SystemColors.Window;
            _panel.Location = new System.Drawing.Point(150, 48);
            _panel.Name = "_panel";
            _panel.Size = new System.Drawing.Size(711, 569);
            _panel.TabIndex = 6;
            _panel.MouseEnter += HandleCanvasEnter;
            _panel.MouseLeave += HandleCanvasLeave;
            _panel.MouseDown += HandleCanvasPressed;
            _panel.MouseUp += HandleCanvasReleased;
            _panel.MouseMove += HandleCanvasMoved;
            _panel.Paint += PaintPanel;
            Controls.Add(_panel);

            _lineToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, LINE_PROPERTY);
            _rectangleToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, RECTANGLE_PROPERTY);
            _circleToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, CIRCLE_PROPERTY);
            _pointerToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, POINTER_PROPERTY);

        }

        // 資訊顯示的新增按鍵
        void ClickAddButton(object sender, EventArgs e)
        {
            _model.PressInfoAdd(_shapeComboBox.Text);
            //_presentationModel.ReleasePointer();
            //ShowClickToolStripButton();
        }

        // 資訊顯示的刪除按鍵
        void ClickInfoDataGridViewCellContent(object sender, DataGridViewCellEventArgs e)
        {
            _model.PressDelete(e.ColumnIndex, e.RowIndex);
            //_presentationModel.ReleasePointer();
            //ShowClickToolStripButton();
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
            _presentationModel.Draw(e.Graphics);
        }

        // 通知 panel modelChange (observer's function)
        public void HandlePanelChanged()
        {
            _panel.Invalidate(true);
        }

        // 通知 DataGridView modelChange (observer's function)
        public void HandleGridViewChanged()
        {
            _infoDataGridView.DataSource = _model.GetInfoDataGridView();
            _panel.Invalidate(true);
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
            _presentationModel.PressPointer(e.X, e.Y);
        }

        // 放掉滑鼠左鍵
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            Cursor = _presentationModel.GetPointerShape();
            _model.ReleasePointer(e.X, e.Y);
            _presentationModel.ReleasePointer();
        }

        // 滑鼠移動
        public void HandleCanvasMoved(object sender, MouseEventArgs e)
        {
            _model.MovePointer(e.X, e.Y);
        }
    }
}

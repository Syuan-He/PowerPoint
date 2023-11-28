﻿using System;
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

        public Form1(PresentationModel presentationModel, Model model)
        {
            InitializeComponent();
            _model = model;
            _presentationModel = presentationModel;
            _model._panelChanged += HandlePanelChanged;

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
            _model.PressInfoAdd(_shapeComboBox.Text, _panel.Width, _panel.Height);
        }

        // 資訊顯示的刪除按鍵
        void ClickInfoDataGridViewCellContent(object sender, DataGridViewCellEventArgs e)
        {
            _model.PressDelete(e.ColumnIndex, e.RowIndex);
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

        // 在小畫面畫出所有在 list 的縮圖
        void PaintSlide(object sender, PaintEventArgs e)
        {
            _presentationModel.DrawSlide(e.Graphics, _panel.Size, _slide1.Size);
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
            _presentationModel.PressPointer(e.X, e.Y);
        }

        // 放掉滑鼠左鍵
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            _model.ReleasePointer(e.X, e.Y);
            _presentationModel.PressPointerButton();
            Cursor = _presentationModel.GetPointerShape();
        }

        // 滑鼠移動
        public void HandleCanvasMoved(object sender, MouseEventArgs e)
        {
            Cursor = _presentationModel.GetPointerShape(e.X, e.Y);
            _model.MovePointer(e.X, e.Y);
        }

        // 按下鍵盤
        private void PressKey(object sender, KeyEventArgs e)
        {
            _presentationModel.PressDelete(e.KeyCode);
        }
    }
}

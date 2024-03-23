using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint
{
    public partial class Form1 : Form
    {
        private const string CHECKED_PROPERTY = "Checked";
        private const int SLIDE_BORDER = 3;
        private const string CAPTION = "訊息";
        Model _model;
        PresentationModel _presentationModel;
        List<Button> _slides = new List<Button>();

        public Form1(PresentationModel presentationModel, Model model)
        {
            _model = model;
            _presentationModel = presentationModel;
            InitializeComponent();
            _model._panelChanged += HandlePanelChanged;
            _model._slideRemoveAt += HandleSlideRemoveAt;
            _model._slideInsert += HandleSlideInsert;
            presentationModel.SetPanelSize(_splitContainer2.Panel1.Width, _splitContainer2.Panel1.Height);
            _panel.Width = presentationModel.PanelWidth;
            _panel.Height = presentationModel.PanelHeight;
            _page.Width = _splitContainer1.Panel1.Width - SLIDE_BORDER;
            _page.Height = _presentationModel.GetPanelHeight(_page.Width);
            _lineToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, DataString.LINE_PROPERTY);
            _rectangleToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, DataString.RECTANGLE_PROPERTY);
            _circleToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, DataString.CIRCLE_PROPERTY);
            _pointerToolStripButton.DataBindings.Add(CHECKED_PROPERTY, presentationModel, DataString.POINTER_PROPERTY);
            _infoDataGridView.DataSource = _model.GetInfoDataGridView();
            _infoDataGridView.Columns[1].HeaderText = "形狀";
            _infoDataGridView.Columns[2].HeaderText = "資訊";
            _page.Paint += PaintSlide;
            _slides.Add(_page);
        }

        // 資訊顯示的新增按鍵
        void ClickAddButton(object sender, EventArgs e)
        {
            Form addForm = new Form2(this, new PresentationModel2());
            addForm.Show();
            
        }

        // 執行資訊顯示的新增
        public void AddShape(int x1, int y1, int x2, int y2)
        {
            _model.PressInfoAdd(_shapeComboBox.Text, new Coordinate(x1, y1), new Coordinate(x2, y2));
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
            _presentationModel.DrawSlide(e.Graphics, _slides[0].Width, _slides.IndexOf((Button)sender));
        }

        // 通知 panel modelChange (observer's function)
        public void HandlePanelChanged()
        {
            _panel.Invalidate(true);
            foreach (Button item in _slides)
            {
                item.Invalidate(true);
            }
        }

        // 通知 slides modelChange (observer's function)
        public void HandleSlideRemoveAt(int index)
        {
            _splitContainer1.Panel1.Controls.Remove(_slides[index]);
            _slides.RemoveAt(index);
            RefreshSlidesLocal();
            _slides[_model.PageIndex].FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            _infoDataGridView.DataSource = _model.GetInfoDataGridView();
        }

        // 通知 slides modelChange (observer's function)
        public void HandleSlideInsert(int index)
        {
            _slides.Insert(index, CreateNewSlide());
            _splitContainer1.Panel1.Controls.Add(_slides[index]);
            RefreshSlidesLocal();
        }

        // 刷新 slides
        public void RefreshSlide()
        {
            _splitContainer1.Panel1.Controls.Clear();
            _slides.Clear();
            for (int i = 0; i < _model.PagesCount; i++)
            {
                Button button = CreateNewSlide();
                _slides.Add(button);
                _splitContainer1.Panel1.Controls.Add(button);
            }
            //RefreshSlidesLocal();
            _slides[_model.PageIndex].FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            _infoDataGridView.DataSource = _model.GetInfoDataGridView();
            _panel.Invalidate(true);
        }

        // 刷新 Slide 的排序位子
        void RefreshSlidesLocal()
        {
            int height = _slides[0].Height;
            for (int i = 0; i < _slides.Count; i++)
            {
                _slides[i].Location = new Point(0, (height + SLIDE_BORDER) * i);
                _slides[i].Invalidate(true);
            }
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
            int width = e.X - SLIDE_BORDER;
            int height = _presentationModel.GetPanelHeight(width);
            for (int i = 0; i < _slides.Count; i++)
            {
                _slides[i].Width = width;
                _slides[i].Height = height;
                _slides[i].Location = new Point(0, (height + SLIDE_BORDER) * i);
                _slides[i].Invalidate(true);
            }
            _panel.Invalidate(true);
        }

        // 按下 ToolStrip 的 undo 按鈕
        private void ClickUndoToolStripButton(object sender, EventArgs e)
        {
            _model.PressUndo();
            RefreshCommandButton();
        }

        // 按下 ToolStrip 的 redo 按鈕
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

        // 按下加 Slide(Page) 的按鈕
        private void ClickAddPageButton(object sender, EventArgs e)
        {
            _slides[_model.PageIndex].FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            _model.PressAddPage();
            SwitchSlide();
            RefreshCommandButton();
        }

        // 創造新的 Slide
        Button CreateNewSlide()
        {
            Button button = new Button();
            button.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            button.BackColor = SystemColors.ButtonHighlight;
            button.FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            button.FlatAppearance.MouseOverBackColor = SystemColors.GradientActiveCaption;
            button.FlatStyle = FlatStyle.Flat;
            button.Name = "_page";
            button.Width = _splitContainer1.Panel1.Width - SLIDE_BORDER;
            button.Height = _presentationModel.GetPanelHeight(button.Width);
            button.Location = new Point(0, (button.Height + SLIDE_BORDER) * _slides.Count);
            button.TabIndex = 3;
            button.UseVisualStyleBackColor = true;
            button.Click += new EventHandler(ClickSlide);
            button.Paint += PaintSlide;
            return button;
        }

        // 按在 Slide(Page) 上
        private void ClickSlide(object sender, EventArgs e)
        {
            Debug.Assert(_model.PageIndex < _slides.Count, "Pages 的 Index 大於索引");
            _slides[_model.PageIndex].FlatAppearance.BorderColor = SystemColors.ButtonShadow;
            _model.PageIndex = _slides.IndexOf((Button)sender);
            SwitchSlide();
        }

        // 切換完 page 後要做的動作
        void SwitchSlide()
        {
            _slides[_model.PageIndex].FlatAppearance.BorderColor = SystemColors.MenuHighlight;
            _panel.Invalidate(true);
            _infoDataGridView.DataSource = _model.GetInfoDataGridView();
        }

        // 按下 Save
        private async void ClickSaveButton(object sender, EventArgs e)
        {
            SetSaveLoadButton(false);
            DialogResult result = MessageBox.Show("是否確定要儲存", CAPTION, MessageBoxButtons.YesNo);
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    _model.Save(result == DialogResult.Yes);
                }
                catch (Exception expection)
                {
                    MessageBox.Show(expection.Message);
                }
            });
            SetSaveLoadButton(true);
            SwitchSlide();
            RefreshCommandButton();
        }

        // 設定 save load 的 enable
        void SetSaveLoadButton(bool enable)
        {
            _saveToolStripButton.Enabled = enable;
            _loadToolStripButton.Enabled = enable;
        }

        // 按下 Load
        private void ClickLoadButton(object sender, EventArgs e)
        {
            _saveToolStripButton.Enabled = false;
            _loadToolStripButton.Enabled = false;
            DialogResult result = MessageBox.Show("是否要重新載入", "訊息", MessageBoxButtons.YesNo);
            try
            {
                _model.Load(result == DialogResult.Yes);
            }
            catch (Exception expection)
            {
                MessageBox.Show(expection.Message);
            }
            _saveToolStripButton.Enabled = true;
            _loadToolStripButton.Enabled = true;
            RefreshSlide();
            RefreshCommandButton();
        }
        
        // 畫面大小變更時，調整 panel 的大小與位子
        private void ChangedPanelSize(object sender, EventArgs e)
        {
            _presentationModel.SetPanelSize(_splitContainer2.Panel1.Width, _splitContainer2.Panel1.Height);
            _panel.Width = _presentationModel.PanelWidth;
            _panel.Height = _presentationModel.PanelHeight;
            _panel.Location = new Point(_presentationModel.PanelLocal.X, _presentationModel.PanelLocal.Y);
            _panel.Invalidate(true);
        }
    }
}

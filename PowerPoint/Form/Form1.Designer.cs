
namespace PowerPoint
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._infoDataGridView = new System.Windows.Forms.DataGridView();
            this._delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this._shape = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._information = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._infoGroupBox = new System.Windows.Forms.GroupBox();
            this._addButton = new System.Windows.Forms.Button();
            this._shapeComboBox = new System.Windows.Forms.ComboBox();
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._slide1 = new System.Windows.Forms.Button();
            this._toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._lineToolStripButton = new PowerPoint.ToolStripBindingButton();
            this._rectangleToolStripButton = new PowerPoint.ToolStripBindingButton();
            this._circleToolStripButton = new PowerPoint.ToolStripBindingButton();
            this._pointerToolStripButton = new PowerPoint.ToolStripBindingButton();
            this._undoToolStripButton = new PowerPoint.ToolStripBindingButton();
            this._redoToolStripButton = new PowerPoint.ToolStripBindingButton();
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._panel = new PowerPoint.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this._infoDataGridView)).BeginInit();
            this._infoGroupBox.SuspendLayout();
            this._menuStrip1.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).BeginInit();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).BeginInit();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _infoDataGridView
            // 
            this._infoDataGridView.AllowUserToAddRows = false;
            this._infoDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._infoDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._infoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._infoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._delete});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._infoDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this._infoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoDataGridView.Location = new System.Drawing.Point(3, 96);
            this._infoDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._infoDataGridView.Name = "_infoDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._infoDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this._infoDataGridView.RowHeadersVisible = false;
            this._infoDataGridView.RowHeadersWidth = 62;
            this._infoDataGridView.RowTemplate.Height = 27;
            this._infoDataGridView.Size = new System.Drawing.Size(337, 578);
            this._infoDataGridView.TabIndex = 2;
            this._infoDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ClickInfoDataGridViewCellContent);
            // 
            // _delete
            // 
            this._delete.DataPropertyName = "_delete";
            this._delete.HeaderText = "刪除";
            this._delete.MinimumWidth = 6;
            this._delete.Name = "_delete";
            this._delete.ReadOnly = true;
            this._delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._delete.Text = "刪除";
            this._delete.UseColumnTextForButtonValue = true;
            this._delete.Width = 80;
            // 
            // _shape
            // 
            this._shape.MinimumWidth = 6;
            this._shape.Name = "_shape";
            this._shape.Width = 125;
            // 
            // _information
            // 
            this._information.MinimumWidth = 6;
            this._information.Name = "_information";
            this._information.Width = 125;
            // 
            // _infoGroupBox
            // 
            this._infoGroupBox.AutoSize = true;
            this._infoGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._infoGroupBox.Controls.Add(this._addButton);
            this._infoGroupBox.Controls.Add(this._shapeComboBox);
            this._infoGroupBox.Controls.Add(this._infoDataGridView);
            this._infoGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoGroupBox.Location = new System.Drawing.Point(0, 0);
            this._infoGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._infoGroupBox.Name = "_infoGroupBox";
            this._infoGroupBox.Padding = new System.Windows.Forms.Padding(3, 74, 3, 4);
            this._infoGroupBox.Size = new System.Drawing.Size(343, 678);
            this._infoGroupBox.TabIndex = 10;
            this._infoGroupBox.TabStop = false;
            this._infoGroupBox.Text = "資訊顯示";
            // 
            // _addButton
            // 
            this._addButton.Location = new System.Drawing.Point(6, 22);
            this._addButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(182, 66);
            this._addButton.TabIndex = 2;
            this._addButton.Text = "新增";
            this._addButton.UseVisualStyleBackColor = true;
            this._addButton.Click += new System.EventHandler(this.ClickAddButton);
            // 
            // _shapeComboBox
            // 
            this._shapeComboBox.FormattingEnabled = true;
            this._shapeComboBox.Items.AddRange(new object[] {
            "線",
            "矩形",
            "圓圈"});
            this._shapeComboBox.Location = new System.Drawing.Point(194, 43);
            this._shapeComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._shapeComboBox.Name = "_shapeComboBox";
            this._shapeComboBox.Size = new System.Drawing.Size(145, 26);
            this._shapeComboBox.TabIndex = 1;
            this._shapeComboBox.Text = "線";
            // 
            // _menuStrip1
            // 
            this._menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this._menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._infoToolStripMenuItem});
            this._menuStrip1.Location = new System.Drawing.Point(0, 0);
            this._menuStrip1.Name = "_menuStrip1";
            this._menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this._menuStrip1.Size = new System.Drawing.Size(1303, 31);
            this._menuStrip1.TabIndex = 2;
            this._menuStrip1.Text = "menuStrip1";
            // 
            // _infoToolStripMenuItem
            // 
            this._infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._aboutToolStripMenuItem});
            this._infoToolStripMenuItem.Name = "_infoToolStripMenuItem";
            this._infoToolStripMenuItem.Size = new System.Drawing.Size(62, 27);
            this._infoToolStripMenuItem.Text = "說明";
            // 
            // _aboutToolStripMenuItem
            // 
            this._aboutToolStripMenuItem.Name = "_aboutToolStripMenuItem";
            this._aboutToolStripMenuItem.Size = new System.Drawing.Size(146, 34);
            this._aboutToolStripMenuItem.Text = "關於";
            // 
            // _slide1
            // 
            this._slide1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._slide1.Location = new System.Drawing.Point(0, 0);
            this._slide1.Name = "_slide1";
            this._slide1.Size = new System.Drawing.Size(160, 90);
            this._slide1.TabIndex = 2;
            this._slide1.UseVisualStyleBackColor = true;
            // 
            // _toolStrip1
            // 
            this._toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lineToolStripButton,
            this._rectangleToolStripButton,
            this._circleToolStripButton,
            this._pointerToolStripButton,
            this._undoToolStripButton,
            this._redoToolStripButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 31);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(1303, 29);
            this._toolStrip1.TabIndex = 5;
            this._toolStrip1.Text = "toolStrip1";
            // 
            // _lineToolStripButton
            // 
            this._lineToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._lineToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_lineToolStripButton.Image")));
            this._lineToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._lineToolStripButton.Name = "_lineToolStripButton";
            this._lineToolStripButton.Size = new System.Drawing.Size(34, 24);
            this._lineToolStripButton.Text = "LineToolStripButton";
            this._lineToolStripButton.Click += new System.EventHandler(this.ClickLineToolStripButton);
            // 
            // _rectangleToolStripButton
            // 
            this._rectangleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._rectangleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_rectangleToolStripButton.Image")));
            this._rectangleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._rectangleToolStripButton.Name = "_rectangleToolStripButton";
            this._rectangleToolStripButton.Size = new System.Drawing.Size(34, 24);
            this._rectangleToolStripButton.Text = "RecatangleToolStripButton";
            this._rectangleToolStripButton.Click += new System.EventHandler(this.ClickRectangleToolStripButton);
            // 
            // _circleToolStripButton
            // 
            this._circleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._circleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_circleToolStripButton.Image")));
            this._circleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._circleToolStripButton.Name = "_circleToolStripButton";
            this._circleToolStripButton.Size = new System.Drawing.Size(34, 24);
            this._circleToolStripButton.Text = "CircleToolStripButton";
            this._circleToolStripButton.Click += new System.EventHandler(this.ClickCircleToolStripButton);
            // 
            // _pointerToolStripButton
            // 
            this._pointerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._pointerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_pointerToolStripButton.Image")));
            this._pointerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._pointerToolStripButton.Name = "_pointerToolStripButton";
            this._pointerToolStripButton.Size = new System.Drawing.Size(34, 24);
            this._pointerToolStripButton.Text = "toolStripBindingButton1";
            this._pointerToolStripButton.Click += new System.EventHandler(this.ClickPointerToolStripButton);
            // 
            // _undoToolStripButton
            // 
            this._undoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._undoToolStripButton.Enabled = false;
            this._undoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_undoToolStripButton.Image")));
            this._undoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._undoToolStripButton.Name = "_undoToolStripButton";
            this._undoToolStripButton.Size = new System.Drawing.Size(34, 24);
            this._undoToolStripButton.Text = "toolStripBindingButton1";
            this._undoToolStripButton.Click += new System.EventHandler(this.ClickUndoToolStripButton);
            // 
            // _redoToolStripButton
            // 
            this._redoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._redoToolStripButton.Enabled = false;
            this._redoToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_redoToolStripButton.Image")));
            this._redoToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._redoToolStripButton.Name = "_redoToolStripButton";
            this._redoToolStripButton.Size = new System.Drawing.Size(34, 24);
            this._redoToolStripButton.Text = "toolStripBindingButton2";
            this._redoToolStripButton.Click += new System.EventHandler(this.ClickRedoToolStripButton);
            // 
            // _splitContainer1
            // 
            this._splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer1.Location = new System.Drawing.Point(0, 60);
            this._splitContainer1.Name = "_splitContainer1";
            // 
            // _splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.Controls.Add(this._slide1);
            // 
            // _splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._splitContainer2);
            this._splitContainer1.Size = new System.Drawing.Size(1303, 680);
            this._splitContainer1.SplitterDistance = 160;
            this._splitContainer1.TabIndex = 7;
            this._splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MovedSplit1);
            // 
            // _splitContainer2
            // 
            this._splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this._splitContainer2.Location = new System.Drawing.Point(0, 0);
            this._splitContainer2.Name = "_splitContainer2";
            // 
            // _splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.Controls.Add(this._panel);
            // 
            // _splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.Controls.Add(this._infoGroupBox);
            this._splitContainer2.Size = new System.Drawing.Size(1139, 680);
            this._splitContainer2.SplitterDistance = 790;
            this._splitContainer2.TabIndex = 7;
            this._splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.MovedSplit2);
            // 
            // _panel
            // 
            this._panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panel.BackColor = System.Drawing.SystemColors.Window;
            this._panel.Location = new System.Drawing.Point(0, 0);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(789, 360);
            this._panel.TabIndex = 6;
            this._panel.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintPanel);
            this._panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleCanvasPressed);
            this._panel.MouseEnter += new System.EventHandler(this.HandleCanvasEnter);
            this._panel.MouseLeave += new System.EventHandler(this.HandleCanvasLeave);
            this._panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandleCanvasMoved);
            this._panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleCanvasReleased);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 740);
            this.Controls.Add(this._splitContainer1);
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this._menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "HW2";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PressKey);
            ((System.ComponentModel.ISupportInitialize)(this._infoDataGridView)).EndInit();
            this._infoGroupBox.ResumeLayout(false);
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
            this._toolStrip1.ResumeLayout(false);
            this._toolStrip1.PerformLayout();
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).EndInit();
            this._splitContainer1.ResumeLayout(false);
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel2.ResumeLayout(false);
            this._splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).EndInit();
            this._splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _infoDataGridView;
        private System.Windows.Forms.GroupBox _infoGroupBox;
        private System.Windows.Forms.Button _addButton;
        private System.Windows.Forms.ComboBox _shapeComboBox;
        private System.Windows.Forms.MenuStrip _menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _aboutToolStripMenuItem;
        private System.Windows.Forms.Button _slide1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _shape;
        private System.Windows.Forms.DataGridViewTextBoxColumn _information;
        private System.Windows.Forms.ToolStrip _toolStrip1;
        private ToolStripBindingButton _lineToolStripButton;
        private ToolStripBindingButton _rectangleToolStripButton;
        private ToolStripBindingButton _circleToolStripButton;
        private ToolStripBindingButton _pointerToolStripButton;
        private System.Windows.Forms.DataGridViewButtonColumn _delete;
        private DoubleBufferedPanel _panel;
        private ToolStripBindingButton _undoToolStripButton;
        private ToolStripBindingButton _redoToolStripButton;
        private System.Windows.Forms.SplitContainer _splitContainer1;
        private System.Windows.Forms.SplitContainer _splitContainer2;
    }
}



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
            this._panel = new PowerPoint.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this._infoDataGridView)).BeginInit();
            this._infoGroupBox.SuspendLayout();
            this._menuStrip1.SuspendLayout();
            this._toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _infoDataGridView
            // 
            this._infoDataGridView.AllowUserToAddRows = false;
            this._infoDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this._infoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._infoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._delete});
            this._infoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoDataGridView.Location = new System.Drawing.Point(3, 96);
            this._infoDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._infoDataGridView.Name = "_infoDataGridView";
            this._infoDataGridView.RowHeadersVisible = false;
            this._infoDataGridView.RowHeadersWidth = 62;
            this._infoDataGridView.RowTemplate.Height = 27;
            this._infoDataGridView.Size = new System.Drawing.Size(331, 609);
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
            this._infoGroupBox.Controls.Add(this._addButton);
            this._infoGroupBox.Controls.Add(this._shapeComboBox);
            this._infoGroupBox.Controls.Add(this._infoDataGridView);
            this._infoGroupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this._infoGroupBox.Location = new System.Drawing.Point(966, 31);
            this._infoGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._infoGroupBox.Name = "_infoGroupBox";
            this._infoGroupBox.Padding = new System.Windows.Forms.Padding(3, 74, 3, 4);
            this._infoGroupBox.Size = new System.Drawing.Size(337, 709);
            this._infoGroupBox.TabIndex = 1;
            this._infoGroupBox.TabStop = false;
            this._infoGroupBox.Text = "資訊顯示";
            // 
            // _addButton
            // 
            this._addButton.Location = new System.Drawing.Point(3, 25);
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
            this._shapeComboBox.Location = new System.Drawing.Point(186, 46);
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
            this._infoToolStripMenuItem.Size = new System.Drawing.Size(62, 32);
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
            this._slide1.Location = new System.Drawing.Point(12, 78);
            this._slide1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._slide1.Name = "_slide1";
            this._slide1.Size = new System.Drawing.Size(160, 120);
            this._slide1.TabIndex = 3;
            this._slide1.UseVisualStyleBackColor = true;
            // 
            // _toolStrip1
            // 
            this._toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lineToolStripButton,
            this._rectangleToolStripButton,
            this._circleToolStripButton,
            this._pointerToolStripButton});
            this._toolStrip1.Location = new System.Drawing.Point(0, 31);
            this._toolStrip1.Name = "_toolStrip1";
            this._toolStrip1.Size = new System.Drawing.Size(966, 29);
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
            this._rectangleToolStripButton.Size = new System.Drawing.Size(34, 33);
            this._rectangleToolStripButton.Text = "RecatangleToolStripButton";
            this._rectangleToolStripButton.Click += new System.EventHandler(this.ClickRectangleToolStripButton);
            // 
            // _circleToolStripButton
            // 
            this._circleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._circleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_circleToolStripButton.Image")));
            this._circleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._circleToolStripButton.Name = "_circleToolStripButton";
            this._circleToolStripButton.Size = new System.Drawing.Size(34, 33);
            this._circleToolStripButton.Text = "CircleToolStripButton";
            this._circleToolStripButton.Click += new System.EventHandler(this.ClickCircleToolStripButton);
            // 
            // _pointerToolStripButton
            // 
            this._pointerToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._pointerToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_pointerToolStripButton.Image")));
            this._pointerToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._pointerToolStripButton.Name = "_pointerToolStripButton";
            this._pointerToolStripButton.Size = new System.Drawing.Size(34, 33);
            this._pointerToolStripButton.Text = "toolStripBindingButton1";
            this._pointerToolStripButton.Click += new System.EventHandler(this.ClickPointerToolStripButton);
            // 
            // _panel
            // 
            this._panel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._panel.BackColor = System.Drawing.SystemColors.Window;
            this._panel.Location = new System.Drawing.Point(237, 78);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(640, 480);
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
            this.Controls.Add(this._toolStrip1);
            this.Controls.Add(this._slide1);
            this.Controls.Add(this._infoGroupBox);
            this.Controls.Add(this._menuStrip1);
            this.Controls.Add(this._panel);
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
    }
}


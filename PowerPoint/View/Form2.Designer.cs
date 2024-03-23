
namespace PowerPoint
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._x1Input = new System.Windows.Forms.TextBox();
            this._x1Label = new System.Windows.Forms.Label();
            this._y1Label = new System.Windows.Forms.Label();
            this._y1Input = new System.Windows.Forms.TextBox();
            this._x2Label = new System.Windows.Forms.Label();
            this._x2Input = new System.Windows.Forms.TextBox();
            this._y2Label = new System.Windows.Forms.Label();
            this._y2Input = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.Enabled = false;
            this._okButton.Location = new System.Drawing.Point(72, 253);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(100, 30);
            this._okButton.TabIndex = 10;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.ClickOkButton);
            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(287, 253);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(100, 30);
            this._cancelButton.TabIndex = 12;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.ClickCancelButton);
            // 
            // _x1Input
            // 
            this._x1Input.Location = new System.Drawing.Point(72, 70);
            this._x1Input.Name = "_x1Input";
            this._x1Input.Size = new System.Drawing.Size(100, 25);
            this._x1Input.TabIndex = 2;
            this._x1Input.TextChanged += new System.EventHandler(this.InputX1);
            // 
            // _x1Label
            // 
            this._x1Label.AutoSize = true;
            this._x1Label.Location = new System.Drawing.Point(69, 52);
            this._x1Label.Name = "_x1Label";
            this._x1Label.Size = new System.Drawing.Size(92, 15);
            this._x1Label.TabIndex = 3;
            this._x1Label.Text = "左上角座標X";
            // 
            // _y1Label
            // 
            this._y1Label.AutoSize = true;
            this._y1Label.Location = new System.Drawing.Point(284, 52);
            this._y1Label.Name = "_y1Label";
            this._y1Label.Size = new System.Drawing.Size(92, 15);
            this._y1Label.TabIndex = 5;
            this._y1Label.Text = "左上角座標Y";
            // 
            // _y1Input
            // 
            this._y1Input.Location = new System.Drawing.Point(287, 70);
            this._y1Input.Name = "_y1Input";
            this._y1Input.Size = new System.Drawing.Size(100, 25);
            this._y1Input.TabIndex = 4;
            this._y1Input.TextChanged += new System.EventHandler(this.InputY1);
            // 
            // _x2Label
            // 
            this._x2Label.AutoSize = true;
            this._x2Label.Location = new System.Drawing.Point(69, 152);
            this._x2Label.Name = "_x2Label";
            this._x2Label.Size = new System.Drawing.Size(92, 15);
            this._x2Label.TabIndex = 7;
            this._x2Label.Text = "右下角座標X";
            // 
            // _x2Input
            // 
            this._x2Input.Location = new System.Drawing.Point(72, 170);
            this._x2Input.Name = "_x2Input";
            this._x2Input.Size = new System.Drawing.Size(100, 25);
            this._x2Input.TabIndex = 6;
            this._x2Input.TextChanged += new System.EventHandler(this.InputX2);
            // 
            // _y2Label
            // 
            this._y2Label.AutoSize = true;
            this._y2Label.Location = new System.Drawing.Point(284, 152);
            this._y2Label.Name = "_y2Label";
            this._y2Label.Size = new System.Drawing.Size(92, 15);
            this._y2Label.TabIndex = 9;
            this._y2Label.Text = "右下角座標Y";
            // 
            // _y2Input
            // 
            this._y2Input.Location = new System.Drawing.Point(287, 170);
            this._y2Input.Name = "_y2Input";
            this._y2Input.Size = new System.Drawing.Size(100, 25);
            this._y2Input.TabIndex = 8;
            this._y2Input.TextChanged += new System.EventHandler(this.InputY2);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 352);
            this.Controls.Add(this._y2Label);
            this.Controls.Add(this._y2Input);
            this.Controls.Add(this._x2Label);
            this.Controls.Add(this._x2Input);
            this.Controls.Add(this._y1Label);
            this.Controls.Add(this._y1Input);
            this.Controls.Add(this._x1Label);
            this.Controls.Add(this._x1Input);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.TextBox _x1Input;
        private System.Windows.Forms.Label _x1Label;
        private System.Windows.Forms.Label _y1Label;
        private System.Windows.Forms.TextBox _y1Input;
        private System.Windows.Forms.Label _x2Label;
        private System.Windows.Forms.TextBox _x2Input;
        private System.Windows.Forms.Label _y2Label;
        private System.Windows.Forms.TextBox _y2Input;
    }
}
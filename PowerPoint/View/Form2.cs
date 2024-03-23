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
    public partial class Form2 : Form
    {
        Form1 _baseForm;
        PresentationModel2 _presentationModel;
        public Form2(Form1 form1, PresentationModel2 presentationModel)
        {
            _baseForm = form1;
            _presentationModel = presentationModel;
            InitializeComponent();
        }

        // 按下 OK 按鍵
        private void ClickOkButton(object sender, EventArgs e)
        {
            _baseForm.AddShape(int.Parse(_x1Input.Text), int.Parse(_y1Input.Text), int.Parse(_x2Input.Text), int.Parse(_y2Input.Text));
            Close();
        }

        // 按下 Cancel 按鍵
        private void ClickCancelButton(object sender, EventArgs e)
        {
            Close();
        }

        // x1 有輸入值
        private void InputX1(object sender, EventArgs e)
        {
            _okButton.Enabled = _presentationModel.IsValidInput(_x1Input.Text, _y1Input.Text, _x2Input.Text, _y2Input.Text);
        }

        // y1 有輸入值
        private void InputY1(object sender, EventArgs e)
        {
            _okButton.Enabled = _presentationModel.IsValidInput(_x1Input.Text, _y1Input.Text, _x2Input.Text, _y2Input.Text);
        }

        // x2 有輸入值
        private void InputX2(object sender, EventArgs e)
        {
            _okButton.Enabled = _presentationModel.IsValidInput(_x1Input.Text, _y1Input.Text, _x2Input.Text, _y2Input.Text);
        }

        // y2 有輸入值
        private void InputY2(object sender, EventArgs e)
        {
            _okButton.Enabled = _presentationModel.IsValidInput(_x1Input.Text, _y1Input.Text, _x2Input.Text, _y2Input.Text);
        }
    }
}

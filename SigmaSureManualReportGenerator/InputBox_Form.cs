using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SigmaSureManualReportGenerator
{
    public partial class InputBox_Form : Form
    {
        public InputBox_Form()
        {
            InitializeComponent();
        }

        public InputBox_Form(String TitleCaption, String QuestionString)
        {
            InitializeComponent();
            this.Text = TitleCaption;
            this.lbl_Question.Text = QuestionString;
        }

        public String Answer;
        
        private void InputBox_Form_Load(object sender, EventArgs e)
        {
            this.tb_Answer.Focus();
        }

        private void tb_Answer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btn_OK_Click(new object(), new EventArgs());
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Answer = this.tb_Answer.Text;
            this.Close();
        }

        private void btn_CANCEL_Click(object sender, EventArgs e)
        {
            this.Answer = "";
            this.Close();
        }

        private void InputBox_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Answer = "";
            }
        }
    }
}

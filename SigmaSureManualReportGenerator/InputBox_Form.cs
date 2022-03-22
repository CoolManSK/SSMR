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

        public InputBox_Form(String TitleCaption, String QuestionString, String[] ComboBoxItems = null)
        {
            InitializeComponent();
            this.Text = TitleCaption;
            this.lbl_Question.Text = QuestionString;
            if (ComboBoxItems == null)
            {
                this.lbl_Question.Size = new Size(this.lbl_Question.Size.Width, 109);
                this.cb_SelectItem.Visible = false;
            }
            else
            {
                this.cb_SelectItem.Items.AddRange(ComboBoxItems);
            }
        }       

        public String Answer;
        public String SelectedItem;
        private bool UserExiting = true;
        
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
            if (this.cb_SelectItem.Visible)
            {
                if (this.cb_SelectItem.SelectedIndex < 0)
                {
                    this.SelectedItem = "";
                }
                else
                {
                    this.SelectedItem = this.cb_SelectItem.Text;
                }
            }
            this.UserExiting = false;
            this.Close();
        }

        private void btn_CANCEL_Click(object sender, EventArgs e)
        {
            this.Answer = "";
            this.SelectedItem = "";
            this.Close();
        }

        private void InputBox_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.UserExiting)
            {
                this.Answer = "";
                this.SelectedItem = "";
            }
        }
    }
}

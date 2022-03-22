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
    public partial class frm_debug : Form
    {
        public frm_debug()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm myMF = new MainForm();
            myMF.Show();
        }
    }
}

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
    public partial class SimpleMode_Form : Form
    {
        public SimpleMode_Form()
        {
            InitializeComponent();
        }

        private void SimpleMode_Form_Load(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb_TestTypesParentForm = (ComboBox)this.Parent.Controls["cb_TestType"];
                foreach (String actTest in cb_TestTypesParentForm.Items)
                {
                    this.cb_TestTypes.Items.Add(actTest);
                }

                        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
    }
}

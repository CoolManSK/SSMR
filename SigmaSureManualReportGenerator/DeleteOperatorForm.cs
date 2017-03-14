using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SigmaSureManualReportGenerator
{
    public partial class OperatorSettingsForm : Form
    {
        public XmlDocument XMLConfigFile;
        private String actUserName;

        public OperatorSettingsForm(String actUserName, XmlDocument XMLConfigFile)
        {
            InitializeComponent();
            this.XMLConfigFile = XMLConfigFile;
            this.actUserName = actUserName;
        }

        private void DeleteOperatorForm_Load(object sender, EventArgs e)
        {
            OperatorData myOD = new OperatorData(this.actUserName, this.XMLConfigFile);
            Array ar_OperatorNames = myOD.GetActualOperators(myOD.Privileges);
            Array.Sort(ar_OperatorNames);
            foreach (String actOperatorName in ar_OperatorNames)
            {
                this.cb_OperatorSurname.Items.Add(actOperatorName);
            }

            Array ar_OperatorNumbers = myOD.GetActualNumbers(myOD.Privileges);
            Array.Sort(ar_OperatorNumbers);
            foreach (String actOperatorNumber in ar_OperatorNumbers)
            {
                this.cb_OperatorNumber.Items.Add(actOperatorNumber);
            }
        }

        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            if ((this.cb_OperatorNumber.SelectedIndex == -1) || (this.cb_OperatorSurname.SelectedIndex == -1))
            {
                this.cb_OperatorSurname.Focus();
                return;
            }

            OperatorData myOD = new OperatorData(this.cb_OperatorSurname.Text, this.XMLConfigFile);
            if (myOD.DeleteOperator())
            {
                MessageBox.Show(String.Concat("Operator ", this.cb_OperatorSurname.Text, " s osobnym cislom ", this.cb_OperatorNumber.Text, " bol vymazany."));
                this.cb_OperatorNumber.Items.Clear();
                this.cb_OperatorSurname.Items.Clear();
                this.DeleteOperatorForm_Load(new object(), new EventArgs());
            }     
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cb_OperatorSurname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_OperatorSurname.SelectedIndex == -1)
            {
                this.cb_OperatorNumber.SelectedIndex = -1;
                this.cb_OperatorSurname.Focus();
                return;
            }

            OperatorData myOD = new OperatorData(this.cb_OperatorSurname.Text, this.XMLConfigFile);
            this.cb_OperatorNumber.Text = myOD.Number;
        }

        private void cb_OperatorNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_OperatorNumber.SelectedIndex == -1)
            {
                this.cb_OperatorSurname.SelectedIndex = -1;
                this.cb_OperatorNumber.Focus();
                return;
            }

            OperatorData myOD = new OperatorData(this.cb_OperatorNumber.Text, this.XMLConfigFile, true);
            this.cb_OperatorSurname.Text = myOD.Surname;
        }
    }
}

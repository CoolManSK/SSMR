using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SigmaSureManualReportGenerator
{
    public partial class OperatorLoginForm : Form
    {
        public XmlDocument myXMLdoc;
        public Boolean PasswordValidation = false;
        public String LoggedOperatorNumber = "";
        public String LoggedOperatorSurname = "";
        public String ConfigFile = "";


        public BelMES BelMESobj;
        private Boolean BelMESenabled = false;
        //public clEnvironment myEnv = new clEnvironment();
        //public clEmployee myEmp = new clEmployee();        

        public OperatorLoginForm(XmlDocument ConfigFile)
        {            
            InitializeComponent();
            this.myXMLdoc = ConfigFile;
            this.BelMESobj = null;
        } 
        
        public OperatorLoginForm(XmlDocument ConfigFile, BelMES BelMESobj)
        {
            InitializeComponent();
            this.myXMLdoc = ConfigFile;
            this.BelMESenabled = true;
            this.BelMESobj = BelMESobj;
        }

        private void OperatorLogin_Load(object sender, EventArgs e)
        {                      
            OperatorData myOD = new OperatorData(this.myXMLdoc);
            Array ar_OperatorsNumbers = myOD.GetActualNumbers("");

            foreach (String actOperatorNumber in ar_OperatorsNumbers)
            {
                this.cb_OperatorLoginNr.Items.Add(actOperatorNumber);
            }

            Array ar_OperatorsSurnames = myOD.GetActualOperators("");
            foreach (String actOperatorSurname in ar_OperatorsSurnames)
            {
                this.cb_OperatorSurname.Items.Add(actOperatorSurname);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((this.tb_OperatorScanField.Text.Length == 8) && (this.tb_OperatorScanField.Text.Substring(0,3).ToUpper() == "OS:"))
            {
                String str_osobneCislo = this.tb_OperatorScanField.Text.Substring(3, 5);
                while (str_osobneCislo.Substring(0,1) == "0")
                {
                    str_osobneCislo = str_osobneCislo.Substring(1);
                }
                try
                {
                    this.cb_OperatorLoginNr.SelectedItem = str_osobneCislo;
                }
                catch
                {
                    this.cb_OperatorLoginNr.SelectedIndex = -1;
                }
                this.tb_OperatorScanField.SelectAll();
            }
        }

        private void tb_OperatorLoginPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btn_OperatorLoginOK_Click(sender, new EventArgs());
            }
        }

        private void btn_OperatorLoginOK_Click(object sender, EventArgs e)
        {
            OperatorData ope_data = new OperatorData(this.myXMLdoc);
            String str_OperatorNumberBelMes = this.cb_OperatorLoginNr.Text;
            /*while (str_OperatorNumberBelMes.Length > 5)
            {
                str_OperatorNumberBelMes = String.Concat("0", str_OperatorNumberBelMes);
            }*/
            if (ope_data.PasswordValidation(this.cb_OperatorLoginNr.Text, this.tb_OperatorLoginPassword.Text, this.myXMLdoc))
            {
                this.PasswordValidation = true;
                if (this.BelMESenabled)
                {                    
                    if (!this.BelMESobj.EmployeeVerification(str_OperatorNumberBelMes))
                    {
                        //return;
                    }
                }

                this.LoggedOperatorNumber = ope_data.Number;
                this.LoggedOperatorSurname = ope_data.Surname;
                this.Close();
            }  
            else
            {
                this.tb_OperatorLoginPassword.SelectAll();
            }          
        }

        private void btn_OperatorLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_OperatorLoginNr_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperatorData myOD = new OperatorData(this.cb_OperatorLoginNr.Text, this.myXMLdoc, true);

            this.cb_OperatorSurname.Text = myOD.Surname;
            
            this.tb_OperatorLoginPassword.Focus();
        }

        private void cb_OperatorSurname_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperatorData myOD = new OperatorData(this.cb_OperatorSurname.Text, this.myXMLdoc);

            this.cb_OperatorLoginNr.Text = myOD.Number;

            this.tb_OperatorLoginPassword.Focus();
        }
    }
}

using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using System.Xml;
using NewLogin;

namespace SigmaSureManualReportGenerator
{
    public partial class OperatorLoginForm : Form
    {
        public XmlDocument myXMLdoc;

        private Boolean ExtraLoginEnabled;
        public XmlDocument ExtraLoginXMLdoc;
        //private String ExtraLoginConfigFilePath = @"S:\Manufacturing_Engineering\Public\Kolman Vladimir\SigmaSure\UserConfigurations\NewLogin.xml";

        public Boolean PasswordValidation = false;
        public String LoggedOperatorNumber = "";
        public String LoggedOperatorSurname = "";
        public String Privileges = "";
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
            this.ExtraLoginEnabled = false;
        } 
        
        public OperatorLoginForm(XmlDocument ConfigFile, BelMES BelMESobj)
        {
            InitializeComponent();
            this.myXMLdoc = ConfigFile;            
            this.BelMESenabled = true;
            this.BelMESobj = BelMESobj;
            this.ExtraLoginEnabled = false;
        }

        public OperatorLoginForm(XmlDocument ConfigFile, XmlDocument ExtraLoginConfigFile, BelMES BelMESobj)
        {
            InitializeComponent();
            this.myXMLdoc = ConfigFile;
            this.ExtraLoginXMLdoc = ExtraLoginConfigFile;            
            this.BelMESenabled = true;
            this.BelMESobj = BelMESobj;
            this.ExtraLoginEnabled = true;
        }

        private void OperatorLogin_Load(object sender, EventArgs e)
        {
            if (!this.ExtraLoginEnabled)
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
            else
            {
                Login myLogin = new Login();
                Array ar_OperatorsData = myLogin.GetOperatorsAllData(this.BelMESobj.Env.strTracePoint);
                foreach (NewLogin.OperatorData actOperatorData in ar_OperatorsData)
                {
                    this.cb_OperatorLoginNr.Items.Add(actOperatorData.Number);
                    this.cb_OperatorSurname.Items.Add(actOperatorData.Name);
                }
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
                this.tb_OperatorScanField.Clear();
                if (this.cb_OperatorLoginNr.SelectedIndex > -1)
                {
                    this.tb_OperatorScanField.Clear();
                    this.tb_OperatorLoginPassword.Focus();
                }                
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
            if (!this.ExtraLoginEnabled)
            {
                OperatorData ope_data = new OperatorData(this.myXMLdoc);
                if (this.tb_OperatorScanField.Text.Trim().Length == 16)
                {

                    String str_typedValidationString = this.tb_OperatorScanField.Text.Trim();
                    ope_data = new OperatorData(str_typedValidationString);
                    if (ope_data.PasswordValidation(str_typedValidationString))
                    {
                        this.PasswordValidation = true;
                        this.LoggedOperatorNumber = ope_data.Number;
                        this.LoggedOperatorSurname = ope_data.Surname;
                        this.Privileges = ope_data.Privileges;
                        if (!this.BelMESobj.EmployeeVerification(this.LoggedOperatorNumber))
                        {
                            this.tb_OperatorScanField.Focus();
                            this.tb_OperatorScanField.SelectAll();
                        }
                        else
                        {
                            this.Close();
                        }
                    }

                    return;
                }

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
                            this.tb_OperatorScanField.Focus();
                            this.tb_OperatorScanField.SelectAll();
                            this.PasswordValidation = false;
                            MessageBox.Show(String.Concat("Neznamy operator. Zavolajte prosim nadriadeneho.\r", this.BelMESobj.Emp.strEmployeeNumber));

                            return;
                        }
                    }

                    this.LoggedOperatorNumber = ope_data.Number;
                    this.LoggedOperatorSurname = ope_data.Surname;
                    this.Privileges = ope_data.Privileges;
                    this.Close();
                }
                else
                {
                    this.tb_OperatorScanField.Text = @"Nespravne heslo";
                    this.tb_OperatorLoginPassword.Focus();
                    this.tb_OperatorLoginPassword.SelectAll();
                    return;
                }
            }
            else
            {
                NewLogin.Login myNL = new Login(this.BelMESobj.Env.strTracePoint);
                try
                {
                    if (!this.BelMESobj.EmployeeVerification(this.cb_OperatorLoginNr.SelectedItem.ToString()))
                    {
                        MessageBox.Show(String.Concat(this.BelMESobj.Emp.strEmployeeCodeInfo));
                    }
                    else
                    {
                        NewLogin.OperatorData ope_data = myNL.GetOperatorData(this.cb_OperatorSurname.SelectedItem.ToString());
                        this.LoggedOperatorNumber = ope_data.Number;
                        this.LoggedOperatorSurname = ope_data.Name;
                        ope_data.Privileges = ope_data.Privileges.Substring(0, ope_data.Privileges.Length - 1);
                        this.Privileges = ope_data.Privileges;
                        this.Close();
                    }
                }
                catch
                {

                }
            }
        }

        private void btn_OperatorLoginCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_OperatorLoginNr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ExtraLoginEnabled)
            {
                OperatorData myOD = new OperatorData(this.cb_OperatorLoginNr.Text, this.myXMLdoc, true);

                this.cb_OperatorSurname.Text = myOD.Surname;                
            }
            else
            {
                Login myNL = new NewLogin.Login();
                NewLogin.OperatorData myOD = myNL.GetOperatorData("", this.cb_OperatorLoginNr.Text.Trim());
                this.cb_OperatorSurname.Text = myOD.Name;
            }

            this.tb_OperatorLoginPassword.Focus();
        }

        private void cb_OperatorSurname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ExtraLoginEnabled)
            {
                OperatorData myOD = new OperatorData(this.cb_OperatorSurname.Text, this.myXMLdoc);

                this.cb_OperatorLoginNr.Text = myOD.Number;
            }
            else
            {
                Login myNL = new NewLogin.Login();
                NewLogin.OperatorData myOD = myNL.GetOperatorData(this.cb_OperatorSurname.Text);
                this.cb_OperatorSurname.Text = myOD.Number;
            }
            
            this.tb_OperatorLoginPassword.Focus();
        }

        private void tb_OperatorScanField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btn_OperatorLoginOK_Click(new object(), new EventArgs());
            }
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            ToolTip myTT = new ToolTip();
            myTT.SetToolTip(this.label3, this.myXMLdoc.BaseURI);
        }
    }
}

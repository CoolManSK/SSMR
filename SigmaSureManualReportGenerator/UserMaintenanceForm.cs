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
    public partial class UserMaintenanceForm : Form
    {
        public UserMaintenanceForm(XmlDocument xmlConfigDocument, String actualUserName)
        {
            InitializeComponent();
            this.ConfigDocument = xmlConfigDocument;
            this.actUserName = actualUserName;
        }

        private XmlDocument ConfigDocument;
        private String actUserName;       

        private void ErrorMessageBoxShow(String Message)
        {
            MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ResetForm()
        {
            this.tb_NewOP_name.Text = "";
            this.tb_NewOP_number.Text = "";
            this.tb_NewOP_password.Text = "";
            this.tb_NewOP_PWVer.Text = "";
            this.tb_NewSurname.Text = "";
            this.tb_NewNumber.Text = "";

            this.cb_OldSurname.Items.Clear();
            this.cb_OldNumber.Items.Clear();
            this.cb_ExistingUserPrivileges.Items.Clear();
            this.cb_NewUserPrivileges.Items.Clear();

            this.UserMaintenanceForm_Load(new object(), new EventArgs());
        }

        private void UserMaintenanceForm_Load(object sender, EventArgs e)
        {
            OperatorData myOD = new OperatorData(this.actUserName, this.ConfigDocument);
            Array ar_OperatorsNames = myOD.GetActualOperators(myOD.Privileges);
            foreach (String actOperatorName in ar_OperatorsNames)
            {
                this.cb_OldSurname.Items.Add(actOperatorName);
            }

            Array ar_OperatorsNumbers = myOD.GetActualNumbers(myOD.Privileges);
            foreach (String actOperatorNumber in ar_OperatorsNumbers)
            {
                this.cb_OldNumber.Items.Add(actOperatorNumber);
            }

            this.cb_NewUserPrivileges.Items.Add("operator");

            this.cb_ExistingUserPrivileges.Items.Add("operator");

            if (myOD.Privileges == "admin")
            {
                this.cb_NewUserPrivileges.Items.Add("admin");
                this.cb_NewUserPrivileges.Items.Add("useradmin");

                this.cb_ExistingUserPrivileges.Items.Add("admin");
                this.cb_ExistingUserPrivileges.Items.Add("useradmin");
            }
            else if (myOD.Privileges == "useradmin")
            {
                this.cb_NewUserPrivileges.Items.Add("useradmin");

                this.cb_ExistingUserPrivileges.Items.Add("useradmin");
            }
        }

        private void btn_NewOperator_Save_Click(object sender, EventArgs e)
        {
            if (this.tb_NewOP_name.Text == "")
            {
                this.ErrorMessageBoxShow("Zadajte meno operatora.");
                this.tb_NewOP_name.Focus();
                return;
            }

            OperatorData myOD = new OperatorData(this.actUserName, this.ConfigDocument);

            if (Array.IndexOf(myOD.GetActualOperators(myOD.Privileges), this.tb_NewOP_name.Text) > -1)
            {
                this.ErrorMessageBoxShow(String.Concat("Operator s menom \"", this.tb_NewOP_name.Text, "\" uz existuje."));
                this.tb_NewOP_name.Focus();
                return;
            }

            if (this.tb_NewOP_number.Text == "")
            {
                this.ErrorMessageBoxShow("Zadajte osobne cislo operatora.");
                this.tb_NewOP_name.Focus();
                return;
            }

            try
            {
                Int32 actNumber = Convert.ToInt32(this.tb_NewOP_number.Text);
            }
            catch
            {
                this.ErrorMessageBoxShow("Osobne cislo operatora nemôže obsahovat ine znaky ako cisla.");
                this.tb_NewOP_number.Focus();
                return;
            }

            if (Array.IndexOf(myOD.GetActualNumbers(myOD.Privileges), this.tb_NewOP_number.Text) > - 1)
            {
                this.ErrorMessageBoxShow(String.Concat("Operator s osobnym cislom \"", this.tb_NewOP_name.Text, "\" uz existuje."));
                this.tb_NewOP_number.Focus();
                return;
            }

            if (this.tb_NewOP_password.Text.Length < 6)
            {
                this.ErrorMessageBoxShow("Heslo musi mat minimalne 6 znakov.");
                this.tb_NewOP_password.Focus();
                this.tb_NewOP_password.SelectAll();
                return;
            }

            if (this.tb_NewOP_password.Text != this.tb_NewOP_PWVer.Text)
            {
                this.ErrorMessageBoxShow("Nove heslo nie je zhodne s jeho potvrdenim.");
                this.tb_NewOP_PWVer.Focus();
                return;
            }

            if (this.cb_NewUserPrivileges.SelectedIndex == -1)
            {
                this.ErrorMessageBoxShow("Vyberte prava noveho uzivatela.");
                this.cb_NewUserPrivileges.Focus();
                return;
            }

            OperatorData newOperator = new OperatorData(this.tb_NewOP_name.Text, this.tb_NewOP_number.Text, this.tb_NewOP_password.Text, this.cb_NewUserPrivileges.Text, this.ConfigDocument);
            if (newOperator.SaveNewOperator())
            {
                MessageBox.Show(String.Concat("Operator s menom ", this.tb_NewOP_name.Text, " a cislom ", this.tb_NewOP_number.Text, " bol uspesne vytvoreny."));
                this.ResetForm();
            }
        }

        private void cb_OldSurname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_OldSurname.SelectedIndex == -1)
            {
                return;
            }

            OperatorData myOD = new OperatorData(this.cb_OldSurname.Text, this.ConfigDocument);

            this.cb_OldNumber.Text = myOD.Number;     
        }

        private void cb_OldNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_OldNumber.SelectedIndex == -1)
            {
                return;
            }

            OperatorData myOD = new OperatorData(this.cb_OldNumber.Text, this.ConfigDocument, true);

            this.cb_OldSurname.Text = myOD.Surname;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_OperatorChangeSave_Click(object sender, EventArgs e)
        {
            if (this.cb_OldSurname.SelectedIndex == - 1)
            {
                this.cb_OldSurname.Focus();
                return;
            }
            if (this.cb_OldNumber.SelectedIndex == -1)
            {
                this.cb_OldNumber.Focus();
                return;
            }
            if ((this.tb_NewSurname.Text.Trim() == "") && (this.tb_NewNumber.Text.Trim() == ""))
            {
                this.tb_NewSurname.Focus();
                return;
            }

            OperatorData myOD = new OperatorData(this.cb_OldSurname.Text, this.ConfigDocument);
            if (myOD.ReplaceOperatorData(this.tb_NewSurname.Text, this.tb_NewNumber.Text, this.cb_ExistingUserPrivileges.Text))
            {
                MessageBox.Show(String.Concat("Operator ", this.cb_OldSurname.Text, " s osobnym cislom ", this.cb_OldNumber.Text, " bol nahradeny operatorom ", (this.tb_NewSurname.Text.Trim() == "") ? (this.cb_OldSurname.Text) : (this.tb_NewSurname.Text.Trim()), " s osobnym cislom ", (this.tb_NewNumber.Text.Trim() == "") ? (this.cb_OldNumber.Text) : (this.tb_NewNumber.Text.Trim()), "."));
                this.ResetForm();
            }
        }
    }
}

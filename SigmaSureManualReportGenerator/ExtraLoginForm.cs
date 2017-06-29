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
    public partial class ExtraLoginForm : Form
    {
        public BelMES BelMESobj;

        public Boolean PasswordValidation = false;
        public String LoggedOperatorNumber = "";
        public String LoggedOperatorSurname = "";
        public Int32 AdminRights = 0;
        public String ConfigFile = "ExtraLoginConfiguration.xml";
        public XmlDocument xmld_ELConfig;

        public ExtraLoginForm()
        {
            InitializeComponent();
            this.xmld_ELConfig = new XmlDocument();

            OpenFileDialog myOFD = new OpenFileDialog();
            myOFD.InitialDirectory = Application.StartupPath;
            myOFD.ShowDialog();
            this.xmld_ELConfig.Load(String.Concat(@"ConfigFiles\", this.ConfigFile));

        }

        public ExtraLoginForm(BelMES BelMESobj) : this()
        {
            this.BelMESobj = BelMESobj;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.tb_ExtraBarcode.Text.Trim() == string.Empty) return;
            if (this.tb_ExtraBarcode.Text.Trim().Length != 16) return;
            String str_typedValidationString = this.tb_ExtraBarcode.Text.Trim();
            XmlNode ConfigurationNode = this.xmld_ELConfig.SelectSingleNode("./Configuration");
            foreach (XmlNode actUserNode in ConfigurationNode.ChildNodes)
            {
                String str_actExtraLogin = actUserNode.SelectSingleNode("./ValidationString").InnerText.Trim();
                if (str_actExtraLogin == str_typedValidationString)
                {
                    this.PasswordValidation = true;
                    this.LoggedOperatorNumber = actUserNode.Name.Substring(1);
                    this.LoggedOperatorSurname = actUserNode.SelectSingleNode("./Surname").InnerText.Trim();
                    this.AdminRights = Convert.ToInt32(actUserNode.SelectSingleNode("./AdminRights").InnerText.Trim());
                    break;
                }
            }
            this.Close();
        }

        private void tb_ExtraBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btn_OK_Click(new object(), new EventArgs());
            }
        }
    }
}

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
    public partial class PasswordChangeForm : Form
    {
        public PasswordChangeForm(String OperatorName, XmlDocument ConfigFile)
        {
            InitializeComponent();
            this.lbl_Operator.Text = OperatorName;
            this.UserConfig = ConfigFile;
        }

        private XmlDocument UserConfig = new XmlDocument();

        public class OperatorData
        {
            public String Surname;
            public String Number;
            public String Password;
            private XmlDocument XMLConfig;
            private XmlNode OperatorNode;

            public OperatorData(String Surname, XmlDocument ConfigDocument)
            {
                this.XMLConfig = ConfigDocument;

                XmlNode node_Assembly = this.XMLConfig.SelectSingleNode(String.Concat("./Operators"));

                foreach (XmlNode actNode in node_Assembly.ChildNodes)
                {
                    if (actNode.SelectSingleNode("./Surname").InnerText == Surname)
                    {
                        this.OperatorNode = actNode;
                        foreach (XmlNode actChildNode in actNode.ChildNodes)
                        {
                            if (actChildNode.Name.ToLower() == "surname") this.Surname = actChildNode.InnerText;
                            if (actChildNode.Name.ToLower() == "number") this.Number = actChildNode.InnerText;
                            if (actChildNode.Name.ToLower() == "password")
                            {
                                String buffer = actChildNode.InnerText;
                                for (Int16 i = 0; i < buffer.Length; i++)
                                {
                                    this.Password = String.Concat(buffer.Substring(i, 1), this.Password);
                                }
                            }
                        }
                    }
                }
            }

            public void ChangePassword(String NewPassword)
            {
                try {
                    String HashToSave = "";
                    for (Int32 i = 0; i < NewPassword.Length; i++)
                    {
                        HashToSave = String.Concat(NewPassword.Substring(i, 1), HashToSave);
                    }
                    this.OperatorNode.SelectSingleNode("./Password").InnerText = HashToSave;
                    this.XMLConfig.Save(this.XMLConfig.BaseURI.Substring(5));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show(this.XMLConfig.BaseURI.Substring(5));
                }
            }
        }

        private void ErrorMessageBoxShow(String Message)
        {
            MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
        }       

        private void btn_CANCEL_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_SAVE_Click(object sender, EventArgs e)
        {
            if (this.tb_OldPW.Text == "")
            {
                this.ErrorMessageBoxShow("Zadajte stare heslo.");
                this.tb_OldPW.Focus();
                return;
            }

            OperatorData actOperator = new OperatorData(this.lbl_Operator.Text, this.UserConfig);
            if (this.tb_OldPW.Text != actOperator.Password)
            {
                this.ErrorMessageBoxShow("Stare heslo nie je spravne.");
                this.tb_OldPW.SelectAll();
                this.tb_OldPW.Focus();
                return;
            }

            if (this.tb_NewPW.Text.Length < 6)
            {
                this.ErrorMessageBoxShow("Nove heslo musi mat minimalne 6 znakov.");
                this.tb_NewPW.SelectAll();
                this.tb_NewPW.Focus();
                return;
            }

            if (this.tb_NewPW.Text != this.tb_NewPWVer.Text)
            {
                this.ErrorMessageBoxShow("Nove heslo nie je zhodne s jeho potvrdenim.");
                this.tb_NewPWVer.SelectAll();
                this.tb_NewPWVer.Focus();
                return;
            }

            actOperator.ChangePassword(this.tb_NewPW.Text);
            MessageBox.Show("Heslo bolo uspesne zmenene.", "Zmena hesla", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        private void tb_NewPWVer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btn_SAVE_Click(sender, new EventArgs());
            }
        }

        
    }
}

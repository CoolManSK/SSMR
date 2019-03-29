using System;
using System.Xml;
using System.Windows.Forms;

namespace SigmaSureManualReportGenerator
{
    public class OperatorData
    {
        public String Surname;
        public String Number;
        public String Password;
        public String Privileges;
        private XmlDocument ELxmlConfig;
        private XmlDocument XMLConfig;
        private XmlNode OperatorsNode;
        private XmlNode OperatorNode;

        public OperatorData(XmlDocument ConfigFile)
        {
            this.Surname = "";
            this.Number = "";
            this.Password = "";
            this.Privileges = "";
            this.ELxmlConfig = new XmlDocument();
            this.ELxmlConfig.Load(@"ConfigFiles\ExtraLoginConfiguration.xml");
            this.XMLConfig = ConfigFile;
            this.OperatorsNode = this.XMLConfig.SelectSingleNode("./Operators");            
        }
        public OperatorData(String Surname, String Number, String Password, String Privileges, XmlDocument ConfigFile)
        {
            this.ELxmlConfig = new XmlDocument();
            this.ELxmlConfig.Load(@"ConfigFiles\ExtraLoginConfiguration.xml");
            this.XMLConfig = ConfigFile;
            this.OperatorsNode = this.XMLConfig.SelectSingleNode("./Operators");
            this.Surname = Surname;
            this.Number = Number;
            this.Password = Password;
            this.Privileges = Privileges;

        }
        public OperatorData(String Surname, XmlDocument ConfigDocument)
        {
            this.Surname = Surname;

            this.ELxmlConfig = new XmlDocument();
            this.ELxmlConfig.Load(@"ConfigFiles\ExtraLoginConfiguration.xml");
            this.XMLConfig = ConfigDocument;           

            this.OperatorsNode = this.XMLConfig.SelectSingleNode(String.Concat("./Operators"));

            foreach (XmlNode actNode in this.OperatorsNode.ChildNodes)
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
                        if (actChildNode.Name.ToLower() == "privileges") this.Privileges = actChildNode.InnerText;
                    }
                }
            }
        }
        public OperatorData(String Number, XmlDocument ConfigDocument, Boolean myBool)
        {
            this.Number = Number;

            this.ELxmlConfig = new XmlDocument();
            this.ELxmlConfig.Load(@"ConfigFiles\ExtraLoginConfiguration.xml");
            this.XMLConfig = ConfigDocument;

            this.OperatorsNode = this.XMLConfig.SelectSingleNode(String.Concat("./Operators"));

            foreach (XmlNode actNode in this.OperatorsNode.ChildNodes)
            {
                if (actNode.SelectSingleNode("./Number").InnerText == Number)
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
                        if (actChildNode.Name.ToLower() == "privileges") this.Privileges = actChildNode.InnerText;
                    }
                }
            }
        }

        public OperatorData(String ExtraLoginPassword)
        {
            this.ELxmlConfig = new XmlDocument();
            this.ELxmlConfig.Load(@"ConfigFiles\ExtraLoginConfiguration.xml");            
        }

        public void ChangePassword(String NewPassword)
        {
            String HashToSave = "";
            for (Int32 i = 0; i < NewPassword.Length; i++)
            {
                HashToSave = String.Concat(NewPassword.Substring(i, 1), HashToSave);
            }
            this.OperatorNode.SelectSingleNode("./Password").InnerText = HashToSave;
            this.XMLConfig.Save(this.GetFilePathFromURI(this.XMLConfig.BaseURI));
        }
        public Boolean SaveNewOperator()
        {
            if (Array.IndexOf(GetActualOperators(""), this.Surname) > -1)
            {
                this.ErrorMessageBoxShow(String.Concat("Operator s menom \"", this.Surname, "\" uz existuje."));
                return false;
            }

            if (Array.IndexOf(GetActualNumbers(""), this.Number) > -1)
            {
                this.ErrorMessageBoxShow(String.Concat("Operator s osobnym cislom \"", this.Number, "\" uz existuje."));
                return false;
            }

            if (this.Password.Length < 6)
            {
                this.ErrorMessageBoxShow("Heslo musi mat minimalne 6 znakov.");
                return false;
            }
            XmlNode OperatorNodeToAdd = this.OperatorsNode.FirstChild.Clone();
            OperatorNodeToAdd.SelectSingleNode("./Surname").InnerText = this.Surname;
            OperatorNodeToAdd.SelectSingleNode("./Number").InnerText = this.Number;
            String PasswordHash = "";
            for (Int32 i = 0; i < this.Password.Length; i++)
            {
                PasswordHash = String.Concat(this.Password.Substring(i, 1), PasswordHash);
            }
            OperatorNodeToAdd.SelectSingleNode("./Password").InnerText = PasswordHash;
            OperatorNodeToAdd.SelectSingleNode("./Privileges").InnerText = this.Privileges;

            this.OperatorsNode.AppendChild(OperatorNodeToAdd);
            this.XMLConfig.Save(this.GetFilePathFromURI(this.XMLConfig.BaseURI));
            return true;
        }
        public Boolean ReplaceOperatorData(String NewSurname, String NewNumber, String NewPrivileges)
        {
            if (NewSurname.Trim() != "")
            {
                this.OperatorNode.SelectSingleNode("./Surname").InnerText = NewSurname.Trim();
            }
            if (NewNumber.Trim() != "")
            {
                this.OperatorNode.SelectSingleNode("./Number").InnerText = NewNumber.Trim();
            }
            if (NewPrivileges.Trim() != "")
            {
                this.OperatorNode.SelectSingleNode("./Privileges").InnerText = NewPrivileges.Trim();
            }

            this.XMLConfig.Save(this.GetFilePathFromURI(this.XMLConfig.BaseURI));
            return true;
        }
        private void ErrorMessageBoxShow(String Message)
        {
            MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public Array GetActualOperators(String Privileges)
        {
            String[] ar_actOperators = { };
            foreach (XmlNode actOperatorNode in this.OperatorsNode)
            {
                String actName = actOperatorNode.SelectSingleNode("./Surname").InnerText;
                String actPrivileges = actOperatorNode.SelectSingleNode("./Privileges").InnerText;
                if (Array.IndexOf(ar_actOperators, actName) > -1)
                {
                    this.ErrorMessageBoxShow(String.Concat("Kriticka chyba. Operator ", actName, " je viackrat ulozeny v config subore."));
                }
                if (((actPrivileges == "admin") && (Privileges != "admin")) && (Privileges != "")) continue;                
                Array.Resize(ref ar_actOperators, ar_actOperators.Length + 1);
                ar_actOperators.SetValue(actName, ar_actOperators.Length - 1);
            }
            return ar_actOperators;
        }
        public Array GetActualNumbers(String Privileges)
        {
            String[] ar_actNumbers = { };
            foreach (XmlNode actOperatorNode in this.OperatorsNode)
            {
                String actName = actOperatorNode.SelectSingleNode("./Number").InnerText;
                String actPrivileges = actOperatorNode.SelectSingleNode("./Privileges").InnerText;
                if (Array.IndexOf(ar_actNumbers, actName) > -1)
                {
                    this.ErrorMessageBoxShow(String.Concat("Kriticka chyba. Cislo ", actName, " je viackrat ulozeny v config subore."));
                }
                if (((actPrivileges == "admin") && (Privileges != "admin")) && (Privileges != "")) continue;
                Array.Resize(ref ar_actNumbers, ar_actNumbers.Length + 1);
                ar_actNumbers.SetValue(actName, ar_actNumbers.Length - 1);
            }
            return ar_actNumbers;
        }
        public Boolean PasswordValidation(String Number, String Password, XmlDocument ConfigDocument)
        {
            Boolean IsValid = false;            

            String[] ar_operators = { };
            XmlNode node_Operators = this.XMLConfig.SelectSingleNode(String.Concat("./Operators"));

            foreach (XmlNode actNode in node_Operators.ChildNodes)
            {
                XmlNode osCisloNode = actNode.SelectSingleNode(String.Concat("./Number"));
                if (osCisloNode.InnerText == Number)
                {
                    XmlNode node_password = actNode.SelectSingleNode(String.Concat("./Password"));
                    String str_buffer_password = node_password.InnerText;
                    String str_Password = "";
                    for (Int16 i = 0; i < str_buffer_password.Length; i++)
                    {
                        str_Password = String.Concat(str_buffer_password.Substring(i, 1), str_Password);
                    }
                    if ((Password == str_Password) || (str_Password == ""))
                    {
                        this.Number = Number;
                        XmlNode node_surname = actNode.SelectSingleNode(String.Concat("./Surname"));
                        this.Surname = node_surname.InnerText;
                        XmlNode privileges_node = actNode.SelectSingleNode(String.Concat("./Privileges"));
                        this.Privileges = privileges_node.InnerText;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return IsValid;
        }

        public Boolean PasswordValidation(String Password)
        {
            Boolean retVal = false;

            try
            {
                XmlNode ConfigurationNode = this.ELxmlConfig.SelectSingleNode("./Configuration");
                foreach (XmlNode actOperatorNode in ConfigurationNode.ChildNodes)
                {
                    if (actOperatorNode.SelectSingleNode("./ValidationString").InnerText.Trim() == Password)
                    {
                        this.Number = actOperatorNode.Name.Substring(1);
                        this.Surname = actOperatorNode.SelectSingleNode("./Surname").InnerText.Trim();
                        this.Privileges = actOperatorNode.SelectSingleNode("./AdminRights").InnerText.Trim();
                        retVal = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return retVal;
        }

        public Boolean DeleteOperator()
        {
            foreach (XmlNode actOperatorNode in this.OperatorsNode)
            {
                if (actOperatorNode.SelectSingleNode("./Surname").InnerText == this.Surname)
                {
                    this.OperatorsNode.RemoveChild(actOperatorNode);
                    this.XMLConfig.Save(this.GetFilePathFromURI(this.XMLConfig.BaseURI));
                    return true;
                }
            }
            return false;
        }
        public Boolean DeleteOperator(String Surname)
        {
            this.Surname = Surname;
            this.DeleteOperator();
            return true;
        }        

        private String GetFilePathFromURI(String URIPath)
        {
            if (URIPath.IndexOf(@"file:///") == 0)
            {
                return (URIPath.Substring(8));
            }
            else if (URIPath.IndexOf(@"file://") == 0)
            {
                return (URIPath.Substring(5));
            }
            return URIPath;
        }
    }
}

using System;
using System.Security;
using System.Xml;

namespace NewLogin
{
    public struct OperatorData
    {
        public String Name;
        public String Number;
        public SecureString Password;        
        public String Stations;
        public String Privileges;
    }

    struct StationData
    {
        public String Name;
        public String Identifier;
    }
    public class Login
    {
        String StationName;

        //SecureString TypedPassword;
        String ConfigFilePath = @"S:\Manufacturing_Engineering\Public\Kolman Vladimir\SigmaSure\UserConfigurations\NewLogin.xml";
        XmlDocument myXMLDoc;

        public Login()
        {
            this.myXMLDoc = new XmlDocument();
            this.myXMLDoc.Load(ConfigFilePath);
        }

        public Login(String StationName)
        {
            this.StationName = StationName;
            this.myXMLDoc = new XmlDocument();
            this.myXMLDoc.Load(ConfigFilePath);
        }

        public OperatorData[] GetOperatorsAllData()
        {
            return this.GetOperatorsAllData("");
        }

        public OperatorData[] GetOperatorsAllData(String StationName)
        {
            OperatorData[] OpList = { };

            XmlNode myXMLOpNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Operators");
            XmlNode myXMLStNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Stations");

            foreach (XmlNode actNode in myXMLOpNode)
            {
                OperatorData myOPData = this.getOperatorData(actNode);
                if (this.IsInStationsString(StationName, myOPData.Stations) || (StationName == ""))
                {
                    Array.Resize(ref OpList, OpList.Length + 1);
                    OpList.SetValue(myOPData, OpList.Length - 1);
                }
            }

            return OpList;
        }

        public OperatorData GetOperatorData(String OperatorName, String OperatorNumber = "")
        {
            if (OperatorNumber == "")
                return getOperatorData(OperatorName);
            else
                return getOperatorData("", OperatorNumber);
        }

        public String[] GetStationsList()
        {
            OperatorData myOD = new OperatorData();
            myOD.Stations = "all";
            return this.GetStationsList(myOD);
        }

        public String[] GetStationsList(OperatorData Operator)
        {
            String[] StationList = { };

            XmlNode myXMLStNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Stations");

            if (Operator.Stations == "all")
            {
                foreach (XmlNode actStationNode in myXMLStNode.ChildNodes)
                {
                    Array.Resize(ref StationList, StationList.Length + 1);
                    StationList.SetValue(actStationNode.InnerText, StationList.Length - 1);
                }
            }
            else
            {
                String[] StationIdentifiers = Operator.Stations.Split(';');
                foreach (String actStationIdentifier in StationIdentifiers)
                {
                    foreach (XmlNode actStationNode in myXMLStNode.ChildNodes)
                    {
                        StationData actSD = this.getStationData(actStationNode);
                        if (actSD.Identifier == actStationIdentifier)
                        {
                            Array.Resize(ref StationList, StationList.Length + 1);
                            StationList.SetValue(actSD.Name, StationList.Length - 1);
                        }
                    }
                }
            }
            return StationList;
        }

        public void SaveOperatorData(OperatorData Operator)
        {
            this.saveOperatorData(Operator);
        }

        public void SaveOperatorData(OperatorData Operator, Array StationNames)
        {
            Operator.Stations = "";
            foreach (String actStationName in StationNames)
            {
                if (!IsStationInList(actStationName))
                {
                    this.saveNewStation(actStationName);
                }
                StationData actSD = getStationData(actStationName);
                if (Operator.Stations == "")
                    Operator.Stations = actSD.Identifier;
                else
                    Operator.Stations = String.Concat(Operator.Stations, ";", actSD.Identifier);
            }

            this.saveOperatorData(Operator);
        }

        private void saveNewStation(String StationName)
        {
            XmlNode myXMLStNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Stations");
            int actIndex = myXMLStNode.ChildNodes.Count + 1;
            XmlNode nodeToAdd = myXMLDoc.CreateNode("element", String.Concat("s", actIndex.ToString("D4")), "");
            nodeToAdd.InnerText = StationName;
            myXMLStNode.AppendChild(nodeToAdd);
            myXMLDoc.Save(this.ConfigFilePath);
            myXMLDoc.Load(this.ConfigFilePath);
        }

        public Boolean IsStationInList(String StationName)
        {
            Boolean retval = false;

            XmlNode myXMLStNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Stations");

            foreach (XmlNode actStationNode in myXMLStNode.ChildNodes)
            {
                StationData actSD = this.getStationData(actStationNode);
                if (actSD.Name == StationName)
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }

        private StationData getStationData(XmlNode StationNode)
        {
            StationData retVal = new StationData();
            retVal.Identifier = StationNode.Name.Substring(1);
            retVal.Name = StationNode.InnerText.Trim();
            return retVal;
        }
        private StationData getStationData(String StationName)
        {
            StationData retVal = new StationData();
            XmlNode myXMLStNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Stations");
            foreach (XmlNode actStationNode in myXMLStNode.ChildNodes)
            {
                retVal = this.getStationData(actStationNode);
                if (retVal.Name == StationName)
                    return retVal;

            }
            return new StationData();
        }

        private OperatorData getOperatorData(String OperatorName)
        {
            XmlNode myXMLOpNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Operators");
            foreach (XmlNode actOpNode in myXMLOpNode.ChildNodes)
            {
                OperatorData actOD = this.getOperatorData(actOpNode);
                if (actOD.Name == OperatorName)
                    return actOD;
            }
            return new OperatorData();
        }

        private OperatorData getOperatorData(String OperatorName, String OperatorNumber)
        {
            XmlNode myXMLOpNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Operators");
            foreach (XmlNode actOpNode in myXMLOpNode.ChildNodes)
            {
                OperatorData actOD = this.getOperatorData(actOpNode);
                if (actOD.Number == OperatorNumber)
                    return actOD;
            }
            return new OperatorData();
        }

        private OperatorData getOperatorData(XmlNode OperatorNode)
        {
            OperatorData retVal = new OperatorData();
            retVal.Name = OperatorNode.SelectSingleNode("Name").InnerText.Trim();
            retVal.Number = OperatorNode.SelectSingleNode("Number").InnerText.Trim();
            String pass = OperatorNode.SelectSingleNode("Password").InnerText.Trim();
            retVal.Password = new SecureString();
            for (int i = 0; i < pass.Length; i++)
            {
                retVal.Password.AppendChar(pass[i]);
            }
            retVal.Stations = OperatorNode.SelectSingleNode("Stations").InnerText.Trim();
            return retVal;
        }

        private void saveOperatorData(OperatorData OperatorDataToSave)
        {
            XmlNode myXMLOpNode = myXMLDoc.SelectSingleNode("NewLoginConfiguration/Operators");

            OperatorData foundedOD = this.getOperatorData(OperatorDataToSave.Name);
            if (foundedOD.Name == "")
            {
                XmlNode OperatorNodeToAdd = myXMLDoc.CreateNode("element", "Operator", "");

                XmlNode nodeNameToAdd = myXMLDoc.CreateNode("element", "Name", "");
                nodeNameToAdd.InnerText = OperatorDataToSave.Name;
                OperatorNodeToAdd.AppendChild(nodeNameToAdd);

                XmlNode nodeNumberToAdd = myXMLDoc.CreateNode("element", "Number", "");
                nodeNumberToAdd.InnerText = OperatorDataToSave.Number;
                OperatorNodeToAdd.AppendChild(nodeNumberToAdd);

                XmlNode nodeStationsToAdd = myXMLDoc.CreateNode("element", "Stations", "");
                nodeStationsToAdd.InnerText = OperatorDataToSave.Stations;
                OperatorNodeToAdd.AppendChild(nodeStationsToAdd);

                XmlNode nodePasswordToAdd = myXMLDoc.CreateNode("element", "Password", "");
                nodeStationsToAdd.InnerText = "";
                OperatorNodeToAdd.AppendChild(nodePasswordToAdd);

                myXMLOpNode.AppendChild(OperatorNodeToAdd);
            }
            else
            {
                foreach (XmlNode actOpNode in myXMLOpNode.ChildNodes)
                {
                    OperatorData actOD = this.getOperatorData(actOpNode);
                    if (actOD.Name == OperatorDataToSave.Name)
                    {
                        actOpNode.SelectSingleNode("Number").InnerText = OperatorDataToSave.Number;
                        actOpNode.SelectSingleNode("Stations").InnerText = OperatorDataToSave.Stations;
                        actOpNode.SelectSingleNode("Password").InnerText = "";
                    }
                }
            }

            myXMLDoc.Save(this.ConfigFilePath);
            myXMLDoc.Load(this.ConfigFilePath);
        }

        private Boolean IsInStationsString(String StationName, String StationsString)
        {
            Boolean retVal = false;
            StationData actSD = this.getStationData(StationName);

            Array ar_stationIndexes = StationsString.Split(';');

            foreach (String actStationIndex in ar_stationIndexes)
            {
                if (actSD.Identifier == actStationIndex)
                    return true;
            }

            return retVal;
        }
    }
}

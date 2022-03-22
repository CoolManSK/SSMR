using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SigmaSureManualReportGenerator
{
    class StationConfig
    {
        public StationConfig()
        {
            Int32 n_majorOSVersion = Environment.OSVersion.Version.Major;

            if (n_majorOSVersion == 5) this.ConfigPath = @"C:\Documents and Settings\All Users\Application Data\SSManualReportGenerator\";
            else if (n_majorOSVersion == 6) this.ConfigPath = @"C:\Users\Public\SSManualReportGenerator\";
            else
            {
                MessageBox.Show("Neznama verzia operacneho systemu. Zavolajte prosim testovacieho inziniera");
                return;
            }

            StationConfigXML.Load(String.Concat(this.ConfigPath, StationConfigFileName));
        }

        private readonly String ConfigPath = "";
        private readonly String StationConfigFileName = "StationConfiguration.xml";
        public readonly XmlDocument StationConfigXML = new XmlDocument();


        public struct SerialNumberIP
        {
            public String SerialNumber;
            public String ProductID;
            public String TestType;
            public String Operator;
            public String StartTime;
        }

        public struct StationInfos
        {
            public String Name;
            public String GUID;
            public struct StationProperty
            {
                public string Name;
                public string Value;
            }
            public StationProperty[] StationProperties;
        }
        public struct PropertyInfo
        {
            private String serialNumber;
            private String propertyName;
            private String propertyValue;

            public string SerialNumber { get => serialNumber; set => serialNumber = value; }
            public string PropertyName { get => propertyName; set => propertyName = value; }
            public string PropertyValue { get => propertyValue; set => propertyValue = value; }
        }
        //private PropertyInfo[] myPIs = { };

        public void MyValidation()
        {
            XmlNode BatchModeAvailableTestsNode = this.StationConfigXML.SelectSingleNode("./Configuration/BatchModeAvailableTests");
            if (BatchModeAvailableTestsNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "BatchModeAvailableTests", "");
                XmlNode childElToAdd = this.StationConfigXML.CreateNode("element", "BurnIn", "");
                childElToAdd.InnerText = "1";
                elToAdd.AppendChild(childElToAdd);
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode HistorySerialNumbersCountNode = this.StationConfigXML.SelectSingleNode("./Configuration/HistorySerialNumbersCount");
            if (HistorySerialNumbersCountNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "HistorySerialNumbersCount", "");
                elToAdd.InnerText = "10";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode HistorySerialNumbersSortingNode = this.StationConfigXML.SelectSingleNode("./Configuration/HistorySerialNumbersSorting");
            if (HistorySerialNumbersSortingNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "HistorySerialNumbersSorting", "");
                elToAdd.InnerText = "1";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode SerialRepeatCheckingEnabled = this.StationConfigXML.SelectSingleNode("./Configuration/SerialRepeatCheckingEnabled");
            if (SerialRepeatCheckingEnabled == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "SerialRepeatCheckingEnabled", "");
                elToAdd.InnerText = "Y";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode BelMESStateNode = this.StationConfigXML.SelectSingleNode("./Configuration/BelMESState");
            if (BelMESStateNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "BelMESState", "");
                elToAdd.InnerText = "N";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode BelMESMessageWarningsNode = this.StationConfigXML.SelectSingleNode("./Configuration/BelMESMessageWarnings");
            if (BelMESMessageWarningsNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "BelMESMessageWarnings", "");
                elToAdd.InnerText = "N";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode AllowStationModeChangeNode = this.StationConfigXML.SelectSingleNode("./Configuration/AllowStationModeChange");
            if (AllowStationModeChangeNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "AllowStationModeChange", "");
                elToAdd.InnerText = "N";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode ExtraLoginEnabledNode = this.StationConfigXML.SelectSingleNode("./Configuration/ExtraLoginEnabled");
            if (ExtraLoginEnabledNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "ExtraLoginEnabled", "");
                elToAdd.InnerText = "N";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode CommandColorNode = this.StationConfigXML.SelectSingleNode("./Configuration/Colors/CommandColor");
            if (CommandColorNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode colorsnode = this.StationConfigXML.SelectSingleNode("./Configuration/Colors");
                if (colorsnode == null)
                {
                    XmlNode elToAdd1 = this.StationConfigXML.CreateNode("element", "Colors", "");
                    confignode.AppendChild(elToAdd1);
                    this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
                }

                colorsnode = this.StationConfigXML.SelectSingleNode("./Configuration/Colors");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "CommandColor", "");
                elToAdd.InnerText = "Red";
                colorsnode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode PartNoNeededNode = this.StationConfigXML.SelectSingleNode("./Configuration/PartNoNeeded");
            if (PartNoNeededNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "PartNoNeeded", "");
                elToAdd.InnerText = "Y";
                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode SimpleModeNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode");
            if (SimpleModeNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "SimpleMode", "");

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode SerialNumbersInProcessNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/SerialNumbersInProcess");
            if (SerialNumbersInProcessNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "SerialNumbersInProcess", "");

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode SimpleModeAvailableNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/Available");
            if (SimpleModeAvailableNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "Available", "");
                elToAdd.InnerText = "N";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode SimpleModeBatchAvailableNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/BatchModeAvailable");
            if (SimpleModeBatchAvailableNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "BatchModeAvailable", "");
                elToAdd.InnerText = "N";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode JobIDCheckingNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/JobIDChecking");
            if (JobIDCheckingNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "JobIDChecking", "");
                elToAdd.InnerText = "N";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode StationNode = this.StationConfigXML.SelectSingleNode("./Configuration/Station");
            if (StationNode == null)
            {
                foreach (XmlNode actNode in StationNode)
                {
                    XmlNode BarcodeNode = actNode.SelectSingleNode("./Barcode");
                    if (BarcodeNode == null)
                    {
                        XmlNode newBarcodeNode = this.StationConfigXML.CreateNode("element", "Barcode", "");
                        actNode.AppendChild(newBarcodeNode);
                        this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
                    }
                }
            }

            XmlNode TcpClientNode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient");
            if (TcpClientNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode newTcpClientNode = this.StationConfigXML.CreateNode("element", "TcpClient", "");
                confignode.AppendChild(newTcpClientNode);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode TcpClientActiveNode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient/Active");
            if (TcpClientActiveNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "Active", "");
                elToAdd.InnerText = "N";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode TcpClientServerNode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient/Server");
            if (TcpClientServerNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "Server", "");
                elToAdd.InnerText = "localhost";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode TcpClientPortNode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient/Port");
            if (TcpClientPortNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "Port", "");
                elToAdd.InnerText = "8910";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            XmlNode ChecklistImmediatelyStartNode = this.StationConfigXML.SelectSingleNode("./Configuration/ChecklistImmediatelyStart");
            if (ChecklistImmediatelyStartNode == null)
            {
                XmlNode confignode = this.StationConfigXML.SelectSingleNode("./Configuration");

                XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "ChecklistImmediatelyStart", "");
                elToAdd.InnerText = "0";

                confignode.AppendChild(elToAdd);
                this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
            }

            /*
            XmlNode DefaultStationNode = this.StationConfig.SelectSingleNode("./Configuration/Station/Default");
            if (DefaultStationNode == null)
            {
                XmlNode stationNode = this.StationConfig.SelectSingleNode("./Configuration/Station");

                XmlNode elToAdd = this.StationConfig.CreateNode("element", "Default", "");
                while (stationNode.ChildNodes.Count != 0)
                {
                    elToAdd.AppendChild(stationNode.ChildNodes[0]);
                }

                stationNode.RemoveAll();
                stationNode.AppendChild(elToAdd);
                this.StationConfig.Save(this.StationConfig.BaseURI.Substring(this.StationConfig.BaseURI.IndexOf("C:/")));
            }


            XmlNode DefaultStationNodeProperties = this.StationConfig.SelectSingleNode("./Configuration/Station/Default/Properties");
            if (DefaultStationNodeProperties == null)
            {
                XmlNode elToAdd = this.StationConfig.CreateNode("element", "Properties", "");
                elToAdd.AppendChild(this.StationConfig.SelectSingleNode("./Configuration/Properties/Station_Name"));

                this.StationConfig.SelectSingleNode("./Configuration/Station/Default").AppendChild(elToAdd);

                this.StationConfig.Save(this.StationConfig.BaseURI.Substring(this.StationConfig.BaseURI.IndexOf("C:/")));
            }
            */
        }

        public Array GetSerialNumbersInProcess()
        {
            SerialNumberIP[] retArray = { };

            XmlNode SerialNumbersInProcessNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/SerialNumbersInProcess");

            foreach (XmlNode actNode in SerialNumbersInProcessNode.ChildNodes)
            {
                SerialNumberIP actSNIP = new SerialNumberIP();

                actSNIP.SerialNumber = actNode.SelectSingleNode("./SerialNumber").InnerText;
                actSNIP.ProductID = actNode.SelectSingleNode("./ProductID").InnerText;
                actSNIP.TestType = actNode.SelectSingleNode("./TestType").InnerText;
                actSNIP.Operator = actNode.SelectSingleNode("./Operator").InnerText;
                actSNIP.StartTime = actNode.SelectSingleNode("./StartTime").InnerText;
                                
                Array.Resize(ref retArray, retArray.Length + 1);
                retArray.SetValue(actSNIP, retArray.GetUpperBound(0));
            }

            return retArray;
        }

        public void SaveSerialNumberIP(SerialNumberIP SNIP)
        {
            XmlNode elToAdd = this.StationConfigXML.CreateNode("element", "SerialNumberIP", "");

            XmlNode childElToAdd = this.StationConfigXML.CreateNode("element", "SerialNumber", "");
            childElToAdd.InnerText = SNIP.SerialNumber;
            elToAdd.AppendChild(childElToAdd);

            childElToAdd = this.StationConfigXML.CreateNode("element", "ProductID", "");
            childElToAdd.InnerText = SNIP.ProductID;
            elToAdd.AppendChild(childElToAdd);

            childElToAdd = this.StationConfigXML.CreateNode("element", "TestType", "");
            childElToAdd.InnerText = SNIP.TestType;
            elToAdd.AppendChild(childElToAdd);

            childElToAdd = this.StationConfigXML.CreateNode("element", "Operator", "");
            childElToAdd.InnerText = SNIP.Operator;
            elToAdd.AppendChild(childElToAdd);

            childElToAdd = this.StationConfigXML.CreateNode("element", "StartTime", "");
            childElToAdd.InnerText = SNIP.StartTime;
            elToAdd.AppendChild(childElToAdd);

            XmlNode ParrentNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/SerialNumbersInProcess");
            ParrentNode.AppendChild(elToAdd);

            this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
        }

        public Boolean IsInProcessAndDelete(String SerialNumber, String TestType, Boolean Delete)
        {
            Boolean retVal = false;
            XmlNode SerialNumbersInProcessNode = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/SerialNumbersInProcess");
            foreach (XmlNode actNode in SerialNumbersInProcessNode.ChildNodes)
            {
                String actSN = actNode.SelectSingleNode("./SerialNumber").InnerText;
                if (actSN == SerialNumber)
                {
                    String actTestType = actNode.SelectSingleNode("./TestType").InnerText;
                    if (actTestType == TestType)
                    {
                        if (Delete)
                        {
                            SerialNumbersInProcessNode.RemoveChild(actNode);
                            this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
                        }
                    }
                    return true;
                }
            }
            return retVal;
        }

        public String GetMode()
        {
            return this.StationConfigXML.SelectSingleNode("./Configuration/Mode").InnerText.Trim();
        }

        public Array GetProperties()
        {
            String[,] retArray = { };

            int counter = 0;
            XmlNode PropNode = this.StationConfigXML.SelectSingleNode("./Configuration/Properties");
            retArray = new String[PropNode.ChildNodes.Count, 2];
            foreach (XmlNode actPropNode in PropNode.ChildNodes)
            {
                retArray[counter, 0] = actPropNode.Name;
                retArray[counter, 1] = actPropNode.InnerText;
                counter++;
            }

            return retArray;
        }

        public StationInfos GetStationInfosForTest(String TestName)
        {
            StationInfos retVal = new StationInfos();
            XmlNode StationConfigInfosNode = this.StationConfigXML.SelectSingleNode("./Configuration/Station");
            if (StationConfigInfosNode == null) MessageBox.Show("Neexistuje node Configuration/Station v StationConfig subore. Zavolajte prosim testovacieho technika.");
            else
            {
                foreach (XmlNode actNode in StationConfigInfosNode.ChildNodes)
                {
                    if (TestName.Replace(" ", "_") == actNode.Name)
                    {
                        retVal.Name = actNode.SelectSingleNode("./Name").InnerText;
                        retVal.GUID = actNode.SelectSingleNode("./GUID").InnerText;
                        foreach (XmlNode actPropNode in actNode.SelectSingleNode("./Properties").ChildNodes)
                        {
                            StationInfos.StationProperty actSIProp = new StationInfos.StationProperty();
                            actSIProp.Name = actPropNode.Name.Replace("_", " ");
                            actSIProp.Value = actPropNode.InnerText;
                            if (retVal.StationProperties == null)
                            {
                                retVal.StationProperties = new StationInfos.StationProperty[0];
                            }
                            Array.Resize(ref retVal.StationProperties, retVal.StationProperties.Length + 1);
                            retVal.StationProperties.SetValue(actSIProp, retVal.StationProperties.Length - 1);
                        }
                        break;
                    }
                }
                if (retVal.Name == null)
                {
                    XmlNode DefaultStationConfigInfosNode = StationConfigInfosNode.SelectSingleNode("./Default");
                    if (DefaultStationConfigInfosNode == null)
                    {
                        retVal.Name = StationConfigInfosNode.ChildNodes[0].SelectSingleNode("./Name").InnerText;
                        retVal.GUID = StationConfigInfosNode.ChildNodes[0].SelectSingleNode("./GUID").InnerText;
                        foreach (XmlNode actPropNode in StationConfigInfosNode.ChildNodes[0].SelectSingleNode("./Properties").ChildNodes)
                        {
                            StationInfos.StationProperty actSIProp = new StationInfos.StationProperty();
                            actSIProp.Name = actPropNode.Name.Replace("_", " ");
                            actSIProp.Value = actPropNode.InnerText;
                            if (retVal.StationProperties == null)
                            {
                                retVal.StationProperties = new StationInfos.StationProperty[0];
                            }
                            Array.Resize(ref retVal.StationProperties, retVal.StationProperties.Length + 1);
                            retVal.StationProperties.SetValue(actSIProp, retVal.StationProperties.Length - 1);
                        }
                    }
                    else
                    {
                        retVal.Name = DefaultStationConfigInfosNode.SelectSingleNode("./Name").InnerText;
                        retVal.GUID = DefaultStationConfigInfosNode.SelectSingleNode("./GUID").InnerText;
                        foreach (XmlNode actPropNode in DefaultStationConfigInfosNode.SelectSingleNode("./Properties").ChildNodes)
                        {
                            StationInfos.StationProperty actSIProp = new StationInfos.StationProperty();
                            actSIProp.Name = actPropNode.Name.Replace("_", " ");
                            actSIProp.Value = actPropNode.InnerText;
                            if (retVal.StationProperties == null)
                            {
                                retVal.StationProperties = new StationInfos.StationProperty[0];
                            }
                            Array.Resize(ref retVal.StationProperties, retVal.StationProperties.Length + 1);
                            retVal.StationProperties.SetValue(actSIProp, retVal.StationProperties.Length - 1);
                        }
                    }
                }
            }
            return retVal;
        }

        public String GetReportPathDirectory()
        {
            String retVal = "";

            XmlNode reportPathNode = this.StationConfigXML.LastChild.SelectSingleNode("./ReportPath/Path");
            if (reportPathNode != null)
            {
                retVal = reportPathNode.InnerText.Trim();
            }
            return retVal;
        }

        public Boolean SimpleModeBatchAvailable()
        {
            Boolean retval = false;
            XmlNode SimpleModeBatchAvailableNode = this.StationConfigXML.LastChild.SelectSingleNode("./SimpleMode/BatchModeAvailable");
            if (SimpleModeBatchAvailableNode != null)
            {
                if (SimpleModeBatchAvailableNode.InnerText.Trim() == "Y") retval = true;
                else retval = false;
            }
            return retval;
        }

        public Boolean GetSimpleModeJobIDCheckingState()
        {
            String str_JobIDChecking_state = this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/JobIDChecking").InnerText.Trim();
            if (str_JobIDChecking_state == "N") return false;
            else return true;
        }

        public void SetSimpleModeJobIDCheckingState(Boolean JobIDCheckingState)
        {            
            string newState = "";
            if (JobIDCheckingState) newState = "Y";
            else newState = "N";

            this.StationConfigXML.SelectSingleNode("./Configuration/SimpleMode/JobIDChecking").InnerText = newState;
            this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
        }

        public Boolean GetTcpClientActive(ref String Server, ref Int32 Port)
        {
            String strTcpClientActive = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient/Active").InnerText;
            Server = this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient/Server").InnerText;
            Port = Convert.ToInt32(this.StationConfigXML.SelectSingleNode("./Configuration/TcpClient/Port").InnerText);
            if (strTcpClientActive == "Y")
            {                
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean GetChecklistImmediatelyStart()
        {
            String strChecklistImmediatelyStart = this.StationConfigXML.SelectSingleNode("./Configuration/ChecklistImmediatelyStart").InnerText;
            
            if (strChecklistImmediatelyStart == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetChecklistImmediatelyStart(Boolean ChecklistImmediatelyStart)
        {
            string newState = "";
            if (ChecklistImmediatelyStart)
                newState = "1";
            else
                newState = "0";

            this.StationConfigXML.SelectSingleNode("./Configuration/ChecklistImmediatelyStart").InnerText = newState;
            this.StationConfigXML.Save(this.StationConfigXML.BaseURI.Substring(this.StationConfigXML.BaseURI.IndexOf("C:/")));
        }
    }
}

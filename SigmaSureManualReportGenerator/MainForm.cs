﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using SigmaSure;
using System.Deployment.Application;
using System.Threading;


namespace SigmaSureManualReportGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public String ActualProgramVersion = "1.1";

        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        private Boolean eventDisabler = false;
        private Boolean resetingCB_TestType = false;
        private Int16 actualOrder;
        private Int32 lastProductIndex = -1;
        private String actualGrade = "PASS";
        //private Boolean ExtraLoginEnabled = false;
        private Boolean PartNoNeeded = true;

        private readonly Bitmap OKpict = new Bitmap("OK.jpg");
        private readonly Bitmap NOTOKpict = new Bitmap("NOTOK.jpg");
        private readonly Bitmap PASSFAILpict = new Bitmap("BarcodeImages\\PassFail.bmp");
        private readonly Bitmap TESTTYPESpict = new Bitmap("BarcodeImages\\TestTypes.bmp");
        private readonly Bitmap GENERATEREPORTpict = new Bitmap("BarcodeImages\\GenerateReport.bmp");

        private String ConfigPath = "";
        private readonly String StationConfigFileName = "StationConfiguration.xml";
        private readonly String UserConfigFileName = "UserConfiguration.xml";
        private readonly String ProductsConfigFileName = "ProductsConfiguration.xml";
        private readonly XmlDocument StationConfig = new XmlDocument();
        private readonly XmlDocument UserConfig = new XmlDocument();
        private ProductsConfigurationFile ProductsConfig;
        private Boolean CheckingOfSerialNumberTested = true;
        private Boolean IgnoreSNLength = false;
        private Boolean SimpleModeAvailable = false;
        private readonly TxtDatabase TD = new TxtDatabase();

        private Boolean BelMESenabled = true;
        private Boolean BelMESMessageWarnings = true;
        private BelMES BelMESobj = new BelMES("", "", "1");
        private readonly StationConfig myStationConfig = new StationConfig();

        public String EmpRights = "";
           
        /*     
        public struct ChildTestInfo
        {
            public string Name;
            public string Instruction;
            public string PicturePath;
        }
        
        public struct FaultCode
        {
            public String Code;
            public String Description;
        }
        */
        
        private struct StationInfos
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
        /*
        private struct PropertyInfo
        {
            public String SerialNumber;
            public String PropertyName;
            public String PropertyValue;
        }
        private enum HistSNsorting
        {
            FromBiggest,
            FromSmallest,
            Chronological
        }
        */
        private int HSNSorting = 0;

        private String[] ar_LastSerialNumbers = { };

        private partial class ResultGroupBoxSingleMode : ResultGroupBox
        {
            public ResultGroupBoxSingleMode(String Name, String Description, Point Location, Size GBSize) : base(Name, Description, Location, GBSize)
            {
                this.btn_Delete.Click += this.btn_Delete_Click;
            }

            private void btn_Delete_Click(object sender, EventArgs e)
            {
                
                GroupBox myGB = (GroupBox)this.Parent;
                foreach (Object actObject in myGB.Controls)
                {
                    if (typeof(ResultGroupBoxSingleMode) == actObject.GetType())
                    {
                        ResultGroupBoxSingleMode actRGBSM = (ResultGroupBoxSingleMode)actObject;

                        if (actRGBSM == this)
                        {
                            this.Dispose();
                            break;
                        }                       
                    }
                }

                int counter = 1;
                foreach (Object actObject in myGB.Controls)
                {
                    if (typeof(ResultGroupBoxSingleMode) == actObject.GetType())
                    {
                        ResultGroupBoxSingleMode actRGBSM = (ResultGroupBoxSingleMode)actObject;

                        actRGBSM.Location = new Point(10, ((counter - 1) * 53) + 20);
                        counter++;
                    }
                }
            }
        }

        private void Login()
        {
            OperatorLoginForm myOLform;
            if (this.BelMESenabled) myOLform = new OperatorLoginForm(this.UserConfig, this.BelMESobj);            
            else myOLform = new OperatorLoginForm(this.UserConfig);
            myOLform.ShowDialog();
            if (this.BelMESenabled)
            {                
                this.BelMESobj = myOLform.BelMESobj;
                XmlNode URModeNode = this.StationConfig.LastChild.SelectSingleNode("./Mode");
                if (URModeNode == null)
                {
                    this.ErrorMessageBoxShow("V Station configu chyba informacia o mode testu.", true);
                }
                if (URModeNode.InnerText != "") this.BelMESobj.Mode = URModeNode.InnerText;
                if (!this.BelMESobj.Activated)
                {
                    this.BelMESenabled = false;
                }
            }
            this.ResetForm(false);
            if (myOLform.PasswordValidation)
            {
                this.lbl_OperatorNr.Text = myOLform.LoggedOperatorNumber;
                this.lbl_OperatorSurname.Text = myOLform.LoggedOperatorSurname;
                this.gb_Product.Enabled = true;
                this.gb_Barcodes.Enabled = true;
                if (this.SimpleModeAvailable)
                {
                    this.btn_SimpleMode.Enabled = true;
                }
            }
            else
            {
                this.lbl_OperatorNr.Text = "";
                this.lbl_OperatorSurname.Text = "";
                this.gb_Product.Enabled = false;
                this.gb_Barcodes.Enabled = false;
                this.btn_SimpleMode.Enabled = false;
            }

            this.cms_AdminContext.Enabled = false;

            if (this.lbl_OperatorNr.Text != "")
            {
                this.EmpRights = myOLform.Privileges;
                if (myOLform.Privileges == "admin")
                {
                    this.cms_AdminContext.Enabled = true;
                    this.cms_AdminContext.Items[0].Enabled = true;
                    this.cms_AdminContext.Items[1].Enabled = true;
                    this.cms_AdminContext.Items[2].Enabled = true;
                    this.cms_AdminContext.Items[3].Enabled = true;
                    this.cms_AdminContext.Items[4].Enabled = true;                    
                    MessageBox.Show(String.Concat("Number of ProductNo in database: ", this.cb_ProductNo.Items.Count.ToString()));
                }
                else if (myOLform.Privileges == "useradmin")
                {
                    this.cms_AdminContext.Enabled = true;
                    this.cms_AdminContext.Items[0].Enabled = false;
                    this.cms_AdminContext.Items[1].Enabled = false;
                    this.cms_AdminContext.Items[2].Enabled = true;
                    this.cms_AdminContext.Items[3].Enabled = true;
                    this.cms_AdminContext.Items[4].Enabled = true;
                }
                else
                {
                    this.cms_AdminContext.Enabled = false;
                    this.cms_AdminContext.Items[0].Enabled = true;
                    this.cms_AdminContext.Items[1].Enabled = true;
                    this.cms_AdminContext.Items[2].Enabled = true;
                    this.cms_AdminContext.Items[3].Enabled = true;
                    this.cms_AdminContext.Items[4].Enabled = true;
                }                
            }
            this.tb_OrderValue.Focus();
        }
        private Boolean GenerateReport()
        {
            DateTime starttime = DateTime.Now;
            DateTime endtime = DateTime.Now.AddSeconds(5);

            if (this.lbl_JobIDValue.Text.Length != 8)
            {
                this.ErrorMessageBoxShow(String.Concat("Zle zadane Job ID: \"", this.lbl_JobIDValue.Text, "\".\tJobID musi mat 8 znakov. Dvojklikom na hodnotu Job ID ju resetnite a postupujte podla instrukcii zobrazenych cervenym pismom. V pripade, ze sa chyba opakuje zavolajte testovacieho technika. Dakujem.\tProductID: ", this.cb_ProductNo.Text), true);
                return false;
            }

            if (this.IgnoreSNLength == false)
            {
                if (!((this.lbl_SerialNumber.Text.Length == 5) || (this.lbl_SerialNumber.Text.Length == 13)))
                {
                    this.ErrorMessageBoxShow(String.Concat("Zle zadane Serial Number: \"", this.lbl_SerialNumber.Text, "\".\tSerial Number musi mat 5 alebo 13 znakov. Dvojklikom na hodnotu Serial Number ju resetnite a postupujte podla instrukcii zobrazenych cervenym pismom. V pripade, ze sa chyba opakuje zavolajte testovacieho technika. Dakujem.\tProductID: ", this.cb_ProductNo.Text), true);
                    return false;
                }
            }
            
            UnitReport myReport = new UnitReport(starttime, endtime, "D", "", false);

            XmlNode OperatorNameMode = this.StationConfig.SelectSingleNode("./Configuration/OperatorNameMode");
            if (OperatorNameMode == null)
            {
                this.tb_OrderValue.Enabled = false;
                this.ErrorMessageBoxShow("Chyba node OperationNameMode v Station config subore. Prosim, zavolajte testovacieho inziniera.", true);
                return false;
            }

            if (this.StationConfig.SelectSingleNode("./Configuration/OperatorNameMode").InnerText == "Number")
            {
                myReport.Operator.name = this.lbl_OperatorNr.Text;
            }
            else if (this.StationConfig.SelectSingleNode("./Configuration/OperatorNameMode").InnerText == "Surname")
            {
                myReport.Operator.name = this.lbl_OperatorSurname.Text;
            }
            else
            {
                this.ErrorMessageBoxShow("Neznama hodnota OperationNameMode v Station config subore. Prosim, zavolajte testovacieho inziniera.", true);
                this.tb_OrderValue.Enabled = true;
                return false;
            }

            if (this.lbl_SerialNumber.Text.Length == 13)
            {
                String buffer = this.lbl_SerialNumber.Text;
                try
                {
                    int n_buf = Convert.ToInt16(buffer);
                }
                catch
                {
                    this.ErrorMessageBoxShow("Seriove cislo nezodpoveda poziadavkam. Jednotku oskenujte este raz. Ak sa problem opakuje, zavolajte prosim testovacieho inziniera.", true);
                    return false;
                }
                finally
                {
                    this.lbl_JobIDValue.Text = buffer.Substring(0, 8);
                    this.lbl_SerialNumber.Text = buffer.Substring(8);
                }
            }

            myReport.Cathegory = new _Cathegory("Default")
            {
                Product = new _Product(this.cb_ProductNo.Text, this.lbl_SerialNumber.Text)
            };

            myReport.TestRun.name = this.cb_TestType.Text;
            myReport.TestRun.grade = this.tb_OrderValue.Text.ToUpper();
            myReport.starttime = starttime;
            myReport.endtime = endtime;

            if (this.gb_Results.Enabled)
            {
                if (this.gb_Results.Text == "Vysledky")
                {
                    for (int i = 1; i < this.gb_Results.Controls.Count; i++)
                    {
                        String TR_Name = this.gb_Results.Controls[i].Text;
                        String TR_Value = this.gb_Results.Controls[i].Controls["tb_Description"].Text;
                        String TR_usl = "*$*";
                        if (TR_Value != "") TR_usl = "";
                        String TR_grade = "FAIL";
                        myReport.TestRun.AddTestRunChild(TR_Name, starttime, endtime, TR_grade, TR_Value, "", TR_usl);
                        if ((TR_grade != "PASS") && (this.actualGrade == "PASS")) this.actualGrade = TR_grade;
                    }
                }
                else if (this.gb_Results.Text == "Instrukcie")
                {
                    Array myTIs = this.ProductsConfig.GetChildTestsInfos(this.cb_ProductNo.Text, this.cb_TestType.Text);
                    for (int i = 0; i < this.dgv_Instructions.Rows.Count; i++)
                    {
                        ProductsConfigurationFile.ChildTestInfo actCTI = (ProductsConfigurationFile.ChildTestInfo)myTIs.GetValue(i);
                        DataGridViewRow actRow = this.dgv_Instructions.Rows[i];
                        String TR_Name = actCTI.Name.Replace('_',' ').Trim();
                        if (TR_Name.Length < 5)
                            TR_Name = actRow.Cells[1].Value.ToString().Trim();
                        String TR_Value = actRow.Cells[2].Value.ToString();
                        if (actRow.Cells[5].Value.ToString().Trim() != "") TR_Value = actRow.Cells[5].Value.ToString();
                        String TR_usl = "PASS";
                        String TR_grade = actRow.Cells[2].Value.ToString();
                        myReport.TestRun.AddTestRunChild(TR_Name, starttime, endtime, TR_grade, TR_Value, "", TR_usl);
                        if ((TR_grade != "PASS") && (this.actualGrade == "PASS")) this.actualGrade = TR_grade;
                    }
                }
            }

            myReport.TestRun.grade = this.actualGrade;            

            myReport.AddProperty("Work Order", this.lbl_JobIDValue.Text);
            XmlNode URModeNode = this.StationConfig.LastChild.SelectSingleNode("./Mode");
            if (URModeNode == null)
            {
                this.ErrorMessageBoxShow("V Station configu chyba informacia o mode testu.", true);
                return false;
            }
            String URMode = URModeNode.InnerText;
            if (URMode == "")
            {
                myReport.mode = "D";
            }
            else
            {
                myReport.mode = URMode;
            }

            XmlNode actProductNode = this.ProductsConfig.GetProductNode(myReport.Cathegory.Product.PartNo);
            if (actProductNode != null)
            {
                XmlNode propertiesNode = actProductNode.SelectSingleNode("./Properties");
                if (propertiesNode != null)
                {
                    foreach (XmlNode actPropNode in propertiesNode)
                    {
                        myReport.AddProperty(actPropNode.Name, actPropNode.InnerText);
                    }
                }
            }

            /*
            XmlNode assembliesNode = ProductsConfig.LastChild.SelectSingleNode("./Assemblies");
            foreach (XmlNode actAssNode in assembliesNode.ChildNodes)
            {
                if (actAssNode.SelectSingleNode("./Name").InnerText == myReport.Cathegory.Product.PartNo)
                {
                    XmlNode propertiesNode = actAssNode.SelectSingleNode("./Properties");
                    if (propertiesNode != null)
                    {
                        foreach (XmlNode actPropNode in propertiesNode)
                        {
                            myReport.AddProperty(actPropNode.Name, actPropNode.InnerText);
                        }
                    }
                }
            }
            */

            XmlNode actFamilyNode = this.ProductsConfig.GetFamilyNode(myReport.Cathegory.Product.PartNo);
            if (actFamilyNode != null)
            {
                XmlNode propertiesNode = actFamilyNode.SelectSingleNode("./Properties");
                if (propertiesNode != null)
                {
                    foreach (XmlNode actPropNode in propertiesNode)
                    {
                        myReport.AddProperty(actPropNode.Name, actPropNode.InnerText);
                    }
                }
            }

            
            /*
            XmlNode familyProperties = ProductsConfig.LastChild.SelectSingleNode("./Families");
            foreach (XmlNode actFamilyNode in familyProperties.ChildNodes)
            {
                if (actFamilyNode.SelectSingleNode("./Name").InnerText == myReport.Cathegory.Product.PartNo)
                {
                    XmlNode propertiesNode = actFamilyNode.SelectSingleNode("./Properties");
                    if (propertiesNode != null)
                    {
                        foreach (XmlNode actPropNode in propertiesNode)
                        {
                            myReport.AddProperty(actPropNode.Name, actPropNode.InnerText);
                        }
                    }
                }
            }
            */

            /*
            if (SpecialRequirements.IsActive(this.ProductsConfig, this.lbl_SerialNumber.Text, "CustomSerialNumber"))
            {
                InputBox_Form myForm = new InputBox_Form("Custom Serial Number", "Oskenujte Custom Serial Number");
                while (myForm.Answer == "")
                {
                    myForm.ShowDialog();                    
                }
                myReport.AddProperty("CustomSerialNumber", myForm.Answer);
                myForm.Dispose();
            }
            */

            XmlNode commonPropertiesNode = this.StationConfig.LastChild.SelectSingleNode("./Properties");
            foreach (XmlNode actAssNode in commonPropertiesNode.ChildNodes)
            {
                String actNameNode = actAssNode.Name.Replace('_', ' ');
                myReport.AddProperty(actNameNode, actAssNode.InnerText);
            }

            StationInfos actSI = this.GetStationInfosForTest(this.cb_TestType.Text);

            String StationName = actSI.Name;
            String StationGUID = actSI.GUID;
            foreach (StationInfos.StationProperty actProp in actSI.StationProperties)
            {
                myReport.AddProperty(actProp.Name, actProp.Value);
            }            
            myReport.Station = new _Station(StationGUID, StationName);
            XmlNode reportPathNode = this.StationConfig.LastChild.SelectSingleNode("./ReportPath/Path");
            if (myReport.TestRun.grade == "FAIL")
            {
                myReport.AddProperty("To Repair", "Yes");
            }
            Array XmlReportLines;
            String XmlReportContent = "";
            try
            {
                XmlReportLines = myReport.GetXMLReport(reportPathNode.InnerText.ToString(), true);
                foreach (String ActLine in XmlReportLines)
                {
                    XmlReportContent = String.Concat(XmlReportContent, ActLine);
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessageBoxShow(ex.Message, true);
            }

            if (this.BelMESenabled)
            {
                if (myReport.TestRun.grade.ToUpper().Trim() == "PASS") myReport.TestRun.grade = "Pass";
                if (myReport.TestRun.grade.ToUpper().Trim() == "FAIL") myReport.TestRun.grade = "Fail";
                if (!this.BelMESobj.SetActualResult(myReport.Cathegory.Product.SerialNo, this.cb_TestType.Text.Trim(), String.Concat(myReport.TestRun.grade.Trim(), "ed"), XmlReportContent))
                {

                }
            }

            if (this.actualGrade == "PASS")
            {
                if (this.CheckingOfSerialNumberTested)
                {
                    this.TD.SaveSerialNumberAndTestTypeToLogFile(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim(), this.cb_TestType.Text);
                }
            }

            while (this.gb_Results.Controls.Count > 1)
            {
                if (this.gb_Results.Controls[0] != this.dgv_Instructions)
                {
                    while (this.gb_Results.Controls[0].Controls.Count > 0)
                    {
                        this.gb_Results.Controls[0].Controls[0].Dispose();
                    }
                    this.gb_Results.Controls[0].Dispose();
                }
                else
                {
                    while (this.gb_Results.Controls[1].Controls.Count > 0)
                    {
                        this.gb_Results.Controls[1].Controls[0].Dispose();
                    }
                    this.gb_Results.Controls[1].Dispose();
                }                
            }
            this.gb_Results.Enabled = false;

            XmlNode HistorySNcount = this.StationConfig.SelectSingleNode("./Configuration/HistorySerialNumbersCount");
            if (HistorySNcount == null)
            {
                this.ErrorMessageBoxShow("V station config subore chyba informacia o maximalnom pocte seriovych cisiel v historii. Zavolajte prosim testovacieho inziniera.", true);
                this.Dispose();
            }
            Int32 n_HistorySNmaxCount = Convert.ToInt32(HistorySNcount.InnerText);
            if (this.ar_LastSerialNumbers.Length < n_HistorySNmaxCount)
            {
                Array.Resize(ref this.ar_LastSerialNumbers, this.ar_LastSerialNumbers.Length + 1);
            }
            for (Int32 i = this.ar_LastSerialNumbers.Length; i > 1; i--)
            {
                this.ar_LastSerialNumbers.SetValue(this.ar_LastSerialNumbers.GetValue(i - 2), i - 1);
            }
            this.ar_LastSerialNumbers.SetValue(this.lbl_SerialNumber.Text, 0);

            this.eventDisabler = true;
            this.cb_LastSerialNumbers.Items.Clear();
            if (this.HSNSorting < 2)
            {
                Array.Sort(this.ar_LastSerialNumbers);
                if (this.HSNSorting == 1)
                {
                    Array.Reverse(this.ar_LastSerialNumbers);
                }
            }
            foreach (String actLastSN in this.ar_LastSerialNumbers)
            {
                this.cb_LastSerialNumbers.Items.Add(actLastSN);
            }
            
            this.cb_LastSerialNumbers.SelectedIndex = 0;
            this.eventDisabler = false;

            this.lbl_SerialNumber.Text = "";

            this.pb_SerialNumber.Image = this.NOTOKpict;
            this.actualOrder = 4;
            this.lbl_ScanEditOrder.Text = String.Concat("Report so SN: ", myReport.Cathegory.Product.SerialNo, " bol uspesne vytvoreny. Zoskenujte alebo zadajte dalsie seriove cislo produktu:");
            this.ResetOrderTextBox();
            this.actualGrade = "PASS";
            return true;
        }
        private void ResetOrderTextBox()
        {
            this.tb_OrderValue.Text = "";
            this.tb_OrderValue.Focus();
        }
        /*
        private XmlNode GetProductNode(String ProductNo)
        {
            XmlNode AssembliesNode = this.ProductsConfig.SelectSingleNode("./Configuration/Assemblies");
            foreach (XmlNode actProdNode in AssembliesNode.ChildNodes)
            {
                if (actProdNode.SelectSingleNode("./Name").InnerText == ProductNo) return actProdNode;
            }
            return null;
        }
        private XmlNode GetFamilyNode(String ProductNo)
        {
            XmlNode FamiliesNode = this.ProductsConfig.SelectSingleNode("./Configuration/Families");
            foreach (XmlNode actFamilyNode in FamiliesNode)
            {
                String actFamilyNodeName = actFamilyNode.SelectSingleNode("./Name").InnerText;
                if (actFamilyNodeName.IndexOf('%') == 0)
                {
                    if ((actFamilyNodeName.Length - 1) > ProductNo.Length) continue;
                    if (ProductNo.IndexOf(actFamilyNodeName.Substring(1)) > -1) return actFamilyNode;
                }
                else
                {
                    if (actFamilyNodeName.Length > ProductNo.Length) continue;
                    if (actFamilyNodeName == ProductNo.Substring(0, actFamilyNodeName.Length)) return actFamilyNode;
                }

            }
            return null;
        }
        
        public Boolean BelMesActivated(String ProductNo)
        {
            Boolean retval = false;

            XmlNode myProdNode = this.ProductsConfig.GetFamilyNode(ProductNo);
            if (this.ProductsConfig.GetProductNode(ProductNo) != null)
            {
                if (myProdNode.SelectSingleNode("./BelMESActivated") != null)
                {
                    String str_buffer = myProdNode.SelectSingleNode("./BelMESActivated").InnerText;
                    if (str_buffer == "Y") retval = true;
                }
            }
            return retval;
        }
        */

        /*
        private void CheckFormDataForReportAndBatchSNEnter()
        {
            this.CheckFormDataForReportAndBatchSNEnter("default");
        }
        */

        private void CheckFormDataForReportAndBatchSNEnter(String warningInfo)
        {
            this.btn_BatchMode.Enabled = false;
            this.btn_GenerateReport.Enabled = false;
            this.btn_SimpleMode.Enabled = false;
            if ((this.lbl_JobIDValue.Text != "")
                && (this.cb_TestType.SelectedIndex != -1))
                //&& (this.gb_Results.Text == "Vysledky"))
            {                
                if (!(this.PartNoNeeded && (this.cb_ProductNo.SelectedIndex == 0)))
                {                        
                    XmlNode BatchModeAvailableTestsNode = this.StationConfig.SelectSingleNode(String.Concat("./Configuration/BatchModeAvailableTests/", this.cb_TestType.Text.Trim().Replace(' ', '_')));
                    if (BatchModeAvailableTestsNode != null)
                    {
                        if (BatchModeAvailableTestsNode.InnerText == "1")
                        {
                            this.btn_BatchMode.Enabled = true;
                        }
                    }
                        
                    if (this.BelMESenabled)
                    {
                        String strProdIDbuff = this.cb_ProductNo.Text.Trim();
                        if (this.lbl_SerialNumber.Text.Trim().Length > 0)
                        {
                            if (!this.BelMESobj.BelMESAuthorization(String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()), this.cb_TestType.Text.Trim(), ref strProdIDbuff, "", true, false, warningInfo))
                            {
                                /*
                                String MsgToShow = String.Concat("Vyrobok so seriovym cislom \"", this.lbl_SerialNumber.Text.Trim(), "\" zo zakazky \"", this.lbl_JobIDValue.Text.Trim(), "\" nie je mozne otestovat.\nBelMES failure: ", this.BelMESobj.Authorization.strResult);
                                MessageBox.Show(MsgToShow);
                                */
                                this.lbl_SerialNumber_DoubleClick(new object(), new EventArgs());
                                /*
                                this.pb_SerialNumber.Image = this.NOTOKpict;
                                */
                            }
                        }
                    }
                    if (this.lbl_SerialNumber.Text != "")
                    {
                        this.btn_GenerateReport.Enabled = true;
                    }
                }
                else
                {
                   
                    this.ShowBarcodePicture(null);
                }                
            }
            else
            {
                String strProdIDbuff = this.cb_ProductNo.Text.Trim();
                if (this.BelMESenabled && (this.BelMESobj.Authorization.strSerialNumber != null)) this.BelMESobj.BelMESAuthorization(String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()), this.cb_TestType.Text.Trim(), ref strProdIDbuff, "", true, false, "warning2");
                if (this.cb_TestType.SelectedIndex == -1)
                {
                    this.eventDisabler = true;
                    this.lbl_SerialNumber.Text = "";
                    this.eventDisabler = false;
                }
            }
            if (this.SimpleModeAvailable) this.btn_SimpleMode.Enabled = true;

        }  
        
        public void ErrorMessageBoxShow(String Message, Boolean onScreen)
        {
            if (onScreen)
            {
                MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Int32 i = 0;
            this.SaveErrorToLogFile(Message);
            while (i < 9999999) i++;
            this.tb_OrderValue.SelectAll();
            this.tb_OrderValue.Focus();
            this.Focus();
        }
        private void ResetForm(Boolean ResetOperator)
        {
            if (PartNoNeeded) this.cb_ProductNo.SelectedIndex = -1;
            this.pB_Assembly.Image = this.NOTOKpict;
            this.lbl_JobIDValue.Text = "";
            this.pB_JobID.Image = this.NOTOKpict;
            this.cb_TestType.SelectedIndex = -1;
            this.pb_TestType.Image = this.NOTOKpict;
            this.lbl_SerialNumber.Text = "";
            this.pb_SerialNumber.Image = this.NOTOKpict;

            this.lbl_ScanEditOrder.Text = "Zoskenujte 2D kod na vyrobku alebo zadajte nazov produktu alebo vyberte produkt z ponuky:";

            for (Int32 i = this.gb_Results.Controls.Count - 1; i != -1; i--)
            {
                if (this.gb_Results.Controls[i] != this.dgv_Instructions)
                {
                    this.gb_Results.Controls[i].Dispose();
                }
            }
            this.gb_Results.Enabled = false;

            this.tb_OrderValue.Text = "";
            this.tb_OrderValue.Focus();

            if (ResetOperator)
            {
                this.lbl_OperatorNr.Text = "";
                this.lbl_OperatorSurname.Text = "";
            }
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

        private void ApplySettings()
        {
            try 
            {
                if (this.StationConfig.SelectSingleNode("./Configuration/SerialRepeatCheckingEnabled").InnerText == "Y")
                {
                    this.CheckingOfSerialNumberTested = true;
                }
                else if (this.StationConfig.SelectSingleNode("./Configuration/SerialRepeatCheckingEnabled").InnerText == "N")
                {
                    this.CheckingOfSerialNumberTested = false;
                }
                else
                {
                    this.CheckingOfSerialNumberTested = true;
                }

                if (this.StationConfig.SelectSingleNode("./Configuration/BelMESState").InnerText == "Y")
                {
                    this.BelMESenabled = true;
                    if (this.StationConfig.SelectSingleNode("./Configuration/BelMESMessageWarnings").InnerText == "Y")
                    {
                        this.BelMESMessageWarnings = true;
                    }
                    else
                    {
                        this.BelMESMessageWarnings = false;
                    }

                    if (this.Text.IndexOf(" BelMES ") == -1)
                    {
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(@"BelMESCommon.dll");
                        this.Text = String.Concat(this.Text, ", BelMES v.", myFileVersionInfo.FileVersion, ", WinUser: ", Environment.UserName);
                    }
                    this.BelMESobj = new BelMES(this.lbl_StationName.Text.Trim(), this.ConfigPath, this.ActualProgramVersion);
                    this.BelMESobj.WarningMessages = this.BelMESMessageWarnings;
                    XmlNode URModeNode = this.StationConfig.LastChild.SelectSingleNode("./Mode");
                    if (URModeNode == null)
                    {
                        this.ErrorMessageBoxShow("V Station configu chyba informacia o mode testu.", true);
                    }
                    if (URModeNode.InnerText != "") this.BelMESobj.Mode = URModeNode.InnerText;
                    if (!this.BelMESobj.Activated) this.BelMESenabled = false;
                    else
                    {
                        if (!System.IO.File.Exists(@"C:\BelMESCommon\BelMES.ini"))
                        {
                            if (!Directory.Exists(@"C:\BelMESCommon"))
                            {
                                try
                                {
                                    Directory.CreateDirectory(@"C:\BelMESCommon");
                                }
                                catch
                                {
                                    MessageBox.Show("Nie je mozne vytvorit adresar BelMESCommon na disku C. Zavolajte prosim testovacieho technika. Aplikacia sa teraz zavrie.", "CHYBA", MessageBoxButtons.OK);
                                    Application.Exit();
                                }
                            }
                            try
                            {
                                System.IO.File.Copy(@"\\dcafs3\Testing_SRO\TESTING\TestStand\BelMESCommon\BelMES.ini", @"C:\BelMESCommon\BelMES.ini", true);
                            }
                            catch
                            {
                                MessageBox.Show("Nie je mozne skopirovat subor BelMES.ini na disk C. Zavolajte prosim testovacieho technika. Aplikacia sa teraz zavrie.", "CHYBA", MessageBoxButtons.OK);
                                Application.Exit();
                            }
                        }

                        if (!System.IO.File.Exists(@"C:\BelMESCommon\BelMESESD.ini"))
                        {
                            try
                            {
                                System.IO.File.Copy(@"\\dcafs3\Testing_SRO\TESTING\TestStand\BelMESCommon\BelMESESD.ini", @"C:\BelMESCommon\BelMESESD.ini", true);
                            }
                            catch
                            {
                                MessageBox.Show("Nie je mozne skopirovat subor BelMESESD.ini na disk C. Zavolajte prosim testovacieho technika. Aplikacia sa teraz zavrie.", "CHYBA", MessageBoxButtons.OK);
                                Application.Exit();
                            }
                        }
                    }
                }
                else
                {
                    this.BelMESenabled = false;

                    //vykomentovat na konci tyzdna 
                    //this.BelMESenabled = true;
                    //this.BelMESobj = new BelMES(this.lbl_StationName.Text.Trim());

                    XmlNode URModeNode = this.StationConfig.LastChild.SelectSingleNode("./Mode");
                    if (URModeNode == null)
                    {
                        this.ErrorMessageBoxShow("V Station configu chyba informacia o mode testu.", true);
                    }
                    if (URModeNode.InnerText != "") this.BelMESobj.Mode = URModeNode.InnerText;

                    //if (!this.BelMESobj.Activated) this.BelMESenabled = false;



                    // force belmes to enabled
                    //this.StationConfig.SelectSingleNode("./Configuration/BelMESState").InnerText = "Y";
                    //this.StationConfig.Save(this.StationConfig.BaseURI.Substring(this.StationConfig.BaseURI.IndexOf("C:/")));
                }
                this.CheckShortcutOnDesktop();
                Int32 n_HistorySNCount = Convert.ToInt32(this.StationConfig.SelectSingleNode("./Configuration/HistorySerialNumbersCount").InnerText);
                this.eventDisabler = true;

                this.cb_LastSerialNumbers.MaxDropDownItems = n_HistorySNCount;
                this.cb_LastSerialNumbers.DropDownHeight = this.cb_LastSerialNumbers.ItemHeight * ((n_HistorySNCount > 20) ? (20) : (n_HistorySNCount));

                this.HSNSorting = Convert.ToInt32(this.StationConfig.SelectSingleNode("./Configuration/HistorySerialNumbersSorting").InnerText);

                this.lbl_ScanEditOrder.ForeColor = Color.FromName(this.StationConfig.SelectSingleNode("./Configuration/Colors/CommandColor").InnerText.Trim());

                if (this.StationConfig.SelectSingleNode("./Configuration/PartNoNeeded").InnerText == "N")
                {
                    this.cb_ProductNo.Enabled = false;
                    this.PartNoNeeded = false;
                }

                if (this.StationConfig.SelectSingleNode("./Configuration/SimpleMode/Available").InnerText == "Y")
                {
                    this.SimpleModeAvailable = true;
                }
                else
                {
                    this.SimpleModeAvailable = false;
                }

                this.eventDisabler = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 2"));
            }
        }
        private void SaveErrorToLogFile(String ErrorMessage)
        {
            try
            {
                String str_ErrorLogFileName = String.Concat("ErrorLog_", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), ".txt");
                FileStream fs = new FileStream(str_ErrorLogFileName, FileMode.Append);
                Boolean b_writeEnabled = true;
                if (this.PathExists(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\"))
                {
                    if (!System.IO.File.Exists(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName)))
                    {
                        try
                        {
                            fs = System.IO.File.Create(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName));
                        }
                        catch
                        {
                            b_writeEnabled = false;
                        }
                    }
                    else
                    {
                        fs = new FileStream(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName), FileMode.Append);
                        //fs = System.IO.File.OpenWrite(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName));
                    }
                }
                else
                {
                    String str_localErrorDir = Path.GetDirectoryName(this.StationConfig.BaseURI.Substring(this.StationConfig.BaseURI.IndexOf(@"C:/")));
                    if (str_localErrorDir.Substring(str_localErrorDir.Length - 1, 1) != "\\")
                    {
                        str_localErrorDir = String.Concat(str_localErrorDir, "\\");
                    }
                    if (!System.IO.File.Exists(String.Concat(str_localErrorDir, @"ErrorLogDir\", str_ErrorLogFileName)))
                    {
                        try
                        {
                            fs = System.IO.File.Create(String.Concat(str_localErrorDir, @"ErrorLogDir\", str_ErrorLogFileName));
                        }
                        catch
                        {
                            b_writeEnabled = false;
                        }
                    }
                    else
                    {
                        fs = new FileStream(String.Concat(str_localErrorDir, @"ErrorLogDir\", str_ErrorLogFileName), FileMode.Append);
                        //fs = System.IO.File.OpenWrite(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName));
                    }
                }

                String StationID = this.lbl_StationName.Text;
                String ActualTime = String.Concat(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), "_", DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"), DateTime.Now.Second.ToString("D2"));
                String Operator = this.lbl_OperatorSurname.Text;
                String ActualOrderString = this.tb_OrderValue.Text;

                try
                {
                    if (b_writeEnabled)
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(String.Concat(ActualTime, ";", StationID, ";", Operator, ";", ActualOrderString, ";", ErrorMessage));
                        sw.Close();
                        fs.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex.Message, " Zavolajte prosim testovacieho inziniera."));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 1"));
            }
        }        
        private void CheckForUpdateAndInstallIt()
        {
            try
            {
                UpdateCheckInfo info = null;

                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                    try
                    {
                        info = ad.CheckForDetailedUpdate();

                    }
                    catch (DeploymentDownloadException dde)
                    {
                        MessageBox.Show("Novsia verzia aplikácie nemôže byť teraz stiahnutá. \n\nProsím skontrolujte sieťové pripojenie, prípadne vypršanie platnosti hesla do windows. \nV prípade opakovania sa chyby zavolajte prosím technika. \n\nError: " + dde.Message);
                        return;
                    }
                    catch (InvalidDeploymentException ide)
                    {
                        MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. \n\nProsim zavolajte technika. \n\nError: " + ide.Message);
                        return;
                    }
                    catch (InvalidOperationException ioe)
                    {
                        MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. \n\nProsim zavolajte technika. \n\nError: " + ioe.Message);
                        return;
                    }

                    if (info.UpdateAvailable)
                    {
                        try
                        {
                            ad.Update();
                            //MessageBox.Show("The application has been upgraded, and will now restart.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Novsia verzia aplikácie nemôže byť teraz stiahnutá. \n\nProsím skontrolujte sieťové pripojenie, prípadne vypršanie platnosti hesla do windows. \nV prípade opakovania sa chyby zavolajte prosím technika. \n\nError: " + dde.Message);
                            return;
                        }

                    }
                }

                // check for new belmes configuration files
                if (!Directory.Exists(@"c:\belmescommon"))
                {
                    try
                    {
                        Directory.CreateDirectory(@"c:\belmescommon");
                    }
                    catch
                    {
                        MessageBox.Show("Nemozem vytvorit adresar c:\\belmescommon. Zavolajte prosim testovacieho technika.", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
                try
                {
                    String str_actUser = Environment.UserName;
                    
                    if ((str_actUser.ToLower() != "finalinspectionhev01") && (str_actUser.ToLower() != "kolman"))
                    {
                        if (System.IO.File.GetLastWriteTime(@"\\dcafs3\Testing_SRO\TESTING\TestStand\BelMESCommon\belmes.ini") != System.IO.File.GetLastWriteTime(@"c:\belmescommon\belmes.ini"))
                            System.IO.File.Copy(@"\\dcafs3\Testing_SRO\TESTING\TestStand\BelMESCommon\belmes.ini", @"c:\belmescommon\belmes.ini", true);

                        if (System.IO.File.GetLastWriteTime(@"\\dcafs3\Testing_SRO\TESTING\TestStand\BelMESCommon\belmesesd.ini") != System.IO.File.GetLastWriteTime(@"c:\belmescommon\belmesesd.ini"))
                            System.IO.File.Copy(@"\\dcafs3\Testing_SRO\TESTING\TestStand\BelMESCommon\belmesesd.ini", @"c:\belmescommon\belmesesd.ini", true);
                    }                     
                }
                catch
                {
                    MessageBox.Show("Nemozem skopirovat konfiguracne subory belmesu. Zavolajte prosim testovacieho technika.", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 3"));
            }
        }
        private bool PathExists(string path)
        {
            try
            {
                if (path.Substring(path.Length - 1, 1) != "\\")
                {
                    path = String.Concat(path, "\\");
                }
                bool exists = true;
                Thread t = new Thread
                (
                    new ThreadStart(delegate ()
                    {
                        exists = Directory.Exists(path);
                    })
                );
                t.Start();
                bool completed = t.Join(500);
                if (!completed) { exists = false; t.Abort(); }
                return exists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 4"));
                return false;
            }
        }
        private void CheckShortcutOnDesktop()
        {
            try
            {

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Array myFiles = Directory.GetFiles(path);

                bool shortcutpresent = false;
                foreach (String actFile in myFiles)
                {
                    if (actFile.IndexOf("SigmaSure Manual Report Generator") > -1)
                    {
                        try
                        {
                            System.IO.File.Delete(actFile);
                            SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
                            break;
                        }
                        catch (Exception ex)
                        {
                            this.SaveErrorToLogFile(ex.Message);
                        }
                    }
                }

                foreach (String actFile in myFiles)
                {
                    if (actFile.IndexOf("BelMes Manual Report Generator") > -1)
                    {
                        shortcutpresent = true;
                        break;
                    }
                }

                if (!shortcutpresent)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                    path = String.Concat(path, @"\Programs\Vladimir Kolman");

                    String DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    try
                    {
                        Array myShortcuts = Directory.GetFiles(path);
                        foreach (string actSC in myShortcuts)
                        {
                            if (actSC.IndexOf("SigmaSure Manual Report Generator") > -1)
                            {
                                try
                                {
                                    System.IO.File.Copy(actSC, String.Concat(DesktopPath, @"\BelMes Manual Report Generator.appref-ms"));
                                }
                                catch (Exception ex)
                                {
                                    this.SaveErrorToLogFile(ex.Message);
                                }

                            }
                        }
                    }
                    catch { }
                    /*
                    object shDesktop = (object)"Desktop";
                    WshShell shell = new WshShell();
                    string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\BelMes Manual Report Generator.lnk";
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                    shortcut.Description = "BelMes Manual Report Generator";
                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.Save();
                    */
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 5"));
            }
        }
        private String[] GetAllTestNames()
        {
            XmlNode actStationMainConfingNode = this.StationConfig.SelectSingleNode("./Configuration/Station");
            String[] ar_actTestTypes = { };
            foreach (XmlNode actChildNode in actStationMainConfingNode.ChildNodes)
            {                
                if (actChildNode.Name == "Default")
                {
                    String[] ar_TestTypes = {"Safety Test"
                            , "PreTest"
                            , "Board Test"
                            , "Functiontest"
                            , "Final Inspection"
                            , "Adjustement"
                            , "Finaltest"
                            , "Functiontest 1"
                            , "Functiontest 2"
                            , "Functiontest 3"
                            , "BurnIn"
                            , "OBA"};
                    return ar_TestTypes;
                }
                else
                {
                    Array.Resize(ref ar_actTestTypes, ar_actTestTypes.Length + 1);
                    ar_actTestTypes.SetValue(actChildNode.Name.Replace("_", " "), ar_actTestTypes.Length - 1);
                }
            }
            return ar_actTestTypes;
        }
        private StationInfos GetStationInfosForTest(String TestName)
        {
            StationInfos retVal = new StationInfos();            
            XmlNode StationConfigInfosNode = this.StationConfig.SelectSingleNode("./Configuration/Station");
            if (StationConfigInfosNode == null) this.ErrorMessageBoxShow("Neexistuje node Configuration/Station v StationConfig subore. Zavolajte prosim testovacieho technika.", true);
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
                            StationInfos.StationProperty actSIProp = new StationInfos.StationProperty
                            {
                                Name = actPropNode.Name.Replace("_", " "),
                                Value = actPropNode.InnerText
                            };
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
                            StationInfos.StationProperty actSIProp = new StationInfos.StationProperty
                            {
                                Name = actPropNode.Name.Replace("_", " "),
                                Value = actPropNode.InnerText
                            };
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
                            StationInfos.StationProperty actSIProp = new StationInfos.StationProperty
                            {
                                Name = actPropNode.Name.Replace("_", " "),
                                Value = actPropNode.InnerText
                            };
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


        /*
        private Array GetChildTestsInfos(string ProdID, string TestType)
        {
            ChildTestInfo[] retArray = { };
            if (TestType == "") return retArray;

            XmlNode ProdNode = this.GetProductNode(ProdID);
            if (ProdNode != null)
            {
                XmlNode childCheckListInfoActTestNode = ProdNode.SelectSingleNode(String.Concat("./CheckListTests/", TestType.Replace(' ', '_')));
                if (childCheckListInfoActTestNode != null)
                {
                    XmlNode InstructionsNode = this.ProductsConfig.LastChild.SelectSingleNode(String.Concat("./Checklists/", TestType.Replace(' ', '_'), "/", childCheckListInfoActTestNode.InnerText));
                    if (InstructionsNode != null)
                    {
                        for (Int32 i = 0; i < InstructionsNode.ChildNodes.Count; i++)
                        {
                            XmlNode actTestNode = InstructionsNode.ChildNodes[i];
                            ChildTestInfo actCTI = new ChildTestInfo();
                            actCTI.Name = actTestNode.Name.Replace('_', ' ');
                            actCTI.Instruction = actTestNode.SelectSingleNode("./Instruction").InnerText;
                            XmlNode actPictureNode = actTestNode.SelectSingleNode("./Picture");
                            if (actPictureNode != null)
                            {
                                actCTI.PicturePath = String.Concat(childCheckListInfoActTestNode.InnerText, @"\", actTestNode.SelectSingleNode("./Picture").InnerText);
                                if (Path.GetFileName(actCTI.PicturePath) == "")
                                {
                                    actCTI.PicturePath = "";
                                }
                            }
                            else
                            {
                                actCTI.PicturePath = "";
                            }
                            Array.Resize(ref retArray, i + 1);
                            retArray.SetValue(actCTI, i);
                        }
                    }
                }
            }
            if (retArray.Length != 0)
            {
                return retArray;
            }

            XmlNode FamilyNode = this.GetFamilyNode(ProdID);

            if (FamilyNode != null)
            {
                XmlNode childCheckListInfoNode = FamilyNode.SelectSingleNode("./CheckListTests");
                if (childCheckListInfoNode != null)
                {
                    XmlNode childCheckListInfoActTestNode = childCheckListInfoNode.SelectSingleNode(String.Concat("./", TestType.Replace(' ', '_')));
                    if (childCheckListInfoActTestNode != null)
                    {
                        XmlNode InstructionsNode = this.ProductsConfig.LastChild.SelectSingleNode(String.Concat("./Checklists/", TestType.Replace(' ', '_'), "/", childCheckListInfoActTestNode.InnerText));
                        if (InstructionsNode != null)
                        {
                            for (Int32 i = 0; i < InstructionsNode.ChildNodes.Count; i++)
                            {
                                XmlNode actTestNode = InstructionsNode.ChildNodes[i];
                                ChildTestInfo actCTI = new ChildTestInfo();
                                actCTI.Name = actTestNode.Name.Replace('_', ' ');
                                actCTI.Instruction = actTestNode.SelectSingleNode("./Instruction").InnerText;
                                XmlNode actPictureNode = actTestNode.SelectSingleNode("./Picture");
                                if (actPictureNode != null)
                                {
                                    actCTI.PicturePath = String.Concat(childCheckListInfoActTestNode.InnerText, @"\", actTestNode.SelectSingleNode("./Picture").InnerText);
                                    if (Path.GetFileName(actCTI.PicturePath) == "")
                                    {
                                        actCTI.PicturePath = "";
                                    }
                                }
                                else
                                {
                                    actCTI.PicturePath = "";
                                }
                                Array.Resize(ref retArray, i + 1);
                                retArray.SetValue(actCTI, i);
                            }
                        }
                    }
                    
                }
            }
            return retArray;
        }
        */
        /*
        private Array GetFaultCodes(String ProdID, String TestType)
        {
            FaultCode[] retArray = { };
            XmlNode ProdNode = this.GetProductNode(ProdID);
            XmlNode childFaultCodesListInfoNode = ProdNode.SelectSingleNode("./FaultCodesLists");
            if (childFaultCodesListInfoNode != null)
            {
                XmlNode childFaultCodesListInfoActTestNode = childFaultCodesListInfoNode.SelectSingleNode(String.Concat("./", TestType.Replace(' ', '_')));
                if (childFaultCodesListInfoActTestNode != null)
                {
                    for (Int32 i = 0; i < childFaultCodesListInfoActTestNode.ChildNodes.Count; i++)
                    {
                        XmlNode actTestNode = childFaultCodesListInfoActTestNode.ChildNodes[i];
                        FaultCode actFC = new FaultCode();
                        actFC.Code = actTestNode.Name.Replace('_', ' ');
                        actFC.Description = actTestNode.SelectSingleNode("./Description").InnerText;
                        Array.Resize(ref retArray, i + 1);
                        retArray.SetValue(actFC, i);
                    }
                }
            }
            else
            {
                XmlNode FamilyNode = this.GetFamilyNode(ProdID);
                if (FamilyNode != null)
                {
                    childFaultCodesListInfoNode = FamilyNode.SelectSingleNode("./FaultCodesLists");
                    if (childFaultCodesListInfoNode != null)
                    {
                        XmlNode childFaultCodesListInfoActTestNode = childFaultCodesListInfoNode.SelectSingleNode(String.Concat("./", TestType.Replace(' ', '_')));
                        if (childFaultCodesListInfoActTestNode != null)
                        {
                            for (Int32 i = 0; i < childFaultCodesListInfoActTestNode.ChildNodes.Count; i++)
                            {
                                XmlNode actTestNode = childFaultCodesListInfoActTestNode.ChildNodes[i];
                                FaultCode actFC = new FaultCode();
                                actFC.Code = actTestNode.Name.Replace('_', ' ');
                                actFC.Description = actTestNode.SelectSingleNode("./Description").InnerText;
                                Array.Resize(ref retArray, i + 1);
                                retArray.SetValue(actFC, i);
                            }
                        }
                    }
                }
            }
            return retArray;
        }
        */
        private bool CheckForDgvInstructionVisibility()
        {
            if (PartNoNeeded)
            {
                if (this.cb_ProductNo.SelectedIndex == -1) return false;
            }
            if (this.lbl_JobIDValue.Text == "") return false;
            if (this.cb_TestType.SelectedIndex == -1) return false;
            Array ChildTestsInfo = this.ProductsConfig.GetChildTestsInfos(this.cb_ProductNo.Text, this.cb_TestType.Text);
            if (ChildTestsInfo.Length == 0) return false;
            return true;
        }
        private void ResetInstructionDGV()
        {
            foreach (DataGridViewRow actRow in this.dgv_Instructions.Rows)
            {
                actRow.Cells[2].Value = "";
                actRow.Cells[5].Value = "";
                actRow.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private bool CheckForAllInstructionsAreComplete()
        {
            if (this.gb_Results.Text != "Instrukcie") return false;
            if (this.dgv_Instructions.Enabled == false) return false;
            foreach (DataGridViewRow actRow in this.dgv_Instructions.Rows)
            {
                if (actRow.Cells[2].Value.ToString() == "") return false;
            }
            return true;
        }
        private void ShowBarcodePicture(Image BPtoShow)
        {
            this.pb_BarcodesToScan.Image = BPtoShow;
            if (BPtoShow != null)
            {
                this.gb_FaultCodes.Visible = false;
                this.pb_BarcodesToScan.Visible = true;
            }
            else
            {
                if (this.actualGrade == "FAIL")
                {
                    if ((this.cb_ProductNo.Text != "") && (this.lbl_JobIDValue.Text != "") && (this.cb_TestType.Text != "") && (this.lbl_SerialNumber.Text != ""))
                    {
                        Array myFaultCodes = this.ProductsConfig.GetFaultCodes(this.cb_ProductNo.Text, this.cb_TestType.Text);
                        if (myFaultCodes.Length > 0)
                        {
                            this.pb_BarcodesToScan.Visible = false;
                            this.gb_FaultCodes.Visible = true;
                            this.cb_FaultCodes_Code.Items.Clear();
                            this.cb_FaultCodes_Description.Items.Clear();
                            for (Int32 i = 0; i < myFaultCodes.Length; i++)
                            {
                                ProductsConfigurationFile.FaultCode actFC = (ProductsConfigurationFile.FaultCode)myFaultCodes.GetValue(i);
                                this.cb_FaultCodes_Code.Items.Add(actFC.Code);
                                this.cb_FaultCodes_Description.Items.Add(actFC.Description);
                            }
                        }
                        else
                        {
                            this.pb_BarcodesToScan.Visible = true;
                            this.gb_FaultCodes.Visible = false;
                        }
                        return;
                    }
                }
                this.pb_BarcodesToScan.Visible = true;
                this.gb_FaultCodes.Visible = false;
            }
        }
        private void GetActualVersion()
        {
            StreamReader sr = new StreamReader("ChangeLog.txt");
            while (!sr.EndOfStream)
            {
                String actLine = sr.ReadLine();
                if (actLine.Length > 2)
                {
                    if (actLine.Substring(0, 2) == "V ")
                    {
                        this.ActualProgramVersion = actLine.Substring(2).Trim();
                    }
                }
            }
        }               
        
        private Boolean CheckUnknownProduct(String Product)
        {
            Boolean retValue = false;
            String Message = String.Concat("Produkt \"", Product, "\" sa nenasiel. Chcete ho pridat do zoznamu?");
            if (MessageBox.Show(Message, "CHYBA", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
                XmlNode mainProductNode = this.ProductsConfig.XmlFile.SelectSingleNode("./Configuration/Assemblies");
                XmlNode assNode = this.ProductsConfig.XmlFile.CreateElement("Assembly");
                XmlNode assNameNode = this.ProductsConfig.XmlFile.CreateElement("Name");
                assNameNode.InnerText = Product;

                assNode.AppendChild(assNameNode);

                mainProductNode.AppendChild(assNode);

                this.ProductsConfig.XmlFile.Save(this.ProductsConfig.XmlFile.BaseURI.Substring(this.ProductsConfig.XmlFile.BaseURI.IndexOf("C:/")));
                this.tb_OrderValue.Focus();
                retValue = true;
            }
            this.ErrorMessageBoxShow(Message, false);
            return retValue;
        }
        
        private void SetIgnoreSNCheckFlag()
        {
            String ProductNo = this.cb_ProductNo.Text;
            XmlNode actProdNode = this.ProductsConfig.GetProductNode(ProductNo);
            if (actProdNode == null)
            {
                this.IgnoreSNLength = false;
                return;
            }
            XmlNode actIgnoreSNCheckNode = actProdNode.SelectSingleNode("./IgnoreSNCheck");
            if (actIgnoreSNCheckNode == null)
            {
                this.IgnoreSNLength = false;
            }
            else
            {
                if (actIgnoreSNCheckNode.InnerText == "0") this.IgnoreSNLength = false;
                else if (actIgnoreSNCheckNode.InnerText == "1") this.IgnoreSNLength = true;
                else this.IgnoreSNLength = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.GetActualVersion();

                this.Text = String.Concat(this.Text, ActualProgramVersion);

                this.CheckForUpdateAndInstallIt();

                Int32 n_majorOSVersion = Environment.OSVersion.Version.Major;

                if (n_majorOSVersion == 5) this.ConfigPath = @"C:\Documents and Settings\All Users\Application Data\SSManualReportGenerator\";
                else if (n_majorOSVersion == 6) this.ConfigPath = @"C:\Users\Public\SSManualReportGenerator\";
                else
                {
                    this.ErrorMessageBoxShow("Neznama verzia operacneho systemu. Zavolajte prosim testovacieho inziniera", true);
                    this.Dispose();
                    return;
                }
                try
                {
                    if (!Directory.Exists(String.Concat(this.ConfigPath, @"Actual\")))
                    {
                        Directory.CreateDirectory(String.Concat(this.ConfigPath, @"Actual\"));
                    }
                    System.IO.File.Copy(String.Concat(@"ConfigFiles\", this.StationConfigFileName), String.Concat(this.ConfigPath, @"Actual\", this.StationConfigFileName), true);
                    if (!Directory.Exists(this.ConfigPath))
                    {
                        Directory.CreateDirectory(this.ConfigPath);
                    }
                }
                catch (Exception ex)
                {
                    this.ErrorMessageBoxShow(String.Concat(ex.Message, "\n", ex.Data.Values, "\n"), true);
                }

                if (!System.IO.File.Exists(String.Concat(this.ConfigPath, StationConfigFileName)))
                {
                    this.ErrorMessageBoxShow(String.Concat("Neexistuje Station config subor v adresari ", this.ConfigPath, ". Zavolajte prosim testovacieho inziniera"), true);
                    this.Dispose();
                    return;
                }
                else
                {
                    this.StationConfig.Load(String.Concat(this.ConfigPath, StationConfigFileName));
                    this.myStationConfig.MyValidation();
                    this.StationConfig.Load(String.Concat(this.ConfigPath, StationConfigFileName));
                }
                try
                {
                    XmlNode actStationConfigsNode = this.StationConfig.SelectSingleNode("./Configuration/Station");

                    this.lbl_StationName.Text = actStationConfigsNode.FirstChild.SelectSingleNode("./Name").InnerText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex.Message, "\n", ex.Data));
                }

                String str_UserConfigServerFile = this.StationConfig.SelectSingleNode("./Configuration/OperatorConfigurationFile").InnerText;
                if (this.PathExists(Path.GetDirectoryName(str_UserConfigServerFile)))
                {
                    if (System.IO.File.Exists(str_UserConfigServerFile))
                    {
                        if (System.IO.File.Exists(String.Concat(this.ConfigPath, @"Actual\", this.UserConfigFileName)))
                        {
                            FileInfo serverFile = new FileInfo(str_UserConfigServerFile);
                            FileInfo localFile = new FileInfo(String.Concat(this.ConfigPath, @"Actual\", this.UserConfigFileName));
                            if (localFile.LastWriteTimeUtc < serverFile.LastWriteTimeUtc)
                            {
                                System.IO.File.Copy(str_UserConfigServerFile, String.Concat(this.ConfigPath, this.UserConfigFileName), true);
                            }
                        }
                        else
                        {
                            System.IO.File.Copy(str_UserConfigServerFile, String.Concat(this.ConfigPath, this.UserConfigFileName), true);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie je dostupny UserConfiguration subor na serveri. Zmenene hesla sa neulozia.");
                    }
                }
                else
                {
                    MessageBox.Show("Nie je pristupny server dcafs3. Nie je mozne zmenit heslo.", "POZOR");
                    this.btn_PasswordChange.Enabled = false;
                }

                this.UserConfig.Load(String.Concat(this.ConfigPath, this.UserConfigFileName));

                System.IO.File.Copy(String.Concat(@"ConfigFiles\", this.ProductsConfigFileName), String.Concat(this.ConfigPath, this.ProductsConfigFileName), true);

                this.ProductsConfig = new ProductsConfigurationFile(this.ConfigPath);

                //this.ProductsConfig.Load(String.Concat(this.ConfigPath, this.ProductsConfigFileName));

                Array ar_assemblies = this.ProductsConfig.GetAssembliesList();

                /*
                XmlNode node_Assembly = this.ProductsConfig.SelectSingleNode("./Configuration/Assemblies");
                foreach (XmlNode actNode in node_Assembly.ChildNodes)
                {
                    XmlNode node_AssemblyName = actNode.SelectSingleNode("./Name");
                    Array.Resize(ref ar_assemblies, ar_assemblies.Length + 1);
                    ar_assemblies.SetValue(node_AssemblyName.InnerText, ar_assemblies.Length - 1);
                }
                */

                Array.Sort(ar_assemblies);
                foreach (String actAssembly in ar_assemblies)
                {
                    this.cb_ProductNo.Items.Add(actAssembly);
                }

                this.pB_Assembly.Image = this.NOTOKpict;
                this.pB_JobID.Image = this.NOTOKpict;
                this.pb_TestType.Image = this.NOTOKpict;
                this.pb_SerialNumber.Image = this.NOTOKpict;

                foreach (InputLanguage actLang in InputLanguage.InstalledInputLanguages)
                {
                    if ((actLang.Culture.Name == "en-US") || (actLang.Culture.Name == "en-EN"))
                    {
                        InputLanguage.CurrentInputLanguage = actLang;
                    }
                }

                String[] ar_TestTypes = this.GetAllTestNames();
                Array.Sort(ar_TestTypes);
                foreach (String actTest in ar_TestTypes)
                {
                    this.cb_TestType.Items.Add(actTest);
                }

                this.ApplySettings();

                this.cb_ProductNo.DropDownHeight = this.Height - this.cb_ProductNo.Location.Y;
                this.cb_TestType.DropDownHeight = this.Height - this.cb_TestType.Location.Y;

                try
                {
                    XmlNode mainSCnode = this.StationConfig.SelectSingleNode("./Configuration");
                    if (mainSCnode.SelectSingleNode("./ExtraLoginEnabled").InnerText.Trim() == "Y")
                    {
                        //this.ExtraLoginEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex.Message, "/n Zavolajte prosim testovacieho technika. Problem s extraLogin."));
                    return;
                }

                this.Login();



                if (!this.cb_ProductNo.Enabled)
                {
                    this.lbl_ScanEditOrder.Text = "Zadajte Job ID alebo zoskenujte barcode na vyrobku:";
                    this.actualOrder = 2;
                }
                else
                {
                    this.actualOrder = 1;
                }
                this.Focus();
                this.tb_OrderValue.Focus();
                this.tb_OrderValue.SelectAll();
                if (this.BelMESobj.Emp.strEmployeeName == null) return;
                if (this.BelMESobj.Emp.strEmployeeName == "") return;
                if (this.EmpRights != "admin")
                {
                    if (this.SimpleModeAvailable && !this.PartNoNeeded)
                    {
                        this.btn_SimpleMode_Click(new object(), new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 6"));
            }
        }        

        private void cb_ProductNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!PartNoNeeded) return;
            if ((this.cb_ProductNo.SelectedIndex != -1) && (this.cb_ProductNo.SelectedIndex != this.lastProductIndex))
            {
                this.lastProductIndex = this.cb_ProductNo.SelectedIndex;
                this.pB_Assembly.Image = this.OKpict;
                this.resetingCB_TestType = true;
                this.cb_TestType.SelectedIndex = -1;
                this.resetingCB_TestType = false;
                this.lbl_JobIDValue.Text = "";
                this.pB_JobID.Image = this.NOTOKpict;
                this.pb_TestType.Image = this.NOTOKpict;
                this.lbl_SerialNumber.Text = "";
                this.pb_SerialNumber.Image = this.NOTOKpict;
                this.actualOrder = 2;
                this.lbl_ScanEditOrder.Text = "Zadajte Job ID alebo zoskenujte barcode na vyrobku:";
                this.ResetOrderTextBox();
                this.eventDisabler = false;
                this.CheckFormDataForReportAndBatchSNEnter("prodNoSelectIndexChanged1");
                this.SetIgnoreSNCheckFlag();
                if (this.ProductsConfig.BelMesActivated(this.cb_ProductNo.Items[this.cb_ProductNo.SelectedIndex].ToString()))
                {
                    this.BelMESobj.ProductVerifiedForMessages = true;
                }
                else
                {
                    this.BelMESobj.ProductVerifiedForMessages = false;
                }
                return;
            }
            if (this.eventDisabler)
            {
                this.eventDisabler = false;
                this.CheckFormDataForReportAndBatchSNEnter("prodNoSelectIndexChanged2");
                return;
            }

            if (this.cb_ProductNo.SelectedIndex > -1)
            {
                this.pB_Assembly.Image = this.OKpict;
            }
            else
            {
                this.pB_Assembly.Image = this.NOTOKpict;
                this.BelMESobj.ProductVerifiedForMessages = false;
            }
            /*
            this.resetingCB_TestType = true;
            this.cb_TestType.SelectedIndex = -1;
            this.resetingCB_TestType = false;
            */
            this.lbl_JobIDValue.Text = "";
            this.pB_JobID.Image = this.NOTOKpict;
            this.lbl_SerialNumber.Text = "";
            this.pb_SerialNumber.Image = this.NOTOKpict;
            this.actualOrder = 2;
            this.lbl_ScanEditOrder.Text = "Zadajte Job ID alebo zoskenujte barcode na vyrobku:";
            this.ResetOrderTextBox();
            this.CheckFormDataForReportAndBatchSNEnter("prodNoSelectIndexChanged3");
            this.cb_LastSerialNumbers.Items.Clear();            
        }        

        private void lbl_OperatorNr_DoubleClick(object sender, EventArgs e)
        {
            this.Login();
        }

        private void tb_OrderValue_KeyUp(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                this.tb_OrderValue.Text = this.tb_OrderValue.Text.Trim();
                if (this.tb_OrderValue.Text == "")
                {
                    if (this.actualOrder != 7)
                    {
                        this.ErrorMessageBoxShow("Zoskenujte alebo zadajte pozadovane udaje.", true);
                        this.tb_OrderValue.SelectAll();
                        return;
                    }
                }
                if (this.tb_OrderValue.Text.ToUpper() == "LOGOUTUSER")
                {
                    if (PartNoNeeded) this.cb_TestType.SelectedIndex = -1;
                    this.eventDisabler = true;
                    this.lbl_SerialNumber.Text = "";
                    this.pb_SerialNumber.Image = this.NOTOKpict;
                    this.pb_TestType.Image = this.NOTOKpict;
                    this.lbl_JobIDValue.Text = "";
                    this.pB_JobID.Image = this.NOTOKpict;
                    if (PartNoNeeded) this.cb_ProductNo.SelectedIndex = -1;
                    this.pB_Assembly.Image = this.NOTOKpict;

                    this.Login();

                    this.ResetOrderTextBox();
                    this.actualOrder = 1; 
                    return;
                }                
                if (this.tb_OrderValue.Text.ToUpper() == "RESULTENABLEDISABLE")
                {
                    if (!this.gb_Results.Enabled) this.gb_Results.Enabled = true;
                    else this.gb_Results.Enabled = false;
                    if (this.gb_Results.Enabled)
                    {
                        this.enableResultsFieldToolStripMenuItem.Visible = false;
                        this.disableResultsFieldToolStripMenuItem.Visible = true;
                        this.actualOrder = 5;
                        this.lbl_ScanEditOrder.Text = "Zadajte popis chyby testovaneho vyrobku:";
                    }
                    else
                    {                        
                        this.enableResultsFieldToolStripMenuItem.Visible = true;
                        this.disableResultsFieldToolStripMenuItem.Visible = false;
                        this.actualOrder = 6;
                        this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                    }
                    this.ResetOrderTextBox();
                    return;
                }
                if (this.tb_OrderValue.Text.ToUpper() == "DELETELASTRESULT")
                {
                    this.ResetOrderTextBox();
                    if (!this.gb_Results.Enabled) return;
                    if (this.lbl_SerialNumber.Text == "") return;
                    if (this.cb_TestType.Text == "") return;
                    if (this.gb_Results.Text != "Instrukcie")
                    {
                        if (this.gb_Results.Controls.Count > 0)
                        {
                            this.gb_Results.Controls[this.gb_Results.Controls.Count - 1].Dispose();
                            this.actualOrder = 5;
                            this.lbl_ScanEditOrder.Text = "Zadajte nazov testovacieho kroku, ktory chcete pridat:";
                        }
                    }
                    return;
                }
                if (this.tb_OrderValue.Text.ToUpper() == "GENERATESIGMASUREREPORT")
                {
                    this.ResetOrderTextBox();

                    if (!this.lbl_SerialNumber.Enabled)
                    {
                        this.ErrorMessageBoxShow("Prihlaste sa dvojklikom na pole osobneho cisla!", true);
                        return;
                    }

                    if ((this.lbl_SerialNumber.Text == "") || (this.cb_TestType.Text == ""))
                    {
                        this.ErrorMessageBoxShow("Vyplnte vsetky potrebne udaje!", true);
                        return;
                    }
                    if (this.CheckingOfSerialNumberTested)
                    {
                        if (this.TD.CheckSerialNumberAndTestType(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim(), this.cb_TestType.Text))
                        {
                            if (MessageBox.Show(String.Concat("V databaze sa uz nachadza PASS vysledok pre vyrobok s JobID \"", this.lbl_JobIDValue.Text.Trim(), "\" a so seriovym cislom \"", this.lbl_SerialNumber.Text.Trim(), "\" pre typ testu \"", this.cb_TestType.Text, "\".\n\nChcete report aj tak poslat?"), "POZOR", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                this.lbl_SerialNumber_DoubleClick(sender, new EventArgs());
                                return;
                            }
                        }
                    }

                    this.GenerateReport();
                    return;
                }
                if ((this.tb_OrderValue.Text.IndexOf(';') > -1) || (this.tb_OrderValue.Text.IndexOf(':') > -1))
                {
                    if ((this.gb_Results.Enabled) && (this.lbl_SerialNumber.Text.Trim() != ""))
                    {
                        if (MessageBox.Show("Naozaj chcete vlozit udaje o novom vyrobku? POZOR - fail report predchadzajuceho vyrobku nebol poslany.", "Varovanie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            this.tb_OrderValue.Text = "";
                            this.tb_OrderValue.SelectAll();
                            this.tb_OrderValue.Focus();
                            return;
                        }
                        else
                        {
                            foreach (Control actControl in this.gb_Results.Controls)
                            {
                                if (actControl.Name.ToLower() != "dgv_ctions")
                                    actControl.Dispose();
                            }
                            this.gb_Results.Enabled = false;
                        }
                    }
                    String actOrderValue = "";
                    String JobID = "";
                    String SerialNumber = "";
                    String CustomSerialNumber = "";
                    String Product = "";
                    String Version = "";
                    String Location = "";
                    String Datecode = "";
                    String WorkOrder = "";
                    if (this.tb_OrderValue.Text[0] == '#')
                    {
                        if (this.tb_OrderValue.Text.Length == 15)
                        {
                            JobID = this.tb_OrderValue.Text.Substring(1, 8);
                            SerialNumber = this.tb_OrderValue.Text.Substring(9, 5);
                            if (!PartNoNeeded)
                            {
                                if (this.lbl_JobIDValue.Text !="")
                                {
                                    if (this.lbl_JobIDValue.Text != JobID)
                                    {
                                        MessageBox.Show("JobID zoskenovaneho vyrobku nie je zhodne s JobID predchadajuceho vyrobku.");
                                        return;
                                    }
                                }
                                this.lbl_JobIDValue.Text = JobID;
                                if (this.cb_TestType.Text == "")
                                {
                                    this.actualOrder = 8;
                                    this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte typ testu:";
                                }
                                else
                                {
                                    this.lbl_SerialNumber.Text = SerialNumber;
                                    if (this.BelMESenabled)
                                    {
                                        String strProdIDbuff = "";
                                        this.BelMESobj.BelMESAuthorization(String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()), this.cb_TestType.Text.Trim(), ref strProdIDbuff, "", false);

                                        if (!BelMESobj.Authorization.blnAuthorized)
                                        {
                                            if (this.BelMESMessageWarnings)
                                            {
                                                MessageBox.Show(this.BelMESobj.Authorization.strResult);
                                            }
                                        }
                                    }
                                    this.actualOrder = 6;
                                    if (this.gb_Results.Text != "Instrukcie")
                                    {
                                        this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                                    }
                                }
                            }
                            else
                            {
                                if (this.cb_ProductNo.Text != "")
                                {
                                    this.lbl_JobIDValue.Text = JobID;
                                    if (this.cb_TestType.Text == "")
                                    {
                                        this.actualOrder = 8;
                                        this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte typ testu:";
                                    }
                                    else
                                    {
                                        this.lbl_SerialNumber.Text = SerialNumber;
                                        if (this.BelMESenabled)
                                        {
                                            String strProdIDbuff = "";
                                            this.BelMESobj.BelMESAuthorization(String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()), this.cb_TestType.Text.Trim(), ref strProdIDbuff, "", false);
                                            
                                            if (!BelMESobj.Authorization.blnAuthorized)
                                            {
                                                if (this.BelMESMessageWarnings)
                                                {
                                                    MessageBox.Show(this.BelMESobj.Authorization.strResult);
                                                }
                                            }
                                        }
                                        this.actualOrder = 6;
                                        if (this.gb_Results.Text != "Instrukcie")
                                        {
                                            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                                        }
                                        else
                                        {
                                            this.lbl_ScanEditOrder.Text = "Vyklikajte prosim vysledky jednotlivych krokov a nasledne stlacte Vytvor report tlacidlo.";
                                            this.gb_Results.Enabled = true;
                                        }
                                    }                                   
                                }
                            }
                            this.ResetOrderTextBox();
                            return;
                        }
                        else
                        {
                            actOrderValue = this.tb_OrderValue.Text.Substring(1);
                            try
                            {
                                
                                JobID = actOrderValue.Substring(0, 8);
                                actOrderValue = actOrderValue.Substring(8);
                                if (actOrderValue.Substring(actOrderValue.Length - 1) == ";")
                                {
                                    actOrderValue = actOrderValue.Substring(0, actOrderValue.Length - 1);
                                }
                                SerialNumber = actOrderValue.Substring(0, 5);
                                if (actOrderValue.IndexOf(';') > -1)
                                {
                                    actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                    if (actOrderValue.IndexOf(';') > -1)
                                    {
                                        Product = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                        if (actOrderValue.IndexOf(';') > -1)
                                        {
                                            actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                            Version = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                            if (actOrderValue.IndexOf(';') > -1)
                                            {
                                                actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                                Datecode = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                                if (actOrderValue.IndexOf(';') > -1)
                                                {
                                                    actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                                    if (actOrderValue.IndexOf(';') > -1)
                                                    {
                                                        Location = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                                    }
                                                    else
                                                    {
                                                        Location = actOrderValue;
                                                    }
                                                    if (actOrderValue.IndexOf(';') > -1)
                                                    {
                                                        actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                                        CustomSerialNumber = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                                        if (actOrderValue.IndexOf(';') > -1)
                                                        {
                                                            actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                                            if (actOrderValue.IndexOf(';') > -1)
                                                            {
                                                                actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                                                if (actOrderValue.IndexOf(';') > -1)
                                                                {
                                                                    actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                                                                    if (actOrderValue.IndexOf(';') > -1)
                                                                    {
                                                                        WorkOrder = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                                                    }
                                                                    else
                                                                    {
                                                                        WorkOrder = actOrderValue;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Product = actOrderValue;
                                    }
                                }
                                if (actOrderValue.IndexOf(';') > -1)
                                {

                                }
                                else
                                {
                                    WorkOrder = actOrderValue;
                                }
                            }
                            catch
                            {
                                this.ErrorMessageBoxShow("Nespravne udaje v zoskenovanom barcode", true);
                                MessageBox.Show(actOrderValue);
                                return;
                            }
                        }
                    }
                    else
                    {
                        actOrderValue = this.tb_OrderValue.Text.ToUpper();
                        Product = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                        actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                        Location = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                        actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                        JobID = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                        actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                        Version = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                        actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                        SerialNumber = "";
                        try
                        {
                            SerialNumber = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                            actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                        }
                        catch
                        {
                            SerialNumber = actOrderValue;
                            actOrderValue = "";
                        }
                        if (actOrderValue != "")
                        {
                            Datecode = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                            actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                            WorkOrder = "";
                            try
                            {
                                WorkOrder = actOrderValue.Substring(0, actOrderValue.IndexOf(';'));
                                actOrderValue = actOrderValue.Substring(actOrderValue.IndexOf(';') + 1);
                            }
                            catch
                            {
                                WorkOrder = actOrderValue;
                            }
                        }
                    }
                    
                    if (PartNoNeeded)
                    {
                        this.eventDisabler = true;
                        if (this.cb_ProductNo.Text != Product)
                        {
                            this.resetingCB_TestType = true;
                            this.cb_TestType.SelectedIndex = -1;
                            this.pb_TestType.Image = this.NOTOKpict;
                            this.resetingCB_TestType = false;
                        }

                        this.cb_ProductNo.SelectedIndex = -1;
                        this.cb_ProductNo.SelectedItem = Product;
                        this.eventDisabler = false;
                        this.lbl_JobIDValue.Text = "";

                        if (this.cb_ProductNo.SelectedIndex == -1)
                        {
                            this.pB_Assembly.Image = this.NOTOKpict;
                            if (this.CheckUnknownProduct(Product))
                            {
                                this.cb_ProductNo.Items.Clear();
                                String[] ar_assemblies = { };
                                XmlNode node_Assembly = this.ProductsConfig.XmlFile.SelectSingleNode("./Configuration/Assemblies");
                                foreach (XmlNode actNode in node_Assembly.ChildNodes)
                                {
                                    XmlNode node_AssemblyName = actNode.SelectSingleNode("./Name");
                                    Array.Resize(ref ar_assemblies, ar_assemblies.Length + 1);
                                    ar_assemblies.SetValue(node_AssemblyName.InnerText, ar_assemblies.Length - 1);
                                }

                                Array.Sort(ar_assemblies);
                                foreach (String actAssembly in ar_assemblies)
                                {
                                    this.cb_ProductNo.Items.Add(actAssembly);
                                }
                                this.cb_ProductNo.SelectedItem = Product;
                                if (this.cb_ProductNo.SelectedIndex > -1)
                                {
                                    this.pB_Assembly.Image = this.OKpict;
                                }
                                this.Focus();
                            }
                            this.tb_OrderValue.Enabled = true;
                            this.tb_OrderValue.SelectAll();
                        }
                        else
                        {
                            this.pB_Assembly.Image = this.OKpict;
                            this.lbl_JobIDValue.Text = JobID;
                            this.pB_JobID.Image = this.OKpict;
                            if (this.cb_TestType.SelectedIndex > -1)
                            {
                                this.lbl_SerialNumber.Text = SerialNumber;
                                if (this.lbl_SerialNumber.Text != "") this.pb_SerialNumber.Image = this.OKpict;
                                else this.pb_SerialNumber.Image = this.NOTOKpict;
                            }
                            if (this.cb_TestType.Text == "")
                            {
                                this.actualOrder = 8;
                                this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte typ testu:";
                                //this.eventDisabler = true;
                            }                            
                            else
                            {
                                if (this.lbl_SerialNumber.Text != "")
                                {
                                    this.actualOrder = 6;
                                    if (this.gb_Results.Text != "Instrukcie")
                                    {
                                        this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                                    }
                                    else
                                    {
                                        this.lbl_ScanEditOrder.Text = "Vyklikajte prosim vysledky jednotlivych krokov a nasledne stlacte Vytvor report tlacidlo.";
                                        this.gb_Results.Enabled = true;
                                    }
                                }
                            }
                        }
                        this.ResetOrderTextBox();
                        return;
                    }
                }
                String str_ValidationValue = ProductBarcode.GetProductNoFromBarcode(this.tb_OrderValue.Text);
                if ((str_ValidationValue != "") && (str_ValidationValue != "Error") && (str_ValidationValue != this.tb_OrderValue.Text))
                {                    
                    String str_TbValue = this.tb_OrderValue.Text;
                    String actProduct = "";
                    actProduct = ProductBarcode.GetProductNoFromBarcode(str_TbValue);
                    if (actProduct == "Error")
                    {
                        this.tb_OrderValue.SelectAll();
                        this.tb_OrderValue.Focus();
                        return;
                    }
                    if (actProduct != str_TbValue)
                    {
                        if (PartNoNeeded) this.cb_ProductNo.SelectedItem = actProduct;
                        this.lbl_JobIDValue.Text = ProductBarcode.GetJobIdFromBarcode(actProduct, str_TbValue);
                        this.pB_JobID.Image = this.OKpict;
                        if (!this.IgnoreSNLength) this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(actProduct, str_TbValue);
                        else this.lbl_SerialNumber.Text = str_TbValue;
                        this.pb_SerialNumber.Image = this.OKpict;
                        if (this.cb_TestType.Text == "")
                        {
                            this.actualOrder = 8;
                            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte typ testu:";                            
                        }
                        else
                        {
                            this.actualOrder = 6;
                            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";                            
                        }
                        this.ResetOrderTextBox();
                        return;
                    }
                }

                //OrderLogic:

                switch (this.actualOrder)
                {
                    case 1:
                        if (PartNoNeeded) this.cb_ProductNo.SelectedIndex = -1;                        
                        String actProduct = this.tb_OrderValue.Text;
                        if (PartNoNeeded) this.cb_ProductNo.SelectedItem = actProduct;                        
                        this.lbl_JobIDValue.Text = "";
                        if (PartNoNeeded)
                        {
                            if (this.cb_ProductNo.SelectedIndex == -1)
                            {
                                if (this.CheckUnknownProduct(actProduct))
                                {
                                    this.cb_ProductNo.Items.Clear();
                                    String[] ar_assemblies = { };
                                    XmlNode node_Assembly = this.ProductsConfig.XmlFile.SelectSingleNode("./Configuration/Assemblies");
                                    foreach (XmlNode actNode in node_Assembly.ChildNodes)
                                    {
                                        XmlNode node_AssemblyName = actNode.SelectSingleNode("./Name");
                                        Array.Resize(ref ar_assemblies, ar_assemblies.Length + 1);
                                        ar_assemblies.SetValue(node_AssemblyName.InnerText, ar_assemblies.Length - 1);
                                    }

                                    Array.Sort(ar_assemblies);
                                    foreach (String actAssembly in ar_assemblies)
                                    {
                                        this.cb_ProductNo.Items.Add(actAssembly);
                                    }
                                    this.cb_ProductNo.SelectedItem = actProduct;
                                    if (this.cb_ProductNo.SelectedIndex > -1)
                                    {
                                        this.pB_Assembly.Image = this.OKpict;
                                    }
                                    this.Focus();
                                    this.tb_OrderValue.SelectAll();
                                    this.tb_OrderValue.Focus();
                                }
                            }
                            else
                            {
                                this.actualOrder = 2;
                                this.lbl_ScanEditOrder.Text = "Zadajte Job ID alebo zoskenujte barcode na vyrobku:";
                                this.ResetOrderTextBox();
                            }
                        }
                        this.Focus();
                        break;
                    case 2:
                        this.lbl_JobIDValue.Text = ProductBarcode.GetJobIdFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text.ToUpper());
                        if (this.lbl_JobIDValue.Text == "")
                        {
                            this.tb_OrderValue.SelectAll();
                            return;
                        }
                        this.pB_JobID.Image = this.OKpict;

                        if (this.lbl_JobIDValue.Text.ToUpper() != this.tb_OrderValue.Text.ToUpper())
                        {
                            this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text.ToUpper());
                            this.pb_SerialNumber.Image = this.OKpict;
                        }
                        else
                        {
                            this.pb_SerialNumber.Image = this.NOTOKpict;
                        }

                        try
                        {
                            Convert.ToInt32(this.lbl_JobIDValue.Text);
                            if (this.lbl_JobIDValue.Text.Length == 7)
                            {
                                if (this.lbl_JobIDValue.Text.Substring(0, 1) != "0")
                                {
                                    this.lbl_JobIDValue.Text = String.Concat("0", this.lbl_JobIDValue.Text);
                                }
                            }
                        }
                        catch
                        {
                            this.lbl_JobIDValue.Text = "";
                            this.tb_OrderValue.Focus();
                            this.tb_OrderValue.SelectAll();
                            this.pB_JobID.Image = this.NOTOKpict;
                            return;
                        }

                        if ((this.lbl_JobIDValue.Text.Length < 7) || (this.lbl_JobIDValue.Text.Length > 8))
                        {
                            this.lbl_JobIDValue.Text = "";
                            this.pB_JobID.Image = this.NOTOKpict;
                            this.tb_OrderValue.Focus();
                            this.tb_OrderValue.SelectAll();
                            return;
                        }
                        if ((this.lbl_JobIDValue.Text.Length == 7) && (this.lbl_JobIDValue.Text.Substring(0, 1) == "0"))
                        {
                            this.lbl_JobIDValue.Text = "";
                            this.pB_JobID.Image = this.NOTOKpict;
                            this.tb_OrderValue.Focus();
                            this.tb_OrderValue.SelectAll();
                            return;
                        }

                        if (!((this.lbl_JobIDValue.Text.Substring(0, 1) == "0") || (this.lbl_JobIDValue.Text.Substring(0, 1) == "1")))
                        {
                            this.lbl_JobIDValue.Text = "";
                            this.tb_OrderValue.Focus();
                            this.tb_OrderValue.SelectAll();
                            this.pB_JobID.Image = this.NOTOKpict;
                            return;
                        }
                        else
                        {
                            this.pB_JobID.Image = this.OKpict;
                            this.actualOrder = 3;
                            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte typ testu:";
                            //this.eventDisabler = true;
                            this.ResetOrderTextBox();
                        }

                        if (this.lbl_JobIDValue.Text != "")
                        {
                            this.ResetOrderTextBox();
                            if (this.cb_TestType.Text != "")
                            {
                                this.actualOrder = 6;
                                this.pb_TestType.Image = this.OKpict;
                                if (this.lbl_SerialNumber.Text != "")
                                {
                                    this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                                }
                                else
                                {
                                    this.actualOrder = 4;
                                    this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte seriove cislo produktu:";
                                }
                            }
                            else
                            {
                                this.actualOrder = 8;
                                this.lbl_ScanEditOrder.Text = String.Concat("Zoskenujte alebo zadajte typ testu:");
                                //this.eventDisabler = true;
                            }
                            return;
                        }
                        break;
                    case 3:
                        this.eventDisabler = true;
                        this.cb_TestType.Text = this.tb_OrderValue.Text;
                        this.eventDisabler = false;
                        if (this.cb_TestType.SelectedIndex == -1)
                        {
                            this.ErrorMessageBoxShow("Zadany typ testu asi nie je spravny. V pripade, ze je spravny kontaktujte testovacieho inziniera.", true);
                            this.tb_OrderValue.SelectAll();
                        }
                        else
                        {
                            this.pb_TestType.Image = this.OKpict;
                            this.actualOrder = 4;
                            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte seriove cislo produktu:";
                            this.ResetOrderTextBox();                            
                        }
                        break;
                    case 4:
                        while (this.tb_OrderValue.Text.Length < 5)
                        {
                            this.tb_OrderValue.Text = String.Concat("0", this.tb_OrderValue.Text);
                        }
                        XmlNode actProdNode = this.ProductsConfig.GetProductNode(this.cb_ProductNo.Text);
                        if (actProdNode != null)
                        {
                            if (actProdNode.SelectSingleNode("./BarcodeLength") != null)
                            {
                                if (Convert.ToInt32(actProdNode.SelectSingleNode("./BarcodeLength").InnerText) == this.tb_OrderValue.Text.Length)
                                {
                                    if (this.lbl_JobIDValue.Text == "")
                                    {
                                        String JobIDfromBarcode = ProductBarcode.GetJobIdFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                        if (JobIDfromBarcode != "") this.lbl_JobIDValue.Text = JobIDfromBarcode;                                        
                                    }
                                    if (!this.IgnoreSNLength) this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                    else this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                                }
                                else
                                {
                                    this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                                }
                                this.pb_SerialNumber.Image = this.OKpict;
                            }
                            else
                            {
                                XmlNode actProdFamilyNode = this.ProductsConfig.GetFamilyNode(this.cb_ProductNo.Text);
                                if (actProdFamilyNode != null)
                                {
                                    XmlNode actProdFamilyNodeBarcodeLength = actProdFamilyNode.SelectSingleNode("./BarcodeLength");
                                    if (actProdFamilyNodeBarcodeLength != null)
                                    {
                                        if (Convert.ToInt32(actProdFamilyNodeBarcodeLength.InnerText) == this.tb_OrderValue.Text.Length)
                                        {
                                            String str_JobID = ProductBarcode.GetJobIdFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                            if ((str_JobID != "") && (str_JobID != this.tb_OrderValue.Text))
                                            {
                                                this.lbl_JobIDValue.Text = str_JobID;
                                            }
                                            if (!this.IgnoreSNLength) this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                            else this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                                            //this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                        }
                                        else
                                        {
                                            if (!this.IgnoreSNLength) this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                            else this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                                            //this.lbl_SerialNumber.Text = ProductBarcode.GetSerialNumberFromBarcode(this.cb_ProductNo.Text, this.tb_OrderValue.Text);
                                        }
                                        this.pb_SerialNumber.Image = this.OKpict;
                                    }
                                    else
                                    {
                                        this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                                    }
                                    this.pb_SerialNumber.Image = this.OKpict;
                                }
                            }
                        }

                        if (this.lbl_JobIDValue.Text != "")
                        {
                            if (this.cb_TestType.Text != "")
                            {
                                this.actualOrder = 6;
                                if (this.gb_Results.Text == "Vysledky")
                                {
                                    this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                                }
                                else
                                {
                                    this.lbl_ScanEditOrder.Text = "Postupujte podla krokov v tabulke nizsie.";
                                    this.gb_Results.Enabled = true;
                                }
                            }
                            else
                            {
                                this.actualOrder = 8;
                                this.lbl_ScanEditOrder.Text = String.Concat("Zadajte typ testu:");
                            }
                            if (this.lbl_SerialNumber.Text == "")
                            {
                                this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                                if (this.lbl_SerialNumber.Text == "")
                                {
                                    this.pb_SerialNumber.Image = this.NOTOKpict;
                                }
                                else
                                {
                                    this.pb_SerialNumber.Image = this.OKpict;
                                }
                            }
                            if (this.BelMESenabled)
                            {
                                if (!this.BelMESobj.Authorization.blnAuthorized)
                                {
                                    String str_SerialNumber = String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim());
                                    String str_ProdNumber = "";
                                    this.BelMESobj.BelMESAuthorization(str_SerialNumber, this.cb_TestType.Text.Trim(), ref str_ProdNumber, "", false);
                                }
                                if (!this.BelMESobj.Authorization.blnAuthorized)
                                {
                                    this.lbl_SerialNumber.Text = "";
                                }
                            }
                            this.ResetOrderTextBox();
                            break;
                        }
                        
                        //this.lbl_SerialNumber.Text = this.tb_OrderValue.Text;
                        this.pb_SerialNumber.Image = this.OKpict;
                        this.actualOrder = 6;
                        this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                        this.ResetOrderTextBox();
                        break;
                    case 5:
                        if (this.tb_OrderValue.Text.Length > "GENERATESIGMASUREREPORT".Length)
                        {
                            if (this.tb_OrderValue.Text.Substring(this.tb_OrderValue.Text.Length - "GENERATESIGMASUREREPORT".Length, "GENERATESIGMASUREREPORT".Length) == "GENERATESIGMASUREREPORT")
                            {
                                this.tb_OrderValue.Text = this.tb_OrderValue.Text.Substring(0, this.tb_OrderValue.Text.Length - "GENERATESIGMASUREREPORT".Length);
                            }
                        }
                        this.gb_Results.Controls.Add(new ResultGroupBoxSingleMode(this.tb_OrderValue.Text, "", new Point(10, ((this.gb_Results.Controls.Count-1) * 53) + 20), new Size(this.gb_Results.Width - 20, 52)));
                        this.Height += 70;
                        this.actualOrder = 7;
                        this.lbl_ScanEditOrder.Text = String.Concat("Popis chyby ", this.tb_OrderValue.Text, " bol vytvoreny. Zadajte poznamku k tejto chybe, alebo iba stlacte enter pre zadanie nazvu dalsieho popisu chyby.");
                        this.ResetOrderTextBox();                        
                        break;
                    case 6:
                        if (this.gb_Results.Text == "Instrukcie")
                        {
                            this.tb_OrderValue.Text = "";
                            this.ErrorMessageBoxShow("Vyklikajte prosím tabulku s instrukciami pre test a nasledne kliknite na \"Vytvor report\" tlacidlo. Dakujeme.", true);
                            return;
                        }
                        if ((this.tb_OrderValue.Text.ToUpper() != "PASS") && (this.tb_OrderValue.Text.ToUpper() != "FAIL"))
                        {
                            this.ErrorMessageBoxShow("Ocakava sa \"PASS\" alebo \"FAIL\" retazec.", true);
                            this.ResetOrderTextBox();
                        }
                        else
                        {
                            if (this.tb_OrderValue.Text.ToUpper() == "PASS")
                            {
                                String str_actSerialNumber = this.lbl_SerialNumber.Text;
                                if (this.CheckingOfSerialNumberTested)
                                {
                                    if (this.TD.CheckSerialNumberAndTestType(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim(), this.cb_TestType.Text))
                                    {
                                        if (MessageBox.Show(String.Concat("V databaze sa uz nachadza PASS vysledok pre vyrobok s JobID \"", this.lbl_JobIDValue.Text.Trim(), "\" a so seriovym cislom \"", this.lbl_SerialNumber.Text.Trim(), "\".\n\nChcete report aj tak poslat?"), "POZOR", MessageBoxButtons.YesNo) == DialogResult.No)
                                        {
                                            this.lbl_SerialNumber_DoubleClick(sender, new EventArgs());
                                            //this.BelMESobj.SetActualResult("Terminated", "", String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()));
                                            break;
                                        }
                                    }                                   
                                }
                                
                                this.GenerateReport();
                                this.actualOrder = 4;                                
                                this.ResetOrderTextBox();
                            }
                            else
                            {
                                this.actualGrade = "FAIL";
                                this.gb_Results.Enabled = true;

                                Array myFaultCodes = this.ProductsConfig.GetFaultCodes(this.cb_ProductNo.Text, this.cb_TestType.Text, this.PartNoNeeded);
                                if (myFaultCodes.Length == 0)
                                {
                                    this.actualOrder = 5;
                                    this.lbl_ScanEditOrder.Text = "Zadajte popis chyby vyrobku:";
                                }
                                else
                                {
                                    this.ShowBarcodePicture(null);
                                }
                                this.ResetOrderTextBox();
                            }
                        }
                        break;
                    case 7:
                        if (this.tb_OrderValue.Text.Length > "GENERATESIGMASUREREPORT".Length)
                        {
                            if (this.tb_OrderValue.Text.Substring(this.tb_OrderValue.Text.Length - "GENERATESIGMASUREREPORT".Length, "GENERATESIGMASUREREPORT".Length) == "GENERATESIGMASUREREPORT")
                            {
                                this.tb_OrderValue.Text = this.tb_OrderValue.Text.Substring(0, this.tb_OrderValue.Text.Length - "GENERATESIGMASUREREPORT".Length);
                            }
                        }
                        this.gb_Results.Controls[this.gb_Results.Controls.Count - 1].Controls[this.gb_Results.Controls[this.gb_Results.Controls.Count - 1].Controls.Count - 2].Text = this.tb_OrderValue.Text;
                        this.actualOrder = 5;
                        this.lbl_ScanEditOrder.Text = String.Concat("Zadajte nazov dalsieho popisu chyby:");
                        this.ResetOrderTextBox();
                        break;
                    case 8:
                        //this.eventDisabler = true;
                        this.cb_TestType.Text = this.tb_OrderValue.Text;
                        break;
                    default:
                        break;
                }
            }
        }

        private void lbl_JobIDValue_DoubleClick(object sender, EventArgs e)
        {
            if (this.PartNoNeeded)
            {
                if (this.cb_ProductNo.SelectedIndex == -1)
                {
                    this.ResetOrderTextBox();
                    return;
                }
            }            
            this.cb_TestType.Text = "";
            this.pb_TestType.Image = this.NOTOKpict;
            this.lbl_JobIDValue.Text = "";
            this.pB_JobID.Image = this.NOTOKpict;
            this.lbl_SerialNumber.Text = "";
            this.pb_SerialNumber.Image = this.NOTOKpict;
            this.tb_OrderValue.Text = "";
            this.actualOrder = 2;
            this.lbl_ScanEditOrder.Text = "Zadajte Job ID alebo zoskenujte barcode na vyrobku:";

            foreach (Control actGB in this.gb_Results.Controls)
            {
                if (actGB != this.dgv_Instructions)
                {
                    actGB.Dispose();
                }
            }
            this.ResetOrderTextBox();
        }

        private void lbl_SerialNumber_DoubleClick(object sender, EventArgs e)
        {
            if (this.cb_TestType.Text == "")
            {
                this.ResetOrderTextBox();
                this.lbl_SerialNumber.Text = "";
                return;
            }

            if (this.BelMESenabled && (this.lbl_SerialNumber.Text != ""))
            {
                String strProdIDbuff = this.cb_ProductNo.Text.Trim();
                if (!this.BelMESobj.SetActualResult(String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()), this.cb_TestType.Text.Trim(),"Terminated",""))
                {
                    //ErrorMessageBoxShow(this.BelMESobj.Authorization.strResult, true);
                    //return;
                }
            }
            else
            {
                return;
            }

            this.lbl_SerialNumber.Text = "";
            this.pb_SerialNumber.Image = this.NOTOKpict;
            this.tb_OrderValue.Text = "";
            this.actualOrder = 4;
            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte seriove cislo produktu:";

            foreach (Control actGB in this.gb_Results.Controls)
            {
                if (actGB != this.dgv_Instructions)
                {
                    actGB.Dispose();
                }
            }
            this.ResetOrderTextBox();           
        }

        private void gb_Results_Enter(object sender, EventArgs e)
        {
            this.gb_Results.Enabled = true;
        }

        private void enableResultsFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gb_Results.Enabled = true;
            this.enableResultsFieldToolStripMenuItem.Visible = false;
            this.disableResultsFieldToolStripMenuItem.Visible = true;
            if (this.actualOrder == 6)
            {
                this.actualOrder = 5;
                this.lbl_ScanEditOrder.Text = "Zadajte nazov testovacieho kroku, ktory chcete pridat:";
            }
        }

        private void disableResultsFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gb_Results.Enabled = false;
            this.enableResultsFieldToolStripMenuItem.Visible = true;
            this.disableResultsFieldToolStripMenuItem.Visible = false;
            if (actualOrder == 5)
            {
                this.actualOrder = 6;
                this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
            }
        }

        private void cb_TestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.eventDisabler) return;
            this.lbl_SerialNumber.Text = "";
            StationInfos actSI = this.GetStationInfosForTest(this.cb_TestType.Text);
            this.lbl_StationName.Text = actSI.Name;
            Boolean b_errorMessageShowed = false;
            if (this.PartNoNeeded)
            {
                if (this.cb_ProductNo.SelectedIndex != -1)
                {
                    Array ar_ChildTestsInfo = this.ProductsConfig.GetChildTestsInfos(this.cb_ProductNo.Text, this.cb_TestType.Text);
                    if (ar_ChildTestsInfo.Length != 0)
                    {
                        this.gb_Results.Text = "Instrukcie";
                        this.gb_Results.Enabled = true;
                        this.dgv_Instructions.Visible = true;
                        this.dgv_Instructions.Rows.Clear();
                        for (Int32 i = 0; i < ar_ChildTestsInfo.Length; i++)
                        {
                            ProductsConfigurationFile.ChildTestInfo actCTI = (ProductsConfigurationFile.ChildTestInfo)ar_ChildTestsInfo.GetValue(i);
                            if ((this.cb_TestType.Text == "OBA") && (actCTI.CameraInspection == "1"))
                                continue;
                            if ((actCTI.ScanBarcode.Trim() == "") && (actCTI.CameraInspection != "1"))
                            {                                
                                this.dgv_Instructions.Rows.Add(actCTI.Name.Substring(0,2), actCTI.Instruction, "", "PASS", "FAIL", "");
                                //this.dgv_Instructions.Rows[i].Cells[1].ToolTipText = actCTI.Description;
                            }
                            else
                            {                                
                                this.dgv_Instructions.Rows.Add(actCTI.Name, actCTI.Instruction, "", "START", "FAIL", "");
                                break;
                            }
                        }
                        this.dgv_Instructions.Columns[1].Width = this.gb_Results.Width - 12 - this.dgv_Instructions.Columns[0].Width - this.dgv_Instructions.Columns[2].Width - this.dgv_Instructions.Columns[3].Width - this.dgv_Instructions.Columns[4].Width - this.dgv_Instructions.Columns[5].Width;
                    }
                    else
                    {
                        this.gb_Results.Text = "Vysledky";
                        this.dgv_Instructions.Visible = false;
                    }              
                    
                }
                else
                {
                    this.eventDisabler = true;
                    this.cb_TestType.SelectedIndex = -1;
                    this.eventDisabler = false;
                    if (!this.resetingCB_TestType)
                    {
                        this.ErrorMessageBoxShow("Riadte sa prosim pokynmi, ktore su zvyraznene cervernou farbou. Dakujeme.", true);
                        b_errorMessageShowed = true;
                    }
                    this.ResetOrderTextBox();
                }                
            }
            else
            {
                this.gb_Results.Text = "Vysledky";
                this.dgv_Instructions.Visible = false;
            }
            if (this.lbl_JobIDValue.Text != "")
            {
                if (this.cb_TestType.SelectedIndex != -1)
                {
                    this.pb_TestType.Image = this.OKpict;
                    if (this.lbl_SerialNumber.Text != "")
                    {
                        this.eventDisabler = false;
                        this.actualOrder = 6;
                        if (this.gb_Results.Text == "Vysledky")
                        {
                            this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                        }
                        else
                        {
                            this.lbl_ScanEditOrder.Text = "Postupujte podla krokov v tabulke nizsie.";
                        }
                    }
                    else
                    {
                        this.pb_SerialNumber.Image = this.NOTOKpict;
                        this.actualOrder = 4;
                        this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte seriove cislo produktu:";
                    }

                    this.ResetOrderTextBox();
                }
                else
                {
                    if (!this.resetingCB_TestType && !b_errorMessageShowed)
                    {
                        this.ErrorMessageBoxShow("Zadany typ testu asi nie je spravny. V pripade, ze je spravny kontaktujte testovacieho inziniera.", true);
                    }
                    this.tb_OrderValue.Focus();
                    this.tb_OrderValue.SelectAll();
                }
            }
            else
            {
                this.cb_TestType.SelectedIndex = -1;
                if (!this.resetingCB_TestType && !b_errorMessageShowed)
                {
                    this.ErrorMessageBoxShow("Riadte sa prosim pokynmi, ktore su zvyraznene cervernou farbou. Dakujeme.", true);
                    this.tb_OrderValue.Enabled = true;
                }
                this.ResetOrderTextBox();
            }
            this.lbl_SerialNumber.Text = "";
            this.CheckFormDataForReportAndBatchSNEnter("testTypeSelectIndexChanged1");
        }

        private void modifyStationConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Pre aplikaciu zmien v station konfiguracii restartujte program. ");
            Process myProc = new Process();
            myProc.StartInfo.FileName = "notepad.exe";
            myProc.StartInfo.Arguments = String.Concat(this.StationConfig.BaseURI.Substring(this.StationConfig.BaseURI.IndexOf("C:/")));
            myProc.Start();

        }

        private void copyPublishedStationConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process myProc = new Process();
            myProc.StartInfo.FileName = "notepad.exe";
            myProc.StartInfo.Arguments = String.Concat("ConfigFiles/", StationConfigFileName);
            myProc.Start();
        }

        private void lbl_JobIDValue_TextChanged(object sender, EventArgs e)
        {
            this.CheckFormDataForReportAndBatchSNEnter("jobIDTextChanged1");
            if (this.lbl_JobIDValue.Text != "") this.pB_JobID.Image = this.OKpict;
            else this.pB_JobID.Image = this.NOTOKpict;
            if (this.CheckForDgvInstructionVisibility())
            {
                this.gb_Results.Text = "Instrukcie";
                this.dgv_Instructions.Visible = true;                
            }
            else
            {
                this.gb_Results.Text = "Vysledky";
                this.dgv_Instructions.Visible = false;
            }
        }

        private void lbl_SerialNumber_TextChanged(object sender, EventArgs e)
        {
            this.CheckFormDataForReportAndBatchSNEnter("serialnumberTextChanged1");            
            if (this.gb_Results.Text == "Instrukcie")
            {
                if ((this.cb_TestType.SelectedIndex != -1) && (this.lbl_SerialNumber.Text != ""))
                {
                    this.dgv_Instructions.Enabled = true;
                }
                else
                {
                    this.dgv_Instructions.Enabled = false;
                }
                this.ResetInstructionDGV();
            }
            if (!this.BelMESobj.Authorization.blnAuthorized && this.BelMESobj.Authorization.blnMustTraced)
            {

            }
        }

        private void btn_GenerateReport_Click(object sender, EventArgs e)
        {
            if (this.gb_Results.Text == "Instrukcie")
            {
                if (this.CheckForAllInstructionsAreComplete())
                {

                }
                else
                {
                    this.ErrorMessageBoxShow("Vyklikajte prosim tabulku s instrukciami pre test a az potom kliknite na \"Vytvor report\" tlacidlo. Dakujeme.", true);
                    return;
                }
            }
            if ((this.actualGrade.ToUpper() == "FAIL") && (this.tb_OrderValue.Text != ""))
            {
                DialogResult myDR = MessageBox.Show(String.Concat("Naozaj chcete vygenerovat report bez aktualneho popisu chyby - ", this.tb_OrderValue.Text, "?"), "POZOR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (myDR == DialogResult.No) return;
                if (myDR == DialogResult.Cancel) return;                
            }
            if (this.tb_OrderValue.Text.ToUpper() != "FAIL")            
            {
                this.tb_OrderValue.Text = "GenerateSigmaSureReport";
            }
            this.tb_OrderValue_KeyUp(new object(), new KeyPressEventArgs(Convert.ToChar(13)));
        }

        private void lbl_OperatorSurname_TextChanged(object sender, EventArgs e)
        {
            if (this.lbl_OperatorSurname.Text == "") this.btn_PasswordChange.Enabled = false;
            else this.btn_PasswordChange.Enabled = true;
        }

        private void btn_PasswordChange_Click(object sender, EventArgs e)
        {
            XmlDocument serverUserConfig = new XmlDocument();
            serverUserConfig.Load(this.StationConfig.SelectSingleNode("./Configuration/OperatorConfigurationFile").InnerText);

            PasswordChangeForm newForm = new PasswordChangeForm(this.lbl_OperatorSurname.Text, serverUserConfig);
            newForm.ShowDialog();

            System.IO.File.Copy(this.GetFilePathFromURI(serverUserConfig.BaseURI), this.GetFilePathFromURI(this.UserConfig.BaseURI), true);
            this.UserConfig.Load(this.GetFilePathFromURI(this.UserConfig.BaseURI));

            this.tb_OrderValue.Text = "LOGOUTUSER";
            this.eventDisabler = false;
            this.tb_OrderValue_KeyUp(sender, new KeyPressEventArgs(Convert.ToChar(13)));
            this.StationConfig.Load(String.Concat(this.ConfigPath, this.StationConfigFileName));
            this.tb_OrderValue.Text = "";
        }

        private void userMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDocument serverUserConfig = new XmlDocument();
            serverUserConfig.Load(this.StationConfig.SelectSingleNode("./Configuration/OperatorConfigurationFile").InnerText);

            OperatorData myOD = new OperatorData(this.lbl_OperatorSurname.Text, serverUserConfig);            

            UserMaintenanceForm newForm = new UserMaintenanceForm(serverUserConfig, this.lbl_OperatorSurname.Text);
            newForm.ShowDialog();

            System.IO.File.Copy(this.GetFilePathFromURI(serverUserConfig.BaseURI), this.GetFilePathFromURI(this.UserConfig.BaseURI), true);
            this.UserConfig.Load(this.GetFilePathFromURI(this.UserConfig.BaseURI));

            this.tb_OrderValue.Text = "LOGOUTUSER";
            this.eventDisabler = false;
            this.tb_OrderValue_KeyUp(sender, new KeyPressEventArgs(Convert.ToChar(13)));
            this.StationConfig.Load(String.Concat(this.ConfigPath, this.StationConfigFileName));
            this.tb_OrderValue.Text = "";
        }

        private void deleteOperatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDocument serverUserConfig = new XmlDocument();
            serverUserConfig.Load(this.StationConfig.SelectSingleNode("./Configuration/OperatorConfigurationFile").InnerText);

            OperatorSettingsForm newForm = new OperatorSettingsForm(this.lbl_OperatorSurname.Text, serverUserConfig);
            newForm.ShowDialog();

            System.IO.File.Copy(this.GetFilePathFromURI(serverUserConfig.BaseURI), this.GetFilePathFromURI(this.UserConfig.BaseURI), true);
            this.UserConfig.Load(this.GetFilePathFromURI(this.UserConfig.BaseURI));

            this.tb_OrderValue.Text = "LOGOUTUSER";
            this.eventDisabler = false;
            this.tb_OrderValue_KeyUp(sender, new KeyPressEventArgs(Convert.ToChar(13)));
            this.StationConfig.Load(String.Concat(this.ConfigPath, this.StationConfigFileName));
            this.tb_OrderValue.Text = "";
        }

        private void openChangeLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process myProc = new Process();
            myProc.StartInfo.FileName = "notepad.exe";
            myProc.StartInfo.Arguments = String.Concat("ChangeLog.txt");
            myProc.Start();
        }

        private void btn_AvailableSettings_Click(object sender, EventArgs e)
        {
            AlwaysAvailableSettingsForm myForm = new AlwaysAvailableSettingsForm(this.StationConfig);
            myForm.ShowDialog();
            this.ApplySettings();
            if (this.BelMESenabled)
            {
                this.BelMESobj.EmployeeVerification(this.lbl_OperatorNr.Text.Trim());
            }
        }

        private BatchSNEnterForm myForm;
        private void btn_BatchMode_Click(object sender, EventArgs e)
        {
            this.myForm = new BatchSNEnterForm(this.cb_ProductNo.Text, this.lbl_JobIDValue.Text, this.cb_TestType.Text, this.ProductsConfig, this.StationConfig, this.lbl_OperatorSurname.Text, this.lbl_OperatorNr.Text);
            if (this.BelMESobj.Activated)
            {
                this.myForm = new BatchSNEnterForm(this.cb_ProductNo.Text, this.lbl_JobIDValue.Text, this.cb_TestType.Text, this.ProductsConfig, this.StationConfig, this.lbl_OperatorSurname.Text, this.lbl_OperatorNr.Text, this.BelMESobj);
            }
            this.myForm.WindowState = FormWindowState.Maximized;
            this.WindowState = FormWindowState.Minimized;
            this.myForm.ShowDialog();
            this.myForm.Dispose();                       
            this.WindowState = FormWindowState.Maximized;
            this.tb_OrderValue.Focus();
        }

        private void lbl_ScanEditOrder_TextChanged(object sender, EventArgs e)
        {
            if (this.lbl_ScanEditOrder.Text.IndexOf("typ testu") != -1)
            {
                this.ShowBarcodePicture(this.TESTTYPESpict);
            }
            else if (this.lbl_ScanEditOrder.Text.IndexOf("PASS") != -1)
            {
                this.ShowBarcodePicture(this.PASSFAILpict);
            }
            else
            {
                this.ShowBarcodePicture(null);
            }
            if ((this.actualOrder == 5) || (this.actualOrder == 7))
            {
                this.ShowBarcodePicture(this.GENERATEREPORTpict);
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.dgv_Instructions.Columns[1].Width = this.gb_Results.Width - 10 - this.dgv_Instructions.Columns[0].Width - this.dgv_Instructions.Columns[2].Width - this.dgv_Instructions.Columns[3].Width - this.dgv_Instructions.Columns[4].Width - this.dgv_Instructions.Columns[5].Width;
            this.tb_OrderValue.Focus();
        }

        private void cb_LastSerialNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.eventDisabler)
            {
                if ((this.cb_ProductNo.Text != "") && (this.lbl_JobIDValue.Text != "") && (this.cb_TestType.Text != ""))
                {
                    this.lbl_SerialNumber.Text = this.cb_LastSerialNumbers.Text;
                    this.pb_SerialNumber.Image = this.OKpict;
                    this.tb_OrderValue.Text = "";
                    this.lbl_ScanEditOrder.Text = "Zoskenujte alebo zadajte vysledok testu (PASS alebo FAIL):";
                    String strProdIDbuff = this.cb_ProductNo.Text.Trim();
                    this.BelMESobj.BelMESAuthorization(String.Concat(this.lbl_JobIDValue.Text.Trim(), this.lbl_SerialNumber.Text.Trim()), this.cb_TestType.Text.Trim(), ref strProdIDbuff, "", true, false, "warning3");
                    this.actualOrder = 6;
                    this.tb_OrderValue.Focus();
                }
                else
                {

                }
            }
        }

        private void dgv_Instructions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 3) || (e.ColumnIndex > 4)) return;
            if (e.ColumnIndex == 3)
            {
                if (this.dgv_Instructions.Rows[e.RowIndex].Cells[3].Value.ToString() == "SCAN")
                {
                    InputBox_Form myIBF = new InputBox_Form("Pouzite snimac ciarovych kodov", this.dgv_Instructions.Rows[e.RowIndex].Cells[1].Value.ToString());
                    myIBF.ShowDialog();

                    if (myIBF.Answer != "")
                    {
                        Array ar_ChildTestsInfo = this.ProductsConfig.GetChildTestsInfos(this.cb_ProductNo.Text, this.cb_TestType.Text);
                        ProductsConfigurationFile.ChildTestInfo actCTI = (ProductsConfigurationFile.ChildTestInfo)ar_ChildTestsInfo.GetValue(e.RowIndex);

                        Boolean b_ScanInfoValidation = true;
                        if (myIBF.Answer.Length >= actCTI.ScanBarcode.Length)
                        {
                            for (int i = 0; i < actCTI.ScanBarcode.Length; i++)
                            {
                                if (actCTI.ScanBarcode[i].ToString().Trim() == "*")
                                    continue;
                                if (actCTI.ScanBarcode[i].ToString().Trim() != myIBF.Answer[i].ToString().Trim())
                                {
                                    b_ScanInfoValidation = false;
                                    break;
                                }
                            }
                        }
                        else
                            b_ScanInfoValidation = false;


                        if (b_ScanInfoValidation)
                        {
                            this.dgv_Instructions.Rows[e.RowIndex].Cells[2].Value = "PASS";                            
                            this.dgv_Instructions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        }
                        else
                        {
                            this.dgv_Instructions.Rows[e.RowIndex].Cells[2].Value = "FAIL";                            
                            this.dgv_Instructions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        }
                        this.dgv_Instructions.Rows[e.RowIndex].Cells[2].ToolTipText = myIBF.Answer;
                    }                    
                }
                else
                {
                    this.dgv_Instructions.Rows[e.RowIndex].Cells[2].Value = "PASS";
                    this.dgv_Instructions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    this.dgv_Instructions.Rows[e.RowIndex].Cells[2].ToolTipText = "";
                }
            }
            else if (e.ColumnIndex == 4)
            {
                this.dgv_Instructions.Rows[e.RowIndex].Cells[2].Value = "FAIL";
                this.dgv_Instructions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                this.dgv_Instructions.Rows[e.RowIndex].Cells[2].ToolTipText = "";
            }
            if (this.CheckForAllInstructionsAreComplete()) this.btn_GenerateReport.Enabled = true;
            else this.btn_GenerateReport.Enabled = false;
        }

        private void dgv_Instructions_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.dgv_Instructions.Enabled) return;
                if ((e.RowIndex < 0) || (e.ColumnIndex < 0))
                {
                    this.pb_BarcodesToScan.Image = null;
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                String str_PicturesPath = @"\\dcafs3\Testing_SRO\TESTING\SigmaSure\SSManualReportGenerator\PictureFolder\";
                Array ar_actChildTI = this.ProductsConfig.GetChildTestsInfos(this.cb_ProductNo.Text, this.cb_TestType.Text);

                if (ar_actChildTI.Length == 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                String str_StepName = this.dgv_Instructions.Rows[e.RowIndex].Cells[0].Value.ToString();
                foreach (ProductsConfigurationFile.ChildTestInfo actCTI in ar_actChildTI)
                {
                    if (actCTI.Name == str_StepName)
                    {
                        if (actCTI.PicturePath == "")
                        {
                            this.pb_BarcodesToScan.Image = null;
                            break;
                        }
                        str_PicturesPath = String.Concat(str_PicturesPath, actCTI.PicturePath);
                        if (Directory.Exists(Path.GetDirectoryName(str_PicturesPath)))
                        {
                            if (!System.IO.File.Exists(str_PicturesPath))
                            {
                                this.Cursor = Cursors.Default;
                                if (Path.GetFileName(str_PicturesPath) == "") 
                                    return;
                                if (Path.GetFileName(str_PicturesPath) == @"default.jpg")
                                    return;
                                else
                                {
                                    this.ErrorMessageBoxShow(String.Concat("Neexistuje subor \"", str_PicturesPath, "\". Zavolajte prosim testovacieho technika."), true);
                                }
                                return;
                            }
                            Image img_PictureToShow = Image.FromFile(str_PicturesPath);
                            this.pb_BarcodesToScan.Image = img_PictureToShow;
                            break;
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 7"));
            }
        }    

        private void cb_FaultCodes_Code_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cb_FaultCodes_Code.SelectedIndex != -1) && (!this.eventDisabler))
            {
                this.eventDisabler = true;
                this.cb_FaultCodes_Description.SelectedIndex = this.cb_FaultCodes_Code.SelectedIndex;
                this.eventDisabler = false;
                this.btn_FaultCodes_AddFailure.Enabled = true;
            }
            else
            {
                this.btn_FaultCodes_AddFailure.Enabled = false;
            }
        }

        private void cb_FaultCodes_Description_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cb_FaultCodes_Description.SelectedIndex != -1) && (!this.eventDisabler))
            {
                this.eventDisabler = true;
                this.cb_FaultCodes_Code.SelectedIndex = this.cb_FaultCodes_Description.SelectedIndex;
                this.eventDisabler = false;
                this.btn_FaultCodes_AddFailure.Enabled = true;
            }
            else
            {
                this.btn_FaultCodes_AddFailure.Enabled = false;
            }
        }

        private void btn_FaultCodes_AddFailure_Click(object sender, EventArgs e)
        {
            this.gb_Results.Controls.Add(new ResultGroupBoxSingleMode(String.Concat(this.cb_FaultCodes_Code.Text, " - ", this.cb_FaultCodes_Description.Text), "", new Point(10, ((this.gb_Results.Controls.Count - 1) * 53) + 20), new Size(this.gb_Results.Width - 20, 52)));
            this.cb_FaultCodes_Code.SelectedIndex = -1;
            this.cb_FaultCodes_Description.SelectedIndex = -1;
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.cb_ProductNo.SelectedIndex > -1)
            {                
                this.cb_ProductNo.SelectedIndex = -1;
                this.eventDisabler = true;
                this.resetingCB_TestType = true;
                this.cb_TestType.SelectedIndex = -1;
                this.pb_TestType.Image = this.NOTOKpict;
                this.resetingCB_TestType = false;
                this.eventDisabler = false;
                this.actualOrder = 1;
                this.lbl_ScanEditOrder.Text = "Zoskenujte 2D kod na vyrobku alebo zadajte nazov produktu alebo vyberte produkt z ponuky:";
            }
        }

        private void btn_BatchMode_EnabledChanged(object sender, EventArgs e)
        {
            if (!this.btn_BatchMode.Enabled)
            {
                toolTip1.SetToolTip(this.btn_BatchMode, "Nie je mozne pouzit hromadne zadavanie Seriovych cisel pre zvoleny druh testu.");
            }
            else
            {
                toolTip1.SetToolTip(this.btn_BatchMode, "Hromadne zadavanie seriovych cisiel.");
            }
        }

        private void tb_OrderValue_Enter(object sender, EventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                this.toolTip2.ShowAlways = true;
                this.toolTip2.IsBalloon = true;
                this.toolTip2.SetToolTip(this.tb_OrderValue, "Caps Lock is ON.");
            }
        }

        private void timer_ToForeground_Tick(object sender, EventArgs e)
        {
            if (this.BelMESobj.Emp.strEmployeeNumber == null) return;
            /*
            if ((this.BelMESobj.Emp.strEmployeeNumber.Trim() == "607") || (this.BelMESobj.Emp.strEmployeeNumber.Trim() == "1805"))
            {
                this.Activate();
                this.Focus();
                if (this.myForm != null)
                {
                    this.myForm.Activate();
                    this.myForm.Focus();
                }                
            }
            */
        }

        private void btn_SimpleMode_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            SimpleMode_Form MySimpleForm = new SimpleMode_Form(this.BelMESobj, this.lbl_StationName.Text);
            MySimpleForm.ShowDialog();
            if (!this.PartNoNeeded) Application.Exit();
        }
    }      
}

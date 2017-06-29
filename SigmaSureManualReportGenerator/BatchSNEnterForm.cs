using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Threading;
using System.Drawing;
using SigmaSure;
using BelMESCommon;

namespace SigmaSureManualReportGenerator
{
    public partial class BatchSNEnterForm : Form
    {
        public BatchSNEnterForm(String ProductNo, String JobID, String TestType, XmlDocument ProductsConfigXML, XmlDocument StationConfigXML, String OperatorName, String OperatorNr)
        {
            InitializeComponent();
            this.ProductNo = ProductNo;
            this.JobID = JobID;
            this.TestType = TestType;
            this.ProductsConfig = ProductsConfigXML;
            this.StationConfig = StationConfigXML;
            this.OperatorName = OperatorName;
            this.OperatorNr = OperatorNr;
            this.mySI = this.GetStationInfosForTest(this.TestType);
            this.BelMesObj = new BelMES("");
        }

        public BatchSNEnterForm(String ProductNo, String JobID, String TestType, XmlDocument ProductsConfigXML, XmlDocument StationConfigXML, String OperatorName, String OperatorNr, BelMES BelMesObj)
        {
            InitializeComponent();
            this.ProductNo = ProductNo;
            this.JobID = JobID;
            this.TestType = TestType;
            this.ProductsConfig = ProductsConfigXML;
            this.StationConfig = StationConfigXML;
            this.OperatorName = OperatorName;
            this.OperatorNr = OperatorNr;
            this.mySI = this.GetStationInfosForTest(this.TestType);
            this.BelMesObj = BelMesObj;
        }

        private String ProductNo;
        private String JobID;
        private String TestType;
        private String OperatorName;
        private String OperatorNr;
        private XmlDocument ProductsConfig;
        private XmlDocument StationConfig;
        private Boolean CheckingOfSerialNumberTested;
        private StationInfos mySI;
        private PropertyInfo[] myPIs = { };
        private Boolean CellMouseEnterActive = false;


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
        private struct PropertyInfo
        {
            public String SerialNumber;
            public String PropertyName;
            public String PropertyValue;
        }
        public class FailedStepInfo
        {
            public String Name;
            public String Description;
            public String Note;
        }
        public class SerialNumberFailedSteps
        {
            private String serialnumber;
            public FailedStepInfo[] FailedSteps = { };

            public SerialNumberFailedSteps()
            {
                this.serialnumber = "";
            }

            public SerialNumberFailedSteps(String SerialNumber)
            {
                this.serialnumber = SerialNumber;
            }

            public String SerialNumber
            {
                get
                {
                    return this.serialnumber;
                }
                set
                {
                    this.serialnumber = value;
                }
            }

            public void DeleteFailedStep(String Name)
            {
                FailedStepInfo[] buffer = { };
                foreach (FailedStepInfo actFSI in this.FailedSteps)
                {
                    if (actFSI.Name == Name)
                    {                        
                    }
                    else
                    {
                        Array.Resize(ref buffer, buffer.Length + 1);
                        buffer.SetValue(actFSI, buffer.Length - 1);
                    }
                }
                this.FailedSteps = buffer;
            }
        }

        private partial class ResultGroupBoxBatchMode : ResultGroupBox
        {
            public ResultGroupBoxBatchMode(String Name, String Description, Point Location, Size GBSize) : base(Name, Description, Location, GBSize)
            {
                this.tb_Description.KeyUp += this.tb_Description_KeyUp;
                this.btn_Delete.Click += this.btn_Delete_Click;
            }

            public void tb_Description_KeyUp(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {                    
                    BatchSNEnterForm myFrm = (BatchSNEnterForm)this.FindForm();
                    String str_SerialNumber = String.Concat(myFrm.lbl_JobID.Text, myFrm.gb_FailedSteps.Text.Substring(myFrm.gb_FailedSteps.Text.Length - 6, 5));
                    foreach (SerialNumberFailedSteps actSerialNumber in myFrm.FailedSerialNumbers)
                    {
                        if (actSerialNumber.SerialNumber == str_SerialNumber)
                        {
                            foreach (FailedStepInfo actFailedStep in actSerialNumber.FailedSteps)
                            {
                                if (actFailedStep.Name == this.Text)
                                {
                                    actFailedStep.Description = this.tb_Description.Text.Trim();
                                    this.tb_Description.Text = actFailedStep.Description;
                                    this.tb_Description.SelectAll();
                                }
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    BatchSNEnterForm myFrm = (BatchSNEnterForm)this.FindForm();
                    String str_SerialNumber = String.Concat(myFrm.lbl_JobID.Text, myFrm.gb_FailedSteps.Text.Substring(myFrm.gb_FailedSteps.Text.Length - 6, 5));
                    foreach (SerialNumberFailedSteps actSerialNumber in myFrm.FailedSerialNumbers)
                    {
                        if (actSerialNumber.SerialNumber == str_SerialNumber)
                        {
                            foreach (FailedStepInfo actFailedStep in actSerialNumber.FailedSteps)
                            {
                                if (actFailedStep.Name == this.Text)
                                {                                    
                                    this.tb_Description.Text = actFailedStep.Description;
                                    this.tb_Description.SelectAll();
                                }
                            }
                        }
                    }
                }
            }

            private void btn_Delete_Click(object sender, EventArgs e)
            {
                BatchSNEnterForm myFrm = (BatchSNEnterForm)this.FindForm();
                String str_SerialNumber = String.Concat(myFrm.lbl_JobID.Text, myFrm.gb_FailedSteps.Text.Substring(myFrm.gb_FailedSteps.Text.Length - 6, 5));
                SerialNumberFailedSteps actSNFS = new SerialNumberFailedSteps();
                foreach (SerialNumberFailedSteps actSerialNumber in myFrm.FailedSerialNumbers)
                {
                    if (actSerialNumber.SerialNumber == str_SerialNumber)
                    {
                        foreach (FailedStepInfo actFailedStep in actSerialNumber.FailedSteps)
                        {
                            if (actFailedStep.Name == this.Text)
                            {
                                this.tb_Description.Text = actFailedStep.Description;
                                this.tb_Description.SelectAll();
                            }
                            actSerialNumber.DeleteFailedStep(this.Text);
                            actSNFS = actSerialNumber;
                        }
                    }
                }
                myFrm.gb_FailedSteps.Controls.Clear();
                foreach (FailedStepInfo actFSI in actSNFS.FailedSteps)
                {
                    Point actLocation = new Point();
                    actLocation.X = 10;
                    actLocation.Y = (myFrm.gb_FailedSteps.Controls.Count * 53) + 20;
                    Size actSize = new Size(myFrm.gb_FailedSteps.Width - 20, 52);
                    ResultGroupBoxBatchMode rgbToAdd = new ResultGroupBoxBatchMode(actFSI.Name, actFSI.Description, actLocation, actSize);
                    myFrm.gb_FailedSteps.Controls.Add(rgbToAdd);
                }
            }
        }

        private BelMES BelMesObj;
        private String[] AuthorizedSNs = { };
        public SerialNumberFailedSteps[] FailedSerialNumbers = { };        
        
        private StationInfos GetStationInfosForTest(String TestName)
        {
            StationInfos retVal = new StationInfos();
            XmlNode StationConfigInfosNode = this.StationConfig.SelectSingleNode("./Configuration/Station");
            if (StationConfigInfosNode == null) this.ErrorMessageBoxShow("Neexistuje node Configuration/Station v StationConfig subore. Zavolajte prosim testovacieho technika.");
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
        private bool PathExists(string path)
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
        private void SaveErrorToLogFile(String ErrorMessage)
        {
            String str_ErrorLogFileName = String.Concat("ErrorLog_", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), ".txt");
            FileStream fs;
            if (PathExists(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\"))
            {
                if (!File.Exists(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName)))
                {
                    fs = File.Create(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName));
                }
                else
                {
                    fs = new FileStream(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName), FileMode.Append);
                    //fs = File.OpenWrite(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName));
                }
            }
            else
            {
                String str_localErrorDir = Path.GetDirectoryName(this.StationConfig.BaseURI.Substring(this.StationConfig.BaseURI.IndexOf(@"C:\")));
                if (str_localErrorDir.Substring(str_localErrorDir.Length - 1, 1) != "\\")
                {
                    str_localErrorDir = String.Concat(str_localErrorDir, "\\");
                }
                if (!File.Exists(String.Concat(str_localErrorDir, @"ErrorLogDir\", str_ErrorLogFileName)))
                {
                    fs = File.Create(String.Concat(str_localErrorDir, @"ErrorLogDir\", str_ErrorLogFileName));
                }
                else
                {
                    fs = new FileStream(String.Concat(str_localErrorDir, @"ErrorLogDir\", str_ErrorLogFileName), FileMode.Append);
                    //fs = File.OpenWrite(String.Concat(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\", str_ErrorLogFileName));
                }
            }

            String StationID = this.mySI.Name;
            String ActualTime = String.Concat(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), "_", DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"), DateTime.Now.Second.ToString("D2"));
            String Operator = this.OperatorName;
            String ActualOrderString = this.tb_ScanField.Text;

            try
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(String.Concat(ActualTime, ";", StationID, ";", Operator, ";", ActualOrderString, ";", ErrorMessage));
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " Zavolajte prosim testovacieho inziniera."));
            }
        }
        private void GenerateReport(String SerialNumber, DateTime StartTime)
        {
            DateTime starttime = StartTime;
            DateTime endtime = StartTime.AddSeconds(5);

            UnitReport myReport = new UnitReport(starttime, endtime, "D", "", false);

            XmlNode OperatorNameMode = this.StationConfig.SelectSingleNode("./Configuration/OperatorNameMode");

            if (this.StationConfig.SelectSingleNode("./Configuration/OperatorNameMode").InnerText == "Number")
            {
                myReport.Operator.name = this.OperatorNr;
            }
            else if (this.StationConfig.SelectSingleNode("./Configuration/OperatorNameMode").InnerText == "Surname")
            {
                myReport.Operator.name = this.OperatorName;
            }
            else
            {
                MessageBox.Show("Neznama hodnota OperationNameMode v Station config subore. Prosim, zavolajte testovacieho inziniera.");
                return;
            }

            myReport.Cathegory = new _Cathegory("Default");
            if (SerialNumber.Length == 5)
            {
                SerialNumber = string.Concat(this.lbl_JobID.Text, SerialNumber);
            }
            myReport.Cathegory.Product = new _Product(this.lbl_ProductNo.Text, SerialNumber);

            myReport.TestRun.name = this.lbl_TestType.Text;
            foreach (DataGridViewRow actDGVR in this.dgv_SerialNumbers.Rows)
            {
                if (String.Concat(this.lbl_JobID.Text, actDGVR.Cells[0].Value.ToString().Trim()) == myReport.Cathegory.Product.SerialNo)
                {
                    if (actDGVR.DefaultCellStyle.BackColor == Color.Green)
                    {
                        myReport.TestRun.grade = "PASS";
                    }
                    else
                    {
                        myReport.TestRun.grade = "FAIL";
                    }
                }
            }            
            myReport.starttime = starttime;
            myReport.endtime = endtime;

            myReport.AddProperty("Work Order", this.lbl_JobID.Text);
            XmlNode URModeNode = this.StationConfig.LastChild.SelectSingleNode("./Mode");
            if (URModeNode == null)
            {
                MessageBox.Show("V Station configu chyba informacia o mode testu.");
                return;
            }
            String URMode = URModeNode.InnerText;
            if (URMode == "")
            {
                URMode = "D";
            }
            else
            {
                myReport.mode = URMode;
            }
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

            foreach (PropertyInfo actPI in this.myPIs)
            {
                if (actPI.SerialNumber == myReport.Cathegory.Product.SerialNo.Substring(8))
                {
                    myReport.AddProperty(actPI.PropertyName, actPI.PropertyValue);
                }
            }

            XmlNode commonPropertiesNode = this.StationConfig.LastChild.SelectSingleNode("./Properties");
            foreach (XmlNode actAssNode in commonPropertiesNode.ChildNodes)
            {
                String actNameNode = actAssNode.Name.Replace('_', ' ');
                myReport.AddProperty(actNameNode, actAssNode.InnerText);
            }

            StationInfos actSI = this.GetStationInfosForTest(this.lbl_TestType.Text);

            foreach (StationInfos.StationProperty actProp in actSI.StationProperties)
            {
                String actNameNode = actProp.Name.Replace('_', ' ');
                myReport.AddProperty(actNameNode, actProp.Value);
            }

            String StationName = actSI.Name;
            String StationGUID = actSI.GUID;

            myReport.Station = new _Station(StationGUID, StationName);

            foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
            {                
                if (myReport.Cathegory.Product.SerialNo == actSNFS.SerialNumber)
                {
                    myReport.AddProperty("To Repair", "Yes");
                    foreach (FailedStepInfo actFSI in actSNFS.FailedSteps)
                    {
                        myReport.TestRun.AddTestRunChildValueString(actFSI.Name, myReport.starttime, myReport.endtime, "FAIL", actFSI.Description, "*$*");
                    }
                }                
            }

            XmlNode reportPathNode = this.StationConfig.LastChild.SelectSingleNode("./ReportPath/Path");
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
                MessageBox.Show(ex.Message);
            }

            if (this.BelMesObj.Activated)
            {
                try
                {
                    foreach (String actSN in this.AuthorizedSNs)
                    {
                        if (actSN == myReport.Cathegory.Product.SerialNo.Substring(8))
                        {
                            String testKind = this.lbl_TestType.Text;
                            if (testKind == "Adjustement") testKind = "Adjustment";
                            this.BelMesObj.SetActualResult(myReport.Cathegory.Product.SerialNo, testKind, String.Concat(myReport.TestRun.grade, "ED"), XmlReportContent);
                            break;
                        }
                    }
                    //this.Authorization.TryAuthorization(myReport.Cathegory.Product.SerialNo, testKind, String.Concat(myReport.TestRun.grade, "ED"), this.Env, false, false, XmlReportContent);
                }
                catch
                {
                                     
                }    
            }
        }
        private void CheckButtonsAvailability()
        {
            if (this.dgv_SerialNumbers.Rows.Count > 0)
            {
                this.btn_CreateReports.Enabled = true;
                if (this.dgv_SerialNumbers.Rows.Count > 1)
                {
                    this.btn_SortSNs.Enabled = true;
                }
                else
                {
                    this.btn_SortSNs.Enabled = false;
                }
            }
            else
            {
                this.btn_CreateReports.Enabled = false;
                this.btn_SortSNs.Enabled = false;
            }
        }        
        private void FailedStepComponentsVisibility(Boolean Visibled)
        {
            if (Visibled)
            {
                this.label7.Visible = true;
                this.tb_FailedStepNameEnter.Visible = true;
                this.gb_FailedSteps.Visible = true;
            }
            else
            {
                this.label7.Visible = false;
                this.tb_FailedStepNameEnter.Visible = false;
                this.gb_FailedSteps.Visible = false;
            }        
        }
        private int DeleteSerialNumberFailedSteps(String SerialNumber)
        {
            SerialNumberFailedSteps[] bufferArray = { };
            try
            {
                foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
                {
                    if (actSNFS.SerialNumber != SerialNumber)
                    {
                        Array.Resize(ref bufferArray, bufferArray.Length + 1);
                        bufferArray.SetValue(actSNFS, bufferArray.Length - 1);
                    }
                }
                this.FailedSerialNumbers = bufferArray;
            }
            catch (Exception ex)
            {
                this.ErrorMessageBoxShow(String.Concat("Vyskytla sa chyba pri vymazavani serioveho cisla. Zavolajte prosim testovacieho technika. ", ex.Message, " ", ex.InnerException.Message));
            }
            return 0;
        }

        private void BatchSNEnterForm_Load(object sender, EventArgs e)
        {            
            this.lbl_ProductNo.Text = this.ProductNo;
            this.lbl_JobID.Text = this.JobID;
            this.lbl_TestType.Text = this.TestType;
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
            this.Activate();
            this.tb_ScanField.Focus();
        }

        private void tb_ScanField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((this.tb_ScanField.Text.Trim() != "") && (e.KeyChar == (char)Keys.Enter))
            {
                this.FailedStepComponentsVisibility(false);
                String str_ProdID = ProductBarcode.GetProductNoFromBarcode(this.tb_ScanField.Text);
                if (str_ProdID != this.tb_ScanField.Text)
                {
                    if ((str_ProdID != "") && (str_ProdID != this.lbl_ProductNo.Text))
                    {
                        this.ErrorMessageBoxShow(String.Concat("Oskenovany vyrobok nie je typ \"", this.lbl_ProductNo.Text, "\", ale typ \"", str_ProdID,  "\". V pripade novej zakazky prosim vytvorte reporty a az potom oskenujte tento vyrobok. Dakujem. V pripade komplikacii volajte prosim testovacieho technika."));
                        this.tb_ScanField.Text = "";
                        this.tb_ScanField.SelectAll();
                        this.tb_ScanField.Focus();
                        return;
                    }
                }
                String str_JobID = ProductBarcode.GetJobIdFromBarcode(this.lbl_ProductNo.Text, this.tb_ScanField.Text);
                if (str_JobID != this.tb_ScanField.Text)
                {
                    if ((str_JobID != "") && (str_JobID != this.lbl_JobID.Text))
                    {
                        this.ErrorMessageBoxShow(String.Concat("Oskenovany vyrobok nie je z JobID \"", this.lbl_JobID.Text, "\", ale \"", str_JobID, ". V pripade novej zakazky prosim vytvorte reporty a az potom oskenujte tento vyrobok. Dakujem. V pripade komplikacii volajte prosim testovacieho technika."));
                        this.tb_ScanField.Text = "";
                        this.tb_ScanField.SelectAll();
                        this.tb_ScanField.Focus();
                        return;
                    }
                }
                String str_FormatedSN = ProductBarcode.GetSerialNumberFromBarcode(this.lbl_ProductNo.Text, this.tb_ScanField.Text);
                if (str_FormatedSN != "")
                {                
                    if (str_FormatedSN.Length > 5)
                    {
                        this.tb_ScanField.Text = "";
                        this.tb_ScanField.SelectAll();
                        this.tb_ScanField.Focus();
                        return;
                    }
                    foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
                    {
                        if (actRow.Cells[0].Value != null)
                        {
                            if (actRow.Cells[0].Value.ToString() == str_FormatedSN)
                            {
                                this.tb_ScanField.Text = "";
                                this.tb_ScanField.SelectAll();
                                this.tb_ScanField.Focus();
                                return;
                            }
                        }
                    }

                    if (SpecialRequirements.IsActive(this.ProductsConfig, this.lbl_ProductNo.Text, "CustomSerialNumber"))
                    {
                        InputBox_Form myForm = new InputBox_Form("Custom Serial Number", "Oskenujte Custom Serial Number");
                        myForm.ShowDialog();
                        if (myForm.Answer != "")
                        {
                            PropertyInfo actPI;
                            actPI.SerialNumber = str_FormatedSN;
                            actPI.PropertyName = "CustomSerialNumber";
                            actPI.PropertyValue = myForm.Answer;
                            Array.Resize(ref this.myPIs, this.myPIs.Length + 1);
                            this.myPIs.SetValue(actPI, this.myPIs.Length - 1);
                        }                        
                    }

                    if (this.BelMesObj.Activated)
                    {
                        if (this.BelMesObj.BelMESAuthorization(String.Concat(this.JobID, str_FormatedSN), this.TestType, this.ProductNo, "", false))                        
                        {
                            Array.Resize(ref this.AuthorizedSNs, this.AuthorizedSNs.Length + 1);
                            this.AuthorizedSNs.SetValue(str_FormatedSN, this.AuthorizedSNs.Length - 1);
                        }
                    }  

                    this.dgv_SerialNumbers.Rows.Add(str_FormatedSN, "PASS", "FAIL", "DELETE");
                    this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Green;
                    this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Green;
                    this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                    this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Black;
                    //this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].Cells[0].Value = str_FormatedSN;                    
                    this.tb_ScanField.Text = "";
                    this.CheckButtonsAvailability();
                }
                this.tb_ScanField.SelectAll();
                this.tb_ScanField.Focus();                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_CreateReports_Click(object sender, EventArgs e)
        {
            String[] ar_serialNumbers = { };
            foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
            {                
                foreach (String actSN in ar_serialNumbers)
                {
                    if (actRow.Cells[0].Value.ToString() == actSN)
                    {
                        DialogResult myRes = MessageBox.Show(String.Concat("Seriove cislo \"", actSN, "\"je v zozname viackrat. Chcete napriek tomu vytvorit reporty so zadanymi seriovymi cislami?"), "UPOZORNENIE", MessageBoxButtons.YesNo);
                        if (myRes == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        
                    }                    
                }
                Array.Resize(ref ar_serialNumbers, ar_serialNumbers.Length + 1);
                ar_serialNumbers.SetValue(actRow.Cells[0].Value.ToString(), ar_serialNumbers.Length - 1);
            }
            if (this.CheckingOfSerialNumberTested)
            {
                TxtDatabase TD = new TxtDatabase();

                foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
                {                    
                    if (actRow.Cells[0].Value.ToString() != "")
                    {
                        if (TD.CheckSerialNumberAndTestType(this.lbl_JobID.Text.Trim(), actRow.Cells[0].Value.ToString().Trim(), this.lbl_TestType.Text))
                        {
                            if (MessageBox.Show(String.Concat("V databaze sa uz nachadza PASS vysledok pre vyrobok s JobID \"", this.lbl_JobID.Text.Trim(), "\" a so seriovym cislom \"", actRow.Cells[0].Value.ToString().Trim(), "\" pre typ testu \"", this.lbl_TestType.Text, "\".\n\nChcete report aj tak poslat?"), "POZOR", MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            Int32 counter = 0;
            try
            {
                foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
                {
                    if (actRow.Cells[0].Value.ToString().Trim() != "")
                    {                        
                        this.GenerateReport(actRow.Cells[0].Value.ToString().Trim(), DateTime.Now.AddSeconds(counter * 5));
                        counter++;
                    }
                }
                MessageBox.Show("Vsetky reporty boli uspesne vytvorene.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " Zavolajte prosim testovacieho inziniera."), "CHYBA");
            }
            if (this.CheckingOfSerialNumberTested)
            {
                TxtDatabase TD = new TxtDatabase();
                foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
                {
                    if (actRow.Cells[0].Value.ToString().Trim() != "")
                    {
                        if (actRow.DefaultCellStyle.BackColor == Color.Green) TD.SaveSerialNumberAndTestTypeToLogFile(this.lbl_JobID.Text.Trim(), actRow.Cells[0].Value.ToString().Trim(), this.lbl_TestType.Text);
                    }
                }
            }
            this.Dispose();
        }

        private void ErrorMessageBoxShow(String Message)
        {
            MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Int32 i = 0;
            this.SaveErrorToLogFile(Message);
            while (i < 9999999) i++;
            this.tb_ScanField.SelectAll();
            this.tb_ScanField.Focus();
        }

        private void btn_SortSNs_Click(object sender, EventArgs e)
        {
            String[] ar_SNs = { };
            foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
            {
                if (actRow.Cells[0].ToString().Trim() != "")
                {
                    Array.Resize(ref ar_SNs, ar_SNs.Length + 1);
                    ar_SNs.SetValue(actRow.Cells[0].Value.ToString(), ar_SNs.GetUpperBound(0));
                }
            }
            Array.Sort(ar_SNs);
            //this.dgv_SerialNumbers.Rows.Clear();

            for (int i = 0; i < ar_SNs.Length; i++)
            {
                this.dgv_SerialNumbers.Rows[i].Cells[0].Value = ar_SNs.GetValue(i).ToString();                
            }

            foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
            {
                String actSN = string.Concat(this.lbl_JobID.Text, actRow.Cells[0].Value.ToString().Trim());
                foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
                {
                    if (actSNFS.SerialNumber == actSN)
                    {
                        actRow.DefaultCellStyle.BackColor = Color.Red;
                        actRow.DefaultCellStyle.SelectionBackColor = Color.Red;
                    }
                    else
                    {
                        actRow.DefaultCellStyle.BackColor = Color.Green;
                        actRow.DefaultCellStyle.SelectionBackColor = Color.Green;
                    }
                }
            }
            this.tb_ScanField.Focus();
        }

        private void dgv_SerialNumbers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.gb_FailedSteps.Text = String.Concat(@"Failed kroky pre SN """, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString(), @"""");
                    this.CellMouseEnterActive = true;
                }
            }
            catch
            { }
        }

        private void tsmi_CopySN_Click(object sender, EventArgs e)
        {
            if (this.dgv_SerialNumbers.Rows.Count < 1) return;
            String str_SNtoCB = "";
            foreach (DataGridViewRow actRow in this.dgv_SerialNumbers.Rows)
            {
                str_SNtoCB = String.Concat(str_SNtoCB, "\n", actRow.Cells[0].Value.ToString());
            }
            Clipboard.SetText(str_SNtoCB);
            this.tb_ScanField.Focus();
        }

        private void tsmi_PasteSN_Click(object sender, EventArgs e)
        {
            String str_CBdata = Clipboard.GetText();
            if (str_CBdata == "") return;
            String[] ar_SNs = { };
            int i = 0;
            while (i != -1)
            {
                String str_actSN = "";
                i = str_CBdata.IndexOf("\n");
                if (i == -1)
                {
                    str_actSN = str_CBdata;
                }
                else
                {
                    str_actSN = str_CBdata.Substring(0, i);
                    str_CBdata = str_CBdata.Substring(i + 1);
                }
                if (str_actSN == "") continue;
                if (str_actSN.Length != 5) continue;
                try
                {
                    Convert.ToInt16(str_actSN);
                }
                catch
                {
                    continue;
                }
                this.dgv_SerialNumbers.Rows.Add(str_actSN, "PASS", "FAIL", "DELETE");
                this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                this.dgv_SerialNumbers.Rows[this.dgv_SerialNumbers.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Green;
            }
            this.tb_ScanField.Text = "";
            this.CheckButtonsAvailability();
            this.tb_ScanField.Focus();
        }

        private void dgv_SerialNumbers_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.CellMouseEnterActive = false;
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex < 0) return;
                String actSerialNumber = String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString().Trim());
                // highlight serial number
                if (e.ColumnIndex == 0)
                {                    
                    if (this.dgv_SerialNumbers.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Green)
                    {
                        this.FailedStepComponentsVisibility(false);
                    }
                    else
                    {
                        this.gb_FailedSteps.Controls.Clear();

                        Boolean actSNFounded = false;
                        SerialNumberFailedSteps actSNFStoShow = new SerialNumberFailedSteps();
                        foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
                        {
                            if (actSNFS.SerialNumber == String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString()))
                            {
                                actSNFounded = true;
                                actSNFStoShow = actSNFS;
                                break;
                            }
                        }
                        if (!actSNFounded)
                        {
                            actSNFStoShow = new SerialNumberFailedSteps(String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        }
                        foreach (FailedStepInfo actFS in actSNFStoShow.FailedSteps)
                        {
                            Point actLocation = new Point();
                            actLocation.X = 10;
                            actLocation.Y = (this.gb_FailedSteps.Controls.Count * 53) + 20;
                            Size actSize = new Size(this.gb_FailedSteps.Width - 20, 52);
                            ResultGroupBoxBatchMode rgbToAdd = new ResultGroupBoxBatchMode(actFS.Name, actFS.Description, actLocation, actSize);
                            this.gb_FailedSteps.Controls.Add(rgbToAdd);
                        }
                        this.FailedStepComponentsVisibility(true);

                    }
                }
                // pass button
                else if (e.ColumnIndex == 1)
                {
                    this.DeleteSerialNumberFailedSteps(String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    this.dgv_SerialNumbers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    this.dgv_SerialNumbers.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Green;
                    this.FailedStepComponentsVisibility(false);

                    if (this.BelMesObj.Activated)
                    {
                        Int32 actSNIndex = Array.IndexOf(this.AuthorizedSNs, actSerialNumber);
                        if (actSNIndex == -1)
                        {
                            Array.Resize(ref this.AuthorizedSNs, this.AuthorizedSNs.Length + 1);
                            this.AuthorizedSNs.SetValue(actSerialNumber, this.AuthorizedSNs.Length - 1);
                        }
                    }
                }
                // fail button
                else if (e.ColumnIndex == 2)
                {
                    this.gb_FailedSteps.Controls.Clear();

                    Boolean actSNFounded = false;
                    SerialNumberFailedSteps actSNFStoShow = new SerialNumberFailedSteps();
                    foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
                    {
                        if (actSNFS.SerialNumber == String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                        {
                            actSNFounded = true;
                            actSNFStoShow = actSNFS;
                            break;
                        }
                    }
                    if (!actSNFounded)
                    {
                        actSNFStoShow = new SerialNumberFailedSteps(String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    }
                    foreach (FailedStepInfo actFS in actSNFStoShow.FailedSteps)
                    {
                        Point actLocation = new Point();
                        actLocation.X = 10;
                        actLocation.Y = (this.gb_FailedSteps.Controls.Count * 53) + 20;
                        Size actSize = new Size(this.gb_FailedSteps.Width - 20, 52);
                        ResultGroupBoxBatchMode rgbToAdd = new ResultGroupBoxBatchMode(actFS.Name, actFS.Description, actLocation, actSize);
                        this.gb_FailedSteps.Controls.Add(rgbToAdd);
                    }
                    this.FailedStepComponentsVisibility(true);
                    this.dgv_SerialNumbers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    this.dgv_SerialNumbers.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
                    this.tb_FailedStepNameEnter.Focus();
                }
                // delete button
                else if (e.ColumnIndex == 3)
                {
                    
                    PropertyInfo[] bufferPIs = { };
                    foreach (PropertyInfo actPI in this.myPIs)
                    {
                        if (actPI.SerialNumber != actSerialNumber)
                        {
                            Array.Resize(ref bufferPIs, bufferPIs.Length + 1);
                            bufferPIs.SetValue(actPI, bufferPIs.Length - 1);
                        }
                    }
                    if (this.BelMesObj.Activated)
                    {
                        Int32 actSNIndex = Array.IndexOf(this.AuthorizedSNs, actSerialNumber);
                        if (actSNIndex != -1)
                        {                            
                            String[] TempArray = { };
                            for (Int32 i = 0; i < this.AuthorizedSNs.Length; i++)
                            {
                                if (i != actSNIndex)
                                {
                                    Array.Resize(ref TempArray, TempArray.Length + 1);
                                    TempArray.SetValue(this.AuthorizedSNs.GetValue(i), TempArray.Length - 1);
                                }
                            }
                            this.AuthorizedSNs = TempArray;                            
                        }
                    }
                    this.myPIs = bufferPIs;
                    this.DeleteSerialNumberFailedSteps(String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()));
                    this.dgv_SerialNumbers.Rows.RemoveAt(e.RowIndex);
                }
                
                
            }            
            else if (e.Button == MouseButtons.Right)
            {
                cms_BatchNumbersCopyPaste.Show(Cursor.Position);
            }
            this.CheckButtonsAvailability();
        }

        private void tb_FailedStepNameEnter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (this.tb_FailedStepNameEnter.Text.Trim() == "") return;
                Point actLocation = new Point();
                actLocation.X = 10;
                actLocation.Y = (this.gb_FailedSteps.Controls.Count * 53) + 20;
                Size actSize = new Size(this.gb_FailedSteps.Width - 20, 52);
                ResultGroupBoxBatchMode rgbToAdd = new ResultGroupBoxBatchMode(this.tb_FailedStepNameEnter.Text.Trim(), "", actLocation, actSize);
                this.gb_FailedSteps.Controls.Add(rgbToAdd);
                FailedStepInfo fsi_actual = new FailedStepInfo();
                fsi_actual.Name = this.tb_FailedStepNameEnter.Text.Trim();
                fsi_actual.Description = "";
                Boolean founded = false;
                SerialNumberFailedSteps actSNFSfounded;
                foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
                {
                    if (actSNFS.SerialNumber == String.Concat(this.lbl_JobID.Text, this.gb_FailedSteps.Text.Substring(this.gb_FailedSteps.Text.Length - 6, 5)));
                    {
                        Array.Resize(ref actSNFS.FailedSteps, actSNFS.FailedSteps.Length + 1);
                        actSNFS.FailedSteps.SetValue(fsi_actual, actSNFS.FailedSteps.Length - 1);
                        founded = true;
                        actSNFSfounded = actSNFS;
                        break;
                    }
                }
                if (!founded)
                {
                    actSNFSfounded = new SerialNumberFailedSteps(String.Concat(this.lbl_JobID.Text, this.gb_FailedSteps.Text.Substring(this.gb_FailedSteps.Text.Length - 6, 5)));                    
                    Array.Resize(ref actSNFSfounded.FailedSteps, actSNFSfounded.FailedSteps.Length + 1);
                    actSNFSfounded.FailedSteps.SetValue(fsi_actual, actSNFSfounded.FailedSteps.Length - 1);
                    Array.Resize(ref this.FailedSerialNumbers, this.FailedSerialNumbers.Length + 1);
                    this.FailedSerialNumbers.SetValue(actSNFSfounded, this.FailedSerialNumbers.Length - 1);
                }
                this.tb_FailedStepNameEnter.Text = "";
            }
        }     

        private void dgv_SerialNumbers_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.CellMouseEnterActive)
            {
                this.gb_FailedSteps.Controls.Clear();
                foreach (SerialNumberFailedSteps actSNFS in this.FailedSerialNumbers)
                {
                    if (actSNFS.SerialNumber == String.Concat(this.lbl_JobID.Text, this.dgv_SerialNumbers.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                    {
                        foreach (FailedStepInfo actFS in actSNFS.FailedSteps)
                        {
                            Point actLocation = new Point();
                            actLocation.X = 10;
                            actLocation.Y = (this.gb_FailedSteps.Controls.Count * 53) + 10;
                            Size actSize = new Size(this.gb_FailedSteps.Width - 20, 52);
                            ResultGroupBoxBatchMode rgbToAdd = new ResultGroupBoxBatchMode(actFS.Name, actFS.Description, actLocation, actSize);
                            this.gb_FailedSteps.Controls.Add(rgbToAdd);
                        }
                        this.FailedStepComponentsVisibility(true);
                        break;
                    }
                }
                this.FailedStepComponentsVisibility(false);
            }
        }        
    }
}

using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using SigmaSure;
using System.Xml;
using System.Net.NetworkInformation;
using BelMESCommon;

namespace SigmaSureManualReportGenerator
{
    public partial class SimpleMode_Form : Form
    {
        readonly StationConfig myStationConfig = new StationConfig();
        String str_OperatorNr = "";
        readonly BelMES objBelMes;
        String ConfigPath = "";
        private ProductsConfigurationFile ProductsConfig;
        private SerialNumberSteps act_SNS;
        private Boolean BatchModeAvailable = false;
        //private Boolean JobIDChecking = false;
        public String StationName = "";

        /*
        private struct PropertyInfo
        {
            public String SerialNumber;
            public String PropertyName;
            public String PropertyValue;
        }
        */
        public class StepInfo
        {
            public String Name = "";
            public String Description = "";
            public String Note = "";
            public String Grade = "";
            public String ResultValue = "";
        }
        public class SerialNumberSteps
        {
            private String serialnumber;
            public StepInfo[] Steps = { };

            public SerialNumberSteps()
            {
                this.serialnumber = "";
            }

            public SerialNumberSteps(String SerialNumber)
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

            public void DeleteStep(String Name)
            {
                StepInfo[] buffer = { };
                foreach (StepInfo actFSI in this.Steps)
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
                this.Steps = buffer;
            }
        }

        public SimpleMode_Form(BelMES objBM, String StationName)
        {
            this.objBelMes = objBM;
            this.StationName = StationName;
            InitializeComponent();
        }

        private void SimpleMode_Form_Load(object sender, EventArgs e)
        {
            Int32 n_majorOSVersion = Environment.OSVersion.Version.Major;

            if (n_majorOSVersion == 5) this.ConfigPath = @"C:\Documents and Settings\All Users\Application Data\SSManualReportGenerator\";
            else if (n_majorOSVersion == 6) this.ConfigPath = @"C:\Users\Public\SSManualReportGenerator\";
            else
            {
                MessageBox.Show("Neznama verzia operacneho systemu. Zavolajte prosim testovacieho inziniera");
                this.Dispose();
                return;
            }

            this.ProductsConfig = new ProductsConfigurationFile(this.ConfigPath);
            try
            {
                ComboBox cb_TestTypesParentForm = (ComboBox)Application.OpenForms[0].Controls["gb_Product"].Controls["cb_TestType"];
                foreach (String actTest in cb_TestTypesParentForm.Items)
                {
                    this.cb_TestTypes.Items.Add(actTest);
                }

                Label lbl_operatorNr = (Label)Application.OpenForms[0].Controls["groupBox2"].Controls["lbl_OperatorNr"];
                str_OperatorNr = lbl_operatorNr.Text.Trim();

                if (this.cb_TestTypes.Items.Count == 1)
                {
                    this.cb_TestTypes.SelectedIndex = 0;
                    this.lbl_Info.Text = "Oskenujte SerialNumber z produktu";
                }
                else
                {
                    this.lbl_Info.Text = "Vyberte typ testu";
                    this.cb_TestTypes.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Text = String.Concat("Simple Tracing - Operator: ", this.objBelMes.Emp.strEmployeeName, @"/", this.objBelMes.Emp.strEmployeeNumber);

            foreach (StationConfig.SerialNumberIP actSN in myStationConfig.GetSerialNumbersInProcess())
            {
                String PassBtnText = "PASS";
                Array actCTIs = ProductsConfig.GetChildTestsInfos(actSN.ProductID, actSN.TestType);
                if (actCTIs.Length > 0)
                    PassBtnText = "START";                 
                this.dgv_SNsIP.Rows.Add(actSN.SerialNumber, actSN.ProductID, actSN.TestType, actSN.Operator, actSN.StartTime, PassBtnText, "FAIL", "TERMINATE");
            }

            for (int i = 1; i < 5000; i++)
            {
                this.cb_FromSN.Items.Add(i);
                this.cb_ToSN.Items.Add(i);
            }

            if (myStationConfig.SimpleModeBatchAvailable())
            {
                this.BatchModeAvailable = true;
                this.label3.Enabled = true;
                this.label4.Enabled = true;
                this.label5.Enabled = true;
                this.tb_JobID.Enabled = true;                
            }

            this.chb_ChecklistImmediatelyStart.Checked = myStationConfig.GetChecklistImmediatelyStart();

            this.chb_JobIDChecking.Checked = this.myStationConfig.GetSimpleModeJobIDCheckingState();

            int HeightOfScreen = Screen.PrimaryScreen.Bounds.Height - 30;

            this.Height = HeightOfScreen;

            this.lbl_Info.ForeColor = System.Drawing.Color.Green;
            this.Activate();
        }



        private void tb_SerialNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            DialogResult myDR = DialogResult.OK;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                //Do your stuffs when network available

            }
            else
            {
                //Do stuffs when network not available
                myDR = MessageBox.Show("Nie je aktivne sietove pripojenie. Skontrolujte ho. \n\nV pripade nutnosti zavolajte nadriadeneho.", "CHYBA", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                while ((myDR == DialogResult.Retry) && (!NetworkInterface.GetIsNetworkAvailable()))
                {
                    myDR = MessageBox.Show("Nie je aktivne sietove pripojenie. Skontrolujte ho. \n\nV pripade nutnosti zavolajte nadriadeneho.", "CHYBA", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                if (myDR == DialogResult.Cancel)
                {
                    this.tb_SerialNumber.SelectAll();
                    this.tb_SerialNumber.Focus();
                    return;
                }
            }

            Ping pingSender = new Ping();
            String hostNameOrAddress = "dcasql14";
            PingReply reply = pingSender.Send(hostNameOrAddress);

            myDR = DialogResult.Retry;
            while ((reply.Status != IPStatus.Success) && (myDR == DialogResult.Retry))
            {
                myDR = MessageBox.Show("Nie je pristupny BelMes/dcasql14 server. \n\nKontaktujte nadriadeneho.", "CHYBA", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            if (reply.Status != IPStatus.Success)
            {
                this.tb_SerialNumber.SelectAll();
                this.tb_SerialNumber.Focus();
                return;
            }                


            if (this.cb_TestTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Najprv zvolte druh testu.");
                this.cb_TestTypes.Focus();
                return;
            }
            try
            {
                String strTestType = this.cb_TestTypes.Text.Trim();

                String strValidatedSerialNumber = this.tb_SerialNumber.Text;
                if (strValidatedSerialNumber[0].ToString() == "#")
                {
                    if (strValidatedSerialNumber[strValidatedSerialNumber.Length - 1].ToString() != ";")
                    {
                        String[] buffArr = strValidatedSerialNumber.Split(';');
                        if (buffArr.Length == 2)
                        {
                            MessageBox.Show(String.Concat("Nespravny format udajov v Barcode. Posledny znak musi byt bodkociarka - \";\". \nBarcode obsahuje udaj:\n\n", strValidatedSerialNumber), "POZOR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.tb_SerialNumber.SelectAll();
                            this.tb_SerialNumber.Focus();
                            return;
                        }
                    }
                    strValidatedSerialNumber = strValidatedSerialNumber.Substring(0, 14);
                }

                strValidatedSerialNumber = Regex.Match(tb_SerialNumber.Text.Trim(), @"\d+").Value;

                if (strValidatedSerialNumber.Length != 13)
                {
                    tb_SerialNumber.SelectAll();
                    this.lbl_Info.Text = String.Concat(strValidatedSerialNumber, " nie je platné SN.");
                    this.lbl_Info.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                String strProdID = this.objBelMes.GetProductID(strValidatedSerialNumber, strTestType);
                if (strProdID != "")
                {
                    if (!ProductsConfig.TestStationAllowed(strTestType, strProdID, this.objBelMes.Env.strTracePoint))
                    {
                        this.lbl_Info.Text = String.Concat("Tento typ vyrobku nie je mozne testovat na tejto stanici. Zavolajte prosim leadera.");
                        this.lbl_Info.ForeColor = System.Drawing.Color.Purple;
                        return;
                    }                        
                }

                StationConfig.SerialNumberIP SNIPtoSAVE = new StationConfig.SerialNumberIP(); //SerialNumber In Process
                SNIPtoSAVE.ProductID = "";
                if (this.myStationConfig.IsInProcessAndDelete(strValidatedSerialNumber, strTestType, true))
                {
                    this.objBelMes.LastSerialNumber = "";
                    foreach (DataGridViewRow actrow in this.dgv_SNsIP.Rows)
                    {
                        if (actrow.Cells[0].Value.ToString().Trim() != strValidatedSerialNumber)
                            continue;
                        
                        String actTestType = actrow.Cells[2].Value.ToString().Trim();
                        if (act_SNS != null)
                        {
                            Array.Resize(ref act_SNS.Steps, 0);
                        }

                        bool nextSN = false;
                        Array ar_actTestInstructions = this.ProductsConfig.GetChildTestsInfos(actrow.Cells[1].Value.ToString().Trim(), strTestType);
                        if (ar_actTestInstructions.Length > 0)
                        {
                            frm_InstructionsOneByOne myInstructionsForm = new frm_InstructionsOneByOne();
                            myInstructionsForm.myStationConfig = this.myStationConfig.StationConfigXML;
                            myInstructionsForm.actSN.SerialNumber = strValidatedSerialNumber;
                            myInstructionsForm.Instructions = ar_actTestInstructions;

                            myInstructionsForm.OperatorName = str_OperatorNr;
                            myInstructionsForm.Item = actrow.Cells[1].Value.ToString().Trim();
                            myInstructionsForm.StationName = this.StationName;

                            this.Visible = false;

                            myInstructionsForm.ShowDialog();

                            this.Visible = true;

                            if (myInstructionsForm.Response == "TERMINATED")
                            {
                                if (this.objBelMes.SetActualResult(strValidatedSerialNumber, strTestType, "TERMINATED", ""))
                                {
                                    this.lbl_Info.ForeColor = System.Drawing.Color.Violet;
                                }
                                else
                                {
                                    this.lbl_Info.ForeColor = System.Drawing.Color.Red;
                                }
                                this.lbl_Info.Text = this.objBelMes.Authorization.strResult;

                                this.dgv_SNsIP.Rows.RemoveAt(actrow.Index);

                                this.lbl_Info.Text = this.objBelMes.Authorization.strResult;

                                continue;
                            }
                            else
                            {
                                this.act_SNS = myInstructionsForm.actSN;
                            }
                            /*
                            foreach (DataGridViewRow actRow in myInstructionsForm.dgv_Instructions.Rows)
                            {
                                StepInfo actSnStep = new StepInfo();
                                actSnStep.Name = actRow.Cells[0].Value.ToString().Trim();
                                while (actRow.Cells[0].Value.ToString().Trim() == "")
                                {
                                    myInstructionsForm.ShowDialog();
                                    if (myInstructionsForm.Response == "TERMINATED")
                                    {
                                        if (this.objBelMes.SetActualResult(strValidatedSerialNumber, strTestType, "TERMINATED", ""))
                                        {
                                            this.lbl_Info.ForeColor = System.Drawing.Color.Violet;
                                        }
                                        else
                                        {
                                            this.lbl_Info.ForeColor = System.Drawing.Color.Red;
                                        }
                                        this.lbl_Info.Text = this.objBelMes.Authorization.strResult;

                                        break;
                                    }
                                }
                                if (myInstructionsForm.Response == "TERMINATED")
                                {
                                    nextSN = true;
                                    break;
                                }

                                if (actRow.DefaultCellStyle.BackColor == Color.Green)
                                {
                                    actSnStep.Grade = "PASS";
                                }
                                else
                                {
                                    actSnStep.Grade = "FAIL";
                                }
                                if (actRow.Cells[2].ToolTipText != "")
                                {
                                    actSnStep.ResultValue = actRow.Cells[2].ToolTipText;
                                }

                            }
                            */
                            myInstructionsForm.Dispose();
                            if (nextSN)
                                continue;
                        }  
                        this.GenerateReport(strValidatedSerialNumber, DateTime.Now);
                        dgv_SNsIP.Rows.Remove(actrow);
                        break;                        
                    }
                }
                else
                {
                    if (this.chb_JobIDChecking.Checked)
                    {
                        if (this.dgv_SNsIP.Rows.Count > 0)
                        {
                            String actJobID = strValidatedSerialNumber.Substring(0, 8);
                            String tableJobID = this.dgv_SNsIP.Rows[0].Cells[0].Value.ToString().Trim().Substring(0, 8);
                            if (actJobID != tableJobID)
                            {
                                this.lbl_Info.Text = String.Concat("Zoskenovane seriove cislo ", strValidatedSerialNumber, " nie je zo zakazky ", tableJobID, ".");
                                this.lbl_Info.ForeColor = Color.Red;
                                this.tb_SerialNumber.Text = "";
                                this.tb_SerialNumber.Focus();
                                return;
                            }
                        }
                    }

                    
                    this.objBelMes.BelMESAuthorization(strValidatedSerialNumber, strTestType, ref SNIPtoSAVE.ProductID, "", false);
                    Boolean hasInstructions = false;
                    if (this.objBelMes.Authorization.blnAuthorized)
                    {
                        SNIPtoSAVE.SerialNumber = strValidatedSerialNumber;
                        SNIPtoSAVE.TestType = strTestType;
                        SNIPtoSAVE.Operator = str_OperatorNr;
                        SNIPtoSAVE.StartTime = DateTime.Now.ToString();
                        this.myStationConfig.SaveSerialNumberIP(SNIPtoSAVE);

                        String PassBtnText = "PASS";
                        Array actCTIs = ProductsConfig.GetChildTestsInfos(SNIPtoSAVE.ProductID, SNIPtoSAVE.TestType);
                        if (actCTIs.Length > 0)
                        {
                            PassBtnText = "START";
                            hasInstructions = true;
                        }
                        this.dgv_SNsIP.Rows.Add(SNIPtoSAVE.SerialNumber, SNIPtoSAVE.ProductID, SNIPtoSAVE.TestType, SNIPtoSAVE.Operator, SNIPtoSAVE.StartTime, PassBtnText, "FAIL", "TERMINATE");
                        this.lbl_Info.ForeColor = Color.Green;
                    }
                    else
                    {
                        this.lbl_Info.ForeColor = Color.Red;
                        /*
                        if (this.objBelMes.Authorization.blnMustTraced)
                        {
                            this.lbl_Info.ForeColor = Color.Red;
                        }
                        else
                        {
                            SNIPtoSAVE.SerialNumber = strValidatedSerialNumber;
                            SNIPtoSAVE.TestType = strTestType;
                            SNIPtoSAVE.Operator = str_OperatorNr;
                            SNIPtoSAVE.StartTime = DateTime.Now.ToString();
                            this.myStationConfig.SaveSerialNumberIP(SNIPtoSAVE);
                            this.dgv_SNsIP.Rows.Add(SNIPtoSAVE.SerialNumber, SNIPtoSAVE.ProductID, SNIPtoSAVE.TestType, SNIPtoSAVE.Operator, SNIPtoSAVE.StartTime, "PASS", "FAIL", "TERMINATE");
                            this.lbl_Info.ForeColor = Color.FromArgb(255, 150, 0);
                        }
                        */
                    }
                    this.lbl_Info.Text = objBelMes.Authorization.strResult;
                    if (this.chb_ChecklistImmediatelyStart.Checked && hasInstructions)
                    {
                        if (this.lbl_Info.ForeColor == Color.Green)
                        {
                            this.tb_SerialNumber.Text = strValidatedSerialNumber;
                            this.tb_SerialNumber_KeyUp(new object(), new KeyEventArgs(Keys.Enter));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.lbl_Info.Text = ex.Message;
            }
            this.tb_SerialNumber.Clear();
            this.tb_SerialNumber.Focus();        
        }

        private void dgv_SNsIP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.cb_TestTypes.SelectedIndex == -1)
            {
                MessageBox.Show("Najprv zvolte druh testu.");
                this.cb_TestTypes.Focus();
                return;
            }
            if ((e.ColumnIndex == 5) && (e.RowIndex == -1) && (this.dgv_SNsIP.Rows.Count > 0))
            {                
                if (MessageBox.Show("Naozaj chcete ukoncit vsetky vyrobky zo zoznamu s vysledkom PASS?", "Hromadne ukoncenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    while (this.dgv_SNsIP.Rows.Count > 0)
                    {
                        String actSN = this.dgv_SNsIP.Rows[0].Cells[0].Value.ToString().Trim();
                        this.tb_SerialNumber.Text = actSN;
                        this.tb_SerialNumber_KeyUp(new object(), new KeyEventArgs(Keys.Enter));
                    }
                }
                return;
            }            
            try
            {
                if (e.RowIndex == -1) return;
                String actSN = this.dgv_SNsIP.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                String actTestType = this.dgv_SNsIP.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                if (e.ColumnIndex == 5)
                {
                    this.tb_SerialNumber.Text = actSN;
                    this.tb_SerialNumber_KeyUp(new object(), new KeyEventArgs(Keys.Enter));
                    return;
                }
                else if (e.ColumnIndex == 6)
                {
                    StepInfo[] arrFSI = { };
                    
                    InputBox_Form myIBF = new InputBox_Form("Failed test", "Zadajte nazov chybneho kroku");
                    if (myIBF.ShowDialog() == DialogResult.OK)
                    {
                        Array.Resize(ref arrFSI, 0);
                        while (myIBF.Answer != "")
                        {
                            StepInfo actFSI = new StepInfo();
                            actFSI.Name = myIBF.Answer;
                            actFSI.Grade = "FAIL";
                            Array.Resize(ref arrFSI, arrFSI.Length + 1);
                            arrFSI.SetValue(actFSI, arrFSI.Length - 1);
                            myIBF = new InputBox_Form("Failed test", "Zadajte nazov dalsieho chybneho kroku");
                            myIBF.ShowDialog();
                        }
                        if (arrFSI.Length == 0)
                        {
                            StepInfo actFSI = new StepInfo();
                            actFSI.Name = "Test1";
                            actFSI.Grade = "FAIL";
                            Array.Resize(ref arrFSI, arrFSI.Length + 1);
                            arrFSI.SetValue(actFSI, arrFSI.Length - 1);
                        }
                        
                        this.act_SNS = new SerialNumberSteps(actSN);
                        this.act_SNS.Steps = arrFSI;
                        this.GenerateReport(actSN, DateTime.Now);
                        this.myStationConfig.IsInProcessAndDelete(actSN, actTestType, true);
                        this.dgv_SNsIP.Rows.RemoveAt(e.RowIndex);
                        this.lbl_Info.ForeColor = System.Drawing.Color.Green;
                        this.lbl_Info.Text = this.objBelMes.Authorization.strResult;
                    }
                }
                else if (e.ColumnIndex == 7)               
                {                    
                    this.objBelMes.SetActualResult("Terminated", "", actSN);
                    this.myStationConfig.IsInProcessAndDelete(actSN, actTestType, true);
                    this.dgv_SNsIP.Rows.RemoveAt(e.RowIndex);
                    this.lbl_Info.ForeColor = System.Drawing.Color.FromArgb(255, 150, 0);
                    this.lbl_Info.Text = this.objBelMes.Authorization.strResult;
                }
                else
                {
                    return;
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.tb_SerialNumber.Clear();
                this.tb_SerialNumber.Focus();
            }
        }

        private Boolean GenerateReport(String SerialNumber, DateTime StartTime)
        {
            try
            {
                DateTime starttime = StartTime;
                DateTime endtime = StartTime.AddSeconds(5);

                UnitReport myReport = new UnitReport(starttime, endtime, "D", "", false);

                myReport.Operator.name = this.str_OperatorNr;

                myReport.Cathegory = new _Cathegory("Default");

                myReport.Cathegory.Product = new _Product("", SerialNumber); //mozno pridam stlpec s PartNumberom, ktory vrati autorizacia belmesu

                myReport.TestRun.name = this.cb_TestTypes.Text;

                myReport.starttime = starttime;
                myReport.endtime = endtime;

                myReport.AddProperty("Work Order", SerialNumber.Substring(0, 8));

                String URMode = myStationConfig.GetMode();
                if (URMode == "")
                {
                    URMode = "D";
                }
                else
                {
                    myReport.mode = URMode;
                }
                XmlNode assembliesNode = ProductsConfig.XmlFile.LastChild.SelectSingleNode("./Assemblies");
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

                XmlNode familyProperties = ProductsConfig.XmlFile.LastChild.SelectSingleNode("./Families");
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

                Array commonProperties = myStationConfig.GetProperties();
                for (int i = 0; i < commonProperties.GetLength(0); i++)
                {
                    String actNameNode = commonProperties.GetValue(i, 0).ToString().Trim().Replace('_', ' ');
                    myReport.AddProperty(actNameNode, commonProperties.GetValue(i, 1).ToString().Trim());
                }


                StationConfig.StationInfos actSI = myStationConfig.GetStationInfosForTest(this.cb_TestTypes.Text);

                foreach (StationConfig.StationInfos.StationProperty actProp in actSI.StationProperties)
                {
                    String actNameNode = actProp.Name.Replace('_', ' ');
                    myReport.AddProperty(actNameNode, actProp.Value);
                }

                String StationName = actSI.Name;
                String StationGUID = actSI.GUID;
                myReport.TestRun.grade = "PASS";

                myReport.Station = new _Station(StationGUID, StationName);
                if (this.act_SNS != null)
                {
                    if (myReport.Cathegory.Product.SerialNo == this.act_SNS.SerialNumber)
                    {
                       
                        foreach (StepInfo actFSI in this.act_SNS.Steps)
                        {
                            if (actFSI.Name.Trim() == "CustomerSerialNumber")
                            {
                                myReport.AddProperty("CustomerSN", actFSI.ResultValue);
                                myReport.TestRun.AddTestRunChild(actFSI.Name, myReport.starttime, myReport.endtime, actFSI.Grade, actFSI.ResultValue, "*$*", actFSI.ResultValue);
                            }
                            else
                            {
                                myReport.TestRun.AddTestRunChild(actFSI.Name, myReport.starttime, myReport.endtime, actFSI.Grade, actFSI.ResultValue, "", "*$*");
                            }
                            if (actFSI.Grade == "FAIL")
                            {
                                myReport.TestRun.grade = "FAIL";                                  
                            }
                        }
                    }
                    if (myReport.TestRun.grade == "FAIL")
                    {
                        myReport.AddProperty("To Repair", "Yes");
                    }

                }

                this.act_SNS = null;

                String strReportPath = this.myStationConfig.GetReportPathDirectory();
                Array XmlReportLines;
                String XmlReportContent = "";
                try
                {
                    XmlReportLines = myReport.GetXMLReport(strReportPath, true);
                    if (myReport.TestRun.grade == "")
                    {
                        MessageBox.Show("Problem pri vytvarani reportu. Zavolajte prosim technika.");
                        this.Close();
                    }
                        
                    foreach (String ActLine in XmlReportLines)
                    {
                        XmlReportContent = String.Concat(XmlReportContent, ActLine);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //

                if (this.objBelMes.Activated)
                {
                    try
                    {
                        String testKind = this.cb_TestTypes.Text;
                        if (testKind == "Adjustement")
                            testKind = "Adjustment";
                        if (myReport.TestRun.grade.ToUpper().Trim() == "PASS")
                            myReport.TestRun.grade = "Pass";
                        if (myReport.TestRun.grade.ToUpper().Trim() == "FAIL")
                            myReport.TestRun.grade = "Fail";

                        if (this.objBelMes.SetActualResult(myReport.Cathegory.Product.SerialNo, testKind, String.Concat(myReport.TestRun.grade, "ed"), XmlReportContent))
                        {
                            if (this.objBelMes.Authorization.intReturnCode == 0)
                            {
                                this.lbl_Info.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                if (this.objBelMes.Authorization.blnMustTraced)
                                {
                                    this.lbl_Info.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                    this.lbl_Info.ForeColor = System.Drawing.Color.FromArgb(255, 150, 0);
                            }                            
                            //this.Authorization.TryAuthorization(myReport.Cathegory.Product.SerialNo, testKind, String.Concat(myReport.TestRun.grade, "ED"), this.Env, false, false, XmlReportContent);
                        }
                        else
                        {
                            return false;
                        }
                        this.lbl_Info.Text = this.objBelMes.Authorization.strResult;
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void cb_TestTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.objBelMes.LastSerialNumber = "";
            this.tb_SerialNumber.Enabled = true;
            if (this.BatchModeAvailable)
            {
                this.tb_JobID.Enabled = true;
            }

            if (this.cb_TestTypes.SelectedIndex > -1)
            {
                this.lbl_Info.Text = "Oskenujte SerialNumber z produktu";
                this.tb_SerialNumber.Clear();
                this.tb_SerialNumber.Focus();
            }
        }

        private void tb_JobID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            String strBuffer = this.tb_JobID.Text.Trim();
            try
            {
                if (strBuffer.Length < 8)
                {
                    this.tb_JobID.SelectAll();
                    this.FormToStateSelection(false);
                    return;
                }
                if (strBuffer.IndexOf(';') > 0)
                {
                    strBuffer = strBuffer.Substring(0, strBuffer.IndexOf(';'));
                    strBuffer = Regex.Match(strBuffer.Trim(), @"\d+").Value;
                    if (strBuffer.Length > 7)
                    {
                        this.tb_JobID.Text = strBuffer.Substring(0, 8);
                    }
                }
                if (strBuffer.Length > 7)
                {
                    strBuffer = Regex.Match(strBuffer.Trim(), @"\d+").Value;
                    if (strBuffer.Length > 7)
                    {
                        this.tb_JobID.Text = strBuffer.Substring(0, 8);
                        this.FormToStateSelection(true);
                        if (this.cb_TestTypes.SelectedIndex < 0)
                        {
                            this.cb_TestTypes.Focus();
                        }
                        return;
                    }
                }
                this.FormToStateSelection(false);
            }
            catch
            {

            }
        }

        private void tb_JobID_Leave(object sender, EventArgs e)
        {
            String strBuffer = this.tb_JobID.Text.Trim();            
            if (strBuffer.IndexOf(';') > 0)
            {
                strBuffer = strBuffer.Substring(0, strBuffer.IndexOf(';'));          
            }
            strBuffer = Regex.Match(strBuffer.Trim(), @"\d+").Value;
            if (strBuffer.Length > 8)
            {
                this.tb_JobID.Text = strBuffer.Substring(0, 8);
            }
            if (strBuffer.Length != 8)
            {
                if (strBuffer.Length == 0)
                {
                    return;
                }
                this.tb_JobID.SelectAll();
                this.FormToStateSelection(false);
                this.tb_JobID.Focus();
                return;
            }
            else
            {
                this.FormToStateSelection(true);
            }

        }

        private void FormToStateSelection(Boolean actState)
        {
            this.label4.Enabled = actState;
            this.label3.Enabled = actState;
            this.cb_FromSN.Enabled = actState;
            this.cb_ToSN.Enabled = actState;
        }

        private void btn_Authorize_Click(object sender, EventArgs e)
        {
            if (this.tb_JobID.Text == "") return;
            if (this.cb_TestTypes.SelectedIndex < 0) return;
            if (this.cb_FromSN.SelectedIndex < 0) return;
            if (this.cb_ToSN.SelectedIndex < 0) return;
            int intFromSN = 0;
            int intToSN = 0;
            try
            {
                intFromSN = Convert.ToInt32(this.cb_FromSN.Text);
                intToSN = Convert.ToInt32(this.cb_ToSN.Text);
            }
            catch
            {

            }

            if (!(intFromSN < intToSN)) return;

            String strJobID = this.tb_JobID.Text.Trim();
            if (strJobID.Length != 8) return;
            for (int i = intFromSN; i < intToSN+1; i++)
            {
                String actSN = i.ToString();
                while (actSN.Length < 5)
                {
                    actSN = String.Concat("0", actSN);
                }
                actSN = String.Concat(strJobID, actSN);

                this.tb_SerialNumber.Text = actSN;                    
                this.tb_SerialNumber_KeyUp(new object(), new KeyEventArgs(Keys.Enter));

            }

        }

        private void cb_FromSN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_FromSN.SelectedIndex < 0) return;
            if (this.cb_ToSN.SelectedIndex < 0) return;
            int n_FromSN = Convert.ToInt32(this.cb_FromSN.Text.Trim());
            int n_ToSN = Convert.ToInt32(this.cb_ToSN.Text.Trim());
            if (n_FromSN < n_ToSN)
            {
                this.btn_Authorize.Enabled = true;
            }
            else
            {
                this.btn_Authorize.Enabled = false;
            }
        }

        private void cb_ToSN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_FromSN.SelectedIndex < 0) return;
            if (this.cb_ToSN.SelectedIndex < 0) return;
            int n_FromSN = Convert.ToInt32(this.cb_FromSN.Text.Trim());
            int n_ToSN = Convert.ToInt32(this.cb_ToSN.Text.Trim());
            if (n_FromSN < n_ToSN)
            {
                this.btn_Authorize.Enabled = true;
            }
            else
            {
                this.btn_Authorize.Enabled = false;
            }
        }

        private void dgv_SNsIP_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.groupBox1.Text = String.Concat("Seriove cisla v procese (", this.dgv_SNsIP.Rows.Count.ToString(), ")");
        }

        private void dgv_SNsIP_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.groupBox1.Text = String.Concat("Seriove cisla v procese (", this.dgv_SNsIP.Rows.Count.ToString(), ")");            
        }

        private void tb_JobID_DoubleClick(object sender, EventArgs e)
        {
            this.dgv_SNsIP.Rows.Add((this.dgv_SNsIP.Rows.Count+1).ToString(), "", "", "", "", "PASS", "FAIL", "TERMINATE");
        }

        private void chb_JobIDChecking_CheckedChanged(object sender, EventArgs e)
        {
            this.myStationConfig.SetSimpleModeJobIDCheckingState(this.chb_JobIDChecking.Checked);
        }

        private void btn_SupportRequest_Click(object sender, EventArgs e)
        {
            String[] ar_SupportRequestCodes = { "SR_Leader", "SR_Material", "SR_MES", "SR_NPI", "SR_Quality", "SR_Sustaining", "SR_Technology", "SR_Testing" };
            InputBox_Form myIBF = new InputBox_Form("Support Request", "Vyberte druh podpory, napiste poziadavku a odoslite klikom na OK");
            myIBF.ShowDialog();
            if (myIBF.SelectedItem != "")
            {                
                String strWarning = String.Empty;
                if (this.objBelMes.SendSupportRequest(myIBF.SelectedItem.Trim(), myIBF.Answer, ref strWarning))
                {
                    this.lbl_Info.Text = "SupportRequest uspesne poslany.";
                    this.lbl_Info.ForeColor = Color.Green;
                }
                else
                {
                    this.lbl_Info.Text = "SupportRequest  poslany.";
                    this.lbl_Info.ForeColor = Color.Red;
                }
            }
        }

        private void chb_ChecklistImmediatelyStart_CheckedChanged(object sender, EventArgs e)
        {
            myStationConfig.SetChecklistImmediatelyStart(this.chb_ChecklistImmediatelyStart.Checked);
            this.tb_SerialNumber.Focus();
            this.tb_SerialNumber.SelectAll();
        }
    }
}

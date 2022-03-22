using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using BelMESCommon;

namespace SigmaSureManualReportGenerator
{
    public class BelMES
    {
        public clEnvironment Env = new clEnvironment();
        public clEmployee Emp = new clEmployee();
        public clAuthorization Authorization = new clAuthorization();
        public clSupportRequest SupportRequest = new clSupportRequest();
        private readonly String LogFilePath = @"\\dcafs3\share\Manufacturing_Engineering\Public\Kolman Vladimir\BelMESCommon\SSMRG_BELLogs\";
        private readonly String LogFileName = "";
        public Boolean Activated = false;
        public Boolean WarningMessages = false;
        public Boolean ProductVerifiedForMessages = false;
        public String Mode = "P";
        private readonly String ConfigDirectory;
        private String actualFixtureID = "";

        //private String OperatorNumber;
        //private String StationName;

        public String LastSerialNumber;        

        private readonly String ActualProgramVersion = "";

        public BelMES(String StationName, String ConfigDirectory, String ActualProgramVersion)
        {
            
            if (StationName == "") return;
            try
            {
                this.ActualProgramVersion = ActualProgramVersion;
                this.Env = new clEnvironment();
                this.Env = this.Env.SetEnvironment();
                //this.Env = this.Env.SetEnvironment("finalinspectionhev01");
                if (this.Env != null)
                {
                    if ((this.Env.strComputer == "") || (this.Env.strComputer == null))
                        this.LogFileName = String.Concat(StationName, "_", String.Format("{0:yyyMMdd}", DateTime.Now), ".txt");
                    else
                        this.LogFileName = String.Concat(this.Env.strComputer, "_", String.Format("{0:yyyyMMdd}", DateTime.Now), ".txt");
                    if (this.Env.blnAuthorizationPaused != false) this.Env.blnAuthorizationPaused = false;
                    this.Activated = true;
                    this.Env.Employee = new clEmployee();
                }
                else
                {
                    this.LogFileName = String.Concat(StationName, "_", String.Format("{0:yyyyMMdd}", DateTime.Now), ".txt");
                    
                }            

                if (!File.Exists(String.Concat(this.LogFilePath, this.LogFileName)))
                {
                    File.Create(String.Concat(this.LogFilePath, this.LogFileName)).Close();

                    try
                    {
                        String newLine = String.Concat("SWVERSION;DATETIME;TRACEPOINT;PCNAME;EMPLOYEENUMBER;SERIALNUMBER;strWO_SerialNumber;strItem;blnAuthorized;blnMustTraced;blnTraceItemStart;strTestKind;strStatus;strResult;intReturnCode;strSQLDatabase;strSQLServer;blnAuthorizationPaused;Activated;ExtendedInfo");

                        StreamWriter sr = new StreamWriter(String.Concat(this.LogFilePath, this.LogFileName), true);
                        sr.WriteLine(newLine);
                        sr.Close();
                        sr.Dispose();
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().IndexOf("is no longer available") > -1)
                        {
                            MessageBox.Show(String.Concat("Nie je dostupna siet.", "\n", ex.Data.ToString()), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show(String.Concat(ex.Message, "\n", ex.Data.ToString()));
                        }
                    }

                    
                }

                this.ConfigDirectory = ConfigDirectory;

                this.WriteLogData("");
            }
            catch
            {
                this.WriteLogData("");
                this.Activated = false;
            }
        }

        public Boolean BelMESAuthorization(String SerialNumber, String TestType, ref String ProductName, String XmlContent)
        {
            return this.BelMESAuthorization(SerialNumber, TestType, ref ProductName, XmlContent, false);
        }

        public Boolean BelMESAuthorization(String SerialNumber, String TestType, ref String ProductName, String XmlContent, Boolean ForceTerminated)
        {
            return this.BelMESAuthorization(SerialNumber, TestType, ref ProductName, XmlContent, ForceTerminated, true, "");
        }

        public Boolean BelMESAuthorization(String SerialNumber, String TestType, ref String ProductName, String XmlContent, Boolean ForceTerminated, Boolean StartNew, String warningInfoMessage)
        {
            if (SerialNumber.Length == 8)
            {
                this.WriteLogData(String.Concat("sn ma len 8 znakov - ", SerialNumber, " - ", warningInfoMessage));
                return false;
            }

            if (SerialNumber.Length == 17)
            {
                SerialNumber = SerialNumber.Substring(8);
            }
            if (SerialNumber.Length == 19)
            {
                SerialNumber = SerialNumber.Substring(10);
            }

            if ((this.LastSerialNumber == SerialNumber) && (this.Authorization.blnAuthorized) && (this.Authorization.blnTraceItemStart))
                return true;

            this.LastSerialNumber = SerialNumber;

            if (TestType == "Adjustement") TestType = "Adjustment";
            if (TestType == "FAI")
            {
                TestType = "OBA";
                this.actualFixtureID = "FAI";
            }
            
            if (this.Env.Employee.strEmployeeNumber == null)
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);
            }
            if (this.Env.Employee.strEmployeeNumber == "nullEmpNumber")
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);
            }
      

            if ((this.Authorization.strWO_SerialNumber != null) && ForceTerminated)
            {
                this.Authorization = this.Authorization.TryAuthorization(this.Authorization.strWO_SerialNumber, this.Authorization.strTestKind, "Terminated", this.Env, true, false, this.Mode, XmlContent, this.actualFixtureID);
                if (!StartNew)
                {
                    this.WriteLogData(String.Concat(SerialNumber, " - ", warningInfoMessage));
                    return true;
                }
            }
            if (TestType != "")
            {
                this.Authorization = this.Authorization.TryAuthorization(SerialNumber, TestType, "", this.Env, false, true, this.Mode, "", this.actualFixtureID);
                this.Authorization.strTestKind = TestType;
            }
            if (this.Authorization.intReturnCode < 0)
            {
                MessageBox.Show(String.Concat(this.Authorization.strResult," \rChyba v spojeni s SQL serverom. Zavolajte prosim svojho nadriadeneho"), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.WriteLogData(String.Concat(this.Authorization.intReturnCode.ToString(), " - ", warningInfoMessage));
                return false;
            }
            this.WriteLogData(String.Concat(SerialNumber, " - ", warningInfoMessage));
            Boolean b_Verified = true;
            if (this.Authorization.blnAuthorized)
            {
                ProductName = this.Authorization.strItem;      
            }
            else
            {
                b_Verified = false;                
                if (this.Authorization.blnMustTraced)
                { 
                    MessageBox.Show(this.Authorization.strResult, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }   
                else
                {
                    ProductName = this.Authorization.strItem;
                    b_Verified = true;
                }
                
            }            
            return b_Verified;
        }

        public Boolean SetActualResult(String Result, String XmlReportString, String SerialNumber)
        {
            Boolean retVal = false;

            if (SerialNumber.Length == 17) SerialNumber = SerialNumber.Substring(8);
            if (SerialNumber.Length == 19) SerialNumber = SerialNumber.Substring(10);

            if ((this.Env.Employee.strEmployeeNumber == null) || (this.Env.Employee.strEmployeeNumber == "nullEmpNumber"))
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);
            }

            try
            {

                if (this.Authorization.strWO_SerialNumber != null)
                {
                    if (this.Authorization.strWO_SerialNumber != "")
                    {
                        this.Authorization = this.Authorization.TryAuthorization(this.Authorization.strWO_SerialNumber, this.Authorization.strTestKind, Result, this.Env, false, true, this.Mode, XmlReportString, "");
                    }
                    else
                    {
                        this.Authorization = this.Authorization.TryAuthorization(SerialNumber, this.Authorization.strTestKind, Result, this.Env, false, true, this.Mode, XmlReportString, "");
                    }
                }
                else
                {
                    this.Authorization = this.Authorization.TryAuthorization(SerialNumber, this.Authorization.strTestKind, Result, this.Env, false, true, this.Mode, XmlReportString, "");
                }

                /*
                if ((this.Authorization.strWO_SerialNumber != null))
                {
                    this.Authorization = this.Authorization.TryAuthorization(this.Authorization.strWO_SerialNumber, this.Authorization.strTestKind, Result, this.Env, true, false, XmlReportString);
                }
                else
                {
                    retVal = false;
                }
                */
            }
            catch
            {
                this.WriteLogData("");
                return false;
            }
            this.WriteLogData("");
            return retVal;
        }

        public Boolean SetActualResult(String SerialNumber, String TestKind, String Result, String XmlReportString)
        {
            Boolean retVal = true;

            if (SerialNumber.Length == 17) SerialNumber = SerialNumber.Substring(8);
            if (SerialNumber.Length == 19) SerialNumber = SerialNumber.Substring(10);

            if ((this.Env.Employee.strEmployeeNumber == null) || (this.Env.Employee.strEmployeeNumber == "nullEmpNumber"))
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);                
            }            

            if (TestKind == "Adjustement") TestKind = "Adjustment";
            if (TestKind == "FAI")
            {
                TestKind = "OBA";
                this.actualFixtureID = "FAI";
            }

            try
            {
                this.Authorization = this.Authorization.TryAuthorization(SerialNumber, TestKind, Result, this.Env, false, true, this.Mode, XmlReportString, this.actualFixtureID);

                if (this.Authorization.intReturnCode < 0)
                {
                    MessageBox.Show(String.Concat(this.Authorization.strResult, " \rChyba v spojeni s SQL serverom. Zavolajte prosim svojho nadriadeneho"), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.WriteLogData(this.Authorization.intReturnCode.ToString());
                    return false;
                }

                if ((!this.Authorization.blnAuthorized) && (TestKind == "OBA"))
                {
                    MessageBox.Show(String.Concat("Nepodaril sa vytvorit zaznam z OBA testu pre SN ", SerialNumber, " do Belmesu. Kontaktujte prosím testovacieho technika."), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.WriteLogData("OBA Fail");
                    return false;
                }
                /*
                if ((this.Authorization.strWO_SerialNumber != null))
                {
                    this.Authorization = this.Authorization.TryAuthorization(this.Authorization.strWO_SerialNumber, this.Authorization.strTestKind, Result, this.Env, true, false, XmlReportString);
                }
                else
                {
                    retVal = false;
                }
                */
            }
            catch
            {
                this.WriteLogData("-00000001");
                return false;
            }
            this.WriteLogData("0");
            return retVal;
        }

        public String GetProductID(String SerialNumber, String TestKind)
        {
            if ((this.Env.Employee.strEmployeeNumber == null) || (this.Env.Employee.strEmployeeNumber == "nullEmpNumber"))
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);
            }

            if (TestKind == "Adjustement")
                TestKind = "Adjustment";
            if (TestKind == "FAI")
            {
                TestKind = "OBA";
                this.actualFixtureID = "FAI";
            }

            try
            {
                this.Authorization = this.Authorization.TryAuthorization(SerialNumber, TestKind, "", this.Env, false, false, this.Mode, "", "");
                return this.Authorization.strItem;
            }
            catch
            {
                return "";
            }
        }

        public Boolean EmployeeVerification(String EmployeeNumber)
        {
            //this.OperatorNumber = EmployeeNumber;
            try
            {
                this.Emp = this.Emp.EmployeeVerify(EmployeeNumber, this.Env.DB_Resource, this.Env.ESD_DB_Resource);
                if (this.Emp == null)
                {
                    this.Activated = false;
                    if (this.WarningMessages)
                    {
                        MessageBox.Show(String.Concat(this.Emp.strEmployeeCodeInfo,". Zavolajte prosim testovacieho technika."), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.WriteLogData(EmployeeNumber);
                    return false;
                }
                else
                {
                    if (this.Emp.shtEmployeeID > 0 && string.IsNullOrEmpty(this.Emp.strEmployeeCodeInfo))
                    {
                        this.Env.Employee = new clEmployee(this.Emp.shtEmployeeID, this.Emp.strEmployeeName, EmployeeNumber, this.Emp.strEmployeeDepartment, this.Emp.strEmployeeProduction, this.Emp.strEmployeeCodeInfo);
                        this.WriteLogData(EmployeeNumber);
                    }
                    else
                    {
                        this.Activated = false;
                        MessageBox.Show(String.Concat(this.Emp.strEmployeeCodeInfo, ". Zavolajte prosim testovacieho technika."), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.WriteLogData(EmployeeNumber);
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                this.WriteLogData(EmployeeNumber);                
                this.Activated = false;
                return false;
            }
        }

        public Boolean SendSupportRequest(String SRCode, String SRMessage, ref String WarningMessage)
        {
            this.SupportRequest = new clSupportRequest();
            String strWarningMessage = String.Empty;
            if (!this.SupportRequest.SendSupportRequest(SRCode, SRMessage, this.Env, ref WarningMessage))
            {
                MessageBox.Show(new Form { TopMost = true }, String.Concat("Support Request nebol odoslany.\n", WarningMessage), "Support Request Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                this.WriteLogData(String.Concat("SupportRequestError:", WarningMessage));
                return false;
            }
            this.WriteLogData(String.Concat("SupportRequest:", WarningMessage));
            return true;
        }
        

        private void WriteLogData(String ExtendedInfo)
        {
            try
            {
                
                String PCName = Environment.MachineName;

                String newLine = String.Concat(
                    this.ActualProgramVersion, ";",
                    DateTime.Now.Hour.ToString("D2"), ":", DateTime.Now.Minute.ToString("D2"), ":", DateTime.Now.Second.ToString("D2"), ";",
                    (this.Env.strTracePoint == null) ? "nullTracePoint" : this.Env.strTracePoint.ToString(), ";",
                    PCName, ";",
                    (this.Env.Employee.strEmployeeNumber == null) ? "nullEmpNumber" : this.Env.Employee.strEmployeeNumber.ToString(), ";",
                    (this.Authorization.strSerialNumber == null) ? "nullSN1" : this.Authorization.strSerialNumber.ToString(), ";",
                    (this.Authorization.strWO_SerialNumber == null) ? "nullSN2" : this.Authorization.strWO_SerialNumber.ToString(), ";",
                    this.Authorization.strItem, ";",
                    this.Authorization.blnAuthorized.ToString(), ";",
                    this.Authorization.blnMustTraced.ToString(), ";",
                    this.Authorization.blnTraceItemStart.ToString(), ";",
                    (this.Authorization.strTestKind == null) ? "nullTestKind" : this.Authorization.strTestKind.ToString(), ";",
                    (this.Authorization.strStatus == null) ? "nullStatus" : this.Authorization.strStatus.ToString(), ";",
                    (this.Authorization.strResult == null) ? "nullResult" : this.Authorization.strResult.ToString(), ";",
                    (this.actualFixtureID == null) ? "nullFixtureID" : this.actualFixtureID.ToString(), ";",
                    this.Authorization.intReturnCode.ToString(), ";",
                    this.Env.DB_Resource.strSQLDatabase ?? "nullDatabase", ";",
                    this.Env.DB_Resource.strSQLServer ?? "nullServer", ";",                    
                    this.Env.blnAuthorizationPaused, ";", this.Activated, ";", ExtendedInfo) ;

                StreamWriter sr;

                if (!Directory.Exists(this.LogFilePath))
                {
                    sr = new StreamWriter(String.Concat(this.ConfigDirectory, this.LogFileName), true);
                }
                else
                {
                    sr = new StreamWriter(string.Concat(this.LogFilePath, this.LogFileName), true);
                }
                sr.WriteLine(newLine);
                this.actualFixtureID = null;
                sr.Close();
                sr.Dispose();

            }
            catch (Exception ex)
            {
                if (ex.ToString().IndexOf("is no longer available") > -1)
                {
                    MessageBox.Show(String.Concat("Nie je dostupna siet.", "\n", ex.Data.ToString()), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(String.Concat(ex.Message, "\n", ex.Data.ToString()));
                }
            }
        }
    }
}

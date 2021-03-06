﻿using System;
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
        private String LogFilePath = @"\\dcafs3\share\Manufacturing_Engineering\Public\Kolman Vladimir\BelMESCommon\SSMRG_BELLogs\";
        private String LogFileName = "";
        public Boolean Activated = false;
        public Boolean WarningMessages = false;
        public Boolean ProductVerifiedForMessages = false;
        public String Mode = "P";
        private String ConfigDirectory;

        private String OperatorNumber;
        private String StationName;

        private String LastSerialNumber;

        public BelMES(String StationName, String ConfigDirectory)
        {
            
            if (StationName == "") return;
            try
            {
                //String userName = "inspection07";
                //this.Env = this.Env.SetEnvironment(userName);
                this.Env = this.Env.SetEnvironment();
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

        public Boolean BelMESAuthorization(String SerialNumber, String TestType, String ProductName, String XmlContent)
        {
            if (TestType == "Adjustement") TestType = "Adjustment";
            return this.BelMESAuthorization(SerialNumber, TestType, ProductName, XmlContent, false);
        }

        public Boolean BelMESAuthorization(String SerialNumber, String TestType, String ProductName, String XmlContent, Boolean ForceTerminated)
        {
            if (SerialNumber.Length == 17)
            {
                SerialNumber = SerialNumber.Substring(8);
            }
            if (SerialNumber.Length == 19)
            {
                SerialNumber = SerialNumber.Substring(10);
            }

            if ((this.LastSerialNumber == SerialNumber) && (this.Authorization.blnAuthorized))
                return true;

            this.LastSerialNumber = SerialNumber;

            if (TestType == "Adjustement") TestType = "Adjustment";

            if ((this.Env.Employee.strEmployeeNumber == null) || (this.Env.Employee.strEmployeeNumber == "nullEmpNumber"))
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);
            }            

            if ((this.Authorization.strWO_SerialNumber != null) && ForceTerminated)
            {
                this.Authorization = this.Authorization.TryAuthorization(this.Authorization.strWO_SerialNumber, this.Authorization.strTestKind, "Terminated", this.Env, true, false, this.Mode, XmlContent, "");
                //if ()
            }
            if (TestType != "")
            {
                this.Authorization = this.Authorization.TryAuthorization(SerialNumber, TestType, "", this.Env, false, true, this.Mode, "", "");
                this.Authorization.strTestKind = TestType;
            }
            this.WriteLogData(SerialNumber);
            Boolean b_Verified = true;
            if (this.Authorization.blnAuthorized)
            {
                if (this.Authorization.strItem != ProductName)
                    b_Verified = false;
            }
            else
            {
                b_Verified = false;
                if (this.WarningMessages || this.ProductVerifiedForMessages)
                {
                    MessageBox.Show(this.Authorization.strResult, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Boolean retVal = false;

            if (SerialNumber.Length == 17) SerialNumber = SerialNumber.Substring(8);
            if (SerialNumber.Length == 19) SerialNumber = SerialNumber.Substring(10);

            if ((this.Env.Employee.strEmployeeNumber == null) || (this.Env.Employee.strEmployeeNumber == "nullEmpNumber"))
            {
                this.EmployeeVerification(this.Emp.strEmployeeNumber);                
            }

            if (TestKind == "Adjustement") TestKind = "Adjustment";

            try
            {
                this.Authorization = this.Authorization.TryAuthorization(SerialNumber, TestKind, Result, this.Env, false, true, this.Mode, XmlReportString, "");

                if ((!this.Authorization.blnAuthorized) && (TestKind == "OBA"))
                {
                    MessageBox.Show(String.Concat("Nepodaril sa vytvorit zaznam z OBA testu pre SN ", SerialNumber, " do Belmesu. Kontaktujte prosím testovacieho technika."), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public Boolean EmployeeVerification(String EmployeeNumber)
        {
            this.OperatorNumber = EmployeeNumber;
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
                        if (this.WarningMessages)
                        {
                            MessageBox.Show(String.Concat(this.Emp.strEmployeeCodeInfo, ". Zavolajte prosim testovacieho technika."), "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }                        
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

        private void WriteLogData(String ExtendedInfo)
        {
            try
            {
                
                String PCName = Environment.MachineName;
                
                String newLine = String.Concat(
                    DateTime.Now.Hour.ToString("D2"), ":", DateTime.Now.Minute.ToString("D2"), ":", DateTime.Now.Second.ToString("D2"), ";",
                    (this.Env.strTracePoint == null) ? "nullTracePoint" : this.Env.strTracePoint.ToString(), ";",
                    PCName, ";",
                    (this.Env.Employee.strEmployeeNumber == null) ? "nullEmpNumber" : this.Env.Employee.strEmployeeNumber.ToString(), ";",
                    (this.Authorization.strSerialNumber == null) ? "nullSN1" : this.Authorization.strSerialNumber.ToString(), ";",
                    (this.Authorization.strWO_SerialNumber == null) ? "nullSN2" : this.Authorization.strWO_SerialNumber.ToString(), ";",
                    this.Authorization.blnMustTraced, ";", 
                    this.Authorization.blnTraceItemStart.ToString(), ";", 
                    (this.Authorization.strTestKind == null) ? "nullTestKind" : this.Authorization.strTestKind.ToString(), ";", 
                    (this.Authorization.strStatus == null) ? "nullStatus" : this.Authorization.strStatus.ToString(), ";", 
                    (this.Authorization.strResult == null) ? "nullResult" : this.Authorization.strResult.ToString(), ";",
                    (this.Env.DB_Resource.strSQLDatabase == null) ? "nullDatabase" : this.Env.DB_Resource.strSQLDatabase, ";", 
                    (this.Env.DB_Resource.strSQLServer == null) ? "nullServer" : this.Env.DB_Resource.strSQLServer, ";",
                    this.Env.blnAuthorizationPaused, ";", this.Activated, ";", ExtendedInfo);

                if (!Directory.Exists(this.LogFilePath))
                {
                    StreamWriter sr1 = new StreamWriter(String.Concat(this.ConfigDirectory, this.LogFileName), true);
                    sr1.WriteLine(newLine);
                    sr1.Close();
                    sr1.Dispose();
                }
                else
                {
                    StreamWriter sr = new StreamWriter(String.Concat(this.LogFilePath, this.LogFileName), true);
                    sr.WriteLine(newLine);
                    sr.Close();
                    sr.Dispose();
                }                
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

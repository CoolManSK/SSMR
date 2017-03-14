using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BelMESCommon;

namespace SigmaSureManualReportGenerator.BelMES
{
    class BelMESfunctions
    {
        public clEnvironment env;
        public clEmployee emp;
        public clAuthorization auth;

        private bool SetEnvironment()
        {
            bool blnEmployeeMandatory = false;
            string strUser = "scanstation08";//Environment.UserName; //"scanstation08"; //
            clEnvironment MESE = new clEnvironment();

            this.env = MESE.SetEnvironment(strUser);
            blnEmployeeMandatory = AuthorizationEnabled();
            return blnEmployeeMandatory;
        }

        //checked environment properties
        private bool AuthorizationEnabled()
        {
            bool blnEnabled = false;
            if (this.env == null)
            {
                //MessageBox.Show("env == null");
                blnEnabled = false;
            }
            else
            {
                if (this.env.shtUser > 0 && this.env.shtTracePoint > 0 && this.env.blnAutoTestTracePoint == true)
                {
                    blnEnabled = true;
                    //MessageBox.Show(string.Format("user = {0} TP = {1} Auto = {2}", env.shtUser ,env.shtTracePoint,env.blnAutoTestTracePoint) );
                }

            }
            //MessageBox.Show(string.Format("return = {0}", blnEnabled ));
            return blnEnabled;
        }

        private bool EmployeeVerify(string strEmpNum, ref string strEmpCodeInfo)
        {
            bool blnValidEmployee = true;
            clEmployee employee = new clEmployee();
            this.emp = employee.EmployeeVerify(strEmpNum, env.DB_Resource);
            if (emp == null)
            {
                strEmpCodeInfo = "Employee Number does not exist";
            }
            else
            {
                if (emp.shtEmployeeID > 0 && string.IsNullOrEmpty(emp.strEmployeeCodeInfo))
                {
                    env.Employee = new clEmployee(emp.shtEmployeeID, emp.strEmployeeName, strEmpNum, emp.strEmployeeDepartment, emp.strEmployeeProduction, emp.strEmployeeCodeInfo);
                    blnValidEmployee = false;
                }
                strEmpCodeInfo = emp.strEmployeeCodeInfo;
            }
            return blnValidEmployee;
        }

        private void Authorization(string SerialNumber, string TestKind, string Status, bool English)
        {
            clAuthorization authorization = new clAuthorization();
            auth = authorization.TryAuthorization(SerialNumber, TestKind, Status, env, English);
            if (auth == null)
                auth = new clAuthorization();
        }        

        //checked environment properties before authorization
        private bool CheckAuth()
        {
            if (env == null || env.shtUser <= 0)
                return false;
            if (env != null && env.shtUser > 0 && env.shtTracePoint > 0 && env.blnAutoTestTracePoint == true && env.Employee == null)
                return false;
            if (env.Employee != null && env.Employee.shtEmployeeID <= 0)
                return false;
            return true;
        }
    }
}

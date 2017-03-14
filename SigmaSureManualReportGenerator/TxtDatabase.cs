using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace SigmaSureManualReportGenerator
{
    class TxtDatabase
    {
        public Boolean CheckSerialNumberAndTestType(String JobID, String SerialNumber, String TestType)
        {
            String FileName = String.Concat(JobID, ".txt");
            String FilePath = @"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\SigmaSureReportLogs\";
            if (!this.PathExists(FilePath)) return false;
            if (File.Exists(String.Concat(FilePath, FileName)))
            {
                Boolean isLocked = true;
                Int32 counter = 0;
                while (isLocked)
                {
                    if (counter > 50)
                    {
                        MessageBox.Show(String.Concat("Súbor \"", String.Concat(FilePath, FileName), "\" sa neda otvorit na zapisovanie.\n\n Zavolajte prosim testovacieho technika."), "CHYBA");
                        return true;
                    }
                    try
                    {
                        StreamReader sr = new StreamReader(String.Concat(FilePath, FileName));
                        String line = "";
                        while (((line = sr.ReadLine()) != null))
                        {
                            String actSN = line.Substring(0, line.IndexOf(';'));
                            if (actSN != SerialNumber) continue;

                            line = line.Substring(line.IndexOf(';') + 1);
                            String actTT = line;
                            if (actTT != TestType) continue;

                            sr.Close();
                            return true;
                        }
                        sr.Close();
                        isLocked = false;
                    }
                    catch
                    {
                        Thread.Sleep(100);
                        counter++;
                    }
                }
                try
                {
                    StreamReader sr = new StreamReader(String.Concat(FilePath, FileName));
                    String line = "";
                    while (((line = sr.ReadLine()) != null))
                    {
                        if (line.Trim() == "") continue;

                        String actSN = line.Substring(0, line.IndexOf(';'));
                        if (actSN != SerialNumber) continue;

                        line = line.Substring(line.IndexOf(';') + 1);
                        String actTT = line;
                        if (actTT != TestType) continue;

                        sr.Close();
                        return true;
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Concat(ex.Message, "\n\n Zavolajte prosim testovacieho technika."), "CHYBA");
                    return true;
                }
            }
            return false;
        }
        public void SaveSerialNumberAndTestTypeToLogFile(String JobID, String SerialNumber, String TestType)
        {
            String FileName = String.Concat(JobID, ".txt");
            String FilePath = @"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\SigmaSureReportLogs\";
            if (!this.PathExists(FilePath)) return;
            if (!File.Exists(String.Concat(FilePath, FileName)))
            {
                try
                {
                    FileStream fs = File.Create(String.Concat(FilePath, FileName));
                    fs.Close();
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(String.Concat(ex1.Message, "\n\n Zavolajte prosim testovacieho technika."), "CHYBA");
                }
            }
            Boolean isLocked = true;
            Int32 counter = 0;
            Exception ex = new Exception();
            while (isLocked)
            {

                if (counter > 50)
                {
                    //MessageBox.Show(String.Concat(ex.Message, "\n\n", ex.InnerException.Message));
                    MessageBox.Show(String.Concat(ex.Message, "\n\nSubor \"", String.Concat(FilePath,FileName), "\" sa neda otvorit na zapisovanie.\n\n Zavolajte prosim testovacieho technika."), "CHYBA");
                    return;
                }
                try
                {
                    StreamWriter sr = new StreamWriter(String.Concat(FilePath, FileName), true);
                    String line = String.Concat(SerialNumber, ";", TestType);
                    sr.WriteLine(line);
                    sr.Close();
                    isLocked = false;
                }
                catch (Exception ex1)
                {
                    ex = ex1;
                    Thread.Sleep(100);
                    counter++;
                }
            }
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
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

namespace SigmaSureManualReportGenerator
{
    public partial class frm_InstructionsOneByOne : Form
    {
        public SimpleMode_Form.SerialNumberSteps actSN;
        public String Response = "";
        public Array Instructions;

        public String OperatorName = "";
        public String StationName = "";
        public String Item = "";

        public XmlDocument myStationConfig;

        private DateTime timeoutStart = DateTime.Now;
        int actTimeout = 0;
        
        public Int16 actInstruction = 0;

        private TcpClient myTcpClientRead;
        private TcpClient myTcpClientWrite;

        String str_PicturesPath = @"\\dcafs3\Testing_SRO\TESTING\SigmaSure\SSManualReportGenerator\PictureFolder\";
        public frm_InstructionsOneByOne()
        {
            InitializeComponent();            
            this.actSN = new SimpleMode_Form.SerialNumberSteps();
        }

        private void frm_InstructionsOneByOne_Load(object sender, EventArgs e)
        {
            this.lbl_SerialNumber.Text = String.Concat("SerialNumber: ", this.actSN.SerialNumber);
            this.lbl_Operator.Text = String.Concat("Operator: ", this.OperatorName);
            this.lbl_Station.Text = String.Concat("Station: ",this.StationName);
            this.lbl_Item.Text = String.Concat("Item/PN: ", this.Item);
            this.lbl_InsCounter.Text = String.Format("Instruction    1/{0}", this.Instructions.Length);

            this.btn_Pass.BackColor = this.BackColor;
            this.btn_Pass.Enabled = false;
            this.btn_Fail.BackColor = this.BackColor;
            this.btn_Fail.Enabled = false;
            

            ProductsConfigurationFile.ChildTestInfo firstCTI = (ProductsConfigurationFile.ChildTestInfo)Instructions.GetValue(0);

            this.lbl_Instruction.Text = firstCTI.Instruction;

            this.timeoutStart = DateTime.Now;
            this.actTimeout = Convert.ToInt32(firstCTI.Timeout);

            String str_actPicturesPath = "";

            if (firstCTI.PicturePath != "")
            {
                if (firstCTI.CameraInspection == "1")
                {
                    str_actPicturesPath = String.Concat(str_PicturesPath, this.Item, "_CAM.jpg");
                }
                else
                {
                    str_actPicturesPath = String.Concat(str_PicturesPath, firstCTI.PicturePath);
                }
                if (Directory.Exists(Path.GetDirectoryName(str_actPicturesPath)))
                {
                    if (!File.Exists(str_actPicturesPath))
                    {
                        this.Cursor = Cursors.Default;
                        if (Path.GetFileName(str_actPicturesPath) == "")
                        {
                            this.pictureBox1.Image = null;
                            return;
                        }
                        else
                        {
                            this.ErrorMessageBoxShow(String.Concat("Neexistuje subor \"", str_actPicturesPath, "\". Zavolajte prosim testovacieho technika."), true);
                        }
                        return;
                    }
                    Image img_PictureToShow = Image.FromFile(str_actPicturesPath);
                    this.pictureBox1.Image = img_PictureToShow;
                    this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            ProductsConfigurationFile.ChildTestInfo actCTI = (ProductsConfigurationFile.ChildTestInfo)this.Instructions.GetValue(actInstruction);
            if (firstCTI.CameraInspection == "1")
            {
                this.btn_Pass.Text = "START";                
            }
            else
                this.btn_Pass.Text = "PASS";
            this.t_Main.Start();
        }
        private void ErrorMessageBoxShow(String Message, Boolean onScreen)
        {
            if (onScreen)
            {
                MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Int32 i = 0;
            this.SaveErrorToLogFile(Message);
            while (i < 9999999)
                i++;
            this.tb_Order.SelectAll();
            this.tb_Order.Focus();
            this.Focus();
        }

        private void SaveErrorToLogFile(String ErrorMessage)
        {
            try
            {
                String str_ErrorLogFileName = String.Concat("ErrorLog_", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), ".txt");
                FileStream fs = new FileStream(str_ErrorLogFileName, FileMode.Append);
                Boolean b_writeEnabled = true;
                if (PathExists(@"\\dcafs3\SHARE\Manufacturing_Engineering\Public\Kolman Vladimir\ErrorLogDir\"))
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
                    String str_localErrorDir = Path.GetDirectoryName(this.myStationConfig.BaseURI.Substring(this.myStationConfig.BaseURI.IndexOf(@"C:/")));
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

                String StationID = this.lbl_Station.Text.Substring(this.lbl_Station.Text.LastIndexOf(':') + 2);
                String ActualTime = String.Concat(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"), "_", DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"), DateTime.Now.Second.ToString("D2"));
                String Operator = this.lbl_Operator.Text.Substring(this.lbl_Station.Text.LastIndexOf(':') + 2);
                String ActualOrderString = this.tb_Order.Text;

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
                if (!completed)
                { exists = false; t.Abort(); }
                return exists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Concat(ex.Message, " 4"));
                return false;
            }
        }

        private void t_Main_Tick(object sender, EventArgs e)
        {
            if (timeoutStart.AddSeconds(actTimeout) < DateTime.Now)
            {
                this.lbl_Timeout.Text = "Mozete odkliknut/odskenovat operaciu";                 
                this.btn_Pass.Enabled = true;
                this.btn_Pass.BackColor = Color.LimeGreen;
                this.btn_Fail.Enabled = true;
                this.btn_Fail.BackColor = Color.Red;
                this.t_Main.Stop();
            }
            else
            {
                int actTOinSec = actTimeout - (DateTime.Now - timeoutStart).Seconds;

                switch (actTOinSec)
                {
                    case 1:
                        this.lbl_Timeout.Text = String.Format("Timeout: {0 } sekunda", actTimeout - (DateTime.Now - timeoutStart).Seconds);
                        break;
                    case 2:
                    case 3:
                    case 4:
                        this.lbl_Timeout.Text = String.Format("Timeout: {0 } sekundy", actTimeout - (DateTime.Now - timeoutStart).Seconds);
                        break;
                    default:
                        this.lbl_Timeout.Text = String.Format("Timeout: {0 } sekund", actTimeout - (DateTime.Now - timeoutStart).Seconds);
                        break;
                }
            }
            this.Focus();
            this.tb_Order.Focus();
        }

        private void btn_Terminate_Click(object sender, EventArgs e)
        {
            this.Response = "TERMINATED";
            this.Close();
        }

        private void tb_Order_KeyUp(object sender, KeyEventArgs e)
        {
            if (!btn_Pass.Enabled)
                return;
            if (e.KeyCode != Keys.Enter)
                return;
            String actOrder = this.tb_Order.Text.Trim();            
            String buffer = Regex.Match(actOrder, @"\d+").Value;
            if (buffer.Length > 12)
            {
                if (buffer.Substring(0,13) == actSN.SerialNumber)
                {
                    actOrder = "$StepPass#";
                }
            }
                    

            ProductsConfigurationFile.ChildTestInfo actCI = (ProductsConfigurationFile.ChildTestInfo)Instructions.GetValue(actInstruction);
            SimpleMode_Form.StepInfo actSI = new SimpleMode_Form.StepInfo();
            if (actCI.CameraInspection == "1")
            {
                actSI = this.TcpConnect();
                if (actSI.Grade == "")
                {
                    this.ErrorMessageBoxShow(String.Concat("Nemozem nadviazat spojenie s kamerovym systemom. Skontrolujte ci je spusteny program pre obsluhu kamery - Ikona je na ploche PC. Ak sa chyba aj napriek tomu opakuje, zavolajte prosim technika."), true);
                    return;
                }
                if (actSI.Grade == "FAIL")
                {
                    return;
                }
            }
            
            if (actCI.ScanBarcode != "")
            {
                if (!ProductsConfigurationFile.CheckValueToMask(actOrder, actCI.ScanBarcode))
                {
                    this.lbl_Instruction.Text = String.Concat("Nespravne zoskenovany udaj\n\n", actCI.Instruction);
                    this.lbl_Instruction.ForeColor = Color.Red;
                    this.tb_Order.Text = "";
                    this.tb_Order.Focus();
                    return;
                }
                else
                {
                    actSI.Name = actCI.Name;
                    actSI.Description = actCI.Instruction;
                    actSI.Grade = "PASS";
                    actSI.ResultValue = actOrder;
                }
                
            }
            else
            {
                if (actOrder == "$StepFail#")
                {
                    actSI.Name = actCI.Name;
                    if (actCI.CameraInspection != "1")
                    {
                        actSI.Description = actCI.Instruction;
                    }                    
                    actSI.Grade = "FAIL";
                    this.Close();
                }
                else if (actOrder == "$StepPass#")
                {
                    actSI.Name = actCI.Name;
                    if (actCI.CameraInspection != "1")
                    {
                        actSI.Description = actCI.Instruction;
                    }
                    actSI.Grade = "PASS";             
                }
                else
                {
                    this.lbl_Instruction.Text = String.Concat("Nespravne zoskenovany/zadany udaj\n\n", actCI.Instruction);
                    this.lbl_Instruction.ForeColor = Color.Red;
                    this.tb_Order.Text = "";
                    this.tb_Order.Focus();
                    return;
                }
            }
            if (actSI.ResultValue.Trim() == "")
            {
                actSI.ResultValue = "Checked";
            }
            Array.Resize(ref actSN.Steps, actSN.Steps.Length + 1);
            actSN.Steps.SetValue(actSI, actSN.Steps.Length - 1);

            this.actInstruction++;
            if (actInstruction == this.Instructions.Length)
            {
                this.Close();
            }
            else
            {
                this.lbl_InsCounter.Text = String.Format("Instruction   {0}/{1}", actInstruction + 1, this.Instructions.Length);
                actCI = (ProductsConfigurationFile.ChildTestInfo)Instructions.GetValue(actInstruction);
                this.actTimeout = Convert.ToInt32(actCI.Timeout);                
                this.lbl_Instruction.Text = actCI.Instruction;
                this.lbl_Instruction.ForeColor = Color.Black;
                if (actCI.CameraInspection == "1")
                    this.btn_Pass.Text = "START";
                else
                    this.btn_Pass.Text = "PASS";
                if (actCI.PicturePath != "")
                {
                    String str_actPicturesPath = String.Concat(str_PicturesPath, actCI.PicturePath);
                    if (actCI.CameraInspection == "1")
                    {
                        Int32 foldIndex = actCI.PicturePath.IndexOf('\\');
                        String instrfolder = actCI.PicturePath.Substring(0, foldIndex + 1);
                        str_actPicturesPath = String.Concat(str_PicturesPath, instrfolder, this.Item, "_CAM.jpg");
                        if (!File.Exists(str_actPicturesPath))
                        {
                            str_actPicturesPath = String.Concat(str_PicturesPath, instrfolder, "Default_CAM.jpg");
                        }
                    }                    
                    if (Directory.Exists(Path.GetDirectoryName(str_actPicturesPath)))
                    {
                        if (!File.Exists(str_actPicturesPath))
                        {
                            this.Cursor = Cursors.Default;
                            if (Path.GetFileName(str_actPicturesPath) == "")
                                return;
                            else
                            {
                                
                                this.ErrorMessageBoxShow(String.Concat("Neexistuje subor \"", str_actPicturesPath, "\". Zavolajte prosim testovacieho technika."), true);
                            }
                            return;
                        }
                        Image img_PictureToShow = Image.FromFile(str_actPicturesPath);
                        this.pictureBox1.Visible = true;
                        this.pictureBox1.Image = img_PictureToShow;
                        this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                }
                else
                {
                    this.pictureBox1.Visible = false;
                }
                this.btn_Pass.BackColor = this.BackColor;
                this.btn_Pass.Enabled = false;
                this.btn_Fail.BackColor = this.BackColor;
                this.btn_Fail.Enabled = false;
                this.tb_Order.Text = "";
                this.timeoutStart = DateTime.Now;
                this.t_Main.Start();
            }
        }

        private void btn_Pass_Click(object sender, EventArgs e)
        {
            this.tb_Order.Text = "$StepPass#";            
            this.tb_Order_KeyUp(new object(), new KeyEventArgs(Keys.Enter));
        }

        private void btn_Fail_Click(object sender, EventArgs e)
        {
            this.tb_Order.Text = "$StepFail#";
            this.tb_Order_KeyUp(new object(), new KeyEventArgs(Keys.Enter));
        }

        String TcpClientServer = "";
        Int32 TcpClientPort = 0;
        

        public SimpleMode_Form.StepInfo TcpConnect()
        {
            SimpleMode_Form.StepInfo retSI = new SimpleMode_Form.StepInfo();

            try
            {
                StationConfig mySC = new StationConfig();
                if (mySC.GetTcpClientActive(ref this.TcpClientServer, ref this.TcpClientPort))
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.myTcpClientRead = new TcpClient(this.TcpClientServer, this.TcpClientPort);
                    NetworkStream myNS = this.myTcpClientRead.GetStream();
                    this.myTcpClientWrite = new TcpClient(this.TcpClientServer, this.TcpClientPort);
                    this.Cursor = Cursors.Default;
                    String strTcpMessage = String.Concat("1000;", this.actSN.SerialNumber, ";", this.Item);
                    byte[] bytesTcpMessage = Encoding.ASCII.GetBytes(strTcpMessage);

                    //MessageBox.Show(String.Concat("Posielam string \n", strTcpMessage, "\n\n byty:\n", bytesTcpMessage.ToString()));
                    this.myTcpClientWrite.Client.Send(bytesTcpMessage, 0, bytesTcpMessage.Length, SocketFlags.None);

                    /*
                    this.myTcpClientRead.Client.Close();
                    this.myTcpClientRead.Close();
                    */
                    //this.myTcpClientWrite = new TcpClient(this.TcpClientServer, this.TcpClientPort);
                    

                    //NetworkStream myNS = this.myTcpClientRead.GetStream();
                    myNS.ReadTimeout = 2000;
                    bytesTcpMessage = new byte[256];
                    String response = String.Empty;
                    Int32 bytes = 0;
                    try
                    {
                        bytes = myNS.Read(bytesTcpMessage, 0, bytesTcpMessage.Length);
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show("kamerovy program nebezi");
                        MessageBox.Show(e.Message);
                    }

                    String strTcpReceivedMessage = Encoding.ASCII.GetString(bytesTcpMessage, 0, bytes);
                    //MessageBox.Show(strTcpReceivedMessage);
                    if (strTcpReceivedMessage == "1001;OK")
                    {
                        this.Visible = false;
                        //myNS.Close();
                        //myNS = this.myTcpClientRead.GetStream();
                        myNS.ReadTimeout = -1;
                        bytesTcpMessage = new byte[256];
                        response = String.Empty;
                        //MessageBox.Show("Zaciatok cakania na vysledok kamerovej kontroly");
                        bytes = myNS.Read(bytesTcpMessage, 0, bytesTcpMessage.Length);
                        strTcpReceivedMessage = Encoding.ASCII.GetString(bytesTcpMessage, 0, bytes);
                        //MessageBox.Show(strTcpReceivedMessage);
                        if (strTcpReceivedMessage.Substring(0, 4) == "2000")
                        {
                            //this.myTcpClientWrite = new TcpClient(TcpClientServer, TcpClientPort);
                            strTcpMessage = "2001;OK";
                            bytesTcpMessage = Encoding.ASCII.GetBytes(strTcpMessage);

                            //MessageBox.Show(String.Concat("Posielam string \n", strTcpMessage, "\n\n byty:\n", bytesTcpMessage.ToString()));
                            this.myTcpClientWrite.Client.Send(bytesTcpMessage, 0, bytesTcpMessage.Length, SocketFlags.None);
                            
                        }
                        if (strTcpReceivedMessage.Substring(0,9) == "2000;PASS")   //navratova hodnota po kamerovej kontrole
                        {
                            Thread.Sleep(2000);
                            retSI.Grade = "PASS";
                            retSI.ResultValue = strTcpReceivedMessage.Substring(10);
                        }
                        else if (strTcpReceivedMessage.Substring(0, 9) == "2000;FAIL")
                        {
                            Thread.Sleep(2000);
                            retSI.Grade = "FAIL";
                            //retSI.ResultValue = strTcpReceivedMessage.Substring(10);
                        }
                        //MessageBox.Show(strTcpReceivedMessage.Substring(10));

                        this.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("TCP komunikacia nepracuje spravne. Zavolajte prosim technika.", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    myNS.Close();
                    this.myTcpClientWrite.Client.Close();
                    this.myTcpClientWrite.Close();
                }
            }
            catch (ArgumentNullException e)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("ArgumentNullException: {0}", e.Message);
            }
            catch (SocketException e)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("SocketException: {0}", e.Message);                
            }

            return retSI; 
        }

        public String TcpListenerStart()
        {

            /*
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            
            this.myTcpListener = new TcpListener(ipAddress, this.TcpClientPort);
            myTcpListener.Start();
            bool done = false;
            while (!done)
            {
                this.myTcpClient = myTcpListener.AcceptTcpClient();
                NetworkStream ns = myTcpClient.GetStream();
                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                try
                {
                    ns.Write(byteTime, 0, byteTime.Length);
                    ns.Close();
                    myTcpClient.Close();
                    done = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            myTcpListener.Stop();
            */
            String retval = "tt";
            return retval;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SigmaSureManualReportGenerator
{
    public partial class frm_Instructions : Form
    {
        public String strFinalResult = "Terminated";
        private String SerialNumber;
        private String Item;
        private String TestKind;
        public SimpleMode_Form.StepInfo[] StepInfos;
        public Array Instructions;

        private String ConfigPath;
        private ProductsConfigurationFile ProductsConfig;

        public String Response;

        public frm_Instructions(String SerialNumber, String Item, String TestKind, Array Instructions)
        {
            InitializeComponent();
            this.SerialNumber = SerialNumber;
            this.Item = Item;
            this.TestKind = TestKind;
            this.Instructions = Instructions;
            this.lbl_Informations.Text = String.Concat(TestKind, @"    SN: ", SerialNumber, @"    ITEM: ", Item);
            
        }

        private void frm_Instructions_Load(object sender, EventArgs e)
        {
            Screen current = Screen.FromControl(this);
            Rectangle area = current.WorkingArea;
            this.Size = new Size(area.Width, area.Height);

            int nBtnsWidth = (area.Width - 48) / 3;
            this.btnPASS.Location = new Point(12, 50);
            this.btnFAIL.Location = new Point(12 + nBtnsWidth + 12, 50);
            this.btnTERMINATE.Location = new Point(12 + nBtnsWidth + 12 + nBtnsWidth + 12, 50);

            this.btnPASS.Size = new Size(nBtnsWidth, 63);
            this.btnFAIL.Size = new Size(nBtnsWidth, 63);
            this.btnTERMINATE.Size = new Size(nBtnsWidth, 63);

            Int32 n_majorOSVersion = Environment.OSVersion.Version.Major;

            if (n_majorOSVersion == 5)
                this.ConfigPath = @"C:\Documents and Settings\All Users\Application Data\SSManualReportGenerator\";
            else if (n_majorOSVersion == 6)
                this.ConfigPath = @"C:\Users\Public\SSManualReportGenerator\";
            else
            {
                MessageBox.Show("Neznama verzia operacneho systemu. Zavolajte prosim testovacieho inziniera");
                this.Dispose();
                return;
            }

            this.ProductsConfig = new ProductsConfigurationFile(this.ConfigPath);

            this.dgv_Instructions = new DGV_Instructions(this.ProductsConfig.GetChildTestsInfos(this.ProductName, this.TestKind));
            this.dgv_Instructions.DefaultCellStyle.Font = new Font("Arial", 20.0F);
            this.dgv_Instructions.Location = new Point(12, 120);
            this.dgv_Instructions.RowHeadersVisible = false;
            this.dgv_Instructions.Size = new Size((area.Width - 12)/2, area.Height - 140);
            this.dgv_Instructions.Columns[1].Width = this.dgv_Instructions.Width / 2;
            //this.dgv_Instructions.Columns[5].Width = this.dgv_Instructions.Width - (this.dgv_Instructions.Columns[0].Width + this.dgv_Instructions.Columns[1].Width + this.dgv_Instructions.Columns[2].Width + this.dgv_Instructions.Columns[3].Width + this.dgv_Instructions.Columns[4].Width);



            for (int i = 0; i < this.Instructions.Length; i++)
            {
                ProductsConfigurationFile.ChildTestInfo actCTI = (ProductsConfigurationFile.ChildTestInfo)this.Instructions.GetValue(i);
                if (actCTI.ScanBarcode == "")
                {
                    this.dgv_Instructions.Rows.Add(actCTI.Name, actCTI.Instruction, "", "PASS", "FAIL", "");
                }
                else
                {
                    this.dgv_Instructions.Rows.Add(actCTI.Name, actCTI.Instruction, "", "SCAN", "FAIL", "");
                }
            }

            this.Controls.Add(this.dgv_Instructions);
            
            this.dgv_Instructions.Columns[5].Width = this.dgv_Instructions.Width - (this.dgv_Instructions.Columns[0].Width + this.dgv_Instructions.Columns[1].Width + this.dgv_Instructions.Columns[2].Width + this.dgv_Instructions.Columns[3].Width + this.dgv_Instructions.Columns[4].Width);

        }

        private void btnTERMINATE_Click(object sender, EventArgs e)
        {
            this.Response = "TERMINATED";
            this.Close();
        }
    }
}

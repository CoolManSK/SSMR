using System;
using System.Windows.Forms;
using System.Drawing;

namespace SigmaSureManualReportGenerator
{
    public class DGV_Instructions : DataGridView
    {
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Instructions;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Result;
        private System.Windows.Forms.DataGridViewButtonColumn c_PassRes;
        private System.Windows.Forms.DataGridViewButtonColumn c_FailRes;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Comment;
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();

        Array ar_ChildTestInfos;

        public DGV_Instructions(Array ChildTestInfos)
        {            
            InitializeComponent();
            this.ar_ChildTestInfos = ChildTestInfos;
        }

        private void InitializeComponent()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            
            this.CellClick += new DataGridViewCellEventHandler(this.dgv_Instructions_CellClick);

            // 
            // c_Name
            //
            this.c_Name = new DataGridViewTextBoxColumn();
            this.c_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Name.HeaderText = "Por.c.";
            this.c_Name.Name = "c_Name";
            this.c_Name.ReadOnly = true;
            this.c_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Name.DefaultCellStyle.Font = new Font("Arial", 18.5F, GraphicsUnit.Pixel);
            this.c_Name.Width = 61;
            // 
            // c_Instructions
            // 
            this.c_Instructions = new DataGridViewTextBoxColumn();
            this.c_Instructions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.c_Instructions.DefaultCellStyle = dataGridViewCellStyle1;
            this.c_Instructions.HeaderText = "Instrukcie/Postup";
            this.c_Instructions.Name = "c_Instructions";
            this.c_Instructions.ReadOnly = true;
            this.c_Instructions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Instructions.DefaultCellStyle.Font = new Font("Arial", 18.5F, GraphicsUnit.Pixel);
            this.c_Instructions.Width = 155;
            // 
            // c_Result
            // 
            this.c_Result = new DataGridViewTextBoxColumn();
            this.c_Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Result.HeaderText = "Vysledok";
            this.c_Result.Name = "c_Result";
            this.c_Result.ReadOnly = true;
            this.c_Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Result.DefaultCellStyle.Font = new Font("Arial", 18.5F, GraphicsUnit.Pixel);
            this.c_Result.Width = 87;
            // 
            // c_PassRes
            // 
            this.c_PassRes = new DataGridViewButtonColumn();
            this.c_PassRes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_PassRes.HeaderText = "PASS";
            this.c_PassRes.Name = "c_PassRes";
            this.c_PassRes.ReadOnly = true;
            this.c_PassRes.DefaultCellStyle.Font = new Font("Arial", 18.5F, GraphicsUnit.Pixel);
            this.c_PassRes.Width = 62;
            // 
            // c_FailRes
            // 
            this.c_FailRes = new DataGridViewButtonColumn();
            this.c_FailRes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_FailRes.HeaderText = "FAIL";
            this.c_FailRes.Name = "c_FailRes";
            this.c_FailRes.ReadOnly = true;
            this.c_FailRes.DefaultCellStyle.Font = new Font("Arial", 18.5F, GraphicsUnit.Pixel);
            this.c_FailRes.Width = 54;
            // 
            // c_Comment
            // 
            this.c_Comment = new DataGridViewTextBoxColumn();
            this.c_Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Comment.HeaderText = "Poznamka";
            this.c_Comment.Name = "c_Comment";
            this.c_Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Comment.DefaultCellStyle.Font = new Font("Arial", 18.5F, GraphicsUnit.Pixel);
            this.c_Comment.Width = 98;

            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_Name,
            this.c_Instructions,
            this.c_Result,
            this.c_PassRes,
            this.c_FailRes,
            this.c_Comment});
        }        

        private void dgv_Instructions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex < 3) || (e.ColumnIndex > 4))
                return;
            if (e.ColumnIndex == 3)
            {
                if (this.Rows[e.RowIndex].Cells[3].Value.ToString() == "SCAN")
                {
                    InputBox_Form myIBF = new InputBox_Form("Pouzite snimac ciarovych kodov", this.Rows[e.RowIndex].Cells[1].Value.ToString());
                    myIBF.ShowDialog();

                    if (myIBF.Answer != "")
                    {
                        ProductsConfigurationFile.ChildTestInfo actCTI = (ProductsConfigurationFile.ChildTestInfo)this.ar_ChildTestInfos.GetValue(e.RowIndex);


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
                            this.Rows[e.RowIndex].Cells[2].Value = "PASS";
                            this.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        }
                        else
                        {
                            this.Rows[e.RowIndex].Cells[2].Value = "FAIL";
                            this.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        }
                        this.Rows[e.RowIndex].Cells[2].ToolTipText = myIBF.Answer;
                    }
                }
                else
                {
                    this.Rows[e.RowIndex].Cells[2].Value = "PASS";
                    this.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                    this.Rows[e.RowIndex].Cells[2].ToolTipText = "";
                }
            }
            else if (e.ColumnIndex == 4)
            {
                this.Rows[e.RowIndex].Cells[2].Value = "FAIL";
                this.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                this.Rows[e.RowIndex].Cells[2].ToolTipText = "";
            }            
        }
    }
}

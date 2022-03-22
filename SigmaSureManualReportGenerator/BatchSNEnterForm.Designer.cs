namespace SigmaSureManualReportGenerator
{
    partial class BatchSNEnterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_ProductNo = new System.Windows.Forms.Label();
            this.lbl_JobID = new System.Windows.Forms.Label();
            this.lbl_TestType = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ScanField = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_CreateReports = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_SortSNs = new System.Windows.Forms.Button();
            this.dgv_SerialNumbers = new System.Windows.Forms.DataGridView();
            this.LineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_SN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_Pass = new System.Windows.Forms.DataGridViewButtonColumn();
            this.c_Fail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.c_Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cms_BatchNumbersCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_CopySN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PasteSN = new System.Windows.Forms.ToolStripMenuItem();
            this.gb_FailedSteps = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_FailedStepNameEnter = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SerialNumbers)).BeginInit();
            this.cms_BatchNumbersCopyPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Job ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Typ testu:";
            // 
            // lbl_ProductNo
            // 
            this.lbl_ProductNo.AutoSize = true;
            this.lbl_ProductNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_ProductNo.Location = new System.Drawing.Point(144, 85);
            this.lbl_ProductNo.Name = "lbl_ProductNo";
            this.lbl_ProductNo.Size = new System.Drawing.Size(0, 20);
            this.lbl_ProductNo.TabIndex = 3;
            this.lbl_ProductNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_JobID
            // 
            this.lbl_JobID.AutoSize = true;
            this.lbl_JobID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_JobID.Location = new System.Drawing.Point(144, 110);
            this.lbl_JobID.Name = "lbl_JobID";
            this.lbl_JobID.Size = new System.Drawing.Size(0, 20);
            this.lbl_JobID.TabIndex = 4;
            this.lbl_JobID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_TestType
            // 
            this.lbl_TestType.AutoSize = true;
            this.lbl_TestType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_TestType.Location = new System.Drawing.Point(144, 135);
            this.lbl_TestType.Name = "lbl_TestType";
            this.lbl_TestType.Size = new System.Drawing.Size(0, 20);
            this.lbl_TestType.TabIndex = 5;
            this.lbl_TestType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 60);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Napoveda";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(396, 33);
            this.label4.TabIndex = 1;
            this.label4.Text = "Do skenovacieho pola postupne oskenujte vsetky vyrobky so spolocnym JobID a nasle" +
    "dne kliknite na \"Vytvor reporty\" tlacidlo. ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Sken pole:";
            // 
            // tb_ScanField
            // 
            this.tb_ScanField.Location = new System.Drawing.Point(102, 168);
            this.tb_ScanField.Name = "tb_ScanField";
            this.tb_ScanField.Size = new System.Drawing.Size(293, 26);
            this.tb_ScanField.TabIndex = 8;
            this.tb_ScanField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ScanField_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(13, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(269, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Zoznam zoskenovanych seriovych cisiel (0):";
            // 
            // btn_CreateReports
            // 
            this.btn_CreateReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_CreateReports.Enabled = false;
            this.btn_CreateReports.Location = new System.Drawing.Point(16, 666);
            this.btn_CreateReports.Name = "btn_CreateReports";
            this.btn_CreateReports.Size = new System.Drawing.Size(122, 31);
            this.btn_CreateReports.TabIndex = 11;
            this.btn_CreateReports.Text = "Vytvor reporty";
            this.btn_CreateReports.UseVisualStyleBackColor = true;
            this.btn_CreateReports.Click += new System.EventHandler(this.btn_CreateReports_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(721, 666);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 31);
            this.button2.TabIndex = 12;
            this.button2.Text = "Koniec";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_SortSNs
            // 
            this.btn_SortSNs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SortSNs.Enabled = false;
            this.btn_SortSNs.Location = new System.Drawing.Point(298, 666);
            this.btn_SortSNs.Name = "btn_SortSNs";
            this.btn_SortSNs.Size = new System.Drawing.Size(122, 31);
            this.btn_SortSNs.TabIndex = 13;
            this.btn_SortSNs.Text = "Zorad cisla";
            this.btn_SortSNs.UseVisualStyleBackColor = true;
            this.btn_SortSNs.Click += new System.EventHandler(this.btn_SortSNs_Click);
            // 
            // dgv_SerialNumbers
            // 
            this.dgv_SerialNumbers.AllowUserToAddRows = false;
            this.dgv_SerialNumbers.AllowUserToDeleteRows = false;
            this.dgv_SerialNumbers.AllowUserToResizeColumns = false;
            this.dgv_SerialNumbers.AllowUserToResizeRows = false;
            this.dgv_SerialNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv_SerialNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SerialNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineNumber,
            this.c_SN,
            this.c_Pass,
            this.c_Fail,
            this.c_Delete});
            this.dgv_SerialNumbers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_SerialNumbers.Location = new System.Drawing.Point(16, 227);
            this.dgv_SerialNumbers.MultiSelect = false;
            this.dgv_SerialNumbers.Name = "dgv_SerialNumbers";
            this.dgv_SerialNumbers.ReadOnly = true;
            this.dgv_SerialNumbers.RowHeadersVisible = false;
            this.dgv_SerialNumbers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_SerialNumbers.RowTemplate.Height = 35;
            this.dgv_SerialNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_SerialNumbers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_SerialNumbers.Size = new System.Drawing.Size(404, 433);
            this.dgv_SerialNumbers.TabIndex = 14;
            this.dgv_SerialNumbers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_SerialNumbers_CellMouseDown);
            this.dgv_SerialNumbers.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_SerialNumbers_CellMouseEnter);
            this.dgv_SerialNumbers.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_SerialNumbers_CellMouseUp);
            this.dgv_SerialNumbers.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_SerialNumbers_RowsAdded);
            this.dgv_SerialNumbers.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_SerialNumbers_RowsRemoved);
            // 
            // LineNumber
            // 
            this.LineNumber.Frozen = true;
            this.LineNumber.HeaderText = "Nr.";
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.ReadOnly = true;
            this.LineNumber.Width = 35;
            // 
            // c_SN
            // 
            this.c_SN.Frozen = true;
            this.c_SN.HeaderText = "SerialNumber";
            this.c_SN.Name = "c_SN";
            this.c_SN.ReadOnly = true;
            this.c_SN.Width = 150;
            // 
            // c_Pass
            // 
            this.c_Pass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Pass.Frozen = true;
            this.c_Pass.HeaderText = "PASS";
            this.c_Pass.Name = "c_Pass";
            this.c_Pass.ReadOnly = true;
            this.c_Pass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_Pass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.c_Pass.Text = "PASS";
            this.c_Pass.Width = 77;
            // 
            // c_Fail
            // 
            this.c_Fail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Fail.Frozen = true;
            this.c_Fail.HeaderText = "FAIL";
            this.c_Fail.Name = "c_Fail";
            this.c_Fail.ReadOnly = true;
            this.c_Fail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.c_Fail.Text = "FAIL";
            this.c_Fail.UseColumnTextForButtonValue = true;
            this.c_Fail.Width = 50;
            // 
            // c_Delete
            // 
            this.c_Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Delete.HeaderText = "DELETE";
            this.c_Delete.Name = "c_Delete";
            this.c_Delete.ReadOnly = true;
            this.c_Delete.Text = "DELETE";
            this.c_Delete.Width = 78;
            // 
            // cms_BatchNumbersCopyPaste
            // 
            this.cms_BatchNumbersCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_CopySN,
            this.tsmi_PasteSN});
            this.cms_BatchNumbersCopyPaste.Name = "cms_BatchNumbersCopyPaste";
            this.cms_BatchNumbersCopyPaste.Size = new System.Drawing.Size(103, 48);
            // 
            // tsmi_CopySN
            // 
            this.tsmi_CopySN.Name = "tsmi_CopySN";
            this.tsmi_CopySN.Size = new System.Drawing.Size(102, 22);
            this.tsmi_CopySN.Text = "Copy";
            this.tsmi_CopySN.Click += new System.EventHandler(this.tsmi_CopySN_Click);
            // 
            // tsmi_PasteSN
            // 
            this.tsmi_PasteSN.Name = "tsmi_PasteSN";
            this.tsmi_PasteSN.Size = new System.Drawing.Size(102, 22);
            this.tsmi_PasteSN.Text = "Paste";
            this.tsmi_PasteSN.Click += new System.EventHandler(this.tsmi_PasteSN_Click);
            // 
            // gb_FailedSteps
            // 
            this.gb_FailedSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_FailedSteps.Location = new System.Drawing.Point(426, 91);
            this.gb_FailedSteps.Name = "gb_FailedSteps";
            this.gb_FailedSteps.Size = new System.Drawing.Size(417, 569);
            this.gb_FailedSteps.TabIndex = 15;
            this.gb_FailedSteps.TabStop = false;
            this.gb_FailedSteps.Text = "Failed kroky";
            this.gb_FailedSteps.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(422, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(305, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Zadajte nazov failed kroku a stlacte Enter:";
            this.label7.Visible = false;
            // 
            // tb_FailedStepNameEnter
            // 
            this.tb_FailedStepNameEnter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FailedStepNameEnter.Location = new System.Drawing.Point(426, 46);
            this.tb_FailedStepNameEnter.Name = "tb_FailedStepNameEnter";
            this.tb_FailedStepNameEnter.Size = new System.Drawing.Size(417, 26);
            this.tb_FailedStepNameEnter.TabIndex = 17;
            this.tb_FailedStepNameEnter.Visible = false;
            this.tb_FailedStepNameEnter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_FailedStepNameEnter_KeyUp);
            // 
            // BatchSNEnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 709);
            this.ControlBox = false;
            this.Controls.Add(this.tb_FailedStepNameEnter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.gb_FailedSteps);
            this.Controls.Add(this.dgv_SerialNumbers);
            this.Controls.Add(this.btn_SortSNs);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_CreateReports);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_ScanField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_TestType);
            this.Controls.Add(this.lbl_JobID);
            this.Controls.Add(this.lbl_ProductNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BatchSNEnterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hromadne zadavanie seriovych cisiel";
            this.Load += new System.EventHandler(this.BatchSNEnterForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SerialNumbers)).EndInit();
            this.cms_BatchNumbersCopyPaste.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_ProductNo;
        private System.Windows.Forms.Label lbl_JobID;
        private System.Windows.Forms.Label lbl_TestType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ScanField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_CreateReports;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_SortSNs;
        private System.Windows.Forms.DataGridView dgv_SerialNumbers;
        private System.Windows.Forms.ContextMenuStrip cms_BatchNumbersCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmi_CopySN;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PasteSN;
        private System.Windows.Forms.GroupBox gb_FailedSteps;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_FailedStepNameEnter;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_SN;
        private System.Windows.Forms.DataGridViewButtonColumn c_Pass;
        private System.Windows.Forms.DataGridViewButtonColumn c_Fail;
        private System.Windows.Forms.DataGridViewButtonColumn c_Delete;
    }
}
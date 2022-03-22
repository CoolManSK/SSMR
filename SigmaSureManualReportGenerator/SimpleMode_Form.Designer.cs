namespace SigmaSureManualReportGenerator
{
    partial class SimpleMode_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_SerialNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_SNsIP = new System.Windows.Forms.DataGridView();
            this.c_SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_TestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_Pass = new System.Windows.Forms.DataGridViewButtonColumn();
            this.c_Fail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.c_Terminate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_TestTypes = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_Info = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_FromSN = new System.Windows.Forms.ComboBox();
            this.cb_ToSN = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_JobID = new System.Windows.Forms.TextBox();
            this.btn_Authorize = new System.Windows.Forms.Button();
            this.chb_JobIDChecking = new System.Windows.Forms.CheckBox();
            this.chb_ChecklistImmediatelyStart = new System.Windows.Forms.CheckBox();
            this.btn_SupportRequest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SNsIP)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seriove cislo:";
            // 
            // tb_SerialNumber
            // 
            this.tb_SerialNumber.Enabled = false;
            this.tb_SerialNumber.Location = new System.Drawing.Point(132, 62);
            this.tb_SerialNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_SerialNumber.Name = "tb_SerialNumber";
            this.tb_SerialNumber.Size = new System.Drawing.Size(268, 26);
            this.tb_SerialNumber.TabIndex = 1;
            this.tb_SerialNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_SerialNumber_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgv_SNsIP);
            this.groupBox1.Location = new System.Drawing.Point(13, 182);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(982, 415);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seriove cisla v procese";
            // 
            // dgv_SNsIP
            // 
            this.dgv_SNsIP.AllowUserToAddRows = false;
            this.dgv_SNsIP.AllowUserToDeleteRows = false;
            this.dgv_SNsIP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_SNsIP.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgv_SNsIP.ColumnHeadersHeight = 30;
            this.dgv_SNsIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_SNsIP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_SerialNumber,
            this.c_ProductID,
            this.c_TestType,
            this.c_operator,
            this.c_StartTime,
            this.c_Pass,
            this.c_Fail,
            this.c_Terminate});
            this.dgv_SNsIP.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_SNsIP.Location = new System.Drawing.Point(9, 29);
            this.dgv_SNsIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_SNsIP.Name = "dgv_SNsIP";
            this.dgv_SNsIP.ReadOnly = true;
            this.dgv_SNsIP.RowHeadersVisible = false;
            this.dgv_SNsIP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_SNsIP.Size = new System.Drawing.Size(965, 367);
            this.dgv_SNsIP.TabIndex = 0;
            this.dgv_SNsIP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_SNsIP_CellClick);
            this.dgv_SNsIP.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_SNsIP_RowsAdded);
            this.dgv_SNsIP.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgv_SNsIP_RowsRemoved);
            // 
            // c_SerialNumber
            // 
            this.c_SerialNumber.HeaderText = "SerialNumber";
            this.c_SerialNumber.Name = "c_SerialNumber";
            this.c_SerialNumber.ReadOnly = true;
            this.c_SerialNumber.Width = 150;
            // 
            // c_ProductID
            // 
            this.c_ProductID.HeaderText = "Product ID";
            this.c_ProductID.Name = "c_ProductID";
            this.c_ProductID.ReadOnly = true;
            this.c_ProductID.Width = 200;
            // 
            // c_TestType
            // 
            this.c_TestType.HeaderText = "Test";
            this.c_TestType.Name = "c_TestType";
            this.c_TestType.ReadOnly = true;
            // 
            // c_operator
            // 
            this.c_operator.HeaderText = "Operator";
            this.c_operator.Name = "c_operator";
            this.c_operator.ReadOnly = true;
            // 
            // c_StartTime
            // 
            this.c_StartTime.HeaderText = "Start time";
            this.c_StartTime.Name = "c_StartTime";
            this.c_StartTime.ReadOnly = true;
            this.c_StartTime.Width = 160;
            // 
            // c_Pass
            // 
            this.c_Pass.HeaderText = "Pass";
            this.c_Pass.Name = "c_Pass";
            this.c_Pass.ReadOnly = true;
            this.c_Pass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.c_Pass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.c_Pass.Width = 76;
            // 
            // c_Fail
            // 
            this.c_Fail.HeaderText = "Fail";
            this.c_Fail.Name = "c_Fail";
            this.c_Fail.ReadOnly = true;
            this.c_Fail.Width = 76;
            // 
            // c_Terminate
            // 
            this.c_Terminate.HeaderText = "Terminate";
            this.c_Terminate.Name = "c_Terminate";
            this.c_Terminate.ReadOnly = true;
            this.c_Terminate.Width = 113;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Typ Testu:";
            // 
            // cb_TestTypes
            // 
            this.cb_TestTypes.FormattingEnabled = true;
            this.cb_TestTypes.Location = new System.Drawing.Point(132, 18);
            this.cb_TestTypes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_TestTypes.Name = "cb_TestTypes";
            this.cb_TestTypes.Size = new System.Drawing.Size(268, 28);
            this.cb_TestTypes.TabIndex = 4;
            this.cb_TestTypes.SelectedIndexChanged += new System.EventHandler(this.cb_TestTypes_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_Info);
            this.groupBox2.Location = new System.Drawing.Point(13, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(879, 80);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info";
            // 
            // lbl_Info
            // 
            this.lbl_Info.AutoSize = true;
            this.lbl_Info.Location = new System.Drawing.Point(6, 22);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(61, 20);
            this.lbl_Info.TabIndex = 0;
            this.lbl_Info.Text = "lbl_Info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(437, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Rozsah SN. Od:";
            // 
            // cb_FromSN
            // 
            this.cb_FromSN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FromSN.Enabled = false;
            this.cb_FromSN.FormattingEnabled = true;
            this.cb_FromSN.ItemHeight = 20;
            this.cb_FromSN.Location = new System.Drawing.Point(560, 62);
            this.cb_FromSN.MaxDropDownItems = 10;
            this.cb_FromSN.Name = "cb_FromSN";
            this.cb_FromSN.Size = new System.Drawing.Size(79, 28);
            this.cb_FromSN.TabIndex = 7;
            this.cb_FromSN.SelectedIndexChanged += new System.EventHandler(this.cb_FromSN_SelectedIndexChanged);
            // 
            // cb_ToSN
            // 
            this.cb_ToSN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ToSN.Enabled = false;
            this.cb_ToSN.FormattingEnabled = true;
            this.cb_ToSN.ItemHeight = 20;
            this.cb_ToSN.Location = new System.Drawing.Point(674, 62);
            this.cb_ToSN.MaxDropDownItems = 10;
            this.cb_ToSN.Name = "cb_ToSN";
            this.cb_ToSN.Size = new System.Drawing.Size(79, 28);
            this.cb_ToSN.TabIndex = 9;
            this.cb_ToSN.SelectedIndexChanged += new System.EventHandler(this.cb_ToSN_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(641, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Do:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(437, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "JobID:";
            // 
            // tb_JobID
            // 
            this.tb_JobID.Enabled = false;
            this.tb_JobID.Location = new System.Drawing.Point(501, 20);
            this.tb_JobID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tb_JobID.Name = "tb_JobID";
            this.tb_JobID.Size = new System.Drawing.Size(145, 26);
            this.tb_JobID.TabIndex = 11;
            this.tb_JobID.DoubleClick += new System.EventHandler(this.tb_JobID_DoubleClick);
            this.tb_JobID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_JobID_KeyUp);
            this.tb_JobID.Leave += new System.EventHandler(this.tb_JobID_Leave);
            // 
            // btn_Authorize
            // 
            this.btn_Authorize.Enabled = false;
            this.btn_Authorize.Location = new System.Drawing.Point(759, 62);
            this.btn_Authorize.Name = "btn_Authorize";
            this.btn_Authorize.Size = new System.Drawing.Size(102, 28);
            this.btn_Authorize.TabIndex = 12;
            this.btn_Authorize.Text = "Authorize";
            this.btn_Authorize.UseVisualStyleBackColor = true;
            this.btn_Authorize.Click += new System.EventHandler(this.btn_Authorize_Click);
            // 
            // chb_JobIDChecking
            // 
            this.chb_JobIDChecking.AutoSize = true;
            this.chb_JobIDChecking.Location = new System.Drawing.Point(653, 22);
            this.chb_JobIDChecking.Name = "chb_JobIDChecking";
            this.chb_JobIDChecking.Size = new System.Drawing.Size(194, 24);
            this.chb_JobIDChecking.TabIndex = 13;
            this.chb_JobIDChecking.Text = "Kontrola jednej zakazky";
            this.chb_JobIDChecking.UseVisualStyleBackColor = true;
            this.chb_JobIDChecking.CheckedChanged += new System.EventHandler(this.chb_JobIDChecking_CheckedChanged);
            // 
            // chb_ChecklistImmediatelyStart
            // 
            this.chb_ChecklistImmediatelyStart.Location = new System.Drawing.Point(867, 23);
            this.chb_ChecklistImmediatelyStart.Name = "chb_ChecklistImmediatelyStart";
            this.chb_ChecklistImmediatelyStart.Size = new System.Drawing.Size(142, 66);
            this.chb_ChecklistImmediatelyStart.TabIndex = 14;
            this.chb_ChecklistImmediatelyStart.Text = "Spustit postup po prvom skenovani";
            this.chb_ChecklistImmediatelyStart.UseVisualStyleBackColor = true;
            this.chb_ChecklistImmediatelyStart.CheckedChanged += new System.EventHandler(this.chb_ChecklistImmediatelyStart_CheckedChanged);
            // 
            // btn_SupportRequest
            // 
            this.btn_SupportRequest.Enabled = false;
            this.btn_SupportRequest.Location = new System.Drawing.Point(896, 95);
            this.btn_SupportRequest.Name = "btn_SupportRequest";
            this.btn_SupportRequest.Size = new System.Drawing.Size(102, 79);
            this.btn_SupportRequest.TabIndex = 15;
            this.btn_SupportRequest.Text = "Support Request";
            this.btn_SupportRequest.UseVisualStyleBackColor = true;
            this.btn_SupportRequest.Click += new System.EventHandler(this.btn_SupportRequest_Click);
            // 
            // SimpleMode_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 601);
            this.Controls.Add(this.btn_SupportRequest);
            this.Controls.Add(this.chb_ChecklistImmediatelyStart);
            this.Controls.Add(this.chb_JobIDChecking);
            this.Controls.Add(this.btn_Authorize);
            this.Controls.Add(this.tb_JobID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_ToSN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_FromSN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cb_TestTypes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_SerialNumber);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SimpleMode_Form";
            this.Text = "Simple Mode";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SimpleMode_Form_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SNsIP)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_SerialNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_TestTypes;
        private System.Windows.Forms.DataGridView dgv_SNsIP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_TestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_StartTime;
        private System.Windows.Forms.DataGridViewButtonColumn c_Pass;
        private System.Windows.Forms.DataGridViewButtonColumn c_Fail;
        private System.Windows.Forms.DataGridViewButtonColumn c_Terminate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_FromSN;
        private System.Windows.Forms.ComboBox cb_ToSN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_JobID;
        private System.Windows.Forms.Button btn_Authorize;
        private System.Windows.Forms.CheckBox chb_JobIDChecking;
        private System.Windows.Forms.CheckBox chb_ChecklistImmediatelyStart;
        private System.Windows.Forms.Button btn_SupportRequest;
    }
}
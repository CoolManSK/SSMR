namespace SigmaSureManualReportGenerator
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gb_Product = new System.Windows.Forms.GroupBox();
            this.cb_LastSerialNumbers = new System.Windows.Forms.ComboBox();
            this.cb_TestType = new System.Windows.Forms.ComboBox();
            this.pb_TestType = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_SerialNumber = new System.Windows.Forms.Label();
            this.lbl_JobIDValue = new System.Windows.Forms.Label();
            this.pb_SerialNumber = new System.Windows.Forms.PictureBox();
            this.pB_JobID = new System.Windows.Forms.PictureBox();
            this.pB_Assembly = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_ProductNo = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbl_OperatorNr = new System.Windows.Forms.Label();
            this.cms_AdminContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modifyStationConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPublishedStationConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMaintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openChangeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_OperatorSurname = new System.Windows.Forms.Label();
            this.gb_Results = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enableResultsFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableResultsFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_Instructions = new System.Windows.Forms.DataGridView();
            this.c_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_Instructions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_PassRes = new System.Windows.Forms.DataGridViewButtonColumn();
            this.c_FailRes = new System.Windows.Forms.DataGridViewButtonColumn();
            this.c_Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_BatchMode = new System.Windows.Forms.Button();
            this.lbl_StationName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_AvailableSettings = new System.Windows.Forms.Button();
            this.btn_PasswordChange = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_Barcodes = new System.Windows.Forms.GroupBox();
            this.btn_GenerateReport = new System.Windows.Forms.Button();
            this.tb_OrderValue = new System.Windows.Forms.TextBox();
            this.lbl_ScanEditOrder = new System.Windows.Forms.Label();
            this.pb_BarcodesToScan = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gb_FaultCodes = new System.Windows.Forms.GroupBox();
            this.btn_FaultCodes_AddFailure = new System.Windows.Forms.Button();
            this.cb_FaultCodes_Description = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_FaultCodes_Code = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.gb_Product.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_TestType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_SerialNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_JobID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Assembly)).BeginInit();
            this.cms_AdminContext.SuspendLayout();
            this.gb_Results.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Instructions)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gb_Barcodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BarcodesToScan)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gb_FaultCodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Product
            // 
            this.gb_Product.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Product.Controls.Add(this.cb_LastSerialNumbers);
            this.gb_Product.Controls.Add(this.cb_TestType);
            this.gb_Product.Controls.Add(this.pb_TestType);
            this.gb_Product.Controls.Add(this.label6);
            this.gb_Product.Controls.Add(this.lbl_SerialNumber);
            this.gb_Product.Controls.Add(this.lbl_JobIDValue);
            this.gb_Product.Controls.Add(this.pb_SerialNumber);
            this.gb_Product.Controls.Add(this.pB_JobID);
            this.gb_Product.Controls.Add(this.pB_Assembly);
            this.gb_Product.Controls.Add(this.label4);
            this.gb_Product.Controls.Add(this.label2);
            this.gb_Product.Controls.Add(this.label1);
            this.gb_Product.Controls.Add(this.cb_ProductNo);
            this.gb_Product.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gb_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gb_Product.Location = new System.Drawing.Point(223, 12);
            this.gb_Product.Name = "gb_Product";
            this.gb_Product.Size = new System.Drawing.Size(748, 161);
            this.gb_Product.TabIndex = 1;
            this.gb_Product.TabStop = false;
            this.gb_Product.Text = "Product";
            // 
            // cb_LastSerialNumbers
            // 
            this.cb_LastSerialNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_LastSerialNumbers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LastSerialNumbers.FormattingEnabled = true;
            this.cb_LastSerialNumbers.Location = new System.Drawing.Point(504, 124);
            this.cb_LastSerialNumbers.Name = "cb_LastSerialNumbers";
            this.cb_LastSerialNumbers.Size = new System.Drawing.Size(204, 28);
            this.cb_LastSerialNumbers.TabIndex = 3;
            this.cb_LastSerialNumbers.SelectedIndexChanged += new System.EventHandler(this.cb_LastSerialNumbers_SelectedIndexChanged);
            // 
            // cb_TestType
            // 
            this.cb_TestType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_TestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TestType.FormattingEnabled = true;
            this.cb_TestType.Location = new System.Drawing.Point(125, 92);
            this.cb_TestType.Name = "cb_TestType";
            this.cb_TestType.Size = new System.Drawing.Size(583, 28);
            this.cb_TestType.TabIndex = 2;
            this.cb_TestType.SelectedIndexChanged += new System.EventHandler(this.cb_TestType_SelectedIndexChanged);
            // 
            // pb_TestType
            // 
            this.pb_TestType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_TestType.Location = new System.Drawing.Point(714, 92);
            this.pb_TestType.Name = "pb_TestType";
            this.pb_TestType.Size = new System.Drawing.Size(26, 26);
            this.pb_TestType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_TestType.TabIndex = 17;
            this.pb_TestType.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(6, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "Test Type:";
            // 
            // lbl_SerialNumber
            // 
            this.lbl_SerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SerialNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_SerialNumber.Location = new System.Drawing.Point(125, 126);
            this.lbl_SerialNumber.Name = "lbl_SerialNumber";
            this.lbl_SerialNumber.Size = new System.Drawing.Size(373, 24);
            this.lbl_SerialNumber.TabIndex = 14;
            this.toolTip1.SetToolTip(this.lbl_SerialNumber, "Lavy dvojklik pre zmenu Serial Number.");
            this.lbl_SerialNumber.TextChanged += new System.EventHandler(this.lbl_SerialNumber_TextChanged);
            this.lbl_SerialNumber.DoubleClick += new System.EventHandler(this.lbl_SerialNumber_DoubleClick);
            // 
            // lbl_JobIDValue
            // 
            this.lbl_JobIDValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_JobIDValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_JobIDValue.Location = new System.Drawing.Point(125, 61);
            this.lbl_JobIDValue.Name = "lbl_JobIDValue";
            this.lbl_JobIDValue.Size = new System.Drawing.Size(583, 24);
            this.lbl_JobIDValue.TabIndex = 13;
            this.toolTip1.SetToolTip(this.lbl_JobIDValue, "Lavy dvojklik pre zmenu Job ID.");
            this.lbl_JobIDValue.TextChanged += new System.EventHandler(this.lbl_JobIDValue_TextChanged);
            this.lbl_JobIDValue.DoubleClick += new System.EventHandler(this.lbl_JobIDValue_DoubleClick);
            // 
            // pb_SerialNumber
            // 
            this.pb_SerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_SerialNumber.Location = new System.Drawing.Point(714, 125);
            this.pb_SerialNumber.Name = "pb_SerialNumber";
            this.pb_SerialNumber.Size = new System.Drawing.Size(26, 26);
            this.pb_SerialNumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_SerialNumber.TabIndex = 12;
            this.pb_SerialNumber.TabStop = false;
            // 
            // pB_JobID
            // 
            this.pB_JobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pB_JobID.Location = new System.Drawing.Point(714, 60);
            this.pB_JobID.Name = "pB_JobID";
            this.pB_JobID.Size = new System.Drawing.Size(26, 26);
            this.pB_JobID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_JobID.TabIndex = 11;
            this.pB_JobID.TabStop = false;
            // 
            // pB_Assembly
            // 
            this.pB_Assembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pB_Assembly.Location = new System.Drawing.Point(714, 27);
            this.pB_Assembly.Name = "pB_Assembly";
            this.pB_Assembly.Size = new System.Drawing.Size(26, 26);
            this.pB_Assembly.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_Assembly.TabIndex = 10;
            this.pB_Assembly.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Serial Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Job ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Product no:";
            this.toolTip1.SetToolTip(this.label1, "Lavy dvojklik pre resetovanie formulara.");
            this.label1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDoubleClick);
            // 
            // cb_ProductNo
            // 
            this.cb_ProductNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_ProductNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ProductNo.FormattingEnabled = true;
            this.cb_ProductNo.Location = new System.Drawing.Point(125, 25);
            this.cb_ProductNo.Name = "cb_ProductNo";
            this.cb_ProductNo.Size = new System.Drawing.Size(583, 28);
            this.cb_ProductNo.TabIndex = 1;
            this.cb_ProductNo.SelectedIndexChanged += new System.EventHandler(this.cb_ProductNo_SelectedIndexChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Napoveda";
            // 
            // lbl_OperatorNr
            // 
            this.lbl_OperatorNr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_OperatorNr.ContextMenuStrip = this.cms_AdminContext;
            this.lbl_OperatorNr.Location = new System.Drawing.Point(108, 18);
            this.lbl_OperatorNr.Name = "lbl_OperatorNr";
            this.lbl_OperatorNr.Size = new System.Drawing.Size(91, 24);
            this.lbl_OperatorNr.TabIndex = 14;
            this.toolTip1.SetToolTip(this.lbl_OperatorNr, "Lavy dvojklik pre zmenu operatora.");
            this.lbl_OperatorNr.DoubleClick += new System.EventHandler(this.lbl_OperatorNr_DoubleClick);
            // 
            // cms_AdminContext
            // 
            this.cms_AdminContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyStationConfigToolStripMenuItem,
            this.copyPublishedStationConfigToolStripMenuItem,
            this.userMaintenanceToolStripMenuItem,
            this.deleteOperatorToolStripMenuItem,
            this.openChangeLogToolStripMenuItem});
            this.cms_AdminContext.Name = "cms_AdminContext";
            this.cms_AdminContext.Size = new System.Drawing.Size(235, 114);
            // 
            // modifyStationConfigToolStripMenuItem
            // 
            this.modifyStationConfigToolStripMenuItem.Name = "modifyStationConfigToolStripMenuItem";
            this.modifyStationConfigToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.modifyStationConfigToolStripMenuItem.Text = "Modify StationConfig";
            this.modifyStationConfigToolStripMenuItem.Click += new System.EventHandler(this.modifyStationConfigToolStripMenuItem_Click);
            // 
            // copyPublishedStationConfigToolStripMenuItem
            // 
            this.copyPublishedStationConfigToolStripMenuItem.Name = "copyPublishedStationConfigToolStripMenuItem";
            this.copyPublishedStationConfigToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.copyPublishedStationConfigToolStripMenuItem.Text = "Open published StationConfig";
            this.copyPublishedStationConfigToolStripMenuItem.Click += new System.EventHandler(this.copyPublishedStationConfigToolStripMenuItem_Click);
            // 
            // userMaintenanceToolStripMenuItem
            // 
            this.userMaintenanceToolStripMenuItem.Name = "userMaintenanceToolStripMenuItem";
            this.userMaintenanceToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.userMaintenanceToolStripMenuItem.Text = "Operators Maintenance";
            this.userMaintenanceToolStripMenuItem.Click += new System.EventHandler(this.userMaintenanceToolStripMenuItem_Click);
            // 
            // deleteOperatorToolStripMenuItem
            // 
            this.deleteOperatorToolStripMenuItem.Name = "deleteOperatorToolStripMenuItem";
            this.deleteOperatorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.deleteOperatorToolStripMenuItem.Text = "Delete Operator";
            this.deleteOperatorToolStripMenuItem.Click += new System.EventHandler(this.deleteOperatorToolStripMenuItem_Click);
            // 
            // openChangeLogToolStripMenuItem
            // 
            this.openChangeLogToolStripMenuItem.Name = "openChangeLogToolStripMenuItem";
            this.openChangeLogToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.openChangeLogToolStripMenuItem.Text = "Open ChangeLog";
            this.openChangeLogToolStripMenuItem.Click += new System.EventHandler(this.openChangeLogToolStripMenuItem_Click);
            // 
            // lbl_OperatorSurname
            // 
            this.lbl_OperatorSurname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_OperatorSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_OperatorSurname.Location = new System.Drawing.Point(84, 49);
            this.lbl_OperatorSurname.Name = "lbl_OperatorSurname";
            this.lbl_OperatorSurname.Size = new System.Drawing.Size(115, 23);
            this.lbl_OperatorSurname.TabIndex = 16;
            this.lbl_OperatorSurname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lbl_OperatorSurname, "Lavy dvojklik pre zmenu Job ID.");
            this.lbl_OperatorSurname.TextChanged += new System.EventHandler(this.lbl_OperatorSurname_TextChanged);
            // 
            // gb_Results
            // 
            this.gb_Results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Results.ContextMenuStrip = this.contextMenuStrip1;
            this.gb_Results.Controls.Add(this.dgv_Instructions);
            this.gb_Results.Enabled = false;
            this.gb_Results.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gb_Results.Location = new System.Drawing.Point(1, 1);
            this.gb_Results.Name = "gb_Results";
            this.gb_Results.Size = new System.Drawing.Size(460, 197);
            this.gb_Results.TabIndex = 3;
            this.gb_Results.TabStop = false;
            this.gb_Results.Text = "Vysledky";
            this.toolTip1.SetToolTip(this.gb_Results, "Pravy klik vyvola ponuku");
            this.gb_Results.Enter += new System.EventHandler(this.gb_Results_Enter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableResultsFieldToolStripMenuItem,
            this.disableResultsFieldToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(179, 48);
            this.toolTip1.SetToolTip(this.contextMenuStrip1, "Napoveda");
            // 
            // enableResultsFieldToolStripMenuItem
            // 
            this.enableResultsFieldToolStripMenuItem.Name = "enableResultsFieldToolStripMenuItem";
            this.enableResultsFieldToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.enableResultsFieldToolStripMenuItem.Text = "Enable Results field";
            this.enableResultsFieldToolStripMenuItem.Click += new System.EventHandler(this.enableResultsFieldToolStripMenuItem_Click);
            // 
            // disableResultsFieldToolStripMenuItem
            // 
            this.disableResultsFieldToolStripMenuItem.Name = "disableResultsFieldToolStripMenuItem";
            this.disableResultsFieldToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.disableResultsFieldToolStripMenuItem.Text = "Disable Results field";
            this.disableResultsFieldToolStripMenuItem.Visible = false;
            this.disableResultsFieldToolStripMenuItem.Click += new System.EventHandler(this.disableResultsFieldToolStripMenuItem_Click);
            // 
            // dgv_Instructions
            // 
            this.dgv_Instructions.AllowUserToAddRows = false;
            this.dgv_Instructions.AllowUserToDeleteRows = false;
            this.dgv_Instructions.AllowUserToResizeRows = false;
            this.dgv_Instructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Instructions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Instructions.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv_Instructions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Instructions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_Name,
            this.c_Instructions,
            this.c_Result,
            this.c_PassRes,
            this.c_FailRes,
            this.c_Comment});
            this.dgv_Instructions.Enabled = false;
            this.dgv_Instructions.Location = new System.Drawing.Point(7, 23);
            this.dgv_Instructions.MultiSelect = false;
            this.dgv_Instructions.Name = "dgv_Instructions";
            this.dgv_Instructions.RowHeadersVisible = false;
            this.dgv_Instructions.Size = new System.Drawing.Size(446, 165);
            this.dgv_Instructions.TabIndex = 0;
            this.dgv_Instructions.Visible = false;
            this.dgv_Instructions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Instructions_CellClick);
            this.dgv_Instructions.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Instructions_CellMouseEnter);
            // 
            // c_Name
            // 
            this.c_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Name.HeaderText = "Por.c.";
            this.c_Name.Name = "c_Name";
            this.c_Name.ReadOnly = true;
            this.c_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Name.Width = 61;
            // 
            // c_Instructions
            // 
            this.c_Instructions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.c_Instructions.DefaultCellStyle = dataGridViewCellStyle1;
            this.c_Instructions.HeaderText = "Instrukcie/Postup";
            this.c_Instructions.Name = "c_Instructions";
            this.c_Instructions.ReadOnly = true;
            this.c_Instructions.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Instructions.Width = 155;
            // 
            // c_Result
            // 
            this.c_Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Result.HeaderText = "Vysledok";
            this.c_Result.Name = "c_Result";
            this.c_Result.ReadOnly = true;
            this.c_Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Result.Width = 87;
            // 
            // c_PassRes
            // 
            this.c_PassRes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_PassRes.HeaderText = "PASS";
            this.c_PassRes.Name = "c_PassRes";
            this.c_PassRes.ReadOnly = true;
            this.c_PassRes.Width = 62;
            // 
            // c_FailRes
            // 
            this.c_FailRes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_FailRes.HeaderText = "FAIL";
            this.c_FailRes.Name = "c_FailRes";
            this.c_FailRes.ReadOnly = true;
            this.c_FailRes.Width = 54;
            // 
            // c_Comment
            // 
            this.c_Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.c_Comment.HeaderText = "Poznamka";
            this.c_Comment.Name = "c_Comment";
            this.c_Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.c_Comment.Width = 98;
            // 
            // btn_BatchMode
            // 
            this.btn_BatchMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_BatchMode.Enabled = false;
            this.btn_BatchMode.Location = new System.Drawing.Point(866, 26);
            this.btn_BatchMode.Name = "btn_BatchMode";
            this.btn_BatchMode.Size = new System.Drawing.Size(85, 50);
            this.btn_BatchMode.TabIndex = 5;
            this.btn_BatchMode.Text = "Batch Mod";
            this.toolTip1.SetToolTip(this.btn_BatchMode, "Hromadne zadavanie seriovych cisiel s PASS vysledkom.");
            this.btn_BatchMode.UseVisualStyleBackColor = true;
            this.btn_BatchMode.EnabledChanged += new System.EventHandler(this.btn_BatchMode_EnabledChanged);
            this.btn_BatchMode.Click += new System.EventHandler(this.btn_BatchMode_Click);
            // 
            // lbl_StationName
            // 
            this.lbl_StationName.ContextMenuStrip = this.cms_AdminContext;
            this.lbl_StationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_StationName.Location = new System.Drawing.Point(11, 24);
            this.lbl_StationName.Name = "lbl_StationName";
            this.lbl_StationName.Size = new System.Drawing.Size(185, 19);
            this.lbl_StationName.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_AvailableSettings);
            this.groupBox2.Controls.Add(this.btn_PasswordChange);
            this.groupBox2.Controls.Add(this.lbl_OperatorSurname);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lbl_OperatorNr);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 107);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operator";
            // 
            // btn_AvailableSettings
            // 
            this.btn_AvailableSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_AvailableSettings.Location = new System.Drawing.Point(8, 79);
            this.btn_AvailableSettings.Name = "btn_AvailableSettings";
            this.btn_AvailableSettings.Size = new System.Drawing.Size(91, 23);
            this.btn_AvailableSettings.TabIndex = 17;
            this.btn_AvailableSettings.Text = "Nastavenia";
            this.btn_AvailableSettings.UseVisualStyleBackColor = true;
            this.btn_AvailableSettings.Click += new System.EventHandler(this.btn_AvailableSettings_Click);
            // 
            // btn_PasswordChange
            // 
            this.btn_PasswordChange.Enabled = false;
            this.btn_PasswordChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_PasswordChange.Location = new System.Drawing.Point(108, 79);
            this.btn_PasswordChange.Name = "btn_PasswordChange";
            this.btn_PasswordChange.Size = new System.Drawing.Size(91, 23);
            this.btn_PasswordChange.TabIndex = 5;
            this.btn_PasswordChange.Text = "Zmena hesla";
            this.btn_PasswordChange.UseVisualStyleBackColor = true;
            this.btn_PasswordChange.Click += new System.EventHandler(this.btn_PasswordChange_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Priezvisko:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Osobné číslo:";
            // 
            // gb_Barcodes
            // 
            this.gb_Barcodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_Barcodes.Controls.Add(this.btn_BatchMode);
            this.gb_Barcodes.Controls.Add(this.btn_GenerateReport);
            this.gb_Barcodes.Controls.Add(this.tb_OrderValue);
            this.gb_Barcodes.Controls.Add(this.lbl_ScanEditOrder);
            this.gb_Barcodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gb_Barcodes.Location = new System.Drawing.Point(12, 179);
            this.gb_Barcodes.Name = "gb_Barcodes";
            this.gb_Barcodes.Size = new System.Drawing.Size(959, 87);
            this.gb_Barcodes.TabIndex = 0;
            this.gb_Barcodes.TabStop = false;
            this.gb_Barcodes.Text = "Scan/Edit pole:";
            // 
            // btn_GenerateReport
            // 
            this.btn_GenerateReport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_GenerateReport.Enabled = false;
            this.btn_GenerateReport.Location = new System.Drawing.Point(775, 26);
            this.btn_GenerateReport.Name = "btn_GenerateReport";
            this.btn_GenerateReport.Size = new System.Drawing.Size(85, 50);
            this.btn_GenerateReport.TabIndex = 4;
            this.btn_GenerateReport.Text = "Vytvor Report";
            this.btn_GenerateReport.UseVisualStyleBackColor = true;
            this.btn_GenerateReport.Click += new System.EventHandler(this.btn_GenerateReport_Click);
            // 
            // tb_OrderValue
            // 
            this.tb_OrderValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_OrderValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tb_OrderValue.Location = new System.Drawing.Point(10, 49);
            this.tb_OrderValue.Name = "tb_OrderValue";
            this.tb_OrderValue.Size = new System.Drawing.Size(759, 26);
            this.tb_OrderValue.TabIndex = 0;
            this.tb_OrderValue.Enter += new System.EventHandler(this.tb_OrderValue_Enter);
            this.tb_OrderValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_OrderValue_KeyUp);
            // 
            // lbl_ScanEditOrder
            // 
            this.lbl_ScanEditOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ScanEditOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_ScanEditOrder.ForeColor = System.Drawing.Color.Red;
            this.lbl_ScanEditOrder.Location = new System.Drawing.Point(6, 26);
            this.lbl_ScanEditOrder.Name = "lbl_ScanEditOrder";
            this.lbl_ScanEditOrder.Size = new System.Drawing.Size(763, 20);
            this.lbl_ScanEditOrder.TabIndex = 0;
            this.lbl_ScanEditOrder.Text = "Zoskenujte 2D kod na vyrobku alebo zadajte nazov produktu alebo vyberte produkt z" +
    " ponuky:";
            this.lbl_ScanEditOrder.TextChanged += new System.EventHandler(this.lbl_ScanEditOrder_TextChanged);
            // 
            // pb_BarcodesToScan
            // 
            this.pb_BarcodesToScan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_BarcodesToScan.Location = new System.Drawing.Point(-1, -1);
            this.pb_BarcodesToScan.Name = "pb_BarcodesToScan";
            this.pb_BarcodesToScan.Size = new System.Drawing.Size(490, 199);
            this.pb_BarcodesToScan.TabIndex = 4;
            this.pb_BarcodesToScan.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_StationName);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 53);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Station";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(12, 272);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gb_Results);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gb_FaultCodes);
            this.splitContainer1.Panel2.Controls.Add(this.pb_BarcodesToScan);
            this.splitContainer1.Size = new System.Drawing.Size(959, 201);
            this.splitContainer1.SplitterDistance = 464;
            this.splitContainer1.TabIndex = 19;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // gb_FaultCodes
            // 
            this.gb_FaultCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_FaultCodes.Controls.Add(this.btn_FaultCodes_AddFailure);
            this.gb_FaultCodes.Controls.Add(this.cb_FaultCodes_Description);
            this.gb_FaultCodes.Controls.Add(this.label8);
            this.gb_FaultCodes.Controls.Add(this.cb_FaultCodes_Code);
            this.gb_FaultCodes.Controls.Add(this.label5);
            this.gb_FaultCodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gb_FaultCodes.Location = new System.Drawing.Point(4, 3);
            this.gb_FaultCodes.Name = "gb_FaultCodes";
            this.gb_FaultCodes.Size = new System.Drawing.Size(481, 86);
            this.gb_FaultCodes.TabIndex = 5;
            this.gb_FaultCodes.TabStop = false;
            this.gb_FaultCodes.Text = "Kodovnik chyb";
            this.gb_FaultCodes.Visible = false;
            // 
            // btn_FaultCodes_AddFailure
            // 
            this.btn_FaultCodes_AddFailure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_FaultCodes_AddFailure.Enabled = false;
            this.btn_FaultCodes_AddFailure.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_FaultCodes_AddFailure.Location = new System.Drawing.Point(392, 25);
            this.btn_FaultCodes_AddFailure.Name = "btn_FaultCodes_AddFailure";
            this.btn_FaultCodes_AddFailure.Size = new System.Drawing.Size(83, 47);
            this.btn_FaultCodes_AddFailure.TabIndex = 4;
            this.btn_FaultCodes_AddFailure.Text = "Pridaj chybu";
            this.btn_FaultCodes_AddFailure.UseVisualStyleBackColor = true;
            this.btn_FaultCodes_AddFailure.Click += new System.EventHandler(this.btn_FaultCodes_AddFailure_Click);
            // 
            // cb_FaultCodes_Description
            // 
            this.cb_FaultCodes_Description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_FaultCodes_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cb_FaultCodes_Description.FormattingEnabled = true;
            this.cb_FaultCodes_Description.Location = new System.Drawing.Point(138, 46);
            this.cb_FaultCodes_Description.MinimumSize = new System.Drawing.Size(40, 0);
            this.cb_FaultCodes_Description.Name = "cb_FaultCodes_Description";
            this.cb_FaultCodes_Description.Size = new System.Drawing.Size(248, 26);
            this.cb_FaultCodes_Description.TabIndex = 3;
            this.cb_FaultCodes_Description.SelectedIndexChanged += new System.EventHandler(this.cb_FaultCodes_Description_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(135, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "Popis chyby";
            // 
            // cb_FaultCodes_Code
            // 
            this.cb_FaultCodes_Code.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cb_FaultCodes_Code.FormattingEnabled = true;
            this.cb_FaultCodes_Code.Location = new System.Drawing.Point(11, 46);
            this.cb_FaultCodes_Code.Name = "cb_FaultCodes_Code";
            this.cb_FaultCodes_Code.Size = new System.Drawing.Size(121, 26);
            this.cb_FaultCodes_Code.TabIndex = 1;
            this.cb_FaultCodes_Code.SelectedIndexChanged += new System.EventHandler(this.cb_FaultCodes_Code_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(8, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Kod chyby";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 485);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gb_Barcodes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_Product);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SigmaSure Manual Report Generator v";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gb_Product.ResumeLayout(false);
            this.gb_Product.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_TestType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_SerialNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_JobID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Assembly)).EndInit();
            this.cms_AdminContext.ResumeLayout(false);
            this.gb_Results.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Instructions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_Barcodes.ResumeLayout(false);
            this.gb_Barcodes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BarcodesToScan)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gb_FaultCodes.ResumeLayout(false);
            this.gb_FaultCodes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Product;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_ProductNo;
        private System.Windows.Forms.PictureBox pB_JobID;
        private System.Windows.Forms.PictureBox pB_Assembly;
        private System.Windows.Forms.PictureBox pb_SerialNumber;
        private System.Windows.Forms.Label lbl_SerialNumber;
        private System.Windows.Forms.Label lbl_JobIDValue;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_OperatorSurname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_OperatorNr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_Barcodes;
        private System.Windows.Forms.TextBox tb_OrderValue;
        private System.Windows.Forms.Label lbl_ScanEditOrder;
        private System.Windows.Forms.GroupBox gb_Results;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem enableResultsFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableResultsFieldToolStripMenuItem;
        private System.Windows.Forms.PictureBox pb_TestType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_TestType;
        private System.Windows.Forms.ContextMenuStrip cms_AdminContext;
        private System.Windows.Forms.ToolStripMenuItem modifyStationConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPublishedStationConfigToolStripMenuItem;
        private System.Windows.Forms.Button btn_GenerateReport;
        private System.Windows.Forms.Button btn_PasswordChange;
        private System.Windows.Forms.ToolStripMenuItem userMaintenanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteOperatorToolStripMenuItem;
        private System.Windows.Forms.PictureBox pb_BarcodesToScan;
        private System.Windows.Forms.ToolStripMenuItem openChangeLogToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb_LastSerialNumbers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_StationName;
        private System.Windows.Forms.Button btn_BatchMode;
        private System.Windows.Forms.Button btn_AvailableSettings;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_Instructions;
        private System.Windows.Forms.GroupBox gb_FaultCodes;
        private System.Windows.Forms.Button btn_FaultCodes_AddFailure;
        private System.Windows.Forms.ComboBox cb_FaultCodes_Description;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_FaultCodes_Code;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Instructions;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Result;
        private System.Windows.Forms.DataGridViewButtonColumn c_PassRes;
        private System.Windows.Forms.DataGridViewButtonColumn c_FailRes;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_Comment;
        private System.Windows.Forms.ToolTip toolTip2;
    }
}


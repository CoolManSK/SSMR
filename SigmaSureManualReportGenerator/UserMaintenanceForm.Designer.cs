namespace SigmaSureManualReportGenerator
{
    partial class UserMaintenanceForm
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
            this.gb_AddUser = new System.Windows.Forms.GroupBox();
            this.cb_NewUserPrivileges = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_NewOperator_Save = new System.Windows.Forms.Button();
            this.tb_NewOP_number = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_NewOP_PWVer = new System.Windows.Forms.TextBox();
            this.tb_NewOP_password = new System.Windows.Forms.TextBox();
            this.tb_NewOP_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_OperatorChange = new System.Windows.Forms.GroupBox();
            this.cb_ExistingUserPrivileges = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_OldNumber = new System.Windows.Forms.ComboBox();
            this.cb_OldSurname = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OperatorChangeSave = new System.Windows.Forms.Button();
            this.tb_NewNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_NewSurname = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.gb_AddUser.SuspendLayout();
            this.gb_OperatorChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_AddUser
            // 
            this.gb_AddUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gb_AddUser.Controls.Add(this.cb_NewUserPrivileges);
            this.gb_AddUser.Controls.Add(this.label6);
            this.gb_AddUser.Controls.Add(this.btn_NewOperator_Save);
            this.gb_AddUser.Controls.Add(this.tb_NewOP_number);
            this.gb_AddUser.Controls.Add(this.label4);
            this.gb_AddUser.Controls.Add(this.tb_NewOP_PWVer);
            this.gb_AddUser.Controls.Add(this.tb_NewOP_password);
            this.gb_AddUser.Controls.Add(this.tb_NewOP_name);
            this.gb_AddUser.Controls.Add(this.label3);
            this.gb_AddUser.Controls.Add(this.label2);
            this.gb_AddUser.Controls.Add(this.label1);
            this.gb_AddUser.Location = new System.Drawing.Point(13, 14);
            this.gb_AddUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_AddUser.Name = "gb_AddUser";
            this.gb_AddUser.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_AddUser.Size = new System.Drawing.Size(306, 365);
            this.gb_AddUser.TabIndex = 0;
            this.gb_AddUser.TabStop = false;
            this.gb_AddUser.Text = "Pridanie noveho operatora";
            // 
            // cb_NewUserPrivileges
            // 
            this.cb_NewUserPrivileges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_NewUserPrivileges.FormattingEnabled = true;
            this.cb_NewUserPrivileges.Location = new System.Drawing.Point(15, 280);
            this.cb_NewUserPrivileges.Name = "cb_NewUserPrivileges";
            this.cb_NewUserPrivileges.Size = new System.Drawing.Size(277, 28);
            this.cb_NewUserPrivileges.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 259);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Privilegia:";
            // 
            // btn_NewOperator_Save
            // 
            this.btn_NewOperator_Save.Location = new System.Drawing.Point(15, 317);
            this.btn_NewOperator_Save.Name = "btn_NewOperator_Save";
            this.btn_NewOperator_Save.Size = new System.Drawing.Size(100, 37);
            this.btn_NewOperator_Save.TabIndex = 5;
            this.btn_NewOperator_Save.Text = "SAVE";
            this.btn_NewOperator_Save.UseVisualStyleBackColor = true;
            this.btn_NewOperator_Save.Click += new System.EventHandler(this.btn_NewOperator_Save_Click);
            // 
            // tb_NewOP_number
            // 
            this.tb_NewOP_number.Location = new System.Drawing.Point(15, 109);
            this.tb_NewOP_number.Name = "tb_NewOP_number";
            this.tb_NewOP_number.Size = new System.Drawing.Size(277, 26);
            this.tb_NewOP_number.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Osobne cislo:";
            // 
            // tb_NewOP_PWVer
            // 
            this.tb_NewOP_PWVer.Location = new System.Drawing.Point(15, 223);
            this.tb_NewOP_PWVer.Name = "tb_NewOP_PWVer";
            this.tb_NewOP_PWVer.PasswordChar = '*';
            this.tb_NewOP_PWVer.Size = new System.Drawing.Size(277, 26);
            this.tb_NewOP_PWVer.TabIndex = 3;
            // 
            // tb_NewOP_password
            // 
            this.tb_NewOP_password.Location = new System.Drawing.Point(15, 166);
            this.tb_NewOP_password.Name = "tb_NewOP_password";
            this.tb_NewOP_password.PasswordChar = '*';
            this.tb_NewOP_password.Size = new System.Drawing.Size(277, 26);
            this.tb_NewOP_password.TabIndex = 2;
            // 
            // tb_NewOP_name
            // 
            this.tb_NewOP_name.Location = new System.Drawing.Point(15, 52);
            this.tb_NewOP_name.Name = "tb_NewOP_name";
            this.tb_NewOP_name.Size = new System.Drawing.Size(277, 26);
            this.tb_NewOP_name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 200);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Potvrdenie hesla:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 143);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Heslo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Meno:";
            // 
            // gb_OperatorChange
            // 
            this.gb_OperatorChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gb_OperatorChange.Controls.Add(this.cb_ExistingUserPrivileges);
            this.gb_OperatorChange.Controls.Add(this.label7);
            this.gb_OperatorChange.Controls.Add(this.cb_OldNumber);
            this.gb_OperatorChange.Controls.Add(this.cb_OldSurname);
            this.gb_OperatorChange.Controls.Add(this.label10);
            this.gb_OperatorChange.Controls.Add(this.label9);
            this.gb_OperatorChange.Controls.Add(this.btn_Cancel);
            this.gb_OperatorChange.Controls.Add(this.btn_OperatorChangeSave);
            this.gb_OperatorChange.Controls.Add(this.tb_NewNumber);
            this.gb_OperatorChange.Controls.Add(this.label5);
            this.gb_OperatorChange.Controls.Add(this.tb_NewSurname);
            this.gb_OperatorChange.Controls.Add(this.label8);
            this.gb_OperatorChange.Location = new System.Drawing.Point(327, 14);
            this.gb_OperatorChange.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_OperatorChange.Name = "gb_OperatorChange";
            this.gb_OperatorChange.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gb_OperatorChange.Size = new System.Drawing.Size(306, 365);
            this.gb_OperatorChange.TabIndex = 1;
            this.gb_OperatorChange.TabStop = false;
            this.gb_OperatorChange.Text = "Zmena udajov operatora";
            // 
            // cb_ExistingUserPrivileges
            // 
            this.cb_ExistingUserPrivileges.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ExistingUserPrivileges.FormattingEnabled = true;
            this.cb_ExistingUserPrivileges.Location = new System.Drawing.Point(15, 280);
            this.cb_ExistingUserPrivileges.Name = "cb_ExistingUserPrivileges";
            this.cb_ExistingUserPrivileges.Size = new System.Drawing.Size(277, 28);
            this.cb_ExistingUserPrivileges.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 259);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Privilegia:";
            // 
            // cb_OldNumber
            // 
            this.cb_OldNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OldNumber.FormattingEnabled = true;
            this.cb_OldNumber.Location = new System.Drawing.Point(15, 166);
            this.cb_OldNumber.Name = "cb_OldNumber";
            this.cb_OldNumber.Size = new System.Drawing.Size(277, 28);
            this.cb_OldNumber.TabIndex = 8;
            this.cb_OldNumber.SelectedIndexChanged += new System.EventHandler(this.cb_OldNumber_SelectedIndexChanged);
            // 
            // cb_OldSurname
            // 
            this.cb_OldSurname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OldSurname.FormattingEnabled = true;
            this.cb_OldSurname.Location = new System.Drawing.Point(15, 52);
            this.cb_OldSurname.Name = "cb_OldSurname";
            this.cb_OldSurname.Size = new System.Drawing.Size(277, 28);
            this.cb_OldSurname.TabIndex = 6;
            this.cb_OldSurname.SelectedIndexChanged += new System.EventHandler(this.cb_OldSurname_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 200);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 20);
            this.label10.TabIndex = 10;
            this.label10.Text = "Nove osobne cislo:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 86);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Nove meno:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(192, 317);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 37);
            this.btn_Cancel.TabIndex = 12;
            this.btn_Cancel.Text = "CLOSE";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OperatorChangeSave
            // 
            this.btn_OperatorChangeSave.Location = new System.Drawing.Point(15, 317);
            this.btn_OperatorChangeSave.Name = "btn_OperatorChangeSave";
            this.btn_OperatorChangeSave.Size = new System.Drawing.Size(100, 37);
            this.btn_OperatorChangeSave.TabIndex = 11;
            this.btn_OperatorChangeSave.Text = "SAVE";
            this.btn_OperatorChangeSave.UseVisualStyleBackColor = true;
            this.btn_OperatorChangeSave.Click += new System.EventHandler(this.btn_OperatorChangeSave_Click);
            // 
            // tb_NewNumber
            // 
            this.tb_NewNumber.Location = new System.Drawing.Point(15, 223);
            this.tb_NewNumber.Name = "tb_NewNumber";
            this.tb_NewNumber.Size = new System.Drawing.Size(277, 26);
            this.tb_NewNumber.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 143);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Osobne cislo:";
            // 
            // tb_NewSurname
            // 
            this.tb_NewSurname.Location = new System.Drawing.Point(15, 109);
            this.tb_NewSurname.Name = "tb_NewSurname";
            this.tb_NewSurname.Size = new System.Drawing.Size(277, 26);
            this.tb_NewSurname.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 29);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Meno:";
            // 
            // UserMaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 393);
            this.ControlBox = false;
            this.Controls.Add(this.gb_OperatorChange);
            this.Controls.Add(this.gb_AddUser);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UserMaintenanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserMaintenanceForm";
            this.Load += new System.EventHandler(this.UserMaintenanceForm_Load);
            this.gb_AddUser.ResumeLayout(false);
            this.gb_AddUser.PerformLayout();
            this.gb_OperatorChange.ResumeLayout(false);
            this.gb_OperatorChange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_AddUser;
        private System.Windows.Forms.TextBox tb_NewOP_number;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_NewOP_PWVer;
        private System.Windows.Forms.TextBox tb_NewOP_password;
        private System.Windows.Forms.TextBox tb_NewOP_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_NewOperator_Save;
        private System.Windows.Forms.GroupBox gb_OperatorChange;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OperatorChangeSave;
        private System.Windows.Forms.TextBox tb_NewNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_NewSurname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_OldNumber;
        private System.Windows.Forms.ComboBox cb_OldSurname;
        private System.Windows.Forms.ComboBox cb_NewUserPrivileges;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_ExistingUserPrivileges;
        private System.Windows.Forms.Label label7;
    }
}
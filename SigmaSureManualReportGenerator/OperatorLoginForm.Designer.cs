namespace SigmaSureManualReportGenerator
{
    partial class OperatorLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorLoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_OperatorLoginNr = new System.Windows.Forms.ComboBox();
            this.tb_OperatorLoginPassword = new System.Windows.Forms.TextBox();
            this.btn_OperatorLoginOK = new System.Windows.Forms.Button();
            this.btn_OperatorLoginCancel = new System.Windows.Forms.Button();
            this.tb_OperatorScanField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_OperatorSurname = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(14, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Osobne cislo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(14, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Heslo:";
            // 
            // cb_OperatorLoginNr
            // 
            this.cb_OperatorLoginNr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OperatorLoginNr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_OperatorLoginNr.FormattingEnabled = true;
            this.cb_OperatorLoginNr.Location = new System.Drawing.Point(124, 78);
            this.cb_OperatorLoginNr.Name = "cb_OperatorLoginNr";
            this.cb_OperatorLoginNr.Size = new System.Drawing.Size(158, 28);
            this.cb_OperatorLoginNr.TabIndex = 2;
            this.cb_OperatorLoginNr.SelectedIndexChanged += new System.EventHandler(this.cb_OperatorLoginNr_SelectedIndexChanged);
            // 
            // tb_OperatorLoginPassword
            // 
            this.tb_OperatorLoginPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_OperatorLoginPassword.Location = new System.Drawing.Point(74, 114);
            this.tb_OperatorLoginPassword.Name = "tb_OperatorLoginPassword";
            this.tb_OperatorLoginPassword.PasswordChar = '*';
            this.tb_OperatorLoginPassword.Size = new System.Drawing.Size(208, 26);
            this.tb_OperatorLoginPassword.TabIndex = 3;
            this.tb_OperatorLoginPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_OperatorLoginPassword_KeyUp);
            // 
            // btn_OperatorLoginOK
            // 
            this.btn_OperatorLoginOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_OperatorLoginOK.Location = new System.Drawing.Point(18, 151);
            this.btn_OperatorLoginOK.Name = "btn_OperatorLoginOK";
            this.btn_OperatorLoginOK.Size = new System.Drawing.Size(114, 30);
            this.btn_OperatorLoginOK.TabIndex = 4;
            this.btn_OperatorLoginOK.Text = "OK";
            this.btn_OperatorLoginOK.UseVisualStyleBackColor = true;
            this.btn_OperatorLoginOK.Click += new System.EventHandler(this.btn_OperatorLoginOK_Click);
            // 
            // btn_OperatorLoginCancel
            // 
            this.btn_OperatorLoginCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_OperatorLoginCancel.Location = new System.Drawing.Point(175, 151);
            this.btn_OperatorLoginCancel.Name = "btn_OperatorLoginCancel";
            this.btn_OperatorLoginCancel.Size = new System.Drawing.Size(107, 30);
            this.btn_OperatorLoginCancel.TabIndex = 5;
            this.btn_OperatorLoginCancel.Text = "CANCEL";
            this.btn_OperatorLoginCancel.UseVisualStyleBackColor = true;
            this.btn_OperatorLoginCancel.Click += new System.EventHandler(this.btn_OperatorLoginCancel_Click);
            // 
            // tb_OperatorScanField
            // 
            this.tb_OperatorScanField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tb_OperatorScanField.Location = new System.Drawing.Point(140, 12);
            this.tb_OperatorScanField.Name = "tb_OperatorScanField";
            this.tb_OperatorScanField.Size = new System.Drawing.Size(141, 26);
            this.tb_OperatorScanField.TabIndex = 0;
            this.tb_OperatorScanField.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(13, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Scan z ID karty:";
            // 
            // cb_OperatorSurname
            // 
            this.cb_OperatorSurname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OperatorSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cb_OperatorSurname.FormattingEnabled = true;
            this.cb_OperatorSurname.Location = new System.Drawing.Point(124, 44);
            this.cb_OperatorSurname.Name = "cb_OperatorSurname";
            this.cb_OperatorSurname.Size = new System.Drawing.Size(158, 28);
            this.cb_OperatorSurname.TabIndex = 1;
            this.cb_OperatorSurname.SelectedIndexChanged += new System.EventHandler(this.cb_OperatorSurname_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(14, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Priezvisko:";
            // 
            // OperatorLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 192);
            this.ControlBox = false;
            this.Controls.Add(this.cb_OperatorSurname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_OperatorScanField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_OperatorLoginCancel);
            this.Controls.Add(this.btn_OperatorLoginOK);
            this.Controls.Add(this.tb_OperatorLoginPassword);
            this.Controls.Add(this.cb_OperatorLoginNr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OperatorLoginForm";
            this.Text = "Prihlasenie operatora";
            this.Load += new System.EventHandler(this.OperatorLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_OperatorLoginNr;
        private System.Windows.Forms.TextBox tb_OperatorLoginPassword;
        private System.Windows.Forms.Button btn_OperatorLoginOK;
        private System.Windows.Forms.Button btn_OperatorLoginCancel;
        private System.Windows.Forms.TextBox tb_OperatorScanField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_OperatorSurname;
        private System.Windows.Forms.Label label4;
    }
}
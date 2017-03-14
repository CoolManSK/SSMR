namespace SigmaSureManualReportGenerator
{
    partial class PasswordChangeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordChangeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Operator = new System.Windows.Forms.Label();
            this.tb_NewPWVer = new System.Windows.Forms.TextBox();
            this.tb_NewPW = new System.Windows.Forms.TextBox();
            this.tb_OldPW = new System.Windows.Forms.TextBox();
            this.btn_SAVE = new System.Windows.Forms.Button();
            this.btn_CANCEL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operator:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stare heslo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nove heslo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 92);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Potvrdit nove heslo:";
            // 
            // lbl_Operator
            // 
            this.lbl_Operator.AutoSize = true;
            this.lbl_Operator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Operator.Location = new System.Drawing.Point(162, 9);
            this.lbl_Operator.Name = "lbl_Operator";
            this.lbl_Operator.Size = new System.Drawing.Size(53, 22);
            this.lbl_Operator.TabIndex = 4;
            this.lbl_Operator.Text = "label5";
            // 
            // tb_NewPWVer
            // 
            this.tb_NewPWVer.Location = new System.Drawing.Point(162, 89);
            this.tb_NewPWVer.Name = "tb_NewPWVer";
            this.tb_NewPWVer.PasswordChar = '*';
            this.tb_NewPWVer.Size = new System.Drawing.Size(224, 26);
            this.tb_NewPWVer.TabIndex = 2;
            this.tb_NewPWVer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_NewPWVer_KeyUp);
            // 
            // tb_NewPW
            // 
            this.tb_NewPW.Location = new System.Drawing.Point(162, 61);
            this.tb_NewPW.Name = "tb_NewPW";
            this.tb_NewPW.PasswordChar = '*';
            this.tb_NewPW.Size = new System.Drawing.Size(224, 26);
            this.tb_NewPW.TabIndex = 1;
            // 
            // tb_OldPW
            // 
            this.tb_OldPW.Location = new System.Drawing.Point(162, 33);
            this.tb_OldPW.Name = "tb_OldPW";
            this.tb_OldPW.PasswordChar = '*';
            this.tb_OldPW.Size = new System.Drawing.Size(224, 26);
            this.tb_OldPW.TabIndex = 0;
            // 
            // btn_SAVE
            // 
            this.btn_SAVE.Location = new System.Drawing.Point(113, 121);
            this.btn_SAVE.Name = "btn_SAVE";
            this.btn_SAVE.Size = new System.Drawing.Size(85, 33);
            this.btn_SAVE.TabIndex = 3;
            this.btn_SAVE.Text = "SAVE";
            this.btn_SAVE.UseVisualStyleBackColor = true;
            this.btn_SAVE.Click += new System.EventHandler(this.btn_SAVE_Click);
            // 
            // btn_CANCEL
            // 
            this.btn_CANCEL.Location = new System.Drawing.Point(206, 121);
            this.btn_CANCEL.Name = "btn_CANCEL";
            this.btn_CANCEL.Size = new System.Drawing.Size(85, 33);
            this.btn_CANCEL.TabIndex = 4;
            this.btn_CANCEL.Text = "CLOSE";
            this.btn_CANCEL.UseVisualStyleBackColor = true;
            this.btn_CANCEL.Click += new System.EventHandler(this.btn_CANCEL_Click);
            // 
            // PasswordChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 162);
            this.ControlBox = false;
            this.Controls.Add(this.btn_CANCEL);
            this.Controls.Add(this.btn_SAVE);
            this.Controls.Add(this.tb_OldPW);
            this.Controls.Add(this.tb_NewPW);
            this.Controls.Add(this.tb_NewPWVer);
            this.Controls.Add(this.lbl_Operator);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PasswordChangeForm";
            this.Text = "Zmena hesla";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Operator;
        private System.Windows.Forms.TextBox tb_NewPWVer;
        private System.Windows.Forms.TextBox tb_NewPW;
        private System.Windows.Forms.TextBox tb_OldPW;
        private System.Windows.Forms.Button btn_SAVE;
        private System.Windows.Forms.Button btn_CANCEL;
    }
}
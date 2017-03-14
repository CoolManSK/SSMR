namespace SigmaSureManualReportGenerator
{
    partial class OperatorSettingsForm
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
            this.cb_OperatorSurname = new System.Windows.Forms.ComboBox();
            this.btn_DeleteUser = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_OperatorNumber = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operator";
            // 
            // cb_OperatorSurname
            // 
            this.cb_OperatorSurname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OperatorSurname.FormattingEnabled = true;
            this.cb_OperatorSurname.Location = new System.Drawing.Point(16, 59);
            this.cb_OperatorSurname.Name = "cb_OperatorSurname";
            this.cb_OperatorSurname.Size = new System.Drawing.Size(230, 28);
            this.cb_OperatorSurname.TabIndex = 1;
            this.cb_OperatorSurname.SelectedIndexChanged += new System.EventHandler(this.cb_OperatorSurname_SelectedIndexChanged);
            // 
            // btn_DeleteUser
            // 
            this.btn_DeleteUser.Location = new System.Drawing.Point(15, 151);
            this.btn_DeleteUser.Name = "btn_DeleteUser";
            this.btn_DeleteUser.Size = new System.Drawing.Size(86, 35);
            this.btn_DeleteUser.TabIndex = 2;
            this.btn_DeleteUser.Text = "DELETE";
            this.btn_DeleteUser.UseVisualStyleBackColor = true;
            this.btn_DeleteUser.Click += new System.EventHandler(this.btn_DeleteUser_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(159, 151);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(86, 35);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "CLOSE";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Meno:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Osobne cislo:";
            // 
            // cb_OperatorNumber
            // 
            this.cb_OperatorNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_OperatorNumber.FormattingEnabled = true;
            this.cb_OperatorNumber.Location = new System.Drawing.Point(16, 115);
            this.cb_OperatorNumber.Name = "cb_OperatorNumber";
            this.cb_OperatorNumber.Size = new System.Drawing.Size(230, 28);
            this.cb_OperatorNumber.TabIndex = 5;
            this.cb_OperatorNumber.SelectedIndexChanged += new System.EventHandler(this.cb_OperatorNumber_SelectedIndexChanged);
            // 
            // DeleteOperatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 200);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_OperatorNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_DeleteUser);
            this.Controls.Add(this.cb_OperatorSurname);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DeleteOperatorForm";
            this.Text = "DeleteOperatorForm";
            this.Load += new System.EventHandler(this.DeleteOperatorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_OperatorSurname;
        private System.Windows.Forms.Button btn_DeleteUser;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_OperatorNumber;
    }
}
namespace SigmaSureManualReportGenerator
{
    partial class frm_Instructions
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
            this.lbl_Informations = new System.Windows.Forms.Label();
            this.btnPASS = new System.Windows.Forms.Button();
            this.btnFAIL = new System.Windows.Forms.Button();
            this.btnTERMINATE = new System.Windows.Forms.Button();            
            this.SuspendLayout();
            // 
            // lbl_Informations
            // 
            this.lbl_Informations.AutoSize = true;
            this.lbl_Informations.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Informations.Location = new System.Drawing.Point(6, 4);
            this.lbl_Informations.Name = "lbl_Informations";
            this.lbl_Informations.Size = new System.Drawing.Size(86, 31);
            this.lbl_Informations.TabIndex = 0;
            this.lbl_Informations.Text = "label1";
            // 
            // btnPASS
            // 
            this.btnPASS.BackColor = System.Drawing.Color.Lime;
            this.btnPASS.Enabled = false;
            this.btnPASS.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPASS.Location = new System.Drawing.Point(12, 50);
            this.btnPASS.Name = "btnPASS";
            this.btnPASS.Size = new System.Drawing.Size(295, 63);
            this.btnPASS.TabIndex = 1;
            this.btnPASS.Text = "PASSED";
            this.btnPASS.UseVisualStyleBackColor = false;
            // 
            // btnFAIL
            // 
            this.btnFAIL.BackColor = System.Drawing.Color.Red;
            this.btnFAIL.Enabled = false;
            this.btnFAIL.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFAIL.Location = new System.Drawing.Point(313, 50);
            this.btnFAIL.Name = "btnFAIL";
            this.btnFAIL.Size = new System.Drawing.Size(295, 63);
            this.btnFAIL.TabIndex = 2;
            this.btnFAIL.Text = "FAILED";
            this.btnFAIL.UseVisualStyleBackColor = false;
            // 
            // btnTERMINATE
            // 
            this.btnTERMINATE.BackColor = System.Drawing.Color.Blue;
            this.btnTERMINATE.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTERMINATE.Location = new System.Drawing.Point(614, 50);
            this.btnTERMINATE.Name = "btnTERMINATE";
            this.btnTERMINATE.Size = new System.Drawing.Size(342, 63);
            this.btnTERMINATE.TabIndex = 3;
            this.btnTERMINATE.Text = "TERMINATED";
            this.btnTERMINATE.UseVisualStyleBackColor = false;
            this.btnTERMINATE.Click += new System.EventHandler(this.btnTERMINATE_Click);
            // 
            // frm_Instructions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 592);
            this.ControlBox = false;
            this.Controls.Add(this.btnTERMINATE);
            this.Controls.Add(this.btnFAIL);
            this.Controls.Add(this.btnPASS);
            this.Controls.Add(this.lbl_Informations);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Instructions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Instructions";
            this.TopMost = false;
            this.Load += new System.EventHandler(this.frm_Instructions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Informations;
        public DGV_Instructions dgv_Instructions;
        private System.Windows.Forms.Button btnPASS;
        private System.Windows.Forms.Button btnFAIL;
        private System.Windows.Forms.Button btnTERMINATE;
    }
}
namespace SigmaSureManualReportGenerator
{
    partial class AlwaysAvailableSettingsForm
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
            this.nUD_LastSNinHystory = new System.Windows.Forms.NumericUpDown();
            this.btn_SAVE = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_HistorySNSorting = new System.Windows.Forms.ComboBox();
            this.cb_StationMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_LastSNinHystory)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pocet zobrazovanych poslednych seriovych cisel:";
            // 
            // nUD_LastSNinHystory
            // 
            this.nUD_LastSNinHystory.Location = new System.Drawing.Point(370, 7);
            this.nUD_LastSNinHystory.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_LastSNinHystory.Name = "nUD_LastSNinHystory";
            this.nUD_LastSNinHystory.Size = new System.Drawing.Size(52, 26);
            this.nUD_LastSNinHystory.TabIndex = 1;
            this.nUD_LastSNinHystory.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_LastSNinHystory.ValueChanged += new System.EventHandler(this.nUD_LastSNinHystory_ValueChanged);
            // 
            // btn_SAVE
            // 
            this.btn_SAVE.Enabled = false;
            this.btn_SAVE.Location = new System.Drawing.Point(16, 109);
            this.btn_SAVE.Name = "btn_SAVE";
            this.btn_SAVE.Size = new System.Drawing.Size(143, 35);
            this.btn_SAVE.TabIndex = 2;
            this.btn_SAVE.Text = "Uloz zmeny";
            this.btn_SAVE.UseVisualStyleBackColor = true;
            this.btn_SAVE.Click += new System.EventHandler(this.btn_SAVE_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(279, 109);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(143, 35);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "Koniec";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Zoradovat posledne seriove cisla:";
            // 
            // cb_HistorySNSorting
            // 
            this.cb_HistorySNSorting.FormattingEnabled = true;
            this.cb_HistorySNSorting.Items.AddRange(new object[] {
            "od najmensieho",
            "od najvacsieho",
            "chronologicky"});
            this.cb_HistorySNSorting.Location = new System.Drawing.Point(261, 39);
            this.cb_HistorySNSorting.Name = "cb_HistorySNSorting";
            this.cb_HistorySNSorting.Size = new System.Drawing.Size(161, 28);
            this.cb_HistorySNSorting.TabIndex = 5;
            this.cb_HistorySNSorting.SelectedIndexChanged += new System.EventHandler(this.cb_HistorySNSorting_SelectedIndexChanged);
            // 
            // cb_StationMode
            // 
            this.cb_StationMode.FormattingEnabled = true;
            this.cb_StationMode.Items.AddRange(new object[] {
            "P - Production",
            "E - Engineering (NPI area)",
            "D - Debug (odladovanie)"});
            this.cb_StationMode.Location = new System.Drawing.Point(126, 73);
            this.cb_StationMode.Name = "cb_StationMode";
            this.cb_StationMode.Size = new System.Drawing.Size(296, 28);
            this.cb_StationMode.TabIndex = 7;
            this.cb_StationMode.SelectedIndexChanged += new System.EventHandler(this.cb_StationMode_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Station Mode:";
            // 
            // AlwaysAvailableSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 154);
            this.ControlBox = false;
            this.Controls.Add(this.cb_StationMode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_HistorySNSorting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_SAVE);
            this.Controls.Add(this.nUD_LastSNinHystory);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AlwaysAvailableSettingsForm";
            this.Text = "Nastavenia";
            this.Load += new System.EventHandler(this.AlwaysAvailableSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_LastSNinHystory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUD_LastSNinHystory;
        private System.Windows.Forms.Button btn_SAVE;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_HistorySNSorting;
        private System.Windows.Forms.ComboBox cb_StationMode;
        private System.Windows.Forms.Label label3;
    }
}
namespace SigmaSureManualReportGenerator
{
    partial class frm_InstructionsOneByOne
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
            this.btn_Pass = new System.Windows.Forms.Button();
            this.btn_Fail = new System.Windows.Forms.Button();
            this.btn_Terminate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lbl_Instruction = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tb_Order = new System.Windows.Forms.TextBox();
            this.lbl_Timeout = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lbl_Operator = new System.Windows.Forms.Label();
            this.lbl_Station = new System.Windows.Forms.Label();
            this.lbl_SerialNumber = new System.Windows.Forms.Label();
            this.lbl_Item = new System.Windows.Forms.Label();
            this.t_Main = new System.Windows.Forms.Timer(this.components);
            this.lbl_InsCounter = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Pass
            // 
            this.btn_Pass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Pass.BackColor = System.Drawing.Color.Lime;
            this.btn_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Pass.Location = new System.Drawing.Point(0, 0);
            this.btn_Pass.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Pass.Name = "btn_Pass";
            this.btn_Pass.Size = new System.Drawing.Size(378, 61);
            this.btn_Pass.TabIndex = 1;
            this.btn_Pass.Text = "PASS";
            this.btn_Pass.UseVisualStyleBackColor = false;
            this.btn_Pass.Click += new System.EventHandler(this.btn_Pass_Click);
            // 
            // btn_Fail
            // 
            this.btn_Fail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Fail.BackColor = System.Drawing.Color.Red;
            this.btn_Fail.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Fail.Location = new System.Drawing.Point(0, 0);
            this.btn_Fail.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Fail.Name = "btn_Fail";
            this.btn_Fail.Size = new System.Drawing.Size(380, 61);
            this.btn_Fail.TabIndex = 2;
            this.btn_Fail.Text = "FAIL";
            this.btn_Fail.UseVisualStyleBackColor = false;
            this.btn_Fail.Click += new System.EventHandler(this.btn_Fail_Click);
            // 
            // btn_Terminate
            // 
            this.btn_Terminate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Terminate.BackColor = System.Drawing.Color.DarkViolet;
            this.btn_Terminate.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Terminate.Location = new System.Drawing.Point(0, 0);
            this.btn_Terminate.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Terminate.Name = "btn_Terminate";
            this.btn_Terminate.Size = new System.Drawing.Size(403, 61);
            this.btn_Terminate.TabIndex = 3;
            this.btn_Terminate.Text = "TERMINATE";
            this.btn_Terminate.UseVisualStyleBackColor = false;
            this.btn_Terminate.Click += new System.EventHandler(this.btn_Terminate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 556);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Pass);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1177, 61);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btn_Fail);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_Terminate);
            this.splitContainer2.Size = new System.Drawing.Size(789, 61);
            this.splitContainer2.SplitterDistance = 380;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(12, 96);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lbl_Instruction);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer3.Size = new System.Drawing.Size(1171, 400);
            this.splitContainer3.SplitterDistance = 437;
            this.splitContainer3.TabIndex = 5;
            // 
            // lbl_Instruction
            // 
            this.lbl_Instruction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Instruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Instruction.Location = new System.Drawing.Point(0, 0);
            this.lbl_Instruction.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Instruction.Name = "lbl_Instruction";
            this.lbl_Instruction.Size = new System.Drawing.Size(430, 368);
            this.lbl_Instruction.TabIndex = 7;
            this.lbl_Instruction.Text = "Instruction";
            this.lbl_Instruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(730, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tb_Order
            // 
            this.tb_Order.Location = new System.Drawing.Point(3, 3);
            this.tb_Order.Name = "tb_Order";
            this.tb_Order.Size = new System.Drawing.Size(100, 20);
            this.tb_Order.TabIndex = 8;
            this.tb_Order.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_Order_KeyUp);
            // 
            // lbl_Timeout
            // 
            this.lbl_Timeout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Timeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Timeout.Location = new System.Drawing.Point(146, 496);
            this.lbl_Timeout.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Timeout.Name = "lbl_Timeout";
            this.lbl_Timeout.Size = new System.Drawing.Size(1037, 60);
            this.lbl_Timeout.TabIndex = 6;
            this.lbl_Timeout.Text = "Timeout: 10 sec";
            this.lbl_Timeout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer4.Location = new System.Drawing.Point(20, 12);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lbl_Operator);
            this.splitContainer4.Panel1.Controls.Add(this.lbl_Station);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.lbl_SerialNumber);
            this.splitContainer4.Panel2.Controls.Add(this.lbl_Item);
            this.splitContainer4.Panel2.Controls.Add(this.tb_Order);
            this.splitContainer4.Size = new System.Drawing.Size(1163, 81);
            this.splitContainer4.SplitterDistance = 571;
            this.splitContainer4.TabIndex = 7;
            // 
            // lbl_Operator
            // 
            this.lbl_Operator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Operator.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Operator.Location = new System.Drawing.Point(0, 42);
            this.lbl_Operator.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Operator.Name = "lbl_Operator";
            this.lbl_Operator.Size = new System.Drawing.Size(562, 39);
            this.lbl_Operator.TabIndex = 9;
            this.lbl_Operator.Text = "Operator: Kolman/1805";
            this.lbl_Operator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Station
            // 
            this.lbl_Station.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Station.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Station.Location = new System.Drawing.Point(0, 0);
            this.lbl_Station.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Station.Name = "lbl_Station";
            this.lbl_Station.Size = new System.Drawing.Size(562, 39);
            this.lbl_Station.TabIndex = 8;
            this.lbl_Station.Text = "Station: MST124";
            this.lbl_Station.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SerialNumber
            // 
            this.lbl_SerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_SerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SerialNumber.Location = new System.Drawing.Point(0, 42);
            this.lbl_SerialNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_SerialNumber.Name = "lbl_SerialNumber";
            this.lbl_SerialNumber.Size = new System.Drawing.Size(579, 39);
            this.lbl_SerialNumber.TabIndex = 11;
            this.lbl_SerialNumber.Text = "SerialNumber: 1015123400001";
            this.lbl_SerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Item
            // 
            this.lbl_Item.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Item.Location = new System.Drawing.Point(0, 0);
            this.lbl_Item.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Item.Name = "lbl_Item";
            this.lbl_Item.Size = new System.Drawing.Size(579, 39);
            this.lbl_Item.TabIndex = 10;
            this.lbl_Item.Text = "Item: 350DNC40-12-8G";
            this.lbl_Item.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // t_Main
            // 
            this.t_Main.Interval = 250;
            this.t_Main.Tick += new System.EventHandler(this.t_Main_Tick);
            // 
            // lbl_InsCounter
            // 
            this.lbl_InsCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_InsCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_InsCounter.Location = new System.Drawing.Point(12, 496);
            this.lbl_InsCounter.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_InsCounter.Name = "lbl_InsCounter";
            this.lbl_InsCounter.Size = new System.Drawing.Size(130, 60);
            this.lbl_InsCounter.TabIndex = 0;
            this.lbl_InsCounter.Text = "Instruction 1/10";
            this.lbl_InsCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frm_InstructionsOneByOne
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 626);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Timeout);
            this.Controls.Add(this.splitContainer4);
            this.Controls.Add(this.lbl_InsCounter);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_InstructionsOneByOne";
            this.ShowInTaskbar = false;
            this.Text = "Instructions One by One";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_InstructionsOneByOne_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Pass;
        private System.Windows.Forms.Button btn_Fail;
        private System.Windows.Forms.Button btn_Terminate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lbl_Timeout;
        private System.Windows.Forms.Label lbl_Instruction;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lbl_Operator;
        private System.Windows.Forms.Label lbl_Station;
        private System.Windows.Forms.Label lbl_SerialNumber;
        private System.Windows.Forms.Label lbl_Item;
        private System.Windows.Forms.TextBox tb_Order;
        private System.Windows.Forms.Timer t_Main;
        private System.Windows.Forms.Label lbl_InsCounter;
    }
}
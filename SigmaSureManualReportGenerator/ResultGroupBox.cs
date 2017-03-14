using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SigmaSureManualReportGenerator
{
    partial class ResultGroupBox : GroupBox
    {
        public Label lbl_Description;
        public TextBox tb_Description;
        public Button btn_Delete;

        public ResultGroupBox(String GBName, Point Location, Size GBSize): this(GBName, "", Location, GBSize)
        {            
        }

        public ResultGroupBox(String GBName, String Description, Point Location, Size GBSize)
        {
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Location = Location;
            this.Size = GBSize;
            this.TabIndex = 0;
            this.TabStop = false;
            this.Text = GBName;
            this.Name = GBName.Replace(' ', '_');
            this.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right))));

            this.lbl_Description = new Label();
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Text = "Poznamka:";
            this.lbl_Description.Size = new Size(100, 17);
            this.lbl_Description.TextAlign = ContentAlignment.MiddleLeft;
            this.lbl_Description.Location = new Point(5, 21);
            this.lbl_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Controls.Add(this.lbl_Description);

            this.tb_Description = new TextBox();
            this.tb_Description.Name = "tb_Description";
            this.tb_Description.Size = new Size(GBSize.Width - 200, 17);
            this.tb_Description.Text = "";
            this.tb_Description.TextAlign = HorizontalAlignment.Left;
            this.tb_Description.Location = new Point(110, 20);
            this.tb_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tb_Description.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right))));
            this.Controls.Add(this.tb_Description);

            this.btn_Delete = new Button();
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Text = "Vymazat";
            this.btn_Delete.Size = new Size(80, 25);
            this.btn_Delete.TextAlign = ContentAlignment.MiddleCenter;
            this.btn_Delete.Location = new Point(this.Width - 85, 19);
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn_Delete.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Right))));
            this.Controls.Add(this.btn_Delete);
            
            if (Description == null) this.tb_Description.Text = "";
            else this.tb_Description.Text = Description;
        }
    }
}

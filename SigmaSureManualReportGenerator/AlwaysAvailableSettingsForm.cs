using System;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SigmaSureManualReportGenerator
{
    public partial class AlwaysAvailableSettingsForm : Form
    {
        public AlwaysAvailableSettingsForm(XmlDocument StationConfigXML)
        {
            InitializeComponent();
            this.XMLconfigDoc = StationConfigXML;

            this.ActualMode = this.XMLconfigDoc.SelectSingleNode("./Configuration/Mode").InnerText;

            XmlNode AllowStationModeChangeNode = XMLconfigDoc.SelectSingleNode("./Configuration/AllowStationModeChange");
            if (AllowStationModeChangeNode.InnerText == "N")
            {
                this.label3.Enabled = false;
                this.cb_StationMode.Enabled = false;
            }
            else if (AllowStationModeChangeNode.InnerText == "Y")
            {
                this.label3.Enabled = true;
                this.cb_StationMode.Enabled = true;
                
                if (this.ActualMode == "P")
                {
                    this.cb_StationMode.SelectedIndex = 0;
                }
                else if (this.ActualMode == "E")
                {
                    this.cb_StationMode.SelectedIndex = 1;
                }
                else if (this.ActualMode == "D")
                {
                    this.cb_StationMode.SelectedIndex = 2;
                }
                else
                {
                    this.cb_StationMode.SelectedIndex = -1;
                }
            }
        }

        XmlDocument XMLconfigDoc;
        Int32 originalNumberHistorySNCount;
        Int32 originalNumberHistorySNSorting;
        String ActualMode;
        String originalCommandColor;

        private void AlwaysAvailableSettingsForm_Load(object sender, EventArgs e)
        {
            Int32 n_HistorySNCount = Convert.ToInt32(this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersCount").InnerText);            
            this.originalNumberHistorySNCount = n_HistorySNCount;
            this.nUD_LastSNinHystory.Value = n_HistorySNCount;

            Int32 n_HistorySNSorting = Convert.ToInt32(this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersSorting").InnerText);
            this.originalNumberHistorySNSorting = n_HistorySNSorting;
            this.cb_HistorySNSorting.SelectedIndex = n_HistorySNSorting;

            ArrayList ColorList = new ArrayList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static |
                                          BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                this.cb_CommandsColor.Items.Add(c.Name);
            }
            this.cb_CommandsColor.DrawMode = DrawMode.OwnerDrawFixed;

            this.originalCommandColor = this.XMLconfigDoc.SelectSingleNode("./Configuration/Colors/CommandColor").InnerText.ToString().Trim();
            this.cb_CommandsColor.SelectedIndex = this.cb_CommandsColor.Items.IndexOf(this.originalCommandColor);
        }

        private void nUD_LastSNinHystory_ValueChanged(object sender, EventArgs e)
        {
            this.CheckForSave();
        }

        private void cb_HistorySNSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckForSave();
        }

        private void CheckForSave()
        {
            if (this.originalCommandColor == null) return;
            Boolean b_savebuttonactive = false;
            if (this.originalNumberHistorySNCount != this.nUD_LastSNinHystory.Value) b_savebuttonactive = true;
            if (this.originalNumberHistorySNSorting != this.cb_HistorySNSorting.SelectedIndex) b_savebuttonactive = true;
            if (this.cb_StationMode.SelectedIndex > -1)
            {
                if (this.ActualMode != this.cb_StationMode.Text.Substring(0, 1).Trim()) b_savebuttonactive = true;
            }
            if (this.originalCommandColor != this.cb_CommandsColor.Items[this.cb_CommandsColor.SelectedIndex].ToString().Trim()) b_savebuttonactive = true;
            this.btn_SAVE.Enabled = b_savebuttonactive;
        }

        private void btn_SAVE_Click(object sender, EventArgs e)
        {
            this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersCount").InnerText = this.nUD_LastSNinHystory.Value.ToString();
            this.originalNumberHistorySNCount = Convert.ToInt32(this.nUD_LastSNinHystory.Value);

            this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersSorting").InnerText = this.cb_HistorySNSorting.SelectedIndex.ToString();            
            this.originalNumberHistorySNSorting = this.cb_HistorySNSorting.SelectedIndex;

            if (this.cb_StationMode.Enabled)
            {
                this.XMLconfigDoc.SelectSingleNode("./Configuration/Mode").InnerText = this.cb_StationMode.Text.Substring(0, 1).Trim();
                this.ActualMode = this.cb_StationMode.Text.Substring(0, 1).Trim();
            }

            this.XMLconfigDoc.SelectSingleNode("./Configuration/Colors/CommandColor").InnerText = this.cb_CommandsColor.Text.Trim();
            this.originalCommandColor = this.cb_CommandsColor.Text.Trim();

            this.XMLconfigDoc.Save(this.XMLconfigDoc.BaseURI.Substring(this.XMLconfigDoc.BaseURI.IndexOf("C:/")));
            this.btn_SAVE.Enabled = false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cb_StationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckForSave();
        }

        private void cb_CommandsColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }

        private void cb_CommandsColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CheckForSave();
        }
    }
}

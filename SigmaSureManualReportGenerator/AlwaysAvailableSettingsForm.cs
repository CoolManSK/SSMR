using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void AlwaysAvailableSettingsForm_Load(object sender, EventArgs e)
        {
            Int32 n_HistorySNCount = Convert.ToInt32(this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersCount").InnerText);            
            this.originalNumberHistorySNCount = n_HistorySNCount;
            this.nUD_LastSNinHystory.Value = n_HistorySNCount;

            Int32 n_HistorySNSorting = Convert.ToInt32(this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersSorting").InnerText);
            this.originalNumberHistorySNSorting = n_HistorySNSorting;
            this.cb_HistorySNSorting.SelectedIndex = n_HistorySNSorting;            
        }

        private void nUD_LastSNinHystory_ValueChanged(object sender, EventArgs e)
        {
            if (this.CheckForSave()) this.btn_SAVE.Enabled = true;
            else this.btn_SAVE.Enabled = false;
        }

        private void cb_HistorySNSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CheckForSave()) this.btn_SAVE.Enabled = true;
            else this.btn_SAVE.Enabled = false;
        }

        private Boolean CheckForSave()
        {            
            if (this.originalNumberHistorySNCount != this.nUD_LastSNinHystory.Value) return true;
            if (this.originalNumberHistorySNSorting != this.cb_HistorySNSorting.SelectedIndex) return true;
            if (this.cb_StationMode.SelectedIndex > -1)
            {
                if (this.ActualMode != this.cb_StationMode.Text.Substring(0, 1).Trim()) return true;
            }
            return false;
        }

        private void btn_SAVE_Click(object sender, EventArgs e)
        {
            this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersCount").InnerText = this.nUD_LastSNinHystory.Value.ToString();
            this.originalNumberHistorySNCount = Convert.ToInt32(this.nUD_LastSNinHystory.Value);

            this.XMLconfigDoc.SelectSingleNode("./Configuration/HistorySerialNumbersSorting").InnerText = this.cb_HistorySNSorting.SelectedIndex.ToString();            
            this.originalNumberHistorySNSorting = this.cb_HistorySNSorting.SelectedIndex;

            this.XMLconfigDoc.SelectSingleNode("./Configuration/Mode").InnerText = this.cb_StationMode.Text.Substring(0, 1).Trim();
            this.originalNumberHistorySNSorting = this.cb_HistorySNSorting.SelectedIndex;

            this.XMLconfigDoc.Save(this.XMLconfigDoc.BaseURI.Substring(this.XMLconfigDoc.BaseURI.IndexOf("C:/")));
            this.btn_SAVE.Enabled = false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cb_StationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CheckForSave()) this.btn_SAVE.Enabled = true;
            else this.btn_SAVE.Enabled = false;
        }
    }
}

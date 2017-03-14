using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace SigmaSureManualReportGenerator
{
    class ProductBarcode
    {
        private String ProductsConfigFileName = "ProductsConfiguration.xml";
        private XmlDocument ProductsConfig = new XmlDocument();
        private String ConfigPath = "";

        private void LoadProductsConfig()
        {
            Int32 n_majorOSVersion = Environment.OSVersion.Version.Major;

            if (n_majorOSVersion == 5) this.ConfigPath = @"C:\Documents and Settings\All Users\Application Data\SSManualReportGenerator\";
            else if (n_majorOSVersion == 6) this.ConfigPath = @"C:\Users\Public\SSManualReportGenerator\";
            else
            {
                this.ErrorMessageBoxShow("Neznama verzia operacneho systemu. Zavolajte prosim testovacieho inziniera");
                return;
            }

            this.ProductsConfig.Load(String.Concat(this.ConfigPath, ProductsConfigFileName));
        }
        private void ErrorMessageBoxShow(String Message)
        {
            MessageBox.Show(Message, "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Int32 i = 0;
            while (i < 9999999) i++;
        }
        private XmlNode GetProductNode(String ProductNo)
        {
            XmlNode AssembliesNode = this.ProductsConfig.SelectSingleNode("./Configuration/Assemblies");
            foreach (XmlNode actProdNode in AssembliesNode.ChildNodes)
            {
                if (actProdNode.SelectSingleNode("./Name").InnerText == ProductNo) return actProdNode;
            }
            return null;
        }
        private XmlNode GetFamilyNode(String ProductNo)
        {
            XmlNode FamiliesNode = this.ProductsConfig.SelectSingleNode("./Configuration/Families");
            foreach (XmlNode actFamilyNode in FamiliesNode)
            {
                String actFamilyNodeName = actFamilyNode.SelectSingleNode("./Name").InnerText;
                if (actFamilyNodeName.IndexOf('%') == 0)
                {
                    if (ProductNo.Length < (actFamilyNodeName.Length - 1)) continue;
                    if (ProductNo.IndexOf(actFamilyNodeName.Substring(1)) > -1) return actFamilyNode;
                }
                else
                {
                    if (ProductNo.Length < actFamilyNodeName.Length) continue;
                    if (actFamilyNodeName == ProductNo.Substring(0, actFamilyNodeName.Length)) return actFamilyNode;
                }
            }
            return null;
        }
        private String FormatSerialNumber(String InputSN)
        {
            while (InputSN.Length < 5)
            {
                InputSN = String.Concat("0", InputSN);
            }
            return InputSN;
        }

        public static String GetProductNoFromBarcode(String BarcodeText)
        {
            ProductBarcode PB = new ProductBarcode();
            PB.LoadProductsConfig();
            if (BarcodeText.ToUpper() == "PASS") return BarcodeText;
            if (BarcodeText.ToUpper() == "FAIL") return BarcodeText;
            String retValue = BarcodeText;
            if (BarcodeText.IndexOf(';') > -1)
            {
                if (BarcodeText[0] != '#')
                {
                    return BarcodeText.Substring(0, BarcodeText.IndexOf(';'));
                }
                else
                {
                    //#0245594100002;350DNC40-24-8G;AA;1614;16;;350DNC40-24-8G;;1834103-RG;12KG
                    String retVal = BarcodeText.Substring(BarcodeText.IndexOf(';') + 1);
                    retVal = retVal.Substring(0, retVal.IndexOf(';'));
                    return retVal;
                }
            }
            foreach (XmlNode actFamilyNode in PB.ProductsConfig.SelectSingleNode("./Configuration/Families").ChildNodes)
            {
                String str_actFamilyName = actFamilyNode.SelectSingleNode("./Name").InnerText;
                if (str_actFamilyName.Substring(0, 1) == "%") str_actFamilyName = str_actFamilyName.Substring(1);
                if (BarcodeText.IndexOf(str_actFamilyName) > -1)
                {
                    XmlNode node_PNStartIndex = actFamilyNode.SelectSingleNode("./BarcodeSettings/ProductNo/StartIndex");
                    if (node_PNStartIndex == null)
                    {
                        PB.ErrorMessageBoxShow("V konfiguracnom subore chyba informacia o zaciatocnej pozicii ProductNo v barcode pre rodinu \"" + str_actFamilyName + "\". Zavolajte prosim testovacieho inziniera.");
                        return "Error";
                    }
                    Int16 n_PNStartIndex = Convert.ToInt16(node_PNStartIndex.InnerText);

                    if (n_PNStartIndex == -1) break;

                    XmlNode node_PNLength = actFamilyNode.SelectSingleNode("./BarcodeSettings/ProductNo/Length");
                    if (node_PNLength == null)
                    {
                        PB.ErrorMessageBoxShow("V konfiguracnom subore chyba informacia o dlzke ProductNo v barcode pre rodinu \"" + str_actFamilyName + "\". Zavolajte prosim testovacieho inziniera.");
                        return "Error";
                    }
                    Int16 n_PNLength = Convert.ToInt16(node_PNLength.InnerText);

                    if (retValue.Length > n_PNStartIndex)
                    {
                        if (n_PNLength == 0)
                        {
                            retValue = BarcodeText.Substring(n_PNStartIndex);
                        }                    
                        else
                        {
                            retValue = BarcodeText.Substring(n_PNStartIndex, n_PNLength);
                        }
                    }
                    break;
                }
            }
            return retValue;
        }
        public static String GetJobIdFromBarcode(String ProductNo, String BarcodeText)
        {
            if (BarcodeText.IndexOf(';') > -1)
            {
                if (BarcodeText[0] != '#')
                {
                    //SPDCSCO-30G;16;02433956;AD;00226;341-0435-01 A0;PSK2016W06N;A9K-750W-DC V03;IPUPAH9AAA;191688
                    String retVal = BarcodeText.Substring(BarcodeText.IndexOf(';') + 1);
                    retVal = retVal.Substring(retVal.IndexOf(';') + 1);
                    retVal = retVal.Substring(0, retVal.IndexOf(';'));
                    return retVal;
                }
                else
                {
                    //#0245594100002;350DNC40-24-8G;AA;1614;16;;350DNC40-24-8G;;1834103-RG;12KG
                    return BarcodeText.Substring(1, 8);
                }
            }
            ProductBarcode PB = new ProductBarcode();
            PB.LoadProductsConfig();
            XmlNode node_actProdNoNode = PB.GetProductNode(ProductNo);
            if (node_actProdNoNode == null)
            {
                return null;
            }
            if (node_actProdNoNode.SelectSingleNode("./BarcodeLength") != null)
            {
                if (BarcodeText.Length < Convert.ToInt32(node_actProdNoNode.SelectSingleNode("./BarcodeLength").InnerText))
                {
                    if ((BarcodeText.Length == 7) || (BarcodeText.Length == 8))
                    {
                        return BarcodeText;
                    }
                    return "";
                }
            }
            XmlNode actBarcodeIdentifierNode = node_actProdNoNode.SelectSingleNode("./BarcodeSettings");
            if (actBarcodeIdentifierNode == null)
            {
                XmlNode actFamilyNode = PB.GetFamilyNode(ProductNo);
                if (actFamilyNode != null)
                {
                    if (BarcodeText.Length < Convert.ToInt32(actFamilyNode.SelectSingleNode("./BarcodeLength").InnerText))
                    {
                        return BarcodeText;
                    }
                    XmlNode actJobIDNode = actFamilyNode.SelectSingleNode("./BarcodeSettings/JobID");
                    if (actJobIDNode == null)
                    {
                        PB.ErrorMessageBoxShow(String.Concat("V konfiguracnom subore chyba info \"BarcodeSettings/JobID\" pre vyrobok ", ProductNo, "\n Zavolajte testovacieho inziniera!"));
                        return "";
                    }
                    else
                    {
                        try
                        {
                            Int16 n_StartIndex = Convert.ToInt16(actJobIDNode.SelectSingleNode("./StartIndex").InnerText);
                            Int16 n_Length = Convert.ToInt16(actJobIDNode.SelectSingleNode("./Length").InnerText);
                            if (n_StartIndex < 0)
                            {
                                return "";
                            }
                            else
                            {
                                return BarcodeText.Substring(n_StartIndex, n_Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            PB.ErrorMessageBoxShow(ex.Message);
                            return "";
                        }
                    }
                }
            }
            else
            {
                XmlNode actJobIDNode = actBarcodeIdentifierNode.SelectSingleNode("./JobID");
                if (actJobIDNode == null)
                {
                    PB.ErrorMessageBoxShow(String.Concat("V konfiguracnom subore chyba info \"BarcodeSettings/JobID\" pre vyrobok ", ProductNo, "\n Zavolajte testovacieho inziniera!"));
                    return "";
                }
                else
                {
                    try
                    {
                        Int16 n_StartIndex = Convert.ToInt16(actJobIDNode.SelectSingleNode("./StartIndex").InnerText);
                        Int16 n_Length = Convert.ToInt16(actJobIDNode.SelectSingleNode("./Length").InnerText);
                        if (n_StartIndex < 0)
                        {
                            return "";
                        }
                        else
                        {
                            return BarcodeText.Substring(n_StartIndex, n_Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        PB.ErrorMessageBoxShow(ex.Message);
                        return "";
                    }
                }
            }
            return BarcodeText;
        }
        public static String GetSerialNumberFromBarcode(String ProductNo, String BarcodeText)
        {            
            ProductBarcode PB = new ProductBarcode();
            PB.LoadProductsConfig();
            if (BarcodeText.Length < 5)
            {
                BarcodeText = PB.FormatSerialNumber(BarcodeText);
                return BarcodeText;
            }

            if (BarcodeText.Length == 13)
            {
                Int32 n_CheckVar = 0;
                try
                {
                    n_CheckVar = Convert.ToInt32(BarcodeText);
                    BarcodeText = BarcodeText.Substring(8);
                    return BarcodeText;
                }
                catch
                { }                
            }

            if (BarcodeText.Substring(0,1) == "#")
            {
                BarcodeText = BarcodeText.Substring(9, 5);
                return BarcodeText;
            }

            if (BarcodeText.IndexOf(';') > -1)
            {
                switch (BarcodeText.Length - BarcodeText.Replace(";", "").Length)
                {
                    case 1:
                    case 2:
                    case 3:
                        PB.ErrorMessageBoxShow("Nespravny format barcodu. Prilis malo znakov ';'");
                        return BarcodeText;
                    case 4:
                        return BarcodeText.Substring(BarcodeText.LastIndexOf(';') + 1);                        
                    default:
                        Int32 n_startPosition = BarcodeText.IndexOf(';') + 1;
                        n_startPosition = BarcodeText.IndexOf(';', n_startPosition) + 1;
                        n_startPosition = BarcodeText.IndexOf(';', n_startPosition) + 1;
                        n_startPosition = BarcodeText.IndexOf(';', n_startPosition) + 1;
                        return BarcodeText.Substring(n_startPosition, BarcodeText.IndexOf(';', n_startPosition) - n_startPosition);
                }
            }

            XmlNode node_actProdNoNode = PB.GetProductNode(ProductNo);

            if (node_actProdNoNode.SelectSingleNode("./BarcodeLength") != null)
            {
                if (BarcodeText.Length < Convert.ToInt32(node_actProdNoNode.SelectSingleNode("./BarcodeLength").InnerText))
                {
                    return "";
                }
            }

            XmlNode actBarcodeIdentifierNode = node_actProdNoNode.SelectSingleNode("./BarcodeSettings");
            if (actBarcodeIdentifierNode == null)
            {
                XmlNode actFamilyNode = PB.GetFamilyNode(ProductNo);
                if (actFamilyNode != null)
                {
                    if (BarcodeText.Length < Convert.ToInt32(actFamilyNode.SelectSingleNode("./BarcodeLength").InnerText))
                    {
                        return PB.FormatSerialNumber(BarcodeText);
                    }
                    XmlNode actSerialNumberNode = actFamilyNode.SelectSingleNode("./BarcodeSettings/SerialNumber");
                    if (actSerialNumberNode == null)
                    {
                        PB.ErrorMessageBoxShow(String.Concat("V konfiguracnom subore chyba info \"BarcodeSettings/SerialNumber\" pre vyrobok ", ProductNo, "\n Zavolajte testovacieho inziniera!"));
                        return "";
                    }
                    else
                    {
                        try
                        {
                            Int16 n_StartIndex = Convert.ToInt16(actSerialNumberNode.SelectSingleNode("./StartIndex").InnerText);
                            Int16 n_Length = Convert.ToInt16(actSerialNumberNode.SelectSingleNode("./Length").InnerText);
                            return PB.FormatSerialNumber(BarcodeText.Substring(n_StartIndex, n_Length));
                        }
                        catch (Exception ex)
                        {
                            PB.ErrorMessageBoxShow(ex.Message);
                            return "";
                        }
                    }
                }
            }
            else
            {
                XmlNode actSerialNumberNode = actBarcodeIdentifierNode.SelectSingleNode("./SerialNumber");
                if (actSerialNumberNode == null)
                {
                    PB.ErrorMessageBoxShow(String.Concat("V konfiguracnom subore chyba info \"BarcodeSettings/SerialNumber\" pre vyrobok ", ProductNo, "\n Zavolajte testovacieho inziniera!"));
                    return "";
                }
                else
                {
                    try
                    {
                        Int16 n_StartIndex = Convert.ToInt16(actSerialNumberNode.SelectSingleNode("./StartIndex").InnerText);
                        Int16 n_Length = Convert.ToInt16(actSerialNumberNode.SelectSingleNode("./Length").InnerText);
                        return PB.FormatSerialNumber(BarcodeText.Substring(n_StartIndex, n_Length));
                    }
                    catch (Exception ex)
                    {
                        PB.ErrorMessageBoxShow(ex.Message);
                        return "";
                    }
                }
            }
            return PB.FormatSerialNumber(BarcodeText);
        }
    }
}

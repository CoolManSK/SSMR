using System;
using System.Xml;

namespace SigmaSureManualReportGenerator
{
    class SpecialRequirements
    {
        public static Boolean IsActive(XmlDocument ConfigFile, String Product, String SRName)
        {
            Boolean retVal = false;
            XmlNode ProductsNode = ConfigFile.SelectSingleNode("./Configuration/Assemblies");
            if (ProductsNode != null)
            {
                foreach (XmlNode actProductNode in ProductsNode.ChildNodes)
                {
                    XmlNode actProductNameNode = actProductNode.SelectSingleNode("./Name");
                    if (actProductNameNode == null) return retVal;
                    if (actProductNameNode.InnerText == Product)
                    {
                        XmlNode SpecialRequirementsNode = actProductNode.SelectSingleNode("./SpecialRequirements");
                        if (SpecialRequirementsNode == null) return retVal;
                        XmlNode finalNode = SpecialRequirementsNode.SelectSingleNode(String.Concat(@"./", SRName, @"/Active"));
                        if (finalNode == null) return retVal;
                        if (finalNode.InnerText == "Y") return true;
                    }
                }
            }
            return retVal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace SigmaSureManualReportGenerator
{
    public class ProductsConfigurationFile
    {
        public String ConfigPath = "";
        public XmlDocument XmlFile = new XmlDocument();
        
        public struct ChildTestInfo
        {
            public string Name;
            public string Instruction;
            public string PicturePath;
        }
        public struct FaultCode
        {
            public String Code;
            public String Description;
        }
        

        public ProductsConfigurationFile(String ConfigPath)
        {
            this.ConfigPath = ConfigPath;
            try
            {
                this.XmlFile.Load(String.Concat(this.ConfigPath, "ProductsConfiguration.xml"));
            }
            catch(Exception ex)
            {

            }
        }

        public Array GetAssembliesList()
        {
            String[] ar_assemblies = { };
            XmlNode node_Assembly = this.XmlFile.SelectSingleNode("./Configuration/Assemblies");
            foreach (XmlNode actNode in node_Assembly.ChildNodes)
            {
                XmlNode node_AssemblyName = actNode.SelectSingleNode("./Name");
                Array.Resize(ref ar_assemblies, ar_assemblies.Length + 1);
                ar_assemblies.SetValue(node_AssemblyName.InnerText, ar_assemblies.Length - 1);
            }
            return ar_assemblies;
        }

        public XmlNode GetProductNode(String ProductNo)
        {
            XmlNode AssembliesNode = this.XmlFile.SelectSingleNode("./Configuration/Assemblies");
            foreach (XmlNode actProdNode in AssembliesNode.ChildNodes)
            {
                if (actProdNode.SelectSingleNode("./Name").InnerText == ProductNo) return actProdNode;
            }
            return null;
        }

        public XmlNode GetFamilyNode(String ProductNo)
        {
            XmlNode FamiliesNode = this.XmlFile.SelectSingleNode("./Configuration/Families");
            foreach (XmlNode actFamilyNode in FamiliesNode)
            {
                String actFamilyNodeName = actFamilyNode.SelectSingleNode("./Name").InnerText;
                if (actFamilyNodeName.IndexOf('%') == 0)
                {
                    if ((actFamilyNodeName.Length - 1) > ProductNo.Length) continue;
                    if (ProductNo.IndexOf(actFamilyNodeName.Substring(1)) > -1) return actFamilyNode;
                }
                else
                {
                    if (actFamilyNodeName.Length > ProductNo.Length) continue;
                    if (actFamilyNodeName == ProductNo.Substring(0, actFamilyNodeName.Length)) return actFamilyNode;
                }

            }
            return null;
        }

        public Boolean BelMesActivated(String ProductNo)
        {
            Boolean retval = false;

            XmlNode myProdNode = this.GetProductNode(ProductNo);
            if (myProdNode != null)
            {
                if (myProdNode.SelectSingleNode("./BelMESActivated") != null)
                {
                    String str_buffer = myProdNode.SelectSingleNode("./BelMESActivated").InnerText;
                    if (str_buffer == "Y") retval = true;
                }
            }

            XmlNode myFamNode = this.GetFamilyNode(ProductNo);
            if (myFamNode != null)
            {
                if (myFamNode.SelectSingleNode("./BelMESActivated") != null)
                {
                    String str_buffer = myFamNode.SelectSingleNode("./BelMESActivated").InnerText;
                    if (str_buffer == "Y") retval = true;
                }
            }
            return retval;
        }

        public Array GetChildTestsInfos(string ProdID, string TestType)
        {
            ChildTestInfo[] retArray = { };
            if (TestType == "") return retArray;

            XmlNode ProdNode = this.GetProductNode(ProdID);
            if (ProdNode != null)
            {
                XmlNode childCheckListInfoActTestNode = ProdNode.SelectSingleNode(String.Concat("./CheckListTests/", TestType.Replace(' ', '_')));
                if (childCheckListInfoActTestNode != null)
                {
                    XmlNode InstructionsNode = this.XmlFile.LastChild.SelectSingleNode(String.Concat("./Checklists/", TestType.Replace(' ', '_'), "/", childCheckListInfoActTestNode.InnerText));
                    if (InstructionsNode != null)
                    {
                        for (Int32 i = 0; i < InstructionsNode.ChildNodes.Count; i++)
                        {
                            XmlNode actTestNode = InstructionsNode.ChildNodes[i];
                            ChildTestInfo actCTI = new ChildTestInfo();
                            actCTI.Name = actTestNode.Name.Replace('_', ' ');
                            actCTI.Instruction = actTestNode.SelectSingleNode("./Instruction").InnerText;
                            XmlNode actPictureNode = actTestNode.SelectSingleNode("./Picture");
                            if (actPictureNode != null)
                            {
                                actCTI.PicturePath = String.Concat(childCheckListInfoActTestNode.InnerText, @"\", actTestNode.SelectSingleNode("./Picture").InnerText);
                                if (Path.GetFileName(actCTI.PicturePath) == "")
                                {
                                    actCTI.PicturePath = "";
                                }
                            }
                            else
                            {
                                actCTI.PicturePath = "";
                            }
                            Array.Resize(ref retArray, i + 1);
                            retArray.SetValue(actCTI, i);
                        }
                    }
                }
            }
            if (retArray.Length != 0)
            {
                return retArray;
            }

            XmlNode FamilyNode = this.GetFamilyNode(ProdID);

            if (FamilyNode != null)
            {
                XmlNode childCheckListInfoNode = FamilyNode.SelectSingleNode("./CheckListTests");
                if (childCheckListInfoNode != null)
                {
                    XmlNode childCheckListInfoActTestNode = childCheckListInfoNode.SelectSingleNode(String.Concat("./", TestType.Replace(' ', '_')));
                    if (childCheckListInfoActTestNode != null)
                    {
                        XmlNode InstructionsNode = this.XmlFile.LastChild.SelectSingleNode(String.Concat("./Checklists/", TestType.Replace(' ', '_'), "/", childCheckListInfoActTestNode.InnerText));
                        if (InstructionsNode != null)
                        {
                            for (Int32 i = 0; i < InstructionsNode.ChildNodes.Count; i++)
                            {
                                XmlNode actTestNode = InstructionsNode.ChildNodes[i];
                                ChildTestInfo actCTI = new ChildTestInfo();
                                actCTI.Name = actTestNode.Name.Replace('_', ' ');
                                actCTI.Instruction = actTestNode.SelectSingleNode("./Instruction").InnerText;
                                XmlNode actPictureNode = actTestNode.SelectSingleNode("./Picture");
                                if (actPictureNode != null)
                                {
                                    actCTI.PicturePath = String.Concat(childCheckListInfoActTestNode.InnerText, @"\", actTestNode.SelectSingleNode("./Picture").InnerText);
                                    if (Path.GetFileName(actCTI.PicturePath) == "")
                                    {
                                        actCTI.PicturePath = "";
                                    }
                                }
                                else
                                {
                                    actCTI.PicturePath = "";
                                }
                                Array.Resize(ref retArray, i + 1);
                                retArray.SetValue(actCTI, i);
                            }
                        }
                    }

                }
            }
            return retArray;
        }

        public Array GetFaultCodes(String ProdID, String TestType, Boolean PartNoNeeded)
        {
            if (PartNoNeeded)
            {
                return GetFaultCodes(ProdID, TestType);
            }
            else
            {
                String[] retArray = { };
                return retArray;
            }
        }

        public Array GetFaultCodes(String ProdID, String TestType)
        {
            FaultCode[] retArray = { };
            XmlNode ProdNode = this.GetProductNode(ProdID);
            XmlNode childFaultCodesListInfoNode = ProdNode.SelectSingleNode("./FaultCodesLists");
            if (childFaultCodesListInfoNode != null)
            {
                XmlNode childFaultCodesListInfoActTestNode = childFaultCodesListInfoNode.SelectSingleNode(String.Concat("./", TestType.Replace(' ', '_')));
                if (childFaultCodesListInfoActTestNode != null)
                {
                    for (Int32 i = 0; i < childFaultCodesListInfoActTestNode.ChildNodes.Count; i++)
                    {
                        XmlNode actTestNode = childFaultCodesListInfoActTestNode.ChildNodes[i];
                        FaultCode actFC = new FaultCode();
                        actFC.Code = actTestNode.Name.Replace('_', ' ');
                        actFC.Description = actTestNode.SelectSingleNode("./Description").InnerText;
                        Array.Resize(ref retArray, i + 1);
                        retArray.SetValue(actFC, i);
                    }
                }
            }
            else
            {
                XmlNode FamilyNode = this.GetFamilyNode(ProdID);
                if (FamilyNode != null)
                {
                    childFaultCodesListInfoNode = FamilyNode.SelectSingleNode("./FaultCodesLists");
                    if (childFaultCodesListInfoNode != null)
                    {
                        XmlNode childFaultCodesListInfoActTestNode = childFaultCodesListInfoNode.SelectSingleNode(String.Concat("./", TestType.Replace(' ', '_')));
                        if (childFaultCodesListInfoActTestNode != null)
                        {
                            for (Int32 i = 0; i < childFaultCodesListInfoActTestNode.ChildNodes.Count; i++)
                            {
                                XmlNode actTestNode = childFaultCodesListInfoActTestNode.ChildNodes[i];
                                FaultCode actFC = new FaultCode();
                                actFC.Code = actTestNode.Name.Replace('_', ' ');
                                actFC.Description = actTestNode.SelectSingleNode("./Description").InnerText;
                                Array.Resize(ref retArray, i + 1);
                                retArray.SetValue(actFC, i);
                            }
                        }
                    }
                }
            }
            return retArray;
        }        
    }
}

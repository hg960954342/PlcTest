using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace SiemensNet
{
    public class OpcXmlManage
    {
        public List<OpcRoutItem> OpcRoutItemList
        {
            get; set;
        }

        public string ServerName { get; set; }

        public string IpAdd { get; set; }

        public OpcXmlManage(string FilePath)
        {
            LoadOpcGroupConfig(FilePath);
        }
        public void LoadOpcGroupConfig(string strConfigFilePath)
        {
            string msg = "";
            try
            {
                if (!File.Exists(strConfigFilePath)) return;
                XDocument xDoc = XDocument.Load(strConfigFilePath);
                XElement xElement = xDoc.Element("System").Element("OpcServer");
                ServerName = xElement.Attribute("ServerName").Value;
                IpAdd = xElement.Attribute("IPAddress").Value;
                OpcRoutItemList = new List<OpcRoutItem>();
                foreach (XElement xElementItem in xElement.Elements())
                {
                    OpcRoutItem opcRoutItem = new OpcRoutItem();
                    opcRoutItem.GroupName = xElementItem.Attribute("GroupName").Value;
                    opcRoutItem.ItemIDList = new List<RouteItem>();
                   
                    foreach (XElement xSubElement in xElementItem.Elements())
                    {
                        RouteItem item = new RouteItem();
                        item.ItemId = xSubElement.Attribute("ItemID").Value;
                        item.CaseId = xSubElement.Attribute("CASEID").Value;
                        item.ClientId = xSubElement.Attribute("ClientHandle").Value;

                        opcRoutItem.ItemIDList.Add(item);
                        
                    }

                    OpcRoutItemList.Add(opcRoutItem);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}

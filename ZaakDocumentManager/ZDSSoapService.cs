using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ZaakDocumentManager
{
    public class ZDSSoapService
    {
        private string soapurl;
        private string soapaction;

        public ZDSSoapService(string soapurl, string soapaction)
        {
            this.soapurl = soapurl;
            this.soapaction = soapaction;
        }


        public class ZDSXmlDocument : System.Xml.XmlDocument
        {
            public XmlNamespaceManager NamespaceManager;
            public ZDSXmlDocument(string templatepath) : base()
            {
                Load(templatepath);

                NamespaceManager = new System.Xml.XmlNamespaceManager(NameTable);
                NamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                NamespaceManager.AddNamespace("StUF", "http://www.egem.nl/StUF/StUF0301");
                NamespaceManager.AddNamespace("ZKN", "http://www.egem.nl/StUF/sector/zkn/0310");
                NamespaceManager.AddNamespace("BG", "http://www.egem.nl/StUF/sector/bg/0310");
            }
            public ZDSXmlDocument(System.IO.StreamReader streamreader) : base()
            {
                var content = streamreader.ReadToEnd();
                LoadXml(content);

                NamespaceManager = new System.Xml.XmlNamespaceManager(NameTable);
                NamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                NamespaceManager.AddNamespace("StUF", "http://www.egem.nl/StUF/StUF0301");
                NamespaceManager.AddNamespace("ZKN", "http://www.egem.nl/StUF/sector/zkn/0310");
                NamespaceManager.AddNamespace("BG", "http://www.egem.nl/StUF/sector/bg/0310");
            }

            public bool NodeExists(string xpath)
            {
                return SelectSingleNode(xpath, NamespaceManager) != null;
            }

            public void SetNodeText(string xpath, string value,  bool checksinglevalue=true)
            {
                var nodes = SelectNodes(xpath, NamespaceManager);
                if(checksinglevalue && nodes.Count != 1) throw new Exception("xpath: " + xpath + " did not return a single node (count:" + nodes.Count+ ")");
                XmlNode node = nodes[0];
                node.InnerText = value;
            }

            public void SetAttributeText(string xpath, string attributename, string value)
            {
                var nodes = SelectNodes(xpath, NamespaceManager);
                if (nodes.Count != 1) throw new Exception("xpath: " + xpath + " did not return a single node (count:" + nodes.Count + ")");
                XmlNode node = nodes[0];
                if(node.Attributes[attributename] == null) throw new Exception("xpath: " + xpath + " found but no attribute with name:" + attributename);
                node.Attributes[attributename].Value = value;
            }

            public string GetNodeText(string xpath, bool raiseexception = true)
            {
                var nodes = SelectNodes(xpath, NamespaceManager);
                if (nodes.Count != 1 && raiseexception) throw new Exception("xpath: " + xpath + " did not return a single node (count:" + nodes.Count + ")");
                if (nodes.Count != 1) return "NULL";
                    XmlNode node = nodes[0];
                return node.InnerText;
            }
        }


        public ZDSXmlDocument PerformRequest(ZDSXmlDocument requestdocument)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(soapurl);
            request.Headers.Add(@"SOAPAction", soapaction);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------");
            System.Diagnostics.Debug.WriteLine(soapurl);
            System.Diagnostics.Debug.WriteLine("SOAPAction:" + soapaction);
            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------");
            System.Diagnostics.Debug.WriteLine("request xml:");
            System.Diagnostics.Debug.WriteLine(requestdocument.OuterXml);
            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------");

            using (System.IO.Stream stream = request.GetRequestStream())
            {
                try
                {
                    requestdocument.Save(stream);
                    using (System.Net.WebResponse response = request.GetResponse())
                    {
                        using (System.IO.StreamReader rd = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            var responsedocument = new ZDSXmlDocument(rd);

                            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------");
                            System.Diagnostics.Debug.WriteLine("response xml:");
                            System.Diagnostics.Debug.WriteLine(responsedocument.OuterXml);
                            System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------");
                            return responsedocument;
                        }
                    }
                }
                catch (System.Net.WebException wex)
                {
                    var errorstream = wex.Response.GetResponseStream();
                    var errorreader = new System.IO.StreamReader(errorstream, Encoding.UTF8);
                    String errormessage = errorreader.ReadToEnd();

                    MessageBox.Show(
                            "soap url: " + soapurl + "\n" +
                            "soap action: " + soapaction + "\n" +
                            "\n-- request ---------------------------------------------------------------------\n" +
                            requestdocument.OuterXml + "\n" +
                            "\n\n-- response " + wex.Message + "---------------------------------------------------------------------\n" +
                            errormessage,
                            wex.ToString()
                        );
                    throw wex;
                }
            }
        }
    }
}
using System;
using System.IO;
using System.Xml;

namespace ZaakDocumentDragAndDrop
{
    internal class DocumentMapping
    {
        private string zaaktypecode;
        private string mimetype;
        public string Documenttype;
        public DateTime CreationTime;
        public string Titel;
        public string Taal;
        public string Vertrouwelijkheid;
        public string Mimetype;
        public string Name;
        public string Formaat;

        public DocumentMapping(string zaaktypecode, string documentfilename, DateTime creationdate)
        {
            this.zaaktypecode = zaaktypecode;
            mimetype = System.Web.MimeMapping.GetMimeMapping(documentfilename);

            this.Documenttype = "documenttype";
            this.CreationTime = creationdate;
            this.Titel = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + documentfilename;
            this.Taal = "nld";
            this.Vertrouwelijkheid = "VERTROUWELIJK";
            this.Mimetype = mimetype;
            this.Name = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + documentfilename;
            this.Formaat = documentfilename.Contains(".") ? documentfilename.Substring(documentfilename.IndexOf(".")) : documentfilename;

            var config = new System.Xml.XmlDocument();
            config.Load("mapping.xml");

            var zaaktypes = config.SelectNodes("//zaaktype");
            foreach(System.Xml.XmlNode zt in zaaktypes)
            {
                if(zt.Attributes["matchfield"] == null ||  
                    ( zt.Attributes["matchvalue"] != null 
                        && zt.Attributes["matchfield"] != null 
                        && zt.Attributes["matchfield"].Value == "code" 
                        && this.zaaktypecode.ToLower().Contains(zt.Attributes["matchvalue"].Value.ToLower()) ))
                {
                    ChooseDocument(zt);
                    return;
                }
            }
            throw new Exception("Aan het zaaktype met code: " + zaaktypecode + " kunnen geen documenten worden toegevoegd, omdat dit niet is ingesteld");
        }

        private void ChooseDocument(XmlNode zaaktype)
        {
            var documenten = zaaktype.SelectNodes("document");
            foreach (System.Xml.XmlNode document in documenten)
            {
                if(document.Attributes["matchfield"] == null ||
                    document.Attributes["matchfield"] != null
                    && document.Attributes["matchvalue"] != null
                    && document.Attributes["matchfield"].Value == "naam"
                    && Name.ToLower().Contains(document.Attributes["matchvalue"].Value.ToLower() ))
                {
                    var titelnode = document.SelectSingleNode("titel");
                    if (titelnode != null) Titel = titelnode.InnerText;
                    var typenode = document.SelectSingleNode("type");
                    if (typenode != null) Documenttype = typenode.InnerText;
                    var vertrouwelijkheidnode = document.SelectSingleNode("vertrouwelijkheid");
                    if (vertrouwelijkheidnode != null) Vertrouwelijkheid = vertrouwelijkheidnode.InnerText.ToUpper();

                    return;
                }
            }
            throw new Exception("Aan het zaaktype met code: " + zaaktypecode + " kon niet een document met naam:" + Name + " worden toegevoegd, omdat dit niet is ingesteld");
        }
    }
}
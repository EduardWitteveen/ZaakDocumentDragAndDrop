using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ZaakDocumentManager
{
    public class Zaak
    {
        private ZDSSoapService.ZDSXmlDocument document;

        public Zaak(ZDSSoapService.ZDSXmlDocument document)
        {
            this.document = document;
        }

        public string ZaakTypeOmschrijving
        {
            get
            {
                return document.GetNodeText("//ZKN:object/ZKN:isVan/ZKN:gerelateerde/ZKN:omschrijving");
            }
        }

        public string ZaakTypeText
        {
            get
            {
                return document.GetNodeText("//ZKN:object/ZKN:isVan/ZKN:gerelateerde/ZKN:code");
            }
        }

        public string ZaakOmschrijving {
            get
            {
                return document.GetNodeText("//ZKN:object/ZKN:omschrijving");
            }
        }
        public string Afzender {
            get
            {
                //if (document.GetNodeText("//ZKN:object/ZKN:heeftAlsInitiator/ZKN:gerelateerde/ZKN:natuurlijkPersoon/BG:geslachtsnaam"))
                if(document.NodeExists("//ZKN:object/ZKN:heeftAlsInitiator/ZKN:gerelateerde/ZKN:natuurlijkPersoon")) { 
                    return document.GetNodeText("//ZKN:object/ZKN:heeftAlsInitiator/ZKN:gerelateerde/ZKN:natuurlijkPersoon/BG:geslachtsnaam")
                         + ", " +
                        document.GetNodeText("//ZKN:object/ZKN:heeftAlsInitiator/ZKN:gerelateerde/ZKN:natuurlijkPersoon/BG:voorletters");
                }
                else if (document.NodeExists("//ZKN:object/ZKN:heeftAlsInitiator/ZKN:gerelateerde/ZKN:nietNatuurlijkPersoon"))
                {
                    return document.GetNodeText("//ZKN:object/ZKN:heeftAlsInitiator/ZKN:gerelateerde/ZKN:nietNatuurlijkPersoon/BG:statutaireNaam");
                }
                else return "geen natuurlijkPersoon/nietNatuurlijkPersoon als initiator";
            }
        }
    }

    public class ZaakDocument
    {
        private ZDSSoapService.ZDSXmlDocument document;
        private XmlNode zaakdocumentnode;

        public ZaakDocument(ZDSSoapService.ZDSXmlDocument document, XmlNode zaakdocumentnode)
        {
            this.document = document;
            this.zaakdocumentnode = zaakdocumentnode;
        }
        //<identificatie xsi:nil="true"/>
        //<dct.omschrijving xsi:nil="true"/>
        //<creatiedatum xsi:nil="true"/>
        //<titel xsi:nil="true"/>
        //<formaat xsi:nil="true"/>
        //<taal xsi:nil="true"/>
        //<vertrouwelijkAanduiding xsi:nil="true"/>
        //<auteur xsi:nil="true"/>
        //<inhoud xsi:nil="true"/>

        public string Titel {
            get
            {
                var childnodes = zaakdocumentnode.ChildNodes;
                foreach(XmlNode node in childnodes)
                {
                    if (node.Name == "titel")
                    {
                        return node.InnerText;
                    }
                }
                return null;
            }
        }
    }

    public class ZaakDocumentServices
    {
        private string standaardZaakDocumentServicesVrijBerichtService;
        private string standaardZaakDocumentServicesOntvangAsynchroonService;
        private string standaardZaakDocumentServicesBeantwoordVraagService;

        public ZaakDocumentServices(string standaardZaakDocumentServicesVrijBerichtService, string standaardZaakDocumentServicesOntvangAsynchroonService, string standaardZaakDocumentServicesBeantwoordVraagService)
        {
            this.standaardZaakDocumentServicesVrijBerichtService = standaardZaakDocumentServicesVrijBerichtService;
            this.standaardZaakDocumentServicesOntvangAsynchroonService = standaardZaakDocumentServicesOntvangAsynchroonService;
            this.standaardZaakDocumentServicesBeantwoordVraagService = standaardZaakDocumentServicesBeantwoordVraagService;
        }

        public Zaak GeefZaakDetails(string zaakidentificatie)
        {
            var soapservice = new ZDSSoapService(
                Properties.Settings.Default.StandaardZaakDocumentServicesBeantwoordVraagService,
                "http://www.egem.nl/StUF/sector/zkn/0310/geefZaakdetails_Lv01");
            var requestdocument = new ZDSSoapService.ZDSXmlDocument("geefZaakdetails_Lv01.xml");

            requestdocument.SetNodeText("//StUF:referentienummer", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            requestdocument.SetNodeText("//StUF:tijdstipBericht", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            requestdocument.SetNodeText("//ZKN:gelijk/ZKN:identificatie", zaakidentificatie);

            return new Zaak(soapservice.PerformRequest(requestdocument));
        }

        public ZaakDocument[] GeefLijstZaakdocumenten(string zaakidentificatie)
        {
            var soapservice = new ZDSSoapService(
                Properties.Settings.Default.StandaardZaakDocumentServicesBeantwoordVraagService,
                "http://www.egem.nl/StUF/sector/zkn/0310/geefLijstZaakdocumenten_Lv01");
            var requestdocument = new ZDSSoapService.ZDSXmlDocument("geefLijstZaakdocumenten_Lv01.xml");

            requestdocument.SetNodeText("//StUF:referentienummer", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            requestdocument.SetNodeText("//StUF:tijdstipBericht", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            requestdocument.SetNodeText("//ZKN:gelijk/ZKN:identificatie", zaakidentificatie);
            //requestdocument.SetNodeText("//ZKN:scope/ZKN:object/ZKN:heeftRelevant/ZKN:gerelateerde/ZKN:identificatie", zaakidentificatie);

            var responsedocument = soapservice.PerformRequest(requestdocument);

            var documentnodes = responsedocument.SelectNodes("//ZKN:object/ZKN:heeftRelevant/ZKN:gerelateerde", responsedocument.NamespaceManager);
            var list = new List<ZaakDocument>();
            foreach(XmlNode documentnode in documentnodes)
            {
                list.Add(new ZaakDocument(responsedocument, documentnode));
            }
            return list.ToArray();
        }

        public string GenereerDocumentidentificatie(string zaakidentificatie)
        {
            var soapservice = new ZDSSoapService(
                Properties.Settings.Default.StandaardZaakDocumentServicesVrijBerichtService,
                "http://www.egem.nl/StUF/sector/zkn/0310/genereerDocumentIdentificatie_Di02");
            var requestdocument = new ZDSSoapService.ZDSXmlDocument("genereerDocumentIdentificatie_Di02.xml");
            var responsedocument = soapservice.PerformRequest(requestdocument);

            return responsedocument.GetNodeText("//ZKN:document/ZKN:identificatie");
        }

        public void VoegZaakdocumentToe(string zaakidentificatie, 
            string zaakdocumentidentificatie, 
            string zaakdocumenttype,
            DateTime creatiedatum,
            /* string titel, */
            string formaat,
            string taal,
            string vertrouwelijkheid,
            string contenttype,
            string bestandsnaam,
            byte[] documentdata
            )
        {

            Cursor.Current = Cursors.WaitCursor;

            //TODO: check if exists
            var documenten = GeefLijstZaakdocumenten(zaakidentificatie);
            var dict = new Dictionary<string, ZaakDocument>();
            foreach(var document in documenten)
            {
                if(!dict.ContainsKey(document.Titel)) {
                    dict.Add(document.Titel, document);
                }                
            }
            if(dict.ContainsKey(bestandsnaam))
            {
                var name = System.IO.Path.GetFileNameWithoutExtension(bestandsnaam);
                var extensie = System.IO.Path.GetExtension(bestandsnaam);
                int i = 1;
                do
                {
                    bestandsnaam = name + "_" + i + extensie;
                    i++;
                }
                while (dict.ContainsKey(bestandsnaam));
            }

            var soapservice = new ZDSSoapService(
                Properties.Settings.Default.StandaardZaakDocumentServicesOntvangAsynchroonService,
                "http://www.egem.nl/StUF/sector/zkn/0310/voegZaakdocumentToe_Lk01");
            var requestdocument = new ZDSSoapService.ZDSXmlDocument("voegZaakdocumentToe_Lk01.xml");

            requestdocument.SetNodeText("//StUF:referentienummer", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            requestdocument.SetNodeText("//StUF:tijdstipBericht", DateTime.Now.ToString("yyyyMMddhhmmssfff"));

            requestdocument.SetNodeText("//ZKN:object/ZKN:identificatie", zaakdocumentidentificatie);
            requestdocument.SetNodeText("//ZKN:object/ZKN:dct.omschrijving", zaakdocumenttype);
            requestdocument.SetNodeText("//ZKN:object/ZKN:creatiedatum", creatiedatum.ToString("yyyyMMdd"));
            requestdocument.SetNodeText("//ZKN:object/ZKN:titel", bestandsnaam);
            requestdocument.SetNodeText("//ZKN:object/ZKN:formaat", formaat);
            requestdocument.SetNodeText("//ZKN:object/ZKN:taal", taal);
            requestdocument.SetNodeText("//ZKN:object/ZKN:vertrouwelijkAanduiding", vertrouwelijkheid);
            requestdocument.SetNodeText("//ZKN:object/ZKN:auteur", System.Security.Principal.WindowsIdentity.GetCurrent().Name);

            requestdocument.SetAttributeText("//ZKN:object/ZKN:inhoud", "xmime:contentType", contenttype);
            requestdocument.SetAttributeText("//ZKN:object/ZKN:inhoud", "StUF:bestandsnaam", bestandsnaam);
            requestdocument.SetNodeText("//ZKN:object/ZKN:inhoud", Convert.ToBase64String(documentdata));

            requestdocument.SetNodeText("//ZKN:object/ZKN:isRelevantVoor/ZKN:gerelateerde/ZKN:identificatie", zaakidentificatie);

            var responsedocument = soapservice.PerformRequest(requestdocument);

            do
            {
                System.Threading.Thread.Sleep(500);

                documenten = GeefLijstZaakdocumenten(zaakidentificatie);
                dict = new Dictionary<string, ZaakDocument>();
                foreach (var document in documenten)
                {
                    if(!dict.ContainsKey(document.Titel)) {
                        dict.Add(document.Titel, document);
                    }                    
                }
            }
            while (!dict.ContainsKey(bestandsnaam));
            Cursor.Current = Cursors.Default;
        }
    }
}
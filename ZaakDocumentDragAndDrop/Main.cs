using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZaakDocumentDragAndDrop
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void txtZaakNummer_TextChanged(object sender, EventArgs e)
        {
            //Refresh();
        }

        void Refresh()
        {
            txtZaakTypeOmschrijving.Text = "";
            txtZaaktypeCode.Text = "";
            txtZaakOmschrijving.Text = "";
            txtAfzender.Text = "";
            lvDocumenten.Clear();
            lvDocumenten.Enabled = false;

            var zds = new ZaakDocumentServices(
                Properties.Settings.Default.StandaardZaakDocumentServicesVrijBerichtService,
                Properties.Settings.Default.StandaardZaakDocumentServicesOntvangAsynchroonService,
                Properties.Settings.Default.StandaardZaakDocumentServicesBeantwoordVraagService
            );
            var zaak = zds.GeefZaakDetails(txtZaakIdentificatie.Text);
            txtZaakTypeOmschrijving.Text = zaak.ZaakTypeOmschrijving;
            txtZaaktypeCode.Text = zaak.ZaakTypeText;
            txtZaakOmschrijving.Text = zaak.ZaakOmschrijving;            
            txtAfzender.Text = zaak.Afzender;


            var documenten = zds.GeefLijstZaakdocumenten(txtZaakIdentificatie.Text);
            foreach (var document in documenten)
            {
                lvDocumenten.Items.Add(document.Titel);
            }
            lvDocumenten.Enabled = true;
        }

        private void lvDocumenten_DragDrop(object sender, DragEventArgs e)
        {
#if !DEBUG
            try
            {
#endif
                var zds = new ZaakDocumentServices(
                    Properties.Settings.Default.StandaardZaakDocumentServicesVrijBerichtService,
                    Properties.Settings.Default.StandaardZaakDocumentServicesOntvangAsynchroonService,
                    Properties.Settings.Default.StandaardZaakDocumentServicesBeantwoordVraagService
                );
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    foreach (string file in files)
                    {
                        var zaakdocumentid = zds.GenereerDocumentidentificatie(txtZaakIdentificatie.Text);
                        var documentfile = new System.IO.FileInfo(file);
                        var documentdata = System.IO.File.ReadAllBytes(documentfile.FullName);

                        var documentmapping = new DocumentMapping(txtZaaktypeCode.Text, documentfile.Name, documentfile.CreationTime);
                        zds.VoegZaakdocumentToe(
                            txtZaakIdentificatie.Text,
                            zaakdocumentid,
                            documentmapping.Documenttype,
                            documentmapping.CreationTime,
                            documentmapping.Titel,
                            documentmapping.Formaat,
                            documentmapping.Taal,
                            documentmapping.Vertrouwelijkheid,
                            documentmapping.Mimetype,
                            documentmapping.Name,
                            documentdata);
                    }
                }
                else if (e.Data.GetDataPresent("FileGroupDescriptor"))
                {
                    OutlookDataObject dataObject = new OutlookDataObject(e.Data);
                    string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
                    System.IO.MemoryStream[] streams = (System.IO.MemoryStream[])dataObject.GetData("FileContents");
                    for (int i = 0; i < filenames.Length; i++)
                    {
                        var zaakdocumentid = zds.GenereerDocumentidentificatie(txtZaakIdentificatie.Text);
                        string filename = filenames[i];
                        System.IO.MemoryStream stream = streams[i];
                        var documentdata = stream.ToArray();
                        var mimetype = System.Web.MimeMapping.GetMimeMapping(filename);

                        var documentmapping = new DocumentMapping(txtZaaktypeCode.Text, filename, DateTime.Now);
                        zds.VoegZaakdocumentToe(
                            txtZaakIdentificatie.Text,
                            zaakdocumentid,
                            documentmapping.Documenttype,
                            documentmapping.CreationTime,
                            documentmapping.Titel,
                            documentmapping.Formaat,
                            documentmapping.Taal,
                            documentmapping.Vertrouwelijkheid,
                            documentmapping.Mimetype,
                            documentmapping.Name,
                            documentdata);
                    }
                }
                else throw new Exception("unexpected format in drap/drop");

                Cursor.Current = Cursors.WaitCursor;
                System.Threading.Thread.Sleep(500);
                System.Threading.Thread.Sleep(500);
                System.Threading.Thread.Sleep(500);
                Cursor.Current = Cursors.Default;
#if !DEBUG
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fout bij toevoegen van documenten aan de zaak", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
            Refresh();

        }

        private void lvDocumenten_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent("FileGroupDescriptor"))
            {
                    e.Effect = DragDropEffects.Copy;
            }
            else { 
                e.Effect = DragDropEffects.None;
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtZaakIdentificatie.Text = Clipboard.GetText();
            Refresh();
        }

        private void txtZaakIdentificatie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Refresh();
            }
        }
    }
}

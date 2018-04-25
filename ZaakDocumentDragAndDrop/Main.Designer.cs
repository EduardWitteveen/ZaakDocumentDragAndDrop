﻿namespace ZaakDocumentDragAndDrop
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtZaaktypeCode = new System.Windows.Forms.TextBox();
            this.btnPaste = new System.Windows.Forms.Button();
            this.txtZaakOmschrijving = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtZaakTypeOmschrijving = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAfzender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtZaakIdentificatie = new System.Windows.Forms.TextBox();
            this.lvDocumenten = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 446);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(292, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtZaaktypeCode);
            this.panel1.Controls.Add(this.btnPaste);
            this.panel1.Controls.Add(this.txtZaakOmschrijving);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtZaakTypeOmschrijving);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtAfzender);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtZaakIdentificatie);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 177);
            this.panel1.TabIndex = 1;
            // 
            // txtZaaktypeCode
            // 
            this.txtZaaktypeCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtZaaktypeCode.Enabled = false;
            this.txtZaaktypeCode.Location = new System.Drawing.Point(218, 65);
            this.txtZaaktypeCode.Name = "txtZaaktypeCode";
            this.txtZaaktypeCode.Size = new System.Drawing.Size(62, 20);
            this.txtZaaktypeCode.TabIndex = 11;
            // 
            // btnPaste
            // 
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.Location = new System.Drawing.Point(252, 22);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(29, 23);
            this.btnPaste.TabIndex = 10;
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // txtZaakOmschrijving
            // 
            this.txtZaakOmschrijving.BackColor = System.Drawing.SystemColors.Control;
            this.txtZaakOmschrijving.Enabled = false;
            this.txtZaakOmschrijving.Location = new System.Drawing.Point(12, 103);
            this.txtZaakOmschrijving.Name = "txtZaakOmschrijving";
            this.txtZaakOmschrijving.Size = new System.Drawing.Size(269, 20);
            this.txtZaakOmschrijving.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Zaakomschrijving:";
            // 
            // txtZaakTypeOmschrijving
            // 
            this.txtZaakTypeOmschrijving.BackColor = System.Drawing.SystemColors.Control;
            this.txtZaakTypeOmschrijving.Enabled = false;
            this.txtZaakTypeOmschrijving.Location = new System.Drawing.Point(12, 64);
            this.txtZaakTypeOmschrijving.Name = "txtZaakTypeOmschrijving";
            this.txtZaakTypeOmschrijving.Size = new System.Drawing.Size(200, 20);
            this.txtZaakTypeOmschrijving.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Zaaktype omschrijving:";
            // 
            // txtAfzender
            // 
            this.txtAfzender.BackColor = System.Drawing.SystemColors.Control;
            this.txtAfzender.Enabled = false;
            this.txtAfzender.Location = new System.Drawing.Point(12, 142);
            this.txtAfzender.Name = "txtAfzender";
            this.txtAfzender.Size = new System.Drawing.Size(269, 20);
            this.txtAfzender.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Afzender:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zaakidentificatie:";
            // 
            // txtZaakIdentificatie
            // 
            this.txtZaakIdentificatie.Location = new System.Drawing.Point(12, 25);
            this.txtZaakIdentificatie.Name = "txtZaakIdentificatie";
            this.txtZaakIdentificatie.Size = new System.Drawing.Size(234, 20);
            this.txtZaakIdentificatie.TabIndex = 3;
            this.txtZaakIdentificatie.TextChanged += new System.EventHandler(this.txtZaakNummer_TextChanged);
            this.txtZaakIdentificatie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZaakIdentificatie_KeyDown);
            // 
            // lvDocumenten
            // 
            this.lvDocumenten.AllowDrop = true;
            this.lvDocumenten.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDocumenten.Location = new System.Drawing.Point(0, 177);
            this.lvDocumenten.Name = "lvDocumenten";
            this.lvDocumenten.Size = new System.Drawing.Size(292, 269);
            this.lvDocumenten.TabIndex = 2;
            this.lvDocumenten.UseCompatibleStateImageBehavior = false;
            this.lvDocumenten.View = System.Windows.Forms.View.List;
            this.lvDocumenten.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvDocumenten_DragDrop);
            this.lvDocumenten.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvDocumenten_DragEnter);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 468);
            this.Controls.Add(this.lvDocumenten);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Zaakdocument Manager";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtZaakIdentificatie;
        private System.Windows.Forms.TextBox txtZaakOmschrijving;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtZaakTypeOmschrijving;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAfzender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvDocumenten;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.TextBox txtZaaktypeCode;
    }
}


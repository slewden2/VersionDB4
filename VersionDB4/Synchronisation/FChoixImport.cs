using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VersionDB4Lib.Business;
using VersionDB4Lib.ForUI;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4.Synchronisation
{
    public partial class FChoixImport : Form
    {
        private const string LBLCLIENTDEFAULT = "Base client (a importer)";
        private const string LBLCLIENTCUSTOMCIENT = "Base client (a importer en spécifique pour {0})";
        private string clientCode = string.Empty;

        public FChoixImport()
        {
            InitializeComponent();
            SetObjectToImport(null, string.Empty);
        }


        public void SetObjectToImport(ObjectToImport import, string cc)
        {
            clientCode = cc;
            if (import == null)
            {
                sqlTextBoxClient.Visible = false;
                lblNoneClient.Visible = true;
                lblNoneClient.BringToFront();
                cbMode.Items.Clear();

            }
            else
            {
                lblNoneClient.Visible = false;
                sqlTextBoxClient.Visible = true;
                lblNoneClient.BringToFront();
                sqlTextBoxClient.Text = import.ObjectSql;

                SetReferential(import.ReferencedObject);

                cbMode.Items.Clear();
                cbMode.Items.Add(new ChoixClass(EImportType.Nop));
                if (import.OriginalStatus != EImportType.Equal)
                {
                    if (import.OriginalStatus == EImportType.New)
                    {
                        cbMode.Items.Add(new ChoixClass(EImportType.New));
                    }
                    else if (import.OriginalStatus == EImportType.Different)
                    {
                        cbMode.Items.Add(new ChoixClass(EImportType.DifferentImportAsReferential));
                    }

                    cbMode.Items.Add(new ChoixClass(EImportType.DifferentImportASCustomClient));
                }
                else
                {
                    cbMode.Items.Add(new ChoixClass(EImportType.Equal));
                }

                cbMode.SelectedItem = new ChoixClass(import.Status);
            }

            EnableButton();
        }

        public void SetReferential(Object referential)
        {
            if (referential == null)
            {
                sqlTextBoxReferential.Visible = false;
                lblNoneReferential.Visible = true;
                lblNoneReferential.BringToFront();
            }
            else
            {
                lblNoneReferential.Visible = true;
                sqlTextBoxReferential.Visible = true;
                sqlTextBoxReferential.BringToFront();
                sqlTextBoxReferential.Text = referential.ObjectSql;
            }
        }

        public EImportType Choix
             => (cbMode.SelectedItem != null && cbMode.SelectedItem is ChoixClass choix) ? choix.Type : EImportType.Unkonw;

        private void CbMode_SelectedIndexChanged(object sender, EventArgs e) => EnableButton();

        private void EnableButton()
        {
            btOk.Enabled = cbMode.SelectedItem != null;
            switch (Choix)
            {
                case EImportType.Unkonw:
                    sqlTextBoxClient.BackColor = SystemColors.Window;
                    sqlTextBoxReferential.BackColor = SystemColors.Window;
                    lblTitleClient.Font = this.Font;
                    lblTitleReferential.Font = this.Font;
                    lblTitleClient.Text = LBLCLIENTDEFAULT;
                    break;
                case EImportType.Nop:
                case EImportType.Equal:
                    sqlTextBoxClient.BackColor = SystemColors.Control;
                    sqlTextBoxReferential.BackColor = SystemColors.Window;
                    lblTitleClient.Font = new Font(this.Font, FontStyle.Strikeout);
                    lblTitleReferential.Font = this.Font;
                    lblTitleClient.Text = LBLCLIENTDEFAULT;
                    break;
                case EImportType.DifferentImportAsReferential:
                    sqlTextBoxClient.BackColor = SystemColors.Window;
                    sqlTextBoxReferential.BackColor = SystemColors.Control;
                    lblTitleClient.Font = this.Font;
                    lblTitleReferential.Font = new Font(this.Font, FontStyle.Strikeout);
                    lblTitleClient.Text = LBLCLIENTDEFAULT;
                    break;
                case EImportType.DifferentImportASCustomClient: 
                    sqlTextBoxClient.BackColor = SystemColors.Window;
                    sqlTextBoxReferential.BackColor = SystemColors.Control;
                    lblTitleClient.Font = this.Font;
                    lblTitleReferential.Font = new Font(this.Font, FontStyle.Strikeout);
                    lblTitleClient.Text = string.Format(LBLCLIENTCUSTOMCIENT, clientCode);
                    break;
            }
        }

        private class ChoixClass
        {
            public ChoixClass(EImportType typ) => Type = typ;

            public EImportType Type { get; private set; }
            public override string ToString() 
                => Type.Libelle();

            public override int GetHashCode() => Type.GetHashCode();
            public override bool Equals(object obj)
                => (obj is ChoixClass ch) && this.Type == ch.Type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business;
using VersionDB4Lib.CRUD;

namespace VersionDB4
{
    public partial class FResume : Form
    {
        private Resume myResume;

        public FResume()
        {
            InitializeComponent();
            cbAction.DataSource = SqlAction.List();
            cbWhat.DataSource = TypeObject.List();
            EnableButtons();
        }

        public int ScriptId { get; set; }
        public Resume Resume
        {
            get => myResume;
            set
            {
                myResume = value;
                if (myResume != null)
                {
                    cbAction.SelectedItem = myResume.GetAction();
                    cbWhat.SelectedItem = myResume.GetWhat();
                    txtDatabase.Text = myResume.ResumeDatabase;
                    txtSchema.Text = myResume.ResumeSchema;
                    txtName.Text = myResume.ResumeName;
                    txtColumn.Text = myResume.ResumeColumn;
                }
                else
                {
                    cbAction.SelectedItem = null;
                    cbWhat.SelectedItem = null;
                    txtDatabase.Text = string.Empty;
                    txtSchema.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtColumn.Text = string.Empty;
                }

                EnableButtons();
            }
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (!ValidationPossible())
            {
                return;
            }

            if (cbAction.SelectedItem == null || !(cbAction.SelectedItem is SqlAction action) || cbWhat.SelectedItem == null || !(cbWhat.SelectedItem is TypeObject typeObject))
            {
                return;
            }

            if (myResume == null)
            { // insertion d'un nouveau
                myResume = new Resume()
                {
                    ScriptId = ScriptId,
                    ResumeDatabase = txtDatabase.Text,
                    ResumeSchema = txtSchema.Text,
                    ResumeName = txtName.Text,
                    ResumeColumn = action.SqlActionIsForTable ? txtColumn.Text : string.Empty,
                    SqlActionId = action.SqlActionId,
                    TypeObjectId = typeObject.TypeObjectId,
                    ResumeManualValidationCode = (byte)EValidation.Manuel
                };
                using var cnn = new DatabaseConnection();
                myResume.ResumeId = cnn.ExecuteScalar(Resume.SQLInsert, myResume);
            }
            else
            { // mise à jour d'un exisant
                myResume.ResumeDatabase = txtDatabase.Text;
                myResume.ResumeSchema = txtSchema.Text;
                myResume.ResumeName = txtName.Text;
                myResume.ResumeColumn = action.SqlActionIsForTable || action.SqlActionIsForIndex || action.SqlActionIsForColumn ? txtColumn.Text : string.Empty;
                myResume.SqlActionId = action.SqlActionId;
                myResume.TypeObjectId = typeObject.TypeObjectId;
                myResume.ResumeManualValidationCode = (byte)EValidation.Manuel;

                using var cnn = new DatabaseConnection();
                cnn.Execute(Resume.SQLUpdate, myResume);
            }

            this.DialogResult = DialogResult.OK;
        }


        private void EnableButtons() 
            => btOk.Enabled = ValidationPossible();

        /// <summary>
        /// Indique si on peut ou pas valider la saisie
        /// </summary>
        /// <returns>Vrai si on peut valider la saisie</returns>
        private bool ValidationPossible()
        {
            bool ok = this.cbAction.SelectedItem != null && this.cbWhat.SelectedItem != null;
            this.lblHelp.Visible = false;
            this.lblHelp2.Visible = false;
            this.lblHelp3.Visible = false;
            this.lblHelp4.Visible = string.IsNullOrWhiteSpace(this.txtName.Text);
            this.lblHelp4.Text = "Le nom d'objet est requis";
            bool editcl = false;
            if (ok)
            { // cas particuliers
                if (!(cbAction.SelectedItem is SqlAction action) || !(cbWhat.SelectedItem is TypeObject typeObject))
                {
                    return false;
                }

                if (action.SqlActionIsForTable)
                {
                    if (typeObject.TypeObjectId != TypeObject.Table && typeObject.TypeObjectId != TypeObject.View)
                    {
                        this.lblHelp.Visible = true;
                        ok = false;
                    }

                    ok = ok && !string.IsNullOrWhiteSpace(txtColumn.Text);
                    this.lblHelp4.Text = "Le nom de table est requis";
                    this.lblColumn.Text = "Colonne";
                    this.lblHelp3.Text = "Le nom de la colonne est requis";
                    this.lblHelp3.Visible = string.IsNullOrWhiteSpace(txtColumn.Text);
                    editcl = true;
                }
                else if (action.SqlActionIsForIndex)
                {
                    ok = ok && !string.IsNullOrWhiteSpace(txtColumn.Text);
                    this.lblHelp4.Text = "Le nom de table est requis";
                    this.lblColumn.Text = "Index";
                    this.lblHelp3.Text = "Le nom de l'index est requis";
                    this.lblHelp3.Visible = string.IsNullOrWhiteSpace(txtColumn.Text);
                    editcl = true;
                }
            }

            if (ok)
            {
                ok = !string.IsNullOrWhiteSpace(txtName.Text);
            }

            if (!string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                ok = ok && !string.IsNullOrWhiteSpace(txtSchema.Text);
                lblHelp2.Visible = string.IsNullOrWhiteSpace(txtSchema.Text);
            }

            txtColumn.Enabled = editcl;
            lblColumn.Enabled = editcl;
            lblObjet.Text = editcl ? "Table" : "Objet";
            return ok;
        }

        private void CbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAction.SelectedItem != null && cbAction.SelectedItem is SqlAction action)
            {
                if (action.SqlActionIsForTable)
                {
                    this.cbWhat.Enabled = false;
                    foreach (TypeObject n in this.cbWhat.Items)
                    {
                        if (n.TypeObjectId == TypeObject.Table)
                        {
                            cbWhat.SelectedItem = n;
                            btOk.Enabled = this.ValidationPossible();
                            return;
                        }
                    }
                }
            }

            cbWhat.Enabled = true;
            btOk.Enabled = this.ValidationPossible();
        }

        private void Control_SelectedIndexChanged(object sender, EventArgs e)
            => EnableButtons();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business;
using VersionDB4Lib.Business.Scripting;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.ForUI;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4.Synchronisation
{
    public partial class FImportFromBdd : Form
    {
        private Base baseClient = null;
        private EStep currentStep;

        public FImportFromBdd()
        {
            InitializeComponent();
            btnChkAll.Text = "";
            btnUnckeAll.Text = "";
            currentStep = EStep.Selection;
            pnlSelection.Dock = DockStyle.Fill;
            pnlAnalyze.Dock = DockStyle.Fill;
            pnlChoice.Dock = DockStyle.Fill;
            DisplayStep();
        }


        public void Initialize(int projectId, Base client, VersionObjectCounter version)
        {
            baseClient = client;
            FillReferentialListOfVersions(projectId, version);
            lblClientName.Text = client.ToString();
            FillTypeObject();
            EnableButtons();
        }

        private void CbVersions_SelectedIndexChanged(object sender, EventArgs e)
            => EnableButtons();

        private void BtnUnckeAll_Click(object sender, EventArgs e)
            => ChangeAllCecks(false);

        private void BtnChkAll_Click(object sender, EventArgs e)
            => ChangeAllCecks(true);

        private void FImportFromBdd_SizeChanged(object sender, EventArgs e)
        {
            lstResume.Columns[0].Width = Math.Max(lstResume.ClientSize.Width - lstResume.Columns[1].Width - 20, 100);
            lstChoice.Columns[0].Width = Math.Max(lstChoice.ClientSize.Width - lstChoice.Columns[1].Width - 20, 100);
        }

        private void LstChoice_DoubleClick(object sender, EventArgs e)
        {
            if (lstChoice.SelectedItems.Count == 1 && lstChoice.SelectedItems[0].Tag != null && lstChoice.SelectedItems[0].Tag is ObjectToImport import)
            {
                using var frm = new FChoixImport();
                Program.Settings.PositionLoad(frm);
                frm.SetObjectToImport(import, ClientCode.Name(baseClient.ClientCodeId));
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    import.Status = frm.Choix;
                    lstChoice.SelectedItems[0].ForeColor = (import.Status.IsChoice()) ? Color.Red : import.Status.IsChoice() ? Color.Red : Color.Green;
                    lstChoice.SelectedItems[0].Tag = import;
                    lstChoice.SelectedItems[0].SubItems[1].Text = import.Status.Libelle();
                    EnableButtons();
                }

                Program.Settings.PositionSave(frm);
            }
        }

        private void LstChoice_SelectedIndexChanged(object sender, EventArgs e)
            => EnableButtons();



        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (currentStep != EStep.Summary)
            {
                currentStep--;
                DisplayStep();
            }
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (currentStep == EStep.Summary)
            {
                this.DialogResult = DialogResult.OK;
            }

            currentStep++;
            DisplayStep();
            switch (currentStep)
            {
                case EStep.Analyze:
                    lblProgression.Text = "Analyse en cours...";
                    lstResume.Items.Clear();
                    DoProcessAnalyze();
                    break;
                case EStep.Object:
                    lstChoice.Columns[0].Width = Math.Max(lstChoice.ClientSize.Width - lstChoice.Columns[1].Width - 20, 100);
                    EnableButtons();
                    break;
                case EStep.Summary:
                    DoImport();
                    break;
            }
        }


        private void DisplayStep()
        {
            switch (currentStep)
            {
                case EStep.Selection:
                    lblTitre.Text = "Import depuis une base client : 1 sur 4 choix des types";
                    pnlSelection.Visible = true;
                    pnlAnalyze.Visible = false;
                    pnlChoice.Visible = false;
                    pnlSummary.Visible = false;
                    lblImportCounter.Visible = false;
                    pnlSelection.BringToFront();
                    btnOk.Text = "Analyser";
                    break;
                case EStep.Analyze:
                    lblTitre.Text = "Import depuis une base client : 2 sur 4 Analyse des types";
                    pnlSelection.Visible = false;
                    pnlAnalyze.Visible = true;
                    pnlChoice.Visible = false;
                    pnlSummary.Visible = false;
                    lblImportCounter.Visible = false;
                    pnlAnalyze.BringToFront();
                    btnOk.Text = "Suivant >>";
                    break;
                case EStep.Object:
                    lblTitre.Text = "Import depuis une base client : 3 sur 4 Import des objets";
                    pnlSelection.Visible = false;
                    pnlAnalyze.Visible = false;
                    pnlChoice.Visible = true;
                    lblImportCounter.Visible = true;
                    pnlSummary.Visible = false;
                    pnlChoice.BringToFront();
                    btnOk.Text = "Importer";
                    break;
                case EStep.Summary:
                    lblTitre.Text = "Import depuis une base client : 4 sur 4 Terminé !";
                    pnlSelection.Visible = false;
                    pnlAnalyze.Visible = false;
                    pnlChoice.Visible = false;
                    pnlSummary.Visible = true;
                    lblImportCounter.Visible = false;
                    pnlSummary.BringToFront();
                    btnOk.Text = "Terminer";
                    break;
            }
        }

        private void FillReferentialListOfVersions(int projectId, VersionObjectCounter versionToSel)
        {
            cbVersions.DataSource = null;
            using var conn = new DatabaseConnection();
            cbVersions.DataSource = conn.Query<VersionObjectCounter>(VersionObjectCounter.SQLSelect, new { ProjectId = projectId }).OrderByDescending(x => x.FullVersion).ToList();
            cbVersions.SelectedItem = versionToSel;
        }

        private void FillTypeObject()
        {
            lstTypeObject.Items.Clear();
            foreach (var typObj in TypeObject.List().Where(x => x.TypeObjectId > 0).OrderBy(x => x.TypeObjectPrestentOrder))
            {
                lstTypeObject.Items.Add(typObj);
            }

            ChangeAllCecks(true);
        }

        private void EnableButtons()
        {
            btnPrevious.Enabled = currentStep != EStep.Selection && currentStep != EStep.Summary;
            btCancel.Enabled = true;
            bool ok = true;
            switch (currentStep)
            {
                case EStep.Selection:

                    ok = cbVersions.SelectedItem != null && baseClient != null;
                    ok = ok && lstTypeObject.CheckedIndices.Count > 0;
                    break;
                case EStep.Analyze:
                    ok = lstChoice.Items.Count > 0;
                    break;
                case EStep.Object:
                    ok = ComputeListChoices();
                    lblHelp.Visible = lstChoice.SelectedItems.Count == 1 && lstChoice.SelectedItems[0].Tag != null && lstChoice.SelectedItems[0].Tag is ObjectToImport import && import.Status.IsChoice();
                    break;
            }

            btnOk.Enabled = ok;
        }

        private bool ComputeListChoices()
        {
            int actionCount = 0;
            bool isOneChoice = false;
            foreach (ListViewItem itx in lstChoice.Items)
            {
                if (itx.Tag != null && itx.Tag is ObjectToImport import)
                {
                    actionCount += import.Status.IsAction() ? 1 : 0;
                    if (import.Status.IsChoice())
                    {
                        isOneChoice = true;
                    }
                }
            }

            lblImportCounter.Text = actionCount.Counter("Aucun objet importable", "Un objet à importer", "{0} objets à importer") +  lstChoice.Items.Count.Counter(string.Empty, " sur le seul objet trouvé", " sur les {0} objets trouvés");
            return actionCount > 0 && !isOneChoice;
        }

        private void ChangeAllCecks(bool isChecked)
        {
            for (int i = 0; i < lstTypeObject.Items.Count; i++)
            {
                lstTypeObject.SetItemChecked(i, isChecked);
            }

            EnableButtons();
        }

        private void DoProcessAnalyze()
        {
            if (cbVersions.SelectedItem == null || !(cbVersions.SelectedItem is VersionObjectCounter version))
            {
                return;
            }

            lblConfirmVersion.Text = $"Sélectionnez les objets à importer dans la version {version}";
            progressBar1.Maximum = lstTypeObject.CheckedItems.Count + 1;
            progressBar1.Minimum = 0;

            // Charger le référentiel utile
            lblProgression.Text = $"Chargement du référentiel...";
            progressBar1.Value = progressBar1.Minimum;
            using var vdbCnn = new DatabaseConnection();
            var where = string.Join(", ", lstTypeObject.CheckedItems.Cast<TypeObject>().Select(x => x.TypeObjectId.ToString()));
            var lstObjects = vdbCnn.Query<Object>(Object.SQLSelect + $" AND typeObjectId IN ({where});", new { version.VersionId });

            // charger des infos deppuis la base client
            lblProgression.Text = $"Analyse de la base client...";

            ListViewItem itx;
            ListViewGroup grp;
            lstResume.Items.Clear();
            lstChoice.Items.Clear();

            int counter = 0;
            using var cnn = new DatabaseConnection(baseClient.BaseConnectionString);
            foreach (TypeObject typOb in lstTypeObject.CheckedItems)
            {
                lblProgression.Text = $"Analyse {typOb.TypeObjectPlurial.ToLower()}...";
                progressBar1.PerformStep();
                Application.DoEvents();

                bool withColumn = typOb.TypeObjectNeedColumnDefinition;

                var lst = cnn.Query<ObjectToImport>(DatabaseExtractor.SQLImportObject(typOb.TypeObjectId), new { version.VersionId });

                itx = new ListViewItem(typOb.TypeObjectPlurial);
                itx.SubItems.Add(lst.Count().Counter("Aucun objet", "Un seul objet trouvé", "{0} objets trouvés"));
                lstResume.Items.Add(itx);

                grp = new ListViewGroup(typOb.ToString());
                lstChoice.Groups.Add(grp);
                foreach (ObjectToImport x in lst)
                {
                    if (withColumn)
                    {
                        x.Columns = cnn.Query<ColumnDefinition>(DatabaseExtractor.SQLImportColumns(typOb.TypeObjectId), x).ToList();
                    }

                    counter++;
                    x.ReferencedObject = lstObjects.FirstOrDefault(o => x.IsMatch(o));
                    itx = new ListViewItem(x.ToString())
                    {
                        Group = grp,
                        Tag = x
                    };
                    if (x.Status.IsChoice())
                    {
                        itx.ForeColor = Color.Red;
                    }

                    itx.SubItems.Add(x.Status.Libelle());
                    lstChoice.Items.Add(itx);
                }
            }

            lblProgression.Text = "Fin d'analyse";
            lstResume.Items.Add(string.Empty);
            itx = new ListViewItem("Total")
            {
                Font = new Font(lstResume.Font, FontStyle.Bold)
            };
            itx.SubItems.Add(counter.Counter("Aucun objet", "Un seul objet trouvé", "{0} objets trouvés"));
            lstResume.Items.Add(itx);

            lstResume.Columns[1].Width = -1;
            lstResume.Columns[0].Width = Math.Max(lstResume.ClientSize.Width - lstResume.Columns[1].Width - 20, 100);

            lstChoice.Columns[1].Width = -1;
            lstChoice.Columns[0].Width = Math.Max(lstChoice.ClientSize.Width - lstChoice.Columns[1].Width - 20, 100);

            EnableButtons();
        }

        private void DoImport()
        {
            this.UseWaitCursor = true;
            lblFinalSumary.Text = "Import en cours...";
            lblFinalSumary.ForeColor = SystemColors.ControlText;
            lblStatusSummary.ForeColor = SystemColors.ControlText;
            lblStatusSummary.Text = "...";
            btnPrevious.Enabled = false;
            btnOk.Enabled = false;
            btCancel.Enabled = false;

            Application.DoEvents();
            try
            {
                
                int nb = 0, ok = 0;
                using var processor = new CRUDObjectProcess();
                processor.BeginTransaction();
                try
                {
                    foreach (ListViewItem itx in lstChoice.Items)
                    {
                        if (itx.Tag != null && itx.Tag is ObjectToImport import && import.Status.IsAction())
                        {
                            if (DoImport(processor, import))
                            {
                                ok++;
                            }

                            nb++;
                        }
                    }

                    processor.CommitTransaction();
                    lblFinalSumary.Text = $"{ok.Counter("Aucun objet importé", "Un seul objet importé", "{0} objets importés")} sur {nb.Counter("x", "l'objet prévu", "les {0} objets prévus")}";
                    lblFinalSumary.ForeColor = ok == nb ? Color.Green : Color.Red;
                    lblStatusSummary.ForeColor = ok == nb ? Color.Green : Color.Red;
                    lblStatusSummary.Text = ok == nb ? "" : "";
                }
                catch (Exception ex)
                {
                    string message = "Erreur de traitement : Opération totalement annulée\n\n" + ex.Message;
                    processor.RollBackTransaction();
                    lblFinalSumary.Text = message;
                    lblFinalSumary.ForeColor = Color.Red;
                    lblStatusSummary.ForeColor = Color.Red;
                    lblStatusSummary.Text = "";
                    MessageBox.Show(message, "Erreur de traitement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.UseWaitCursor = false;
            }

            EnableButtons();
        }
        private bool DoImport(CRUDObjectProcess processor, ObjectToImport import)
        {
            import.ClientCodeId = (import.Status == EImportType.DifferentImportASCustomClient) ? (int?)baseClient.ClientCodeId : null;
            int id = processor.Add(import);

            return id > 0;
        }


        private enum EStep
        {
            Selection = 0,
            Analyze,
            Object,
            Summary
        }

    }
}

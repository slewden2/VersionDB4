using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.UI;

namespace VersionDB4
{
    public partial class FDetailScript : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;


        private Script myScript;
        public FDetailScript() => InitializeComponent();

        public Script Script
        {
            set
            {
                ClearDisplay();
                myScript = value;
                if (myScript != null)
                {
                    lblTitle.Text = myScript.ToString();
                    txtSql.Text = myScript.ScriptText;
                    LoadScriptAnalyses();
                }
            }
        }

        private void LoadScriptAnalyses(bool force = false)
        {
            lstBloc.DataSource = null;
            lstDatabaseObject.DataSource = null;
            lstResume.DataSource = null;
            var analyzer = myScript.GetAnalyzer(force);
            lstBloc.DataSource = analyzer.Blocs;
            lstDatabaseObject.DataSource = analyzer.SqlObjets;
            lstResume.DataSource = analyzer.Resumes;
            lstBloc.SelectedItem = null;
            lstDatabaseObject.SelectedItem = null;
            lstResume.SelectedItem = null;
            EnableButtons();
        }

        private void ClearDisplay()
        {
            lblTitle.Text = "Script";
            txtSql.Text = string.Empty;
            lstBloc.DataSource = null;
            lstDatabaseObject.DataSource = null;
            lstResume.DataSource = null;
            EnableButtons();
        }

        private void LstBloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBloc.SelectedItem != null && lstBloc.SelectedItem is Bloc bloc)
            {
                txtSql.Select(bloc.BlocIndex, bloc.BlocLength);
            }
        }
        private void LstResume_SelectedIndexChanged(object sender, EventArgs e) => EnableButtons();

        private void EnableButtons()
        {
            var resume = lstResume.SelectedItem as Resume;
            bool isSelection = resume != null;
            btnValid.Enabled = isSelection;
            BtnRefuse.Enabled = isSelection;
            MnuMigrateAlterColumn.Enabled = isSelection && resume.SqlActionId == SqlAction.Rename && resume.TypeObjectId == TypeObject.Table && !string.IsNullOrEmpty(resume.ResumeSchema) && !string.IsNullOrWhiteSpace(resume.ResumeName);
            mnuEditResume.Enabled = isSelection;
            mnuDelResume.Enabled = isSelection;
        }

        private void BtReload_Click(object sender, EventArgs e)
        {
            lstBloc.DataSource = null;
            lstDatabaseObject.DataSource = null;
            lstResume.DataSource = null;
            var analyzer = SqlAnalyzer.Analyse(myScript.ScriptId, myScript.ScriptText);
            lstBloc.DataSource = analyzer.Blocs;
            lstDatabaseObject.DataSource = analyzer.SqlObjets;
            lstResume.DataSource = analyzer.Resumes;

            using var cnn = new DatabaseConnection();
            analyzer.Save(cnn);

            lstBloc.SelectedItem = null;
            lstDatabaseObject.SelectedItem = null;
            lstResume.SelectedItem = null;
            EnableButtons();
        }

        private void BtnValidAll_Click(object sender, EventArgs e)
        {
            using var cnn = new DatabaseConnection();
            cnn.Execute(Script.SQLValidAll, myScript);
            LoadScriptAnalyses(true);
        }

        private void BtnRefuseAll_Click(object sender, EventArgs e)
        {
            if (lstResume.SelectedItem != null && lstResume.SelectedItem is Resume resume)
            {
                using var cnn = new DatabaseConnection();
                resume.ResumeManualValidationCode = (byte)EValidation.NonValide;
                cnn.Execute(Resume.SQLUpdateValidation, resume);
                LoadScriptAnalyses(true);
            }
        }


        private void BtnValid_Click(object sender, EventArgs e)
        {
            if (lstResume.SelectedItem != null && lstResume.SelectedItem is Resume resume)
            {
                using var cnn = new DatabaseConnection();
                resume.ResumeManualValidationCode = (byte)EValidation.Valide;
                cnn.Execute(Resume.SQLUpdateValidation, resume);
                LoadScriptAnalyses(true);
            }
        }

        private void BtnPop_Click(object sender, EventArgs e)
        {
            EnableButtons();
            contextMenuStrip1.Show(btnPop.PointToScreen(new Point(0, btnPop.Height)));
        }

        private void MnuAddResume_Click(object sender, EventArgs e)
        {
            using var frm = new FResume();
            Program.Settings.PositionLoad(frm);
            frm.ScriptId = myScript.ScriptId;
            frm.Resume = null;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadScriptAnalyses(true);
            }

            Program.Settings.PositionSave(frm);
        }

        /// <summary>
        /// Migre un rename Table name en Rename Column name car il y a ambiguïté à la détection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuMigrateAlterColumn_Click(object sender, EventArgs e)
        {
            if (lstResume.SelectedItem != null && lstResume.SelectedItem is Resume resume && resume.SqlActionId == SqlAction.Rename && resume.TypeObjectId == TypeObject.Table && !string.IsNullOrEmpty(resume.ResumeSchema) && !string.IsNullOrWhiteSpace(resume.ResumeName))
            {
                if (MessageBox.Show(this, "Confirmez vous la mutation de ce résumé en 'Renomme Colonne' ?", "Confirmez", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    resume.SqlActionId = SqlAction.RenameColumn;
                    resume.TypeObjectId = TypeObject.Table;
                    resume.ResumeColumn = resume.ResumeName;
                    resume.ResumeName = resume.ResumeSchema;
                    resume.ResumeSchema = "dbo"; // Schéma par défaut 
                    using var cnn = new DatabaseConnection();
                    cnn.Execute(Resume.SQLUpdate, resume);
                    LoadScriptAnalyses(true);
                }
            }
        }

        private void MnuEditResume_Click(object sender, EventArgs e)
        {
            if (lstResume.SelectedItem != null && lstResume.SelectedItem is Resume resume)
            {
                using var frm = new FResume();
                Program.Settings.PositionLoad(frm);
                frm.Resume = resume;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadScriptAnalyses(true);
                }

                Program.Settings.PositionSave(frm);
            }
        }

        private void MnuDelResume_Click(object sender, EventArgs e)
        {
            if (lstResume.SelectedItem != null && lstResume.SelectedItem is Resume resume && resume.ResumeId > 0)
            {
                if (MessageBox.Show(this, "Etes vous certain de vouloir supprimer ce résumé ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    using var cnn = new DatabaseConnection();
                    cnn.Execute(Resume.SQLDelete, resume);
                    LoadScriptAnalyses(true);
                }
            }
        }

        #region Look

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        protected override void WndProc(ref Message m)
        {
            if (!ControlWindow.ProcessWndProcForSizingWindow(this, ref m))
            {
                base.WndProc(ref m);
            }
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawLine(SystemPens.ControlDark, 0, panel1.ClientSize.Height - 1, panel1.ClientSize.Width - 1, panel1.ClientSize.Height - 1);

        #endregion
    }
}

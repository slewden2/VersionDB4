using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
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

        private void LoadScriptAnalyses()
        {
            var analyzer = myScript.GetAnalyzer();
            lstBloc.DataSource = analyzer.Blocs;
            lstDatabaseObject.DataSource = analyzer.SqlObjets;
            lstResume.DataSource = analyzer.Resumes;
            lstBloc.SelectedItem = null;
            lstDatabaseObject.SelectedItem = null;
            lstResume.SelectedItem = null;
        }

        private void ClearDisplay()
        {
            lblTitle.Text = "Script";
            txtSql.Text = string.Empty;
            lstBloc.DataSource = null;
            lstDatabaseObject.DataSource = null;
            lstResume.DataSource = null;
        }

        private void LstBloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBloc.SelectedItem != null && lstBloc.SelectedItem is Bloc bloc)
            {
                txtSql.Select(bloc.BlocIndex, bloc.BlocLength);
            }
        }

        private void BtReload_Click(object sender, EventArgs e)
        {
            var analyzer = SqlAnalyzer.Analyse(myScript.ScriptId, myScript.ScriptText);
            lstBloc.DataSource = analyzer.Blocs;
            lstDatabaseObject.DataSource = analyzer.SqlObjets;
            lstResume.DataSource = analyzer.Resumes;

            using var cnn = new DatabaseConnection();
            analyzer.Save(cnn);
        }

        private void BtnValidAll_Click(object sender, EventArgs e)
        {
            using var cnn = new DatabaseConnection();
            cnn.Execute(Script.SQLValidAll, myScript);
            LoadScriptAnalyses();
        }

        private void BtnRefuseAll_Click(object sender, EventArgs e)
        {
            using var cnn = new DatabaseConnection();
            cnn.Execute(Script.SQLRefuseAll, myScript);
            LoadScriptAnalyses();
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

        private void FDetailScript_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        #endregion
    }
}

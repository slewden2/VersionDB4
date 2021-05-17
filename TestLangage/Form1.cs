using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;

namespace TestLangage
{
    public partial class Form1 : Form
    {
        private RegexFounding pattern;
        private readonly string fullFileName;
        private bool isChange;

        private readonly List<RegexFoundingUnitTest> datas = new List<RegexFoundingUnitTest>();
        public Form1()
        {
            InitializeComponent();

            fullFileName = Path.Combine(Application.StartupPath, "Jeux2Test.Json");

            datas = RegexFoundingUnitTest.Load(fullFileName).ToList();
            isChange = false;

            lblNo.Visible = false;
            cbAction.DataSource = SqlAction.List().Where(x => x.SqlActionId > 1).ToList();
            cbWhat.DataSource = TypeObject.List().Where(x => x.TypeObjectId >= 0).ToList();
            lblInfos.Text = string.Empty;
            Choix_SelectedIndexChanged(null, null);
            GereBoutton();
        }

        private void Choix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAction.SelectedItem != null && cbWhat.SelectedItem != null && cbAction.SelectedItem is SqlAction action && cbWhat.SelectedItem is TypeObject what)
            {
                pattern = RegexFounding.List.FirstOrDefault(x => x.Action == action.SqlActionId && x.ApplyOn == what.TypeObjectId);
                if (pattern == null)
                {
                    txtRegex.Text = "La combinaison n'existe pas (encore !!)";
                    lblInfos.Text = null;
                    return;
                }

                txtRegex.Text = pattern.Expression.ToString();
                int nb = datas.Count(x => x.SqlActionId == action.SqlActionId && x.SqlWhatId == what.TypeObjectId);
                if (nb == 0)
                {
                    lblInfos.Text = "Aucun cas de test trouvé";
                }
                else
                {
                    int ok = datas.Count(x => x.SqlActionId == action.SqlActionId && x.SqlWhatId == what.TypeObjectId && x.Result);
                    int ko = datas.Count(x => x.SqlActionId == action.SqlActionId && x.SqlWhatId == what.TypeObjectId && !x.Result);
                    lblInfos.Text = $"{ok} cas valide(s), {ko} cas invalide(s)";
                }
            }
            else
            {
                pattern = null;
                txtRegex.Text = null;
                lblInfos.Text = null;
            }

            GereBoutton();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            lblNo.Visible = false;
            lstMatch.DataSource = null;
            GereBoutton();
        }

        private void GereBoutton()
        {
            btTest.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text) && pattern != null;
            btSave.Enabled = btTest.Enabled;
        }

        private void BtTest_Click(object sender, EventArgs e)
        {
            var bal = new BlocAnalyzer();
            bal.Analyze(pattern, 0, textBox1.Text);
            
            lstMatch.DataSource = bal.Blocs;
            lblNo.Visible = lstMatch.Items.Count == 0;
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            if (cbAction.SelectedItem != null && cbWhat.SelectedItem != null 
                && cbAction.SelectedItem is SqlAction action && cbWhat.SelectedItem is TypeObject what
                && !string.IsNullOrWhiteSpace(textBox1.Text) && (lblNo.Visible || lstMatch.Items.Count > 0))
            {
                datas.Add(new RegexFoundingUnitTest()
                {
                    SqlActionId = action.SqlActionId,
                    SqlWhatId = what.TypeObjectId,
                    Text = textBox1.Text,
                    Result = !lblNo.Visible
                });
                isChange = true;
                Choix_SelectedIndexChanged(null, null);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isChange)
            {
                RegexFoundingUnitTest.Save(fullFileName, datas);
            }
        }
    }
}

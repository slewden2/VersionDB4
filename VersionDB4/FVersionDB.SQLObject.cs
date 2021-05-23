using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business.Scripting;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.ForUI;
using Object = VersionDB4Lib.CRUD.Object;
using Version = VersionDB4Lib.CRUD.Version;

namespace VersionDB4
{
    public partial class FVersionDB
    {
    
        /// <summary>
        /// Verifie que dans le script en cours d'édition pour un objet on a bien le CREATE XXX qui correspond
        /// (ET met à jour l'interface utilisateur pour le ui indiquer)
        /// </summary>
        private void AnalyseEntete()
        {
            if (currentObjectEdited == null || currentObjectEdited.TypeObjectId <= 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(sqlTextBox1.Text))
            {
                lblResumes.Text = string.Empty;
                return;
            }

            RegexFounding pattern = RegexFounding.List.FirstOrDefault(x => x.Action == SqlAction.Create && x.ApplyOn == currentObjectEdited.TypeObjectId);
            if (pattern == null)
            {
                return;
            }

            var bla = new BlocAnalyzer();
            bla.Analyze(pattern, 0, sqlTextBox1.Text);
            if (bla.Blocs.Count == 1)
            {
                currentObjectEdited.ObjectSchema = bla.Blocs[0].BlocSchema;
                currentObjectEdited.ObjectName = bla.Blocs[0].BlocName;
                lblResumes.Text = $"Trouvé {currentObjectEdited.TypeObjectName()} {currentObjectEdited}";
            }
            else
            {
                currentObjectEdited.ObjectSchema = string.Empty;
                currentObjectEdited.ObjectName = string.Empty;
                lblResumes.Text = "... Analyse du script...";
            }
        }

        private void ProcessSqlObjectEndAdd()
        {
            if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is TypeObjectCounter typeObjetAddEnd && cbVersions.SelectedItem != null && cbVersions.SelectedItem is VersionObjectCounter versionObjectCounterAddEnd)
            {
                currentObjectEdited.ObjectSql = sqlTextBox1.Text;
                AnalyseEntete();
                if (string.IsNullOrWhiteSpace(currentObjectEdited.ObjectName))
                {
                    MessageBox.Show(this, $"L'analyse du script a échoué.\nImpossible de déterminer le nom de {currentObjectEdited.TypeObjectName()}", "Insertion imposible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var crudProcess = new CRUDObjectProcess();
                int id = crudProcess.Add(currentObjectEdited);

                CancelEdition(null);
                FillReferentialTypeObject(treeView1.SelectedNode, versionObjectCounterAddEnd, typeObjetAddEnd, true);
                treeView1.SelectedNode.Expand();
                SelectNodeObject(treeView1.SelectedNode, id);
            }
        }
        private void ProcessSqlObjectEndEdit()
        {
            if (currentObjectEdited != null)
            {
                if (sqlTextBox1.Text != currentObjectEdited.ObjectSql)
                {
                    currentObjectEdited.ObjectSql = sqlTextBox1.Text;
                    AnalyseEntete();
                    if (string.IsNullOrWhiteSpace(currentObjectEdited.ObjectName))
                    {
                        MessageBox.Show(this, $"L'analyse du script a échoué.\nImpossible de déterminer le nom de {currentObjectEdited.TypeObjectName()}", "Mise à jour imposible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using var crudProcess = new CRUDObjectProcess();
                    crudProcess.Edit(currentObjectEdited);

                    if (treeView1.SelectedNode.Text != currentObjectEdited.ToString())
                    {
                        treeView1.SelectedNode.Text = currentObjectEdited.ToString();
                        treeView1.SelectedNode.Tag = currentObjectEdited;
                    }
                }

                CancelEdition(treeView1.SelectedNode);
            }
        }
        private void ProcessSqlObjectDelete()
        {
            if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Object objectDel && cbVersions.SelectedItem != null && cbVersions.SelectedItem is VersionObjectCounter versionObjectCounterDel)
            {
                if (MessageBox.Show(this, $"Etes vous certain de vouloir supprimer {objectDel} ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    using var crudProcess = new CRUDObjectProcess();
                    crudProcess.Delete(objectDel);

                    var parent = treeView1.SelectedNode.Parent;
                    if (parent != null && parent.Tag != null && parent.Tag is TypeObjectCounter typeObjectCounterDel)
                    {
                        FillReferentialTypeObject(parent, versionObjectCounterDel, typeObjectCounterDel);
                        treeView1.SelectedNode.Expand();
                        NodeSelected(parent);
                    }
                }
            }
        }
    }
}

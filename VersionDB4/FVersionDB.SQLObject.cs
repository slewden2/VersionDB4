using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business;
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
        #region Remplissage du treeView
        private void FillReferentialTreeView(int typeObjectId = 0)
        {
            treeView1.Nodes.Clear();
            if (!(cbVersions.SelectedItem is Version version))
            {
                return;
            }

            TreeNode racine = new TreeNode($"Référentiel de la version {version}")
            {
                Tag = version
            };
            treeView1.BeginUpdate();
            try
            {
                treeView1.Nodes.Add(racine);

                using var conn = new DatabaseConnection();
                var lstCounter = conn.Query<TypeObjectCounter>(TypeObjectCounter.SQLSelect, new { version.VersionId });

                foreach (var typObject in lstCounter.OrderBy(x => x.TypeObjectPrestentOrder))
                {
                    TreeNode nod = new TreeNode(typObject.TypeObjectPlurial)
                    {
                        Tag = typObject
                    };
                    racine.Nodes.Add(nod);
                    if (typObject.Count > 0)
                    {
                        nod.Nodes.Add(new TreeNode());
                    }

                    if (typObject.TypeObjectId == typeObjectId)
                    {
                        treeView1.SelectedNode = nod;
                    }
                }
            }
            finally
            {
                racine.Expand();
                treeView1.EndUpdate();
                if (treeView1.SelectedNode == null)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0];
                }
            }
        }

        private void FillReferentialTypeObject(TreeNode node, Version version, TypeObjectCounter typ, bool select = false)
        {
            node.Nodes.Clear();

            using var conn = new DatabaseConnection();
            var lst = conn.Query<ObjectWithClientSpecific>(ObjectWithClientSpecific.SQLSelect, new { version.VersionId, typ.TypeObjectId });
            int count = 0;
            foreach (var obj in lst.OrderBy(x => x.ToString()))
            {
                TreeNode nod = new TreeNode(obj.ToString())
                {
                    Tag = obj
                };

                if (obj.NumberOfClientImplementation > 0)
                {
                    nod.Nodes.Add(new TreeNode());
                }

                node.Nodes.Add(nod);
                count++;
            }

            if (count != typ.Count)
            {
                int delta = count - typ.Count;
                typ.Count = count;
                node.Tag = typ;
                node.Text = typ.ToString();

                if (node.Parent != null && node.Parent.Tag != null && node.Parent.Tag is VersionObjectCounter vers)
                {
                    vers.Count += delta;
                    node.Parent.Tag = vers;
                    cbVersions.Invalidate(); // force le redessin
                }
            }

            if (select)
            {
                treeView1.SelectedNode = node;
                treeView1.SelectedNode.Expand();
            }
        }

        private void FillCustomClientList(TreeNode node, ObjectWithClientSpecific myObject, bool select = false)
        {
            node.Nodes.Clear();

            if (myObject.NumberOfClientImplementation == 0)
            {
                return;
            }

            int count = 0;
            foreach (var obj in myObject.ObjectSpecificClientList(true).OrderBy(x => x.ToString()))
            {
                TreeNode nod = new TreeNode(obj.ToString())
                {
                    Tag = obj
                };

                node.Nodes.Add(nod);
                count++;
            }

            if (select)
            {
                treeView1.SelectedNode = node;
                treeView1.ExpandAll();
            }
        }
        #endregion

        /// <summary>
        /// Durant l'édition du Texte SQL (ou en fin de saisie) :
        /// Vérifie que dans le script en cours d'édition pour un objet on a bien le CREATE XXX qui correspond
        /// (ET met à jour l'interface utilisateur pour le ui indiquer)
        /// </summary>
        private void AnalyseEntete()
        {
            if (currentObjectEdited == null || currentObjectEdited.TypeObjectId <= 0 || string.IsNullOrWhiteSpace(sqlTextBox1.Text))
            {
                return;
            }

            RegexFounding pattern = RegexFounding.List.FirstOrDefault(x => x.Action == SqlAction.Create && x.ApplyOn == currentObjectEdited.TypeObjectId);
            if (pattern == null)
            { // pas d'analyse possible pour ce type d'objet
                lblResumes.Text = string.Empty;
                return;
            }

            var bla = new BlocAnalyzer();
            bla.Analyze(pattern, 0, sqlTextBox1.Text);
            if (bla.Blocs.Count == 1)
            {
                currentObjectEdited.ObjectSchema = bla.Blocs[0].BlocSchema;
                currentObjectEdited.ObjectName = bla.Blocs[0].BlocName;
                lblResumes.Text = $"Trouvé {currentObjectEdited.GetTypeObject().TypeObjectName} {currentObjectEdited}";
            }
            else
            {
                currentObjectEdited.ObjectSchema = string.Empty;
                currentObjectEdited.ObjectName = string.Empty;
                lblResumes.Text = "Analyse du script...";
            }
        }

        /// <summary>
        /// Démarre une saisie d'objet SQL (implémentation de base ou custom client)
        /// </summary>
        /// <param name="validAction"></param>
        /// <param name="title"></param>
        private void BeginAddSqlObjectUpdateDisplay(EAction validAction, string title)
        {
            lblType.Text = title;
            sqlTextBox1.Text = currentObjectEdited.ObjectSql;
            lblResumes.Visible = true;
            lblResumes.Text = string.Empty;
            SetRightPanel(ERightPanelMode.TextSqlEdition);
            ActionsFill(new List<EAction>() { validAction, EAction.Cancel }, 0);
        }

        private void ProcessSqlObjectAddBegin()
        {
            if (treeView1.SelectedNode.Tag == null || !(treeView1.SelectedNode.Tag is TypeObjectCounter objectAdd) || cbVersions.SelectedItem == null || !(cbVersions.SelectedItem is VersionObjectCounter versionObjectCounter))
            {
                return;
            }

            currentObjectEdited = new ObjectToImport()
            {
                VersionId = versionObjectCounter.VersionId,
                TypeObjectId = objectAdd.TypeObjectId,
                ObjectSql = string.Empty,
            };

            BeginAddSqlObjectUpdateDisplay(EAction.SqlObjectAddEnd, $"Ajout d'un élément de type {objectAdd.TypeObjectName} à la version {versionObjectCounter}");
        }
        private void ProcessSqlObjectEditBegin()
        {
            if (treeView1.SelectedNode.Tag == null || !(treeView1.SelectedNode.Tag is Object objectEdited) || cbVersions.SelectedItem == null || !(cbVersions.SelectedItem is VersionObjectCounter))
            {
                return;
            }

            currentObjectEdited = new ObjectToImport(objectEdited);
            BeginAddSqlObjectUpdateDisplay(EAction.SqlObjectEditEnd, $"Modification de {objectEdited}");
        }
        private void ProcessSqlObjectAddCustomClientBegin()
        {
            if (treeView1.SelectedNode.Tag == null || !(treeView1.SelectedNode.Tag is ObjectWithClientSpecific objectAdd) || cbVersions.SelectedItem == null || !(cbVersions.SelectedItem is VersionObjectCounter versionObjectCounter))
            {
                return;
            }

            using var frm = new FCustomClient();
            Program.Settings.PositionLoad(frm);
            frm.Refill(ClientCode.List().Except(objectAdd.ClientCodeList()).ToList());
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                currentObjectEdited = new ObjectToImport()
                {
                    VersionId = versionObjectCounter.VersionId,
                    TypeObjectId = objectAdd.TypeObjectId,
                    ObjectSchema = objectAdd.ObjectSchema,
                    ObjectName = objectAdd.ObjectName,
                    ObjectColumn = objectAdd.ObjectColumn,
                    ObjectDeleted = false,
                    ObjectEmpty = false,
                    ClientCodeId = frm.ClientCode.ClientCodeId,
                    ObjectSql = objectAdd.ObjectSql
                };

                BeginAddSqlObjectUpdateDisplay(EAction.SqlObjectAddCustomClientEnd, $"Ajout d'une implémentation pour {frm.ClientCode.ClientCodeName} de {objectAdd}");
            }

            Program.Settings.PositionSave(frm);
        }

        private void ProcessSqlObjectAddEnd()
        {
            if (treeView1.SelectedNode.Tag == null || !(treeView1.SelectedNode.Tag is TypeObjectCounter typeObjetAddEnd) || cbVersions.SelectedItem == null || !(cbVersions.SelectedItem is VersionObjectCounter versionObjectCounterAddEnd))
            {
                return;
            }

            TreeNode node = null;
            if (currentObjectEdited != null && sqlTextBox1.Text != currentObjectEdited.ObjectSql)
            {
                currentObjectEdited.ObjectSql = sqlTextBox1.Text;
                AnalyseEntete();
                if (string.IsNullOrWhiteSpace(currentObjectEdited.ObjectName))
                {
                    MessageBox.Show(this, $"L'analyse du script a échoué.\nImpossible de déterminer le nom de {currentObjectEdited.GetTypeObject().TypeObjectName}", "Insertion imposible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var crudProcess = new CRUDObjectProcess();
                int id = crudProcess.Add(currentObjectEdited);

                FillReferentialTypeObject(treeView1.SelectedNode, versionObjectCounterAddEnd, typeObjetAddEnd, true);
                node = SelectNodeObject(treeView1.SelectedNode, id);
            }

            CancelEdition(node);
        }
        private void ProcessSqlObjectEditEnd()
        {
            if (currentObjectEdited != null && sqlTextBox1.Text != currentObjectEdited.ObjectSql)
            {
                currentObjectEdited.ObjectSql = sqlTextBox1.Text;
                AnalyseEntete();
                if (string.IsNullOrWhiteSpace(currentObjectEdited.ObjectName))
                {
                    MessageBox.Show(this, $"L'analyse du script a échoué.\nImpossible de déterminer le nom de {currentObjectEdited.GetTypeObject().TypeObjectName}", "Mise à jour imposible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var crudProcess = new CRUDObjectProcess();
                crudProcess.Edit(currentObjectEdited);

                if ((!currentObjectEdited.ClientCodeId.HasValue || currentObjectEdited.ClientCodeId.Value <= 0) && treeView1.SelectedNode.Text != currentObjectEdited.ToString())
                {
                    treeView1.SelectedNode.Text = currentObjectEdited.ToString();
                    treeView1.SelectedNode.Tag = currentObjectEdited;
                }
            }

            CancelEdition(treeView1.SelectedNode);
        }
        private void ProcessSqlObjectAddCustomClientEnd()
        {
            if (treeView1.SelectedNode.Tag == null || !(treeView1.SelectedNode.Tag is ObjectWithClientSpecific ObjetAddEnd) || currentObjectEdited == null)
            {
                return;
            }

            if (sqlTextBox1.Text == currentObjectEdited.ObjectSql)
            {
                MessageBox.Show(this, $"Aucun changement entre l'implémentation et la version de départ", "Insertion imposible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentObjectEdited.ObjectSql = sqlTextBox1.Text;
            AnalyseEntete();
            if (string.IsNullOrWhiteSpace(currentObjectEdited.ObjectName))
            {
                MessageBox.Show(this, $"L'analyse du script a échoué.\nImpossible de déterminer le nom de {currentObjectEdited.GetTypeObject().TypeObjectName}", "Insertion imposible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var crudProcess = new CRUDObjectProcess();
            int id = crudProcess.Add(currentObjectEdited);

            FillCustomClientList(treeView1.SelectedNode, ObjetAddEnd, true);
            var node = SelectNodeObject(treeView1.SelectedNode, id);

            CancelEdition(node);
        }

        private void ProcessSqlObjectDelete()
        {
            if (treeView1.SelectedNode.Tag == null || !(treeView1.SelectedNode.Tag is Object objectDel) || cbVersions.SelectedItem == null || !(cbVersions.SelectedItem is VersionObjectCounter versionObjectCounterDel))
            {
                return;
            }

            if (MessageBox.Show(this, $"Etes vous certain de vouloir supprimer {objectDel} ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            var objectisNotAnImplementation = treeView1.SelectedNode.Tag as ObjectWithClientSpecific;

            using var crudProcess = new CRUDObjectProcess();
            crudProcess.Delete(objectDel, objectisNotAnImplementation?.NumberOfClientImplementation ?? 0);

            var parent = treeView1.SelectedNode.Parent;
            if (parent != null && parent.Tag != null)
            {
                if (parent.Tag is TypeObjectCounter typeObjectCounterDel)
                {
                    FillReferentialTypeObject(parent, versionObjectCounterDel, typeObjectCounterDel, true);
                    treeView1.SelectedNode.Expand();
                    DisplayNodeSelected(parent);
                }
                else if (parent.Tag is ObjectWithClientSpecific objectWithSpecific)
                {
                    FillCustomClientList(parent, objectWithSpecific, true);
                    treeView1.SelectedNode.Expand();
                    DisplayNodeSelected(parent);
                }
            }
        }
    }
}

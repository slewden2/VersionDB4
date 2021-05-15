using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;
using Version = VersionDB4Lib.CRUD.Version;
using Object = VersionDB4Lib.CRUD.Object;
using VersionDB4Lib.Business;
using VersionDB4Lib.ForUI;
using VersionDB4Lib.UI;
using VersionDB4Lib.Business.SqlAnalyze;

namespace VersionDB4
{
    public partial class FVersionDB : Form
    {
        #region Properties
        private readonly int projectId = 1;

        private readonly Project project;
        private Version lastVersion;
        private bool versionListIsDirty = true;

        private Script currentScriptEdited = null;
        #endregion

        public FVersionDB()
        {
            InitializeComponent();

            rdClients.Checked = false;
            rdReferential.Checked = false;
            rdScript.Checked = true;

            SetRightPanel(ERightPanelMode.TextSqlReadOnly);

            using var cnn = new DatabaseConnection();
            var filter = new { ProjectId = projectId };
            project = cnn.QueryFirstOrDefault<Project>(Project.SQLSelect + " WHERE ProjectId = @ProjectId;", filter);

            this.Text = $"VersionDB4 - {project}";

            FillReferentialListOfVersions();
        }

        #region Events
        private void FVersionDB_Load(object sender, EventArgs e) 
            => ProcessAction(EAction.ProjectScriptReload);

        private void CbVersions_SelectedIndexChanged(object sender, EventArgs e)
            => ProcessAction(EAction.ProjectReferentialReload);

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Tag == null)
            {
                if (e.Node.Tag is VersionScriptCounter version)
                { // faut remplir la collection de scripts
                    FillScriptVersion(e.Node, version);
                }
                else if (e.Node.Tag is TypeObjectCounter typ && cbVersions.SelectedItem is Version version2)
                {
                    FillReferentialTypeObject(e.Node, version2, typ);
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e) 
            => NodeSelected(e.Node);

        private void BtActions_Click(object sender, EventArgs e)
        {
            if (sender is Button bt && bt.Tag != null && bt.Tag is EAction action && treeView1.SelectedNode != null)
            {
                ProcessAction(action);
            }
        }

        private void RdScript_Click(object sender, EventArgs e)
            => ProcessAction(EAction.ProjectScriptReload);

        private void RdClients_Click(object sender, EventArgs e)
            => ProcessAction(EAction.ClientsReload);

        private void RdReferential_Click(object sender, EventArgs e)
            => ProcessAction(EAction.ProjectReferentialReload);
        #endregion

        private void ProcessAction(EAction action)
        {
            switch (action)
            {
                case EAction.Cancel:
                    CancelEdition(treeView1.SelectedNode);
                    break;
                case EAction.ClientsReload:
                    pnlVersion.Visible = false;
                    lblVersion.Visible = true;
                    FillClientTreeView();
                    break;
                case EAction.ClientAdd:
                    int baseNewId = ClientNew();
                    if (baseNewId > 0)
                    {
                        FillClientTreeView(baseNewId);
                    }
                    break;
                case EAction.ClientEdit:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Base clientEdit && ClientEdit(clientEdit))
                    {
                        FillClientTreeView(clientEdit.BaseId);
                    }

                    break;
                case EAction.ClientDel:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Base clientDel)
                    {
                        if (MessageBox.Show(this, $"Etes vous certain de vouloir supprimer la base client {clientDel} ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            using var cnn = new DatabaseConnection();
                            cnn.Execute(Base.SQLDelete, clientDel);
                            FillClientTreeView();
                        }
                        
                    }

                    break;
                case EAction.ProjectScriptReload:
                    pnlVersion.Visible = false;
                    lblVersion.Visible = false;
                    FillScriptTreeView();
                    break;
                case EAction.ProjectReferentialReload:
                    FillReferentialListOfVersions();
                    pnlVersion.Visible = true;
                    lblVersion.Visible = false;
                    FillReferentialTreeView();
                    break;
                case EAction.ProjectVersionScriptAdd:
                    int versionNewId = NewVersion();
                    if (versionNewId > 0)
                    {
                        FillScriptTreeView(versionNewId);
                        versionListIsDirty = true;
                    }
                    break;
                case EAction.ProjetVersionScriptDelete:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionDelete)
                    {
                        if (MessageBox.Show(this, $"Etes vous certain de vouloir supprimer la version {versionDelete} ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            using var cnn = new DatabaseConnection();
                            cnn.Execute(Version.SQLDelete, versionDelete);
                            versionListIsDirty = true;
                            FillScriptTreeView();
                        }
                    }

                    break;
                ////case EAction.VersionScriptRefresh:
                ////    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionCounter)
                ////    {
                ////        FillScriptVersion(treeView1.SelectedNode, versionCounter);
                ////        treeView1.SelectedNode.Expand();
                ////    }

                ////    break;
                case EAction.ScriptBeginAdd:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionCounterAdd)
                    {
                        currentScriptEdited = new Script()
                        {
                            VersionId = versionCounterAdd.VersionId,
                            Version = versionCounterAdd,
                            ScriptOrder = versionCounterAdd.CountScript + 1
                        };
                        lblType.Text = $"Ajout d'un script à la version {versionCounterAdd.FullVersion}";
                        TxtScriptText.Text = string.Empty;
                        SetRightPanel(ERightPanelMode.TextSqlEdition);
                        ActionsFill(new List<EAction>() { EAction.ScriptEndAdd, EAction.Cancel });
                    }

                    break;
                case EAction.ScriptEndAdd:
                    if (currentScriptEdited != null && treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionCounterAdd2)
                    {
                        currentScriptEdited.ScriptText = TxtScriptText.Text;
                        using var cnn = new DatabaseConnection();
                        int id = cnn.ExecuteScalar(Script.SQLInsert, currentScriptEdited);
                        var analyzer = SqlAnalyzer.Analyse(id, currentScriptEdited.ScriptText);
                        analyzer.Save(cnn);
                        CancelEdition(null);
                        FillScriptVersion(treeView1.SelectedNode, versionCounterAdd2);
                        treeView1.SelectedNode.Expand();
                        SelectNodeScript(treeView1.SelectedNode, id);
                    }

                    break;
                case EAction.ScriptBeginEdit:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Script script)
                    {
                        lblType.Text = $"Modification du script {script}";
                        SetRightPanel(ERightPanelMode.TextSqlEdition);
                        currentScriptEdited = script;
                        ActionsFill(new List<EAction>() { EAction.ScriptEndEdit, EAction.Cancel });
                    }

                    break;
                case EAction.ScriptEndEdit:
                    if (currentScriptEdited != null)
                    {
                        currentScriptEdited.ScriptText = TxtScriptText.Text;
                        var analyzer = SqlAnalyzer.Analyse(currentScriptEdited.ScriptId, currentScriptEdited.ScriptText);
                        using var cnn = new DatabaseConnection();
                        cnn.Execute(Script.SQLUpdate, currentScriptEdited);
                        analyzer.Save(cnn);
                        CancelEdition(treeView1.SelectedNode);
                    }

                    break;
                case EAction.ScriptAnalyze:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Script scriptA)
                    {
                        using (var frm = new FDetailScript())
                        {
                            frm.Script = scriptA;
                            frm.ShowDialog(this);
                        }
                    }

                    break;
                case EAction.ScriptDelete:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Script scriptDel)
                    {
                        if (MessageBox.Show(this, $"Etes vous certain de vouloir supprimer le script {scriptDel} ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            using var cnn = new DatabaseConnection();
                            cnn.Execute(Script.SQLDelete, scriptDel);

                            var parent = treeView1.SelectedNode.Parent;
                            if (parent != null && parent.Tag != null && parent.Tag is VersionScriptCounter versionWithDeletedScript)
                            {
                                FillScriptVersion(parent, versionWithDeletedScript, true);
                                treeView1.SelectedNode.Expand();
                                NodeSelected(parent);
                            }
                        }
                    }

                    break;
                default:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is IPresentable presentable)
                    {
                        MessageBox.Show($"TODO {action} on {presentable}");
                    }

                    break;
            }
        }

        #region Remplissage du treeView
        private void FillReferentialListOfVersions()
        {
            if (versionListIsDirty)
            {
                cbVersions.DataSource = null;
                using var conn = new DatabaseConnection();
                cbVersions.DataSource = conn.Query<VersionObjectCounter>(VersionObjectCounter.SQLSelect, new { ProjectId = projectId }).OrderByDescending(x => x.VersionNumber()).ToList();
                versionListIsDirty = false;
            }
        }

        private void FillClientTreeView(int id = 0)
        {
            treeView1.Nodes.Clear();
            var cl = new Clients();
            TreeNode racine = new TreeNode(cl.ToString())
            {
                Tag = cl
            };
            treeView1.BeginUpdate();
            try
            {
                treeView1.Nodes.Add(racine);

                using var conn = new DatabaseConnection();
                var lstClient = conn.Query<Base>(Base.SQLSelect + ";");

                foreach (var client in lstClient.OrderBy(x => x.BaseName))
                {
                    TreeNode nod = new TreeNode(client.BaseName)
                    {
                        Tag = client
                    };
                    racine.Nodes.Add(nod);

                    if (client.BaseId == id)
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

        private void FillScriptTreeView(int versionId = 0)
        {
            treeView1.Nodes.Clear();
            TreeNode racine = new TreeNode($"Scripts par version")
            {
                Tag = project
            };
            treeView1.BeginUpdate();
            try
            {
                treeView1.Nodes.Add(racine);

                using var conn = new DatabaseConnection();
                var lstCounter = conn.Query<VersionScriptCounter>(VersionScriptCounter.SQLSelect + ";", new { ProjectId = projectId });

                foreach (var versionCounter in lstCounter.OrderBy(x => x.VersionNumber()))
                {
                    TreeNode nod = new TreeNode(versionCounter.ToString())
                    {
                        Tag = versionCounter
                    };
                    racine.Nodes.Add(nod);
                    if (versionCounter.CountScript > 0)
                    {
                        nod.Nodes.Add(new TreeNode());
                    }

                    if (lastVersion == null || versionCounter > lastVersion)
                    {
                        lastVersion = versionCounter;
                    }

                    if (versionCounter.VersionId == versionId)
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

        private void FillReferentialTreeView(int typeObjectId = 0)
        {
            treeView1.Nodes.Clear();
            lblVersion.Text = string.Empty;
            if (cbVersions.SelectedItem is Version version)
            {
                lblVersion.Text = version.ToString();
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
                        string tit = typObject.TypeObjectPlurial;
                        if (typObject.Count > 0)
                        {
                            tit += $" ({typObject.Count})";
                        }

                        TreeNode nod = new TreeNode(tit)
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
        }
        private void FillScriptVersion(TreeNode node, VersionScriptCounter versionCounter, bool select = false)
        {
            node.Nodes.Clear();
            var conn = new DatabaseConnection();
            var lst = conn.Query<Script>(Script.SQLSelect + " WHERE VersionId = @VersionId;", new { versionCounter.VersionId });
            int count = 0;
            foreach (var script in lst.OrderBy(x => x.ToString()))
            {
                script.Version = versionCounter;
                TreeNode nod = new TreeNode(script.ToString())
                {
                    Tag = script
                };
                node.Nodes.Add(nod);
                count++;
            }

            if (count != versionCounter.CountScript)
            {
                versionCounter.CountScript = count;
                node.Tag = versionCounter;
                node.Text = versionCounter.ToString();
            }

            if (select)
            {
                treeView1.SelectedNode = node;
            }
        }

        private void FillReferentialTypeObject(TreeNode node, Version version, TypeObjectCounter typ)
        {
            node.Nodes.Clear();

            using var conn = new DatabaseConnection();
            var lst = conn.Query<Object>(Object.SQlSelectWithVersionAndType, new { version.VersionId, typ.TypeObjectId });
            foreach (var obj in lst.OrderBy(x => x.ToString()))
            {
                TreeNode nod = new TreeNode(obj.ToString())
                {
                    Tag = obj
                };
                node.Nodes.Add(nod);
            }
        }

        private void NodeSelected(TreeNode selectedNode)
        {
            lblType.Text = string.Empty;
            TxtScriptText.Text = string.Empty;
            lblResumes.Text = string.Empty;

            if (selectedNode != null && selectedNode.Tag != null && selectedNode.Tag is IPresentable presentable)
            {
                lblType.Text = presentable.ToString();
                var cat = presentable.GetCategory();
                switch (cat)
                {
                    case ETypeObjectPresentable.Project:
                        SetRightPanel(ERightPanelMode.List);
                        break;
                    case ETypeObjectPresentable.VersionScript:
                        if (selectedNode.Tag is VersionScriptCounter version)
                        {
                            lblType.Text = version.FullVersion;
                            versionScriptControl1.Version = version;
                        }

                        SetRightPanel(ERightPanelMode.VersionScript);
                        break;
                    case ETypeObjectPresentable.Clients:
                        SetRightPanel(ERightPanelMode.List);
                        break;
                    case ETypeObjectPresentable.Client:
                        if (selectedNode.Tag != null && selectedNode.Tag is Base baseClient)
                        {
                            baseClientControl1.ClientBase = baseClient;
                            SetRightPanel(ERightPanelMode.BaseClient);
                        }
                        break;
                    case ETypeObjectPresentable.VersionReferential:
                        lblType.Text = $"Version {presentable}";
                        SetRightPanel(ERightPanelMode.List);
                        break;
                    case ETypeObjectPresentable.SqlGroup:
                        lblType.Text = $"Liste des {presentable}";
                        SetRightPanel(ERightPanelMode.List);
                        break;
                    case ETypeObjectPresentable.SqlObject:
                        SetRightPanel(ERightPanelMode.TextSqlReadOnly);
                        var parent = selectedNode.Parent;
                        if (parent != null && parent.Tag is TypeObject typ2)
                        {
                            lblType.Text = $"{typ2.TypeObjectName} {presentable}";
                        }

                        if (selectedNode.Tag is Object myobject)
                        {
                            TxtScriptText.Text = myobject.ObjectSql;
                            SqlColorizer.Colorise(TxtScriptText);
                        }

                        break;
                    case ETypeObjectPresentable.Script:
                        SetRightPanel(ERightPanelMode.TextSqlReadOnly);
                        if (selectedNode.Tag is Script script)
                        {
                            TxtScriptText.Text = script.ScriptText;
                            var analyzer = script.GetAnalyzer();
                            lblResumes.Text = analyzer.ResumeText;
                            SqlColorizer.Colorise(TxtScriptText);
                        }

                        break;
                }

                ActionsFill(cat.GetActions(presentable));
            }
            else
            {
                ActionsClear();
            }
        }

        private void SelectNodeScript(TreeNode node, int scriptId)
        {
            foreach (TreeNode nod in node.Nodes)
            {
                if (nod.Tag != null && nod.Tag is Script script && script.ScriptId == scriptId)
                {
                    treeView1.SelectedNode = nod;
                    return;
                }
            }
        }
        ////private void SelectNodeVersion(TreeNode node, int versionId)
        ////{
        ////    foreach (TreeNode nod in node.Nodes)
        ////    {
        ////        if (nod.Tag != null && nod.Tag is VersionScriptCounter version && version.VersionId == versionId)
        ////        {
        ////            treeView1.SelectedNode = nod;
        ////            return;
        ////        }
        ////    }
        ////}

        #endregion

        #region Edition

        private void SetRightPanel(ERightPanelMode mode)
        {
            switch(mode)
            {
                case ERightPanelMode.BaseClient:
                    versionScriptControl1.Visible = false;
                    TxtScriptText.Visible = false;
                    baseClientControl1.Visible = true;
                    break;
                case ERightPanelMode.TextSqlEdition:
                    versionScriptControl1.Visible = false;
                    TxtScriptText.Visible = true;
                    TxtScriptText.Enabled = true;
                    baseClientControl1.Visible = false;
                    break;
                case ERightPanelMode.TextSqlReadOnly:
                    versionScriptControl1.Visible = false;
                    TxtScriptText.Visible = true;
                    TxtScriptText.Enabled = false;
                    baseClientControl1.Visible = false;
                    break;
                case ERightPanelMode.VersionScript:
                    versionScriptControl1.Visible = true;
                    TxtScriptText.Visible = false;
                    TxtScriptText.Enabled = false;
                    baseClientControl1.Visible = false;
                    break;
                default:
                    versionScriptControl1.Visible = false;
                    TxtScriptText.Visible = false;
                    baseClientControl1.Visible = false;
                    break;
            }
        }

        private void ActionsClear()
        {
            foreach (var ctrl in pnlActions.Controls)
            {
                if (ctrl is Button bt)
                {
                    bt.Click -= BtActions_Click;
                }
            }

            pnlActions.Controls.Clear();
        }

        private void ActionsFill(IEnumerable<EAction> lstAction)
        {
            ActionsClear();
            int mw = 0;
            foreach (EAction action in lstAction)
            {
                Button bt = new Button
                {
                    Name = action.ToString(),
                    Tag = action,
                    Text = action.GetIcon(),
                    ForeColor = action.GetColor(),
                    Font = new Font("Segoe MDL2 Assets", 12f),
                    FlatStyle = FlatStyle.Flat,
                    Height = 30,
                    Width = 30,
                    Top = 1,
                    Left = mw
                };
                bt.FlatAppearance.BorderSize = 0;
                bt.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 239, 247);
                pnlActions.Controls.Add(bt);
                bt.Click += BtActions_Click;
                mw += bt.Width + 10;
            }
        }

        private void CancelEdition(TreeNode selectedNode)
        {
            SetRightPanel(ERightPanelMode.TextSqlReadOnly);
            currentScriptEdited = null;
            NodeSelected(selectedNode);
        }

        private int NewVersion()
        {
            using var frm = new FNewVersion();
            frm.SetVersion(
                    new Version() { ProjectId = projectId, VersionPrincipal = lastVersion.VersionPrincipal, VersionSecondary = lastVersion.VersionSecondary + 1 },
                    new Version() { ProjectId = projectId, VersionPrincipal = lastVersion.VersionPrincipal + 1, VersionSecondary = 0 });
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                using var cnn = new DatabaseConnection();
                return cnn.ExecuteScalar(Version.SQLInsert, frm.Choice());
            }

            return 0;
        }

        private int ClientNew()
        {
            using var frm = new FEditBase();
            bool res = frm.ShowDialog(this) == DialogResult.OK;
            if (res)
            {
                using var cnn = new DatabaseConnection();
                return cnn.ExecuteScalar(Base.SQLInsert, frm.Base);
            }

            return 0;
        }

        private bool ClientEdit(Base client)
        {
            using var frm = new FEditBase();
            frm.Base = client;
            bool res = frm.ShowDialog(this) == DialogResult.OK;
            if (res)
            {
                using var cnn = new DatabaseConnection();
                cnn.Execute(Base.SQLUpdate, frm.Base);
                return true;
            }

            return false;
        }
        #endregion


        private enum ERightPanelMode
        {
            TextSqlReadOnly,
            TextSqlEdition,
            BaseClient,
            VersionScript,
            List
        }
    }
}

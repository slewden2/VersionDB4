using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4.Synchronisation;
using VersionDB4Lib.Business;
using VersionDB4Lib.Business.Scripting;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.ForUI;
using VersionDB4Lib.UI;
using Object = VersionDB4Lib.CRUD.Object;
using Version = VersionDB4Lib.CRUD.Version;

namespace VersionDB4
{
    public partial class FVersionDB : Form
    {
        #region Properties
        private readonly int projectId = 1;

        private readonly Project project;
        private Version lastVersion;

        private Script currentScriptEdited = null;
        private ObjectToImport currentObjectEdited = null;
        private Button buttonOk = null;
        private bool processing;
        #endregion

        public FVersionDB()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.controlWindow1.BringToFront();

            ////rdClients.Checked = false;
            ////rdReferential.Checked = false;
            ////rdScript.Checked = true;

            //SetRightPanel(ERightPanelMode.TextSqlReadOnly);

            using var cnn = new DatabaseConnection();
            var filter = new { ProjectId = projectId };
            project = cnn.QueryFirstOrDefault<Project>(Project.SQLSelect + " WHERE ProjectId = @ProjectId;", filter);

            lblTitle.Text = $"VersionDB4 - {project}";

            FillReferentialListOfVersions();
        }

        #region Events

        private void FVersionDB_Load(object sender, EventArgs e)
        {
            switch(Program.Settings.FVersionDBDefaultSceen)
            {
                case EAction.ProjectScriptReload:
                    rdScript.Checked = true;
                    break;
                case EAction.ClientsReload:
                    rdClients.Checked = true;
                    break;
                case EAction.ProjectReferentialReload:
                    rdReferential.Checked = true;
                    break;
                default:
                    Program.Settings.FVersionDBDefaultSceen = EAction.ProjectScriptReload;
                    rdScript.Checked = true;
                    break;

            }

            ProcessAction(Program.Settings.FVersionDBDefaultSceen);
        }

        private void CbVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!processing)
            {
                ProcessAction(EAction.ProjectReferentialReload);
            }

            lblVersion.Text = (cbVersions.SelectedItem != null && cbVersions.SelectedItem is Version version) ? version.ToString() : string.Empty;
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Tag == null)
            {
                if (e.Node.Tag is VersionScriptCounter version)
                { // faut remplir la collection de scripts
                    FillScriptVersion(e.Node, version);
                }
                else if (e.Node.Tag is TypeObjectCounter typObject && cbVersions.SelectedItem is Version version2)
                { // Faut remplir la liste des objets d'un type
                    FillReferentialTypeObject(e.Node, version2, typObject);
                }
                else if (e.Node.Tag is ObjectWithClientSpecific objectWithclientSpecific)
                { // Faut remplir les implémentations clients d'un objet
                    FillCustomClientList(e.Node, objectWithclientSpecific);
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
            => DisplayNodeSelected(e.Node);

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

        private void VersionScriptControl1_OnLinkReferential(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter version)
            {
                SelectionneVersionObjet(version.VersionId);
                rdReferential.Checked = true;
                ProcessAction(EAction.ProjectReferentialReload);
            }
        }

        private void SqlTextBox1_OnChange(object sender, EventArgs e)
        {
            AnalyseEntete();
            GereBoutonOk();
        }


        #endregion

        private void ProcessAction(EAction action)
        {
            switch (action)
            {
                case EAction.Cancel:
                    CancelEdition(treeView1.SelectedNode);
                    break;
                #region Onglets Scripts/Clients/Objets
                case EAction.ClientsReload:
                    pnlVersion.Visible = false;
                    lblVersion.Visible = true;
                    Program.Settings.FVersionDBDefaultSceen = EAction.ClientsReload; 
                    FillClientTreeView();
                    SetRightPanel(ERightPanelMode.Clients);
                    break;
                case EAction.ProjectScriptReload:
                    pnlVersion.Visible = false;
                    lblVersion.Visible = false;
                    Program.Settings.FVersionDBDefaultSceen = EAction.ProjectScriptReload;
                    FillScriptTreeView();
                    SetRightPanel(ERightPanelMode.Versions);
                    break;
                case EAction.ProjectReferentialReload:
                    FillReferentialListOfVersions();
                    pnlVersion.Visible = true;
                    lblVersion.Visible = false;
                    Program.Settings.FVersionDBDefaultSceen = EAction.ProjectReferentialReload;
                    FillReferentialTreeView();
                    SetRightPanel(ERightPanelMode.Referential);
                    break;
                #endregion
                #region Clients
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
                case EAction.ClientDBToReferential:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Base clientImport && cbVersions.SelectedItem != null && cbVersions.SelectedItem is VersionObjectCounter versionImport)
                    {
                        using var frm = new FImportFromBdd();
                        Program.Settings.PositionLoad(frm);
                        frm.Initialize(projectId, clientImport, versionImport);
                        if (frm.ShowDialog(this) == DialogResult.OK)
                        {
                            // NOTHING TODO !!
                        }
                        Program.Settings.PositionSave(frm);

                    }
                    break;
                #endregion
                #region Versions 
                case EAction.VersionAdd:
                    int versionNewId = NewVersion();
                    if (versionNewId > 0)
                    {
                        FillScriptTreeView(versionNewId);
                    }
                    break;
                case EAction.VersionDelete:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionDelete)
                    {
                        if (MessageBox.Show(this, $"Etes vous certain de vouloir supprimer la version {versionDelete} ?", "Confirmez la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            using var cnn = new DatabaseConnection();
                            cnn.Execute(Version.SQLDelete, versionDelete);
                            FillScriptTreeView();
                        }
                    }

                    break;
                case EAction.VersionLock:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionLock)
                    {
                        if (MessageBox.Show(this, $"Etes vous certain de vouloir vérouiller la version {versionLock} ?", "Confirmez le verrouillage", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            using var cnn = new DatabaseConnection();
                            versionLock.VersionIsLocked = true;
                            cnn.Execute(Version.SQLUpdate, versionLock);
                            FillScriptTreeView();
                        }
                    }

                    break;
                case EAction.VersionUnLock:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionUnlock)
                    {
                        using var cnn = new DatabaseConnection();
                        versionUnlock.VersionIsLocked = false;
                        cnn.Execute(Version.SQLUpdate, versionUnlock);
                        FillScriptTreeView();

                    }

                    break;
                case EAction.VersionToDisk:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionToDisk && folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
                    {
                        var scToDisk = new ScriptToDisk(versionToDisk.VersionId, folderBrowserDialog1.SelectedPath);
                        scToDisk.WriteScripts();
                    }

                    break;
                #endregion
                #region Scripts
                case EAction.ScriptAddBegin:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionCounterAdd)
                    {
                        currentScriptEdited = new Script()
                        {
                            VersionId = versionCounterAdd.VersionId,
                            Version = versionCounterAdd,
                            ScriptOrder = versionCounterAdd.Count + 1
                        };
                        lblType.Text = $"Ajout d'un script à la version {versionCounterAdd}";
                        lblResumes.Visible = false;
                        sqlTextBox1.Text = string.Empty;
                        SetRightPanel(ERightPanelMode.TextSqlEdition);
                        ActionsFill(new List<EAction>() { EAction.ScriptAddEnd, EAction.Cancel }, 0);
                    }

                    break;
                case EAction.ScriptAddEnd:
                    if (currentScriptEdited != null && treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is VersionScriptCounter versionCounterAdd2)
                    {
                        currentScriptEdited.ScriptText = sqlTextBox1.Text;
                        using var cnn = new DatabaseConnection();
                        int id = cnn.ExecuteScalar(Script.SQLInsert, currentScriptEdited);
                        var analyzer = SqlAnalyzer.Analyse(id, currentScriptEdited.ScriptText);
                        analyzer.Save(cnn);
                        CancelEdition(treeView1.SelectedNode);
                        FillScriptVersion(treeView1.SelectedNode, versionCounterAdd2);
                        treeView1.SelectedNode.Expand();
                        SelectNodeScript(treeView1.SelectedNode, id);
                    }

                    break;
                case EAction.ScriptEditBegin:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Script script)
                    {
                        lblType.Text = $"Modification du script {script}";
                        lblResumes.Visible = false;
                        SetRightPanel(ERightPanelMode.TextSqlEdition);
                        currentScriptEdited = script;
                        ActionsFill(new List<EAction>() { EAction.ScriptEditEnd, EAction.Cancel }, 0);
                    }

                    break;
                case EAction.ScriptEditEnd:
                    if (currentScriptEdited != null)
                    {
                        currentScriptEdited.ScriptText = sqlTextBox1.Text;
                        var analyzer = SqlAnalyzer.Analyse(currentScriptEdited.ScriptId, currentScriptEdited.ScriptText);
                        using var cnn = new DatabaseConnection();
                        cnn.Execute(Script.SQLUpdate, currentScriptEdited);
                        analyzer.Save(cnn);
                        CancelEdition(treeView1.SelectedNode);
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
                                DisplayNodeSelected(parent);
                            }
                        }
                    }

                    break;
                case EAction.ScriptAnalyze:
                    if (treeView1.SelectedNode.Tag != null && treeView1.SelectedNode.Tag is Script scriptA)
                    {
                        using var frm = new FDetailScript();
                        Program.Settings.PositionLoad(frm);
                        frm.Script = scriptA;
                        frm.ShowDialog(this);
                        Program.Settings.PositionSave(frm);
                    }

                    break;
                #endregion
                #region Objets SQL
                case EAction.SqlObjectAddBegin:
                    ProcessSqlObjectAddBegin();
                    break;
                case EAction.SqlObjectAddEnd:
                    ProcessSqlObjectAddEnd();
                    break;
                case EAction.SqlObjectEditBegin:
                    ProcessSqlObjectEditBegin();

                    break;
                case EAction.SqlObjectEditEnd:
                    ProcessSqlObjectEditEnd();
                    break;
                case EAction.SqlObjectDelete:
                case EAction.SqlObjectRemoveCustomClient:
                    ProcessSqlObjectDelete();
                    break;
                case EAction.SqlObjectAddCustomClient:
                    ProcessSqlObjectAddCustomClientBegin();
                    break;
                case EAction.SqlObjectAddCustomClientEnd:
                    ProcessSqlObjectAddCustomClientEnd();
                    break;
                #endregion
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
            if (!processing)
            {
                processing = true;
                try
                {
                    cbVersions.DataSource = null;
                    using var conn = new DatabaseConnection();
                    cbVersions.DataSource = conn.Query<VersionObjectCounter>(VersionObjectCounter.SQLSelect, new { ProjectId = projectId }).OrderByDescending(x => x.FullVersion).ToList();
                }
                finally
                {
                    processing = false;
                }
            }
        }

        private void SelectionneVersionObjet(int versionId)
        {
            foreach (VersionObjectCounter v in cbVersions.Items)
            {
                if (v.VersionId == versionId)
                {
                    cbVersions.SelectedItem = v;
                    return;
                }
            }
        }

        private void FillClientTreeView(int id = 0)
        {
            treeView1.Nodes.Clear();

            using var conn = new DatabaseConnection();
            var lstClient = conn.Query<Base>(Base.SQLSelect + ";").OrderBy(x => x.BaseName).ToList();

            var cl = new Clients(lstClient.Count);
            TreeNode racine = new TreeNode(cl.ToString())
            {
                Tag = cl
            };
            treeView1.BeginUpdate();
            try
            {
                treeView1.Nodes.Add(racine);
                foreach (var client in lstClient)
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

                foreach (var versionCounter in lstCounter.OrderBy(x => x.FullVersion))
                {
                    TreeNode nod = new TreeNode(versionCounter.ToString())
                    {
                        Tag = versionCounter
                    };
                    racine.Nodes.Add(nod);
                    if (versionCounter.Count > 0)
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

        private void FillScriptVersion(TreeNode node, VersionScriptCounter versionCounter, bool select = false)
        {
            node.Nodes.Clear();
            var conn = new DatabaseConnection();
            var lst = conn.Query<Script>(Script.SQLSelect + " WHERE VersionId = @VersionId;", new { versionCounter.VersionId });
            int count = 0;
            foreach (var script in lst.OrderBy(x => x.FullVersion))
            {
                script.Version = versionCounter;
                TreeNode nod = new TreeNode(script.ToString())
                {
                    Tag = script
                };
                node.Nodes.Add(nod);
                count++;
            }

            if (count != versionCounter.Count)
            {
                versionCounter.Count = count;
                node.Tag = versionCounter;
                node.Text = versionCounter.ToString();
            }

            if (select)
            {
                treeView1.SelectedNode = node;
            }
        }

        private void DisplayNodeSelected(TreeNode selectedNode)
        {
            lblType.Text = string.Empty;
            sqlTextBox1.Text = string.Empty;
            lblResumes.Visible = false;

            if (selectedNode == null || selectedNode.Tag == null || !(selectedNode.Tag is IPresentable presentable))
            {
                ActionsClear();
                return;
            }

            bool versionIsLocked = false;
            bool checkCBVersion = false;

            lblType.Text = presentable.ToString();
            var cat = presentable.GetCategory();
            switch (cat)
            {
                case ETypeObjectPresentable.Project:
                    SetRightPanel(ERightPanelMode.Versions);
                    break;
                case ETypeObjectPresentable.VersionScript:
                    SetRightPanel(ERightPanelMode.VersionScript);
                    if (selectedNode.Tag is VersionScriptCounter version)
                    {
                        versionScriptControl1.Version = version;
                        versionIsLocked = version.VersionIsLocked;
                    }

                    break;
                case ETypeObjectPresentable.Script:
                    SetRightPanel(ERightPanelMode.TextSqlReadOnly);
                    if (selectedNode.Tag is Script script)
                    {
                        sqlTextBox1.Text = script.ScriptText;
                        var analyzer = script.GetAnalyzer();
                        lblResumes.Text = analyzer.ResumeText;
                        lblResumes.Visible = true;

                        if (selectedNode.Parent != null && selectedNode.Parent.Tag != null && selectedNode.Parent.Tag is VersionScriptCounter versionParent)
                        {
                            versionIsLocked = versionParent.VersionIsLocked;
                        }
                    }

                    break;
                case ETypeObjectPresentable.Clients:
                    SetRightPanel(ERightPanelMode.Clients);
                    break;
                case ETypeObjectPresentable.Client:
                    SetRightPanel(ERightPanelMode.BaseClient);
                    if (selectedNode.Tag != null && selectedNode.Tag is Base baseClient)
                    {
                        baseClientControl1.ClientBase = baseClient;
                    }
                    break;
                case ETypeObjectPresentable.VersionReferential:
                    SetRightPanel(ERightPanelMode.Referential);
                    checkCBVersion = true;
                    break;
                case ETypeObjectPresentable.SqlGroup:
                    SetRightPanel(ERightPanelMode.Referential);
                    checkCBVersion = true;
                    break;
                case ETypeObjectPresentable.SqlObject:
                    SetRightPanel(ERightPanelMode.TextSqlReadOnly);
                    checkCBVersion = true;
                    if (selectedNode.Tag is Object myobject)
                    {
                        sqlTextBox1.Text = myobject.ObjectSql;
                    }

                    break;
                case ETypeObjectPresentable.SQlObjectCustomClient:
                    SetRightPanel(ERightPanelMode.TextSqlReadOnly);
                    checkCBVersion = true;
                    if (selectedNode.Tag is Object myobjectClient)
                    {
                        sqlTextBox1.Text = myobjectClient.ObjectSql;
                    }

                    break;

            }


            if (checkCBVersion && cbVersions.SelectedItem != null && cbVersions.SelectedItem is Version vers)
            {
                versionIsLocked = vers.VersionIsLocked;
            }

            ActionsFill(cat.GetActions(versionIsLocked, presentable));
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

        private TreeNode SelectNodeObject(TreeNode node, int objectId)
        {
            foreach (TreeNode nod in node.Nodes)
            {
                if (nod.Tag != null && nod.Tag is Object theobject && theobject.ObjectId == objectId)
                {
                    treeView1.SelectedNode = nod;
                    return nod;
                }
            }

            return null;
        }

        #endregion

        #region Edition
        private void GereBoutonOk()
        {
            if (buttonOk != null)
            {
                if (currentObjectEdited != null)
                {
                    buttonOk.Enabled = !string.IsNullOrWhiteSpace(sqlTextBox1.Text) && !string.IsNullOrWhiteSpace(currentObjectEdited.ObjectName);
                }
                else if (currentScriptEdited != null)
                {
                    buttonOk.Enabled = !string.IsNullOrWhiteSpace(sqlTextBox1.Text);
                }
            }
        }

        private void SetRightPanel(ERightPanelMode mode)
        {
            switch (mode)
            {
                case ERightPanelMode.BaseClient:
                    pictureBox1.Visible = false;
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = false;
                    baseClientControl1.Visible = true;
                    break;
                case ERightPanelMode.TextSqlEdition:
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = true;
                    sqlTextBox1.ReadOnly = false;
                    baseClientControl1.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case ERightPanelMode.TextSqlReadOnly:
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = true;
                    sqlTextBox1.ReadOnly = true;
                    baseClientControl1.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case ERightPanelMode.VersionScript:
                    versionScriptControl1.Visible = true;
                    sqlTextBox1.Visible = false;
                    baseClientControl1.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case ERightPanelMode.Clients:
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = false;
                    baseClientControl1.Visible = false;
                    pictureBox1.BackgroundImage = Properties.Resources.Client;
                    pictureBox1.Visible = true;
                    break;
                case ERightPanelMode.Referential:
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = false;
                    baseClientControl1.Visible = false;
                    pictureBox1.BackgroundImage = Properties.Resources.Referentiel;
                    pictureBox1.Visible = true;
                    break;
                case ERightPanelMode.Versions:
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = false;
                    baseClientControl1.Visible = false;
                    pictureBox1.BackgroundImage = Properties.Resources.Table;
                    pictureBox1.Visible = true;
                    break;
                default:
                    versionScriptControl1.Visible = false;
                    sqlTextBox1.Visible = false;
                    baseClientControl1.Visible = false;
                    pictureBox1.Visible = false;
                    break;
            }
        }

        private void ActionsClear()
        {
            buttonOk = null;
            foreach (var ctrl in pnlActions.Controls)
            {
                if (ctrl is Button bt)
                {
                    bt.Click -= BtActions_Click;
                }
            }

            pnlActions.Controls.Clear();
        }

        private void ActionsFill(IEnumerable<EAction> lstAction, int indexBtOk = -1)
        {
            ActionsClear();
            int mw = 0;
            int i = 0;
            foreach (EAction action in lstAction)
            {
                Button bt = new Button
                {
                    Name = action.ToString(),
                    Font = new Font("Segoe MDL2 Assets", 12f),
                    Tag = action,
                    Text = action.GetIcon(),
                    ForeColor = action.GetColor(),
                    FlatStyle = FlatStyle.Flat,
                    Height = 30,
                    Width = 30,
                    Top = 1,
                    Left = mw
                };
                bt.FlatAppearance.BorderSize = 0;
                bt.FlatAppearance.MouseOverBackColor = Color.FromArgb(205, 230, 247);
                bt.FlatAppearance.MouseDownBackColor = Color.FromArgb(146, 192, 224);
                toolTip1.SetToolTip(bt, action.GetToolTipText());
                pnlActions.Controls.Add(bt);
                bt.Click += BtActions_Click;
                mw += bt.Width + 10;
                if (i == indexBtOk)
                {
                    buttonOk = bt;
                    GereBoutonOk();
                }

                i++;
            }
        }

        private void CancelEdition(TreeNode selectedNode)
        {
            SetRightPanel(ERightPanelMode.TextSqlReadOnly);
            currentScriptEdited = null;
            currentObjectEdited = null;
            DisplayNodeSelected(selectedNode);
        }

        private int NewVersion()
        {
            using var frm = new FNewVersion();
            Program.Settings.PositionLoad(frm);
            frm.SetVersion(
                    new Version() { ProjectId = projectId, VersionPrincipal = lastVersion.VersionPrincipal, VersionSecondary = lastVersion.VersionSecondary + 1 },
                    new Version() { ProjectId = projectId, VersionPrincipal = lastVersion.VersionPrincipal + 1, VersionSecondary = 0 });
            int id = 0;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                using var cnn = new DatabaseConnection();
                id = cnn.ExecuteScalar(Version.SQLInsert, frm.Choice());
            }

            Program.Settings.PositionSave(frm);
            return id;
        }

        private int ClientNew()
        {
            using var frm = new FEditBase();
            Program.Settings.PositionLoad(frm);
            bool res = frm.ShowDialog(this) == DialogResult.OK;
            int id = 0;
            if (res)
            {
                using var cnn = new DatabaseConnection();
                id = cnn.ExecuteScalar(Base.SQLInsert, frm.Base);
            }

            Program.Settings.PositionSave(frm);
            return id;
        }

        private bool ClientEdit(Base client)
        {
            using var frm = new FEditBase();
            Program.Settings.PositionLoad(frm); 
            frm.Base = client;
            bool res = frm.ShowDialog(this) == DialogResult.OK;
            if (res)
            {
                using var cnn = new DatabaseConnection();
                cnn.Execute(Base.SQLUpdate, frm.Base);
            }

            Program.Settings.PositionSave(frm);
            return res;
        }
        #endregion

        private enum ERightPanelMode
        {
            TextSqlReadOnly,
            TextSqlEdition,
            BaseClient,
            VersionScript,
            Clients,
            Referential,
            Versions
        }
    }
}

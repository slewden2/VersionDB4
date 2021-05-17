
namespace VersionDB4
{
    partial class FVersionDB
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new VersionDB4Lib.UI.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.rdReferential = new VersionDB4Lib.UI.RadioButton();
            this.rdClients = new VersionDB4Lib.UI.RadioButton();
            this.rdScript = new VersionDB4Lib.UI.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlVersion = new System.Windows.Forms.Panel();
            this.cbVersions = new VersionDB4Lib.UI.ComboBox();
            this.lblResumes = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.sqlTextBox1 = new VersionDB4.Control.SqlTextBox();
            this.baseClientControl1 = new VersionDB4.Control.BaseClientControl();
            this.versionScriptControl1 = new VersionDB4.Control.VersionScriptControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlVersion.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.ItemHeight = 24;
            this.treeView1.Location = new System.Drawing.Point(0, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(400, 370);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.rdReferential);
            this.panel1.Controls.Add(this.rdClients);
            this.panel1.Controls.Add(this.rdScript);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 410);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 40);
            this.panel1.TabIndex = 1;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(138, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(72, 20);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "V99.99.99";
            // 
            // rdReferential
            // 
            this.rdReferential.Checked = false;
            this.rdReferential.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.rdReferential.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.rdReferential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdReferential.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdReferential.Location = new System.Drawing.Point(95, 0);
            this.rdReferential.Name = "rdReferential";
            this.rdReferential.Size = new System.Drawing.Size(37, 40);
            this.rdReferential.TabIndex = 2;
            this.rdReferential.Text = "";
            this.rdReferential.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdReferential.Click += new System.EventHandler(this.RdReferential_Click);
            // 
            // rdClients
            // 
            this.rdClients.Checked = false;
            this.rdClients.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.rdClients.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.rdClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdClients.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdClients.Location = new System.Drawing.Point(49, 0);
            this.rdClients.Name = "rdClients";
            this.rdClients.Size = new System.Drawing.Size(37, 40);
            this.rdClients.TabIndex = 1;
            this.rdClients.Text = "";
            this.rdClients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdClients.Click += new System.EventHandler(this.RdClients_Click);
            // 
            // rdScript
            // 
            this.rdScript.Checked = true;
            this.rdScript.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.rdScript.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.rdScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdScript.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdScript.Location = new System.Drawing.Point(0, 0);
            this.rdScript.Name = "rdScript";
            this.rdScript.Size = new System.Drawing.Size(37, 40);
            this.rdScript.TabIndex = 0;
            this.rdScript.TabStop = true;
            this.rdScript.Text = "";
            this.rdScript.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdScript.Click += new System.EventHandler(this.RdScript_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.pnlVersion);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblResumes);
            this.splitContainer1.Panel2.Controls.Add(this.pnlActions);
            this.splitContainer1.Panel2.Controls.Add(this.lblType);
            this.splitContainer1.Panel2.Controls.Add(this.sqlTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.baseClientControl1);
            this.splitContainer1.Panel2.Controls.Add(this.versionScriptControl1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 450);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Paint);
            // 
            // pnlVersion
            // 
            this.pnlVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnlVersion.Controls.Add(this.cbVersions);
            this.pnlVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVersion.Location = new System.Drawing.Point(0, 0);
            this.pnlVersion.Name = "pnlVersion";
            this.pnlVersion.Size = new System.Drawing.Size(400, 40);
            this.pnlVersion.TabIndex = 2;
            // 
            // cbVersions
            // 
            this.cbVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVersions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersions.FormattingEnabled = true;
            this.cbVersions.Location = new System.Drawing.Point(3, 6);
            this.cbVersions.Name = "cbVersions";
            this.cbVersions.Size = new System.Drawing.Size(395, 28);
            this.cbVersions.TabIndex = 1;
            this.cbVersions.SelectedIndexChanged += new System.EventHandler(this.CbVersions_SelectedIndexChanged);
            // 
            // lblResumes
            // 
            this.lblResumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResumes.AutoEllipsis = true;
            this.lblResumes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResumes.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblResumes.Location = new System.Drawing.Point(260, 51);
            this.lblResumes.Name = "lblResumes";
            this.lblResumes.Size = new System.Drawing.Size(338, 17);
            this.lblResumes.TabIndex = 5;
            this.lblResumes.Text = "label1";
            // 
            // pnlActions
            // 
            this.pnlActions.Location = new System.Drawing.Point(11, 46);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(236, 32);
            this.pnlActions.TabIndex = 2;
            // 
            // lblType
            // 
            this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblType.AutoEllipsis = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblType.Location = new System.Drawing.Point(11, 11);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(586, 30);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "lblType";
            // 
            // sqlTextBox1
            // 
            this.sqlTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sqlTextBox1.Location = new System.Drawing.Point(11, 83);
            this.sqlTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sqlTextBox1.Name = "sqlTextBox1";
            this.sqlTextBox1.ReadOnly = false;
            this.sqlTextBox1.Size = new System.Drawing.Size(597, 367);
            this.sqlTextBox1.TabIndex = 8;
            this.sqlTextBox1.WordWrap = true;
            this.sqlTextBox1.OnChange += new System.EventHandler(this.SqlTextBox1_OnChange);
            // 
            // baseClientControl1
            // 
            this.baseClientControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.baseClientControl1.Location = new System.Drawing.Point(11, 81);
            this.baseClientControl1.Margin = new System.Windows.Forms.Padding(4);
            this.baseClientControl1.Name = "baseClientControl1";
            this.baseClientControl1.Size = new System.Drawing.Size(597, 369);
            this.baseClientControl1.TabIndex = 6;
            // 
            // versionScriptControl1
            // 
            this.versionScriptControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.versionScriptControl1.Location = new System.Drawing.Point(11, 83);
            this.versionScriptControl1.Margin = new System.Windows.Forms.Padding(4);
            this.versionScriptControl1.Name = "versionScriptControl1";
            this.versionScriptControl1.Size = new System.Drawing.Size(597, 367);
            this.versionScriptControl1.TabIndex = 7;
            this.versionScriptControl1.OnLinkReferential += new System.EventHandler(this.VersionScriptControl1_OnLinkReferential);
            // 
            // FVersionDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 450);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FVersionDB";
            this.Text = "VersionDB4";
            this.Load += new System.EventHandler(this.FVersionDB_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlVersion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VersionDB4Lib.UI.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Panel pnlActions;
        private VersionDB4Lib.UI.RadioButton rdReferential;
        private VersionDB4Lib.UI.RadioButton rdClients;
        private VersionDB4Lib.UI.RadioButton rdScript;
        private System.Windows.Forms.Panel pnlVersion;
        private VersionDB4Lib.UI.ComboBox cbVersions;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblResumes;
        private Control.BaseClientControl baseClientControl1;
        private Control.VersionScriptControl versionScriptControl1;
        private VersionDB4.Control.SqlTextBox sqlTextBox1;
    }
}


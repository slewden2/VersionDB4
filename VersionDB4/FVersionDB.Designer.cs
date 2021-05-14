
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.rdReferential = new VersionDB4Lib.UI.RadioButton();
            this.rdClients = new VersionDB4Lib.UI.RadioButton();
            this.rdScript = new VersionDB4Lib.UI.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlVersion = new System.Windows.Forms.Panel();
            this.cbVersions = new System.Windows.Forms.ComboBox();
            this.lblResumes = new System.Windows.Forms.Label();
            this.TxtScriptText = new System.Windows.Forms.RichTextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
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
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.HotTracking = true;
            this.treeView1.Location = new System.Drawing.Point(0, 84);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowLines = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(400, 366);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.rdReferential);
            this.panel1.Controls.Add(this.rdClients);
            this.panel1.Controls.Add(this.rdScript);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 42);
            this.panel1.TabIndex = 1;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(155, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(80, 21);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "V99.99.99";
            // 
            // rdReferential
            // 
            this.rdReferential.Checked = false;
            this.rdReferential.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.rdReferential.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdReferential.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdReferential.Location = new System.Drawing.Point(107, 0);
            this.rdReferential.Name = "rdReferential";
            this.rdReferential.Size = new System.Drawing.Size(42, 42);
            this.rdReferential.TabIndex = 2;
            this.rdReferential.Text = "";
            this.rdReferential.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdReferential.Click += new System.EventHandler(this.RdReferential_Click);
            // 
            // rdClients
            // 
            this.rdClients.Checked = false;
            this.rdClients.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.rdClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdClients.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdClients.Location = new System.Drawing.Point(55, 0);
            this.rdClients.Name = "rdClients";
            this.rdClients.Size = new System.Drawing.Size(42, 42);
            this.rdClients.TabIndex = 1;
            this.rdClients.Text = "";
            this.rdClients.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdClients.Click += new System.EventHandler(this.RdClients_Click);
            // 
            // rdScript
            // 
            this.rdScript.Checked = true;
            this.rdScript.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.rdScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdScript.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdScript.Location = new System.Drawing.Point(0, 0);
            this.rdScript.Name = "rdScript";
            this.rdScript.Size = new System.Drawing.Size(42, 42);
            this.rdScript.TabIndex = 0;
            this.rdScript.TabStop = true;
            this.rdScript.Text = "";
            this.rdScript.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdScript.Click += new System.EventHandler(this.RdScript_Click);
            // 
            // splitContainer1
            // 
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
            this.splitContainer1.Panel2.Controls.Add(this.TxtScriptText);
            this.splitContainer1.Panel2.Controls.Add(this.pnlActions);
            this.splitContainer1.Panel2.Controls.Add(this.lblType);
            this.splitContainer1.Size = new System.Drawing.Size(904, 450);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 2;
            // 
            // pnlVersion
            // 
            this.pnlVersion.Controls.Add(this.cbVersions);
            this.pnlVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlVersion.Location = new System.Drawing.Point(0, 42);
            this.pnlVersion.Name = "pnlVersion";
            this.pnlVersion.Size = new System.Drawing.Size(400, 42);
            this.pnlVersion.TabIndex = 2;
            // 
            // cbVersions
            // 
            this.cbVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersions.FormattingEnabled = true;
            this.cbVersions.Location = new System.Drawing.Point(3, 6);
            this.cbVersions.Name = "cbVersions";
            this.cbVersions.Size = new System.Drawing.Size(394, 29);
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
            this.lblResumes.Location = new System.Drawing.Point(292, 27);
            this.lblResumes.Name = "lblResumes";
            this.lblResumes.Size = new System.Drawing.Size(196, 18);
            this.lblResumes.TabIndex = 5;
            this.lblResumes.Text = "label1";
            // 
            // TxtScriptText
            // 
            this.TxtScriptText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtScriptText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtScriptText.Location = new System.Drawing.Point(12, 103);
            this.TxtScriptText.Name = "TxtScriptText";
            this.TxtScriptText.Size = new System.Drawing.Size(476, 335);
            this.TxtScriptText.TabIndex = 4;
            this.TxtScriptText.Text = "";
            // 
            // pnlActions
            // 
            this.pnlActions.Location = new System.Drawing.Point(12, 48);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(372, 49);
            this.pnlActions.TabIndex = 2;
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblType.Location = new System.Drawing.Point(12, 12);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(274, 32);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "lblType";
            // 
            // FVersionDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 450);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.RichTextBox TxtScriptText;
        private VersionDB4Lib.UI.RadioButton rdReferential;
        private VersionDB4Lib.UI.RadioButton rdClients;
        private VersionDB4Lib.UI.RadioButton rdScript;
        private System.Windows.Forms.Panel pnlVersion;
        private System.Windows.Forms.ComboBox cbVersions;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblResumes;
    }
}



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
            this.rdReferential = new VersionDB4Lib.UI.RadioButton();
            this.rdClients = new VersionDB4Lib.UI.RadioButton();
            this.rdScript = new VersionDB4Lib.UI.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlVersion = new System.Windows.Forms.Panel();
            this.cbVersions = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.TxtScriptText = new System.Windows.Forms.RichTextBox();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
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
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.listBox3);
            this.splitContainer1.Panel2.Controls.Add(this.listBox2);
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Panel2.Controls.Add(this.TxtScriptText);
            this.splitContainer1.Panel2.Controls.Add(this.pnlActions);
            this.splitContainer1.Panel2.Controls.Add(this.lblType);
            this.splitContainer1.Size = new System.Drawing.Size(1613, 450);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(520, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 37);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 21;
            this.listBox3.Location = new System.Drawing.Point(957, 26);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(240, 88);
            this.listBox3.TabIndex = 7;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 21;
            this.listBox2.Location = new System.Drawing.Point(778, 26);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(173, 88);
            this.listBox2.TabIndex = 6;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(386, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(386, 88);
            this.listBox1.TabIndex = 5;
            // 
            // TxtScriptText
            // 
            this.TxtScriptText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtScriptText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtScriptText.Location = new System.Drawing.Point(12, 193);
            this.TxtScriptText.Name = "TxtScriptText";
            this.TxtScriptText.Size = new System.Drawing.Size(1185, 245);
            this.TxtScriptText.TabIndex = 4;
            this.TxtScriptText.Text = "";
            // 
            // pnlActions
            // 
            this.pnlActions.Location = new System.Drawing.Point(12, 128);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(372, 49);
            this.pnlActions.TabIndex = 2;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(22, 39);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(59, 21);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "lblType";
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
            // FVersionDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1613, 450);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FVersionDB";
            this.Text = "VersionDB4";
            this.Load += new System.EventHandler(this.FVersionDB_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private VersionDB4Lib.UI.RadioButton rdReferential;
        private VersionDB4Lib.UI.RadioButton rdClients;
        private VersionDB4Lib.UI.RadioButton rdScript;
        private System.Windows.Forms.Panel pnlVersion;
        private System.Windows.Forms.ComboBox cbVersions;
        private System.Windows.Forms.Label lblVersion;
    }
}


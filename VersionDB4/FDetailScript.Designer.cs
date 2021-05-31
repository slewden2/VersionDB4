
namespace VersionDB4
{
    partial class FDetailScript
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtSql = new VersionDB4.Control.SqlTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lstResume = new VersionDB4Lib.UI.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPop = new System.Windows.Forms.Button();
            this.btnValid = new System.Windows.Forms.Button();
            this.controlWindow1 = new VersionDB4Lib.UI.ControlWindow();
            this.BtnRefuse = new System.Windows.Forms.Button();
            this.btnValidAll = new System.Windows.Forms.Button();
            this.btReload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lstDatabaseObject = new VersionDB4Lib.UI.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lstBloc = new VersionDB4Lib.UI.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddResume = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuMigrateAlterColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditResume = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelResume = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSql);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(984, 610);
            this.splitContainer1.SplitterDistance = 599;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // txtSql
            // 
            this.txtSql.BackColor = System.Drawing.SystemColors.Window;
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Location = new System.Drawing.Point(0, 41);
            this.txtSql.Name = "txtSql";
            this.txtSql.ReadOnly = false;
            this.txtSql.Size = new System.Drawing.Size(597, 567);
            this.txtSql.TabIndex = 0;
            this.txtSql.WordWrap = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 41);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(597, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Script";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LblTitle_MouseDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lstResume);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(379, 608);
            this.splitContainer2.SplitterDistance = 198;
            this.splitContainer2.TabIndex = 0;
            // 
            // lstResume
            // 
            this.lstResume.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstResume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResume.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstResume.FormattingEnabled = true;
            this.lstResume.IntegralHeight = false;
            this.lstResume.ItemHeight = 20;
            this.lstResume.Location = new System.Drawing.Point(0, 100);
            this.lstResume.Name = "lstResume";
            this.lstResume.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.lstResume.Size = new System.Drawing.Size(379, 98);
            this.lstResume.TabIndex = 1;
            this.lstResume.SelectedIndexChanged += new System.EventHandler(this.LstResume_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPop);
            this.panel2.Controls.Add(this.btnValid);
            this.panel2.Controls.Add(this.controlWindow1);
            this.panel2.Controls.Add(this.BtnRefuse);
            this.panel2.Controls.Add(this.btnValidAll);
            this.panel2.Controls.Add(this.btReload);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 100);
            this.panel2.TabIndex = 0;
            // 
            // btnPop
            // 
            this.btnPop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPop.ContextMenuStrip = this.contextMenuStrip1;
            this.btnPop.FlatAppearance.BorderSize = 0;
            this.btnPop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPop.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPop.Location = new System.Drawing.Point(302, 45);
            this.btnPop.Name = "btnPop";
            this.btnPop.Size = new System.Drawing.Size(32, 28);
            this.btnPop.TabIndex = 6;
            this.btnPop.Text = "";
            this.btnPop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPop.UseVisualStyleBackColor = true;
            this.btnPop.Click += new System.EventHandler(this.BtnPop_Click);
            // 
            // btnValid
            // 
            this.btnValid.FlatAppearance.BorderSize = 0;
            this.btnValid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValid.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValid.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnValid.Location = new System.Drawing.Point(113, 45);
            this.btnValid.Name = "btnValid";
            this.btnValid.Size = new System.Drawing.Size(86, 28);
            this.btnValid.TabIndex = 5;
            this.btnValid.Text = " Valider";
            this.btnValid.UseVisualStyleBackColor = true;
            this.btnValid.Click += new System.EventHandler(this.BtnValid_Click);
            // 
            // controlWindow1
            // 
            this.controlWindow1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlWindow1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.controlWindow1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlWindow1.Location = new System.Drawing.Point(244, 0);
            this.controlWindow1.MaximumSize = new System.Drawing.Size(135, 31);
            this.controlWindow1.MinimumSize = new System.Drawing.Size(135, 31);
            this.controlWindow1.Name = "controlWindow1";
            this.controlWindow1.Size = new System.Drawing.Size(135, 31);
            this.controlWindow1.TabIndex = 4;
            this.controlWindow1.Text = "controlWindow1";
            // 
            // BtnRefuse
            // 
            this.BtnRefuse.FlatAppearance.BorderSize = 0;
            this.BtnRefuse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefuse.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnRefuse.ForeColor = System.Drawing.Color.Red;
            this.BtnRefuse.Location = new System.Drawing.Point(192, 45);
            this.BtnRefuse.Name = "BtnRefuse";
            this.BtnRefuse.Size = new System.Drawing.Size(91, 28);
            this.BtnRefuse.TabIndex = 3;
            this.BtnRefuse.Text = " Refuser";
            this.BtnRefuse.UseVisualStyleBackColor = true;
            this.BtnRefuse.Click += new System.EventHandler(this.BtnRefuseAll_Click);
            // 
            // btnValidAll
            // 
            this.btnValidAll.FlatAppearance.BorderSize = 0;
            this.btnValidAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidAll.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValidAll.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnValidAll.Location = new System.Drawing.Point(0, 45);
            this.btnValidAll.Name = "btnValidAll";
            this.btnValidAll.Size = new System.Drawing.Size(107, 28);
            this.btnValidAll.TabIndex = 2;
            this.btnValidAll.Text = " Valider tout";
            this.btnValidAll.UseVisualStyleBackColor = true;
            this.btnValidAll.Click += new System.EventHandler(this.BtnValidAll_Click);
            // 
            // btReload
            // 
            this.btReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReload.FlatAppearance.BorderSize = 0;
            this.btReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReload.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btReload.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btReload.Location = new System.Drawing.Point(340, 41);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(36, 36);
            this.btReload.TabIndex = 1;
            this.btReload.Text = "";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.BtReload_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(0, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Résumé des actions";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lstDatabaseObject);
            this.splitContainer3.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lstBloc);
            this.splitContainer3.Panel2.Controls.Add(this.panel4);
            this.splitContainer3.Size = new System.Drawing.Size(379, 406);
            this.splitContainer3.SplitterDistance = 204;
            this.splitContainer3.TabIndex = 0;
            // 
            // lstDatabaseObject
            // 
            this.lstDatabaseObject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDatabaseObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDatabaseObject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDatabaseObject.FormattingEnabled = true;
            this.lstDatabaseObject.IntegralHeight = false;
            this.lstDatabaseObject.ItemHeight = 20;
            this.lstDatabaseObject.Location = new System.Drawing.Point(0, 20);
            this.lstDatabaseObject.Name = "lstDatabaseObject";
            this.lstDatabaseObject.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.lstDatabaseObject.Size = new System.Drawing.Size(379, 184);
            this.lstDatabaseObject.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(379, 20);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Objets en base impactés";
            // 
            // lstBloc
            // 
            this.lstBloc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBloc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBloc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBloc.FormattingEnabled = true;
            this.lstBloc.IntegralHeight = false;
            this.lstBloc.ItemHeight = 20;
            this.lstBloc.Location = new System.Drawing.Point(0, 20);
            this.lstBloc.Name = "lstBloc";
            this.lstBloc.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
            this.lstBloc.Size = new System.Drawing.Size(379, 178);
            this.lstBloc.TabIndex = 3;
            this.lstBloc.SelectedIndexChanged += new System.EventHandler(this.LstBloc_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(379, 20);
            this.panel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Blocs trouvés";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddResume,
            this.MnuMigrateAlterColumn,
            this.mnuEditResume,
            this.mnuDelResume});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 92);
            // 
            // mnuAddResume
            // 
            this.mnuAddResume.Name = "mnuAddResume";
            this.mnuAddResume.Size = new System.Drawing.Size(129, 22);
            this.mnuAddResume.Text = "Ajouter";
            this.mnuAddResume.Click += new System.EventHandler(this.MnuAddResume_Click);
            // 
            // MnuMigrateAlterColumn
            // 
            this.MnuMigrateAlterColumn.Name = "MnuMigrateAlterColumn";
            this.MnuMigrateAlterColumn.Size = new System.Drawing.Size(129, 22);
            this.MnuMigrateAlterColumn.Text = "Changer";
            this.MnuMigrateAlterColumn.Click += new System.EventHandler(this.MnuMigrateAlterColumn_Click);
            // 
            // mnuEditResume
            // 
            this.mnuEditResume.Name = "mnuEditResume";
            this.mnuEditResume.Size = new System.Drawing.Size(129, 22);
            this.mnuEditResume.Text = "Modifier";
            this.mnuEditResume.Click += new System.EventHandler(this.MnuEditResume_Click);
            // 
            // mnuDelResume
            // 
            this.mnuDelResume.Name = "mnuDelResume";
            this.mnuDelResume.Size = new System.Drawing.Size(129, 22);
            this.mnuDelResume.Text = "Supprimer";
            this.mnuDelResume.Click += new System.EventHandler(this.MnuDelResume_Click);
            // 
            // FDetailScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(986, 612);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FDetailScript";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FDetailScript";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private VersionDB4.Control.SqlTextBox txtSql;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private VersionDB4Lib.UI.ListBox lstResume;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private VersionDB4Lib.UI.ListBox lstDatabaseObject;
        private System.Windows.Forms.Panel panel3;
        private VersionDB4Lib.UI.ListBox lstBloc;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.Button btnValidAll;
        private System.Windows.Forms.Button BtnRefuse;
        private VersionDB4Lib.UI.ControlWindow controlWindow1;
        private System.Windows.Forms.Button btnValid;
        private System.Windows.Forms.Button btnPop;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuAddResume;
        private System.Windows.Forms.ToolStripMenuItem MnuMigrateAlterColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuEditResume;
        private System.Windows.Forms.ToolStripMenuItem mnuDelResume;
    }
}
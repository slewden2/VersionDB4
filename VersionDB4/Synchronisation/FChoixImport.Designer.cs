
namespace VersionDB4.Synchronisation
{
    partial class FChoixImport
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
            this.btOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sqlTextBoxReferential = new VersionDB4.Control.SqlTextBox();
            this.lblTitleReferential = new System.Windows.Forms.Label();
            this.lblNoneReferential = new System.Windows.Forms.Label();
            this.sqlTextBoxClient = new VersionDB4.Control.SqlTextBox();
            this.lblTitleClient = new System.Windows.Forms.Label();
            this.lblNoneClient = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(599, 17);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(113, 37);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Valider";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(728, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 37);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbMode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 391);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 66);
            this.panel1.TabIndex = 2;
            // 
            // cbMode
            // 
            this.cbMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Location = new System.Drawing.Point(140, 20);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(411, 28);
            this.cbMode.TabIndex = 3;
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.CbMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mode choisi";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.lblTitre);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 33);
            this.panel2.TabIndex = 3;
            // 
            // lblTitre
            // 
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitre.Location = new System.Drawing.Point(0, 0);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(261, 25);
            this.lblTitre.TabIndex = 1;
            this.lblTitre.Text = "Choix du mode d\'import";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sqlTextBoxReferential);
            this.splitContainer1.Panel1.Controls.Add(this.lblTitleReferential);
            this.splitContainer1.Panel1.Controls.Add(this.lblNoneReferential);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sqlTextBoxClient);
            this.splitContainer1.Panel2.Controls.Add(this.lblTitleClient);
            this.splitContainer1.Panel2.Controls.Add(this.lblNoneClient);
            this.splitContainer1.Size = new System.Drawing.Size(850, 358);
            this.splitContainer1.SplitterDistance = 425;
            this.splitContainer1.TabIndex = 4;
            // 
            // sqlTextBoxReferential
            // 
            this.sqlTextBoxReferential.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlTextBoxReferential.Location = new System.Drawing.Point(0, 30);
            this.sqlTextBoxReferential.Name = "sqlTextBoxReferential";
            this.sqlTextBoxReferential.ReadOnly = false;
            this.sqlTextBoxReferential.Size = new System.Drawing.Size(425, 328);
            this.sqlTextBoxReferential.TabIndex = 1;
            this.sqlTextBoxReferential.WordWrap = true;
            // 
            // lblTitleReferential
            // 
            this.lblTitleReferential.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitleReferential.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleReferential.Location = new System.Drawing.Point(0, 0);
            this.lblTitleReferential.Name = "lblTitleReferential";
            this.lblTitleReferential.Size = new System.Drawing.Size(425, 30);
            this.lblTitleReferential.TabIndex = 0;
            this.lblTitleReferential.Text = "Référentiel (existant)";
            this.lblTitleReferential.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoneReferential
            // 
            this.lblNoneReferential.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoneReferential.Location = new System.Drawing.Point(0, 0);
            this.lblNoneReferential.Name = "lblNoneReferential";
            this.lblNoneReferential.Size = new System.Drawing.Size(425, 358);
            this.lblNoneReferential.TabIndex = 2;
            this.lblNoneReferential.Text = "Aucune information";
            this.lblNoneReferential.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sqlTextBoxClient
            // 
            this.sqlTextBoxClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlTextBoxClient.Location = new System.Drawing.Point(0, 30);
            this.sqlTextBoxClient.Name = "sqlTextBoxClient";
            this.sqlTextBoxClient.ReadOnly = false;
            this.sqlTextBoxClient.Size = new System.Drawing.Size(421, 328);
            this.sqlTextBoxClient.TabIndex = 2;
            this.sqlTextBoxClient.WordWrap = true;
            // 
            // lblTitleClient
            // 
            this.lblTitleClient.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitleClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleClient.Location = new System.Drawing.Point(0, 0);
            this.lblTitleClient.Name = "lblTitleClient";
            this.lblTitleClient.Size = new System.Drawing.Size(421, 30);
            this.lblTitleClient.TabIndex = 1;
            this.lblTitleClient.Text = "Base client (a importer)";
            this.lblTitleClient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoneClient
            // 
            this.lblNoneClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoneClient.Location = new System.Drawing.Point(0, 0);
            this.lblNoneClient.Name = "lblNoneClient";
            this.lblNoneClient.Size = new System.Drawing.Size(421, 358);
            this.lblNoneClient.TabIndex = 3;
            this.lblNoneClient.Text = "Aucune information";
            this.lblNoneClient.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FChoixImport
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(850, 457);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(669, 161);
            this.Name = "FChoixImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Control.SqlTextBox sqlTextBoxReferential;
        private System.Windows.Forms.Label lblTitleReferential;
        private Control.SqlTextBox sqlTextBoxClient;
        private System.Windows.Forms.Label lblTitleClient;
        private System.Windows.Forms.Label lblNoneReferential;
        private System.Windows.Forms.Label lblNoneClient;
    }
}
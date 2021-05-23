
namespace VersionDB4.Synchronisation
{
    partial class FImportFromBdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lstTypeObject = new System.Windows.Forms.CheckedListBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVersions = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.btnUnckeAll = new System.Windows.Forms.Button();
            this.btnChkAll = new System.Windows.Forms.Button();
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlAnalyze = new System.Windows.Forms.Panel();
            this.lstResume = new System.Windows.Forms.ListView();
            this.colObject = new System.Windows.Forms.ColumnHeader();
            this.colCount = new System.Windows.Forms.ColumnHeader();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgression = new System.Windows.Forms.Label();
            this.pnlChoice = new System.Windows.Forms.Panel();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lstChoice = new System.Windows.Forms.ListView();
            this.colObj2 = new System.Windows.Forms.ColumnHeader();
            this.colExist = new System.Windows.Forms.ColumnHeader();
            this.lblConfirmVersion = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.lblImportCounter = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblStatusSummary = new System.Windows.Forms.Label();
            this.lblFinalSumary = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlSelection.SuspendLayout();
            this.pnlAnalyze.SuspendLayout();
            this.pnlChoice.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(27, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base client";
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Location = new System.Drawing.Point(129, 13);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(104, 20);
            this.lblClientName.TabIndex = 1;
            this.lblClientName.Text = "lblClientName";
            // 
            // lstTypeObject
            // 
            this.lstTypeObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTypeObject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstTypeObject.CheckOnClick = true;
            this.lstTypeObject.FormattingEnabled = true;
            this.lstTypeObject.IntegralHeight = false;
            this.lstTypeObject.Location = new System.Drawing.Point(129, 141);
            this.lstTypeObject.Name = "lstTypeObject";
            this.lstTypeObject.Size = new System.Drawing.Size(651, 387);
            this.lstTypeObject.TabIndex = 3;
            this.lstTypeObject.SelectedIndexChanged += new System.EventHandler(this.CbVersions_SelectedIndexChanged);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(672, 15);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(113, 37);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(542, 15);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(113, 37);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Suivant >>";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(27, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Version";
            // 
            // cbVersions
            // 
            this.cbVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersions.FormattingEnabled = true;
            this.cbVersions.Location = new System.Drawing.Point(129, 52);
            this.cbVersions.Name = "cbVersions";
            this.cbVersions.Size = new System.Drawing.Size(651, 28);
            this.cbVersions.TabIndex = 7;
            this.cbVersions.SelectedIndexChanged += new System.EventHandler(this.CbVersions_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblTitre);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 33);
            this.panel1.TabIndex = 17;
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitre.Location = new System.Drawing.Point(12, -2);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(261, 25);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Import depuis une base client";
            // 
            // btnUnckeAll
            // 
            this.btnUnckeAll.FlatAppearance.BorderSize = 0;
            this.btnUnckeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnckeAll.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUnckeAll.Location = new System.Drawing.Point(97, 171);
            this.btnUnckeAll.Name = "btnUnckeAll";
            this.btnUnckeAll.Size = new System.Drawing.Size(26, 26);
            this.btnUnckeAll.TabIndex = 18;
            this.btnUnckeAll.UseVisualStyleBackColor = true;
            this.btnUnckeAll.Click += new System.EventHandler(this.BtnUnckeAll_Click);
            // 
            // btnChkAll
            // 
            this.btnChkAll.FlatAppearance.BorderSize = 0;
            this.btnChkAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChkAll.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnChkAll.Location = new System.Drawing.Point(97, 203);
            this.btnChkAll.Name = "btnChkAll";
            this.btnChkAll.Size = new System.Drawing.Size(26, 26);
            this.btnChkAll.TabIndex = 19;
            this.btnChkAll.UseVisualStyleBackColor = true;
            this.btnChkAll.Click += new System.EventHandler(this.BtnChkAll_Click);
            // 
            // pnlSelection
            // 
            this.pnlSelection.Controls.Add(this.label2);
            this.pnlSelection.Controls.Add(this.lstTypeObject);
            this.pnlSelection.Controls.Add(this.btnChkAll);
            this.pnlSelection.Controls.Add(this.btnUnckeAll);
            this.pnlSelection.Controls.Add(this.cbVersions);
            this.pnlSelection.Controls.Add(this.label3);
            this.pnlSelection.Controls.Add(this.lblClientName);
            this.pnlSelection.Controls.Add(this.label1);
            this.pnlSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelection.Location = new System.Drawing.Point(0, 0);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(792, 528);
            this.pnlSelection.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(27, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Objets";
            // 
            // pnlAnalyze
            // 
            this.pnlAnalyze.Controls.Add(this.lstResume);
            this.pnlAnalyze.Controls.Add(this.progressBar1);
            this.pnlAnalyze.Controls.Add(this.lblProgression);
            this.pnlAnalyze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAnalyze.Location = new System.Drawing.Point(0, 0);
            this.pnlAnalyze.Name = "pnlAnalyze";
            this.pnlAnalyze.Size = new System.Drawing.Size(792, 528);
            this.pnlAnalyze.TabIndex = 21;
            // 
            // lstResume
            // 
            this.lstResume.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResume.BackColor = System.Drawing.SystemColors.Control;
            this.lstResume.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstResume.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObject,
            this.colCount});
            this.lstResume.FullRowSelect = true;
            this.lstResume.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstResume.HideSelection = false;
            this.lstResume.LabelWrap = false;
            this.lstResume.Location = new System.Drawing.Point(12, 108);
            this.lstResume.MultiSelect = false;
            this.lstResume.Name = "lstResume";
            this.lstResume.Size = new System.Drawing.Size(768, 420);
            this.lstResume.TabIndex = 2;
            this.lstResume.UseCompatibleStateImageBehavior = false;
            this.lstResume.View = System.Windows.Forms.View.Details;
            // 
            // colObject
            // 
            this.colObject.Text = "Type d\'objet";
            this.colObject.Width = 200;
            // 
            // colCount
            // 
            this.colCount.Text = "Trouvés";
            this.colCount.Width = 80;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(768, 21);
            this.progressBar1.TabIndex = 1;
            // 
            // lblProgression
            // 
            this.lblProgression.AutoEllipsis = true;
            this.lblProgression.Location = new System.Drawing.Point(12, 26);
            this.lblProgression.Name = "lblProgression";
            this.lblProgression.Size = new System.Drawing.Size(510, 26);
            this.lblProgression.TabIndex = 0;
            this.lblProgression.Text = "lblProgression";
            this.lblProgression.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlChoice
            // 
            this.pnlChoice.Controls.Add(this.lblHelp);
            this.pnlChoice.Controls.Add(this.lstChoice);
            this.pnlChoice.Controls.Add(this.lblConfirmVersion);
            this.pnlChoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChoice.Location = new System.Drawing.Point(0, 0);
            this.pnlChoice.Name = "pnlChoice";
            this.pnlChoice.Size = new System.Drawing.Size(792, 528);
            this.pnlChoice.TabIndex = 22;
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHelp.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblHelp.Location = new System.Drawing.Point(594, 53);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(186, 13);
            this.lblHelp.TabIndex = 22;
            this.lblHelp.Text = "Double click pour modifier le choix";
            // 
            // lstChoice
            // 
            this.lstChoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstChoice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstChoice.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObj2,
            this.colExist});
            this.lstChoice.FullRowSelect = true;
            this.lstChoice.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstChoice.HideSelection = false;
            this.lstChoice.LabelWrap = false;
            this.lstChoice.Location = new System.Drawing.Point(12, 69);
            this.lstChoice.MultiSelect = false;
            this.lstChoice.Name = "lstChoice";
            this.lstChoice.Size = new System.Drawing.Size(768, 459);
            this.lstChoice.TabIndex = 1;
            this.lstChoice.UseCompatibleStateImageBehavior = false;
            this.lstChoice.View = System.Windows.Forms.View.Details;
            this.lstChoice.SelectedIndexChanged += new System.EventHandler(this.LstChoice_SelectedIndexChanged);
            this.lstChoice.DoubleClick += new System.EventHandler(this.LstChoice_DoubleClick);
            // 
            // colObj2
            // 
            this.colObj2.Text = "Objet";
            // 
            // colExist
            // 
            this.colExist.Text = "Existe";
            // 
            // lblConfirmVersion
            // 
            this.lblConfirmVersion.AutoSize = true;
            this.lblConfirmVersion.Location = new System.Drawing.Point(11, 14);
            this.lblConfirmVersion.Name = "lblConfirmVersion";
            this.lblConfirmVersion.Size = new System.Drawing.Size(234, 20);
            this.lblConfirmVersion.TabIndex = 0;
            this.lblConfirmVersion.Text = "Sélectionnez les objets à importer";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPrevious);
            this.panel2.Controls.Add(this.lblImportCounter);
            this.panel2.Controls.Add(this.btCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 466);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 62);
            this.panel2.TabIndex = 23;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Location = new System.Drawing.Point(423, 15);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(113, 37);
            this.btnPrevious.TabIndex = 7;
            this.btnPrevious.Text = "Précédent";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // lblImportCounter
            // 
            this.lblImportCounter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImportCounter.AutoEllipsis = true;
            this.lblImportCounter.Location = new System.Drawing.Point(11, 23);
            this.lblImportCounter.Name = "lblImportCounter";
            this.lblImportCounter.Size = new System.Drawing.Size(406, 20);
            this.lblImportCounter.TabIndex = 6;
            this.lblImportCounter.Text = "XX";
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.lblStatusSummary);
            this.pnlSummary.Controls.Add(this.lblFinalSumary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(792, 528);
            this.pnlSummary.TabIndex = 24;
            // 
            // lblStatusSummary
            // 
            this.lblStatusSummary.Font = new System.Drawing.Font("Segoe MDL2 Assets", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatusSummary.Location = new System.Drawing.Point(13, 52);
            this.lblStatusSummary.Name = "lblStatusSummary";
            this.lblStatusSummary.Size = new System.Drawing.Size(78, 89);
            this.lblStatusSummary.TabIndex = 1;
            this.lblStatusSummary.Text = "OK";
            this.lblStatusSummary.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFinalSumary
            // 
            this.lblFinalSumary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFinalSumary.Location = new System.Drawing.Point(97, 52);
            this.lblFinalSumary.Name = "lblFinalSumary";
            this.lblFinalSumary.Size = new System.Drawing.Size(688, 476);
            this.lblFinalSumary.TabIndex = 0;
            this.lblFinalSumary.Text = "lblFinalSumary";
            // 
            // FImportFromBdd
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(792, 528);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlChoice);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlSelection);
            this.Controls.Add(this.pnlAnalyze);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1081, 821);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(573, 361);
            this.Name = "FImportFromBdd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.SizeChanged += new System.EventHandler(this.FImportFromBdd_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSelection.ResumeLayout(false);
            this.pnlSelection.PerformLayout();
            this.pnlAnalyze.ResumeLayout(false);
            this.pnlChoice.ResumeLayout(false);
            this.pnlChoice.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.CheckedListBox lstTypeObject;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbVersions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Button btnUnckeAll;
        private System.Windows.Forms.Button btnChkAll;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.Panel pnlAnalyze;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgression;
        private System.Windows.Forms.Panel pnlChoice;
        private System.Windows.Forms.ListView lstResume;
        private System.Windows.Forms.ColumnHeader colObject;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.Label lblConfirmVersion;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lstChoice;
        private System.Windows.Forms.ColumnHeader colObj2;
        private System.Windows.Forms.ColumnHeader colExist;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblFinalSumary;
        private System.Windows.Forms.Label lblStatusSummary;
        private System.Windows.Forms.Label lblImportCounter;
        private System.Windows.Forms.Button btnPrevious;
    }
}
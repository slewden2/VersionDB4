
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtSql = new VersionDB4.Control.SqlTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lstResume = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.controlWindow1 = new VersionDB4Lib.UI.ControlWindow();
            this.BtnRefuseAll = new System.Windows.Forms.Button();
            this.btnValidAll = new System.Windows.Forms.Button();
            this.btReload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lstDatabaseObject = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lstBloc = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
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
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSql);
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
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
            this.txtSql.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSql.Location = new System.Drawing.Point(20, 100);
            this.txtSql.Name = "txtSql";
            this.txtSql.ReadOnly = true;
            this.txtSql.Size = new System.Drawing.Size(579, 510);
            this.txtSql.TabIndex = 0;
            this.txtSql.Text = "";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 100);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(20, 510);
            this.panel5.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 100);
            this.panel1.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(0, -1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(599, 40);
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
            this.splitContainer2.Size = new System.Drawing.Size(381, 610);
            this.splitContainer2.SplitterDistance = 199;
            this.splitContainer2.TabIndex = 0;
            // 
            // lstResume
            // 
            this.lstResume.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstResume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResume.FormattingEnabled = true;
            this.lstResume.IntegralHeight = false;
            this.lstResume.ItemHeight = 20;
            this.lstResume.Location = new System.Drawing.Point(0, 100);
            this.lstResume.Name = "lstResume";
            this.lstResume.Size = new System.Drawing.Size(381, 99);
            this.lstResume.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.controlWindow1);
            this.panel2.Controls.Add(this.BtnRefuseAll);
            this.panel2.Controls.Add(this.btnValidAll);
            this.panel2.Controls.Add(this.btReload);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(381, 100);
            this.panel2.TabIndex = 0;
            // 
            // controlWindow1
            // 
            this.controlWindow1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlWindow1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.controlWindow1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlWindow1.Location = new System.Drawing.Point(246, 0);
            this.controlWindow1.MaximumSize = new System.Drawing.Size(135, 31);
            this.controlWindow1.MinimumSize = new System.Drawing.Size(135, 31);
            this.controlWindow1.Name = "controlWindow1";
            this.controlWindow1.Size = new System.Drawing.Size(135, 31);
            this.controlWindow1.TabIndex = 4;
            this.controlWindow1.Text = "controlWindow1";
            // 
            // BtnRefuseAll
            // 
            this.BtnRefuseAll.FlatAppearance.BorderSize = 0;
            this.BtnRefuseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRefuseAll.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnRefuseAll.ForeColor = System.Drawing.Color.Red;
            this.BtnRefuseAll.Location = new System.Drawing.Point(63, 41);
            this.BtnRefuseAll.Name = "BtnRefuseAll";
            this.BtnRefuseAll.Size = new System.Drawing.Size(123, 28);
            this.BtnRefuseAll.TabIndex = 3;
            this.BtnRefuseAll.Text = " Refuser tout";
            this.BtnRefuseAll.UseVisualStyleBackColor = true;
            this.BtnRefuseAll.Click += new System.EventHandler(this.BtnRefuseAll_Click);
            // 
            // btnValidAll
            // 
            this.btnValidAll.FlatAppearance.BorderSize = 0;
            this.btnValidAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidAll.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnValidAll.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnValidAll.Location = new System.Drawing.Point(63, 7);
            this.btnValidAll.Name = "btnValidAll";
            this.btnValidAll.Size = new System.Drawing.Size(123, 28);
            this.btnValidAll.TabIndex = 2;
            this.btnValidAll.Text = " Valider tout";
            this.btnValidAll.UseVisualStyleBackColor = true;
            this.btnValidAll.Click += new System.EventHandler(this.BtnValidAll_Click);
            // 
            // btReload
            // 
            this.btReload.FlatAppearance.BorderSize = 0;
            this.btReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReload.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btReload.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btReload.Location = new System.Drawing.Point(21, 3);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(36, 36);
            this.btReload.TabIndex = 1;
            this.btReload.Text = "";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.BtReload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
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
            this.splitContainer3.Size = new System.Drawing.Size(381, 407);
            this.splitContainer3.SplitterDistance = 205;
            this.splitContainer3.TabIndex = 0;
            // 
            // lstDatabaseObject
            // 
            this.lstDatabaseObject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDatabaseObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDatabaseObject.FormattingEnabled = true;
            this.lstDatabaseObject.IntegralHeight = false;
            this.lstDatabaseObject.ItemHeight = 20;
            this.lstDatabaseObject.Location = new System.Drawing.Point(0, 25);
            this.lstDatabaseObject.Name = "lstDatabaseObject";
            this.lstDatabaseObject.Size = new System.Drawing.Size(381, 180);
            this.lstDatabaseObject.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(381, 25);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Objets en base impactés";
            // 
            // lstBloc
            // 
            this.lstBloc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstBloc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBloc.FormattingEnabled = true;
            this.lstBloc.IntegralHeight = false;
            this.lstBloc.ItemHeight = 20;
            this.lstBloc.Location = new System.Drawing.Point(0, 21);
            this.lstBloc.Name = "lstBloc";
            this.lstBloc.Size = new System.Drawing.Size(381, 177);
            this.lstBloc.TabIndex = 3;
            this.lstBloc.SelectedIndexChanged += new System.EventHandler(this.LstBloc_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(381, 21);
            this.panel4.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Blocs trouvés";
            // 
            // FDetailScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FDetailScript_Paint);
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
            this.panel2.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private VersionDB4.Control.SqlTextBox txtSql;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lstResume;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListBox lstDatabaseObject;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox lstBloc;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.Button btnValidAll;
        private System.Windows.Forms.Button BtnRefuseAll;
        private System.Windows.Forms.Panel panel5;
        private VersionDB4Lib.UI.ControlWindow controlWindow1;
    }
}
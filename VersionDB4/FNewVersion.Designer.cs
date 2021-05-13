
namespace VersionDB4
{
    partial class FNewVersion
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
            this.rdChoice2 = new System.Windows.Forms.RadioButton();
            this.rdChoice1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btOk = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdChoice2
            // 
            this.rdChoice2.AutoSize = true;
            this.rdChoice2.Location = new System.Drawing.Point(38, 113);
            this.rdChoice2.Name = "rdChoice2";
            this.rdChoice2.Size = new System.Drawing.Size(59, 24);
            this.rdChoice2.TabIndex = 0;
            this.rdChoice2.TabStop = true;
            this.rdChoice2.Text = "V 4.3";
            this.rdChoice2.UseVisualStyleBackColor = true;
            // 
            // rdChoice1
            // 
            this.rdChoice1.AutoSize = true;
            this.rdChoice1.Location = new System.Drawing.Point(38, 68);
            this.rdChoice1.Name = "rdChoice1";
            this.rdChoice1.Size = new System.Drawing.Size(59, 24);
            this.rdChoice1.TabIndex = 1;
            this.rdChoice1.TabStop = true;
            this.rdChoice1.Text = "V 5.0";
            this.rdChoice1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selectionnez le numero de version à créer";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 53);
            this.panel1.TabIndex = 3;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(152, 167);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 35);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Valider";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // BtCancel
            // 
            this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtCancel.Location = new System.Drawing.Point(233, 167);
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.Size = new System.Drawing.Size(75, 35);
            this.BtCancel.TabIndex = 5;
            this.BtCancel.Text = "Annuler";
            this.BtCancel.UseVisualStyleBackColor = true;
            // 
            // FNewVersion
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtCancel;
            this.ClientSize = new System.Drawing.Size(320, 214);
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rdChoice1);
            this.Controls.Add(this.rdChoice2);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FNewVersion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Nouvelle version";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdChoice2;
        private System.Windows.Forms.RadioButton rdChoice1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button BtCancel;
    }
}
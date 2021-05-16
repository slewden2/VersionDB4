
namespace VersionDB4.Control
{
    partial class VersionScriptControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfoScripts = new System.Windows.Forms.Label();
            this.lstResume = new System.Windows.Forms.ListBox();
            this.lblTitleResume = new System.Windows.Forms.Label();
            this.lblNoAction = new System.Windows.Forms.Label();
            this.lblInfoObjects = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblInfoScripts
            // 
            this.lblInfoScripts.AutoSize = true;
            this.lblInfoScripts.Location = new System.Drawing.Point(27, 21);
            this.lblInfoScripts.Name = "lblInfoScripts";
            this.lblInfoScripts.Size = new System.Drawing.Size(76, 15);
            this.lblInfoScripts.TabIndex = 0;
            this.lblInfoScripts.Text = "lblInfoScripts";
            // 
            // lstResume
            // 
            this.lstResume.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResume.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstResume.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstResume.FormattingEnabled = true;
            this.lstResume.IntegralHeight = false;
            this.lstResume.ItemHeight = 20;
            this.lstResume.Location = new System.Drawing.Point(43, 101);
            this.lstResume.Name = "lstResume";
            this.lstResume.Size = new System.Drawing.Size(424, 134);
            this.lstResume.TabIndex = 2;
            this.lstResume.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstResume_DrawItem);
            // 
            // lblTitleResume
            // 
            this.lblTitleResume.AutoSize = true;
            this.lblTitleResume.Location = new System.Drawing.Point(28, 83);
            this.lblTitleResume.Name = "lblTitleResume";
            this.lblTitleResume.Size = new System.Drawing.Size(214, 15);
            this.lblTitleResume.TabIndex = 3;
            this.lblTitleResume.Text = "Synthèse des actions dans cette version";
            // 
            // lblNoAction
            // 
            this.lblNoAction.AutoSize = true;
            this.lblNoAction.ForeColor = System.Drawing.Color.Red;
            this.lblNoAction.Location = new System.Drawing.Point(43, 102);
            this.lblNoAction.Name = "lblNoAction";
            this.lblNoAction.Size = new System.Drawing.Size(141, 15);
            this.lblNoAction.TabIndex = 4;
            this.lblNoAction.Text = "Aucune action pertinente";
            // 
            // lblInfoObjects
            // 
            this.lblInfoObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoObjects.AutoEllipsis = true;
            this.lblInfoObjects.LinkColor = System.Drawing.Color.SteelBlue;
            this.lblInfoObjects.Location = new System.Drawing.Point(28, 52);
            this.lblInfoObjects.Name = "lblInfoObjects";
            this.lblInfoObjects.Size = new System.Drawing.Size(439, 23);
            this.lblInfoObjects.TabIndex = 5;
            this.lblInfoObjects.TabStop = true;
            this.lblInfoObjects.Text = "lblInfoObjects";
            this.lblInfoObjects.VisitedLinkColor = System.Drawing.Color.SteelBlue;
            this.lblInfoObjects.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblInfoObjects_LinkClicked);
            // 
            // VersionScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfoObjects);
            this.Controls.Add(this.lblNoAction);
            this.Controls.Add(this.lblTitleResume);
            this.Controls.Add(this.lstResume);
            this.Controls.Add(this.lblInfoScripts);
            this.Name = "VersionScriptControl";
            this.Size = new System.Drawing.Size(497, 254);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfoScripts;
        private System.Windows.Forms.ListBox lstResume;
        private System.Windows.Forms.Label lblTitleResume;
        private System.Windows.Forms.Label lblNoAction;
        private System.Windows.Forms.LinkLabel lblInfoObjects;
    }
}

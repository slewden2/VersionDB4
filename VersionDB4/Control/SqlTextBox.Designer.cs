
namespace VersionDB4.Control
{
    partial class SqlTextBox
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ChkWrap = new System.Windows.Forms.CheckBox();
            this.lblPanleInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ShowSelectionMargin = true;
            this.richTextBox1.Size = new System.Drawing.Size(560, 408);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.RichTextBox1_TextChanged);
            this.richTextBox1.Enter += new System.EventHandler(this.RichTextBox1_Enter);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RichTextBox1_KeyDown);
            this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RichTextBox1_KeyPress);
            this.richTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RichTextBox1_KeyUp);
            this.richTextBox1.Leave += new System.EventHandler(this.RichTextBox1_Leave);
            this.richTextBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RichTextBox1_MouseUp);
            // 
            // ChkWrap
            // 
            this.ChkWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkWrap.AutoSize = true;
            this.ChkWrap.Location = new System.Drawing.Point(542, 6);
            this.ChkWrap.Name = "ChkWrap";
            this.ChkWrap.Size = new System.Drawing.Size(15, 14);
            this.ChkWrap.TabIndex = 2;
            this.ChkWrap.UseVisualStyleBackColor = true;
            this.ChkWrap.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // lblPanleInfo
            // 
            this.lblPanleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPanleInfo.AutoEllipsis = true;
            this.lblPanleInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPanleInfo.Location = new System.Drawing.Point(0, 0);
            this.lblPanleInfo.Name = "lblPanleInfo";
            this.lblPanleInfo.Size = new System.Drawing.Size(539, 23);
            this.lblPanleInfo.TabIndex = 1;
            this.lblPanleInfo.Text = "label1";
            this.lblPanleInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPanleInfo);
            this.panel1.Controls.Add(this.ChkWrap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 408);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 25);
            this.panel1.TabIndex = 3;
            // 
            // SqlTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "SqlTextBox";
            this.Size = new System.Drawing.Size(560, 433);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox ChkWrap;
        private System.Windows.Forms.Label lblPanleInfo;
        private System.Windows.Forms.Panel panel1;
    }
}

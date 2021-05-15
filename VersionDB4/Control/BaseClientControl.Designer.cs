
namespace VersionDB4.Control
{
    partial class BaseClientControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblClientCode = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblAuthentification = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBase = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code client";
            // 
            // lblClientCode
            // 
            this.lblClientCode.AutoSize = true;
            this.lblClientCode.Location = new System.Drawing.Point(147, 22);
            this.lblClientCode.Name = "lblClientCode";
            this.lblClientCode.Size = new System.Drawing.Size(67, 15);
            this.lblClientCode.TabIndex = 1;
            this.lblClientCode.Text = "Code client";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Serveur";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(147, 79);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(39, 15);
            this.lblServer.TabIndex = 3;
            this.lblServer.Text = "Server";
            // 
            // lblAuthentification
            // 
            this.lblAuthentification.AutoSize = true;
            this.lblAuthentification.Location = new System.Drawing.Point(147, 116);
            this.lblAuthentification.Name = "lblAuthentification";
            this.lblAuthentification.Size = new System.Drawing.Size(93, 15);
            this.lblAuthentification.TabIndex = 5;
            this.lblAuthentification.Text = "Authentification";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Authentification";
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(147, 154);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(39, 15);
            this.lblBase.TabIndex = 7;
            this.lblBase.Text = "Server";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Base";
            // 
            // BaseClientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAuthentification);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblClientCode);
            this.Controls.Add(this.label1);
            this.Name = "BaseClientControl";
            this.Size = new System.Drawing.Size(363, 187);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClientCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblAuthentification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.Label label5;
    }
}

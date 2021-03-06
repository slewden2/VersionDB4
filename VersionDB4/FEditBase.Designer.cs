
namespace VersionDB4
{
    partial class FEditBase
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblClientCode = new System.Windows.Forms.Label();
            this.cbClientCode = new System.Windows.Forms.ComboBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.cbAuthentification = new System.Windows.Forms.ComboBox();
            this.lblAuthentification = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cbBase = new System.Windows.Forms.ComboBox();
            this.lblBase = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitre = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(40, 73);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(42, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Nom";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(171, 69);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(320, 27);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // lblClientCode
            // 
            this.lblClientCode.AutoSize = true;
            this.lblClientCode.Location = new System.Drawing.Point(40, 126);
            this.lblClientCode.Name = "lblClientCode";
            this.lblClientCode.Size = new System.Drawing.Size(84, 20);
            this.lblClientCode.TabIndex = 3;
            this.lblClientCode.Text = "Code client";
            // 
            // cbClientCode
            // 
            this.cbClientCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClientCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClientCode.FormattingEnabled = true;
            this.cbClientCode.Location = new System.Drawing.Point(171, 123);
            this.cbClientCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbClientCode.Name = "cbClientCode";
            this.cbClientCode.Size = new System.Drawing.Size(320, 28);
            this.cbClientCode.TabIndex = 4;
            this.cbClientCode.SelectedIndexChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // txtServer
            // 
            this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServer.Location = new System.Drawing.Point(171, 188);
            this.txtServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtServer.MaxLength = 50;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(320, 27);
            this.txtServer.TabIndex = 6;
            this.txtServer.TextChanged += new System.EventHandler(this.ControlBase_TextChanged);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(40, 192);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(114, 20);
            this.lblServer.TabIndex = 5;
            this.lblServer.Text = "Nom du serveur";
            // 
            // cbAuthentification
            // 
            this.cbAuthentification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAuthentification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthentification.FormattingEnabled = true;
            this.cbAuthentification.Location = new System.Drawing.Point(171, 225);
            this.cbAuthentification.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbAuthentification.Name = "cbAuthentification";
            this.cbAuthentification.Size = new System.Drawing.Size(320, 28);
            this.cbAuthentification.TabIndex = 8;
            this.cbAuthentification.SelectedIndexChanged += new System.EventHandler(this.ControlBase_TextChanged);
            // 
            // lblAuthentification
            // 
            this.lblAuthentification.AutoSize = true;
            this.lblAuthentification.Location = new System.Drawing.Point(40, 228);
            this.lblAuthentification.Name = "lblAuthentification";
            this.lblAuthentification.Size = new System.Drawing.Size(115, 20);
            this.lblAuthentification.TabIndex = 7;
            this.lblAuthentification.Text = "Authentification";
            // 
            // txtLogin
            // 
            this.txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogin.Location = new System.Drawing.Point(210, 278);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(281, 27);
            this.txtLogin.TabIndex = 10;
            this.txtLogin.TextChanged += new System.EventHandler(this.ControlBase_TextChanged);
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(61, 281);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(123, 20);
            this.lblLogin.TabIndex = 9;
            this.lblLogin.Text = "Nom d\'utilisateur";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(210, 314);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(281, 27);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.TextChanged += new System.EventHandler(this.ControlBase_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(61, 318);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(98, 20);
            this.lblPassword.TabIndex = 11;
            this.lblPassword.Text = "Mot de passe";
            // 
            // cbBase
            // 
            this.cbBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase.FormattingEnabled = true;
            this.cbBase.Location = new System.Drawing.Point(171, 369);
            this.cbBase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBase.Name = "cbBase";
            this.cbBase.Size = new System.Drawing.Size(320, 28);
            this.cbBase.TabIndex = 14;
            this.cbBase.DropDown += new System.EventHandler(this.CbBase_DropDown);
            this.cbBase.SelectedIndexChanged += new System.EventHandler(this.Controls_TextChanged);
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Location = new System.Drawing.Point(40, 373);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(121, 20);
            this.lblBase.TabIndex = 13;
            this.lblBase.Text = "Base de données";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(378, 422);
            this.btCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(113, 37);
            this.btCancel.TabIndex = 16;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(249, 422);
            this.btOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(113, 37);
            this.btOk.TabIndex = 15;
            this.btOk.Text = "Valider";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblTitre);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 39);
            this.panel1.TabIndex = 16;
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitre.Location = new System.Drawing.Point(14, -2);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(218, 25);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "Edition d\'une base client";
            // 
            // FEditBase
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(503, 472);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.cbBase);
            this.Controls.Add(this.lblBase);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.cbAuthentification);
            this.Controls.Add(this.lblAuthentification);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.cbClientCode);
            this.Controls.Add(this.lblClientCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(687, 511);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(381, 511);
            this.Name = "FEditBase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblClientCode;
        private System.Windows.Forms.ComboBox cbClientCode;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.ComboBox cbAuthentification;
        private System.Windows.Forms.Label lblAuthentification;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.ComboBox cbBase;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitre;
    }
}
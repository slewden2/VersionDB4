
namespace VersionDB4
{
    partial class FResume
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblObjet = new System.Windows.Forms.Label();
            this.lblColumn = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblHelp2 = new System.Windows.Forms.Label();
            this.lblHelp4 = new System.Windows.Forms.Label();
            this.lblHelp3 = new System.Windows.Forms.Label();
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.cbWhat = new System.Windows.Forms.ComboBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtSchema = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtColumn = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 43);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Résumé personalisé";
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(131, 365);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(113, 37);
            this.btOk.TabIndex = 17;
            this.btOk.Text = "Valider";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(266, 365);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(113, 37);
            this.btCancel.TabIndex = 18;
            this.btCancel.Text = "Annuler";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Action";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Porte sur";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Base";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Schéma";
            // 
            // lblObjet
            // 
            this.lblObjet.AutoSize = true;
            this.lblObjet.Location = new System.Drawing.Point(14, 259);
            this.lblObjet.Name = "lblObjet";
            this.lblObjet.Size = new System.Drawing.Size(46, 20);
            this.lblObjet.TabIndex = 23;
            this.lblObjet.Text = "Objet";
            // 
            // lblColumn
            // 
            this.lblColumn.AutoSize = true;
            this.lblColumn.Location = new System.Drawing.Point(14, 311);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new System.Drawing.Size(64, 20);
            this.lblColumn.TabIndex = 24;
            this.lblColumn.Text = "Colonne";
            // 
            // lblHelp
            // 
            this.lblHelp.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHelp.ForeColor = System.Drawing.Color.Red;
            this.lblHelp.Location = new System.Drawing.Point(267, 108);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(114, 32);
            this.lblHelp.TabIndex = 25;
            this.lblHelp.Text = "Action incompatible avec le type d\'objet";
            // 
            // lblHelp2
            // 
            this.lblHelp2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHelp2.ForeColor = System.Drawing.Color.Red;
            this.lblHelp2.Location = new System.Drawing.Point(169, 202);
            this.lblHelp2.Name = "lblHelp2";
            this.lblHelp2.Size = new System.Drawing.Size(210, 32);
            this.lblHelp2.TabIndex = 26;
            this.lblHelp2.Text = "Si une base est précisée, il faut aussi préciser le schéma";
            // 
            // lblHelp4
            // 
            this.lblHelp4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHelp4.ForeColor = System.Drawing.Color.Red;
            this.lblHelp4.Location = new System.Drawing.Point(106, 286);
            this.lblHelp4.Name = "lblHelp4";
            this.lblHelp4.Size = new System.Drawing.Size(198, 18);
            this.lblHelp4.TabIndex = 27;
            this.lblHelp4.Text = "Le Nom d\'objet est requis";
            // 
            // lblHelp3
            // 
            this.lblHelp3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHelp3.ForeColor = System.Drawing.Color.Red;
            this.lblHelp3.Location = new System.Drawing.Point(106, 338);
            this.lblHelp3.Name = "lblHelp3";
            this.lblHelp3.Size = new System.Drawing.Size(177, 17);
            this.lblHelp3.TabIndex = 28;
            this.lblHelp3.Text = "Le nom de colonne est requis";
            // 
            // cbAction
            // 
            this.cbAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Location = new System.Drawing.Point(106, 62);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(273, 28);
            this.cbAction.TabIndex = 29;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.CbAction_SelectedIndexChanged);
            // 
            // cbWhat
            // 
            this.cbWhat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhat.FormattingEnabled = true;
            this.cbWhat.Location = new System.Drawing.Point(106, 108);
            this.cbWhat.Name = "cbWhat";
            this.cbWhat.Size = new System.Drawing.Size(155, 28);
            this.cbWhat.TabIndex = 30;
            this.cbWhat.SelectedIndexChanged += new System.EventHandler(this.Control_SelectedIndexChanged);
            // 
            // txtDatabase
            // 
            this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase.Location = new System.Drawing.Point(106, 155);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(273, 27);
            this.txtDatabase.TabIndex = 31;
            this.txtDatabase.TextChanged += new System.EventHandler(this.Control_SelectedIndexChanged);
            // 
            // txtSchema
            // 
            this.txtSchema.Location = new System.Drawing.Point(106, 202);
            this.txtSchema.Name = "txtSchema";
            this.txtSchema.Size = new System.Drawing.Size(48, 27);
            this.txtSchema.TabIndex = 32;
            this.txtSchema.TextChanged += new System.EventHandler(this.Control_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(106, 256);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(273, 27);
            this.txtName.TabIndex = 33;
            this.txtName.TextChanged += new System.EventHandler(this.Control_SelectedIndexChanged);
            // 
            // txtColumn
            // 
            this.txtColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColumn.Location = new System.Drawing.Point(106, 308);
            this.txtColumn.Name = "txtColumn";
            this.txtColumn.Size = new System.Drawing.Size(273, 27);
            this.txtColumn.TabIndex = 34;
            this.txtColumn.TextChanged += new System.EventHandler(this.Control_SelectedIndexChanged);
            // 
            // FResume
            // 
            this.AcceptButton = this.btOk;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(391, 414);
            this.Controls.Add(this.txtColumn);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtSchema);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.cbWhat);
            this.Controls.Add(this.cbAction);
            this.Controls.Add(this.lblHelp3);
            this.Controls.Add(this.lblHelp4);
            this.Controls.Add(this.lblHelp2);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.lblColumn);
            this.Controls.Add(this.lblObjet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(793, 453);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(407, 453);
            this.Name = "FResume";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblObjet;
        private System.Windows.Forms.Label lblColumn;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblHelp2;
        private System.Windows.Forms.Label lblHelp4;
        private System.Windows.Forms.Label lblHelp3;
        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.ComboBox cbWhat;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtSchema;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtColumn;
    }
}
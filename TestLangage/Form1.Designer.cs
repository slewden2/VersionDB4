
namespace TestLangage
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbAction = new System.Windows.Forms.ComboBox();
            this.cbWhat = new System.Windows.Forms.ComboBox();
            this.txtRegex = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btTest = new System.Windows.Forms.Button();
            this.lstMatch = new System.Windows.Forms.ListBox();
            this.btSave = new System.Windows.Forms.Button();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblInfos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbAction
            // 
            this.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAction.FormattingEnabled = true;
            this.cbAction.Location = new System.Drawing.Point(36, 22);
            this.cbAction.Name = "cbAction";
            this.cbAction.Size = new System.Drawing.Size(161, 23);
            this.cbAction.TabIndex = 0;
            this.cbAction.SelectedIndexChanged += new System.EventHandler(this.Choix_SelectedIndexChanged);
            // 
            // cbWhat
            // 
            this.cbWhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWhat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhat.FormattingEnabled = true;
            this.cbWhat.Location = new System.Drawing.Point(212, 22);
            this.cbWhat.Name = "cbWhat";
            this.cbWhat.Size = new System.Drawing.Size(469, 23);
            this.cbWhat.TabIndex = 1;
            this.cbWhat.SelectedIndexChanged += new System.EventHandler(this.Choix_SelectedIndexChanged);
            // 
            // txtRegex
            // 
            this.txtRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegex.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRegex.Location = new System.Drawing.Point(36, 51);
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.ReadOnly = true;
            this.txtRegex.Size = new System.Drawing.Size(644, 27);
            this.txtRegex.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(35, 117);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(645, 157);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // btTest
            // 
            this.btTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btTest.Location = new System.Drawing.Point(35, 292);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(78, 35);
            this.btTest.TabIndex = 4;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.BtTest_Click);
            // 
            // lstMatch
            // 
            this.lstMatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMatch.FormattingEnabled = true;
            this.lstMatch.ItemHeight = 15;
            this.lstMatch.Location = new System.Drawing.Point(36, 348);
            this.lstMatch.Name = "lstMatch";
            this.lstMatch.Size = new System.Drawing.Size(646, 94);
            this.lstMatch.TabIndex = 5;
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(592, 292);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(90, 35);
            this.btSave.TabIndex = 6;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.BtSave_Click);
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.ForeColor = System.Drawing.Color.Red;
            this.lblNo.Location = new System.Drawing.Point(141, 311);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(81, 15);
            this.lblNo.TabIndex = 7;
            this.lblNo.Text = "Aucun résutat";
            // 
            // lblInfos
            // 
            this.lblInfos.AutoSize = true;
            this.lblInfos.Location = new System.Drawing.Point(36, 85);
            this.lblInfos.Name = "lblInfos";
            this.lblInfos.Size = new System.Drawing.Size(16, 15);
            this.lblInfos.TabIndex = 8;
            this.lblInfos.Text = "...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 454);
            this.Controls.Add(this.lblInfos);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.lstMatch);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtRegex);
            this.Controls.Add(this.cbWhat);
            this.Controls.Add(this.cbAction);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbAction;
        private System.Windows.Forms.ComboBox cbWhat;
        private System.Windows.Forms.TextBox txtRegex;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.ListBox lstMatch;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblInfos;
    }
}


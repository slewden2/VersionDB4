using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Version = VersionDB4Lib.CRUD.Version;

namespace VersionDB4
{
    public partial class FNewVersion : Form
    {
        private bool isSelection = false;
        public FNewVersion() 
            => InitializeComponent();


        public void SetVersion(Version v1, Version v2)
        {
            rdChoice1.Text = v1.ToString();
            rdChoice1.Tag = v1;
            rdChoice2.Text = v2.ToString();
            rdChoice2.Tag = v2;
            rdChoice1.Checked = true;
        }

        public Version Choice()
            => isSelection ? (rdChoice1.Checked ? (Version)rdChoice1.Tag : (Version)rdChoice2.Tag) : null;

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (rdChoice1.Checked || rdChoice2.Checked)
            {
                isSelection = true;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VersionDB4Lib.UI;

namespace VersionDB4
{
    public partial class FTest : Form
    {

        public FTest()
        {
            InitializeComponent();

            richTextBox1.Text = @"
-------------------------------------------------------------------------------
SELECT * 
FROM dbo.toto

WHERE x = '5s33'
";

        }

        private void Button1_Click(object sender, EventArgs e) 
            => richTextBox1.ReadOnly = !richTextBox1.ReadOnly;

        private void Button2_Click(object sender, EventArgs e) 
            => richTextBox1.Visible = !richTextBox1.Visible;
    }
}

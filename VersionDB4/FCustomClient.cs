using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VersionDB4Lib.CRUD;

namespace VersionDB4
{
    public partial class FCustomClient : Form
    {

        public FCustomClient()
        {
            InitializeComponent();
            EnableButton();
        }

        public void Refill(List<ClientCode> list, ClientCode selected = null)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = list;
            listBox1.SelectedItem = selected;
            EnableButton();
        }

        public ClientCode ClientCode
            => listBox1.SelectedItem as ClientCode;
         

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e) 
            => EnableButton();

        private void EnableButton()
            => btOk.Enabled = listBox1.SelectedItem != null;

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                btOk.PerformClick();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;

namespace VersionDB4
{
    /// <summary>
    /// Gère l'édition d'une base client
    /// </summary>
    public partial class FEditBase : Form
    {
        private bool isValidated = false;
        private Base internalBase = null;
        
        /// <summary>
        /// indique si la liste des bases doit être rechargée
        /// </summary>
        private bool mustListbase = false;

        public FEditBase()
        {
            InitializeComponent();

            cbAuthentification.Items.Clear();
            cbAuthentification.Items.Add("Autentification Windows");
            cbAuthentification.Items.Add("Autentification SQL Server");
            cbAuthentification.SelectedIndex = 0;

            cbClientCode.DataSource = null;
            cbClientCode.DataSource = ClientCode.List();

            FillUiFromObject();
            
            btOk.Enabled = ControlUI();
        }

        public Base Base
        {
            get => !isValidated ? null : internalBase;
            set
            {
                internalBase = value;
                FillUiFromObject();
            }
        }


        private void Controls_TextChanged(object sender, EventArgs e) => btOk.Enabled = ControlUI();
        private void ControlBase_TextChanged(object sender, EventArgs e)
        {
            mustListbase = true;
            btOk.Enabled = ControlUI();
        }

        private void CbBase_DropDown(object sender, EventArgs e) => FillBases();

        private void BtOk_Click(object sender, EventArgs e)
        {
            isValidated = ControlUI();
            if (isValidated)
            {
                if (internalBase == null)
                {
                    internalBase = new Base();
                }

                internalBase.BaseName = txtName.Text;
                internalBase.ClientCodeId = GetClientCodeId();
                internalBase.BaseConnectionString = GetConnectionString();
                this.DialogResult = DialogResult.OK;
            }
        }

        private int GetClientCodeId()
         => cbClientCode.SelectedItem != null && cbClientCode.SelectedItem is ClientCode cc ? cc.ClientCodeId : 0;

        private void SelectClientCode(int clientCodeId)
        {
            foreach(ClientCode cc in cbClientCode.Items)
            {
                if (cc.ClientCodeId == clientCodeId)
                {
                    cbClientCode.SelectedItem = cc;
                    return;
                }
            }
        }

        private string GetConnectionString()
        {
            if (!CanSelectDatabase())
            {
                return string.Empty;
            }

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServer.Text,
                InitialCatalog = cbBase.SelectedValue.ToString()
            };

            if (cbAuthentification.SelectedIndex == 0)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = txtLogin.Text;
                builder.Password = txtPassword.Text;
            }

            return builder.ToString();
        }

        private void FillUiFromObject()
        {
            if (internalBase == null)
            {
                txtName.Text = string.Empty;
                cbClientCode.SelectedItem = null;
                txtServer.Text = string.Empty;
                cbAuthentification.SelectedItem = null;
                txtLogin.Text = string.Empty;
                txtPassword.Text = string.Empty;
                cbBase.SelectedItem = null;
                lblTitre.Text = "Création d'une base client";
            } 
            else
            {
                txtName.Text = internalBase.BaseName;
                SelectClientCode(internalBase.ClientCodeId);
                var builder = new SqlConnectionStringBuilder(internalBase.BaseConnectionString);
                txtServer.Text = builder.DataSource;
                cbAuthentification.SelectedIndex = builder.IntegratedSecurity ? 0 : 1;
                txtLogin.Text = builder.UserID;
                txtPassword.Text = builder.Password;
                mustListbase = true;
                FillBases(builder.InitialCatalog);
                lblTitre.Text = "Edition d'une base client";
            }

            mustListbase = false;
        }

        private bool ControlUI()
        {
            bool ok = !string.IsNullOrWhiteSpace(txtName.Text);
            ok = ok && cbClientCode.SelectedItem != null;
            ok = ok && !string.IsNullOrWhiteSpace(txtServer.Text);
            ok = ok && cbAuthentification.SelectedItem != null; ;

            bool okAuthent = cbAuthentification.SelectedIndex == 1;
            lblLogin.Enabled = okAuthent;
            txtLogin.Enabled = okAuthent;
            lblPassword.Enabled = okAuthent;
            txtPassword.Enabled = okAuthent;
            if (okAuthent)
            {
                ok = ok && !string.IsNullOrWhiteSpace(txtLogin.Text);
            }

            ok = ok && cbBase.SelectedItem != null;

            cbBase.Enabled = CanSelectDatabase();

            return ok;
        }

        private bool CanSelectDatabase()
        {
            if (string.IsNullOrWhiteSpace(txtServer.Text) || cbAuthentification.SelectedItem == null)
            {
                return false;
            }

            if (cbAuthentification.SelectedIndex == 1 && string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                return false;
            }

            return true;
        }

        private void FillBases(string catalog = null)
        {
            if (!mustListbase)
            {
                return;
            }

            if (!CanSelectDatabase())
            {
                return;
            }

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = txtServer.Text,
                InitialCatalog = "tempDb"
            };
            if (cbAuthentification.SelectedIndex == 0)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = txtLogin.Text;
                builder.Password = txtPassword.Text;
            }

            builder.ConnectTimeout = 1;

            cbBase.DataSource = null;
            try
            {
                cbBase.DataSource = ListDatabaseInfo.List(builder.ToString()).ToList();

                if (! string.IsNullOrWhiteSpace(catalog))
                {
                    cbBase.SelectedItem = catalog;
                }

                mustListbase = false;
            }
            catch
            {
            }
        }

    }
}

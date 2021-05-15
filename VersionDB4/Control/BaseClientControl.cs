using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VersionDB4Lib.CRUD;

namespace VersionDB4.Control
{
    public partial class BaseClientControl : UserControl
    {
        public BaseClientControl()
            => InitializeComponent();

        public Base ClientBase
        {
            set
            {
                if (value == null)
                {
                    lblClientCode.Text = string.Empty;
                    lblServer.Text = string.Empty;
                    lblAuthentification.Text = string.Empty;
                    lblBase.Text = string.Empty;
                }
                else
                {
                    lblClientCode.Text = ClientCode.List().First(x => x.ClientCodeId == value.ClientCodeId).ClientCodeName;
                    var builder = new SqlConnectionStringBuilder(value.BaseConnectionString);
                    lblServer.Text = builder.DataSource;
                    lblAuthentification.Text = builder.IntegratedSecurity ? "Authetification Windows" : $"Authentification SQL Server (login {builder.UserID})";
                    lblBase.Text = builder.InitialCatalog;
                }
            }
        }
    }
}

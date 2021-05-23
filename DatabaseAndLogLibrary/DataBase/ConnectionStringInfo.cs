using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DatabaseAndLogLibrary.DataBase
{
    /// <summary>
    /// Classe qui analyse ue chaine de connexion pour fournir de l'info dessus
    /// </summary>
    public class ConnectionStringInfo
    {
        private readonly SqlConnectionStringBuilder builder;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConnectionStringInfo" />
        /// </summary>
        /// <param name="cnn">La chaine de connexion</param>
        public ConnectionStringInfo(string cnn)
        {
            try
            {
                builder = new SqlConnectionStringBuilder(cnn);
            }
            catch
            {
                builder = new SqlConnectionStringBuilder();                     
            }
        }

        #region Properties
        /// <summary>
        /// Renvoie uniquement le nom du serveur
        /// </summary>
        public string ServeurOnly
        {
            get
            {
                string srv = builder.DataSource;
                int n = srv.IndexOf(',');
                if (n <= 0)
                {
                    return srv;
                }

                return srv.Substring(0, n);
            }
        }

        /// <summary>
        /// Renvoie les infos du serveur et son N° de port
        /// </summary>
        public string Serveur => builder.DataSource;

        /// <summary>
        /// Le nom de la base
        /// </summary>
        public string Base => builder.InitialCatalog;

        /// <summary>
        /// Sécurité intégrée ou pas
        /// </summary>
        public bool IntegratedSecurity => builder.IntegratedSecurity;

        /// <summary>
        /// L'utilisateur de la bdd
        /// </summary>
        public string User => builder.UserID;

        /// <summary>
        /// Le mot de passe
        /// </summary>
        public string Pass => builder.Password;

        /// <summary>
        /// Le port
        /// </summary>
        public string Port
        {
            get
            {
                string srv = builder.DataSource;
                int n = srv.IndexOf(',');
                if (n <= 0)
                {
                    return "[default]";
                }

                return srv[n..];
            }
        }

        #endregion

        /// <summary>
        /// Pour affichage ( = la chaine sans mot de passe !!
        /// </summary>
        /// <returns>Le texte à afficher</returns>
        public override string ToString()
            => $"Serveur = {builder.DataSource}; Base = {builder.InitialCatalog}";

        /// <summary>
        /// Lance ISQL sur cette base
        /// </summary>
        /// <param name="isqlPath">Le chemin de la bdd</param>
        /// <returns>Le message d'erreur</returns>
        public string LaunchIsql(string isqlPath) => this.LaunchIsql(isqlPath, string.Empty);

        /// <summary>
        /// Lance ISQL sur cette base
        /// </summary>
        /// <param name="isqlPath">Le chemin de la bdd</param>
        /// <param name="file">Le fichier à ouvrir en plus</param>
        /// <returns>Le message d'erreur</returns>
        public string LaunchIsql(string isqlPath, string file)
        {
            if (isqlPath == string.Empty)
            {
                return "Chemin d'accès non configuré";
            }

            bool v9 = isqlPath.ToLower(System.Globalization.CultureInfo.InvariantCulture).IndexOf("sqlwb.exe", StringComparison.InvariantCulture) >= 0;
            bool v10 = isqlPath.ToLower(System.Globalization.CultureInfo.InvariantCulture).IndexOf("ssms.exe", StringComparison.InvariantCulture) >= 0;

            System.Text.StringBuilder fil = new System.Text.StringBuilder();
            fil.AppendFormat(this.GetSqlCmdParam());

            if (file != string.Empty && System.IO.File.Exists(file))
            {
                fil.AppendFormat(" {0} \"{1}\"", (v9 || v10) ? string.Empty : "-f", file);
            }

            if (v10)
            {
                fil.Append(" -nosplash ");
            }

            System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo
            {
                FileName = isqlPath,
                Arguments = fil.ToString(),
                UseShellExecute = true
            };
            try
            {
                System.Diagnostics.Process.Start(p);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Renvoie les paramètre de connexion fonction de la Base
        /// Util pour les utilitaires SQL Server (comme BCP ou ISQL, ...)
        /// </summary>
        /// <returns>La ligne de commande</returns>
        public string GetSqlCmdParam()
        {
            System.Text.StringBuilder fil = new System.Text.StringBuilder();
            fil.Append($" -S \"{builder.DataSource}\" -d \"{builder.InitialCatalog}\"");
            if (builder.IntegratedSecurity)
            {
                fil.Append(" -E");
            }
            else
            {
                fil.AppendFormat($" -U \"{builder.UserID}\" -P \"{builder.Password}\"");
            }

            return fil.ToString();
        }
    }
}
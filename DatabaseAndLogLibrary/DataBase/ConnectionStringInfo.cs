using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAndLogLibrary.DataBase
{
    /// <summary>
    /// Classe qui analyse ue chaine de connexion pour fournir de l'info dessus
    /// </summary>
    public class ConnectionStringInfo
    {
        #region Membre privés
        /// <summary>
        /// Le serveur sql
        /// </summary>
        private readonly string serveur;

        /// <summary>
        /// Le nom de la base
        /// </summary>
        private readonly string laBase;

        /// <summary>
        /// Sécurité intégrée ou pas ?
        /// </summary>
        private readonly bool integratedSecurity;

        /// <summary>
        /// Le user
        /// </summary>
        private readonly string user;

        /// <summary>
        /// Le passxord
        /// </summary>
        private readonly string password;

        /// <summary>
        /// Le port
        /// </summary>
        private readonly string port;
        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ConnectionStringInfo" />
        /// </summary>
        /// <param name="cnn">La chaine de connexion</param>
        public ConnectionStringInfo(string cnn)
        {
            string[] nfo = cnn.Split(';');
            foreach (string d in nfo)
            {
                if (d.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("data source="))
                {
                    this.serveur = d[12..];
                    int n = this.serveur.IndexOf(",", StringComparison.InvariantCulture);
                    if (n != -1)
                    {
                        this.port = this.serveur[(n + 1)..];
                        this.serveur = this.serveur.Substring(0, n);
                    }
                    else
                    {
                        this.port = string.Empty;
                    }
                }
                else if (d.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("initial catalog="))
                {
                    this.laBase = d[16..];
                }
                else if (d.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("integrated security=sspi"))
                {
                    this.integratedSecurity = true;
                    this.user = string.Empty;
                    this.password = string.Empty;
                }
                else if (d.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("user id="))
                {
                    this.integratedSecurity = false;
                    this.user = d[8..];
                }
                else if (d.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("password="))
                {
                    this.integratedSecurity = false;
                    this.password = d[9..];
                }
            }
        }

        #region Properties
        /// <summary>
        /// Renvoie uniquement le nom du serveur
        /// </summary>
        public string ServeurOnly => this.serveur;

        /// <summary>
        /// Renvoie les infos du serveur et son N° de port
        /// </summary>
        public string Serveur
        {
            get
            {
                if (string.IsNullOrEmpty(this.port))
                {
                    return this.ServeurOnly;
                }
                else
                {
                    return $"{this.serveur},{this.port}";
                }
            }
        }

        /// <summary>
        /// Le nom de la base
        /// </summary>
        public string Base => this.laBase;

        /// <summary>
        /// Sécurité intégrée ou pas
        /// </summary>
        public bool IntegratedSecurity => this.integratedSecurity;

        /// <summary>
        /// L'utilisateur de la bdd
        /// </summary>
        public string User => this.user;

        /// <summary>
        /// Le mot de passe
        /// </summary>
        public string Pass => this.password;

        /// <summary>
        /// Le port
        /// </summary>
        public string Port
        => !string.IsNullOrEmpty(this.port) ? this.port : "[default]";

        #endregion

        /// <summary>
        /// Pour affichage ( = la chaine sans mot de passe !!
        /// </summary>
        /// <returns>Le texte à afficher</returns>
        public override string ToString()
        {
            System.Text.StringBuilder res = new System.Text.StringBuilder();
            res.Append($"Serveur = {this.Serveur}");
            if (this.port != string.Empty)
            {
                res.Append($",{this.port}");
            }

            res.Append($"; Base = {this.Base}");
            return res.ToString();
        }

        /// <summary>
        /// Lance ISQL sur cette base
        /// </summary>
        /// <param name="isqlPath">Le chemin de la bdd</param>
        /// <returns>Le message d'erreur</returns>
        public string LaunchIsql(string isqlPath)
        {
            return this.LaunchIsql(isqlPath, string.Empty);
        }

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

            System.Diagnostics.ProcessStartInfo p = new System.Diagnostics.ProcessStartInfo();
            p.FileName = isqlPath;
            p.Arguments = fil.ToString();
            p.UseShellExecute = true;
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
            fil.AppendFormat(" -S \"{0}", this.Serveur);
            if (this.port != string.Empty)
            {
                fil.AppendFormat(",{0}", this.port);
            }

            fil.Append("\"");
            fil.AppendFormat(" -d \"{0}\"", this.Base);
            if (this.IntegratedSecurity)
            {
                fil.Append(" -E");
            }
            else
            {
                fil.AppendFormat(" -U \"{0}\" -P \"{1}\"", this.User, this.Pass);
            }

            return fil.ToString();
        }

    }
}
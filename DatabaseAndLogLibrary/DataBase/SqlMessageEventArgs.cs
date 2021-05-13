using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseAndLogLibrary.DataBase
{
    /// <summary>
    /// Classe paramètre pour les messages de notification en provenance de ConnectionParam
    /// Sert à notifier les messages en provenance du serveur : Message d'information, Erreurs, Nombre de lignes impactées
    /// </summary>
    public sealed class SqlMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Empêche la création d'une instance par défaut de la classe<see cref="SqlMessageEventArgs" />.
        /// </summary>
        private SqlMessageEventArgs()
        {
        }

        /// <summary>
        /// Obtient le texte du message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Obtient le SQL Error (qui n'est pas forcement une erreur) mais le message renvoyé par le moteur SQL server
        /// Les messages dont la propriété 'class' est supérieure à 16 sont des erreurs 
        /// </summary>
        public SqlError Error { get; private set; }

        /// <summary>
        /// Obtient une valeur indiquant si ce message constitue une erreur ou pas
        /// </summary>
        public bool IsError => this.Error != null && this.Error.Class >= 16;

        /// <summary>
        /// Obtient le nombre de lignes affecté de la dernier instruction
        /// </summary>
        public int RowCount { get; private set; }

        /// <summary>
        /// Renvoie un SqlMessageEventArg créé à partir d'un SqlError
        /// </summary>
        /// <param name="e">Le SqlError</param>
        /// <returns>L'objet instancié</returns>
        public static SqlMessageEventArgs From(SqlError e)
            => new SqlMessageEventArgs()
            {
                Message = e.Message,
                Error = e,
            };

        /// <summary>
        /// Renvoie un SqlMessageEventArg créé à partir d'un seul message texte 
        /// </summary>
        /// <param name="message">Le message</param>
        /// <returns>L'objet instancié</returns>
        public static SqlMessageEventArgs From(string message)
            => new SqlMessageEventArgs()
            {
                Message = message
            };

        /// <summary>
        /// Renvoie un SqlMessageEventArg créé à partir d'un résultat de requête
        /// </summary>
        /// <param name="e">Le résultat de requête</param>
        /// <returns>L'objet instancié</returns>
        public static SqlMessageEventArgs From(StatementCompletedEventArgs e)
            => new SqlMessageEventArgs()
            {
                RowCount = e.RecordCount
            };

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Message))
            {
                return DisplayCount(this.RowCount, "No row affected", "One unique row affected", "{0} rows affected");
            }
            else if (this.Error == null || !this.IsError)
            {
                return this.Message;
            }
            else
            {
                string p = !string.IsNullOrWhiteSpace(this.Error.Procedure) ? $" Procedure : {this.Error.Procedure}" : string.Empty;
                string s = !string.IsNullOrWhiteSpace(this.Error.Server) ? $" Sever : {this.Error.Server}" : string.Empty;
                string o = !string.IsNullOrWhiteSpace(this.Error.Source) ? $" Source : {this.Error.Source}" : string.Empty;

                string msg = this.Error.Message.Replace((char)160, ' ');
                return $"Message {this.Error.Number}, Level {this.Error.Class}, Status {this.Error.State}, Line {this.Error.LineNumber} : {msg}{s}{p}{o}";
            }

        }

        /// <summary>
        /// Affiche un compteur (normalement entier positif !)
        /// </summary>
        /// <param name="count">Nombre de valeur</param>
        /// <param name="textIfNone">Texte a afficher si le compteur est à 0</param>
        /// <param name="textIfOne">Texte a afficher si le compteur est à 1</param>
        /// <param name="text">Texte a afficher si le compteur est supérieur à 1 (doit contenir un formatteur d'indice 0 pour le nombre)</param>
        /// <returns>le texte a afficher</returns>
        private static string DisplayCount(int count, string textIfNone, string textIfOne, string text)
        {
            if (count == 0)
            {
                return textIfNone;
            }
            else if (count == 1)
            {
                return textIfOne;
            }
            else
            {
                return string.Format(text, count);
            }
        }
    }
}

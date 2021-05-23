using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DatabaseAndLogLibrary.DataBase
{
    /// <summary>
    /// Crée une connexion à SQL Server (uniquement), et fournit les méthodes pour tout faire avec : SELECT, EXcte, avec ou sans trantrations
    /// Gère les transactions imbriquées
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    public class DatabaseConnection : IDatabaseConnection
    {
        /// <summary>
        /// L'évent d'infos sur la connexion
        /// </summary>
        private event EventHandler<SqlMessageEventArgs> OnConnectionInfoMessageInternal;

        /// <summary>
        /// Y a t il un élément à plugger ?
        /// </summary>
        private bool isEventPluggedOnConection;

        protected int transactionLevel = 0;
        protected SqlConnection connection;
        protected DbTransaction transaction;
        protected bool bufferd = true;

        private readonly string customConnectionString = string.Empty;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DatabaseConnection"/>
        /// </summary>
        public DatabaseConnection()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this.CommandTimeOut = Provider.ExecutRequestTimeOut;
        }

        public DatabaseConnection(string connectionString)
            : this() => customConnectionString = connectionString;

        /// <summary>
        /// Renvoie le provider SQL server par default
        /// </summary>
        public SqlServerDatabaseProvider Provider { get; } = SqlServerDatabaseProvider.Instance;

        /// <summary>
        /// Un client s'abonne pour avoir les évènements d'information sur la connexion sql
        /// </summary>
        public event EventHandler<SqlMessageEventArgs> OnConnectionInfoMessage
        {
            add
            {
                if (!isEventPluggedOnConection)
                {
                    isEventPluggedOnConection = true;
                    if (connection != null && connection.State != ConnectionState.Closed && connection.State != ConnectionState.Broken)
                    {
                        connection.FireInfoMessageEventOnUserErrors = false;
                        connection.InfoMessage += new SqlInfoMessageEventHandler(this.Connection_InfoMessage);
                    }
                }

                OnConnectionInfoMessageInternal = value;
            }

            remove
            {
                if (isEventPluggedOnConection)
                {
                    if (connection != null && connection.State != ConnectionState.Closed && connection.State != ConnectionState.Broken)
                    {
                        connection.InfoMessage -= new SqlInfoMessageEventHandler(this.Connection_InfoMessage);
                    }

                    isEventPluggedOnConection = false;
                    OnConnectionInfoMessageInternal = null;
                }
            }
        }

        /// <summary>
        /// Renvoie le niveau d'imbrication des transactions
        /// </summary>
        public int TransactionLevel => this.transactionLevel;

        /// <summary>
        /// Renvoie ou définit le délai d'exécution en millisecondes
        /// </summary>
        public int? CommandTimeOut { get; set; }

        /// <summary>
        /// Nettoyage de l'objet
        /// </summary>
        public void Dispose()
        {
            if (isEventPluggedOnConection)
            {
                connection.FireInfoMessageEventOnUserErrors = false;
                connection.InfoMessage -= new SqlInfoMessageEventHandler(this.Connection_InfoMessage);
            }

            this.RollBackTransaction();
            this.connection?.Dispose();
            this.connection = null;
            this.Provider?.Dispose();
        }

        /// <summary>
        /// Execute une requête sur la base de données et renvoie la liste typée d'objets
        /// </summary>
        /// <typeparam name="T">Le type de retour attendu</typeparam>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>La liste de résultats</returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null) 
            => Connection.Query<T>(sql, param, transaction, bufferd, CommandTimeOut, commandType);

        /// <summary>
        /// Execute une requête sur la base de données et renvoie le premier objet typée trouvé
        /// </summary>
        /// <typeparam name="T">Le type de retour attendu</typeparam>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>L'objet trouvé correctement instancié</returns>
        public T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null) 
            => Connection.QueryFirst<T>(sql, param, transaction, CommandTimeOut, commandType);

        /// <summary>
        /// Execute une requête sur la base de données et renvoie le premier objet typée trouvé ou l'objet par defaut
        /// </summary>
        /// <typeparam name="T">Le type de retour attendu</typeparam>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>L'objet trouvé correctement instancié ou l'objet par défaut de la classe T (souvent null)</returns>
        public T QueryFirstOrDefault<T>(string sql, object param = null, CommandType? commandType = null) 
            => Connection.QueryFirstOrDefault<T>(sql, param, transaction, CommandTimeOut, commandType);

        /// <summary>
        /// Execute une requête qui renvoie 2 dataTable à combiner sur un seul type de retour 
        /// Le type de requête doit renseigner des objets de type "Parent enfant" (comme un client et ses factures par exemple)
        /// </summary>
        /// <typeparam name="TFirst">Le premier type attendu (type parent)</typeparam>
        /// <typeparam name="TSecond">Le second type attendu (Type enfant)</typeparam>
        /// <typeparam name="TReturn">Le type de résultat (Parent avec enfant) (il peut être le même que le TFirst ou différent)</typeparam>
        /// <param name="sql">La requête SQL</param>
        /// <param name="map">La fonction de mapping qui permet à partir des 2 premiers types de passer au type de retour</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="splitOn">Optionnel la colonne qui permet de d'agreger les resultats</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>La liste de résultats</returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType? commandType = null) 
            => Connection.Query<TFirst, TSecond, TReturn>(sql, map, param, transaction, bufferd, splitOn, CommandTimeOut, commandType);

        /// <summary>
        /// Execute une requête qui renvoie plusieurs listes de résultat à recupérer
        /// </summary>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>La grille de résultat à lire</returns>
        public GridReader QueryMultiple(string sql, object param = null, CommandType? commandType = null) 
            => Connection.QueryMultiple(sql, param, transaction, CommandTimeOut, commandType);

        /// <summary>
        /// Execute une requête SQL qui ne renvoie pas de liste de données (Requête action : comme un UPDATE par exemple)
        /// </summary>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>Le code de retourn de l'appel</returns>
        public int Execute(string sql, object param = null, CommandType? commandType = null)
        {
            if (param is IEnumerable)
            {
                return Connection.Execute(sql, param, transaction, CommandTimeOut, commandType);
            }

            // On remplace ici Execute par ExecuteReader pour que les évènements info_message soient déclenchés durant le traitement
            // => Connection.Execute(sql, param, transaction, CommandTimeOut, commandType);
            int nb = -1;
            using (IDataReader reader = Connection.ExecuteReader(sql, param, transaction, CommandTimeOut, commandType))
            {
                nb = reader.RecordsAffected;
            }

            return nb;
        }

        /// <summary>
        /// Execute de manière asynchrone une requête SQL qui ne renvoie pas de liste de données (Requête action : comme un UPDATE par exemple)
        /// </summary>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>La tache contenant le code de retourn de l'appel</returns>
        public Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null)
            => Connection.ExecuteAsync(sql, param, transaction, CommandTimeOut, commandType);

        /// <summary>
        /// Execute une requête SQL qui renvoit la colonne "[Key]" de la première ligne du jeu de résultat
        /// </summary>
        /// <param name="sql">La requête SQL</param>
        /// <param name="param">Optionnel L'objet paramètre de la requête</param>
        /// <param name="commandType">Optionnel le type de requête (Texte SQL ou procédure stockée)</param>
        /// <returns>Le code de retourn de l'appel</returns>
        public int ExecuteScalar(string sql, object param = null, CommandType? commandType = null)
        {
            var x =  this.Connection.QueryFirstOrDefault(sql, param, transaction, CommandTimeOut, commandType);
            return Convert.ToInt32(x?.Key ?? 0);
        }

        /// <summary>
        /// Démarre une transaction (imbriquée ou pas)
        /// </summary>
        public void BeginTransaction()
        {
            this.transactionLevel++;
            if (this.TransactionLevel == 1)
            {
                this.transaction = this.Connection.BeginTransaction();
            }
        }

        /// <summary>
        /// Termine une transaction (Seule la dernière transaction s'il y en a plusieures imbriquées sera réélement commitée sur la base)
        /// </summary>
        public void CommitTransaction()
        {
            this.transactionLevel--;
            if (this.TransactionLevel <= 0 && this.transaction != null)
            {
                this.transaction.Commit();
                this.transactionLevel = 0;
            }

            if (this.TransactionLevel < 0)
            {
                this.transactionLevel = 0;
            }
        }

        /// <summary>
        /// Annulte toutes les transaction en cours sur la base de données
        /// </summary>
        public void RollBackTransaction()
        {
            if (this.TransactionLevel > 0 && this.transaction != null)
            {
                this.transaction.Rollback();
            }
               
            this.transactionLevel = 0;
        }

        /// <summary>
        /// Renvoie la connexion : crée la si besoin
        /// </summary>
        protected SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = Provider.GetOpenConnection(customConnectionString);
                    if (isEventPluggedOnConection)
                    {
                        connection.InfoMessage += new SqlInfoMessageEventHandler(this.Connection_InfoMessage);
                    }
                }

                return connection;
            }
        }

        /// <summary>
        /// Propage les évènements de la connexion au parent
        /// </summary>
        /// <param name="sender">Qui appelle</param>
        /// <param name="e">Paramètre du message</param>
        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            if (e.Errors.Count > 0)
            {
                foreach (SqlError err in e.Errors)
                {
                    this.OnConnectionInfoMessageInternal?.Invoke(this, SqlMessageEventArgs.From(err));
                }
            }
            else if (!string.IsNullOrWhiteSpace(e.Message))
            {
                this.OnConnectionInfoMessageInternal?.Invoke(this, SqlMessageEventArgs.From(e.Message));
            }
        }
    }
}

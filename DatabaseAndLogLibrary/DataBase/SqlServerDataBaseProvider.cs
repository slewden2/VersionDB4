using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

namespace DatabaseAndLogLibrary.DataBase
{
    /// <summary>
    /// Fournit un moyen unifié de se connecter à un serveur SQL Server
    /// Le time out utilisé pour les requetes execute peut s'ajuter dans la appsettings.json : clé "DataBaseSettings": { "ExecutRequestTimeOut": 120 },
    /// La chaine de connexion se définit dans le fichier appsettings.json : clé "ConnectionStrings": { "base": "xxx"}
    /// </summary>
    public sealed class SqlServerDatabaseProvider : IDisposable
    {
        public const string APPSETTING_FILE_NAME = "appsettings.json";
        public const string APPSETTING_CONNECTION_STRING_RUBRIQUE = "Base";
        public const string APPSETTING_DATABASE_SETTINGS = "DataBaseSettings";
        public const string APPSETTING_DATABASE_EXECTIMEOUT = "ExecutRequestTimeOut";

        private readonly string configurationString;
        private readonly int? executRequestTimeOut;

        /// <summary>
        /// Obtient une valeur indiquant le fournisseur de database
        /// </summary>
        private readonly SqlClientFactory factory = SqlClientFactory.Instance;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="SqlServerDatabaseProvider"/>
        /// </summary>
        public SqlServerDatabaseProvider()
        {
            DatabaseSettings dBaseSettings = SqlServerDatabaseProvider.Settings();
            if (dBaseSettings == null || string.IsNullOrWhiteSpace(dBaseSettings.Base))
            {
                throw new ArgumentException($"the file {APPSETTING_FILE_NAME} does not containts {APPSETTING_CONNECTION_STRING_RUBRIQUE} rubrik with the connection string");
            }

            if (dBaseSettings.ExecutRequestTimeOut.HasValue)
            {
                this.executRequestTimeOut = dBaseSettings.ExecutRequestTimeOut.Value;
            }

            this.configurationString = dBaseSettings.Base;
        }

        public static void ResetInstance()
        {

        }


        /// <summary>
        /// Fournit le singleton du connecteur 
        /// </summary>
        public static SqlServerDatabaseProvider Instance { get; } = Activator.CreateInstance<SqlServerDatabaseProvider>();

        /// <summary>
        /// Renvoie la chaine de connexion à la base de données
        /// </summary>
        /// <returns>la chaine de connexion</returns>
        public string GetConnectionString() => configurationString;

        /// <summary>
        /// Délai d'exécution des requetes Execute
        /// </summary>
        public int? ExecutRequestTimeOut => this.executRequestTimeOut;

        public void Dispose()
        {
        }

        /// <summary>
        /// Renvoie une chaine de connexion
        /// </summary>
        /// <param name="mars">Indique si la connexion doit accepter les jeux de résultats actifs multiples (Multiple Active Result Set)</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>une connexion ouverte</returns>
        public SqlConnection GetOpenConnection(string customConnectionString, bool mars)
        {
            if (!mars)
            {
                return GetOpenConnection(customConnectionString);
            }

            var scsb = factory.CreateConnectionStringBuilder();
            try
            {
                scsb.ConnectionString = string.IsNullOrWhiteSpace(customConnectionString) ? GetConnectionString() : customConnectionString;
                ((dynamic)scsb).MultipleActiveResultSets = true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Creation connexion failed", ex);
            }

            var conn = (SqlConnection)factory.CreateConnection();
            conn.ConnectionString = scsb.ConnectionString;
            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex2)
            {
                throw new InvalidOperationException("Opening connexion (Multiple Active Result Set) Fail", ex2);
            }
        }

        /// <summary>
        /// Renvoie une connexion ouverte
        /// </summary>
        /// <returns>une connexion ouverte</returns>
        public SqlConnection GetOpenConnection(string customConnectionString)
        {
            var conn = (SqlConnection)factory.CreateConnection();
            try
            {
                conn.ConnectionString = string.IsNullOrWhiteSpace(customConnectionString) ? GetConnectionString() : customConnectionString;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Creation connexion failed", ex);
            }

            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex2)
            {
                throw new InvalidOperationException("Opening connexion Fail", ex2);
            }
        }

        /// <summary>
        /// Renvoie une connexion fermée (mais prête à être ouverte)
        /// </summary>
        /// <returns>une connexion fermée</returns>
        public SqlConnection GetClosedConnection()
        {
            var conn = (SqlConnection)factory.CreateConnection();
            try
            {
                conn.ConnectionString = GetConnectionString();
                return conn;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Creation connexion failed", ex);
            }
        }

        public DbParameter CreateRawParameter(string name, object value)
        {
            var p = factory.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            return p;
        }

        /// <summary>
        /// Renvoie les propriétés nécessaire à la connexion à la base de données
        /// </summary>
        private static DatabaseSettings Settings()
        {
            var fullFileName = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, APPSETTING_FILE_NAME);
            if (File.Exists(fullFileName))
            {
                var configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                      .AddJsonFile(APPSETTING_FILE_NAME, false)
                      .Build();


                var section = configuration.GetSection(nameof(DatabaseSettings));
                return section.Get<DatabaseSettings>();
            }

            return null;
        }
    }
}

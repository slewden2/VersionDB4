using System;
using System.Collections.Generic;
using System.Data;

namespace DatabaseAndLogLibrary
{
    /// <summary>
    /// Fournit les moyens d'accès à la base ainsi que le requétage simplifié
    /// </summary>
    public interface IDatabaseConnection : IDisposable
    {
        /// <summary>
        /// Execute une requeête de type select et renvoie la liste d'objets typés trouvés
        /// </summary>
        /// <typeparam name="T">Le type d'objet requété</typeparam>
        /// <param name="sql">Le texte SQL de la requête</param>
        /// <param name="param">Les paramètres de la requête</param>
        /// <param name="commandType">le type de requête</param>
        /// <returns>La liste des données typées</returns>
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);
        
        /// <summary>
        /// Execute une requeête de type select et renvoie la première ligne de la liste d'objets typés trouvés
        /// </summary>
        /// <typeparam name="T">Le type d'objet requété</typeparam>
        /// <param name="sql">Le texte SQL de la requête</param>
        /// <param name="param">Les paramètres de la requête</param>
        /// <param name="commandType">le type de requête</param>
        /// <returns>Le premier objet de la liste</returns>
        T QueryFirst<T>(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute une requeête de type select et renvoie la première colonne de la première ligne de la liste d'objets typés trouvés
        /// </summary>
        /// <param name="sql">Le texte SQL de la requête</param>
        /// <param name="param">Les paramètres de la requête</param>
        /// <param name="commandType">le type de requête</param>
        /// <returns>Lapremière valeur de la première ligne de la liste</returns>
        int ExecuteScalar(string sql, object param = null, CommandType? commandType = null);

        /// <summary>
        /// Execute une requête de type action (ne renvoyant pas de résultat)
        /// </summary>
        /// <param name="sql">Le texte SQL de la requête</param>
        /// <param name="param">Les paramètres de la requête</param>
        /// <param name="commandType">le type de requête</param>
        /// <returns>Le nombre de lignes impactées par la requête</returns>
        int Execute(string sql, object param = null, CommandType? commandType = null);


        /// <summary>
        /// Démarre une transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Valide la transaction en cours s'il y a lieu
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Annule la transaction en cours s'il y a lieu
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// Renvoie le niveau d'imbrication des transactions 
        /// (<0 invalide, 0= pas de transaction)
        /// </summary>
        int TransactionLevel { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAndLogLibrary.DataBase;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Les types d'actions : 18 valeurs
    /// 
    /// 0 Inconu
    /// 1 DBComparer
    /// 2 CodeClient
    /// 3 Comment
    /// 4 Create
    /// 5 Alter
    /// 6 Drop
    /// 7 Rename
    /// 8 AddColumn
    /// 9 AlterColumn
    /// 10 DropColumn
    /// 11 RenameColumn
    /// 12 Update
    /// 13 Insert
    /// 14 Delete
    /// 15 Execute
    /// 16 AddColumNotNull
    /// 17 RaiseError
    /// 18 Print   ==> TODO identifier RaiseError du print no wait
    /// </summary>
    public class SqlAction
    {
        private static List<SqlAction> actions = null;

        /// <summary>
        /// Clé du type d'action
        /// </summary>
        public int SqlActionId { get; set; }

        /// <summary>
        /// Nom du type d'action
        /// </summary>
        public string SqlActionName { get; set; }

        /// <summary>
        /// Indique si l'action concerne une colonne ou pas
        /// </summary>
        public bool SqlActionIsForColumn { get; set; }

        /// <summary>
        /// Indique si l'action concerne une table ou pas
        /// </summary>
        public bool SqlActionIsForTable { get; set; }

        /// <summary>
        /// Indique si l'action concerne un index ou pas
        /// </summary>
        public bool SqlActionIsForIndex { get; set; }

        /// <summary>
        /// Le titre pour afficher l'action
        /// </summary>
        public string SqlActionTitle { get; set; }

        public override string ToString() => SqlActionName;

        public static int Unknow => 0;
        public static int DbComparer => 1;
        public static int CodeClient => 2;
        public static int Comment => 3;
        public static int Create => 4;
        public static int Alter => 5;
        public static int Drop => 6;
        public static int Rename => 7;
        public static int AddColumn => 8;
        public static int AlterColumn => 9;
        public static int DropColumn => 10;
        public static int RenameColumn => 11;
        public static int Update => 12;
        public static int Insert => 13;
        public static int Delete => 14;
        public static int Execute => 15;
        public static int AddColumnNotNull => 16;

        public static int RaiseError = 17;
        public static int Print = 18;

        public static bool IsForColumn(int action)
            => List().First(x => x.SqlActionId == action).SqlActionIsForColumn;
        public static bool IsForIndex(int action)
            => List().First(x => x.SqlActionId == action).SqlActionIsForIndex;

        public static string SQLSelect
            => @"
SELECT SqlActionId, SqlActionName, SqlActionIsForColumn, SqlActionIsForTable, SqlActionIsForIndex, SqlActionTitle 
FROM dbo.SqlAction
";

        public static List<SqlAction> List()
        {
            if (actions == null)
            {
                ////using var cnn = new DatabaseConnection();
                ////actions = cnn.Query<SqlAction>(SqlAction.SQLSelect).ToList();
                ///
                actions = new List<SqlAction>()
                {
                    new SqlAction() { SqlActionId = 0,  SqlActionName = "UnKnow",          SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Rien à faire"},
                    new SqlAction() { SqlActionId = 1,  SqlActionName = "DBComparer",      SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Script créé par DBComparer"},
                    new SqlAction() { SqlActionId = 2,  SqlActionName = "CodeClient",      SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Code spécifique à un client"},
                    new SqlAction() { SqlActionId = 3,  SqlActionName = "Comment",         SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Commentaire"},
                    new SqlAction() { SqlActionId = 4,  SqlActionName = "Create",          SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = true,  SqlActionTitle = "Création de"},
                    new SqlAction() { SqlActionId = 5,  SqlActionName = "Alter",           SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = true,  SqlActionTitle = "Modification de"},
                    new SqlAction() { SqlActionId = 6,  SqlActionName = "Drop",            SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = true,  SqlActionTitle = "Suppression de"},
                    new SqlAction() { SqlActionId = 7,  SqlActionName = "Rename",          SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Changement de nom de"},
                    new SqlAction() { SqlActionId = 8,  SqlActionName = "AddColumn",       SqlActionIsForColumn = true,  SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Ajout de colonne à"},
                    new SqlAction() { SqlActionId = 9,  SqlActionName = "AlterColumn",     SqlActionIsForColumn = true,  SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Modification d'une colonne de"},
                    new SqlAction() { SqlActionId = 10, SqlActionName = "DropColumn",      SqlActionIsForColumn = true,  SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Suppression d'une colonne dans"},
                    new SqlAction() { SqlActionId = 11, SqlActionName = "RenameColumn",    SqlActionIsForColumn = true,  SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Changement de nom d'une colonne de"},
                    new SqlAction() { SqlActionId = 12, SqlActionName = "Update",          SqlActionIsForColumn = false, SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Mise à jour de données dans"},
                    new SqlAction() { SqlActionId = 13, SqlActionName = "Insert",          SqlActionIsForColumn = false, SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Insersion de données dans"},
                    new SqlAction() { SqlActionId = 14, SqlActionName = "Delete",          SqlActionIsForColumn = false, SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Suppression de données dans"},
                    new SqlAction() { SqlActionId = 15, SqlActionName = "Execute",         SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Execution de"},
                    new SqlAction() { SqlActionId = 16, SqlActionName = "AddColumNotNull", SqlActionIsForColumn = true,  SqlActionIsForTable = true,  SqlActionIsForIndex = false, SqlActionTitle = "Ajout de colonne non nulle à"},
                    new SqlAction() { SqlActionId = 17, SqlActionName = "RaiseError",      SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Erreur programmée"},
                    new SqlAction() { SqlActionId = 18, SqlActionName = "Print",           SqlActionIsForColumn = false, SqlActionIsForTable = false, SqlActionIsForIndex = false, SqlActionTitle = "Affichage d'un message"},
                };
            }

            return actions;
        }


        /// <summary>
        /// Donne un nom à une action
        /// </summary>
        /// <param name="action">L'action à nommer</param>
        /// <param name="colName">Nom de la colonne</param>
        /// <returns>Le nom</returns>
        public static string Name(SqlAction action, string colName) 
            => action.SqlActionId switch
                {
                    // SqlAction.AddColumn:
                    8 => $"Ajout de colonne {colName} à",
                    // SqlAction.AddColumnNotNull:
                    16 => $"Ajout de colonne non nulle {colName} à",
                    //SqlAction.AlterColumn:
                    9 => $"Modification de la colonne {colName} de",
                    // SqlAction.DropColumn:
                    10 => $"Suppression de la colonne {colName} dans",
                    // SqlAction.RenameColumn:
                    11 => $"Changement de nom de la colonne {colName} de",
                    _ => action.SqlActionName,
                };
    }
}
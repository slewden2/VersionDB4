using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib.Business
{
    /// <summary>
    /// Type de validation
    /// </summary>
    public enum EValidation : byte
    {
        /// <summary>
        /// Aucune en attente
        /// </summary>
        None = 0,

        /// <summary>
        /// Validé par le user
        /// </summary>
        Valide = 1,

        /// <summary>
        /// refuseé ou non valide
        /// </summary>
        NonValide = 2,

        /// <summary>
        /// Imposé manuellement
        /// </summary>
        Manuel = 3,

        /// <summary>
        /// Le script a été supprimé il doit être ignoré
        /// </summary>
        Supprime = 4,

        /// <summary>
        /// Valeur utiliser pour les effacements de résumés avec des codes clients
        /// </summary>
        Effacement = 254
    }

    public enum ETypeObjectPresentable
    {
        Project = 1,
        VersionReferential = 2,
        VersionScript = 3,
        SqlGroup = 4,
        SqlObject = 5,
        Script = 6,
        Clients = 7,
        Client = 8,
    }


    public enum EAction
    {
        /// <summary>
        /// Annule l'opération en cours
        /// </summary>
        Cancel = 0,


        //SqlGroupReLoad = 10,
        //SqlGroupReoadFromBdd = 11,
        SqlObjectAddBegin = 12,
        SqlObjectAddEnd = 13,
        SqlObjectEditBegin = 14,
        SqlObjectEditEnd = 15,

        SqlObjectDelete = 31,
        SqlObjectLock = 32,
        SqlObjectUnlock = 33,
        SqlObjectAddCustomClient = 34,
        SqlObjectRemoveCustomClient = 35,
        SqlObjectMakeFullCustomClient = 36,
        SqlObjectSaveSqlToDisk = 37,


        ProjectScriptReload = 40,           // rechargement global des versions avec les scipts
        ProjectReferentialReload = 41,      // rechargement global des versions avec les objets SQL
        ProjectVersionAdd = 42,             // Ajout d'une version
        ProjectVersionDelete = 43,          // Supression d'une version (vide !)

        ScriptAddBegin = 50,
        ScriptAddEnd = 51,
        ScriptEditBegin = 52,
        ScriptEditEnd = 53,
        ScriptDelete = 54,
        ScriptAnalyze = 55,

        ClientsReload = 60,                 // rechargement de la liste des clients
        ClientAdd = 61,
        //ClientReload = 62,
        ClientEdit = 63,
        ClientDel = 64

    }


}

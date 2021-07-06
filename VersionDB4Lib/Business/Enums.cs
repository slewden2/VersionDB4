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

    /// <summary>
    /// Gère les statuts et les actions associés à un import 
    /// statuts : Egaux/Différents/nouveau/..
    /// Actions : importer/ne pas importer/...
    /// </summary>
    public enum EImportType
    {
        /// <summary>
        /// Inconnu (ou non encore défini)
        /// </summary>
        Unkonw = 0,
        
        /// <summary>
        /// Ne pas importer
        /// </summary>
        Nop = 1,

        /// <summary>
        /// Objets identiques (ne pas importer)
        /// </summary>
        Equal = 2,

        /// <summary>
        /// Objets différents (décider quoi faire)
        /// </summary>
        Different = 3,

        /// <summary>
        /// Importer en tant que nouvel objet
        /// </summary>
        New = 4,

        /// <summary>
        /// Objets différents, importer comme référentiel
        /// </summary>
        DifferentImportAsReferential = 5,

        /// <summary>
        /// Objets différents, importer comme spécifique client
        /// </summary>
        DifferentImportASCustomClient = 6
    }

    /// <summary>
    /// Les catégories d'objets présenté dans les écrans
    /// </summary>
    public enum ETypeObjectPresentable
    {
        Project = 1,
        VersionReferential = 2,
        VersionScript = 3,
        SqlGroup = 4,
        SqlObject = 5,
        SQlObjectCustomClient = 6,
        Script = 7,
        Clients = 8,
        Client = 9,
    }

    /// <summary>
    /// Les actions métier disponible dans l'interface
    /// </summary>
    public enum EAction
    {
        /// <summary>
        /// Annule l'opération en cours
        /// </summary>
        Cancel = 0,


        SqlObjectAddBegin = 12,
        SqlObjectAddEnd = 13,
        SqlObjectEditBegin = 14,
        SqlObjectEditEnd = 15,
        SqlObjectAddCustomClientEnd = 16,

        SqlObjectDelete = 31,
        SqlObjectLock = 32,
        SqlObjectUnlock = 33,
        SqlObjectAddCustomClient = 34,
        SqlObjectRemoveCustomClient = 35,
        SqlObjectMakeFullCustomClient = 36,
        SqlObjectSaveSqlToDisk = 37,


        ProjectScriptReload = 40,           // rechargement global des versions avec les scipts
        ProjectReferentialReload = 41,      // rechargement global des versions avec les objets SQL
        
        
        VersionAdd = 42,             // Ajout d'une version
        VersionDelete = 43,          // Supression d'une version (vide !)
        VersionLock = 44,
        VersionUnLock = 45,


        ScriptAddBegin = 50,
        ScriptAddEnd = 51,
        ScriptEditBegin = 52,
        ScriptEditEnd = 53,
        ScriptDelete = 54,
        ScriptAnalyze = 55,

        ClientsReload = 60,                 // rechargement de la liste des clients
        ClientAdd = 61,
        ClientEdit = 63,
        ClientDel = 64,

        ClientDBToReferential = 65,

    }


}

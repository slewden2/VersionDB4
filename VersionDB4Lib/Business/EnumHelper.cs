using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.ForUI;

namespace VersionDB4Lib.Business
{
    public static class EnumHelper
    {
        public static string Counter(this int n, string zero, string one, string plurial)
            => n == 0 ? zero : n == 1 ? one : string.Format(plurial, n);

        public static readonly Color CSTLockColor = Color.Gold;  // Unlock  lock

        public static IEnumerable<EAction> GetActions(this ETypeObjectPresentable typeObjectPresentable, bool versionIsLocked, object theObject)
        {
            switch (typeObjectPresentable)
            {
                case ETypeObjectPresentable.VersionReferential:
                    yield return EAction.ProjectReferentialReload;
                    break;

                case ETypeObjectPresentable.SqlGroup:
                    if (!versionIsLocked)
                    {
                        yield return EAction.SqlObjectAddBegin;
                    }

                    break;
                case ETypeObjectPresentable.SqlObject:
                    if (!versionIsLocked && theObject != null && theObject is CRUD.Object sqlObject && !sqlObject.ObjectDeleted)
                    {
                        yield return EAction.SqlObjectEditBegin;
                        yield return EAction.SqlObjectDelete;

                        using var cnn = new DatabaseConnection();
                        if (sqlObject.CanAddCustomClient(cnn))
                        {
                            yield return EAction.SqlObjectAddCustomClient;
                        }
                    }

                    break;
                case ETypeObjectPresentable.SQlObjectCustomClient:
                    if (!versionIsLocked && theObject != null && theObject is CRUD.Object sqlObjectClient && !sqlObjectClient.ObjectDeleted)
                    {
                        yield return EAction.SqlObjectEditBegin;
                        yield return EAction.SqlObjectRemoveCustomClient;
                    }

                    break;
                case ETypeObjectPresentable.Project:
                    yield return EAction.ProjectScriptReload;
                    yield return EAction.VersionAdd;
                    break;
                case ETypeObjectPresentable.VersionScript:
                    if (versionIsLocked)
                    {
                        yield return EAction.VersionUnLock;
                        
                    }
                    else
                    {
                        yield return EAction.ScriptAddBegin;

                        if (theObject is VersionScriptCounter vc && vc.IsLastVersion && vc.Count == 0 && vc.CountObject == 0)
                        {
                            yield return EAction.VersionDelete;
                        }

                        yield return EAction.VersionLock;
                    }

                    yield return EAction.VersionToDisk;

                    break;

                case ETypeObjectPresentable.Script:
                    if (!versionIsLocked)
                    {
                        yield return EAction.ScriptEditBegin;
                        yield return EAction.ScriptAnalyze;
                        yield return EAction.ScriptDelete;
                    }
                    else
                    {
                        yield return EAction.ScriptAnalyze;
                    }

                    break;

                case ETypeObjectPresentable.Clients:
                    yield return EAction.ClientsReload;
                    yield return EAction.ClientAdd;
                    break;
                case ETypeObjectPresentable.Client:
                    yield return EAction.ClientEdit;
                    yield return EAction.ClientDel;
                    yield return EAction.ClientDBToReferential;
                    break;
            }
        }

        public static string GetIcon(this EAction action)
              => action switch
              {
                  EAction.Cancel => "",                        // 0xE106;  Cancel

                  EAction.ClientAdd => "",                     // 0xE109;  Add
                  EAction.ClientEdit => "",                    // 0xE104;  Edit
                  //EAction.ClientReload => "",                  // 0xE72C;  Reload
                  EAction.ClientDel => "",                     // 0xE107;  Delete
                  EAction.ClientsReload => "",                 // 0xE72C;
                  EAction.ClientDBToReferential => "",         // 0xEA53; client to référential (exporter)


                  EAction.ProjectReferentialReload => "",      // 0xE72C;
                  EAction.ProjectScriptReload => "",           // 0xE72C;

                  EAction.ScriptAddBegin => "",                // 0xE109;
                  EAction.ScriptEditBegin => "",               // 0xE104;
                  EAction.ScriptAddEnd => "",                  // 0xE081;  Valider
                  EAction.ScriptEditEnd => "",                 // 0xE081;
                  EAction.ScriptDelete => "",                  // 0xE107; 
                  EAction.ScriptAnalyze => "",                 // 0xE773;  Analyze

                  EAction.SqlObjectAddBegin => "",             // 0xE109;  
                  EAction.SqlObjectAddEnd => "",               // 0xE081; 
                  EAction.SqlObjectEditBegin => "",            // 0xE104;  
                  EAction.SqlObjectEditEnd => "",              // 0xE081; 
                  EAction.SqlObjectDelete => "",               // 0xE107;  

                  EAction.SqlObjectAddCustomClient => "",      // 0xE8FA;  Custom client Add
                  EAction.SqlObjectAddCustomClientEnd => "",   // 0xE081; 

                  EAction.SqlObjectLock => "",                 // 0xE72E;  Lock
                  EAction.SqlObjectMakeFullCustomClient => "", // 0xE2AF;  Full client
                  EAction.SqlObjectRemoveCustomClient => "",   // 0xE8CF;  Custom client Remove
                  EAction.SqlObjectSaveSqlToDisk => "",        // 0xEA35;  Save to disk
                  EAction.SqlObjectUnlock => "",               // 0xE785;  Unlock
                                                                //EAction.VersionScriptAdd => "",              // 0xE109;

                  EAction.VersionDelete => "",                 // 0xE107;  Delete
                  EAction.VersionAdd => "",                    // 0xE109;
                  EAction.VersionLock => "",                   // 0xE72E;  Lock
                  EAction.VersionUnLock => "",                 // 0xE785;  Unlock
                  EAction.VersionToDisk => "",                 // 0xE74E;  Save to Disque

                  //EAction.VersionScriptRefresh => "",          // 0xE72C;
                  _ => string.Empty
              };

        public static Color GetColor(this EAction action)
                => action switch
                {
                    EAction.Cancel => Color.Red,                             // Cancel  : Rouge

                    EAction.ClientAdd => Color.Navy,                         // Add     : Bleu
                    EAction.ClientEdit => Color.DarkViolet,                  // Edit    : Rose
                    //EAction.ClientReload => Color.DeepSkyBlue,             // Reload  : Bleu clair
                    EAction.ClientsReload => Color.DeepSkyBlue,
                    EAction.ClientDel => Color.Red,                          // Delete  : rouge
                    EAction.ClientDBToReferential => Color.Navy,              // Exporter : Bleu

                    EAction.ProjectReferentialReload => Color.DeepSkyBlue,
                    EAction.ProjectScriptReload => Color.DeepSkyBlue,

                    EAction.ScriptAddBegin => Color.Navy,
                    EAction.ScriptEditBegin => Color.DarkViolet,
                    EAction.ScriptAddEnd => Color.MediumSeaGreen,            // Valider : Vert
                    EAction.ScriptEditEnd => Color.MediumSeaGreen,
                    EAction.ScriptDelete => Color.Red,
                    EAction.ScriptAnalyze => Color.Violet,                 // Analyze script Violet ?

                    EAction.SqlObjectAddBegin => Color.Navy,
                    EAction.SqlObjectAddEnd => Color.MediumSeaGreen,
                    EAction.SqlObjectEditBegin => Color.DarkViolet,
                    EAction.SqlObjectEditEnd => Color.MediumSeaGreen,
                    EAction.SqlObjectDelete => Color.Red,
                    EAction.SqlObjectAddCustomClient => Color.Navy,          // Add Client    : Bleu ?
                    EAction.SqlObjectAddCustomClientEnd => Color.MediumSeaGreen,

                    EAction.SqlObjectLock => CSTLockColor,                     // Lock : jaune foncé
                    EAction.SqlObjectMakeFullCustomClient => Color.Navy,     // Full Client    : Bleu ?
                    EAction.SqlObjectRemoveCustomClient => Color.Red,
                    EAction.SqlObjectSaveSqlToDisk => Color.Navy,            // Save disk     : Bleu ?
                    EAction.SqlObjectUnlock => CSTLockColor,                   // UnLock : jaune foncé 
                    //EAction.VersionScriptAdd => Color.Navy,
                    
                    EAction.VersionDelete => Color.Red,
                    EAction.VersionAdd => Color.Navy,
                    EAction.VersionLock => CSTLockColor,                     // Lock : jaune foncé
                    EAction.VersionUnLock => CSTLockColor,                   // UnLock : jaune foncé 
                    EAction.VersionToDisk => Color.Navy,                   


                    _ => Color.Black
                };

        public static string GetToolTipText(this EAction action)
            => action switch
            {
                EAction.Cancel => "Annuler l'opération en cours",
                EAction.SqlObjectAddBegin => "Ajouter un élément",
                EAction.SqlObjectAddEnd => "Valider l'ajout",
                EAction.SqlObjectEditBegin => "Modifier l'élément",
                EAction.SqlObjectEditEnd => "Valider les modifications",
                EAction.SqlObjectAddCustomClient => "Ajouter une implémentation client",
                EAction.SqlObjectAddCustomClientEnd => "Valider l'implémentation client",
                EAction.SqlObjectDelete => "Supprimer l'élément sélectionné",
                EAction.SqlObjectLock => "Vérouiller l'objet",
                EAction.SqlObjectUnlock => "Déverrouller l'objet",
                EAction.SqlObjectRemoveCustomClient => "Supprimer cette implémentation client",
                EAction.SqlObjectMakeFullCustomClient => "Vider l'implémentation par défaut",
                EAction.SqlObjectSaveSqlToDisk => "Enregistrer sur disque",
                EAction.ProjectScriptReload => "Recharger les scripts",
                EAction.ProjectReferentialReload => "Recharger le référentiel",
                EAction.VersionAdd => "Ajouter une version",
                EAction.VersionDelete => "Supprimer cette version vide",
                EAction.VersionLock => "Verrouiller cette version",
                EAction.VersionUnLock => "Libérer cette version",
                EAction.VersionToDisk => "Exporter sur disque",
                EAction.ScriptAddBegin => "Ajouter un script manuellement",
                EAction.ScriptAddEnd => "Valider l'ajout du script",
                EAction.ScriptEditBegin => "Modifier ce script",
                EAction.ScriptEditEnd => "Valider la modification du script",
                EAction.ScriptDelete => "Supprimer ce script",
                EAction.ScriptAnalyze => "Voir les détails de l'analyse du script",
                EAction.ClientsReload => "Recharger la liste des clients",
                EAction.ClientAdd => "Ajouter un client",
                EAction.ClientEdit => "Modifier ce client",
                EAction.ClientDel => "Supprimer cette base client du référentiel",
                EAction.ClientDBToReferential => "Importer les informations de la base client dans le référentiel",
                _ => string.Empty
            };

        /// <summary>
        /// Renvoie une chaine pour afficher la représentation de l'objet
        /// </summary>
        /// <returns>Le texte à afficher</returns>
        public static string ToString(SqlAction action, TypeObject applyOn, ObjectIdentifier identifier)
        {
            if (action == null && applyOn == null)
            {
                return "[Aucune action pour ce script]";
            }
            ////else if (action.SqlActionId == SqlAction.CodeClient)
            ////{
            ////    string clients = this.ClientCodes.Select(x => x == 0 ? "[Tous les clients]" : x == ALLOTHERCLIENTCODE ? "[+ tous les autres clients]" : x.ToString()).Aggregate((x, y) => x + ", " + y);
            ////    return $"S'applique pour les clients : {clients}";
            ////}
            ////else if (action.SqlActionId == SqlAction.DbComparer)
            ////{
            ////    if (!string.IsNullOrWhiteSpace(this.FullName))
            ////    {
            ////        if (this.ApplyOn == TypeObject.None)
            ////        {
            ////            return $"Script DBComparer ajout ou modification de l'état perso : {this.Name}";
            ////        }
            ////        else
            ////        {
            ////            return $"Script DBComparer pour {this.ApplyOn.Name()} {this.FullName}";
            ////        }
            ////    }
            ////    else
            ////    {
            ////        return $"Script DBComparer pour {this.ApplyOn.Name()}";
            ////    }
            ////}
            else if (action.SqlActionIsForColumn && !string.IsNullOrWhiteSpace(identifier.Column))
            { // Quand on a des infos de colonnes
                return $"{SqlAction.Name(action, identifier.Column)} {applyOn.TypeObjectName} {identifier}";
            }
            else
            {
                return $"{action.SqlActionTitle} {applyOn.TypeObjectName.ToLower()} {identifier}";
            }
        }



        public static string Libelle(this EValidation validation)
            => validation switch
            {
                EValidation.None => "A valider",
                EValidation.Valide => "Validé ",
                EValidation.NonValide => "Refusé ",
                EValidation.Manuel => "Imposé ",
                EValidation.Supprime => "Invalide ",
                EValidation.Effacement => "Supprimé",
                _ => "?"
            };

        public static Color GetColor(this EValidation validation)
            => validation switch
            {
                EValidation.None => Color.Orange,
                EValidation.Valide => Color.Green,
                EValidation.NonValide => Color.Red,
                EValidation.Manuel => Color.Navy,
                EValidation.Supprime => Color.Red,
                EValidation.Effacement => Color.DarkGray,
                _ => Color.Black
            };

        public static string Libelle(this EImportType import)
            => import switch
            {
                EImportType.Unkonw => "...",
                EImportType.Nop => "Ne pas importer",
                EImportType.Equal => "Identique (Ne pas importer)",
                EImportType.Different => "Différent [Choix à définir]",
                EImportType.New => "Nouveau (importer dans le reférentiel)",
                EImportType.DifferentImportAsReferential => "Remplacer le référentiel existant",
                EImportType.DifferentImportASCustomClient => "Importer comme spécifique au client",
                _ => ((int)import).ToString()
            };

        /// <summary>
        /// quels sont les types d'import qui sont des actions Vs des statuts
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static bool IsAction(this EImportType import)
            => import switch
            {
                EImportType.Unkonw => false,
                EImportType.Nop => false,
                EImportType.Equal => false,
                EImportType.Different => false,
                EImportType.New => true,
                EImportType.DifferentImportAsReferential => true,
                EImportType.DifferentImportASCustomClient => true,
                _ => false
            };

        /// <summary>
        /// Le type d'import nécessite t il un choix utilisateur
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public static bool IsChoice(this EImportType import)
            => import == EImportType.Different || import == EImportType.Unkonw;

    }
}

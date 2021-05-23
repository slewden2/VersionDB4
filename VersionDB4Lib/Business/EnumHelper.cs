using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.ForUI;

namespace VersionDB4Lib.Business
{
    public static class EnumHelper
    {
        public static string Counter(this int n, string zero, string one, string plurial)
            => n == 0 ? zero : n == 1 ? one : string.Format(plurial, n);


        public static IEnumerable<EAction> GetActions(this ETypeObjectPresentable typeObjectPresentable, object theObject)
        {
            switch (typeObjectPresentable)
            {
                case ETypeObjectPresentable.VersionReferential:
                    yield return EAction.ProjectReferentialReload;
                    break;

                case ETypeObjectPresentable.SqlGroup:
                    //yield return EAction.SqlGroupReLoad;
                    //yield return EAction.SqlGroupReoadFromBdd;
                    yield return EAction.SqlObjectAddBegin;
                    break;
                case ETypeObjectPresentable.SqlObject:
                    if (theObject != null && theObject is CRUD.Object sqlObject)
                    {
                        if (!sqlObject.ObjectDeleted)
                        {
                            ////if (string.IsNullOrWhiteSpace(sqlObject.ObjectLockedBy))
                            ////{
                            yield return EAction.SqlObjectEditBegin;
                            yield return EAction.SqlObjectDelete;
                            ////yield return EAction.SqlObjectLock;

                            ////if (sqlObject.ObjectEmpty)
                            ////{
                            ////    yield return EAction.SqlObjectAddCustomClient;
                            ////    yield return EAction.SqlObjectRemoveCustomClient;
                            ////}
                            ////else
                            ////{
                            ////    yield return EAction.SqlObjectMakeFullCustomClient;
                            ////}
                            ////}
                            ////else
                            ////{
                            ////    yield return EAction.SqlObjectUnlock;
                            ////}
                        }

                        // yield return EAction.SqlObjectSaveSqlToDisk;
                    }

                    break;

                case ETypeObjectPresentable.Project:
                    yield return EAction.ProjectScriptReload;
                    yield return EAction.ProjectVersionAdd;
                    break;
                case ETypeObjectPresentable.VersionScript:
                    ////yield return EAction.VersionScriptRefresh;
                    yield return EAction.ScriptAddBegin;

                    if (theObject is VersionScriptCounter vc && vc.IsLastVersion && vc.Count == 0 && vc.CountObject == 0)
                    {
                        yield return EAction.ProjectVersionDelete;
                    }
                    break;

                case ETypeObjectPresentable.Script:
                    yield return EAction.ScriptEditBegin;
                    yield return EAction.ScriptAnalyze;
                    yield return EAction.ScriptDelete;
                    break;

                case ETypeObjectPresentable.Clients:
                    yield return EAction.ClientsReload;
                    yield return EAction.ClientAdd;
                    break;
                case ETypeObjectPresentable.Client:
                    //yield return EAction.ClientReload;
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

                  //EAction.SqlGroupReLoad => "",                // 0xE72C;  
                  //EAction.SqlGroupReoadFromBdd => "",          // 0xE72C;   
                  EAction.SqlObjectAddCustomClient => "",      // 0xE8FA;  Custom client Add
                  EAction.SqlObjectDelete => "",               // 0xE107;  
                  EAction.SqlObjectLock => "",                 // 0xE72E;  Lock
                  EAction.SqlObjectMakeFullCustomClient => "", // 0xE2AF;  Full client
                  EAction.SqlObjectRemoveCustomClient => "",   // 0xE8CF;  Custom client Remove
                  EAction.SqlObjectSaveSqlToDisk => "",        // 0xEA35;  Save to disk
                  EAction.SqlObjectUnlock => "",               // 0xE785;  Unlock
                                                                //EAction.VersionScriptAdd => "",              // 0xE109;

                  EAction.ProjectVersionDelete => "",           // 0xE107;  Delete
                  EAction.ProjectVersionAdd => "",             // 0xE109;
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

                    //EAction.SqlGroupReLoad => Color.DeepSkyBlue,
                    //EAction.SqlGroupReoadFromBdd => Color.DeepSkyBlue,

                    EAction.SqlObjectAddCustomClient => Color.Navy,          // Add Client    : Bleu ?
                    EAction.SqlObjectLock => Color.Gold,                     // Lock : jaune foncé
                    EAction.SqlObjectMakeFullCustomClient => Color.Navy,     // Full Client    : Bleu ?
                    EAction.SqlObjectRemoveCustomClient => Color.Red,
                    EAction.SqlObjectSaveSqlToDisk => Color.Navy,            // Save disk     : Bleu ?
                    EAction.SqlObjectUnlock => Color.Gold,                   // UnLock : jaune foncé 
                    //EAction.VersionScriptAdd => Color.Navy,
                    EAction.ProjectVersionDelete => Color.Red,
                    EAction.ProjectVersionAdd => Color.Navy,

                    _ => Color.Black
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

        public static bool IsChoice(this EImportType import)
            => import == EImportType.Different || import == EImportType.Unkonw;

    }
}

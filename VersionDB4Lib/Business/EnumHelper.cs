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
        public static IEnumerable<EAction> GetActions(this ETypeObjectPresentable typeObjectPresentable, object theObject)
        {
            switch (typeObjectPresentable)
            {
                case ETypeObjectPresentable.VersionReferential:
                    yield return EAction.VersionReferentialRefresh;
                    break;

                case ETypeObjectPresentable.SqlGroup:
                    yield return EAction.SqlGroupReLoad;
                    yield return EAction.SqlGroupReoadFromBdd;
                    yield return EAction.SqlGroupAdd;
                    break;
                case ETypeObjectPresentable.SqlObject:
                    if (theObject != null && theObject is CRUD.Object sqlObject)
                    {
                        if (!sqlObject.ObjectDeleted)
                        {
                            if (string.IsNullOrWhiteSpace(sqlObject.ObjectLockedBy))
                            {
                                yield return EAction.SqlObjectEdit;
                                yield return EAction.SqlObjectDelete;
                                yield return EAction.SqlObjectLock;

                                if (sqlObject.ObjectEmpty)
                                {
                                    yield return EAction.SqlObjectAddCustomClient;
                                    yield return EAction.SqlObjectRemoveCustomClient;
                                }
                                else
                                {
                                    yield return EAction.SqlObjectMakeFullCustomClient;
                                }
                            }
                            else
                            {
                                yield return EAction.SqlObjectUnlock;
                            }
                        }

                        yield return EAction.SqlObjectSaveSqlToDisk;
                    }

                    break;

                case ETypeObjectPresentable.Project:
                    yield return EAction.ProjectScriptReload;
                    yield return EAction.ProjectVersionScriptAdd;
                    break;
                case ETypeObjectPresentable.VersionScript:
                    ////yield return EAction.VersionScriptRefresh;
                    yield return EAction.ScriptBeginAdd;

                    if (theObject is VersionScriptCounter vc && vc.IsLastVersion && vc.CountScript == 0 && vc.CountObject == 0)
                    {
                        yield return EAction.ProjetVersionScriptDelete;
                    }
                    break;

                case ETypeObjectPresentable.Script:
                    yield return EAction.ScriptBeginEdit;
                    yield return EAction.ScriptAnalyze;
                    yield return EAction.ScriptDelete;
                    break;

                case ETypeObjectPresentable.Clients:
                    yield return EAction.ClientsReload;
                    yield return EAction.ClientAdd;
                    break;
                case ETypeObjectPresentable.Client:
                    yield return EAction.ClientReload;
                    yield return EAction.ClientEdit;
                    yield return EAction.ClientDel;
                    break;
            }
        }

        public static string GetIcon(this EAction action)
              => action switch
              {
                  EAction.Cancel => "",                        // 0xE106;  Cancel
                  EAction.ClientAdd => "",                     // 0xE109;  Add
                  EAction.ClientEdit => "",                    // 0xE104;  Edit
                  EAction.ClientReload => "",                  // 0xE72C;  Reload
                  EAction.ClientDel => "",                     // 0xE107;  Delete
                  EAction.ClientsReload => "",                 // 0xE72C;
                  EAction.ProjectReferentialReload => "",      // 0xE72C;
                  EAction.ProjectScriptReload => "",           // 0xE72C;
                  EAction.ScriptAnalyze => "",                 // 0xE773;  Analyze
                  EAction.ScriptBeginAdd => "",                // 0xE109;
                  EAction.ScriptBeginEdit => "",               // 0xE104;
                  EAction.ScriptEndAdd => "",                  // 0xE081;  Valider
                  EAction.ScriptEndEdit => "",                 // 0xE081;
                  EAction.ScriptDelete => "",                  // 0xE107; 
                  EAction.SqlGroupAdd => "",                   // 0xE109;  
                  EAction.SqlGroupReLoad => "",                // 0xE72C;  
                  EAction.SqlGroupReoadFromBdd => "",          // 0xE72C;   
                  EAction.SqlObjectAddCustomClient => "",      // 0xE8FA;  Custom client Add
                  EAction.SqlObjectDelete => "",               // 0xE107;  
                  EAction.SqlObjectEdit => "",                 // 0xE104;  
                  EAction.SqlObjectLock => "",                 // 0xE72E;  Lock
                  EAction.SqlObjectMakeFullCustomClient => "", // 0xE2AF;  Full client
                  EAction.SqlObjectRemoveCustomClient => "",   // 0xE8CF;  Custom client Remove
                  EAction.SqlObjectSaveSqlToDisk => "",        // 0xEA35;  Save to disk
                  EAction.SqlObjectUnlock => "",               // 0xE785;  Unlock
                  EAction.VersionReferentialRefresh => "",     // 0xE72C;
                  EAction.VersionScriptAdd => "",              // 0xE109;
                  EAction.ProjetVersionScriptDelete => "",           // 0xE107;  Delete
                  EAction.ProjectVersionScriptAdd => "",             // 0xE109;
                                                                      //EAction.VersionScriptRefresh => "",          // 0xE72C;
                  _ => string.Empty
              };

        public static Color GetColor(this EAction action)
                => action switch
                {
                    EAction.Cancel => Color.Red,                             // Cancel  : Rouge
                    EAction.ClientAdd => Color.Navy,                         // Add     : Bleu
                    EAction.ClientEdit => Color.DarkViolet,                  // Edit    : Rose
                    EAction.ClientReload => Color.DeepSkyBlue,               // Reload  : Bleu clair
                    EAction.ClientsReload => Color.DeepSkyBlue,
                    EAction.ClientDel => Color.Red,                          // Delete  : rouge
                    EAction.ProjectReferentialReload => Color.DeepSkyBlue,
                    EAction.ProjectScriptReload => Color.DeepSkyBlue,
                    EAction.ScriptBeginAdd => Color.Navy,
                    EAction.ScriptBeginEdit => Color.DarkViolet,
                    EAction.ScriptEndAdd => Color.MediumSeaGreen,            // Valider : Vert
                    EAction.ScriptEndEdit => Color.MediumSeaGreen,
                    EAction.ScriptDelete => Color.Red,
                    EAction.SqlGroupAdd => Color.Navy,
                    EAction.SqlGroupReLoad => Color.DeepSkyBlue,
                    EAction.SqlGroupReoadFromBdd => Color.DeepSkyBlue,
                    EAction.SqlObjectAddCustomClient => Color.Navy,          // Add Client    : Bleu ?
                    EAction.SqlObjectDelete => Color.Red,
                    EAction.SqlObjectEdit => Color.DarkViolet,
                    EAction.SqlObjectLock => Color.Gold,                     // Lock : jaune foncé
                    EAction.SqlObjectMakeFullCustomClient => Color.Navy,     // Full Client    : Bleu ?
                    EAction.SqlObjectRemoveCustomClient => Color.Red,
                    EAction.SqlObjectSaveSqlToDisk => Color.Navy,            // Save disk     : Bleu ?
                    EAction.SqlObjectUnlock => Color.Gold,                   // UnLock : jaune foncé 
                    EAction.VersionReferentialRefresh => Color.DeepSkyBlue,
                    EAction.VersionScriptAdd => Color.Navy,
                    EAction.ProjetVersionScriptDelete => Color.Red,
                    EAction.ProjectVersionScriptAdd => Color.Navy,

                    _ => Color.Black
                };



        /// <summary>
        /// Renvoie une chaine pour afficher la représentation de l'objet
        /// </summary>
        /// <returns>Le texte à afficher</returns>
        public static string ToString(SqlAction action, SqlWhat applyOn, string database, string schema, string name, string column)
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
            ////        if (this.ApplyOn == SqlWhat.None)
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
            else if (action.SqlActionIsForColumn && !string.IsNullOrWhiteSpace(column))
            { // Quand on a des infos de colonnes
                string db = string.IsNullOrWhiteSpace(database) ? string.Empty : $"{database}.";
                string sh = string.IsNullOrWhiteSpace(schema) ? string.Empty : $"{schema}.";
                string theName = $"{db}{sh}{name}";
                return $"{SqlAction.Name(action, column)} {applyOn.SqlWhatName} {theName}";
            }
            else
            {
                return $"{action.SqlActionTitle} {applyOn.SqlWhatName} {ToString(database, schema, name)}";
            }
        }

        /// <summary>
        /// Renvoie le nom formatté d'un objet pour une base
        /// </summary>
        /// <param name="database">Le nom de la Base</param>
        /// <param name="schema">Le nom du schéma</param>
        /// <param name="name">Le nom de l'objet</param>
        /// <returns></returns>
        public static string ToString(string database, string schema, string name)
        {
            string db = string.IsNullOrWhiteSpace(database) ? string.Empty : $"{database}.";
            string sh = string.IsNullOrWhiteSpace(schema) ? string.Empty : $"{schema}.";
            return $"{db}{sh}{name}";
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
    }
}

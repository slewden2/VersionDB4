﻿using System;
using System.Collections.Generic;
using System.Linq;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Une action synthétique résumée
    /// </summary>
    public class Resume
    {
        #region Properties
        /// <summary>
        /// Clé unique du résumé
        /// </summary>
        public int ResumeId { get; set; }

        /// <summary>
        /// La clé du script associé
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// La clé de l'action
        /// </summary>
        public int SqlActionId { get; set; }

        /// <summary>
        /// La clé du type d'objet
        /// </summary>
        public int TypeObjectId { get; set; }

        /// <summary>
        /// Base de données concernée par ce résumé
        /// </summary>
        public string ResumeDatabase { get; set; }

        /// <summary>
        /// Schéma concerné par ce résumé
        /// </summary>
        public string ResumeSchema { get; set; }

        /// <summary>
        /// Nom de l'objet concerné par ce résumé
        /// </summary>
        public string ResumeName { get; set; }

        /// <summary>
        /// Nom de la colonne concernée par ce résumé
        /// </summary>
        public string ResumeColumn { get; set; }

        /// <summary>
        /// Indique si le résumé est applicable aux autres clients que ceux listés
        /// </summary>
        public bool ResumeForOtherClients { get; set; }

        /// <summary>
        /// Code de validation de ce résumé
        /// </summary>
        public byte ResumeManualValidationCode { get; set; }

        #endregion

        public EValidation GetResumeManualValidationCode() => (EValidation)ResumeManualValidationCode;
        public void SetResumeManualValidationCode(EValidation value) => ResumeManualValidationCode = (byte)value;

        public SqlAction GetAction() => SqlAction.List().FirstOrDefault(x => x.SqlActionId == SqlActionId);
        public void SetAction(SqlAction value) => SqlActionId = (value == null ? 0 : value.SqlActionId);

        public TypeObject GetWhat() => TypeObject.List().FirstOrDefault(x => x.TypeObjectId == TypeObjectId);
        public void SetWhat(TypeObject value) => TypeObjectId = (value == null ? 0 : value.TypeObjectId);

        public IEnumerable<ClientCode> Clients { get; set; } = new List<ClientCode>();

        /// <summary>
        /// Pour affichage
        /// </summary>
        /// <returns>Le texte à afficher</returns>
        public override string ToString()
        {
            string valid = GetResumeManualValidationCode().Libelle(); 

            if (SqlActionId == SqlAction.CodeClient)
            {
                string clients = string.Join(", ", this.Clients.Select(x => x.ToString()));
                return $"{valid} : S'applique pour les clients : {clients}";
            }
            else
            {
                return $"{valid} : {EnumHelper.ToString(GetAction(), GetWhat(), ResumeDatabase, ResumeSchema, ResumeName, ResumeColumn)}";
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Resume res)
            {
                return this.ScriptId == res.ScriptId && this.SqlActionId == res.SqlActionId && this.TypeObjectId == res.TypeObjectId
                    && this.ResumeDatabase == res.ResumeDatabase && this.ResumeSchema == res.ResumeSchema && this.ResumeName == res.ResumeName && this.ResumeColumn == res.ResumeColumn;
            }

            return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(ScriptId, SqlActionId, TypeObjectId, ResumeDatabase, ResumeSchema, ResumeName, ResumeColumn);

        public static string SQLSelect
            => @"
SELECT r.ResumeId, r.ScriptId, r.SqlActionId, r.TypeObjectId, r.ResumeDatabase, r.ResumeSchema, r.ResumeName, r.ResumeColumn, r.ResumeForOtherClients, r.ResumeManualValidationCode
FROM dbo.Resume r
";
        public static string SQLUpdateValidation
            => @"
UPDATE dbo.Resume
SET ResumeManualValidationCode = @ResumeManualValidationCode
WHERE ScriptId = @ScriptId
;
";

        public bool IsSame(Resume res)
            => this.SqlActionId == res.SqlActionId 
            && this.TypeObjectId == res.TypeObjectId
            && this.ScriptId == res.ScriptId 
            && ((string.IsNullOrWhiteSpace(this.ResumeDatabase) && string.IsNullOrWhiteSpace(res.ResumeDatabase)) || this.ResumeDatabase == res.ResumeDatabase)
            && ((string.IsNullOrWhiteSpace(this.ResumeSchema) && string.IsNullOrWhiteSpace(res.ResumeSchema)) || this.ResumeSchema == res.ResumeSchema)
            && ((string.IsNullOrWhiteSpace(this.ResumeName) && string.IsNullOrWhiteSpace(res.ResumeName)) || this.ResumeName == res.ResumeName) 
            && ((string.IsNullOrWhiteSpace(this.ResumeColumn) && string.IsNullOrWhiteSpace(res.ResumeColumn)) || this.ResumeColumn == res.ResumeColumn);

        public string GetFullName()
         => EnumHelper.ToString(ResumeDatabase, ResumeSchema, ResumeName) + (string.IsNullOrWhiteSpace(ResumeColumn) ? string.Empty : $".{ResumeColumn}");
    }
}
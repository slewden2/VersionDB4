using System;
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
        public string ResumeName { get; set; } = string.Empty;

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

        public IEnumerable<ClientCode> Clients { get; set; } = new List<ClientCode>();

        public ObjectIdentifier Identifier
        {
            get => new ObjectIdentifier(ResumeName) { DataBase = ResumeDatabase, Schema = ResumeSchema, Column = ResumeColumn };
            set
            {
                ResumeDatabase = value?.DataBase;
                ResumeSchema = value?.Schema;
                ResumeName = value?.Name;
                ResumeColumn = value?.Column;
            }
        }

        #endregion

        public EValidation GetResumeManualValidationCode() => (EValidation)ResumeManualValidationCode;

        public SqlAction GetAction() => SqlAction.List().FirstOrDefault(x => x.SqlActionId == SqlActionId);

        public TypeObject GetWhat() => TypeObject.List().FirstOrDefault(x => x.TypeObjectId == TypeObjectId);

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
                return $"{valid} : {EnumHelper.ToString(GetAction(), GetWhat(), Identifier)}";
            }
        }
        public string GetFullName()
             => Identifier.ToString();


        public override bool Equals(object obj)
        {
            if (obj is Resume res)
            {
                return this.ScriptId == res.ScriptId && this.SqlActionId == res.SqlActionId && this.TypeObjectId == res.TypeObjectId
                    && this.Identifier.Equals(res.Identifier);
            }

            return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(ScriptId, SqlActionId, TypeObjectId, Identifier);

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
    }
}
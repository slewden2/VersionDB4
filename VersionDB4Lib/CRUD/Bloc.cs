using System;
using System.Collections.Generic;
using System.Linq;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Un bloc de texte identifi�
    /// </summary>
    public class Bloc
    {
        #region Properties
        /// <summary>
        /// Cl� du bloc
        /// </summary>
        public long BlocId { get; set; }

        /// <summary>
        /// La cl� du script associ�
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// La cl� de l'action
        /// </summary>
        public int SqlActionId { get; set; }

        /// <summary>
        /// La cl� du type d'objet
        /// </summary>
        public int TypeObjectId { get; set; }

        /// <summary>
        /// La cl� du script associ�
        /// </summary>
        public int? ClientCodeId { get; set; }

        /// <summary>
        /// Position du bloc
        /// </summary>
        public int BlocIndex { get; set; }

        /// <summary>
        /// Taille du bloc
        /// </summary>
        public int BlocLength { get; set; }

        /// <summary>
        /// Base concern�e par ce bloc
        /// </summary>
        public string BlocDatabase { get; set; }

        /// <summary>
        /// Sch�ma concern� par ce bloc
        /// </summary>
        public string BlocSchema { get; set; }

        /// <summary>
        /// Nom de l'objet concern� par ce boc
        /// </summary>
        public string BlocName { get; set; }

        /// <summary>
        /// Ce bloc est exclu des r�sum�s
        /// </summary>
        public bool BlocExcludeFromResume { get; set; }

        /// <summary>
        /// Nom �ventuel de la colonne concern�e par ce bloc
        /// </summary>
        public string BlocColumn { get; set; }
        #endregion
        public SqlAction GetAction() => SqlAction.List().FirstOrDefault(x => x.SqlActionId == SqlActionId);
        public void SetAction(SqlAction value) => SqlActionId = (value == null ? 0 : value.SqlActionId);

        public TypeObject GetWhat() => TypeObject.List().FirstOrDefault(x => x.TypeObjectId == TypeObjectId);
        public void SetWhat(TypeObject value) => TypeObjectId = (value == null ? 0 : value.TypeObjectId);

        public ClientCode GetClientCode() => ClientCodeId == null ? null : ClientCode.List().FirstOrDefault(x => x.ClientCodeId == ClientCodeId);
        public void SetClientCode(ClientCode value) => ClientCodeId = value == null ? null : (int?)value.ClientCodeId;


        /// <summary>
        /// Pour affichage
        /// </summary>
        /// <returns>Le texte � afficher</returns>
        public override string ToString()
        {
            if (this.SqlActionId == SqlAction.CodeClient)
            {
                return $"Bloc CodeClientIs({GetClientCode()?.ClientCodeName ?? ClientCodeId.ToString()}) at ({BlocIndex}, {BlocLength})";
            }

            return EnumHelper.ToString(GetAction(), GetWhat(), BlocDatabase, BlocSchema, BlocName, BlocColumn) + $" at ({BlocIndex}, {BlocLength})";
        }

        public override int GetHashCode()
            => HashCode.Combine(SqlActionId, TypeObjectId, ClientCodeId, BlocDatabase, BlocSchema, BlocName, BlocColumn);

        public override bool Equals(object obj)
        {
            if (obj is Bloc bl)
            {
                return this.SqlActionId == bl.SqlActionId
                    && this.TypeObjectId == bl.TypeObjectId
                    && this.ClientCodeId == bl.ClientCodeId
                    && (this.BlocDatabase == bl.BlocDatabase || (string.IsNullOrWhiteSpace(this.BlocDatabase) && string.IsNullOrWhiteSpace(bl.BlocDatabase)))
                    && (this.BlocSchema == bl.BlocSchema || (string.IsNullOrWhiteSpace(this.BlocSchema) && string.IsNullOrWhiteSpace(bl.BlocSchema)))
                    && (this.BlocName == bl.BlocName || (string.IsNullOrWhiteSpace(this.BlocName) && string.IsNullOrWhiteSpace(bl.BlocName)))
                    && (this.BlocColumn == bl.BlocColumn || (string.IsNullOrWhiteSpace(this.BlocColumn) && string.IsNullOrWhiteSpace(bl.BlocColumn)));
            }

            return false;
        }

        public static string SQLSelect
            => @"
SELECT BlocId, ScriptId, SqlActionId, TypeObjectId, ClientCodeId, BlocIndex, BlocLength, BlocDataBase, BlocSchema, BlocName, BlocExcludeFromResume, BlocColumn
FROM dbo.Bloc
";

        public DataBaseObject GetDatabaseObject()
            => new DataBaseObject() 
            { 
                DatabaseObjectDatabase = BlocDatabase,
                DatabaseObjectSchema = BlocSchema,
                DatabaseObjectName = BlocName,
                DatabaseObjectColumn = BlocColumn,
                ScriptId = ScriptId,
                TypeObjectId = TypeObjectId
            };

        /// <summary>
        /// Obtient un clone d'objet r�sum� (utile pour le r�sum�)
        /// </summary>
        /// <param name="withName">Avec le nom ou pas (pour une factorisation maximale !!)</param>
        /// <returns>Renvoie l'objet clon� resultbase</returns>
        public Resume GetResume(bool withName = true)
        {
            if (withName)
            {
                var r = new Resume()
                {
                    ScriptId = this.ScriptId,
                    SqlActionId = this.SqlActionId,
                    TypeObjectId = this.TypeObjectId,
                    ResumeDatabase = this.BlocDatabase,
                    ResumeSchema = this.BlocSchema,
                    ResumeName = this.BlocName,
                    ResumeColumn = this.BlocColumn,
                };

                if (this.ClientCodeId.HasValue && this.ClientCodeId > 0)
                {
                    r.Clients = new List<ClientCode>()
                    {
                        new ClientCode() { ClientCodeId = this.ClientCodeId.Value }
                    };
                }

                return r;
            }
            else
            {
                return new Resume()
                {
                    ScriptId = this.ScriptId,
                    SqlActionId = this.SqlActionId,
                    TypeObjectId = this.TypeObjectId,
                };
            }
        }

        public string GetFullName()
         => EnumHelper.ToString(BlocDatabase, BlocSchema, BlocName) + (string.IsNullOrWhiteSpace(BlocColumn) ? string.Empty : $".{BlocColumn}");

        /// <summary>
        /// Indique si l'�l�ment fournit est enti�rement � l'int�rieur de celui-ci
        /// (utilis� pour d�tecter les instructions � l'int�rieur de commentaires ! ==> elles doivent �tre ignor�es)
        /// </summary>
        /// <param name="other">Le bloc � comparer</param>
        /// <returns>Vrai si le bloc fournit est enti�rement contenu</returns>
        public bool IsIn(Bloc other)
        {
            if (other == null)
            {
                return false;
            }

            return this.BlocIndex <= other.BlocIndex && this.BlocIndex + this.BlocLength >= other.BlocIndex + other.BlocLength;
        }
    }
}
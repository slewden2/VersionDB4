using System;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Un script du référentiel
    /// </summary>
    public class Script : IPresentable
    {
        /// <summary>
        /// Clé du script
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// Clé de la verison
        /// </summary>
        public int VersionId { get; set; }

        /// <summary>
        /// Ordre du script dans la varsion
        /// </summary>
        public int ScriptOrder { get; set; }

        /// <summary>
        /// Contenu du texte SQL du script
        /// </summary>
        public string ScriptText { get; set; }

        public static string SQLSelect
            => @"
SELECT ScriptId, VersionId, ScriptOrder, ScriptText
FROM dbo.Script
";

        public static string SQLInsert
            => @"
INSERT INTO dbo.Script (
VersionId, ScriptOrder, ScriptText
) VALUES (
@VersionId, @ScriptOrder, @ScriptText
);
SELECT TOP 1 SCOPE_IDENTITY() AS [Key];
";
        public static string SQLUpdate
            => @"
UPDATE dbo.Script 
SET VersionId   = @VersionId
  , ScriptOrder = @ScriptOrder
  , ScriptText  = @ScriptText
WHERE ScriptId  = @ScriptId
;
";

        public Version Version { get; set; }

        public override string ToString()
            => Version == null ? $"Script N° {ScriptOrder}" : $"Script V{Version.VersionPrincipal}.{Version.VersionSecondary}.{ScriptOrder}";

        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.Script;
    }
}
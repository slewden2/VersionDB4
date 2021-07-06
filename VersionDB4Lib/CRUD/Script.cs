using System.Drawing;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.UI;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Un script du référentiel
    /// </summary>
    public class Script : IPresentable, IWithStatusColor
    {
        private SqlAnalyzer myAnalyser = null;

        /// <summary>
        /// Clé du script
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// Clé de la version
        /// </summary>
        public int VersionId { get; set; }

        public Version Version { get; set; }

        /// <summary>
        /// Ordre du script dans la varsion
        /// </summary>
        public int ScriptOrder { get; set; }

        /// <summary>
        /// Contenu du texte SQL du script
        /// </summary>
        public string ScriptText { get; set; }

        public long FullVersion => Version == null ? ScriptOrder : (Version.FullVersion * 1000 ) + ScriptOrder;

        public override string ToString()
            => Version == null ? $"Script N° {ScriptOrder}" : $"Script V{Version.VersionPrincipal}.{Version.VersionSecondary}.{ScriptOrder}";

        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.Script;

        public Color GetColorStatus()
            => GetAnalyzer()?.Valide.GetColor() ?? Color.Black; 

        public SqlAnalyzer GetAnalyzer(bool force = false)
        {
            if (myAnalyser != null && !force)
            {
                return myAnalyser;
            }

            if (ScriptId > 0)
            {
                var cnn = new DatabaseConnection();
                myAnalyser = SqlAnalyzer.Load(cnn, this.ScriptId);
            }

            return myAnalyser;
        }

        public static string SQLSelect
            => @"
SELECT ScriptId, VersionId, ScriptOrder, ScriptText
FROM dbo.Script
";

        public static string SQLInsert
            => @"
INSERT INTO dbo.Script (ScriptOrder, ScriptText, VersionId)
SELECT COALESCE(MAX(s.ScriptOrder), 0) + 1, @ScriptText, @VersionId
FROM dbo.Script s
WHERE s.VersionId = @VersionId
;
SELECT COALESCE(SCOPE_IDENTITY(), @@Identity) AS [Key];
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

        public static string SQLValidAll
            => @"
UPDATE dbo.Resume 
SET ResumeManualValidationCode = 1  ---- 1 = Validé
WHERE ScriptId  = @ScriptId
;";

        public static string SQLRefuseAll
            => @"
UPDATE dbo.Resume 
SET ResumeManualValidationCode = 2  ---- 2 = refusé
WHERE ScriptId  = @ScriptId
;";
        public static string SQLDelete
            => @"
DELETE FROM dbo.Resume WHERE ScriptId = @ScriptId;
DELETE FROM dbo.DatabaseObject WHERE ScriptId = @ScriptId;
DELETE FROM dbo.Bloc WHERE ScriptId = @ScriptId;
UPDATE s SET ScriptOrder = s.ScriptOrder - 1 
FROM dbo.Script s INNER JOIN dbo.Script s2  ON s.VersionId = s2.VersionId AND s.ScriptOrder > s2.ScriptOrder
WHERE s2.ScriptId = @ScriptId;
DELETE FROM dbo.Script WHERE ScriptId = @ScriptId;
";

    }
}
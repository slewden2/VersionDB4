using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using Object = VersionDB4Lib.CRUD.Object;

namespace VersionDB4Lib.Business.Scripting
{
    public class CRUDObjectProcess : IDisposable
    {
        private readonly DatabaseConnection cnn;

        public CRUDObjectProcess() 
            => cnn = new DatabaseConnection();
        public void Dispose() => cnn.Dispose();

        public void BeginTransaction() => cnn.BeginTransaction();
        public void CommitTransaction() => cnn.CommitTransaction();
        public void RollBackTransaction() => cnn.RollBackTransaction();

        public int Add(Object objectInserted)
        {
            objectInserted.ObjectDeleted = false;
            objectInserted.ObjectEmpty = false;

            int id = cnn.ExecuteScalar(Object.SQLInsert, objectInserted);

            AddScriptToObjectAction(objectInserted, SqlAction.Create);

            return id;
        }

        public void Edit(Object objectEdited)
        {
            objectEdited.ObjectDeleted = false;
            objectEdited.ObjectEmpty = false;
            
            using var cnn = new DatabaseConnection();
            cnn.Execute(Object.SQLUpdate, objectEdited);

            AddScriptToObjectAction(objectEdited, SqlAction.Alter);
        }

        public void Delete(Object objectDel)
        {
            objectDel.ObjectDeleted = true;

            using var cnn = new DatabaseConnection();
            cnn.Execute(Object.SQLDelete, objectDel);

            AddScriptToObjectAction(objectDel, SqlAction.Delete);
        }

        private void AddScriptToObjectAction(Object currentObjectEdited, int action)
        {
            // Ajouter un script à la version 
            var script = new ScriptObject(currentObjectEdited, new SqlAction() { SqlActionId = action });
            var crudScript = new Script()
            {
                ScriptText = script.ToString(),
                VersionId = currentObjectEdited.VersionId,
            };
            var scriptId = cnn.ExecuteScalar(Script.SQLInsert, crudScript);

            // Ajouter l'anlyse pour ce script
            var analyzer = SqlAnalyzer.Analyse(scriptId, crudScript.ScriptText);
            analyzer.Save(cnn);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using VersionDB4Lib.ForUI;
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

            if (objectInserted.GetTypeObject().TypeObjectNeedColumnDefinition)
            {
                // TODO : Add synchronisation des colonnes Here
            }

            AddScriptToObjectAction(objectInserted, SqlAction.Create);

            return id;
        }

        public void Edit(Object objectEdited)
        {
            objectEdited.ObjectDeleted = false;
            objectEdited.ObjectEmpty = false;
            
            using var cnn = new DatabaseConnection();
            cnn.Execute(Object.SQLUpdate, objectEdited);

            if (objectEdited.GetTypeObject().TypeObjectNeedColumnDefinition)
            {
                // TODO : Add synchronisation des colonnes Here
            }

            AddScriptToObjectAction(objectEdited, SqlAction.Alter);
        }

        public void Delete(Object objectDel, int numberOfImplementation)
        {
            objectDel.ObjectDeleted = true;

            string sql = numberOfImplementation > 0 ? ObjectWithClientSpecific.SQLDelete : Object.SQLDelete;

            using var cnn = new DatabaseConnection();
            if (objectDel.GetTypeObject().TypeObjectNeedColumnDefinition)
            {
                cnn.Execute(ColumnDefinition.SQLDelete, objectDel);
            }

            cnn.Execute(sql, objectDel);

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
            analyzer.Valide = EValidation.Valide;
            analyzer.Save(cnn);
        }
    }
}

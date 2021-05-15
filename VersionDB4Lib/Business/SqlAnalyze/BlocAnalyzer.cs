using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.Business.SqlAnalyze
{
    public class BlocAnalyzer
    {
        private readonly List<Bloc> blocs = new List<Bloc>();

        public List<Bloc> Blocs => blocs;

        public void Analyze(RegexFounding e, int scriptId, string text)
        {
            blocs.Clear();

            MatchCollection cll = e.Expression.Matches(text);
            if (cll != null && cll.Count > 0)
            {
                foreach (Match m in cll)
                {
                    string db = m.Groups["database"]?.Value;
                    string sch = m.Groups["schema"]?.Value;
                    if (string.IsNullOrWhiteSpace(sch) && !string.IsNullOrWhiteSpace(db))
                    {
                        sch = db;
                        db = null;
                    }

                    string name = m.Groups["name"]?.Value;
                    string col = string.Empty;
                    if (SqlAction.IsForColumn(e.Action))
                    {
                        col = m.Groups["col"]?.Value;
                    }
                    else if (e.ApplyOn == SqlWhat.Index && SqlAction.IsForIndex(e.Action))
                    {
                        col = m.Groups["col"]?.Value;
                    }

                    int? clientCode = null;
                    if (int.TryParse(m.Groups["codeClient"].Value, out int cc))
                    {
                        clientCode = cc;
                    }

                    var res = new Bloc()
                    {
                        ScriptId = scriptId,
                        SqlActionId = e.Action,
                        SqlWhatId = e.ApplyOn,
                        BlocIndex = m.Index,
                        BlocLength = m.Length,
                        BlocDatabase = RegexFounding.Filtre(db),
                        BlocSchema = RegexFounding.Filtre(sch),
                        BlocName = RegexFounding.Filtre(name),
                        BlocColumn = RegexFounding.Filtre(col),
                        ClientCodeId = clientCode
                    };

                    if (SqlAction.IsForColumn(e.Action))
                    {
                        res.BlocColumn = m.Groups["col"]?.Value;
                    }

                    blocs.Add(res);
                }
            }

        }
    }
}

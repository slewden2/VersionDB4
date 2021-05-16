using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VersionDB4Lib.Business.SqlAnalyze;
using VersionDB4Lib.CRUD;
using Xunit;

namespace VersionDB4LibUnitTests.Business.SqlAnalyze
{
    public class BlocAnalyzerTest
    {

        [Fact]
        public void TestDBComparerProcedure()
        {
            var e = RegexFounding.List.First(x => x.Action == SqlAction.DbComparer && x.ApplyOn == TypeObject.Procedure);
            string text = @"
--- DBComparer Création de la procédure base.dbo.Procedure2test
";
            Bloc expected = new Bloc()
            { 
                ScriptId = 0,
                SqlActionId = SqlAction.DbComparer,
                TypeObjectId = TypeObject.Procedure,
            };

            var ba = new BlocAnalyzer();
            ba.Analyze(e, 0, text);
            Assert.NotNull(ba.Blocs);
            Assert.Single(ba.Blocs);
            Assert.Collection(ba.Blocs, b => Assert.Equal(expected, b));
        }
    }
}

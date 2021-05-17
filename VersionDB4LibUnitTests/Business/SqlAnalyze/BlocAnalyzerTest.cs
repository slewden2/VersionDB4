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

        public static IEnumerable<object[]> Jeu2TestFile()
        {
            var tests = RegexFoundingUnitTest.Load(@"C:\projets\Bdd\VersionDB4\VersionDB4LibUnitTests\Jeux2TestDBComparer.Json").ToList();
            foreach (var exp in tests)
            {
                yield return new object[] { exp };
            }
        }


        [Theory]
        [MemberData(nameof(Jeu2TestFile))]
        public void TestDBComparerProcedure(RegexFoundingUnitTest param)
        {
            // Arrange
            var e = RegexFounding.List.First(x => x.Action == param.SqlActionId && x.ApplyOn == param.SqlWhatId);
            Bloc expected = new Bloc()
            {
                ScriptId = 0,
                SqlActionId = param.SqlActionId,
                TypeObjectId = param.SqlWhatId,
            };

            // Act
            var ba = new BlocAnalyzer();
            ba.Analyze(e, 0, param.Text);

            // Assert
            Assert.NotNull(ba.Blocs);
            if (param.Result)
            {
                Assert.Single(ba.Blocs);
                Assert.Collection(ba.Blocs, b => Assert.Equal(expected, b));
            }
            else
            {
                Assert.Empty(ba.Blocs);
            }
        }

        [Fact]
        public void TestCodeClient()
        {
            var e = RegexFounding.List.First(x => x.Action == SqlAction.CodeClient && x.ApplyOn == TypeObject.None);
            string text = @"
SELECT  CodeClientIs(1)
";
            Bloc expected = new Bloc()
            {
                ScriptId = 0,
                SqlActionId = SqlAction.CodeClient,
                TypeObjectId = TypeObject.None,
                ClientCodeId = 1
            };

            // Act
            var ba = new BlocAnalyzer();
            ba.Analyze(e, 0, text);

            // Assert
            Assert.NotNull(ba.Blocs);
            Assert.Single(ba.Blocs);
            Assert.Collection(ba.Blocs, b => Assert.Equal(expected, b));
        }

        [Fact]
        public void TestCommentMonoLine()
        {
            var e = RegexFounding.List.First(x => x.Action == SqlAction.Comment && x.ApplyOn == TypeObject.None && x.Expression.ToString().StartsWith("(?m)"));
            string text = @"
DECLARE @x VARCHAR(100)
--- RAISERROR ('message info', 3, 16)
RAISERROR ('message 10', 10, 33) 

RAISE---RROR ('message 11', ---11, 33) 
RAISERROR ('message 12', 12, 33) 
RAISERROR ('message 13', 13, 33) 
RAISERROR ('message 14', 14, 33) 
RAISERROR ('message 15', 15, 33) 
RAISERROR ('message 16', 16, 33) 
RAISER---ROR ('message 17', 17, 33) with nowait; SET @x='EE';
RAISERROR ('message 18', 18, 33) 
RAISERROR ('message plusieurs mots', 19, 16)
---d";
            Bloc expected = new Bloc()
            {
                ScriptId = 0,
                SqlActionId = SqlAction.Comment,
                TypeObjectId = TypeObject.None
            };

            // Act
            var ba = new BlocAnalyzer();
            ba.Analyze(e, 0, text);

            // Assert
            Assert.NotNull(ba.Blocs);
            Assert.Equal(4, ba.Blocs.Count);
            Assert.All(ba.Blocs, b => Assert.Equal(expected, b)
            );

        }

        [Fact]
        public void TestCommentMultiLine()
        {
            var e = RegexFounding.List.First(x => x.Action == SqlAction.Comment && x.ApplyOn == TypeObject.None && !x.Expression.ToString().StartsWith("(?m)"));
            string text = @"
/****DECLARE @x VAR****CHAR(100)**
--- RAISERROR ('message info', 3, 16)****/
RAISERROR ('message 10', 10, 33) 

RAISE---RROR ('message 11', ---11, 33) 
/*RAISERROR ('message 12', 12, 33) 
RAISERROR ('m/*essage 13', 13, 33) 
RAISERROR ('message*/ 14', 14, 33) 
RAISERROR ('message 15', 15, 33) 
RAISERROR ('message 16', 16*/, 33)*/ 
RAISER---ROR ('message 17', 17, 33) with nowait; SET @x='EE';
RAISERROR ('message 18', 18, 33) 
RAISERROR ('message plusieurs mots', 19, 16)
---d";
            Bloc expected = new Bloc()
            {
                ScriptId = 0,
                SqlActionId = SqlAction.Comment,
                TypeObjectId = TypeObject.None
            };

            // Act
            var ba = new BlocAnalyzer();
            ba.Analyze(e, 0, text);

            // Assert
            Assert.NotNull(ba.Blocs);
            Assert.Equal(2, ba.Blocs.Count);
            Assert.All(ba.Blocs, b => Assert.Equal(expected, b)
            );
        }



        [Fact]
        public void TestRaiseError()
        {
            var e = RegexFounding.List.First(x => x.Action == SqlAction.RaiseError && x.ApplyOn == TypeObject.None);
            string text = @"
RAISERROR ('message plusieurs mots', 3, 16)

DECLARE @x VARCHAR(100)
RAISERROR ('message info', 3, 16)
RAISERROR ('message 10', 10, 33) 

RAISERROR ('message 11', 11, 33) 
RAISERROR ('message 12', 12, 33) 
RAISERROR ('message 13', 13, 33) 
RAISERROR ('message 14'' plusiers tests', 14, 33) 
RAISERROR ('message 15', 15, 33) 
RAISERROR ('message 16', 16, 33) 
RAISERROR ('message 17', 17, 33) with nowait; SET @x='EE';
RAISERROR ('message 18', 18, 33) 
RAISERROR ('message plusieurs mots', 19, 16)
";
            Bloc expected = new Bloc()
            {
                ScriptId = 0,
                SqlActionId = SqlAction.RaiseError,
                TypeObjectId = TypeObject.None
            };

            // Act
            var ba = new BlocAnalyzer();
            ba.Analyze(e, 0, text);

            // Assert
            Assert.NotNull(ba.Blocs);
            Assert.Equal(8, ba.Blocs.Count);
            Assert.All(ba.Blocs, b => Assert.Equal(expected, b)
            );
        }
    }
}


using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DataBaseAndLogLibrary_UT")]
namespace DatabaseAndLogLibrary.DataBase
{
    internal class DatabaseSettings
    {
        public int? ExecutRequestTimeOut { get; set; }
        public string Base { get; set; }
    }
}

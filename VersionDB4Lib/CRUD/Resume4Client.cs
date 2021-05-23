using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Mémorise le fait que le résumé est spécifique à un code client
    /// </summary>
    public class Resume4Client
    {
        /// <summary>
        /// Clé unique du résumé
        /// </summary>
        public int ResumeId { get; set; }

        public int ClientCodeId { get; set; }


        public static string SQLSelect => @"
SELECT ResumeId, ClientCodeId FROM dbo.Resume4Client
";

    }

}

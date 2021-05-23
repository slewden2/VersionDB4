using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    /// <summary>
    /// Un projet qui contient tous les objets
    /// </summary>
    public class Project : IPresentable
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public static string SQLSelect => @"
SELECT ProjectId, ProjectName FROM dbo.Project
";

        public override string ToString() => $"Projet {ProjectName}";
        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.Project;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.CRUD
{
    public class Version : IPresentable
    {
        public int VersionId { get; set; }
        public int ProjectId { get; set; }
        public int VersionPrincipal { get; set; }
        public int VersionSecondary { get; set; }

        public static Version Empty => new Version();

        public int VersionNumber() => (VersionPrincipal * 10000) + VersionSecondary;

        public static Version operator ++(Version v1)
        {
            v1.VersionSecondary++;
            if (v1.VersionSecondary >= 1000)
            {
                v1.VersionPrincipal++;
                v1.VersionSecondary = 0;
            }

            return v1;
        }

        public static Version operator --(Version v1)
        {
            v1.VersionSecondary--;
            if (v1.VersionSecondary < 0)
            {
                v1.VersionPrincipal--;
                v1.VersionSecondary = 999;
                if (v1.VersionPrincipal < 0)
                {
                    throw new InvalidOperationException("Empty version can't be descreased");
                }
            }

            return v1;
        }
        public static bool operator ==(Version v1, Version v2)
            => v1 is object ? v1.IsEquals(v2) : v2 is null;

        public static bool operator !=(Version v1, Version v2)
            => v1 is object ? !v1.IsEquals(v2) : v2 is object;

        public static bool operator >(Version v1, Version v2)
            => v1.VersionPrincipal == v2.VersionPrincipal ? v1.VersionSecondary > v2.VersionSecondary : v1.VersionPrincipal > v2.VersionPrincipal;

        public static bool operator <(Version v1, Version v2)
            => v1.VersionPrincipal == v2.VersionPrincipal ? v1.VersionSecondary < v2.VersionSecondary : v1.VersionPrincipal < v2.VersionPrincipal;

        public static bool operator <=(Version v1, Version v2) => v1 == v2 || v1 < v2;
        public static bool operator >=(Version v1, Version v2) => v1 == v2 || v1 > v2;

        public static string SQLSelect => @"
SELECT VersionId, ProjectId, VersionPrincipal, VersionSecondary
FROM dbo.Version 
";
        public static string SQLInsert => @"
INSERT INTO dbo.Version (
ProjectId, VersionPrincipal, VersionSecondary
) VALUES (
@ProjectId, @VersionPrincipal, @VersionSecondary
);
SELECT TOP 1 COALESCE(SCOPE_IDENTITY(), @@IDENTITY) AS [Key];
";
        public static string SQLDelete => @"
DELETE FROM dbo.Version WHERE VersionId = @VersionId;
";


        public string FullVersion => $"Version {VersionPrincipal}.{VersionSecondary}";

        public override string ToString() => FullVersion;
        public override bool Equals(object obj)
         => (obj is Version v) && IsEquals(v);

        public override int GetHashCode()
            => HashCode.Combine(VersionPrincipal, VersionSecondary);


        private bool IsEquals(Version v)
            => this.VersionPrincipal.Equals(v?.VersionPrincipal ?? -1) && this.VersionSecondary.Equals(v?.VersionSecondary ?? -1);

        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.VersionReferential;
    }
}

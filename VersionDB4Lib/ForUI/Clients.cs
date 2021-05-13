using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.ForUI
{
    public class Clients : IPresentable
    {
        public override string ToString() => "Bases client";
        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.Clients;
    }
}

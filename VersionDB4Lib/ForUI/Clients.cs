using System;
using System.Collections.Generic;
using System.Text;
using VersionDB4Lib.Business;

namespace VersionDB4Lib.ForUI
{
    public class Clients : IPresentable, ICounter
    {
        public Clients(int count) => Count = count;

        public int Count { get; private set; }

        public override string ToString() => "Bases de données du projet";

        public ETypeObjectPresentable GetCategory() => ETypeObjectPresentable.Clients;
    }
}

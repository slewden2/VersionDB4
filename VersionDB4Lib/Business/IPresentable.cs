using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib.Business
{
    public interface IPresentable
    {
        string ToString();
        ETypeObjectPresentable GetCategory();
    }
}

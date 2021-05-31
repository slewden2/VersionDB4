using System;
using System.Collections.Generic;
using System.Text;

namespace VersionDB4Lib
{
    public interface ICounter
    {
        int Count { get; }
    }

    public interface ILocked
    {
        bool VersionIsLocked { get; }
    }
}

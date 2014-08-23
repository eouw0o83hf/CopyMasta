using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyMasta.Core
{
    public class KeyState
    {
        public MetaKeys MetaKeys { get; set; }
        public List<char> Keys { get; set; }
    }

    [Flags]
    public enum MetaKeys
    {
        Ctrl = 1 << 0,
        Alt = 1 << 1,
        Shift = 1 << 2,
        CapsLock = 1 << 3,
        Tab = 1 << 4
    }
}

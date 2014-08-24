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
        
        public KeyState Clone()
        {
            return new KeyState
                {
                    MetaKeys = MetaKeys,
                    Keys = Keys.ToList()
                };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is KeyState))
            {
                return false;
            }

            var keys = (KeyState)obj;
            return keys.MetaKeys == MetaKeys && keys.Keys.OrderBy(a => a).SequenceEqual(Keys.OrderBy(a => a));
        }
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

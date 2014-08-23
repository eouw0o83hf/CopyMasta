using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyMasta.Core
{
    public abstract class KeystrokeListenerBase
    {
        public delegate void StateChanged(KeyState state);

        public event StateChanged OnStateChange;
    }

    public class KeystrokeLitener : KeystrokeListenerBase
    {
    }
}

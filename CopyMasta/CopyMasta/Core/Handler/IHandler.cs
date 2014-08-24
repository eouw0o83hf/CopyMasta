using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyMasta.Core.Handler
{
    public interface IHandler
    {
        void Handle(KeyState state);
    }
}

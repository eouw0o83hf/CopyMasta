using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CopyMasta.Core.Handler;

namespace CopyMasta.Core
{
    public interface IKeystrokeManager
    {
    }

    public class KeystrokeManager : IKeystrokeManager
    {
        private readonly ICollection<IHandler> _handlers;
        private readonly KeystrokeListenerBase _listener;

        public KeystrokeManager(ICollection<IHandler> handlers, KeystrokeListenerBase listener)
        {
            _handlers = handlers;
            _listener = listener;

            _listener.OnStateChange += StateChanged;
        }

        private void StateChanged(KeyState state)
        {
            foreach (var h in _handlers)
            {
                h.Handle(state);
            }
        }
    }
}

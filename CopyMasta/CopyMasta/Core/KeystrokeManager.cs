using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CopyMasta.Core.Handler;

namespace CopyMasta.Core
{
    public class KeystrokeManager
    {
        private readonly ICollection<IHandler> _handlers;
        private readonly KeystrokeListenerBase _listener;

        public KeystrokeManager(ICollection<IHandler> handlers, KeystrokeListenerBase listener)
        {
            _handlers = handlers;
            _listener = listener;
            _listener.RegisterListener(StateChanged);
        }

        private bool StateChanged(KeyState state, bool isActiveTransition)
        {
            var continueInternal = true;
            var continueExternal = true;
            foreach (var h in _handlers.OrderBy(a => a.AbsoluteExecutionOrder))
            {
                var status = h.Handle(state, isActiveTransition);

                switch (status)
                {
                    case EventContinuation.Abort:
                        continueInternal = false;
                        continueExternal = false;
                        break;

                    case EventContinuation.ExternalOnly:
                        continueInternal = false;
                        break;

                    case EventContinuation.InternalOnly:
                        continueExternal = false;
                        break;

                    case EventContinuation.Continue:
                    default:
                        break;
                }

                if (!continueInternal)
                {
                    break;
                }
            }

            return continueExternal;
        }
    }
}

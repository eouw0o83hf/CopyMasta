using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CopyMasta.Core.Handler
{
    public class NewGuidHandler : IHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.NewGuidHandler; } }

        public EventContinuation Handle(KeyState state, bool isActiveTransition)
        {
            if (!isActiveTransition)
            {
                return EventContinuation.Continue;
            }

            if (state.MetaKeys.HasFlag(MetaKeys.Ctrl)
                && state.MetaKeys.HasFlag(MetaKeys.Alt)
                && state.MetaKeys.HasFlag(MetaKeys.Shift)
                && state.Keys.Contains('G'))
            {
                Clipboard.SetText(Guid.NewGuid().ToString());
                return EventContinuation.ExternalOnly;
            }

            return EventContinuation.Continue;
        }
    }
}

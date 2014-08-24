using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyMasta.Core.Handler
{
    public class ChromeSafetyHelmet : IHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.ChromeSafetyHelmet; } }

        public EventContinuation Handle(KeyState state)
        {
            if (state.MetaKeys.HasFlag(MetaKeys.Ctrl)
                && state.MetaKeys.HasFlag(MetaKeys.Shift)
                && !state.MetaKeys.HasFlag(MetaKeys.Alt)
                && state.Keys.Intersect(new [] { 'Q', 'W' }).Any())
            {
                return EventContinuation.InternalOnly;
            }

            return EventContinuation.Continue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CopyMasta.Core.Handler
{
    public class FakeSsnHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.FakeSsnHandler; } }
        private static readonly Random Random = new Random();

        public EventContinuation Handle(KeyState state, bool isActiveTransition)
        {
            if (!isActiveTransition)
            {
                return EventContinuation.Continue;
            }

            if (state.MetaKeys.HasFlag(MetaKeys.Ctrl)
                && state.MetaKeys.HasFlag(MetaKeys.Alt)
                && state.MetaKeys.HasFlag(MetaKeys.Shift)
                && state.Keys.Contains('S'))
            {
                var ssn = Random.Next(666000000, 666999999);
                Clipboard.SetText(ssn.ToString());
                return EventContinuation.ExternalOnly;
            }

            return EventContinuation.Continue;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CopyMasta.Core.Handler
{
    public class PrettyHandler : IHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.PrettyHandler; } }

        public EventContinuation Handle(KeyState state, bool isActiveTransition)
        {
            if (!isActiveTransition)
            {
                return EventContinuation.Continue;
            }

            if (!state.MetaKeys.HasFlag(MetaKeys.Alt)
                || !state.MetaKeys.HasFlag(MetaKeys.Ctrl)
                || !state.MetaKeys.HasFlag(MetaKeys.Shift)
                || !state.Keys.Contains('P')
                || !Clipboard.ContainsText())
            {
                return EventContinuation.Continue;
            }

            Clipboard.SetText(Regex.Unescape(Clipboard.GetText()));

            return EventContinuation.Continue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CopyMasta.Core.Handler
{
    public class BucketHandler : IHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.BucketHandler; } }

        private readonly IDictionary<char, string> _buckets = new Dictionary<char, string>();
        private readonly ICollection<char> _targets = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private readonly object _locker = new object();

        public EventContinuation Handle(KeyState state)
        {
            if (!Clipboard.ContainsText() 
                || !state.MetaKeys.HasFlag(MetaKeys.Ctrl) 
                || !state.MetaKeys.HasFlag(MetaKeys.Alt)
                || state.Keys.Count != 1
                || !_targets.Contains(state.Keys[0]))
            {
                return EventContinuation.Continue;
            }

            lock (_locker)
            {
                if (!state.MetaKeys.HasFlag(MetaKeys.Shift))
                {
                    _buckets[state.Keys[0]] = Clipboard.GetText();
                    return EventContinuation.Abort;
                }
                else if (_buckets.ContainsKey(state.Keys[0]))
                {
                    Clipboard.SetText(_buckets[state.Keys[0]]);
                    return EventContinuation.Abort;
                }
            }

            return EventContinuation.Continue;
        }
    }
}

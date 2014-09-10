using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyMasta.Core.Handler
{
    public class ConsoleDebuggerHandler : IHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.ConsoleDebugger; } }

        private static readonly IDictionary<MetaKeys, string> _metaKeys =
            Enum.GetValues(typeof(MetaKeys)).Cast<MetaKeys>().ToDictionary(a => a, a => a.ToString().ToUpperInvariant());

        public EventContinuation Handle(KeyState state)
        {
            var builder = new StringBuilder();

            var metaState = from m in _metaKeys
                            select state.MetaKeys.HasFlag(m.Key) ? m.Value : new string(' ', m.Value.Length);
            var keys = state.Keys.Select(a => a.ToString().ToUpperInvariant());

            var joined = string.Join("\t", metaState.Concat(keys));
            Console.WriteLine("KEYSTATE: {0}", joined);

            return EventContinuation.Continue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            
            var text = Clipboard.GetText();
            text = Jsonify(text);
            text = Regexify(text);

            Clipboard.SetText(text);

            return EventContinuation.Continue;
        }

        private static string Jsonify(string text)
        {
            // Json.Net annoyingly doesn't have a TryParse analog, so we're left with
            // no choice but to try/catch
            try
            {
                var json = JToken.Parse(text);
                return JsonConvert.SerializeObject(json, Formatting.Indented);
            }
            catch
            {
                return text;
            }

        }

        private static string Regexify(string text)
        {
            try
            {
                return Regex.Unescape(text);
            }
            catch
            {
                return text;
            }
        }
    }
}

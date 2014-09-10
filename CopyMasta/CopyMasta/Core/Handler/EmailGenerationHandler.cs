using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CopyMasta.Core.Handler
{
    public class EmailGenerationHandler : IHandler
    {
        public int AbsoluteExecutionOrder { get { return ExecutionOrders.EmailGenerator; } }

        private int _position;
        private readonly List<Guid> _guids = new List<Guid>();

        public EventContinuation Handle(KeyState state)
        {
            if (!state.MetaKeys.HasFlag(MetaKeys.Alt)
                || !state.MetaKeys.HasFlag(MetaKeys.Ctrl))
            {
                return EventContinuation.Continue;
            }

            bool moveToClipboard;

            // E: New entry
            if (state.Keys.Contains('E') && state.MetaKeys.HasFlag(MetaKeys.Shift))
            {
                _guids.Add(Guid.NewGuid());
                _position = _guids.Count - 1;
                moveToClipboard = true;
            }
            // W: Load currently-pointed entry
            else if (state.Keys.Contains('W') && _guids.Any())
            {
                // If !shift, move the pointer to the last position before loading
                if (!state.MetaKeys.HasFlag(MetaKeys.Shift))
                {
                    _position = _guids.Count - 1;
                }
                moveToClipboard = state.MetaKeys.HasFlag(MetaKeys.Shift);
            }
            // Q: Previous position
            else if (state.Keys.Contains('Q') && _position > 0)
            {
                _position--;
                moveToClipboard = state.MetaKeys.HasFlag(MetaKeys.Shift);
            }
            // R: Next position
            else if (state.Keys.Contains('R') && _position < (_guids.Count - 1))
            {
                _position++;
                moveToClipboard = state.MetaKeys.HasFlag(MetaKeys.Shift);
            }
            // Otherwise, nothing to do here /jetpack
            else
            {
                return EventContinuation.Continue;
            }

            if (moveToClipboard)
            {
                Clipboard.SetText(string.Format("testemail+{0}@testdomain.local", _guids[_position]));
            }

            return EventContinuation.Continue;
        }
    }
}

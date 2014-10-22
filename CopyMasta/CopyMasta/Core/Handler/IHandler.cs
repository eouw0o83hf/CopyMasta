using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyMasta.Core.Handler
{
    public interface IHandler
    {
        int AbsoluteExecutionOrder { get; }

        /// <summary>
        /// Handle a state change.
        /// </summary>
        /// <param name="state">New state</param>
        /// <returns>How to allow the event to return</returns>
        EventContinuation Handle(KeyState state, bool isActiveTransition);
    }

    public enum EventContinuation
    {
        Continue,
        InternalOnly,
        ExternalOnly,
        Abort
    }

    public static class ExecutionOrders
    {
        public const int ConsoleDebugger = 0;

        public const int ChromeSafetyHelmet = 100;

        public const int BucketHandler = 1000;

        public const int EmailGenerator = 1100;

        public const int PrettyHandler = 1200;
    }
}

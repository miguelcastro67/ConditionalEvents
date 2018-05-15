using System;
using System.Collections.Generic;
using System.Linq;

namespace ConditionalEvents.Lib
{
    public static class EventManager
    {
        static ConditionalEvent _ConditionalEvent = null;

        public static ConditionalEvent RegisterEvent()
        {
            _ConditionalEvent = new ConditionalEvent<EventArgs>();
            return _ConditionalEvent;
        }

        public static ConditionalEvent<T> RegisterEvent<T>() where T : EventArgs
        {
            _ConditionalEvent = new ConditionalEvent<T>();
            return (ConditionalEvent<T>)_ConditionalEvent;
        }

        public static void AttachHandler<T>(ConditionalEvent<T> conditionalEvent, EventHandler<T> handler) where T : EventArgs
        {
            conditionalEvent.AttachHandler(handler);
        }

        public static void DetachHandler<T>(ConditionalEvent<T> conditionalEvent, EventHandler<T> handler) where T : EventArgs
        {
            conditionalEvent.DetachHandler(handler);
        }

        public static void RaiseEvent<T>(ConditionalEvent<T> conditionalEvent, object sender, T e) where T : EventArgs
        {
            if (conditionalEvent.Condition != null && conditionalEvent.Condition.Invoke())
            {
                foreach (EventHandler<T> handler in conditionalEvent.Handlers)
                    handler.Invoke(sender, e);
            }
        }

        public static void ClearHandlers<T>(ConditionalEvent<T> conditionalEvent) where T : EventArgs
        {
            conditionalEvent.ClearHandlers();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConditionalEvents.Lib
{
    public class ConditionalEvent
    {
        
    }

    public class ConditionalEvent<T> : ConditionalEvent where T : EventArgs
    {
        internal ConditionalEvent()
            : this(() => { return true; })
        {
        }

        internal ConditionalEvent(Func<bool> condition)
        {
            _Condition = condition;
        }

        Func<bool> _Condition = null;
        bool _AllowDuplicateHandlers = false;
        List<EventHandler<T>> _Handlers = new List<EventHandler<T>>();


        internal Func<bool> Condition
        {
            get { return _Condition; }
        }

        internal List<EventHandler<T>> Handlers
        {
            get { return _Handlers; }
        }

        public ConditionalEvent<T> WithCondition(Func<bool> condition)
        {
            _Condition = condition;
            return this;
        }

        public ConditionalEvent<T> AllowDuplicateHandlers
        {
            get
            {
                _AllowDuplicateHandlers = true;
                return this;
            }
        }

        internal void AttachHandler(EventHandler<T> handler)
        {
            if (!_Handlers.Contains(handler) || _AllowDuplicateHandlers)
                _Handlers.Add(handler);
        }

        internal void DetachHandler(EventHandler<T> handler)
        {
            while (_Handlers.Contains(handler))
                _Handlers.Remove(handler);
        }

        internal void RaiseEvent(object sender, T e)
        {
            if (_Condition.Invoke())
            {
                foreach (EventHandler<T> handler in _Handlers)
                    handler.Invoke(sender, e);
            }
        }

        internal void ClearHandlers()
        {
            foreach (EventHandler<T> handler in _Handlers)
                DetachHandler(handler);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Build1.PostMVC.Extensions.MVCS.Events.Impl
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IDictionary<EventBase, object> _listeners;
        private readonly IDictionary<EventBase, object> _listenersOnce;

        public EventDispatcher()
        {
            _listeners = new Dictionary<EventBase, object>();
            _listenersOnce = new Dictionary<EventBase, object>();
        }

        /*
         * Add.
         */

        public void AddListener(Event @event, Action listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listeners[@event] = listeners + listener;
        }

        public void AddListener<T1>(Event<T1> @event, Action<T1> listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action<T1>;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listeners[@event] = listeners + listener;
        }

        public void AddListener<T1, T2>(Event<T1, T2> @event, Action<T1, T2> listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action<T1, T2>;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listeners[@event] = listeners + listener;
        }

        public void AddListener<T1, T2, T3>(Event<T1, T2, T3> @event, Action<T1, T2, T3> listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action<T1, T2, T3>;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listeners[@event] = listeners + listener;
        }

        /*
         * Add Once.
         */

        public void AddListenerOnce(Event @event, Action listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listenersOnce[@event] = listeners + listener;
        }

        public void AddListenerOnce<T1>(Event<T1> @event, Action<T1> listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action<T1>;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listenersOnce[@event] = listeners + listener;
        }

        public void AddListenerOnce<T1, T2>(Event<T1, T2> @event, Action<T1, T2> listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action<T1, T2>;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listenersOnce[@event] = listeners + listener;
        }

        public void AddListenerOnce<T1, T2, T3>(Event<T1, T2, T3> @event, Action<T1, T2, T3> listener)
        {
            _listeners.TryGetValue(@event, out var listenersObj);
            var listeners = listenersObj as Action<T1, T2, T3>;
            if (listeners == null || !listeners.GetInvocationList().Contains(listener))
                _listenersOnce[@event] = listeners + listener;
        }

        /*
         * Remove.
         */

        public void RemoveListener(Event @event, Action listener)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                _listeners[@event] = (Action)listeners - listener;
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
                _listenersOnce[@event] = (Action)listenersOnce - listener;
        }

        public void RemoveListener<T1>(Event<T1> @event, Action<T1> listener)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                _listeners[@event] = (Action<T1>)listeners - listener;
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
                _listenersOnce[@event] = (Action<T1>)listenersOnce - listener;
        }

        public void RemoveListener<T1, T2>(Event<T1, T2> @event, Action<T1, T2> listener)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                _listeners[@event] = (Action<T1, T2>)listeners - listener;
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
                _listenersOnce[@event] = (Action<T1, T2>)listenersOnce - listener;
        }

        public void RemoveListener<T1, T2, T3>(Event<T1, T2, T3> @event, Action<T1, T2, T3> listener)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                _listeners[@event] = (Action<T1, T2, T3>)listeners - listener;
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
                _listenersOnce[@event] = (Action<T1, T2, T3>)listenersOnce - listener;
        }

        /*
         * Remove All.
         */

        public void RemoveAllListeners()
        {
            _listeners.Clear();
            _listenersOnce.Clear();
        }

        public void RemoveAllListeners(EventBase @event)
        {
            _listeners.Remove(@event);
            _listenersOnce.Remove(@event);
        }

        /*
         * Dispatch.
         */

        public void Dispatch(Event @event)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                ((Action)listeners)?.Invoke();

            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
            {
                _listenersOnce.Remove(@event); // While this is called before invocation, once listeners added during invocation will not be removed.
                ((Action)listenersOnce)?.Invoke();
            }
        }

        public void Dispatch<T1>(Event<T1> @event, T1 param01)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                ((Action<T1>)listeners)?.Invoke(param01);
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
            {
                _listenersOnce.Remove(@event); // While this is called before invocation, once listeners added during invocation will not be removed.
                ((Action<T1>)listenersOnce)?.Invoke(param01);
            }
        }

        public void Dispatch<T1, T2>(Event<T1, T2> @event, T1 param01, T2 param02)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                ((Action<T1, T2>)listeners)?.Invoke(param01, param02);
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
            {
                _listenersOnce.Remove(@event); // While this is called before invocation, once listeners added during invocation will not be removed.
                ((Action<T1, T2>)listenersOnce)?.Invoke(param01, param02);
            }
        }

        public void Dispatch<T1, T2, T3>(Event<T1, T2, T3> @event, T1 param01, T2 param02, T3 param03)
        {
            if (_listeners.TryGetValue(@event, out var listeners))
                ((Action<T1, T2, T3>)listeners)?.Invoke(param01, param02, param03);
            
            if (_listenersOnce.TryGetValue(@event, out var listenersOnce))
            {
                _listenersOnce.Remove(@event); // While this is called before invocation, once listeners added during invocation will not be removed.
                ((Action<T1, T2, T3>)listenersOnce)?.Invoke(param01, param02, param03);
            }
        }
    }
}
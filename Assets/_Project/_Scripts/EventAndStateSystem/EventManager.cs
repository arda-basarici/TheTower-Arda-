using System;
using System.Collections.Generic;

namespace Game
{
    public class EventManager : ObserverManager<Action<object>>
    {
        public void NotifyEventObservers(object eventData = null)
        {
            NotifyObservers(observer => observer.Invoke(eventData));
        }
    }
}

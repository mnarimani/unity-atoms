using System;
using System.Collections.Generic;

namespace UnityAtoms
{
    public class ObservableEvent<T> : IObservable<T>
    {
        private Action<Action<T>> unregister;
        private List<IObserver<T>> observers = new List<IObserver<T>>();

        public ObservableEvent(Action<Action<T>> register, Action<Action<T>> unregister)
        {
            register(NotifyObservers);
            unregister = unregister;
        }

        ~ObservableEvent()
        {
            unregister?.Invoke(NotifyObservers);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new ObservableUnsubscriber<T>(observers, observer);
        }

        private void NotifyObservers(T value)
        {
            for (int i = 0; observers != null && i < observers.Count; ++i)
            {
                observers[i].OnNext(value);
            }
        }
    }
}

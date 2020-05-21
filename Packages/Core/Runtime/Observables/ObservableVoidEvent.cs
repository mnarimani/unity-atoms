using System;
using System.Collections.Generic;

namespace UnityAtoms
{
    public class ObservableVoidEvent : IObservable<Void>
    {
        private Action<Action> unregister = default(Action<Action>);
        private List<IObserver<Void>> observers = new List<IObserver<Void>>();

        public ObservableVoidEvent(Action<Action> register, Action<Action> unregister)
        {
            register(NotifyObservers);
        }

        ~ObservableVoidEvent()
        {
            unregister?.Invoke(NotifyObservers);
        }

        public IDisposable Subscribe(IObserver<Void> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new ObservableUnsubscriber<Void>(observers, observer);
        }

        private void NotifyObservers()
        {
            for (int i = 0; observers != null && i < observers.Count; ++i)
            {
                observers[i].OnNext(new Void());
            }
        }
    }
}

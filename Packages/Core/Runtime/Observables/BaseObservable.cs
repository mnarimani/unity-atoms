using System;
using System.Collections.Generic;

namespace UnityAtoms
{
    public abstract class BaseObservable<T> : IObservable<T>
    {
        protected List<IObserver<T>> observers = new List<IObserver<T>>();

        protected abstract bool IsCompleted { get; }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new ObservableUnsubscriber<T>(observers, observer);
        }

        protected void OnCompleted()
        {
            if (IsCompleted)
            {
                for (int i = 0; observers != null && i < observers.Count; ++i)
                {
                    observers[i].OnCompleted();
                }
            }
        }

        protected void OnError(Exception e)
        {
            for (int i = 0; observers != null && i < observers.Count; ++i)
            {
                observers[i].OnError(e);
            }
        }
    }
}

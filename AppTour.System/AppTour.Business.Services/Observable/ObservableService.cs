using System;
using System.Collections.Generic;

namespace AppTour.Business.Services.Observable
{
    public class ObservableService<T> : IObservable<T>
    {
        private List<IObserver<T>> observers;

        public ObservableService()
        {
            observers = new List<IObserver<T>>();
        }

        protected void Notify(T obj)
        {
            foreach (IObserver<T> observer in observers)
            {
                observer.OnNext(obj);
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<T>> observers;
            private IObserver<T> observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observer != null && observers.Contains(observer))
                {
                    observers.Remove(observer);
                }
            }
        }
    }
}

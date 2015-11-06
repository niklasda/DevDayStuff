using System;
using System.Reactive.Concurrency;

namespace RxDemoCode.Observers
{
    public class DemoScheduler : IScheduler
    {
        public IDisposable Schedule<TState>(TState state, Func<DemoScheduler,TState,ObserverOfString> action)
        {
            throw new System.NotImplementedException();
        }

        public IDisposable Schedule<TState>(TState state, TimeSpan dueTime, Func action)
        {
            throw new System.NotImplementedException();
        }

        public IDisposable Schedule<TState>(TState state, DateTimeOffset dueTime, Func action)
        {
            throw new System.NotImplementedException();
        }

        public DateTimeOffset Now { get; }
    }
}
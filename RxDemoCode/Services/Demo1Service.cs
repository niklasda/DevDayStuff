using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using RxDemoCode.Interfaces;
using RxDemoCode.Observers;

namespace RxDemoCode.Services
{
    public class Demo1Service : IDemo1Service
    {
        public void Demo0(Action<string> callback)
        {
            IEnumerable<long> oneNumberPerSecond = new long[] {0,1,2,3,4,5,6,7,8,9};

            IEnumerable<string> lowNums = from n in oneNumberPerSecond where n < 10 select n.ToString();
            IObserver<string> observer = new ObserverOfString(callback);

            lowNums.Subscribe(observer);
        }

        public void Demo1(Action<string> callback)
        {
            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<string> lowNums = from n in oneNumberPerSecond where n < 10 select n.ToString();
            
            lowNums.Subscribe(callback, () => callback("Completed"));
        }

        public void Demo2(Action<string> callback)
        {
            Observable.Return("Welcome to Rx.NET\n").Subscribe(callback);

            char[] chars = "Welcome to Rx.NET\n".ToCharArray();

            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            var sub1 = lowNums.Subscribe(callback, () => callback("Completed"));
        }

        public void Demo2_2(Action<string> callback)
        {
            Observable.Return("Welcome to Rx.NET\n").Subscribe(callback);

            char[] chars = "Welcome to Rx.NET\n".ToCharArray();

            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            var sub2 = lowNums.SubscribeOn(Scheduler.Immediate);
            sub2.Subscribe(callback, () => callback("Completed"));
        }

        public void Demo2_3(Action<string> callback)
        {
            Observable.Return("Welcome to Rx.NET\n").Subscribe(callback);

            char[] chars = "Welcome to Rx.NET\n".ToCharArray();

            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            IObserver<string> observer = new ObserverOfString(callback);
            IDisposable sub3 = lowNums.SubscribeSafe(observer);
        }

        public void Demo3(Action<string> callback)
        {
            char[] chars = "Another text\n".ToCharArray();
            var rand = new Random();

            var stopTime = DateTime.Now.AddSeconds(5);

            IObservable<string> oneNumberPerSecond = Observable.Generate(
                5.0,
                i => DateTime.Now < stopTime,
                i => i + rand.NextDouble() - 0.5,
                i => i.ToString(),
                i => TimeSpan.FromSeconds(0.1)
                );

            oneNumberPerSecond.Subscribe(callback, () => callback("Completed"));
        }

        private ObserverOfMouse _obsemo;
        private IDisposable _mouseSub;
        private IObservable<EventPattern<MouseEventArgs>> _movingEvents;

        public void Demo4Setup(UIElement wnd, Action<string> callback)
        {
            _movingEvents = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(ev => wnd.MouseMove += ev, ev => wnd.MouseMove -= ev);
            _obsemo = new ObserverOfMouse(callback);
          //  _mouseSub = _movingEvents.Subscribe(_obsemo);

            //var deltas = from pair in movingEvents.Buffer(2)
            //             let array = pair.ToArray()
            //             let a = array[0].EventArgs.GetPosition((UIElement)array[0].Sender)
            //             let b = array[1].EventArgs.GetPosition((UIElement)array[1].Sender)
            //             select new Size(b.X - a.X, b.Y - a.Y);

            //deltas.Subscribe(new ObserverOfMouseMoves());
        }

        public void Demo4Toggle()
        {
            var ms = _mouseSub as CompositeDisposable;
            if (ms != null && !ms.IsDisposed)
            {
                _mouseSub.Dispose();
            }
            else
            {
                _mouseSub = _movingEvents.Subscribe(_obsemo);
            }
        }

        // http://mtaulty.com/CommunityServer/blogs/mike_taultys_blog/archive/2011/08/09/rx-and-schedulers.aspx
        // http://blogs.microsoft.co.il/bnaya/2010/03/13/rx-for-beginners-part-9-hot-vs-cold-observable/

        public void Demo5(Action<string> callback)
        {
            var observable = Observable.Return(101).Repeat(5).Take(5);

            var foea = observable.ForEachAsync(value => callback(string.Format("Value produced is {0}", value)));
            foea.Wait();

            callback("Done");
        }

        public void Demo6(Action<string> callback)
        {
            var observable = Observable.Return(101).Repeat(5).Take(5);

            observable.Subscribe(i => callback(i.ToString()), () => callback("Completed"));
        }
    }
}
using System;
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
        public void Demo1(Action<string> callback)
        {
            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<string> lowNums = from n in oneNumberPerSecond where n < 10 select n.ToString();

            lowNums.Subscribe(callback);
        }

        public void Demo2(Action<string> callback)
        {
            //   IObservable<char> chars2 = "Welcome to Rx.NET\n".ToObservable();
            //  var obs1 = chars2.TimeInterval();
            //    chars2.Subscribe(callback);
            Observable.Return("Welcome to Rx.NET\n").Subscribe(callback);

            char[] chars = "Welcome to Rx.NET\n".ToCharArray();

            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            var sub1 = lowNums.Subscribe(callback);

            var obs = Observable.Return(101, Scheduler.CurrentThread);

            var sub2 = lowNums.SubscribeOn(Scheduler.Immediate);


            IObserver<string> observer = new ObserverOfString(callback);
            IDisposable sub3 = lowNums.SubscribeSafe(observer);
            observer.OnCompleted(); // not a good place for this though 
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

            //IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            oneNumberPerSecond.Subscribe(callback);
        }

        private ObserverOfMouse _obsemo;
        private IDisposable _mouseSub;
        private IObservable<EventPattern<MouseEventArgs>> _movingEvents;

        public void Demo4Setup(UIElement wnd, Action<string> callback)
        {
            // IObserver<EventPattern<MouseEventArgs>> callback
            // observable from mouseMove event, plot speed

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

        public void Demo5(Action<string> callback)
        {
            var observable = Observable.Return(101).Repeat(5).Take(5);

            //observable.ForEach(value => Console.WriteLine("Value produced is {0}", value));

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
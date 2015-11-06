using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RxDemoCode.Interfaces;
using RxDemoCode.Observers;

namespace RxDemoCode.Services
{
    public class Demo1Service : IDemo1Service
    {
        public void Demo1(Action<long> callback)
        {
            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<long> lowNums = from n in oneNumberPerSecond where n < 10 select n;

            lowNums.Subscribe(callback);
        }

        public void Demo2(Action<string> callback)
        {
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

        public void Demo3(Action<double> callback)
        {
            char[] chars = "Another text\n".ToCharArray();
            var rand = new Random();

            IObservable<double> oneNumberPerSecond = Observable.Generate(
                5.0,
                i => i > 0,
                i => i + rand.NextDouble() - 0.5,
                i => i,
                i => TimeSpan.FromSeconds(0.1)
                );

            //IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            oneNumberPerSecond.Subscribe(callback);
        }

        public void Demo4(UIElement wnd, Action<string> callback)
        {
            // IObserver<EventPattern<MouseEventArgs>> callback
            // observable from mouseMove event, plot speed

            var movingEvents = Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(ev => wnd.MouseMove += ev, ev => wnd.MouseMove -= ev);

            movingEvents.Subscribe(new ObserverOfMouse(callback));

            //var deltas = from pair in movingEvents.Buffer(2)
            //             let array = pair.ToArray()
            //             let a = array[0].EventArgs.GetPosition((UIElement)array[0].Sender)
            //             let b = array[1].EventArgs.GetPosition((UIElement)array[1].Sender)
            //             select new Size(b.X - a.X, b.Y - a.Y);

            //deltas.Subscribe(new ObserverOfMouseMoves());
        }

        // http://mtaulty.com/CommunityServer/blogs/mike_taultys_blog/archive/2011/08/09/rx-and-schedulers.aspx

        public async void somet()
        {
            var observable = Observable.Return(101).Repeat().Take(5);

            observable.ForEach(value => Console.WriteLine("Value produced is {0}", value));

            //await observable.ForEachAsync(value => Console.WriteLine("Value produced is {0}", value));

            Console.WriteLine("Done");
        }

        public void someth()
        {
            var observable = Observable.Return(101).Repeat().Take(5);

            observable.Subscribe(i => Console.WriteLine(i), () => Console.WriteLine("Completed"));
        }
    }
}
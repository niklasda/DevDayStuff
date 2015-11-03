using System;
using System.Reactive.Linq;
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

            lowNums.Subscribe(callback);
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
    }
}
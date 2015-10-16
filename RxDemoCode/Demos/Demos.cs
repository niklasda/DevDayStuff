using System;
using System.Reactive.Linq;

namespace RxDemoCode.Demos
{
    public class Demos
    {
        public void Demo1(Func<long, string> callback)
        {
            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));

            IObservable<long> lowNums = from n in oneNumberPerSecond where n < 10 select n;

            lowNums.Subscribe(lowNum => callback(lowNum));
        }

        public void Demo2(Action<string> callback)
        {
            char[] chars = "Welcome to Rx.NET\n".ToCharArray();

            IObservable<long> oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(0.2));
           
            IObservable<string> lowNums = from n in oneNumberPerSecond where n < chars.Length select chars[n].ToString();

            lowNums.Subscribe(callback);
        }
    }
}

using System;
using System.Reactive.Linq;

namespace RxDemoCode
{
    public class Demos
    {
        public void Demo1(Func<long, string> callback)
        {
            var oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(1));

            var lowNums = from n in oneNumberPerSecond
                          where n < 5
                          select n;

            //Console.WriteLine("Numbers < 5:");

            lowNums.Subscribe(lowNum =>
            {
                callback(lowNum);
            });
        }
    }
}

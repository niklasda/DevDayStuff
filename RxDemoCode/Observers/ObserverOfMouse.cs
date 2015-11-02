using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace RxDemoCode.Observers
{
    public class ObserverOfMouse : IObserver<EventPattern<MouseEventArgs>>
    {
        public void OnNext(EventPattern<MouseEventArgs> value)
        {
            var p = value.EventArgs.GetPosition((UIElement)value.Sender);
            var s = string.Format("{0} {1}", p.X, p.Y);
            Debug.WriteLine(s);
        }

        public void OnError(Exception error)
        {
            Debug.WriteLine(error.ToString());
        }

        public void OnCompleted()
        {
            Debug.WriteLine("Complete");
        }
    }
}
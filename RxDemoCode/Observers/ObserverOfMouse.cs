using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace RxDemoCode.Observers
{
    public class ObserverOfMouse : IObserver<EventPattern<MouseEventArgs>>
    {
        private Action<string> _callback;

        public ObserverOfMouse(Action<string> callback)
        {
            _callback = callback;
        }

        public void OnNext(EventPattern<MouseEventArgs> value)
        {
            var p = value.EventArgs.GetPosition((UIElement)value.Sender);
            var s = string.Format("{0} {1}", p.X, p.Y);

            _callback(s);
//            Debug.WriteLine(s);
        }

        public void OnError(Exception error)
        {
            _callback(error.ToString());
 //           Debug.WriteLine(error.ToString());
        }

        public void OnCompleted()
        {
            _callback("Complete");
   //n         Debug.WriteLine("Complete");
        }
    }
}
using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace RxDemoCode.Observers
{
    public class ObserverOfString : IObserver<string>
    {
        private Action<string> _callback;

        public ObserverOfString(Action<string> callback)
        {
            _callback = callback;
        }

        public void OnNext(string value)
        {
            //var p = value.EventArgs.GetPosition((UIElement)value.Sender);
            var s = string.Format("{0}", value);

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
   //         Debug.WriteLine("Complete");
        }
    }
}
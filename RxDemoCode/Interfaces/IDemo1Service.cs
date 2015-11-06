using System;
using System.Windows;

namespace RxDemoCode.Interfaces
{
    public interface IDemo1Service
    {
        void Demo3(Action<double> callback);

        void Demo1(Action<long> callback);

        void Demo2(Action<string> callback);

        void Demo4(UIElement wnd, Action<string> callback);
        void Demo5();
        void Demo6();
    }
}
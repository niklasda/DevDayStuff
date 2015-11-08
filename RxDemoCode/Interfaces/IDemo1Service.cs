using System;
using System.Windows;

namespace RxDemoCode.Interfaces
{
    public interface IDemo1Service
    {
        void Demo1(Action<string> callback);

        void Demo2(Action<string> callback);

        void Demo3(Action<string> callback);

        void Demo4Setup(UIElement wnd, Action<string> callback);

        void Demo5(Action<string> callback);

        void Demo6(Action<string> callback);

        void Demo4Toggle();
    }
}
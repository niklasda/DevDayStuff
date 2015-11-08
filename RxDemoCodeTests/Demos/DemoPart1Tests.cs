using System;
using System.Threading;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax;
using FakeItEasy.ExtensionSyntax.Full;
using RxDemoCode.Interfaces;
using RxDemoCode.Services;

namespace RxDemoCode.Tests.Demos
{
    [TestClass]
    public class DemoPart1Tests
    {
        [TestMethod]
        public void TestDemo1()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo1(callb);
            //Thread.Sleep(3000); // method should take 2s to play out async
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10)); //0
        }

        [TestMethod]
        public void TestDemo2()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo2(callb);
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10)); //1
        }

        [TestMethod]
        public void TestDemo2_2()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo2_2(callb);
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));//1
        }

        [TestMethod]
        public void TestDemo2_3()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo2_3(callb);
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));//1
        }

        [TestMethod]
        public void TestDemo3()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo3(callb);
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));//0
        }

        [TestMethod]
        public void TestDemo4()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();
            var uie = A.Fake<UIElement>();

            d.Demo4Setup(uie, callb);
            d.Demo4Toggle();
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));//0
        }

        [TestMethod]
        public void TestDemo5()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo5(callb);
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));//5
        }

        [TestMethod]
        public void TestDemo6()
        {
            IDemo1Service d = new Demo1Service();

            var callb = A.Fake<Action<string>>();

            d.Demo6(callb);
            A.CallTo(() => callb.Invoke(A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));//5
        }

    }
}

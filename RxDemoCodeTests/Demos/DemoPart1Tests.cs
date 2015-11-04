using System;
using System.Threading;
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
        public void TestDemo12()
        {
            IDemo1Service d = new Demo1Service();

 //           var d = new RxDemoCode.Demos.DemoPart1();
            var callb = A.Fake<Action<long>>();

            d.Demo1(callb);
            Thread.Sleep(3000); // method should take 2s to play out async
            A.CallTo(() => callb.Invoke(A<long>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(10));
            
        }
    }
}

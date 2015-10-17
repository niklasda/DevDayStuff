using System;
using System.Security.Cryptography.X509Certificates;
using FsCheck;
using Microsoft.FSharp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RxDemoCodeTests.Demos
{
    [TestClass]
    public class DemoTests
    {
        [TestMethod]
        public void TestDemo1()
        {
            Func<int, string> funUnderTest = x => x.ToString(); 
            //Arb.Default.Int32().Generator();
            Func<int, Property> numProp = x => (x.ToString() == funUnderTest(x)).When(x%3 != 0);
            Prop.ForAll(numProp).QuickCheck();

           
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestDemo11()
        {
            Func<int, string> funUnderTest = x => (2*x).ToString();
            //Arb.Default.Int32().Generator();
            Func<int, Property> numProp = x => (x.ToString() == funUnderTest(x)).When(x % 3 != 0);
            Prop.ForAll(numProp).QuickCheckThrowOnFailure();


        }
    }
}

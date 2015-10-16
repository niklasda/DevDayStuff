using System;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RxDemoCodeTests
{
    [TestClass]
    public class DemoTests
    {
        [TestMethod]
        public void TestDemo1()
        {
            Func<int, Property> numProp = x => (x.ToString() == "2").When(x%3 != 0);
            Prop.ForAll(numProp).QuickCheck();
        }
    }
}

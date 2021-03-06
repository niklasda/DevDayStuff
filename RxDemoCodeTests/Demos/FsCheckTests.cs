﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RxDemoCode.Tests.Demos
{
    [TestClass]
    public class FsCheckTests
    {
        [TestMethod]
        public void TestDemo1()
        {
            Func<int, string> funUnderTest = x => x.ToString();
            //Arb.Default.Int32().Generator();
            Func<int, Property> numProp = x => (x.ToString() == funUnderTest(x)).When(x%3 != 0);
            Prop.ForAll(numProp).QuickCheckThrowOnFailure();

        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void TestDemo11()
        {
            Func<int, string> funUnderTest = x => (2*x).ToString();
            //Arb.Default.Int32().Generator();
            Func<int, Property> numProp = x => (x.ToString() == funUnderTest(x)).When(x%3 != 0);
            Prop.ForAll(numProp).QuickCheckThrowOnFailure();

            IEnumerable<int> all = Enumerable.Range(1, 100);

            var gen = from x in Arb.Generate<int>()
                from y in Gen.Choose(5, 10)
                where x > 5
                select new {Fst = x, Snd = y};

            //Gen<string> xx = Arb.Generate<string>();
            List<StringNoNulls> rndgen = Arb.Default.StringWithoutNullChars().Generator.Sample(50, 10).ToList();

            List<int> sa1 = Gen.Choose(1, 100).Sample(50, 3).ToList();
            
            List<int> sa2 = Gen.Choose(1, 100).Sample(50, 2).ToList();

            Gen<bool> chooseBool2 = Gen.Frequency(
                new WeightAndValue<Gen<bool>>(2, Gen.Constant(true)),
                new WeightAndValue<Gen<bool>>(1, Gen.Constant(false)));

            List<bool> bols = chooseBool2.Sample(2, 10).ToList();

            Debug.WriteLine(all);
            Debug.WriteLine(gen);
        }
        
    }
}

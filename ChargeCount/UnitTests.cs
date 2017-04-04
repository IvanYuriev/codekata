using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ChargeCount
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void ChargeCount_ExampleInDescription()
        {
            var c = new ChargeCounter();
            var result = c.Attempt1(4, new int[] { 1, 2 });

            Assert.AreEqual(3, result);
        }

        [Test]
        public void ChargeCount_7with125()
        {
            var c = new ChargeCounter();
            var result = c.Attempt1(7, new int[] { 1, 2, 5 });
            // 15 could be charged as:
            // 1. 1+1+1+1+1+1+1+1
            // 2. 1+1+1+1+1+2
            // 3. 1+1+1+2+2
            // 4. 1+2+2+2
            // 5. 1+1+5
            // 6. 2+5

            Assert.AreEqual(6, result);
        }

        [Test]
        public void ChargeCount_ComplexExampleSorted()
        {
            var c = new ChargeCounter();
            var result = c.Attempt1(300, new int[] { 5, 10, 20, 50, 100, 200, 500 });
            Assert.AreEqual(1022, result);
        }

        [Test]
        public void ChargeCount_ComplexNoPennies()
        {
            var c = new ChargeCounter();
            var result = c.Attempt1(301, new int[] { 5, 10, 20, 50, 100, 200, 500 });
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ChargeCount_ComplexUnsorted()
        {
            var c = new ChargeCounter();
            var result = c.Attempt1(300, new int[] { 500, 5, 50, 100, 20, 200, 10 });
            Assert.AreEqual(1022, result);
        }
    }
}

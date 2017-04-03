using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace KarateChop
{
    [TestFixture]
    public class UnitTests
    {
        private static readonly object[] ReturnChopCases =
        {
            new TestCaseData(3, new List<int> {}).Returns(-1).SetName("3 in new List<int> {}"),
            new TestCaseData(3, new List<int> {1}).Returns(-1).SetName("3 in new List<int> {1}"),
            new TestCaseData(1, new List<int> {1,2}).Returns(0).SetName("1 in new List<int> {1,2}"),
            new TestCaseData(3, new List<int> {1,2,3}).Returns(2).SetName("3 in new List<int> {1,2,3}"),
            new TestCaseData(3, new List<int> {1,2,4}).Returns(-1).SetName("3 in new List<int> {1,2,4}"),

            new TestCaseData(1, new List<int> {1}).Returns(0),

            new TestCaseData(1, new List<int> {1, 3, 5}).Returns(0),
            new TestCaseData(3, new List<int> {1, 3, 5}).Returns(1),
            new TestCaseData(5, new List<int> {1, 3, 5}).Returns(2),
            new TestCaseData(0, new List<int> {1, 3, 5}).Returns(-1),
            new TestCaseData(2, new List<int> {1, 3, 5}).Returns(-1),
            new TestCaseData(4, new List<int> {1, 3, 5}).Returns(-1),
            new TestCaseData(6, new List<int> {1, 3, 5}).Returns(-1),

            new TestCaseData(1, new List<int> {1, 3, 5, 7}).Returns(0),
            new TestCaseData(3, new List<int> {1, 3, 5, 7}).Returns(1),
            new TestCaseData(5, new List<int> {1, 3, 5, 7}).Returns(2),
            new TestCaseData(7, new List<int> {1, 3, 5, 7}).Returns(3),
            new TestCaseData(0, new List<int> {1, 3, 5, 7}).Returns(-1),
            new TestCaseData(2, new List<int> {1, 3, 5, 7}).Returns(-1),
            new TestCaseData(4, new List<int> {1, 3, 5, 7}).Returns(-1),
            new TestCaseData(6, new List<int> {1, 3, 5, 7}).Returns(-1),
            new TestCaseData(8, new List<int> {1, 3, 5, 7}).Returns(-1),

            new TestCaseData(9, new List<int> {1, 3, 5, 7, 9}).Returns(4),
            new TestCaseData(8, new List<int> {1, 3, 5, 7, 9}).Returns(-1),
        };

        [TestCaseSource("ReturnChopCases")]
        public int Recursion_Test(int SearchNumber, List<int> SearchArray)
        {
            var chopper = new RecursionChopper();
            return chopper.Chop(SearchNumber, SearchArray);
        }

        [TestCaseSource("ReturnChopCases")]
        public int Procedural_Test(int SearchNumber, List<int> SearchArray)
        {
            var chopper = new ProceduralChopper();
            return chopper.Chop(SearchNumber, SearchArray);
        }

        [TestCaseSource("ReturnChopCases")]
        public int Functional_Test(int SearchNumber, List<int> SearchArray)
        {
            var chopper = new FunctionalChopper();
            return chopper.Chop(SearchNumber, SearchArray);
        }

        [TestCaseSource("ReturnChopCases")]
        public int Functional2_Test(int SearchNumber, List<int> SearchArray)
        {
            var chopper = new FunctionalChopper2();
            return chopper.Chop(SearchNumber, SearchArray);
        }

        [TestCaseSource("ReturnChopCases")]
        public int MapReduce_Test(int SearchNumber, List<int> SearchArray)
        {
            var chopper = new MapReduceChopper();
            return chopper.Chop(SearchNumber, SearchArray);
        }

        [TestCaseSource("ReturnChopCases")]
        public int BinaryTree_Test(int SearchNumber, List<int> SearchArray)
        {
            var chopper = new BinaryTreeChopper();
            return chopper.Chop(SearchNumber, SearchArray);
        }

        [Test, Ignore("Take some time to run, so run it manually!")]
        public void PerformanceComparer()
        {
            var perf = new Stopwatch();
            var opt = Stopwatch.IsHighResolution;

            var list = Enumerable.Range(1, 2000000).ToList();
            IKarateChop chopper = null;
            int index = -1;
            int searching = 2;

            perf.Start();
            chopper = new RecursionChopper();
            index = chopper.Chop(searching, list);
            perf.Stop();
            Console.WriteLine(chopper.GetType().Name + " result=" + index + ": " + perf.ElapsedMilliseconds);
            perf.Reset();

            perf.Start();
            chopper = new ProceduralChopper();
            index = chopper.Chop(searching, list);
            perf.Stop();
            Console.WriteLine(chopper.GetType().Name + " result=" + index + ": " + perf.ElapsedMilliseconds);
            perf.Reset();

            perf.Start();
            chopper = new FunctionalChopper();
            index = chopper.Chop(searching, list);
            perf.Stop();
            Console.WriteLine(chopper.GetType().Name + " result=" + index + ": " + perf.ElapsedMilliseconds);

            perf.Start();
            chopper = new FunctionalChopper2();
            index = chopper.Chop(searching, list);
            perf.Stop();
            Console.WriteLine(chopper.GetType().Name + " result=" + index + ": " + perf.ElapsedMilliseconds);
            perf.Reset();

            perf.Start();
            chopper = new MapReduceChopper();
            index = chopper.Chop(searching, list);
            perf.Stop();
            Console.WriteLine(chopper.GetType().Name + " result=" + index + ": " + perf.ElapsedMilliseconds);
            perf.Reset();

            perf.Start();
            chopper = new BinaryTreeChopper();
            index = chopper.Chop(searching, list);
            perf.Stop();
            Console.WriteLine(chopper.GetType().Name + " result=" + index + ": " + perf.ElapsedMilliseconds);

        }
    }
}

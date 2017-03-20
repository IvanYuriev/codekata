using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KarateChop;

namespace CodeKata
{
    class ChopperFactory
    {
        private static Dictionary<int, IKarateChop>
            instances = new Dictionary<int, IKarateChop>
            {
                { 1, new RecursionChopper() },
                { 2, new ProceduralChopper() },
            };

        public static IKarateChop GetChopperAlgorithm(int index)
        {
            if (index <= 0 || index > instances.Count)
                throw new ArgumentOutOfRangeException("index");

            return instances[index];
        }
    }

}

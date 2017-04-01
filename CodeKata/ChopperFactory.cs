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
            instances;
        //= new Dictionary<int, IKarateChop>
        //    {
        //        { 1, new RecursionChopper() },
        //        { 2, new ProceduralChopper() },
        //        { 3, new FunctionalChopper() },
        //        { 4, new MapReduceChopper() },
        //    };

        static ChopperFactory()
        {
            var type = typeof(IKarateChop);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .Select((x, i) => new { Key = i, Value = x });
            instances = types.ToDictionary(
                x => x.Key + 1, 
                y => (IKarateChop)Activator.CreateInstance(y.Value) );
        }


        public static IKarateChop GetChopperAlgorithm(int index)
        {
            if (index <= 0 || index > instances.Count)
                throw new ArgumentOutOfRangeException("index");

            return instances[index];
        }

        public static IList<string> GetAllNames()
        {
            return instances.Select(x => String.Format("{0}. {1}", x.Key, x.Value.GetType().Name)).ToList();
        }
    }

}

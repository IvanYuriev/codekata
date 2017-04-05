using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateChop
{
    public class MapReduceChopper : IKarateChop
    {
        public int Chop(int lookingFor, IList<int> list)
        {
            //Mapper create parts (chops) of data with markers about its content (need to precess or not)
            //   also make sure that each mapper step is running in different thread/process/machine
            //Reducer - gather the chops all together and calculates the result
            //ha! Looks like a piece of cake!

            var parts = Map(lookingFor, list);
            return Reduce(parts);
        }

        private IEnumerable<Part> Map(int lookingFor, IEnumerable<int> list)
        {
            var allParts = GetAll(lookingFor, Split(lookingFor, 0, list));
            return allParts.ToList();
        }

        private IEnumerable<Part> GetAll(int lookingFor, IEnumerable<Part> parts)
        {
            return parts
                .Where(x => x != null)
                .SelectMany(x => x.List.Count() > 1 ? 
                    GetAll(lookingFor, Split(lookingFor, x.Index, x.List)) : 
                    new[] { x });
        }

        private int Reduce(IEnumerable<Part> parts)
        {
            if (parts == null) return -1;

            var results = parts.FirstOrDefault(x => x != null && x.Contains && x.Equals);

            return (results ?? Part.NullPart).Index;
        }

        private IEnumerable<Part> Split(int lookingFor, int startingIndex, IEnumerable<int> list)
        {
            if (list != null)
            {
                var half = (int)Math.Ceiling(list.Count() / 2.0f);

                var left = list.Take(half);
                var right = list.Skip(half);

                var middleCompare = left.LastOrDefault().CompareTo(lookingFor);
                yield return !left.Any() ? null : 
                    new Part(startingIndex, middleCompare >= 0, left)
                    {
                        Equals = left.FirstOrDefault() == lookingFor
                    };
                yield return !right.Any() ? null : 
                    new Part(startingIndex + half, middleCompare < 0, right)
                    {
                        Equals = right.FirstOrDefault() == lookingFor
                    };
            }
        }

        class Part
        {
            public static Part NullPart = new Part(-1, false, null);

            public int Index { get; set; } = 0;
            public IEnumerable<int> List { get; set; }
            public bool Contains { get; set; } = false;
            public bool Equals { get; set; } = false;

            public Part(int index, bool contains, IEnumerable<int> list)
            {
                Index = index;
                Contains = contains;
                if (contains) List = list;
                else List = new List<int>();
            }

            public override string ToString()
            {
                return String.Format("{0} {1} {2}. {3}", Index, Contains, Equals, String.Join(",", List.Select(x => x.ToString())));
            }
        }
    }


}

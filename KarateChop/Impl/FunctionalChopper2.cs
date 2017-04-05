using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateChop
{

    /// <summary>
    /// Approach to solve it really fubctional way:
    ///   - function is a first and main citizen
    ///   - no statefullness
    ///   - functions composition
    /// </summary>
    public class FunctionalChopper2 : IKarateChop
    {
        public int Chop(int lookingFor, IList<int> list)
        {
            //check the middle element
            // if larger take the left part
            // else take the right part

            Func<int, int, int> func = null; //makes possible to use a recursion in lambda expression
            func = (start, end) =>
                {
                    if (start > end) return -1;
                    var half = start + (end - start) / 2;
                    if (half >= list.Count) return -1;
                    switch (list[half].CompareTo<int>(lookingFor))
                    {
                        case ComparableEx.Cmp.Equal:    return half;
                        case ComparableEx.Cmp.Larger:   return func(start, half - 1);
                        case ComparableEx.Cmp.Smaller:  return func(half + 1, end);
                    }
                    return -1;
                };
            var result = func(0, list.Count);

            return result;
        }
    }

    public static class ComparableEx 
    {
        public enum Cmp { Equal = 0, Larger = 1, Smaller = -1 };
        public static Cmp CompareTo<T>(this T that, T value) where T : IComparable<T>
        {
            return (Cmp)that.CompareTo(value);
        }
    }
}

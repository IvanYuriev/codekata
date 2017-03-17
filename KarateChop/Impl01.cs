using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KarateChop;

namespace KarateChop
{
    /// <summary>
    /// Description: http://codekata.com/kata/kata02-karate-chop/
    /// A binary chop (sometimes called the more prosaic binary search) finds the position of 
    /// value in a sorted array of values. It achieves some efficiency by halving the number 
    /// of items under consideration each time it probes the values: 
    ///  in the first pass it determines whether the required value is in the top or the bottom half 
    ///    of the list of values. 
    ///  In the second pass in considers only this half, again dividing it in to two. 
    /// It stops when it finds the value it is looking for, or when it runs out of array to search. 
    /// Binary searches are a favorite of CS lecturers.
    /// This Kata is straightforward.Implement a binary search routine(using the specification below) 
    /// in the language and technique of your choice.Tomorrow, implement it again, using a totally 
    /// different technique.Do the same the next day, until you have five totally unique implementations 
    /// of a binary chop. (For example, one solution might be the traditional iterative approach, 
    /// one might be recursive, one might use a functional style passing array slices around, and so on).
    /// 
    /// 
    /// 
    /// this implementation uses a simple algorithmic way!
    /// </summary>
    public class RecursionChopper : IKarateChop
    {
        /// <summary>
        /// Returns an index of lookingFor element in sorted list .
        /// -1 means element not found.
        /// </summary>
        /// <param name="lookingFor">element which index we're looking for</param>
        /// <param name="list">sorted list of values</param>
        /// <returns></returns>
        public int Chop(int lookingFor, IList<int> list)
        {
            if (list.Count == 0) return -1;

            var index = Chop(0, lookingFor, list);
            return index;
        }

        private int Chop(int baseIndex, int lookingFor, IList<int> list)
        {
            if (list.Count == 0) return -1;

            var half = list.Count / 2; //in case Count = 1, half = 0
            // looking for = 1
            // 1, 2, 3, 4  = count:4, half:2
            // half - 2
            // 1. 1 < 3 ? yes, take 2: 1, 2 = count:2, half:1, baseIndex:0
            // 2. 1 < 2 ? yes, take 1: 1 = count:1, half:0, baseIndex:0
            // 3. 1 < 1 ? NO, 1 == 1 ? YES - return 0 + 0 = 0
            // OK

            // looking for = 4
            // 1, 2, 3, 4  = count:4, half:2
            // 1. 4 < 3 ? no, skip 2: 3, 4 = count:2, half:1, baseIndex:0 + 2 = 2
            // 2. 4 < 4 ? no
            // 3. 4 == 4 ? yes: 2 + 1 = 3
            // OK

            if (lookingFor < list[half])
            {
                //take the left part - searching item < current item
                var l = new List<int>(list.Take(half).ToList());
                return Chop(baseIndex, lookingFor, l);
            }
            else
            {
                //take the right part - searching item >= current item
                if (list[half] == lookingFor)
                    return baseIndex + half;
                else if (half == 0) //there is no other elements left
                    return -1;

                var l = new List<int>(list.Skip(half).ToList());
                return Chop(baseIndex + half, lookingFor, l);
            }
        }
    }
}

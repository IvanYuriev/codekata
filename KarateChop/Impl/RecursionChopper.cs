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

        private int Chop(int baseIndex, int lookingFor, IEnumerable<int> list)
        {
            var cnt = list.Count();
            if (cnt == 0) return -1;

            var half = cnt / 2; //in case Count = 1, half = 0
            var middle = list.Skip(half).FirstOrDefault();
            //TODO: take and skip linq functions could influence the performance!
            if (middle == lookingFor)
                return baseIndex + half;
            else if (half == 0)
                return -1; 
            else if (lookingFor < middle)
                return Chop(baseIndex, lookingFor, list.Take(half));
            else
                return Chop(baseIndex + half, lookingFor, list.Skip(half));
        }
    }
}

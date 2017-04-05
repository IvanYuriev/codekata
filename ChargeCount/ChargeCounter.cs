using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargeCount
{
    /// <summary>
    /// Task:
    /// count how many different charges could be made for some amount and list of coin denominations.
    /// Input: amount (unsigned integer), list of denominations
    /// Output: count of charges (integer) (lets allow values below zero for some special situations)
    /// Example: amount=4, coins=[1,2]
    /// 1.  1+1+1+1 = 4
    /// 2.  1+1+2 = 4
    /// 3.  2+2 = 4
    /// Result = 3 different ways to give a charge
    /// </summary>
    public class ChargeCounter : IChargeCounterAttempts
    {
        /// <summary>
        /// very very dirty solution of the task
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        public int Attempt1(int amount, int[] coins)
        {
            //1. generate all the possible(?? excluding anagrams) lists of coins till the sum of 'em below the amount
            //2. count how many sums are equal to amount

            /// TOUGHTS!
            /// [1, 2] split [1] [2]
            /// 1 -> [1] and [2] : [1, 1] [2, 1]
            /// 2 -> [1] and [2] : [2, 1] [2, 2]==4 excluding
            /// 1 -> [1, 1] [2, 1] : [1, 1, 1] [2, 1, 1] == 4 exluding
            /// 2 -> [2, 1] : [2, 1, 2] > 4 excluding
            /// 1 -> [1, 1, 1] : [1, 1, 1, 1] == 4 excluding
            /// 2 -> null
            /// 1 -> null
            /// total count of equals = 3
            /// 
            /// 5 with [1, 2, 5]
            /// 1 1 <- 2 <- 5
            /// 1 1 1 <- 2
            /// 1 1 1 1
            /// 1 1 1 1 1
            /// 
            /// 1 [1, 2, 5]
            /// 1 1 [1, 2, 5]
            /// 1 1 1
            /// 1 1 2
            /// 1 1 5
            /// 1 2 [2, 5]
            /// 1 2 1
            /// 1 2 2
            /// 1 2 5
            /// 1 5 [5]
            ///

            var count = 0;
            //input: number and list, Output: new list with inserted number
            Func<int, int[], int[]> merge = (c, list) =>
                {
                    var array = new int[list.Length + 1];
                    array[0] = c;
                    for (int i = 0; i < list.Length; i++) array[i + 1] = list[i];
                    return array;
                };
            Action<int[], int[]> calculate = null;
            calculate = (list, restCoins) =>
            {
                foreach (var c in restCoins)
                {
                    var set = merge(c, list);
                    //Console.WriteLine(String.Join(", ", set));
                    var sum = set.Sum();
                    //to avoid repeated combinations (1 1 2 5, 1 2 1 5, 2 1 1 5, ...)
                    //take only coins in ascending order
                    if (sum < amount) calculate(set, restCoins.Where(x => x >= c).ToArray());
                    else if (sum == amount) count++;
                }
            };
            foreach (var c in coins)
            {
                calculate(new int[] { c }, coins.Where(x => x >= c).ToArray());
            }
            return count;
        }

        /// <summary>
        /// Much more elegant way to solve the task using smart recursion
        /// </summary>
        /// <param name="amount">amount to charge</param>
        /// <param name="coins">coin denominations</param>
        /// <returns></returns>
        public int Attempt2(int amount, int[] coins)
        {
            // generates all the necessary lists without duplications
            // combining each coins with others skipping by one to remove situations like:
            // 1 1 2 1, 1 2 1 1, 2 1 1 1 - all the same charge

            // 1 arg is an accumulator that will contain every new generated list
            // 2 arg is the coins from which we're generating new lists by concatenating
            //       nextList with every coin using recursive call
            // returns the count of lists that could be used to successfully charge the amount
            Func<IEnumerable<int>, IEnumerable<int>, int> f = null;
            f = (nextList, restCoins) => {
                var sum = nextList.Sum();
                if (sum == amount) return 1; //use Console.WriteLine(...) here to output the nextList
                if (sum >  amount) return 0;
                return restCoins.Select((x, i) => 
                    f(nextList.Concat(new[] { x }), restCoins.Skip(i)))
                    .Sum();
            };

            return f(Enumerable.Empty<int>(), coins);
        }
    }
}

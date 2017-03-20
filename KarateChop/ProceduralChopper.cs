using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarateChop
{
    public class ProceduralChopper : IKarateChop
    {
        public int Chop(int lookingFor, IList<int> list)
        {
            if (list.Count == 0) return -1;

            var index = 0;
            var count = list.Count;
            while(count > 0)
            {
                var half = count / 2.0f;
                var tempIndex = index;

                count = (int)half;
                index = index + count;

                #region Thoughts
                // 0  1  2  3  4  5  6
                // count 3, half = 1
                //    v
                // 1  2  3

                // count 4, half = 2
                //       v
                // 1  2  3  4

                // count 5, half = 2
                //       v
                // 1  3  5  7  9

                // count 6, half = 3
                //          v
                // 1  3  5  7  9  8

                // count 7, half = 3
                //          v
                // 1  3  5  7  9  8  0
                #endregion

                //I think we can get rid of it now
                //if (index >= list.Count) return -1;

                if (list[index] == lookingFor) return index;
                else if(index == tempIndex) return -1; //the end is reached, but item not found

                if (lookingFor < list[index])
                {
                    index = tempIndex;
                }
                else
                {
                    //special case when looking for the last symbol in array of odd size
                    // if count = 1 then count / 2 = 0, so we won't get the
                    // last element of array
                    var hasReminder = (half - count > 0);
                    if (hasReminder) count++;
                }
                
            }
            return -1;
        }
    }
}

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

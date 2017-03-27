using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateChop
{
    /// <summary>
    /// NOT exactly functional! But interesting way, more clear for reading and understanding
    /// way to solve the task with some LINQ approach.
    /// Also async\await pattern helps to separate it in multiple threads easily
    /// </summary>
    public class FunctionalChopper : IKarateChop
    {
        public int Chop(int lookingFor, IList<int> list)
        {
            var result = ChopAsync(lookingFor, Split(0, list)).Result;
            return result;
        }

        private async Task<int> ChopAsync(int lookingFor, Part part)
        {
            Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);

            var cmp = part.MiddleValue.CompareTo(lookingFor); //-1 0 +1  (< == >)

            if (cmp != 0 &&
                part.Right.Count == 0 && 
                part.Left.Count == 0)
                return -1;

            //pattern matching should be used here 
            switch(cmp)
            {
                case 0:
                    return part.StartingIndex + part.Left.Count;
                case +1:
                    return await Task.Run(
                        () => { return ChopAsync(lookingFor, TakeLeftPart(part));
                    });
                case -1:
                    return await Task.Run(
                        () => { return ChopAsync(lookingFor, TakeRightPart(part));
                    });
            }
            return -1;
        }

        private Part TakeLeftPart(Part part)
        {
            return Split(part.StartingIndex, part.Left);
        }

        private Part TakeRightPart(Part part)
        {
            return Split(part.StartingIndex + part.Left.Count + 1, part.Right);
        }

        private Part Split(int startingIndex, ICollection<int> list)
        {
            var half = list.Count / 2;
            return new Part() {
                StartingIndex = startingIndex,
                MiddleValue = list.Skip(half).FirstOrDefault(),
                Left = list.Take(half).ToList(),
                Right = list.Skip(half + 1).ToList()
            };
        }
    }

    class Part
    {
        public IList<int> Left { get; set; }
        public IList<int> Right { get; set; }
        public int MiddleValue { get; set; }
        public int StartingIndex { get; set; }
    }
}

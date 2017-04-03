using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKata
{
    public class SearchInfoBuilder
    {
        public int Algo { get; private set; }
        public int SearchValue { get; private set; }
        public IList<int> List { get; private set; }
        public int FoundIndex { get; private set; } = -1;

        private readonly TextReader input;
        private readonly TextWriter output;

        public SearchInfoBuilder(TextReader inputStream, TextWriter outputStream)
        {
            input = inputStream;
            output = outputStream;
        }

        public void AskAlgorithm()
        {
            output.WriteLine("Select what algorithm do you want to use:");
            ChopperFactory.GetAllNames().Select(x => { Console.WriteLine(x); return x; });

            var answer = input.ReadLine();
            var algorithm = 1;
            if (!Int32.TryParse(answer, out algorithm))
                throw new ArgumentOutOfRangeException("Wrong number was entered! Default algorithm will be used...");

            Algo = algorithm;
        }

        public void AskSortedListOfValues()
        {
            output.WriteLine("Enter the sorted array of numbers (separated by space):");
            var answer = input.ReadLine();
            var list = answer
                .Split(' ')
                .Select(x => x.Trim())
                .Select(x => { var num = 0; Int32.TryParse(x, out num); return num; })
                .ToList();
            List = list;
        }

        public void AskSearchingValue()
        {
            output.WriteLine("Enter the value you're looking for:");
            var answer = input.ReadLine();
            var value = 1;
            if (!Int32.TryParse(answer, out value))
                throw new ArgumentOutOfRangeException("Wrong number was entered!");
            SearchValue = value;
        }

        public void SetFoundIndex(int index)
        {
            FoundIndex = index;
        }

        public override string ToString()
        {
            return String.Format("Index of {0} in {1} list = {2} using {3} algorithm.",
                SearchValue, 
                String.Join(", ", List.ToArray()),
                FoundIndex,
                Algo);
        }
    }
}

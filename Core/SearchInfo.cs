using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeKata;

namespace CodeKata.Core
{
    public class SearchInfoBuilder<T>
    {
        public int Algo { get; private set; }
        public int SearchValue { get; private set; }
        public IList<int> List { get; private set; }
        public int FoundIndex { get; private set; } = -1;

        private readonly TextReader input;
        private readonly TextWriter output;
        private readonly IAlgoFactory<T> algo;

        public SearchInfoBuilder(TextReader inputStream, TextWriter outputStream, IAlgoFactory<T> algoFactory)
        {
            input = inputStream;
            output = outputStream;
            algo = algoFactory;
        }

        public void AskAlgorithm()
        {
            output.WriteLine("Select what algorithm do you want to use:");
            algo.GetAllNames().Select(x => { Console.WriteLine(x); return x; }).ToList();

            var answer = input.ReadLine();
            var algorithm = 1;
            if (!Int32.TryParse(answer, out algorithm))
                throw new ArgumentOutOfRangeException("Wrong number was entered! Default algorithm will be used...");

            Algo = algorithm;
        }

        public void AskListOfValues(string message)
        {
            output.WriteLine(message);
            var answer = input.ReadLine();
            var list = answer
                .Split(' ')
                .Select(x => x.Trim())
                .Select(x => { var num = 0; Int32.TryParse(x, out num); return num; })
                .ToList();
            List = list;
        }

        public void AskSingleValue(string message)
        {
            output.WriteLine(message);
            var answer = input.ReadLine();
            var value = 1;
            if (!Int32.TryParse(answer, out value))
                throw new ArgumentOutOfRangeException("Wrong number was entered!");
            SearchValue = value;
        }

        public void SetResult(int index)
        {
            FoundIndex = index;
        }

        public void ShowResult()
        {
            output.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return String.Format("Result of {0} with {1} list = {2} using {3} algorithm.",
                SearchValue, 
                String.Join(", ", List.ToArray()),
                FoundIndex,
                Algo);
        }
    }
}

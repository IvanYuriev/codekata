using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeKata.Core;
using Core;

namespace ChargeCount
{
    public class UI : UIBase
    {
        public UI(string msg) : base(msg) { }

        protected override void Interact()
        {
            var kataTask = new ChargeCounterFactory();
            var info = new SearchInfoBuilder<IChargeCounterAttempts>(Console.In, Console.Out, kataTask);

            info.AskAlgorithm();

            info.AskSingleValue("Enter the amount:");

            info.AskListOfValues("Enter the array of coin denominations:");

            Func<int, int[], int> func = null;
            switch(info.Algo)
            {
                case 1: func = kataTask.GetAlgorithm(info.Algo).Attempt1; break;
                case 2: func = kataTask.GetAlgorithm(info.Algo).Attempt2; break;
                default: throw new ArgumentException("Wrong algorithm index was entered.");
            }
            info.SetResult(func(info.SearchValue, info.List.ToArray()));

            info.ShowResult();
        }
    }

    public interface IChargeCounterAttempts
    {
        int Attempt1(int amount, int[] coins);
        int Attempt2(int amount, int[] coins);
    }

    public class ChargeCounterFactory : IAlgoFactory<IChargeCounterAttempts>
    {
        public IChargeCounterAttempts GetAlgorithm(int index)
        {
            return new ChargeCounter();
        }

        public IList<string> GetAllNames()
        {
            return new[] { "1. Dirty attempt", "2. Elegant Recursive" };
        }
    }
}

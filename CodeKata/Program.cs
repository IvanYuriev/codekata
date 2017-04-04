using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================== Welcome to KarateChop Kata! ===================");
            var answer = String.Empty;
            var info = new SearchInfoBuilder(Console.In, Console.Out);
            do
            {
                try
                {
                    info.AskAlgorithm();
                    info.AskSortedListOfValues();
                    info.AskSearchingValue();

                    info.SetFoundIndex(ChopperFactory.GetChopperAlgorithm(info.Algo)
                        .Chop(info.SearchValue, info.List));

                    info.ToString();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("Do you want to repeat? y/n");
                answer = Console.ReadLine();
            } while (answer.Equals("y", StringComparison.InvariantCultureIgnoreCase));
        }



    }
}

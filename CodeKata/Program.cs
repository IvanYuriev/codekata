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
            do
            {
                Console.WriteLine("Select what algorithm do you want to use:");
                foreach(var name in ChopperFactory.GetAllNames())
                    Console.WriteLine(name);

                try
                {
                    answer = Console.ReadLine();
                    var algorithm = 1;
                    if (!Int32.TryParse(answer, out algorithm))
                        throw new ArgumentOutOfRangeException("Wrong number was entered! Default algorithm will be used...");


                    Console.WriteLine("Enter the sorted array of numbers (separated by space):");
                    answer = Console.ReadLine();
                    var array = answer
                        .Split(' ')
                        .Select(x => x.Trim())
                        .Select(x => { var num = 0; Int32.TryParse(x, out num); return num; })
                        .ToList();

                    Console.WriteLine("Enter the value you're looking for:");
                    answer = Console.ReadLine();
                    var value = 1;
                    if (!Int32.TryParse(answer, out value))
                        throw new ArgumentOutOfRangeException("Wrong number was entered!");

                    var result = ChopperFactory.GetChopperAlgorithm(algorithm).Chop(value, array);

                    Console.WriteLine("Index of searching element is " + result);
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

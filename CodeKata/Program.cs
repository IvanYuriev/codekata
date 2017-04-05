using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChargeCount;
using Core;

namespace CodeKata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What task do you want to run:");
            Console.WriteLine("1. Karate Chop");
            Console.WriteLine("2. Charge Counter");
            var answer = Console.ReadLine();
            UIBase ui = null;
            switch(answer.ToLower().Trim(' ', '.'))
            {
                case "1": ui = new KarateChop.UI(" ==== Karate Chop Task is running! ==== "); break;
                case "2": ui = new ChargeCount.UI(" ==== Charge Counter Task is running! ==== "); break;
                default: Console.WriteLine("Task not found!"); break;
            }
            ui?.Run();
        }
    }
}

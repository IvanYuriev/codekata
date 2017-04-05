using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class UIBase
    {
        private string welcomeMsg;

        public UIBase(string message)
        {
            welcomeMsg = message;
        }

        public void Run()
        {
            Console.WriteLine(welcomeMsg);
            var answer = String.Empty;

            do
            {
                try
                {
                    Interact();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("Do you want to repeat? y/n");
                answer = Console.ReadLine();
            } while (answer.Equals("y", StringComparison.InvariantCultureIgnoreCase));
        }

        protected abstract void Interact();
    }
}

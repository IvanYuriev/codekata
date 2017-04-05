using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeKata;
using CodeKata.Core;
using Core;

namespace KarateChop
{
    public class UI : UIBase
    {
        public UI(string msg) : base(msg) {        }

        protected override void Interact()
        {
            var kataTask = new ChopperFactory();
            var info = new SearchInfoBuilder<IKarateChop>(Console.In, Console.Out, kataTask);

            info.AskAlgorithm();
            info.AskListOfValues("Enter the sorted array of numbers (separated by space):");
            info.AskSingleValue("Enter the value you're looking for:");

            info.SetResult(kataTask.GetAlgorithm(info.Algo).Chop(info.SearchValue, info.List));

            info.ShowResult();
        }
    }
}

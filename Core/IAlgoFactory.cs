using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKata.Core
{
    public interface IAlgoFactory<T>
    {
        IList<string> GetAllNames();
        T GetAlgorithm(int index);
    }
}

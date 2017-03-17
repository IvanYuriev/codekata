using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarateChop
{
    public interface IKarateChop//<T> where T : System.IEquatable<T>
    {
        int Chop(int lookingFor, IList<int> list);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IName : IComparable
    {
        string Name
        {
            get;
        }
    }
    public interface IName<T>:IComparable<T>
    {
        string Name
        {
            get;
        }
    }
}

using Store.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IName : IComparable
    {
        event PriceUpdateHandler<PriceUpdateEventArgs> PriceUpdate;
        string Name
        {
            get;
        }
        decimal Price
        {
            get;
        }
    }
    public interface IName<T>:IComparable<T>
    {
        event PriceUpdateHandler<PriceUpdateEventArgs> PriceUpdate;
        string Name
        {
            get;
        }
        decimal Price
        {
            get;
        }
    }
}

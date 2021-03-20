using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    abstract class Container
    {
        public abstract void Add(Product p);
        public abstract int Count{ get; protected set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    delegate bool CompareParam<T>(T a, T b);
    delegate bool finder<T>(T obj);
}

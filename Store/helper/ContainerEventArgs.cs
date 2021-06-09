using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    public class ContainerEventArgs:EventArgs
    {
        public decimal Sum { get; }
        public ContainerEventArgs(decimal sum)
        {
            Sum = sum;
        }
    }
}

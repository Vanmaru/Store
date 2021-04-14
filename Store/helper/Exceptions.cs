using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    class NegativeValueException : Exception
    {
        public NegativeValueException() : base() { }
        public NegativeValueException(string message) : base(message) { }
        //public NegativePriceException(string paramName, string Message) : base(paramName, Message) { }
    }
}
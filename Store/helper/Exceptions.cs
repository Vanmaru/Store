using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    class NegativeValueException : Exception
    {
        public NegativeValueException() : base("Negative value exception.") { }
        public NegativeValueException(string message) : base(message) { }
        public NegativeValueException(string Message, Exception inner) : base(Message, inner) { }
    }


    class IndexerException : Exception
    {
        public IndexerException() : base("Indexer exception.") { }
        public IndexerException(string message) : base(message) { }
        public IndexerException(string Message, Exception inner) : base(Message, inner) { }
    }
    class OverflowException : Exception
    {
        public OverflowException() : base("collection is overflow") { }
        public OverflowException(string message) : base(message) { }
        public OverflowException(string Message, Exception inner) : base(Message, inner) { }
    }
}
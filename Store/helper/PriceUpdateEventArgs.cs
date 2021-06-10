using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    //public event EventHandler<PriceUpdateEventArgs> PriceUpdate;
    public delegate void PriceUpdate<PriceUpdateEventArgs>(object t, PriceUpdateEventArgs e);
    public delegate void PriceUpdateHandler<PriceUpdateEventArgs>(object t, PriceUpdateEventArgs e);
    public class PriceUpdateEventArgs : EventArgs
    {
        public decimal Price { get; }
        public PriceUpdateEventArgs(decimal price)
        {
            Price = price;
            Console.WriteLine("it works");
        }
    }
}

using Store.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IContainer<T>
    {
        int Count { get; }
        T this[int index] { get; }
        void Add(T p);
        void Remove(int index);
        void Add(IContainer<T> container);
    }

    interface IOrdereableContainer<T> : IContainer<T>
    {
        void Sort();

    }
}

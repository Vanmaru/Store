using Store.helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class ArrayContainer<T> : IEnumerable, IOrdereableContainer<T> where T : IName<T>
    {
        internal T[] data;
        public int Count { get { return data != null ? data.Length : 0; } protected set { } }
        public void Add(T p)
        {
            T[] tempData = new T[Count + 1];
            for (int i = 0; i < Count; i++)
                tempData[i] = data[i];
            tempData[Count] = p;
            data = tempData;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString()).Append("\n");
            }
            return sb.ToString();
        }
        public void Remove(int index)
        {
            if (index < 0 || index >= data.Length)
                throw new ArgumentException(
                nameof(index),
                $"Index {index} out of array range");

            T[] temp = new T[Count - 1];
            for (int i = 0, j = 0; i < data.Length; i++)
            {
                if (i == index) { i++; }
                temp[j++] = data[i];
            }

        }
        public void Add(IContainer<T> container)
        {
            for (int i = 0; i < container.Count; i++)
            {
                this.Add(container[i]);
            }
        }
        public void Sort()
        {
            for (int i = 0; i < data.Length - 1; i++)
                for (int j = 0; j < data.Length - i - 1; j++)
                    if (data[j].CompareTo(data[j + 1]) > 1)
                    {
                        T temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
        }
        public T this[int index]
        {
            get
            {
                try
                {
                    return data[index];
                }
                catch(Exception e)
                {
                    throw new IndexerException("Array list indexer exception", e);
                }
            }
            set
            {
                data[index] = value;
            }
        }
        public T this[string name]
        {
            get
            {
                return FindByName(name);
            }
        }

        private T FindByName(string name)
        {
            for (int j = 0; j < data.Length; j++)
            {
                if (data[j].Name == name)
                {
                    return data[j];
                }
            }

            throw new ArgumentException(
                nameof(name),
                $"Name {name} does not exist in container");
        }
        //    public Product this[decimal price]
        //    {
        //        get
        //        {
        //            return FindByPrice(price);
        //        }
        //    }
        //    private Product FindByPrice(decimal price)
        //    {
        //        for (int j = 0; j < data.Length; j++)
        //        {
        //            if (data[j].Price == price)
        //            {
        //                return data[j];
        //            }
        //        }
        //        throw new ArgumentOutOfRangeException(
        //            nameof(price),
        //            $"Item with price {price} was not found");
        //    }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public ArrayContainerEnum<T> GetEnumerator()
        {
            return new ArrayContainerEnum<T>(this);
        }
    }
}
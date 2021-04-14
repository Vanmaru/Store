using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class ArrayContainer : Container
    {
        private Product[] data;
        public override int Count { get { return data != null ? data.Length : 0; } protected set { } }
        public override void Add(Product p)
        {
            Product[] tempData = new Product[Count + 1];
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
                throw new ArgumentOutOfRangeException(
                nameof(index),
                $"Index {index} out of array range");

            Product[] temp = new Product[Count - 1];
            for (int i = 0, j = 0; i < data.Length; i++)
            {
                if (i == index) { i++; }
                temp[j++] = data[i];
            }

        }
        public void Sort()
        {
            for (int i = 0; i < data.Length - 1; i++)
                for (int j = 0; j < data.Length - i - 1; j++)
                    if (data[j].Price < data[j + 1].Price)
                    {
                        Product temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
        }
        public Product this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;
            }
        }
        public Product this[string name]
        {
            get
            {
                return FindByName(name);
            }
        }

        private Product FindByName(string name)
        {
            for (int j = 0; j < data.Length; j++)
            {
                if (data[j].Name == name)
                {
                    return data[j];
                }
            }

            throw new ArgumentOutOfRangeException(
                nameof(name),
                $"Name {name} does not exist in container");
        }

        public override int CompareTo(object obj)
        {
            throw new NotImplementedException();
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
    }
}
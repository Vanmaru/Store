using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class ArrayContainer:Container
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
            for (int i = 0; i < data.Length ; i++)
            {
                sb.Append(data[i].ToString()).Append("\n");
            }
            return sb.ToString();
        }
        public void Remove(int index)
        {

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
    }
}

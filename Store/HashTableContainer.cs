using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class HashTableContainer : Container
    {
        int count = 0;
        struct HashNode{
            public int key;
            public Product data;
        }

        private HashNode[] basket = new HashNode[100];
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < basket.Length; i++)
            {
                if (basket[i].data==null)
                {
                    continue;
                }
                sb.Append(basket[i].data.ToString()).Append("\n");
            }
            return sb.ToString();
        }
        public override int Count
        {
            get { return count; }
            protected set { if (value >= 0) count = value; }
        }
        private int Hash(Product p)
        {
            int key;
            key = p.GetHashCode();
            return key % basket.Length;
        }
        public override void Add(Product p)
        {
            HashNode prodToAdd;
            prodToAdd.data = p;
            prodToAdd.key = Hash(p);
            int[] place = { prodToAdd.key, prodToAdd.key };
            while (prodToAdd.key!=basket.Length)
            {
                if (basket[place[0]].data == null)
                {
                    basket[place[0]] = prodToAdd;
                    //basket[place[0]].key = place[0];
                    count++;
                    return;
                }
                if (basket[place[1]].data == null)
                {
                    basket[place[1]] = prodToAdd;
                    //basket[place[1]].key = place[1];
                    count++;
                    return;
                }
                prodToAdd.key++;
                place[0]++;
                place[1]--;
            }
            Console.WriteLine("Something went wrong");
        }
        public void Remove(int index)
        {
            basket[index].key = 0;
            basket[index].data = null;
        }
        public Product this[int key] => FindByKey(key);

        private Product FindByKey(int key)
        {
            for (int i = basket.Length/2, j = i; i < basket.Length | j < 0; i++, j--)
            {
                if (basket[i].key==key)
                {
                    return basket[i].data;
                }
                if (basket[j].key==key)
                {
                    return basket[j].data;
                }
            }
            throw new ArgumentOutOfRangeException(
                nameof(key),
                $"Key {key} not found");
        }
        public Product this[string name] => FindByName(name);

        private Product FindByName(string name)
        {
            for (int i = 0; i < basket.Length; i++)
            {
                if (basket[i].data.Name == name)
                {
                    return basket[i].data;
                }
            }

            throw new ArgumentOutOfRangeException(
                nameof(name),
                $"Name {name} does not exist in container");
        }
        public Product this[decimal price] => FindByPrice(price);

        private Product FindByPrice(decimal price)
        {
            for (int i = 0; i < basket.Length; i++)
            {
                if (basket[i].data.Price == price)
                {
                    return basket[i].data;
                }
            }

            throw new ArgumentOutOfRangeException(
                nameof(price),
                $"Name {price} does not exist in container");
        }
    }
}
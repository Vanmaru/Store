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
                    count++;
                    return;
                }
                if (basket[place[1]].data == null)
                {
                    basket[place[1]] = prodToAdd;
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
    }
}
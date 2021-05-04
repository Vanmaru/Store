using System;
using System.Text;

namespace Store
{
    class HashTableContainer<T> : IContainer<T> where T : IName<T>
    {
        int count = 0;
        struct HashNode{
            public int key;
            public T data;
        }

        private HashNode[] basket;

        public HashTableContainer(int size)
        {
            basket = new HashNode[size];
        }
        
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
        public int Count
        {
            get { return count; }
            protected set { if (value >= 0) count = value; }
        }
        private int Hash(T p)
        {
            int key;
            key = p.GetHashCode();
            return key % basket.Length;
        }
        public void Add(T p)
        {
            if (basket.Length >= count)
                throw new OverflowException(
                $"Count of elements {basket.Length}. Hash-table overflow");

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
            throw new ArgumentOutOfRangeException(
                nameof(prodToAdd.key),
                $"No room for element {prodToAdd.key}");
        }
        public void Remove(int index)
        {
            try
            {
                basket[index].key = 0;
                basket[index].data = default;
            }
            catch(Exception e)
            {
                throw new ArgumentException(
                nameof(index),
                $"Index {index} does not exist in container", e);
            }
        }
        public T this[int key]
        {
            get
            {
                return FindByKey(key);
            }
        }

        private T FindByKey(int key)
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
            throw new ArgumentException(
                nameof(key),
                $"Key {key} not found");
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
            for (int i = 0; i < basket.Length; i++)
            {
                if (basket[i].data.Name == name)
                {
                    return basket[i].data;
                }
            }

            throw new ArgumentException(
                nameof(name),
                $"Name {name} does not exist in container");
        }

        public void Add(IContainer<T> container)
        {
            throw new NotImplementedException();
        }
        //public Product this[decimal price]
        //{
        //    get
        //    {
        //        return FindByPrice(price);
        //    }
        //}

        //private Product FindByPrice(decimal price)
        //{
        //    for (int i = 0; i < basket.Length; i++)
        //    {
        //        if (basket[i].data.Price == price)
        //        {
        //            return basket[i].data;
        //        }
        //    }

        //    throw new ArgumentOutOfRangeException(
        //        nameof(price),
        //        $"Name {price} does not exist in container");
        //}
    }
}
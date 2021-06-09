using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Store.helper;

namespace Store
{
    internal struct HashNode<T>
    {
        public int key;
        public T data;
    }
    class HashTableContainer<T> : IEnumerable, IContainer<T>, ICustomSerializable where T : IName<T>, ICustomSerializable
    {
        int count = 0;

        private HashNode<T>[] container;
        public HashTableContainer(int size)
        {
            container = new HashNode<T>[size];
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < container.Length; i++)
            {
                if (container[i].data == null)
                {
                    continue;
                }
                sb.Append(container[i].data.ToString()).Append("\n");
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
            return key % container.Length;
        }
        public void Add(T p)
        {
            if (container.Length == count)
                throw new helper.OverflowException(
                $"Count of elements {container.Length}. Hash-table overflow");

            HashNode<T> prodToAdd;
            prodToAdd.data = p;
            prodToAdd.key = Hash(p);
            int[] place = { prodToAdd.key, prodToAdd.key };
            while (prodToAdd.key != container.Length)
            {
                if (container[place[0]].data == null)
                {
                    container[place[0]] = prodToAdd;
                    count++;
                    return;
                }
                if (container[place[1]].data == null)
                {
                    container[place[1]] = prodToAdd;
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
                container[index].key = 0;
                container[index].data = default;
            }
            catch (Exception e)
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
            for (int i = container.Length / 2, j = i; i < container.Length | j < 0; i++, j--)
            {
                if (container[i].key == key)
                {
                    return container[i].data;
                }
                if (container[j].key == key)
                {
                    return container[j].data;
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
            for (int i = 0; i < container.Length; i++)
            {
                if (container[i].data.Name == name)
                {
                    return container[i].data;
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
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
        //private IEnumerator<T> GetEnumerator()
        //{
        //    for (int i = 0; i < basket.Length; i++)
        //    {
        //        if (this[i].Name != null)
        //        {
        //            yield return this[i];
        //        }
        //    }
        //}
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private HashTableContainerEnum<T> GetEnumerator()
        {
            return new HashTableContainerEnum<T>(this);
        }
        #region Serialization
        public virtual void SetObjectData(BinaryReader stream)
        {
            while (stream.PeekChar() != -1)
            {
                Add((T)Serializer.Load(stream));
            }
        }
        public virtual void GetObjectData(BinaryWriter stream)
        {
            for(int i =0;i<Count;i++)
            {
                Serializer.Save(stream, container[i].data);
            }
        }
        #endregion
        public T Find(finder<T> del)
        {
            for (int i = 0; i < Count; i++)
            {
                if (del(container[i].data))
                {
                    return container[i].data;
                }
            }
            throw new IndexerException();
        }
        public IEnumerable<T> FindAll(finder<T> del)
        {
            for (int i = 0; i < Count; i++)
            {
                if (del(container[i].data))
                    yield return container[i].data;
            }
        }
    }
}
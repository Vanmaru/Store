using Store.helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    internal class Node<T>
    {
        public Node<T> prev;
        public Node<T> next;
        public T data;
        public Node(T p = default)
        {
            data = p;
        }
        public override string ToString()
        {
            return data.ToString();
        }
    }
    class ListContainer<T> : IEnumerable, IOrdereableContainer<T>, ICustomSerializable where T : IName<T>, ICustomSerializable
    {

        internal Node<T> start = null;
        internal Node<T> finish = null;
        int count = 0;
        public int Count
        {
            get { return count; }
            protected set { if (value >= 0) count = value; }
        }
        public void Add(T p)
        {
            Node<T> node = new Node<T>(p);
            if (finish == null)
            {
                start = node;
                finish = node;
            }
            else
            {
                node.prev = finish;
                finish.next = node;
                finish = node;
            }
            count++;
        }
        public void Add(IContainer<T> container)
        {
            for (int i = 0; i < container.Count; i++)
            {
                this.Add(container[i]);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (Node<T> n = start; n != null; n = n.next)
            {
                sb.Append(n.data.ToString()).Append("\n");
            }
            return sb.ToString();
        }
        private T PopHead()
        {
            if (start != null)
            {
                T p = start.data;
                start = start.next;
                if (start != null)
                    start.prev = null;
                Count--;
                return p;
            }
            return default;

        }
        private T PopFinish()
        {
            if (finish != null)
            {
                T p = finish.data;
                finish = finish.prev;
                if (finish != null)
                    finish.next = null;
                Count--;
                return p;
            }
            return default;
        }
        private Node<T> GetElement(int index)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(
                nameof(index),
                $"Index {index} out of range");
            Node<T> prom = start;
            for (int i = 0; i < index; i++)
            {
                prom = prom.next;
            }
            return prom;
        }
        public void Remove(int index)
        {
            if (index > count)
                throw new ArgumentException(
                    nameof(index),
                    $"Index {index} does not exist");

            Node<T> toDelete = GetElement(index);

            if (toDelete == start)
                PopHead();
            if (toDelete == finish)
                PopFinish();
            toDelete.prev.next = toDelete.next;
            toDelete.next.prev = toDelete.prev;
            Count--;
        }
        private void Swap(Node<T> a, Node<T> b)
        {
            T temp = a.data;
            a.data = b.data;
            b.data = temp;
        }
        public void Sort()
        {
            for (Node<T> i = start; i.next != null; i = i.next)
            {
                for (Node<T> j = i; j.next != null; j = j.next)
                {
                    if (j.data.Name.CompareTo(j.next.data.Name) > 0)
                    {
                        Swap(j, j.next);
                    }
                }
            }
        }
        public void Clear()
        {
            while (count != 0)
            {
                PopHead();
            }
        }
        public T this[int index]
        {
            get
            {
                return FindByIndex(index);
            }
        }
        private T FindByIndex(int index)
        {
            if (index > Count | index < 0)
            {
                throw new ArgumentException(
                    nameof(index),
                    $"Index {index} out of range");
            }
            Node<T> prom = GetElement(index);
            return prom.data;
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
            Node<T> prom = start;
            for (int i = 0; i < Count; i++)
            {
                if (prom.data.Name == name)
                {
                    return prom.data;
                }
                prom = prom.next;
            }
            throw new ArgumentOutOfRangeException(
                nameof(name),
                $"Name {name} does not exist in container");
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
        //    Node prom = start;
        //    for (int i = 0; i < Count; i++)
        //    {
        //        if (prom.data.Price == price)
        //        {
        //            return prom.data;
        //        }
        //        prom = prom.next;
        //    }
        //    throw new ArgumentOutOfRangeException(
        //        nameof(price),
        //        $"Name {price} does not exist in container");
        //}
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public ListContainerEnum<T> GetEnumerator()
        {
            return new ListContainerEnum<T>(start);
        }
        public IEnumerable<T> InverseEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return this[i];
            }
        }
        public IEnumerable<T> FilterEnumerator(string needToContain)
        {
            for (Node<T> i = start; i != null; i = i.next)
            {
                if (i.data.Name.Contains(needToContain))
                {
                    yield return i.data;
                }
            }
        }
        public IEnumerable<T> SortedEnumerator()
        {
            Node<T> lessThenToWrite = start;
            for (Node<T> i = start; i != null; i = i.next)
            {
                if (lessThenToWrite.data.CompareTo(i.data) < 0)
                {
                    lessThenToWrite = i;
                }
            }
            yield return lessThenToWrite.data;
            Node<T> toWrite = start;
            int n = count;
            while (n != 0)
            {
                for (Node<T> i = start; i != null; i = i.next)
                {
                    if (i.data.CompareTo(toWrite.data) > 0 && (lessThenToWrite.data.CompareTo(i.data) > 0))
                    {
                        toWrite = i;
                    }
                }
                yield return toWrite.data;
                n--;
            }
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
            foreach (var t in this)
            {
                Serializer.Save(stream,t);
            }
        }
        #endregion
        public void Sort(CompareParam<T> del)
        {
            if (Count < 1) return;
            for (Node<T> i = start; i.next != null; i = i.next)
            {
                for (Node<T> j = i; j.next != null; j = j.next)
                {
                    if (del(j.data, j.next.data))
                    {
                        Swap(j, j.next);
                    }
                }
            }
        }
        public T Find(finder<T> del)
        {
            for (Node<T> i = start; i !=null; i=i.next)
            {
                if (del(i.data))
                {
                    return i.data;
                }
            }
            throw new IndexerException();
        }
        public IEnumerable<T> FindAll(finder<T> del)
        {
            for (Node<T> i = start; i != null; i = i.next)
            {
                if (del(i.data))
                    yield return i.data;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class ListContainer<T> : IOrdereableContainer<T> where T: IName<T>
    {
        private class Node
        {
            public Node prev;
            public Node next;
            public T data;
            public Node(T p)
            {
                data = p;
            }
            public override string ToString()
            {
                return data.ToString();
            }
        }
        Node start = null;
        Node finish = null;
        int count = 0;
        public int Count
        {
            get { return count; }
            protected set { if (value >= 0) count = value; }
        }
        public void Add(T p)
        {
            Node node = new Node(p);
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
            for(int i=0;i<container.Count;i++)
            {
                this.Add(container[i]);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (Node n = start; n != null; n = n.next)
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
            return default(T);

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
        private Node GetElement(int index)
        {
            if (index<0 || index > count)
                throw new ArgumentOutOfRangeException(
                nameof(index),
                $"Index {index} out of range");
            Node prom = start;
            for (int i = 0; i < index; i++)
            {
                prom = prom.next;
            }
            return prom;
        }
        public void Remove(int index)
        {
            if(index>count)
                throw new ArgumentException(
                    nameof(index),
                    $"Index {index} does not exist");

            Node toDelete = GetElement(index);

            if (toDelete == start)
                PopHead();
            if (toDelete == finish)
                PopFinish();
            toDelete.prev.next = toDelete.next;
            toDelete.next.prev = toDelete.prev;
            Count--;
        }
        private void Swap(Node a, Node b)
        {
            T temp = a.data;
            a.data = b.data;
            b.data = temp;
        }
        public void Sort()
        {
            for(Node i=start; i.next!=null;i=i.next)
            {
                for (Node j=i; j.next!=null; j=j.next)
                {
                    if (j.data.Name.CompareTo(j.next.data.Name)>0)
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
            if (index > Count|index < 0)
            {
                throw new ArgumentException(
                    nameof(index),
                    $"Index {index} out of range");
            }
            Node prom = GetElement(index);
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
            Node prom = start;
            for (int i = 0; i < Count; i++)
            {
                if (prom.data.Name==name)
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    class ListContainer : Container
    {
        private class Node
        {
            public Node prev;
            public Node next;
            public Product data;
            public Node(Product p)
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
        public override int Count 
        { 
            get { return count; } 
            protected set { if (value >= 0) count = value; }
        }
        public override void Add(Product p)
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
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (Node n = start; n != null; n = n.next)
            {
                sb.Append(n.data.ToString()).Append("\n");
            }
            return sb.ToString();
        }
        private Product PopHead()
        {
            if (start != null)
            {
                Product p = start.data;
                start = start.next;
                if (start != null)
                    start.prev = null;
                Count--;
                return p;
            }
            return null;

        }
        private Product PopFinish()
        {
            if (finish != null)
            {
                Product p = finish.data;
                finish = finish.prev;
                if (finish != null)
                    finish.next = null;
                Count--;
                return p;
            }
            return null;
        }
        private Node GetElement(int index)
        {
            Node prom = start;
            for (int i = 0; i < index ; i++)
            {
                prom = prom.next;
            }
            return prom;
        }
        public void Remove(int index)
        {
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
            Product temp = a.data;
            a.data = b.data;
            b.data = temp;
        }
        public void SortByPrice()
        {
            Node toSort = start;
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (toSort.data.Price < toSort.next.data.Price)
                    {
                        Swap(toSort, toSort.next);
                    }
                }
                if (i == 0) start = toSort;
            }
        }
        public void Clear()
        {
            while (count != 0)
            {
                PopHead();
            }
        }
        public Product this[int index] => FindByIndex(index);
        private Product FindByIndex(int index)
        {
            if (index > Count|index < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    $"Index {index} out of range");
            }
            Node prom = GetElement(index);
            return prom.data;
        }
        public Product this[string name] => FindByName(name);
        private Product FindByName(string name)
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
        public Product this[decimal price] => FindByPrice(price);
        private Product FindByPrice(decimal price)
        {
            Node prom = start;
            for (int i = 0; i < Count; i++)
            {
                if (prom.data.Price == price)
                {
                    return prom.data;
                }
                prom = prom.next;
            }
            throw new ArgumentOutOfRangeException(
                nameof(price),
                $"Name {price} does not exist in container");
        }
    }
}
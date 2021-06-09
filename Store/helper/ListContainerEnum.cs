using System;
using System.Collections;
using System.Collections.Generic;

namespace Store.helper
{
    public class ListContainerEnum<T> : IEnumerator<T> where T : IName<T>, IComparable<T>
    {
        Node<T> reset;
        Node<T> position;
        internal ListContainerEnum(Node<T> start)
        {
            reset = new Node<T>() { next = start };
            position = reset;

        }
        public bool MoveNext()
        {
            position = position.next;
            return (position != null);
        }
        public void Reset()
        {
            position = reset;
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        public T Current
        {
            get
            {
                return position.data;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Store.helper
{
    public class ListContainerEnum<T> : IEnumerator<T> where T : IName<T>, IComparable<T>
    {
        private ListContainer<T> container;
        int position =-1;
        internal ListContainerEnum(ListContainer<T> container)
        { this.container = container; }
        public bool MoveNext()
        {
            position ++;
            return (position < container.Count);
        }
        public void Reset()
        {
            position = -1;
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
                return container[position];
            }
        }
    }
}
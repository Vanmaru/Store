using System;
using System.Collections;
using System.Collections.Generic;

namespace Store.helper
{
    public class ArrayContainerEnum<T> : IEnumerator<T> where T : IName<T>, IComparable<T>, ICustomSerializable
    {
        private readonly ArrayContainer<T> container;
        int position = -1;
        internal ArrayContainerEnum(ArrayContainer<T> container)
        { this.container = container; }
        public bool MoveNext()
        {
            position++;
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
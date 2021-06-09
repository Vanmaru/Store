using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.helper
{
    class HashTableContainerEnum<T> : IEnumerator<T> where T : IName<T>, IComparable<T>, ICustomSerializable
    {
        private HashTableContainer<T> container;
        int position = -1;
        internal HashTableContainerEnum(HashTableContainer<T> container)
        { this.container = container; }
        public bool MoveNext()
        {
            do
            {
                position++;
            } while (container[position].Name == default);
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
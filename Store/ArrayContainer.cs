using Store.helper;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Store
{
    class ArrayContainer<T> : IEnumerable<T>, IOrdereableContainer<T>, ICustomSerializable where T : IName<T>, IComparable<T>, ICustomSerializable
    {
        internal T[] data;
        public int Count { get => data != null ? data.Length : 0; protected set { } }
        public decimal totalPrice = 0;
        public decimal TotalPrice { get => totalPrice; protected set => totalPrice = value; }
        public event PriceUpdateHandler<PriceUpdateEventArgs> TotalPriceUpdate;
        public void Add(T p)
        {
            //p.PriceUpdate += helper.PriceUpdate;
            //TotalPriceUpdate?.Invoke(this, e);
            p.PriceUpdate += PriceUpdate;
            TotalPrice += p.Price;
            T[] tempData = new T[Count + 1];
            for (int i = 0; i < Count; i++)
                tempData[i] = data[i];
            tempData[Count] = p;
            data = tempData;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString()).Append("\n");
            }
            return sb.ToString();
        }
        public void Remove(int index)
        {
            if (index < 0 || index >= data.Length)
                throw new ArgumentException(
                nameof(index),
                $"Index {index} out of array range");
            this[index].PriceUpdate -= PriceUpdate;
            TotalPrice -= this[index].Price;
            T[] temp = new T[Count - 1];
            for (int i = 0, j = 0; i < data.Length-1; i++)
            {
                if (i == index) { i++; }
                temp[j++] = data[i];
            }
            data = temp;
        }
        public void Add(IContainer<T> container)
        {
            for (int i = 0; i < container.Count; i++)
            {
                this.Add(container[i]);
            }
        }
        public void Sort()
        {
            for (int i = 0; i < data.Length - 1; i++)
                for (int j = 0; j < data.Length - i - 1; j++)
                    if (data[j].CompareTo(data[j + 1]) > 1)
                    {
                        T temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
        }
        public T this[int index]
        {
            get
            {
                try
                {
                    return data[index];
                }
                catch (Exception e)
                {
                    throw new IndexerException("Array list indexer exception", e);
                }
            }
            set
            {
                data[index] = value;
            }
        }
        #region Indexers
        public T this[string name] => FindByName(name);
        //{
        //    get
        //    {
        //        return FindByName(name);
        //    }
        //}

        private T FindByName(string name)
        {
            for (int j = 0; j < data.Length; j++)
            {
                if (data[j].Name == name)
                {
                    return data[j];
                }
            }

            throw new ArgumentException(
                nameof(name),
                $"Name {name} does not exist in container");
        }
        public T this[decimal price]
        {
            get
            {
                return FindByPrice(price);
            }
        }
        private T FindByPrice(decimal price)
        {
            for (int j = 0; j < data.Length; j++)
            {
                if (data[j].Price == price)
                {
                    return data[j];
                }
            }
            throw new ArgumentOutOfRangeException(
                nameof(price),
                $"Item with price {price} was not found");
        }
        #endregion
        #region Enumerators
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayContainerEnum<T>(this);
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
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name.Contains(needToContain))
                {
                    yield return this[i];
                }
            }
        }
        public IEnumerable<T> SortedEnumerator()
        {
            T lessThenToWrite = default;
            for (int i = 0; i < Count; i++)
            {
                if (lessThenToWrite.CompareTo(data[i]) < 0)
                {
                    lessThenToWrite = data[i];
                }
            }
            yield return lessThenToWrite;
            T toWrite = lessThenToWrite;
            int n = Count;
            while (n != 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (data[i].CompareTo(toWrite) > 0 && (lessThenToWrite.CompareTo(data[i]) > 0))
                    {
                        toWrite = data[i];
                    }
                }
                yield return toWrite;
                n--;
            }
        }
        #endregion
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
                Serializer.Save(stream, t);
            }
        }
        #endregion
        public void Sort(CompareParam<T> del)
        {
            if (Count < 1) return;
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    if (del(data[j], data[j + 1]))
                    {

                        T temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
        }
        public T Find(finder<T> del)
        {
            for (int i = 0; i < Count; i++)
            {
                if (del(data[i]))
                {
                    return data[i];
                }
            }
            throw new IndexerException();
        }
        public IEnumerable<T> FindAll(finder<T> del)
        {
            for (int i = 0; i < Count; i++)
            {
                if (del(data[i]))
                    yield return data[i];
            }
        }
        protected virtual void PriceUpdate(object sender, PriceUpdateEventArgs e)
        {
            TotalPrice += e.Price;
        }
        public IEnumerable<T> MaxPrice()
        {
            var result = (from prod in this
                          where prod.Price == ((from t in this select t).Max((p) => p.Price))
                          select prod);
            foreach (T item in result)
            {
                yield return item;
            }
        }
        public IEnumerable<T> MinPrice()
        {
            var result =
                (from prod in this
                 where prod.Price == ((from t in this select t).Min((p) => p.Price))
                 select prod);
            foreach (T item in result)
            {
                yield return item;
            }
        }
        public IEnumerable Average()
        {
            var items =
                (from prod in this
                 group prod by prod.GetType().Name into types
                 select new { keys = types.Key, averages = types.Average((p) => p.Price) });
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}
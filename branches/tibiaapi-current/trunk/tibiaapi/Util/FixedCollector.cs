using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Util
{
    public class FixedCollector<T> : IEnumerable<T>
    {
        private T[] array;
        private int index;
        public int Count { get; set; }
        public int Size { get; set; }

        public FixedCollector(int size)
        {
            Reset(size);
        }

        public void Reset(int size)
        {
            Size = size;
            T[] old = array;
            array = new T[Size];
            index = 0;
            Count = 0;
        }

        public void Push(T element)
        {
            array[index] = element;
            index = (index + 1) % Size;
            if (Count < Size)
                Count++;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            int i = index;
            for (int j = 0; j < Count; j++)
            {
                i = ((i + Size) - 1) % Size;
                yield return array[i];
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

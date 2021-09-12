using System;
using System.Collections;
using System.Collections.Generic;

namespace LpL1
{
    public class DataMonitor<TData> : IEnumerable<TData>
    {
        private int _lastIndex = 0;
        private TData[] Data { get; set; }

        public DataMonitor(int size)
        {
            Data = new TData[size];
        }

        public void Add(TData newItem)
        {
            if (_lastIndex >= Data.Length)
            {
                throw new IndexOutOfRangeException("No more space in data monitor left");
            }

            Data[_lastIndex] = newItem;
            _lastIndex++;
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return ((IEnumerable<TData>) Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public TData this [int index] => Data[index];
    }
}
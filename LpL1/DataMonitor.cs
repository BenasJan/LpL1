using System;
using System.Collections;
using System.Collections.Generic;

namespace LpL1
{
    public class DataMonitor : IEnumerable<Vehicle>
    {
        private int _lastIndex = 0;
        private Vehicle[] Data { get; set; }

        public DataMonitor(int size)
        {
            Data = new Vehicle[size];
        }

        public void Add(Vehicle newItem)
        {
            if (_lastIndex >= Data.Length)
            {
                throw new IndexOutOfRangeException("No more space in data monitor left");
            }

            Data[_lastIndex] = newItem;
            _lastIndex++;
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return ((IEnumerable<Vehicle>) Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public Vehicle this [int index] => Data[index];
    }
}
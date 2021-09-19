using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LpL1.Models;

namespace LpL1.Monitors
{
    public class DataMonitor : IEnumerable<Vehicle>
    {
        private static readonly object AddItemPadlock = new();
        private static readonly object GetItemPadlock = new();
        
        public bool ItemsExist => _maxSize > _takenItemsCount;
        public bool AllItemsAdded => !(_maxSize > _addedItemsCount);
        
        private int _lastIndex = -1;
        private Vehicle[] Data { get; set; }
        private readonly int _maxSize;
        private int _addedItemsCount;
        private int _takenItemsCount;

        public DataMonitor(int size)
        {
            _maxSize = size;
            Data = new Vehicle[size];
        }

        public void AddItem(Vehicle newItem)
        {
            if (_lastIndex >= Data.Length || AllItemsAdded)
            { 
                throw new IndexOutOfRangeException("No more space in data monitor left");
            }
            
            _lastIndex++;
            Data[_lastIndex] = newItem;
            _addedItemsCount++;
        }

        public Vehicle GetItem()
        {
            var vehicleToReturn = Data[_lastIndex];
            
            Data[_lastIndex] = null;
            _lastIndex = _lastIndex <= 0 ? 0 : _lastIndex - 1;
            _takenItemsCount++;
            
            return vehicleToReturn;
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return ((IEnumerable<Vehicle>) Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
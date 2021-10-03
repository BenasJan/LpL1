using System.Collections;
using System.Collections.Generic;
using System.Threading;
using LpL1.Models;

namespace LpL1.Monitors
{
    public class DataMonitor : IEnumerable<Vehicle>
    {
        public bool AllDataUploaded { get; set; }

        private Vehicle[] Data { get; set; }
        private readonly int _maxSize;
        private int _lastItemIndex = -1;
        private int _count = 0;

        public DataMonitor(int size)
        {
            _maxSize = size / 2;
            Data = new Vehicle[size / 2];
        }

        public void AddItem(Vehicle item)
        {
            Monitor.Enter(Data);
            while(_count == _maxSize)
            {
                Monitor.Wait(Data);
            }
            _lastItemIndex++;
            _count++;
            Data[_lastItemIndex] = item;
            Monitor.Pulse(Data);
            Monitor.Exit(Data);
        }

        public Vehicle GetItem()
        {
            Monitor.Enter(Data);
            
            while(_count == 0)
            {
                if (AllDataUploaded)
                {
                    Monitor.Pulse(Data);
                    Monitor.Exit(Data);
                    return null;
                }
                Monitor.Wait(Data);
            }
            
            var result = Data[_lastItemIndex];
            Data[_lastItemIndex] = null;
            
            _lastItemIndex--;
            _count--;
            
            Monitor.Pulse(Data);
            Monitor.Exit(Data);
            
            return result;
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
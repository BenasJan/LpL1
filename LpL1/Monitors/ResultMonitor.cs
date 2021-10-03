using System.IO;
using System.Threading;
using LpL1.Models;

namespace LpL1.Monitors
{
    public class ResultMonitor
    {
        private int _lastItemIndex = -1;
        private ProcessedVehicle[] Data { get; }
        private bool IsEmpty => Data[0] == null;


        public ResultMonitor(int size)
        {
            Data = new ProcessedVehicle[size];
        }

        public void AddItem(ProcessedVehicle processedVehicle)
        {
            Monitor.Enter(Data);

            if (IsEmpty)
            {
                _lastItemIndex++;
                Data[_lastItemIndex] = processedVehicle;
            }
            else
            {
                var isInserted = false;
                
                for (var i = 0; i <= _lastItemIndex; i++)
                {
                    if (int.Parse(processedVehicle.Hash) > int.Parse(Data[i].Hash))
                    {
                        for (var j = _lastItemIndex + 1; j > i ; j--)
                        {
                            Data[j] = Data[j - 1];
                        }
            
                        Data[i] = processedVehicle;
                        _lastItemIndex++;
                        isInserted = true;
                        break;
                    }
                }

                if (!isInserted)
                {
                    _lastItemIndex++;
                    Data[_lastItemIndex] = processedVehicle;
                }
            }
            
            Monitor.Exit(Data);
        }

        public void PrintToFile(string fileName)
        {
            var tableBorder = new string('-', 85);

            using var outputFileStream = new StreamWriter($"../../../Data/{fileName}");
            
            outputFileStream.WriteLine("Vehicles:");
            outputFileStream.WriteLine(tableBorder);
            outputFileStream.WriteLine(
                $"|{"Manufacturer",-15}|{"Model",-30}|{"Year",-4}|{"Price",-7}|{"VIN",-17}|{"Hash",-5}|"
                );
            outputFileStream.WriteLine(tableBorder);
            foreach (var vehicle in Data)
            {
                if (vehicle != null)
                {
                    outputFileStream.WriteLine(vehicle.ToString());
                }
            }
            outputFileStream.WriteLine(tableBorder);
        }
    }
}
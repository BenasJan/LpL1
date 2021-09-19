using System;
using System.IO;
using LpL1.Models;

namespace LpL1.Monitors
{
    public class ResultMonitor
    {
        private static readonly object AddItemPadlock = new();
        private int _lastIndex = -1;
        private ProcessedVehicle[] Data { get; }

        public ResultMonitor(int size)
        {
            Data = new ProcessedVehicle[size];
        }

        public void AddItem(ProcessedVehicle processedVehicle)
        {
            lock (AddItemPadlock)
            {
                _lastIndex++;
                // Console.WriteLine($"INSERTING INTO RESULT MONITOR:\nLAST INDEX: {_lastIndex}\nDATA SIZE: {Data.Length}");
                Data[_lastIndex] = processedVehicle;
            }
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
                outputFileStream.WriteLine(vehicle.ToString());
            }
            outputFileStream.WriteLine(tableBorder);
            outputFileStream.WriteLine();
        }
    }
}
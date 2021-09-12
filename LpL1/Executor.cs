using System;
using System.Threading;
using LpL1.Data;

namespace LpL1
{
    public class Executor
    {
        private readonly int _index;
        public DataMonitor<Vehicle> DataMonitor { get; set; }
        public ResultMonitor ResultMonitor { get; set; }
        public Executor(int index)
        {
            _index = index;
        }

        public void Execute()
        {
            var vehicleToProcess = DataMonitor[_index];

            Console.WriteLine("Starting to execute");
            
            // simulating some big hash function idunnno
            Thread.Sleep(TimeSpan.FromSeconds(10));

            ResultMonitor.Add(new ProcessedVehicle
            {
                Model = vehicleToProcess.Model,
                Price = vehicleToProcess.Price,
                YearManufactured = vehicleToProcess.YearManufactured,
                Hash = "AJHGYSHEBGJ0"
            });

            Console.WriteLine("Finished execution");
        }
    }
}
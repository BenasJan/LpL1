using System;
using LpL1.Models;
using LpL1.Monitors;

namespace LpL1
{
    public class Worker
    {
        private readonly Guid _workerId;
        private DataMonitor DataMonitor { get; }
        private ResultMonitor ResultMonitor { get; }
        
        public Worker(ResultMonitor resultMonitor, DataMonitor dataMonitor)
        {
            _workerId = Guid.NewGuid();
            ResultMonitor = resultMonitor;
            DataMonitor = dataMonitor;
        }

        public void Execute()
        {
            Console.WriteLine($"Worker (ID: {_workerId}) is starting execution");
            
            while (true)
            {
                var vehicle = DataMonitor.GetItem();
                if (vehicle == null && DataMonitor.AllDataUploaded)
                {
                    Console.WriteLine($"Worker (ID: {_workerId}) has finished execution");
                    break;
                }

                var hash = CalculateVehicleHash(vehicle);

                if (hash.EndsWithOddNumber())
                {
                    var newProcessedVehicle = new ProcessedVehicle
                    {
                        Manufacturer = vehicle.Manufacturer,
                        Model = vehicle.Model,
                        Price = vehicle.Price,
                        YearManufactured = vehicle.YearManufactured,
                        VinNumber = vehicle.VinNumber,
                        Hash = hash
                    };

                    ResultMonitor.AddItem(newProcessedVehicle);
                }
            }
        }

        private string CalculateVehicleHash(Vehicle vehicle)
        {
            Console.WriteLine($"Worker (ID: {_workerId}) is processing a vehicle");
            var sum = 0;
            var hashString =
                $"{vehicle.Manufacturer}{vehicle.Model}{vehicle.YearManufactured}{vehicle.Price}{vehicle.VinNumber}";

            foreach (var character in hashString) 
            {
                sum += character;
            }
            
            for (var j = 0; j < 10000; j++)
            { 
                for (var i = 0; i < 10000; i++)
                {
                    j += 1;
                    j -= 1;
                }
            }

            return sum.ToString();
        }
    }
}
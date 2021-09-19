using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LpL1.Models;
using LpL1.Monitors;

namespace LpL1
{
    public class Worker
    {
        private Guid WorkerId;
        private DataMonitor DataMonitor { get; set; }
        private ResultMonitor ResultMonitor { get; set; }
        
        public Worker(ResultMonitor resultMonitor, DataMonitor dataMonitor)
        {
            WorkerId = Guid.NewGuid();
            ResultMonitor = resultMonitor;
            DataMonitor = dataMonitor;
        }

        public void Execute()
        {
            Console.WriteLine($"Worker (ID: {WorkerId}) is starting execution");
            
            while (DataMonitor.ItemsExist)
            {
                var vehicle = DataMonitor.GetItem();
                
                if (vehicle == null)
                {
                    continue;
                }
                
                var hash = CalculateVehicleHash(vehicle);
                
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

            Console.WriteLine("Worker has finished execution");
        }

        private string CalculateVehicleHash(Vehicle vehicle)
        {
            Console.WriteLine($"Worker (ID: {WorkerId}) is processing a vehicle");
            var sum = 0;
            var hashString =
                $"{vehicle.Manufacturer}{vehicle.Model}{vehicle.YearManufactured}{vehicle.Price}{vehicle.VinNumber}";

            foreach (var character in hashString) 
            {
                sum += character;
            }
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
            return sum.ToString();
        }
    }
}
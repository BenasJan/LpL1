using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LpL1.Data;

namespace LpL1
{
    public class Worker
    {
        private readonly Vehicle[] _vehiclesToProcess;
        public ResultMonitor ResultMonitor { get; set; }
        public Worker(IEnumerable<Vehicle> vehicles, ResultMonitor resultMonitor)
        {
            _vehiclesToProcess = vehicles.Select(v => v).ToArray();
            ResultMonitor = resultMonitor;
        }

        public void Execute()
        {
            Console.WriteLine("Worker is starting execution");

            foreach (var vehicle in _vehiclesToProcess)
            {
                var hash = CalculateVehicleHash(vehicle);
                
                ResultMonitor.Add(new ProcessedVehicle
                {
                    Manufacturer = vehicle.Manufacturer,
                    Model = vehicle.Model,
                    Price = vehicle.Price,
                    YearManufactured = vehicle.YearManufactured,
                    VinNumber = vehicle.VinNumber,
                    Hash = hash
                });
            }
            
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
            Console.WriteLine("Worker has finished execution");
        }

        private string CalculateVehicleHash(Vehicle vehicle)
        {
            var sum = 0;
            var hashString =
                $"{vehicle.Manufacturer}{vehicle.Model}{vehicle.YearManufactured}{vehicle.Price}{vehicle.VinNumber}";

            foreach (var character in hashString) 
            {
                sum += character;
            }

            return sum.ToString();
        }
    }
}
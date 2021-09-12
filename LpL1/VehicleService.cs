using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LpL1.Models;
using LpL1.Monitors;

namespace LpL1
{
    public static class VehicleService
    {
        public static List<Vehicle> GetVehicles()
        {
            var jsonString = File.ReadAllText(Constants.InvalidVehiclesFilePath);
            
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonString);
            var validVehicles = GetValidVehicles(vehicles);
            
            return validVehicles;
        }

        private static List<Vehicle> GetValidVehicles(IEnumerable<Vehicle> vehicles)
        {
            var validVehicles = vehicles.Where(v =>
                v.Manufacturer != null &&
                v.Model != null &&
                v.YearManufactured != null &&
                v.Price != null &&
                v.VinNumber != null
            );

            return validVehicles.ToList();
        }

        public static Vehicle[] GetVehiclesForWorker(DataMonitor dataMonitor, int startingIndex)
        {
            var vehicleList = new List<Vehicle>();
            
            for(var i = startingIndex; i < startingIndex + Constants.ItemCountPerWorker; i++)
            {
                var vehicle = dataMonitor[i];

                if (vehicle != null)
                {
                    vehicleList.Add(vehicle);
                }
            }

            return vehicleList.ToArray();
        }
    }
}
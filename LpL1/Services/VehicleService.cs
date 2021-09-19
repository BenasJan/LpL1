using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LpL1.Models;

namespace LpL1.Services
{
    public static class VehicleService
    {
        public static List<Vehicle> GetVehicles()
        {
            var jsonString = File.ReadAllText(Constants.ValidVehiclesFilePath);
            
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonString);
            
            return vehicles;
        }
    }
}
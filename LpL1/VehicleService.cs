using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LpL1
{
    public class VehicleService
    {
        private const string DataFilePath = "";
        public static List<Vehicle> GetVehicles()
        {
            var jsonString = File.ReadAllText(DataFilePath);
            
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonString);
            
            return vehicles;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LpL1
{
    public class VehicleService
    {
        public static List<Vehicle> GetVehicles()
        {

            return new List<Vehicle>
            {
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
                CreateVehicle("Ford", 500, 2006),
            };
            // var filePath = "sdgdshsrg";
            //
            // var fileText = File.ReadAllText(filePath);
            //
            // var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(fileText);
            //
            // return vehicles;
        }

        private static Vehicle CreateVehicle(string model, double price, int yearManufactured)
        {
            return new Vehicle
            {
                Model = model,
                Price = price,
                YearManufactured = yearManufactured
            };
        }
    }
}
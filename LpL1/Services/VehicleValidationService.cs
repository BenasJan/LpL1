using LpL1.Models;

namespace LpL1.Services
{
    public static class VehicleValidationService
    {
        public static bool IsVehicleValid(Vehicle vehicle)
        {
            return vehicle.Manufacturer != null &&
                   vehicle.Model != null &&
                   vehicle.YearManufactured != null &&
                   vehicle.Price != null &&
                   vehicle.VinNumber != null;
        }
    }
}
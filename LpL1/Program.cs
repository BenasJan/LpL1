using System.Linq;
using LpL1.Monitors;
using LpL1.Services;

namespace LpL1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var vehicles = VehicleService.GetVehicles();
            
            var dataMonitor = new DataMonitor(vehicles.Count);
            var resultMonitor = new ResultMonitor(vehicles.Count);

            var threads = ThreadService.GetThreads(dataMonitor, resultMonitor);
            
            ThreadService.StartThreads(threads);
            
            foreach (var vehicle in
                from vehicle
                in vehicles
                let isValid = VehicleValidationService.IsVehicleValid(vehicle)
                where isValid
                select vehicle)
            {
                dataMonitor.AddItem(vehicle);
            }

            dataMonitor.AllDataUploaded = true;
            
            ThreadService.JoinThreads(threads);
            
            resultMonitor.PrintToFile("Results.txt");
        }
    }
}
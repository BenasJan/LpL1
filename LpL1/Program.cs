using System.Collections.Generic;
using System.Threading;
using LpL1.Monitors;

namespace LpL1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var vehicles = VehicleService.GetVehicles();

            var dataMonitor = new DataMonitor(vehicles.Count);
            var resultMonitor = new ResultMonitor(vehicles.Count);

            foreach (var vehicle in vehicles)
            {
                dataMonitor.Add(vehicle);
            }

            var threads = ThreadService.GetThreads(dataMonitor, resultMonitor);
            ThreadService.RunThreads(threads);

            resultMonitor.PrintToFile("Results.txt");
        }
    }
}
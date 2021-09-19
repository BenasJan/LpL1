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

            var threads = ThreadService.GetThreads(dataMonitor, resultMonitor);
            
            ThreadService.StartThreads(threads);
            foreach (var vehicle in vehicles)
            {
                dataMonitor.AddItem(vehicle);
            }
            ThreadService.JoinThreads(threads);
            
            resultMonitor.PrintToFile("Results.txt");
        }
    }
}
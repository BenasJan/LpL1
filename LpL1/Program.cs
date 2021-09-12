using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            
            var threads = new List<Thread>();
            
            for(var i = 0; i <= vehicles.Count; i += 4)
            {
                var workerVehicles = new Vehicle[]
                {
                    dataMonitor[i],
                    dataMonitor[i + 1],
                    dataMonitor[i + 2],
                    dataMonitor[i + 3]
                };

                var worker = new Worker(workerVehicles, resultMonitor);
                threads.Add(new Thread(worker.Execute));
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }
            
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine(resultMonitor);
        }
    }
}
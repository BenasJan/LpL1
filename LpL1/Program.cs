using System;
using System.Linq;
using System.Threading;

namespace LpL1
{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicles = VehicleService.GetVehicles();

            var dataMonitor = new DataMonitor<Vehicle>(vehicles.Count);
            var resultMonitor = new ResultMonitor(vehicles.Count);
            
            foreach (var vehicle in vehicles)
            {
                dataMonitor.Add(vehicle);
            }

            var threads = dataMonitor.Select((vehicle, index) =>
            {
                var executor = new Executor(index);
                executor.DataMonitor = dataMonitor;
                executor.ResultMonitor = resultMonitor;
                
                return new Thread(executor.Execute);
            }).ToList();

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
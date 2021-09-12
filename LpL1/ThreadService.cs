using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LpL1.Monitors;

namespace LpL1
{
    public static class ThreadService
    {
        public static List<Thread> GetThreads(DataMonitor dataMonitor, ResultMonitor resultMonitor)
        {
            var threads = new List<Thread>();
            
            for(var i = 0; i < dataMonitor.Count(); i += Constants.ItemCountPerWorker)
            {
                var workerVehicles = VehicleService.GetVehiclesForWorker(dataMonitor, i);

                var worker = new Worker(workerVehicles, resultMonitor);
                threads.Add(new Thread(worker.Execute));
            }

            return threads;
        }

        public static void RunThreads(List<Thread> threads)
        {
            foreach (var thread in threads)
            {
                thread.Start();
            }
            
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
    }
}
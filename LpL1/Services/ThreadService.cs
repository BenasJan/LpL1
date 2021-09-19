using System.Collections.Generic;
using System.Threading;
using LpL1.Monitors;

namespace LpL1.Services
{
    public static class ThreadService
    {
        public static Thread[] GetThreads(DataMonitor dataMonitor, ResultMonitor resultMonitor)
        {
            var threads = new List<Thread>();

            for (var i = 0; i < Constants.ThreadCount; i++)
            {
                var worker = new Worker(resultMonitor, dataMonitor);
                threads.Add(new Thread(worker.Execute));
            }

            return threads.ToArray();
        }

        public static void StartThreads(Thread[] threads)
        {
            foreach (var thread in threads)
            {
                thread.Start();
            }
        }

        public static void JoinThreads(Thread[] threads)
        {
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
    }
}
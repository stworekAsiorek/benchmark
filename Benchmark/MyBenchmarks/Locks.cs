using BenchmarkDotNet.Attributes;
namespace Benchmark.MyBenchmarks
{
    public class Locks
    {
        private static object _lock = new object();
        private static int Sum;

        public Locks()
        {
        }

        [Benchmark(Baseline = true)]
        public void InterlockedThreadAddition()
        {
            Sum = 0;
            MultiThreadAddition(AdditionWithInterLocked);
        }

        [Benchmark]
        public void LockMultiThreadAddition()
        {
            Sum = 0;
            MultiThreadAddition(AdditionWithLock);
        }

        [Benchmark]
        public void MonitorThreadAddition()
        {
            Sum = 0;
            MultiThreadAddition(AdditionWithMonitor);
        }

        private void MultiThreadAddition(Action addition)
        {
            ThreadStart threadStart = new ThreadStart(addition);
            Thread t1 = new Thread(threadStart);
            Thread t2 = new Thread(threadStart);
            Thread t3 = new Thread(threadStart);

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total sum is-" + Sum);

        }

        private static void AdditionWithMonitor()
        {
            for (int i = 1; i < 50000; i++)
            {
                bool lockTaken = false;
                Monitor.Enter(_lock, ref lockTaken);
                try
                {
                    Sum++;
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(_lock);
                    }
                }
            }
        }

        private static void AdditionWithLock()
        {
            for (int i = 1; i < 50000; i++)
            {
                lock (_lock)
                {
                    Sum++;
                }
            }
        }

        private static void AdditionWithInterLocked()
        {
            for (int i = 1; i < 50000; i++)
            {
                Interlocked.Increment(ref Sum);
            }
        }

    }
}

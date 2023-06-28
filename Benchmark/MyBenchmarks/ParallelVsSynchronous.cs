using BenchmarkDotNet.Attributes;

namespace MyBenchmarks
{
    public class ParallelVsSynchronous
    {
        private readonly int length;
        public ParallelVsSynchronous() {
            length = 100;
        }

        [Benchmark(Baseline = true)]
        public void SynchronousFor()
        {
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"the value is {i} and Thread : {Thread.CurrentThread.ManagedThreadId} ");
                Thread.Sleep(10);
            }
        }

        [Benchmark]
        public void ParallelFor()
        {
            Parallel.For(0, length, count =>
            {
                Console.WriteLine($"the value is {count} and Thread : {Thread.CurrentThread.ManagedThreadId} ");
                Thread.Sleep(10);
            });
        }
    }

}


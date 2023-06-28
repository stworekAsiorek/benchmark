using BenchmarkDotNet.Attributes;

namespace Benchmark.MyBenchmarks
{
    public class LINQParallelVsSynchronous
    {
        public LINQParallelVsSynchronous()
        {
        }

        [Benchmark(Baseline = true)]
        public void SynchronousLINQ()
        {
            Console.WriteLine($"Main Thread : {Environment.CurrentManagedThreadId} Started");

            var source = Enumerable.Range(100, 500);

            var evenNums = from num in source
                           where num % 2 == 0
                           select num;

            Console.WriteLine("{0} even numbers out of {1} total",
                evenNums.Count(), source.Count());

            Console.WriteLine($"Main Thread : {Environment.CurrentManagedThreadId} Completed");
        }

        [Benchmark]
        public void ParallelLINQ()
        {
            Console.WriteLine($"Main Thread : {Environment.CurrentManagedThreadId} Started");

            var source = Enumerable.Range(100, 500);

            var evenNums = from num in source.AsParallel()
                           where num % 2 == 0
                           select num;

            Console.WriteLine("{0} even numbers out of {1} total", 
                evenNums.Count(), source.Count());

            Console.WriteLine($"Main Thread : {Environment.CurrentManagedThreadId} Completed");
        }
    }
}

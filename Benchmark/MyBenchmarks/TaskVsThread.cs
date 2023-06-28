using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.MyBenchmarks
{
    public class TaskVsThread
    {
        private readonly int length;
        public TaskVsThread()
        {
            length = 100;
        }

        [Benchmark(Baseline = true)]
        public void TaskBased()
        {
            var t = Task.Run(() => Method());
            t.Wait();
        }

        [Benchmark]
        public void ThreadBased()
        {
            ThreadStart threadStart = new ThreadStart(Method);
            Thread t1 = new Thread(threadStart);

            t1.Start();
            t1.Join();
        }

        private void Method()
        {
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine($"the value is {i} and Thread : {Thread.CurrentThread.ManagedThreadId} ");
                //Thread.Sleep(10);
            }
        }
    }
}

using BenchmarkDotNet.Attributes;

namespace Benchmark.MyBenchmarks
{
    public class SemaphoreVsManualResetEvent
    {
        static ManualResetEvent _mre = new ManualResetEvent(false);
        static Semaphore _event = new Semaphore(5, 5);
        public SemaphoreVsManualResetEvent()
        {
        }

        [Benchmark(Baseline = true)]
        public void Semaphore()
        {
            var threadStart = new ThreadStart(SlaveSem);
            var t1 = new Thread(threadStart);
            var t2 = new Thread(threadStart);
            var t3 = new Thread(threadStart);

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

        [Benchmark]
        public void ManualResetEvent()
        {
            var threadStartMaster = new ThreadStart(MasterMre);
            Thread t1 = new Thread(threadStartMaster);
            t1.Start();

            var threadStartSlave = new ThreadStart(SlaveMre);
            var t2 = new Thread(threadStartSlave);
            var t3 = new Thread(threadStartSlave);
            var t4 = new Thread(threadStartSlave);

            t2.Start();
            t3.Start();
            t4.Start();

            t2.Join();
            t3.Join();
            t4.Join();

        }

        private static void MasterMre() //Manual Reset Event needs master thread
        {
            Console.WriteLine("Write Thread Working");
            _mre.Reset();
            Console.WriteLine("Write Thread Completed");
            _mre.Set();
        }

        private static void SlaveMre()
        {
            Console.WriteLine("Read Thread Wait");
            _mre.WaitOne();
            Console.WriteLine("Read Thread Completed");
        }

        private static void SlaveSem()
        {
            Console.WriteLine("Read Thread Wait");
            _event.WaitOne();
            Console.WriteLine("Read Thread Completed");
            _event.Release();
        }
    }
}

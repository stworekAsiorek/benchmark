using BenchmarkDotNet.Attributes;

namespace Benchmark.MyBenchmarks
{
    public class MutexVsAutoResetEvent
    {
        static AutoResetEvent _are = new AutoResetEvent(true);
        static Mutex _event = new Mutex();
        public MutexVsAutoResetEvent()
        {
        }

        [Benchmark(Baseline = true)]
        public void Mutex()
        {

            var threadStartSlave = new ThreadStart(DoMut);
            var t2 = new Thread(threadStartSlave);
            var t3 = new Thread(threadStartSlave);
            var t1 = new Thread(threadStartSlave);

            t2.Start();
            t3.Start();
            t1.Start();

            t2.Join();
            t3.Join();
            t1.Join();

        }

        [Benchmark]
        public void AutoResetEvent()
        {

            var threadStartSlave = new ThreadStart(DoAre);
            var t2 = new Thread(threadStartSlave);
            var t3 = new Thread(threadStartSlave);
            var t1 = new Thread(threadStartSlave);

            t2.Start();
            t3.Start();
            t1.Start();

            t2.Join();
            t3.Join();
            t1.Join();

        }

        public static void DoAre()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Write Thread Waiting");
            _are.WaitOne();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Write Thread Working");

            //Thread.Sleep(100);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Write Thread Completed");

            _are.Set();
        }

        public static void DoMut()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Write Thread Waiting");
            _event.WaitOne();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Write Thread Working");

            //Thread.Sleep(100);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "Write Thread Completed");

            _event.ReleaseMutex();
        }

    }

}

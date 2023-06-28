// See https://aka.ms/new-console-template for more information
using Benchmark.MyBenchmarks;
using BenchmarkDotNet.Running;
using MyBenchmarks;

Console.WriteLine("Benchmark Project");
//BenchmarkRunner.Run<ParallelVsSynchronous>();
//BenchmarkRunner.Run<LINQParallelVsSynchronous> ();
//BenchmarkRunner.Run<Locks> ();
//BenchmarkRunner.Run<SemaphoreVsManualResetEvent>();
//BenchmarkRunner.Run<MutexVsAutoResetEvent>();
//BenchmarkRunner.Run<TaskVsThread>();
BenchmarkRunner.Run<IEnumerableVsList>();
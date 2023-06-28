using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.MyBenchmarks
{
    public class IEnumerableVsList
    {

        [Benchmark(Baseline = true)]
        public void WithToList()
        {
            IEnumerable<int> list = new List<int> { 2, 1, 2, 13, 19 };
            var filteredList = list.Where(el => el > 10).ToList();
            if (!filteredList.Any())
            {
                return;
            }

            var value = filteredList.First();
        }

        [Benchmark]
        public void WithoutToList()
        {
            IEnumerable<int> list = new List<int> { 2, 1, 2, 13, 19 };
            var filteredList = list.Where(el => el > 10);
            if (!filteredList.Any())
            {
                return;
            }
            var value = filteredList.First();
        }
    }
}

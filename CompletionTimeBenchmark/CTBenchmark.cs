using System;
using System.Linq;

namespace CompletionTimeBenchmark
{
    public static class CTBenchmark
    {
        private const int TEST_ITEARTIONS = 10;
        public static double RunBenchmark(Action workload)
        {
            Console.Write("[");
            var results = new long[TEST_ITEARTIONS];
            for (int i = 0; i < TEST_ITEARTIONS; i++)
            {
                results[i] = STBStopWatch.MeasureInMs(workload);
                Console.Write(" . ");
            }
            Console.WriteLine("]");
            return results.Average();
        }
    }
}

using System;
using System.Diagnostics;

namespace CompletionTimeBenchmark
{
    public static class STBStopWatch
    {
        public static long MeasureInMs(Action workload)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            workload();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}

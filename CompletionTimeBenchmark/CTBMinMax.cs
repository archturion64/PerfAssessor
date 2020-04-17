using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace CompletionTimeBenchmark
{
    public class CTBMinMax
    {
        private static Random rng = new Random();

        public static int[] GenerateArray(uint size)
        {
            int[] array =  new int [size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rng.Next(0, array.Length);
            }
            return array;
        }

        public static double DoBenchmarkMinMaxLinq(uint size)
        {
            return CTBenchmark.RunBenchmark(() => {
                var array = GenerateArray(size);
                var max = array.Max();
                var min = array.Min();
                Console.Write("{0} {1}", min, max);
            });
        }

        public static double DoBenchmarkMinMaxManual(uint size)
        {
            return CTBenchmark.RunBenchmark(() => {
                IEnumerable<int> array = GenerateArray(size);

                var iterator = array.GetEnumerator();
                if (!iterator.MoveNext())
                {
                    throw new ArgumentNullException();
                }

                int min = iterator.Current;
                int max = iterator.Current;
                while (iterator.MoveNext())
                {
                    max = Math.Max(max, iterator.Current);
                    min = Math.Min(min, iterator.Current);
                }
                Console.Write("{0} {1}", min, max);
            });
        }

    }
}

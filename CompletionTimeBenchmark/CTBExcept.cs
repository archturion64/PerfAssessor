using System;
using System.Collections.Generic;
using System.Linq;

namespace CompletionTimeBenchmark
{
    public class CTBExcept
    {
        public static T GenerateCollection<T>(uint size) where T: ICollection<string>, new()
        {
            var retVal =  new T();
            var rng = new Random();
            while (retVal.Count < size)
            {
                retVal.Add($"MiddleClk_{(rng.Next(0, (int)size)).ToString()}");
            }
            return retVal;
        }

        public static T GenerateSortedCollection<T>(uint size) where T: ICollection<string>, new()
        {
            var retVal =  new T();
            for (int i = 0; i< size; i++)
            {
                retVal.Add($"MiddleClk_{i.ToString()}");
            }
            return retVal;
        }

        public static double DoBenchmarkStringListUnsortedLinq(uint size)
        {
            var oldArray = GenerateCollection<List<string>>(size);
            var newArray = GenerateCollection<List<string>>(size/2);
            return CTBenchmark.RunBenchmark(() => {

                Console.Write("{0}", oldArray.Except(newArray).Last());
            });
        }

        public static double DoBenchmarkStringListSortedLinq(uint size)
        {
            var oldArray = GenerateSortedCollection<List<string>>(size);
            var newArray = GenerateSortedCollection<List<string>>(size/2);
            return CTBenchmark.RunBenchmark(() => {

                Console.Write("{0}", oldArray.Except(newArray).Last());
            });
        }

        public static double DoBenchmarkStringHashSetLinq(uint size)
        {
            var oldArray = GenerateCollection<HashSet<string>>(size);
            var newArray = GenerateCollection<HashSet<string>>(size/2);
            return CTBenchmark.RunBenchmark(() => {

                Console.Write("{0}", oldArray.Except(newArray).Last());
            });
        }
    }
}

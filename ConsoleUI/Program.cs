/*
    This code is by no means readable since that was not aim whilst writing it.
    I wanded to see how far would I be able to push the functional programming envelope in C#.
    In the process I wanted to explore some of C# more used containers.
*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CompletionTimeBenchmark;
using System.Threading.Tasks;




namespace ConsoleUI
{
    class Program
    {
        private static uint CONTAINER_SIZE = 1_000_000;

        public delegate void Instruction();
        public delegate double Benchmark(uint size);

        private static bool shouldStop = false;

        private static Dictionary<byte, Tuple <string, Instruction>>  CurrentMenu;

        private static readonly Dictionary<byte, Tuple <string, Instruction>> _MainMenu = new Dictionary<byte, Tuple <string, Instruction>>
        {
            {0x00, new Tuple<string, Instruction>("Exit", () => {shouldStop = true;})},
            {0x01, new Tuple<string, Instruction>("HashSet", () => {CurrentMenu = _HashsetMenu;})},
            {0x02, new Tuple<string, Instruction>("MinMax", () => {CurrentMenu = _MinMaxMenu;})},
            {0x03, new Tuple<string, Instruction>("UnSorted", () => {CurrentMenu = _UnSortedMenu;})}    
        };

        private static readonly Dictionary<byte, Tuple <string, Instruction>> _HashsetMenu = new Dictionary<byte, Tuple <string, Instruction>>
        {
            {0x00, new Tuple<string, Instruction>("Back", () => {CurrentMenu = _MainMenu;})},
            {0x01, new Tuple<string, Instruction>("Hashset<Tuple>", () => {ReportResult("Hashset<Tuple>", CTBHashSet.DoBenchmarkTuple, CONTAINER_SIZE);})},
            {0x02, new Tuple<string, Instruction>("Hashset<ValueTuple>", () => {ReportResult("Hashset<ValueTuple>", CTBHashSet.DoBenchmarkValueTuple, CONTAINER_SIZE);})},

            {0x03, new Tuple<string, Instruction>("Run all async", () => Program.RunAllEntryesAsync(_HashsetMenu).GetAwaiter().GetResult())}
        };

        private static readonly Dictionary<byte, Tuple <string, Instruction>> _MinMaxMenu = new Dictionary<byte, Tuple <string, Instruction>>
        {
            {0x00, new Tuple<string, Instruction>("Back", () => {CurrentMenu = _MainMenu;})},
            {0x01, new Tuple<string, Instruction>("Array Min Max with LINQ", () => {ReportResult("Array Min Max with LINQ", CTBMinMax.DoBenchmarkMinMaxLinq, CONTAINER_SIZE);})},
            {0x02, new Tuple<string, Instruction>("Array Min Max manual", () => {ReportResult("Array Min Max manual", CTBMinMax.DoBenchmarkMinMaxManual, CONTAINER_SIZE);})},

            {0x03, new Tuple<string, Instruction>("Run all async", () => Program.RunAllEntryesAsync(_MinMaxMenu).GetAwaiter().GetResult())}
        };

        private static readonly Dictionary<byte, Tuple <string, Instruction>> _UnSortedMenu = new Dictionary<byte, Tuple <string, Instruction>>
        {
            {0x00, new Tuple<string, Instruction>("Back", () => {CurrentMenu = _MainMenu;})},
            {0x01, new Tuple<string, Instruction>("Except String List with LINQ", () => {ReportResult("Except String List with LINQ", CTBExcept.DoBenchmarkStringListUnsortedLinq, CONTAINER_SIZE);})},
            {0x02, new Tuple<string, Instruction>("Except String Set with LINQ", () => {ReportResult("Except String Set with LINQ", CTBExcept.DoBenchmarkStringListSortedLinq, CONTAINER_SIZE);})},
            {0x03, new Tuple<string, Instruction>("Run all async", () => Program.RunAllEntryesAsync(_UnSortedMenu).GetAwaiter().GetResult())}
        };


        static void ReportResult(in string testName, Benchmark test, uint containerSize)
        {
            Console.WriteLine($"Bench {testName} complete in: {test(containerSize)} ms");
        }

        static async Task RunAllEntryesAsync(Dictionary<byte, Tuple <string, Instruction>> entries)
        {
            var tasks = new List<Task>();
            foreach (var item in entries.Skip(1).SkipLast(1)) // first and last entries are not regular tests
            {
                tasks.Add(Task.Run(() => item.Value.Item2()));
            }

            await Task.WhenAll(tasks);
        }
    
        static StringBuilder generateMenu(in Dictionary<byte, Tuple <string, Instruction>> entries)
        {
            StringBuilder sb = new StringBuilder(Environment.NewLine + 
                                                "Enter a number for your choice of action: ");
            sb.Append(Environment.NewLine);
            foreach( var e in entries)
            {
                sb.Append(e.Key.ToString() +  ": " + e.Value.Item1 + Environment.NewLine);
            }

            return sb;
        }

        static void ReactOnInput(Dictionary<byte, Tuple <string, Instruction>> dict)
        {
            try
            {
                dict.ElementAt(Convert.ToByte(Console.ReadLine())).Value.Item2();
            } catch (Exception){
                Console.WriteLine("Invalid input");
            }
        }
        

        static void Main(string[] args)
        {
            CurrentMenu = _MainMenu;
            while(shouldStop == false)
            {
                Console.WriteLine("{0}",generateMenu(CurrentMenu));
                ReactOnInput(CurrentMenu);
            }
        }
    }
}

/*
    This code is by no means readable since that was not aim whilst writing it.
    I wanded to see how far would I be able to push the functional programming envelope in C#.
    In the process I wanted to explore some of C# more used containers.
*/

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using CompletionTimeBenchmark;




namespace ConsoleUI
{
    class Program
    {
        public delegate void Instruction();
        public delegate double Benchmark(uint size);

        private static bool shouldStop = false;

        private static Dictionary<byte, Tuple <string, Instruction>>  CurrentMenu;

        private static readonly Dictionary<byte, Tuple <string, Instruction>> _MainMenu = new Dictionary<byte, Tuple <string, Instruction>>
        {
            {0x00, new Tuple<string, Instruction>("Exit", () => {shouldStop = true;})},
            {0x01, new Tuple<string, Instruction>("HashSet", () => {CurrentMenu = _HashsetMenu;})}
        };

        private static readonly Dictionary<byte, Tuple <string, Instruction>> _HashsetMenu = new Dictionary<byte, Tuple <string, Instruction>>
        {
            {0x00, new Tuple<string, Instruction>("Back", () => {CurrentMenu = _MainMenu;})},
            {0x01, new Tuple<string, Instruction>("Hashset<Tuple>", () => {Console.WriteLine($"Bench complete in: {CTBHashSet.DoBenchmarkTuple(1000000)} ms");})}
        };
    
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

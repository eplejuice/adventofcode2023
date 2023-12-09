using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Day9
{
    public static class Day9
    {
        public static void Run()
        {
            int total = 0;

            foreach (int[] node in ParseInput())
            {
                total += Extrapolate(node);
            }

            Console.WriteLine(total);
        }

        public static void RunPartTwo()
        {
            int total = 0;

            foreach (int[] node in ParseInput())
            {
                total += Extrapolate(node.Reverse().ToArray());
            }

            Console.WriteLine(total);
        }


        internal static int[][] ParseInput()
        {
            return File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day9\\input.txt"))
                .ToList().Select(line => line
                .Split(" ")
                .Where(x => int.TryParse(x.ToString(), out _))
                .Select(x => int.Parse(x.ToString())).ToArray()
            ).ToArray();
        }

        internal static int Extrapolate(int[] node)
        {
            List<int[]> list = new() { node };
            var currentArray = node;

            while (!currentArray.All(x => x == 0))
            {
                var currentSubArray = currentArray
                    .Zip(currentArray.Skip(1))
                    .Select(x => x.Second - x.First)
                    .ToArray();

                list.Add(currentSubArray);
                currentArray = currentSubArray;
            }

            list.Reverse();

            int currentLast = list.First().Last();
            int previousLast = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                previousLast = currentLast;
                currentLast = previousLast + list[i + 1].Last();
            }

            return currentLast;
        }


    }
}

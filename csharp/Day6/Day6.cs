using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Day6
{
    public static class Day6
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day6\\input.txt"));
            Dictionary<int, int> Races = new Dictionary<int, int>();
            int NumerOfWays = 0;

            var times = lines[0].Split(' ').Where(x => int.TryParse(x, out _)).ToArray();
            var distances = lines[1].Split(' ').Where(x => int.TryParse(x, out _)).ToArray();

            if (times.Length == distances.Length)
            {
                for (int i = 0; i < times.Length; i++)
                {
                    Races.Add(int.Parse(times[i].ToString()), int.Parse(distances[i].ToString()));
                }
            }

            foreach (var race in Races)
            {
                int counter = 0;
                int totalTime = race.Key;
                int recordDistance = race.Value;

                for (int second = 0; second <= totalTime; second++)
                {
                    if (((totalTime - second) * second) > recordDistance)
                    {
                        counter++;
                    }
                }

                if (NumerOfWays is 0)
                {
                    NumerOfWays = counter;
                }
                else
                {
                    NumerOfWays *= counter;
                }
            }
            Console.WriteLine(NumerOfWays);
        }

        public static void RunPartTwo()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day6\\input.txt"));
            double time = double.Parse(string.Join("", lines[0].Where(char.IsDigit).ToArray()));
            double distance = double.Parse(string.Join("", lines[1].Where(char.IsDigit).ToArray()));

            int counter = 0;

            for (double second = 0; second < time; second++)
            {
                if (((time - second) * second) > distance)
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }
    }
}

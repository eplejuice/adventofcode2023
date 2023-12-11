using System;
using System.Linq;

namespace csharp.Day4
{
    public static class Day4
    {
        public static void Run()
        {
            int totalPoints = 0;
            int[][][] inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day4\\input.txt"))
                            .ToList().Select(line => new int[2][]
                            {
                              line.Split("|")[0].Split(":")[1].Split(" ").Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x.ToString())).ToArray(),
                              line.Split("|")[1].Split(" ").Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x.ToString())).ToArray()
                            }
                            ).ToArray();



            for (int i = 0; i < inputs.Length; i++)
            {
                int worth = 0;
                var winningNumbers = inputs[i][1].Where(x => inputs[i][0].Contains(x)).ToArray();

                for (int j = 0; j < winningNumbers.Length; j++)
                {
                    worth = worth == 0 ? 1 : worth * 2; 
                }
                totalPoints += worth;
            }

           Console.WriteLine(totalPoints);
        }

        public static void RunPArtTwo()
        {
            int[][][] inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day4\\input.txt"))
                            .ToList().Select(line => new int[2][]
                            {
                              line.Split("|")[0].Split(":")[1].Split(" ").Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x.ToString())).ToArray(),
                              line.Split("|")[1].Split(" ").Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x.ToString())).ToArray()
                            }
                            ).ToArray();



            int[] IndexWithAmount = new int[inputs.Length];
            for (int i = 0; i < IndexWithAmount.Length; i++)
            {
                IndexWithAmount[i] = 1;
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                var winningNumbers = inputs[i][1].Where(x => inputs[i][0].Contains(x)).ToArray();

                for (int j = 0; j < IndexWithAmount[i]; j++)
                {
                    for (int k = winningNumbers.Length; k > 0; k--)
                    {
                        IndexWithAmount[i+k] += 1;
                    }
                }
            }

            Console.WriteLine(IndexWithAmount.Sum());
        }
    }
}

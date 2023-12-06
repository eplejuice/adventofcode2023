using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Day1
{
    public static class Day1
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day1\\input.txt"));
            List<char> digitsInString = new();
            List<int> results = new();

            foreach (string line in lines)
            {
                foreach (char item in line)
                {
                    if (char.IsDigit(item))
                    {
                        digitsInString.Add(item);
                    }
                }
                int output = int.Parse(new char[] { digitsInString.First(), digitsInString.Last() });
                results.Add(output);
                digitsInString.Clear();
            }
            Console.WriteLine(results.Sum());
        }

        public static void RunPartTwo()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day1\\input.txt"));
            List<int> results = new();

            Dictionary<string, int> validDigits = new()
            {
                {"one",     1},
                {"two",     2},
                {"three",   3},
                {"four",    4},
                {"five",    5},
                {"six",     6},
                {"seven",   7},
                {"eight",   8},
                {"nine",    9}
            };


            foreach (string line in lines)
            {
                Dictionary<int, int> Matches = new();

                foreach (var validDigit in validDigits.Keys)
                {
                    if (line.IndexOf(validDigit) != -1)
                    {
                        Matches.Add(line.IndexOf(validDigit), validDigits[validDigit]);
                    }

                    if (line.LastIndexOf(validDigit) != -1)
                    {
                        if (!Matches.ContainsKey(line.LastIndexOf(validDigit)))
                        {
                            Matches.Add(line.LastIndexOf(validDigit), validDigits[validDigit]);
                        }
                    }
                }
                foreach (char item in line)
                {
                    if (char.IsDigit(item))
                    {
                        if (!Matches.ContainsKey(line.IndexOf(item)))
                        {
                            Matches.Add(line.IndexOf(item), int.Parse(new char[] { item }));
                        }
                        if (!Matches.ContainsKey(line.LastIndexOf(item)))
                        {
                            Matches.Add(line.LastIndexOf(item), int.Parse(new char[] { item }));
                        }

                    }
                }

                var list = Matches.OrderBy(x => x.Key).ToList();
                var currentFirstDigit = list.First().Value;
                var currentLastDigit = list.Last().Value;
                int output = int.Parse(new char[] { char.Parse(currentFirstDigit.ToString()), char.Parse(currentLastDigit.ToString()) });
                results.Add(output);
                Console.WriteLine(results.Sum());
            }
        }
    }
}

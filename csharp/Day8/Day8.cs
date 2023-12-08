using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace csharp.Day8
{
    public static class Day8
    {
        public static void Run()
        {
            double steps = 0;
            int instructionIndex = 0;
            string goal = "ZZZ";
            string start = "AAA";

            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day8\\input.txt")).ToArray();

            char[] instructions = lines[0].ToCharArray();

            string[][] nodes = lines.ToList().Skip(2).Select(line =>
            new string[3]
            {
                line.Split(' ')[0],
                line.Split("(")[1].Split(",")[0],
                line.Split(",")[1].TrimStart().TrimEnd(')')
            }
            ).ToArray();

            string[] currentNode = nodes.FirstOrDefault(node => node[0] == start);

            while (currentNode?[0] != goal)
            {
                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }

                string[]? nextNode = (instructions[instructionIndex] == 'R') ? nodes.FirstOrDefault(n => n[0] == currentNode?[2]) : nodes.FirstOrDefault(n => n[0] == currentNode?[1]);
                currentNode = nextNode;
                instructionIndex++;
                steps++;
            }

            Console.WriteLine(steps);
        }

        public static void RunPartTwo()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day8\\input.txt")).ToArray();

            char[] instructions = lines[0].ToCharArray();

            string[][] nodes = lines.ToList().Skip(2).Select(line =>
            new string[3]
            {
                line.Split(' ')[0],
                line.Split("(")[1].Split(",")[0],
                line.Split(",")[1].TrimStart().TrimEnd(')')
            }
            ).ToArray();

            var startNodes = nodes.Where(node => new Regex("[A-Za-z0-9][A-Za-z0-9]A", RegexOptions.IgnoreCase).IsMatch(node[0])).ToArray();

            double[] cycles = new double[startNodes.Length];


            for (int i = 0; i < startNodes.Length; i++)
            {
                string[] currentNode = startNodes[i];
                double steps = 0;
                int instructionIndex = 0;

                while (!new Regex("[A-Za-z0-9][A-Za-z0-9]Z", RegexOptions.IgnoreCase).IsMatch(currentNode[0]))
                {
                    if (instructionIndex >= instructions.Length)
                    {
                        instructionIndex = 0;
                    }
                    string[]? nextNode = (instructions[instructionIndex] == 'R') ? nodes.FirstOrDefault(n => n[0] == currentNode?[2]) : nodes.FirstOrDefault(n => n[0] == currentNode?[1]);
                    currentNode = nextNode;
                    instructionIndex++;
                    steps++;
                }

                cycles[i] = steps;

            }

            foreach (var node in cycles)
            {
                Console.WriteLine(node);
            }

            double result = 1;
            for (int i = 0; i < cycles.Length; i++)
            {
                result = lcm(result, cycles[i]);
            }

            Console.WriteLine("LCM = {0}", result);


            static double gcd(double a, double b)
            {

                while (b != 0)
                {
                    double gcd = b;
                    b = a % b;
                    a = gcd;
                }
                return a;
            }

            static double lcm(double a, double b)
            {
                return (a / gcd(a, b)) * b;
            }    
        }
    }
}

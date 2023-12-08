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

            //Console.WriteLine(Traverse(steps, instructions, instructionIndex, nodes.FirstOrDefault(node => node[0] == start), nodes, goal));
        }


        public static void RunPartTwo()
        {
            double steps = 0;
            int instructionIndex = 0;

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

            while (!startNodes.All(node => new Regex("[A-Za-z0-9][A-Za-z0-9]Z", RegexOptions.IgnoreCase).IsMatch(node[0])))
            {
                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }

                if (instructions[instructionIndex] == 'R')
                {
                    for (int i = 0; i < startNodes.Length; i++)
                    {
                        startNodes[i] = nodes.FirstOrDefault(node => startNodes[i][2] == node[0]).ToArray();
                    }
                }
                else if (instructions[instructionIndex] == 'L')
                {
                    for (int i = 0; i < startNodes.Length; i++)
                    {
                        startNodes[i] = nodes.FirstOrDefault(node => startNodes[i][1] == node[0]).ToArray();
                    }
                }

                instructionIndex++;
                steps++;
            }

            Console.WriteLine(steps);
        }



        //static double Traverse(double steps, char[] instructions, int instructionIndex, string[]? currentNode, string[][] nodes, string goal)
        //{
        //    if (currentNode?[1] == goal || currentNode?[2] == goal)
        //    {
        //        var currentInstruction = instructions[instructionIndex];
        //    }
        //    if (currentNode?[0] == goal)
        //    {
        //        return steps;
        //    }
        //    if (instructionIndex >= instructions.Length)
        //    {
        //        instructionIndex = 0;
        //    }

        //    string[]? nextNode = (instructions[instructionIndex] == 'R') ? nodes.FirstOrDefault(n => n[0] == currentNode?[2]) : nodes.FirstOrDefault(n => n[0] == currentNode?[1]);
        //    return Traverse(++steps, instructions, ++instructionIndex, nextNode, nodes, goal);
        //}
    }
}

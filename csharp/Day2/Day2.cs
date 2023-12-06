using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace csharp.Day2
{
    public static class Day2
    {
        public static void Run()
        {

            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day2\\input.txt"));
            int LoadedRedCubes = 12;
            int LoadedGreenCubes = 13;
            int LoadedBlueCubes = 14;

            List<Game> Games = GetGames(lines);

            int sum = Games.Where(game => game.GreenCubes <= LoadedGreenCubes && game.RedCubes <= LoadedRedCubes && game.BlueCubes <= LoadedBlueCubes).Select(game => game.ID).Sum();

            Console.WriteLine(sum.ToString());
        }

        public static void RunPartTwo()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day2\\input.txt"));

            List<Game> Games = GetGames(lines);

            int result = 0;

            foreach (Game game in Games)
            {
                int power = game.BlueCubes * game.GreenCubes * game.RedCubes;
                result += power;
            }

            Console.WriteLine(result);
        }

        internal record Game(int ID, int BlueCubes, int GreenCubes, int RedCubes);

        internal static List<Game> GetGames(string[] lines)
        {
            List<Game> Games = new();

            foreach (string line in lines)
            {
                int Id = int.Parse(line.Split(':')[0].Split(' ').Last());
                int maxBlue = 0;
                int maxRed = 0;
                int maxGreen = 0;

                string[] lists = line.Split(':')[1].Split(';');

                foreach (string list in lists)
                {
                    string[] sublist = list.Split(',');

                    foreach (string listing in sublist)
                    {
                        int amount = int.Parse(string.Join("", listing.ToCharArray().Where(char.IsDigit)));

                        string[] listingProps = listing.Split(' ');

                        if (listing.Contains("green"))
                        {
                            if (amount > maxGreen)
                            {
                                maxGreen = amount;
                            }
                        }
                        else if (listing.Contains("blue"))
                        {
                            if (amount > maxBlue)
                            {
                                maxBlue = amount;
                            }
                        }
                        else if (listing.Contains("red"))
                        {
                            if (amount > maxRed)
                            {
                                maxRed = amount;
                            }
                        }
                    }
                }
                Games.Add(new Game(Id, maxBlue, maxGreen, maxRed));
            }
            return Games;
        }
    }
}

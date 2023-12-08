using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static csharp.Day7.Day7;

namespace csharp.Day7
{
    public static class Day7
    {
        public static void Run()
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day7\\input.txt")).ToList();

            string[] hands = lines.Select(x => x.Split(" ")[0]).ToArray();
            int[] bids = lines.Select(x => int.Parse(x.Split(" ")[1])).ToArray();

            if (bids.Length == hands.Length)
            {
                List<Hand> handsWithRank = new();

                for (int i = 0; i < hands.Length; i++)
                {
                    handsWithRank.Add(GetHandWithRank(hands[i], bids[i]));
                }

                var orderedResult = handsWithRank
                    .OrderByDescending(hand => hand.HandRank)
                    .ThenBy(hand => hand.Cards, new SelectBestCard())
                    .ToArray();


                int totalSum = 0;
                for (int i = 1; i <= orderedResult.Length; i++)
                {
                    totalSum += (i * orderedResult[i-1].Bid);
                }

                Console.WriteLine(totalSum);
            }
        }

        internal class SelectBestCard : IComparer<List<int>>
        {
            public int Compare(List<int>? x, List<int>? y)
            {
                for (int i = 0; i < x?.Count; ++i)
                {
                    if (x[i] < y?[i]) return -1;
                    if (x[i] > y?[i]) return 1;
                }
                return 0;
            }
        }


        private static Hand GetHandWithRank(string hand, int bid)
        {
            List<int> cards = new();

            foreach (char c in hand)
            {
                cards.Add((int)CharToCardRank(c));
            }

            return new Hand(cards, GetHandRank(cards), bid, hand);
        }

        internal static HandRanking GetHandRank(List<int> cards)
        {
            if (IsFiveOfAKind(cards)) return HandRanking.FiveOfAKind;
            if (IsFourOfAKind(cards)) return HandRanking.FourOfAKind;
            if (IsFullHouse(cards)) return HandRanking.FullHouse;
            if (IsThreeOfAKind(cards)) return HandRanking.ThreeOfAKind;
            if (IsTwoPair(cards)) return HandRanking.TwoPair;
            if (IsOnePair(cards)) return HandRanking.OnePair;
            return HandRanking.HighCard;
        }
        internal static CardRank CharToCardRank(char input)
        {
            return input switch
            {
                'A' => CardRank.A,
                'K' => CardRank.K,
                'Q' => CardRank.Q,
                'J' => CardRank.J,
                'T' => CardRank.T,
                '9' => CardRank.Nine,
                '8' => CardRank.Eight,
                '7' => CardRank.Seven,
                '6' => CardRank.Six,
                '5' => CardRank.Five,
                '4' => CardRank.Four,
                '3' => CardRank.Three,
                '2' => CardRank.Two,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        internal enum HandRanking
        {
            FiveOfAKind,
            FourOfAKind,
            FullHouse,
            ThreeOfAKind,
            TwoPair,
            OnePair,
            HighCard
        }

        internal enum CardRank
        {
            A = 14,
            K = 13,
            Q = 12,
            J = 11,
            T = 10,
            Nine = 9,
            Eight = 8,
            Seven = 7,
            Six = 6,
            Five = 5,
            Four = 4,
            Three = 3,
            Two = 2,
        }

        internal static bool IsFiveOfAKind(List<int> hand)
        {
            return hand.GroupBy(card => card).Any(group => group.Count() == 5);
        }
        internal static bool IsFourOfAKind(List<int> hand)
        {
            return hand.GroupBy(card => card).Any(group => group.Count() == 4);
        }
        internal static bool IsFullHouse(List<int> hand)
        {
            return hand.GroupBy(card => card).Any(group => group.Count() == 3)
                && hand.GroupBy(card => card).Any(group => group.Count() == 2);
        }
        internal static bool IsThreeOfAKind(List<int> hand)
        {
            return hand.GroupBy(card => card).Any(group => group.Count() == 3);
        }
        internal static bool IsTwoPair(List<int> hand)
        {
            return hand.GroupBy(card => card).Count(group => group.Count() == 2) == 2;
        }
        internal static bool IsOnePair(List<int> hand)
        {
            return hand.GroupBy(card => card).Any(group => group.Count() == 2);
        }

        internal record Hand(List<int> Cards, HandRanking HandRank, int Bid, string OriginalInput);
    }
}

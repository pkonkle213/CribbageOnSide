using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cribbage2021.Classes
{
    public class AIPoints
    {
        public int TestFifteen(List<Card> testHand, int points)
        {
            const int sumNeeded = 15;
            const int fifteenSuccess = 2;
            int sum = 0;
            foreach (Card card in testHand)
            {
                sum += card.AddingValue;
            }

            if (sum == sumNeeded)
            {
                points += fifteenSuccess;
            }

            return points;
        }

        public int Fifteens(List<Card> cards, int points)
        {
            //Checking each combination of two cards
            for (int cardA = 0; cardA < cards.Count; cardA++)
            {
                for (int cardB = cardA + 1; cardB < cards.Count; cardB++)
                {
                    List<Card> testHand = new List<Card>();
                    testHand.Add(cards[cardA]);
                    testHand.Add(cards[cardB]);
                    points = TestFifteen(testHand, points);
                }
            }

            //Checking each combination of three cards
            for (int cardA = 0; cardA < cards.Count; cardA++)
            {
                for (int cardB = cardA + 1; cardB < cards.Count; cardB++)
                {
                    for (int cardC = cardB + 1; cardC < cards.Count; cardC++)
                    {
                        List<Card> testHand = new List<Card>();
                        testHand.Add(cards[cardA]);
                        testHand.Add(cards[cardB]);
                        testHand.Add(cards[cardC]);
                        points = TestFifteen(testHand, points);
                    }
                }
            }

            //Checking each combination of four cards
            for (int cardA = 0; cardA < cards.Count; cardA++)
            {
                for (int cardB = cardA + 1; cardB < cards.Count; cardB++)
                {
                    for (int cardC = cardB + 1; cardC < cards.Count; cardC++)
                    {
                        for (int cardD = cardC + 1; cardD < cards.Count; cardD++)
                        {
                            List<Card> testHand = new List<Card>();
                            testHand.Add(cards[cardA]);
                            testHand.Add(cards[cardB]);
                            testHand.Add(cards[cardC]);
                            testHand.Add(cards[cardD]);
                            points = TestFifteen(testHand, points);
                        }
                    }
                }
            }

            //Checking the five cards
            if (cards.Count == 5)
            {
                points = TestFifteen(cards, points);
            }

            return points;
        }

        public int Pairs(List<Card> cards, int points)
        {
            const int pairSuccess = 2;
            for (int card1 = 0; card1 < cards.Count; card1++)
            {
                for (int card2 = card1 + 1; card2 < cards.Count; card2++)
                {
                    if (cards[card1].NameOfCard == cards[card2].NameOfCard)
                    {
                        points += pairSuccess;
                    }
                }
            }

            return points;
        }

        //Need to figure out a way to skip indexes. Multiple runs (5,6,7,8,8) not working
        public int Runs(List<Card> cards, int points)
        {
            cards = cards.OrderBy(x => x.NumberValue)
                .ThenBy(x => x.SuitOfCard)
                .ToList();

            bool runs = TestRun(cards);

            if (runs)
            {
                points += 5;
            }
            else
            {
                for (int skip = 0; skip < cards.Count; skip++)
                {
                    List<Card> testHand = new List<Card>();
                    for (int i = 0; i < cards.Count; i++)
                    {
                        if (skip != i)
                        {
                            testHand.Add(cards[i]);
                        }
                    }

                    if (TestRun(testHand))
                    {
                        points += 4;
                        runs = true;
                    }
                }

                if (!runs)
                {
                    for (int skip1 = 0; skip1 < cards.Count; skip1++)
                    {
                        for (int skip2 = skip1 + 1; skip2 < cards.Count; skip2++)
                        {
                            List<Card> testHand = new List<Card>();
                            for (int i = 0; i < cards.Count; i++)
                            {
                                if (i != skip1 && i != skip2)
                                {
                                    testHand.Add(cards[i]);
                                }
                            }

                            if (TestRun(testHand))
                            {
                                points += 3;
                            }
                        }
                    }
                }
            }

            return points;
        }

        public bool TestRun(List<Card> cards)
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].NumberValue != cards[i + 1].NumberValue - 1)
                {
                    return false;
                }
            }

            return true;
        }

        public int Flush(List<Card> cards, Card starter, int points)
        {
            bool flush = true;
            for (int i = 0; i < cards.Count - 2; i++)
            {
                if (cards[i].SuitOfCard != cards[i + 1].SuitOfCard)
                {
                    flush = false;
                }
            }

            if (flush)
            {
                if (cards[0].SuitOfCard == starter.SuitOfCard)
                {
                    points += 5;
                }
                else
                {
                    points += 4;
                }
            }

            return points;
        }

        public int Knobs(List<Card> cards, Card starter, int points)
        {
            foreach (Card card in cards)
            {
                if (card.NameOfCard == "Jack" && card.SuitOfCard == starter.SuitOfCard)
                {
                    points++;
                }
            }
            return points;
        }
    }
}

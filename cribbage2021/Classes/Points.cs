using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cribbage2021.Classes
{
    public class Points
    {
        Output output = new Output();

        public int Heels(Card starter, int tempPoints, bool outWrite)
        {
            const int heelsSuccess = 2;
            const string category = "Heels";
            if (starter.NameOfCard == "Jack")
            {
                tempPoints += heelsSuccess;
                if (outWrite)
                {
                    output.Score(category, tempPoints);
                }
            }

            return tempPoints;
        }

        public int TestFifteen(List<Card> testHand, int points, bool outWrite)
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
                if (outWrite)
                {
                    output.Score("15", points);
                }
            }

            return points;
        }

        public int Fifteens(List<Card> cards, int points, bool outWrite)
        {
            //Checking each combination of two cards
            for (int cardA = 0; cardA < cards.Count; cardA++)
            {
                for (int cardB = cardA + 1; cardB < cards.Count; cardB++)
                {
                    List<Card> testHand = new List<Card>();
                    testHand.Add(cards[cardA]);
                    testHand.Add(cards[cardB]);
                    points = TestFifteen(testHand, points, outWrite);
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
                        points = TestFifteen(testHand, points, outWrite);
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
                            points = TestFifteen(testHand, points, outWrite);
                        }
                    }
                }
            }

            //Checking the five cards
            if (cards.Count == 5)
            {
                points = TestFifteen(cards, points, outWrite);
            }

            return points;
        }

        public int Pairs(List<Card> cards, int points, bool outWrite)
        {
            const int pairSuccess = 2;
            for (int card1 = 0; card1 < cards.Count; card1++)
            {
                for (int card2 = card1 + 1; card2 < cards.Count; card2++)
                {
                    if (cards[card1].NameOfCard == cards[card2].NameOfCard)
                    {
                        points += pairSuccess;
                        if (outWrite)
                        {
                            output.Score("Pair", points);
                        }
                    }
                }
            }

            return points;
        }

        public int Runs(List<Card> cards, int points, bool outWrite)
        {
            cards = cards.OrderBy(x => x.NumberValue)
                .ThenBy(x => x.SuitOfCard)
                .ToList();

            bool runs = false;

            if (TestRun(cards))
            {
                points += cards.Count;
                if (outWrite)
                {
                    output.Score("Run", points);
                }
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
                        points += cards.Count - 1;
                        if (outWrite)
                        {
                            output.Score("Run", points);
                        }
                        runs = true;
                    }
                }

                if (!runs && cards.Count - 2 >= 3)
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
                                if (outWrite)
                                {
                                    output.Score("Run", points);
                                }
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

        public int Flush(List<Card> cards, Card starter, int points, bool crib, bool outWrite)
        {
            bool fourFlush = true;
            for (int i = 0; i < cards.Count - 2; i++)
            {
                if (cards[i].SuitOfCard != cards[i + 1].SuitOfCard)
                {
                    fourFlush = false;
                }
            }

            if (fourFlush)
            {
                if (cards[0].SuitOfCard == starter.SuitOfCard)
                {
                    points += 5;
                    if (outWrite)
                    {
                        output.Score("Flush", points);
                    }
                }
                else if (!crib)
                {
                    points += 4;
                    if (outWrite)
                    {
                        output.Score("Flush", points);
                    }
                }
            }

            return points;
        }

        public int Knobs(List<Card> cards, Card starter, int points, bool outWrite)
        {
            foreach (Card card in cards)
            {
                if (card.NameOfCard == "Jack" && card.SuitOfCard == starter.SuitOfCard)
                {
                    points++;
                    if (outWrite)
                    {
                        output.Score("Knobs", points);
                    }
                }
            }
            return points;
        }
    }
}

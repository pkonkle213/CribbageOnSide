using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class Points
    {
        Output output = new Output();

        /*There should be multiple calls for methods
         * 15s for multiple combinations (2,3,4,5)
         * Runs (2,3,4,5)
         * Flushes (4,5)
         * 
         * (hand,starter)
         */

        public int Heels(Card starter, int tempPoints)
        {
            const int heelsSuccess = 2;
            const string category = "Heels";
            if (starter.NameOfCard == "Jack")
            {
                tempPoints += heelsSuccess;
                output.Score(category, tempPoints);
            }

            return tempPoints;
        }

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
                output.Score("15", points);
            }

            return points;
        }

        public int Fifteens(List<Card> cards, Card starter, int points)
        {
            cards.Add(starter);

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
                            points = TestFifteen(testHand, points);
                        }
                    }
                }
            }

            //Checking the five cards
            points = TestFifteen(cards, points);

            return points;
        }

        public int Pairs(List<Card> cards, Card starter, int points)
        {
            const int pairSuccess = 2;
            cards.Add(starter);
            for (int card1 = 0; card1 < cards.Count; card1++)
            {
                for (int card2 = card1 + 1; card2 < cards.Count; card2++)
                {
                    if (cards[card1].NameOfCard == cards[card2].NameOfCard)
                    {
                        points += pairSuccess;
                        output.Score("Pair", points);
                    }

                }
            }

            return points;
        }

        public bool Run(List<Card> cards)
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].NumberValue != cards[i + 1].NumberValue)
                {
                    return false;
                }
            }

            return true;
        }

        public bool Flush(List<Card> cards)
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].SuitOfCard != cards[i + 1].SuitOfCard)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

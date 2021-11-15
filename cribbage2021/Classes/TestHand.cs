using cribbage2021.AI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cribbage2021.Classes
{
    public class TestHand
    {
        CountingPoints points = new CountingPoints();

        public void ShowHands(List<Card> hand, List<Card> deck)
        {
            int maxPointsStarter = 0;
            int maxPoints = 0;
            List<int> handIndexStarter = new List<int>();
            List<int> handIndex = new List<int>();
            Console.WriteLine();
            Console.WriteLine();

            for (int a = 0; a < hand.Count; a++)
            {
                for (int b = a + 1; b < hand.Count; b++)
                {
                    for (int c = b + 1; c < hand.Count; c++)
                    {
                        for (int d = c + 1; d < hand.Count; d++)
                        {
                            List<Card> testHand = new List<Card>();
                            testHand.Add(hand[a]);
                            testHand.Add(hand[b]);
                            testHand.Add(hand[c]);
                            testHand.Add(hand[d]);

                            Card starter = new Card(0, "None");
                            Console.WriteLine($"{a} {b} {c} {d} {points.CountingCards(testHand, starter, false, true)}");
                            int thesePoints = points.CountingCards(testHand, starter, false, false);
                            if (thesePoints > maxPoints)
                            {
                                maxPoints = thesePoints;
                                handIndex = new List<int> { a, b, c, d };
                            }

                            foreach (Card card in deck)
                            {
                                int thesePointsStart = points.CountingCards(testHand, card, false, false);
                                if (thesePointsStart > maxPoints)
                                {
                                    maxPointsStarter = thesePointsStart;
                                    handIndexStarter = new List<int> { a, b, c, d };
                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine($"For {maxPoints}, keep: ");
            foreach (int num in handIndex)
            {
                Console.WriteLine($"{hand[num].NameOfCard} {hand[num].SuitOfCard}");
            }

            Console.WriteLine();
            Console.WriteLine($"For a possible {maxPointsStarter}, keep: ");
            foreach (int num in handIndexStarter)
            {
                Console.WriteLine($"{hand[num].NameOfCard} {hand[num].SuitOfCard}");
            }
        }
    }
}

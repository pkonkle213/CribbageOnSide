using cribbage2021.AI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cribbage2021.Classes
{
    public class BuildingAI
    {
        //private CreateRecord record = new CreateRecord();

        public void mathTime()
        {
            //This is only because I'm a monster and I like pain
            Deck deck = new Deck();
            List<Card> fullDeck = deck.CreateDeck();
            List<int> scores = new List<int>();
            AICount count = new AICount();


            for (int starterIndex = 0; starterIndex < fullDeck.Count; starterIndex++)
            {
                for (int cardA = 0; cardA < fullDeck.Count; cardA++)
                {
                    if (cardA != starterIndex)
                    {
                        for (int cardB = cardA + 1; cardB < fullDeck.Count; cardB++)
                        {
                            if (cardB != starterIndex)
                            {
                                for (int cardC = cardB + 1; cardC < fullDeck.Count; cardC++)
                                {
                                    if (cardC != starterIndex)
                                    {
                                        for (int cardD = cardC + 1; cardD < fullDeck.Count; cardD++)
                                        {
                                            if (cardD != starterIndex)
                                            {
                                                List<Card> hand = new List<Card>();
                                                hand.Add(fullDeck[cardA]);
                                                hand.Add(fullDeck[cardB]);
                                                hand.Add(fullDeck[cardC]);
                                                hand.Add(fullDeck[cardD]);

                                                int handScore = count.CountingCards(hand, fullDeck[starterIndex]);
                                                // Console.WriteLine(cardA.ToString().PadRight(3) + cardB.ToString().PadRight(3) + cardC.ToString().PadRight(3) + cardD.ToString().PadRight(3) + starterIndex.ToString().PadRight(3) + handScore);
                                                // record.Record(fullDeck[cardA], fullDeck[cardB], fullDeck[cardC], fullDeck[cardD], fullDeck[starterIndex], handScore);
                                                scores.Add(handScore);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int[] quantity = new int[30];
            foreach (int score in scores)
            {
                quantity[score]++;
            }

            for (int i = 0; i < quantity.Length; i++)
            {
                Console.WriteLine($"{i}: {quantity[i]}");
            }
        }

        public void OddsOfHand()
        {
            List<Card> hand = new List<Card>();
            Console.Clear();
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    bool pass = false;
                    while (!pass)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"For Card {i + 1}: ");
                        Console.Write("Number Value: ");
                        string answerNum = Console.ReadLine();
                        Console.Write("Suit: ");
                        string answerSuit = Console.ReadLine();
                        if (answerSuit == "Clubs" || answerSuit == "Diamonds" || answerSuit == "Hearts" || answerSuit == "Spades")
                        {
                            Card card = new Card(int.Parse(answerNum), answerSuit);
                            hand.Add(card);
                            pass = true;
                        }
                        else
                        {
                            Console.WriteLine("A valid suit is needed");
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please try a valid card number (Ace = 1, Jack = 11, etc");
                }
            }

            //Select two cards to discard, then test the hand and all starters for possible point values
            for (int cardA = 0; cardA < hand.Count; cardA++)
            {
                for (int cardB = cardA + 1; cardB < hand.Count; cardB++)
                {
                    for(int cardC = cardB + 1; cardC < hand.Count; cardC++)
                    {
                        for (int cardD = cardC + 1; cardD < hand.Count; cardD++)
                        {
                            List<Card> sample = new List<Card>();
                            sample.Add(hand[cardA]);
                            sample.Add(hand[cardB]);
                            sample.Add(hand[cardC]);
                            sample.Add(hand[cardD]);

                            List<int> scores = TestHand(sample);
                            Console.WriteLine(scores.Max());
                        }
                    }
                }
            }
        }

        public List<int> TestHand(List<Card> hand)
        {
            Deck deck = new Deck();
            List<Card> fullDeck = deck.CreateDeck();
            List<int> scores = new List<int>();
            AICount count = new AICount();

            foreach (Card card in fullDeck)
            {
                if (!(hand.Contains(card)))
                {
                    scores.Add(count.CountingCards(hand, card));
                }
            }

            return scores;
        }
    }
}

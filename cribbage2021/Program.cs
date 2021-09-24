using System;
using System.Collections.Generic;

namespace cribbage2021
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a deck of cards
            List<decimal> fullDeck = new List<decimal>();
            for (decimal cardValue = 1.0M; cardValue <= 13; cardValue++)
            {
                for (decimal cardSuit = 1.0M; cardSuit <= 4; cardSuit++)
                {
                    fullDeck.Add(cardValue + cardSuit / 10.0M);
                }
            }

            //Start a running total
            int runningTotalPoints = 0;

            //Shuffle deck
            Random rnd = new Random();

            for (int shufIndex = 0; shufIndex < 100; shufIndex++)
            {
                int cardAIndex = rnd.Next(fullDeck.Count);
                int cardBIndex = rnd.Next(fullDeck.Count);
            // Console.WriteLine(shufIndex + ": " + cardAIndex + " & " + cardBIndex);

                while (cardAIndex == cardBIndex)  // Testing to make sure that the selected cards aren't the same card
                {
                    cardAIndex = rnd.Next(fullDeck.Count);
                    cardBIndex = rnd.Next(fullDeck.Count);
                }

                decimal storage;
                storage = fullDeck[cardAIndex];
                fullDeck[cardAIndex] = fullDeck[cardBIndex];
                fullDeck[cardBIndex] = storage;
            }

            //Deal 3 cards to hand, 2 to crib, then 3 to hand
            List<decimal> hand = new List<decimal> {
                fullDeck[0],
                fullDeck[1],
                fullDeck[2],
                fullDeck[5],
                fullDeck[6],
                fullDeck[7]
            };

            List<decimal> crib = new List<decimal>
            {
                fullDeck[3],
                fullDeck[4]
            };

            //Remove the cards from the deck
            fullDeck.RemoveRange(0, 8);

            //Sort the hand
            hand.Sort();

            //Reveal what's in the hand
            Console.WriteLine("1        2        3        4        5        6");
            Console.WriteLine("-        -        -        -        -        -");

            //Writing the values
            foreach (int cardHand in hand)
            {
                if (cardHand / 1 == 1)
                {
                    Console.Write("Ace      ");
                }
                else if (cardHand / 1 == 10)
                {
                    Console.Write("10       ");
                }
                else if (cardHand / 1 == 11)
                {
                    Console.Write("Jack     ");
                }
                else if (cardHand / 1 == 12)
                {
                    Console.Write("Queen    ");
                }
                else if (cardHand == 13)
                {
                    Console.Write("King     ");
                }
                else
                {
                    Console.Write(cardHand + "        ");
                }
            }
            Console.WriteLine();

            //Writing the suits
            foreach (decimal suit in hand)
            {
                double suitHand = (double)((suit % 1) * 10);
                if (suitHand == 1)
                {
                    Console.Write("Clubs    ");
                }
                else if (suitHand == 2)
                {
                    Console.Write("Diamonds ");
                }
                else if (suitHand == 3)
                {
                    Console.Write("Hearts   ");
                }
                else
                {
                    Console.Write("Spades   ");
                }
            }

            //Prompt the user to select two cards to send to the crib
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("What two cards would you like to discard? (Their associated numbers separated by a space) ");
            string discardStr = Console.ReadLine();
            string[] discardStrSplit = discardStr.Split(" ");
            string discardCardOne = discardStrSplit[0];
            string discardCardTwo = discardStrSplit[1];

            int discardOne = int.Parse(discardCardOne);
            int discardTwo = int.Parse(discardCardTwo);

            crib.Add(hand[discardOne - 1]);
            crib.Add(hand[discardTwo - 1]);
            decimal rHand1 = hand[discardOne - 1];
            decimal rHand2 = hand[discardTwo - 1];
            hand.Remove(rHand1);
            hand.Remove(rHand2);

            crib.Sort();

            // Randomly select Starter card
            int starterIndex = rnd.Next(fullDeck.Count);
            hand.Add(fullDeck[starterIndex]);
            crib.Add(fullDeck[starterIndex]);

            //Rewrite the hand
            /*
            // For testing purposes, setting the hand to specific values
            hand[0] = 6.3M;
            hand[1] = 7.1M;
            hand[2] = 7.3M;
            hand[3] = 8.2M;
            hand[4] = 8.4M;
            hand.RemoveRange(5, 1);
            //End test
            */

            Console.Clear();
            Console.WriteLine("1        2        3        4        Starter");
            Console.WriteLine("-        -        -        -        -------");
            foreach (int cardHand in hand)
            {
                if (cardHand / 1 == 1)
                {
                    Console.Write("Ace      ");
                }
                else if (cardHand / 1 == 10)
                {
                    Console.Write("10       ");
                }
                else if (cardHand / 1 == 11)
                {
                    Console.Write("Jack     ");
                }
                else if (cardHand / 1 == 12)
                {
                    Console.Write("Queen    ");
                }
                else if (cardHand == 13)
                {
                    Console.Write("King     ");
                }
                else
                {
                    Console.Write(cardHand + "        ");
                }
            }
            Console.WriteLine();

            //Writing the suits
            foreach (decimal suit in hand)
            {
                int suitHand = (int)((suit % 1.0M) * 10);
                if (suitHand == 1)
                {
                    Console.Write("Clubs    ");
                }
                else if (suitHand == 2)
                {
                    Console.Write("Diamonds ");
                }
                else if (suitHand == 3)
                {
                    Console.Write("Hearts   ");
                }
                else
                {
                    Console.Write("Spades   ");
                }
            }

            hand.Sort();
            int tempPoints = 0;
            // Step 0: Check for heels (starter being a jack)
            if (fullDeck[starterIndex] / 1 == 11)
            {
                tempPoints += 2;
                Console.WriteLine("Heels for " + tempPoints);
            }
            // Count the points in hand

            List<int> handNumValue = new List<int>();
            foreach (int card in hand)
            {
                if (card / 1 >= 10)
                {
                    handNumValue.Add(10);
                }
                else
                {
                    handNumValue.Add(card / 1);
                }
            }

            // Adding an empty value for the card not being applicable
            handNumValue.Add(0);

            Console.WriteLine();
            
            //Step 1: Count the 15s
            for(int handCardA = 0; handCardA < handNumValue.Count; handCardA += 5)
            {
                for(int handCardB = 1; handCardB < handNumValue.Count; handCardB += 4)
                {
                    for(int handCardC = 2; handCardC < handNumValue.Count; handCardC += 3)
                    {
                        for(int handCardD=3; handCardD < handNumValue.Count; handCardD += 2)
                        {
                            for (int handCardE = 4; handCardE < handNumValue.Count; handCardE++)
                            {
                                if (handNumValue[handCardA] + handNumValue[handCardB] + handNumValue[handCardC] + handNumValue[handCardD] + handNumValue[handCardE] == 15)
                                {
                                    tempPoints += 2;
                                    Console.WriteLine("15 for " + tempPoints);
                                }
                            }
                        }
                    }
                }
            }
            handNumValue.RemoveAt(5);

            //Step 2: Count pairs
            for (int handCardA = 0; handCardA < hand.Count - 1; handCardA++)
            {
                for (int handCardB = handCardA + 1; handCardB < hand.Count; handCardB++)
                {
                    if ((int)hand[handCardA] == (int)hand[handCardB])
                    {
                        tempPoints += 2;
                        Console.WriteLine("Pair for " + tempPoints);
                    }
                }
            }

            //Step 3: Count runs (consecutive numbers)
            bool runs = false;
            //Step 3a: Find a 5 card run

            if ((int)hand[0] + 1 == (int)hand[1] && (int)hand[1] + 1 == (int)hand[2] && (int)hand[2] + 1 == (int)hand[3] && (int)hand[3] + 1 == (int)hand[4])
            {
                tempPoints += 5;
                Console.WriteLine("Run for " + tempPoints);
                runs = true;
            }

            //Step 3b: If there's no 5 card run, check for 4 card run(s)
            if (!runs)
            {
                List<int> testHand = new List<int>();
                for (int indexSkip = 0; indexSkip < handNumValue.Count; indexSkip++)
                {
                    for (int indexUse = 0;indexUse < handNumValue.Count; indexUse++)
                    {
                        if (indexSkip != indexUse)
                        {
                            testHand.Add(handNumValue[indexUse]);
                        }
                    }

                    if (testHand[0] + 1 == testHand[1] && testHand[1] + 1 == testHand[2] && testHand[2] + 1 == testHand[3])
                    {
                        tempPoints += 4;
                        Console.WriteLine("Run for " + tempPoints);
                        runs = true;
                    }

                    testHand.RemoveRange(0,4);
                }
            }

            //Step 3c: If there's no 4 card run, check for 3 card run(s)
            //REMEMBER: Don't loop through different cards to apply, loop through two cards to not apply
            if (!runs)
            {
                List<int> testHand = new List<int>();
                for (int indexSkip1 = 0; indexSkip1 < handNumValue.Count; indexSkip1++)
                {
                    for (int indexSkip2 = indexSkip1 + 1; indexSkip2 < handNumValue.Count; indexSkip2++)
                    {
                        for (int indexUse = 0; indexUse < handNumValue.Count; indexUse++)
                        {
                            if (indexSkip1 != indexUse && indexSkip2 != indexUse)
                            {
                                testHand.Add(handNumValue[indexUse]);
                            }
                        }


                        if (testHand[0] + 1 == testHand[1] && testHand[1] + 1 == testHand[2])
                        {
                            tempPoints += 3;
                            Console.WriteLine("Run for " + tempPoints);
                        }

                        testHand.RemoveRange(0, 3);
                    }
                }
            }

            //Step 4: Check for flush (hand must be all the same suit, one bonus point if the starter matches)
            List<decimal> handSuit = new List<decimal>();
            foreach (decimal card in hand)
            {
                handSuit.Add(card % 1);
            }

            if (handSuit[0] == handSuit[1] && handSuit[1]==handSuit[2] && handSuit[2] == handSuit[3])
            {
                tempPoints += 4;
                if (handSuit[3] == handSuit[4])
                {
                    tempPoints += 1;
                }
                Console.WriteLine("Flush for " + tempPoints);
            }
            //Step 5: Check for nobs (jack of suit matching the starter)
            foreach(decimal card in hand)
            {
                if (card != fullDeck[starterIndex])
                {
                    if (card / 1 == 11 && card % 1 == fullDeck[starterIndex] % 1)
                    {
                        tempPoints += 1;
                        Console.WriteLine("Nobs for " + tempPoints);
                    }
                }
            }

            runningTotalPoints += tempPoints;

            //Check crib for points (aka redo the 15s, pairs, runs, flush, and nobs for the crib) - need to implement classes

            //Add points to running total, reshuffle deck and start again (this process will go until all but 4 cards have been used)
            //If there's four cards in the deck, treat the remaining four cards as your hand with no starter and add to the running total to make the final total
        }
    }
}

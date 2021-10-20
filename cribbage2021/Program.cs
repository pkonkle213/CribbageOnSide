using cribbage2021.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cribbage2021
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            ui.Run();

            /*
             * Methods I need:
             * -Shuffling the deck
             * -Printing the deck on the screen
             * -Allowing the user to choose cards to discard
             * -Checking for Heels
             * -Counting 15s
             * -Counting Pairs
             * -Counting Runs
             * -Counting Flush(es)
             * -Checking for Nobs
             * -Returning to the beginning of the process if the deck still has 5 or more cards
             * -If the deck has 4 cards, count 15s/pairs/runs/flush for the 4 card hand
             * -A new feature: a "hall of fame" or "top scores" of 5-10 people? Record high hands?
             * -Future feature: An "opponent" using the counting methods to determine their selected hands {high difficulty opponent would always select the best hand, low difficulty would select the worst hand
             */

            //Creating a deck of cards

            Points points = new Points();
            CountingPoints count = new CountingPoints();

            List<Card> fullDeck = new List<Card>();
            string suit;
            for (int cardValue = 1; cardValue <= 13; cardValue++)
            {
                for (int cardSuit = 1; cardSuit <= 4; cardSuit++)
                {
                    if (cardSuit == 1)
                    {
                        suit = "Clubs";
                    }
                    else if (cardSuit == 2)
                    {
                        suit = "Diamonds";
                    }
                    else if (cardSuit == 3)
                    {
                        suit = "Hearts";
                    }
                    else
                    {
                        suit = "Spades";
                    }

                    fullDeck.Add(new Card(cardValue, suit));
                }
            }

            //Start a running total
            int runningTotalPoints = 0;

            while (fullDeck.Count > 4)
            {
                //Shuffle deck
                Random rnd = new Random();

                for (int shufIndex = 0; shufIndex < 1000; shufIndex++)
                {
                    int cardAIndex = rnd.Next(fullDeck.Count);
                    int cardBIndex = rnd.Next(fullDeck.Count);
                    // Console.WriteLine(shufIndex + ": " + cardAIndex + " & " + cardBIndex);

                    while (cardAIndex == cardBIndex)  // Testing to make sure that the selected cards aren't the same card
                    {
                        cardAIndex = rnd.Next(fullDeck.Count);
                        cardBIndex = rnd.Next(fullDeck.Count);
                    }

                    Card storage;
                    storage = fullDeck[cardAIndex];
                    fullDeck[cardAIndex] = fullDeck[cardBIndex];
                    fullDeck[cardBIndex] = storage;
                }

                //Deal 3 cards to hand, 2 to crib, then 3 to hand
                List<Card> hand = new List<Card> {
                fullDeck[0],
                fullDeck[1],
                fullDeck[2],
                fullDeck[5],
                fullDeck[6],
                fullDeck[7]
                };

                List<Card> crib = new List<Card> {
                fullDeck[3],
                fullDeck[4]
                };

                //Remove the cards from the deck
                fullDeck.RemoveRange(0, 8);

                // Sort the hand, thank you google!
                hand = hand.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                //Reveal what's in the hand
                Console.WriteLine("1        2        3        4        5        6");
                Console.WriteLine("-        -        -        -        -        -");

                //Writing the values
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(9));
                }
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(9));
                }

                //Prompt the user to select two cards to send to the crib
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("What two cards would you like to discard? (Their associated numbers separated by a space) ");
                string discardStr = Console.ReadLine();
                string[] discardStrSplit = discardStr.Split(" ");
                string discardCardOne = discardStrSplit[0];
                string discardCardTwo = discardStrSplit[1];

                while (!int.TryParse(discardCardOne, out int CardOne) || !int.TryParse(discardCardOne, out int CardTwo))
                {
                    Console.WriteLine("Please enter appropriate values");
                }

                int discardOne = int.Parse(discardCardOne);
                int discardTwo = int.Parse(discardCardTwo);

                crib.Add(hand[discardOne - 1]);
                crib.Add(hand[discardTwo - 1]);
                hand.RemoveAt(discardOne - 1);
                hand.RemoveAt(discardTwo - 1);

                crib = crib.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                // Randomly select Starter card
                int starterIndex = rnd.Next(fullDeck.Count);
                Card starter = fullDeck[starterIndex];

                count.CountingFiveCards(hand, starter);

                /*
                hand.Add(fullDeck[starterIndex]);
                crib.Add(fullDeck[starterIndex]);
                */
                Console.Clear();
                Console.WriteLine("    1         2         3         4      Starter");
                Console.WriteLine("--------- --------- --------- --------- ---------");
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(10));
                }
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(10));
                }


                int tempPoints = 0;
           

                Console.WriteLine();

                // Count the points in hand
                //Step 1: Count the 15s
                //For each permutation of cards adds to 15 (face cards = 10), the player scores 2 points

                //Step1.a - Count 2 card 15s
                const int pointsFifteenEarned = 2;

                for (int handCardA = 0; handCardA < hand.Count; handCardA++)
                {
                    for (int handCardB = handCardA; handCardB < hand.Count; handCardB++)
                    {
                        List<Card> sampleHand = new List<Card>
                        {
                            hand[handCardA],
                            hand[handCardB]
                        };

                        if (points.Fifteen(sampleHand))
                        {
                            tempPoints += pointsFifteenEarned;
                            Console.WriteLine($"15 for {tempPoints}");
                        }
                    }
                }

                //Step1.b - Count 3 card 15s
                for (int handCardA = 0; handCardA < hand.Count; handCardA++)
                {
                    for (int handCardB = handCardA; handCardB < hand.Count; handCardB++)
                    {
                        for (int handCardC = handCardB; handCardC < hand.Count; handCardC++)
                        {
                            List<Card> sampleHand = new List<Card>
                            {
                                hand[handCardA],
                                hand[handCardB],
                                hand[handCardC]
                            };

                            if (points.Fifteen(sampleHand))
                            {
                                tempPoints += pointsFifteenEarned;
                                Console.WriteLine($"15 for {tempPoints}");
                            }
                        }
                    }
                }

                //Step1.c - Count 4 card 15s
                for (int handCardA = 0; handCardA < hand.Count; handCardA++)
                {
                    for (int handCardB = handCardA; handCardB < hand.Count; handCardB++)
                    {
                        for (int handCardC = handCardB; handCardC < hand.Count; handCardC++)
                        {
                            for (int handCardD = handCardC; handCardD < hand.Count; handCardD++)
                            {
                                List<Card> sampleHand = new List<Card>
                                {
                                    hand[handCardA],
                                    hand[handCardB],
                                    hand[handCardC],
                                    hand[handCardD]
                                };

                                if (points.Fifteen(sampleHand))
                                {
                                    tempPoints += pointsFifteenEarned;
                                    Console.WriteLine($"15 for {tempPoints}");
                                }
                            }
                        }
                    }

                }

                {
                    List<Card> sampleHand = new List<Card>
                    {
                        hand[0],
                        hand[1],
                        hand[2],
                        hand[3],
                        hand[4]
                    };

                    if (points.Fifteen(sampleHand))
                    {
                        tempPoints += pointsFifteenEarned;
                        Console.WriteLine($"15 for {tempPoints}");
                    }
                }

                //Step 2: Count pairs
                const int pairSuccess = 2;

                for (int handCardA = 0; handCardA < hand.Count - 1; handCardA++)
                {
                    for (int handCardB = handCardA + 1; handCardB < hand.Count; handCardB++)
                    {
                        if (points.Pair(hand[handCardA], hand[handCardB]))
                        {
                            tempPoints += 2;
                            Console.WriteLine("Pair for " + tempPoints);
                        }
                    }
                }

                //Step 3: Count runs (consecutive numbers)
                bool runs = false;

                //Sorting the hand to test for runs
                List<Card> handSorted = hand.OrderBy(x => x.NumberValue)
                          .ThenBy(x => x.SuitOfCard)
                          .ToList();

                //Step 3a: Find a 5 card run

                if (points.Run(handSorted))
                {
                    tempPoints += 5;
                    Console.WriteLine("Run for " + tempPoints);
                    runs = true;
                }

                //Step 3b: If there's no 5 card run, check for 4 card run(s)
                if (!runs)
                {
                    List<Card> testHand = new List<Card>();
                    for (int indexSkip = 0; indexSkip < handSorted.Count; indexSkip++)
                    {
                        for (int indexUse = 0; indexUse < handSorted.Count; indexUse++)
                        {
                            if (indexSkip != indexUse)
                            {
                                testHand.Add(handSorted[indexUse]);
                            }
                        }

                        if (points.Run(testHand))
                        {
                            tempPoints += 4;
                            Console.WriteLine("Run for " + tempPoints);
                            runs = true;
                        }

                        testHand.RemoveRange(0, 4);
                    }
                }

                //Step 3c: If there's no 4 card run, check for 3 card run(s)
                //REMEMBER: Don't loop through different cards to apply, loop through two cards to not apply
                if (!runs)
                {
                    List<Card> testHand = new List<Card>();
                    for (int indexSkip1 = 0; indexSkip1 < hand.Count; indexSkip1++)
                    {
                        for (int indexSkip2 = indexSkip1 + 1; indexSkip2 < hand.Count; indexSkip2++)
                        {
                            for (int indexUse = 0; indexUse < hand.Count; indexUse++)
                            {
                                if (indexSkip1 != indexUse && indexSkip2 != indexUse)
                                {
                                    testHand.Add(handSorted[indexUse]);
                                }
                            }

                            if (points.Run(testHand))
                            {
                                tempPoints += 3;
                                Console.WriteLine("Run for " + tempPoints);
                            }

                            testHand.RemoveRange(0, 3);
                        }
                    }
                }

                //Step 4: Check for flush (hand must be all the same suit, one bonus point if the starter matches)
                bool flush = false;
                {
                    List<Card> testHand = new List<Card> {
                    hand[0],
                    hand[1],
                    hand[2],
                    hand[3],
                    hand[4]
                };

                    if (points.Flush(testHand))
                    {
                        tempPoints += 5;
                        Console.WriteLine("Flush for " + tempPoints);
                        flush = true;
                    }

                    hand.RemoveAt(4);
                    if (!flush)
                    {
                        if (points.Flush(testHand))
                        {
                            tempPoints += 4;
                            Console.WriteLine("Flush for " + tempPoints);
                        }
                    }


                    //Step 5: Check for nobs (jack of suit matching the starter)
                    foreach (Card card in hand)
                    {
                        if (card != fullDeck[starterIndex])
                        {
                            if (card.NumberValue == 11 && card.SuitOfCard == fullDeck[starterIndex].SuitOfCard)
                            {
                                tempPoints += 1;
                                Console.WriteLine("Nobs for " + tempPoints);
                            }
                        }
                    }
                }

                runningTotalPoints += tempPoints;
                tempPoints = 0;

                //Check crib for points (aka redo the 15s, pairs, runs, flush, and nobs for the crib) - need to implement classes
                // Adding an empty value for the card not being applicable
                crib.Add(new Card(0, "None"));
                for (int handCardA = 0; handCardA < crib.Count; handCardA += 5)
                {
                    for (int handCardB = 1; handCardB < crib.Count; handCardB += 4)
                    {
                        for (int handCardC = 2; handCardC < crib.Count; handCardC += 3)
                        {
                            for (int handCardD = 3; handCardD < crib.Count; handCardD += 2)
                            {
                                for (int handCardE = 4; handCardE < crib.Count; handCardE++)
                                {
                                    if (crib[handCardA].AddingValue + crib[handCardB].AddingValue + crib[handCardC].AddingValue + crib[handCardD].AddingValue + crib[handCardE].AddingValue == 15)
                                    {
                                        tempPoints += 2;
                                        Console.WriteLine("15 for " + tempPoints);
                                    }
                                }
                            }
                        }
                    }
                }

                //Remove the blank value at the end of the list
                crib.RemoveAt(5);

                //Step 2: Count pairs
                for (int handCardA = 0; handCardA < crib.Count - 1; handCardA++)
                {
                    for (int handCardB = handCardA + 1; handCardB < crib.Count; handCardB++)
                    {
                        if (crib[handCardA].NumberValue == crib[handCardB].NumberValue)
                        {
                            tempPoints += 2;
                            Console.WriteLine("Pair for " + tempPoints);
                        }
                    }
                }

                //Step 3: Count runs (consecutive numbers)
                runs = false;
                List<Card> cribSorted = crib.OrderBy(x => x.NumberValue)
                                            .ThenBy(x => x.SuitOfCard)
                                            .ToList();
                //Step 3a: Find a 5 card run

                if (cribSorted[0].NumberValue + 1 == cribSorted[1].NumberValue && cribSorted[1].NumberValue + 1 == cribSorted[2].NumberValue && cribSorted[2].NumberValue + 1 == cribSorted[3].NumberValue && cribSorted[3].NumberValue + 1 == cribSorted[4].NumberValue)
                {
                    tempPoints += 5;
                    Console.WriteLine("Run for " + tempPoints);
                    runs = true;
                }

                //Step 3b: If there's no 5 card run, check for 4 card run(s)
                if (!runs)
                {
                    List<int> testHand = new List<int>();
                    for (int indexSkip = 0; indexSkip < cribSorted.Count; indexSkip++)
                    {
                        for (int indexUse = 0; indexUse < cribSorted.Count; indexUse++)
                        {
                            if (indexSkip != indexUse)
                            {
                                testHand.Add(cribSorted[indexUse].NumberValue);
                            }
                        }

                        if (testHand[0] + 1 == testHand[1] && testHand[1] + 1 == testHand[2] && testHand[2] + 1 == testHand[3])
                        {
                            tempPoints += 4;
                            Console.WriteLine("Run for " + tempPoints);
                            runs = true;
                        }

                        testHand.RemoveRange(0, 4);
                    }
                }

                //Step 3c: If there's no 4 card run, check for 3 card run(s)
                //REMEMBER: Don't loop through different cards to apply, loop through two cards to not apply
                if (!runs)
                {
                    List<int> testHand = new List<int>();
                    for (int indexSkip1 = 0; indexSkip1 < cribSorted.Count; indexSkip1++)
                    {
                        for (int indexSkip2 = indexSkip1 + 1; indexSkip2 < crib.Count; indexSkip2++)
                        {
                            for (int indexUse = 0; indexUse < cribSorted.Count; indexUse++)
                            {
                                if (indexSkip1 != indexUse && indexSkip2 != indexUse)
                                {
                                    testHand.Add(cribSorted[indexUse].NumberValue);
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
                //Issue with this code: the program should be testing the four cards that make the crib for the initial flush and then the starter for a bonus point
                if (crib[0].SuitOfCard == crib[1].SuitOfCard && crib[1].SuitOfCard == crib[2].SuitOfCard && crib[2].SuitOfCard == crib[3].SuitOfCard)
                {
                    tempPoints += 4;
                    if (crib[3].SuitOfCard == crib[4].SuitOfCard)
                    {
                        tempPoints += 1;
                    }
                    Console.WriteLine("Flush for " + tempPoints);
                }
                //Step 5: Check for nobs (jack of suit matching the starter)
                foreach (Card card in crib)
                {
                    if (card != fullDeck[starterIndex])
                    {
                        if (card.NumberValue == 11 && card.SuitOfCard == fullDeck[starterIndex].SuitOfCard)
                        {
                            tempPoints += 1;
                            Console.WriteLine("Nobs for " + tempPoints);
                        }
                    }
                }

                runningTotalPoints += tempPoints;
                tempPoints = 0;
                //Add points to running total, reshuffle deck and start again (this process will go until all but 4 cards have been used)
            }

            //When there's four cards in the deck, treat the remaining four cards as your hand with no starter and add to the running total to make the final total
        }
    }
}

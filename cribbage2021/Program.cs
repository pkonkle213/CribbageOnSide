using cribbage2021.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cribbage2021
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a deck of cards
            //To do this with classes, We could for() loop the numbers and the suits again, but not include decimal spots

            /*
             * Methods I need:
             * -Shuffling the deck
             * -Printing the deck on the screen
             * -Allowing the user to choose cards to discard
             * -Checking for Heels
             * -Counting 15s (Must be able to calculate based upon 4 or 5 cards for normal hands, normal cribs, and the last hand.
             * -Counting Pairs
             * -Counting Runs
             * -Counting Flush(es)
             * -Checking for Nobs
             * -Returning to the beginning of the process if the deck still has 5 or more cards
             */

            List<Card> fullDeck = new List<Card>();
            string name = "";
            string suit = "";
            for (int cardValue = 1; cardValue <= 13; cardValue++)
            {
                for (int cardSuit = 1; cardSuit <= 4; cardSuit++)
                {
                    if (cardValue == 1)
                    {
                        name = "Ace";
                    }
                    else if (cardValue == 11)
                    {
                        name = "Jack";
                    }
                    else if (cardValue == 12)
                    {
                        name = "Queen";
                    }
                    else if (cardValue == 13)
                    {
                        name = "King";
                    }
                    else
                    {
                        name = cardValue.ToString();
                    }

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

                    fullDeck.Add(new Card(name, suit, cardValue));
                }
            }

            //Start a running total
            int runningTotalPoints = 0;

            while (fullDeck.Count > 4)
            {
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

                List<Card> crib = new List<Card>
            {
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
                    Console.Write(cardHand.NameOfCard);
                    for (int i = 0; i < 9 - cardHand.NameOfCard.Length; i++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.SuitOfCard);
                    for (int i = 0; i < 9 - cardHand.SuitOfCard.Length; i++)
                    {
                        Console.Write(" ");
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
                Card rHand1 = hand[discardOne - 1];
                Card rHand2 = hand[discardTwo - 1];
                hand.Remove(rHand1);
                hand.Remove(rHand2);

                crib = crib.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                // Randomly select Starter card
                int starterIndex = rnd.Next(fullDeck.Count);
                hand.Add(fullDeck[starterIndex]);
                crib.Add(fullDeck[starterIndex]);
                //End test


                Console.Clear();
                Console.WriteLine("1        2        3        4        Starter");
                Console.WriteLine("-        -        -        -        -------");
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.NameOfCard);
                    for (int i = 0; i < 9 - cardHand.NameOfCard.Length; i++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();


                //Writing the suits
                foreach (Card cardHand in hand)
                {
                    Console.Write(cardHand.SuitOfCard);
                    for (int i = 0; i < 9 - cardHand.SuitOfCard.Length; i++)
                    {
                        Console.Write(" ");
                    }
                }

                //Resorting to include the starter

                /*
                 * 
                 * 
                 * 
                 * 
                 * This code should initialize a handSorted for purposes of keeping the original four cards true in the case of looking for a flush
                 * 
                 * 
                 * 
                 * 
                 * 
                 * */
                hand = hand.OrderBy(x => x.NumberValue)
                          .ThenBy(x => x.SuitOfCard)
                          .ToList();

                int tempPoints = 0;
                // Step 0: Check for heels (starter being a jack)
                if (fullDeck[starterIndex].NameOfCard == "Jack")
                {
                    tempPoints += 2;
                    Console.WriteLine("Heels for " + tempPoints);
                }
                // Count the points in hand

                // Adding an empty value for the card not being applicable
                hand.Add(new Card("None", "None", 0));

                Console.WriteLine();

                //Step 1: Count the 15s
                //For each permutation of cards adds to 15 (face cards = 10), the player scores 2 points

                for (int handCardA = 0; handCardA < hand.Count; handCardA += 5)
                {
                    for (int handCardB = 1; handCardB < hand.Count; handCardB += 4)
                    {
                        for (int handCardC = 2; handCardC < hand.Count; handCardC += 3)
                        {
                            for (int handCardD = 3; handCardD < hand.Count; handCardD += 2)
                            {
                                for (int handCardE = 4; handCardE < hand.Count; handCardE++)
                                {
                                    if (hand[handCardA].AddingValue + hand[handCardB].AddingValue + hand[handCardC].AddingValue + hand[handCardD].AddingValue + hand[handCardE].AddingValue == 15)
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
                hand.RemoveAt(5);

                //Step 2: Count pairs
                for (int handCardA = 0; handCardA < hand.Count - 1; handCardA++)
                {
                    for (int handCardB = handCardA + 1; handCardB < hand.Count; handCardB++)
                    {
                        if (hand[handCardA].NumberValue == hand[handCardB].NumberValue)
                        {
                            tempPoints += 2;
                            Console.WriteLine("Pair for " + tempPoints);
                        }
                    }
                }

                //Step 3: Count runs (consecutive numbers)
                bool runs = false;
                //Step 3a: Find a 5 card run

                if (hand[0].NumberValue + 1 == hand[1].NumberValue && hand[1].NumberValue + 1 == hand[2].NumberValue && hand[2].NumberValue + 1 == hand[3].NumberValue && hand[3].NumberValue + 1 == hand[4].NumberValue)
                {
                    tempPoints += 5;
                    Console.WriteLine("Run for " + tempPoints);
                    runs = true;
                }

                //Step 3b: If there's no 5 card run, check for 4 card run(s)
                if (!runs)
                {
                    List<int> testHand = new List<int>();
                    for (int indexSkip = 0; indexSkip < hand.Count; indexSkip++)
                    {
                        for (int indexUse = 0; indexUse < hand.Count; indexUse++)
                        {
                            if (indexSkip != indexUse)
                            {
                                testHand.Add(hand[indexUse].NumberValue);
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
                    for (int indexSkip1 = 0; indexSkip1 < hand.Count; indexSkip1++)
                    {
                        for (int indexSkip2 = indexSkip1 + 1; indexSkip2 < hand.Count; indexSkip2++)
                        {
                            for (int indexUse = 0; indexUse < hand.Count; indexUse++)
                            {
                                if (indexSkip1 != indexUse && indexSkip2 != indexUse)
                                {
                                    testHand.Add(hand[indexUse].NumberValue);
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
                if (hand[0].SuitOfCard == hand[1].SuitOfCard && hand[1].SuitOfCard == hand[2].SuitOfCard && hand[2].SuitOfCard == hand[3].SuitOfCard)
                {
                    tempPoints += 4;
                    if (hand[3].SuitOfCard == hand[4].SuitOfCard)
                    {
                        tempPoints += 1;
                    }
                    Console.WriteLine("Flush for " + tempPoints);
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

                runningTotalPoints += tempPoints;
                tempPoints = 0;

                //Check crib for points (aka redo the 15s, pairs, runs, flush, and nobs for the crib) - need to implement classes
                // Adding an empty value for the card not being applicable
                crib.Add(new Card("None", "None", 0));
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
                //Step 3a: Find a 5 card run

                if (crib[0].NumberValue + 1 == crib[1].NumberValue && crib[1].NumberValue + 1 == crib[2].NumberValue && crib[2].NumberValue + 1 == crib[3].NumberValue && crib[3].NumberValue + 1 == crib[4].NumberValue)
                {
                    tempPoints += 5;
                    Console.WriteLine("Run for " + tempPoints);
                    runs = true;
                }

                //Step 3b: If there's no 5 card run, check for 4 card run(s)
                if (!runs)
                {
                    List<int> testHand = new List<int>();
                    for (int indexSkip = 0; indexSkip < crib.Count; indexSkip++)
                    {
                        for (int indexUse = 0; indexUse < crib.Count; indexUse++)
                        {
                            if (indexSkip != indexUse)
                            {
                                testHand.Add(crib[indexUse].NumberValue);
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
                    for (int indexSkip1 = 0; indexSkip1 < crib.Count; indexSkip1++)
                    {
                        for (int indexSkip2 = indexSkip1 + 1; indexSkip2 < crib.Count; indexSkip2++)
                        {
                            for (int indexUse = 0; indexUse < crib.Count; indexUse++)
                            {
                                if (indexSkip1 != indexUse && indexSkip2 != indexUse)
                                {
                                    testHand.Add(crib[indexUse].NumberValue);
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

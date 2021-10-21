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
                List<Card> handNew = new List<Card> {
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
                handNew = handNew.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                //Reveal what's in the hand
                Console.WriteLine("    1         2         3         4         5         6");
                Console.WriteLine("--------- --------- --------- --------- --------- ---------");

                //Writing the values
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(10));
                }
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(10));
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

                crib.Add(handNew[discardOne - 1]);
                crib.Add(handNew[discardTwo - 1]);
                handNew.Remove(crib[2]);
                handNew.Remove(crib[3]);

                crib = crib.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                // Randomly select Starter card
                int starterIndex = rnd.Next(fullDeck.Count);
                Card starterNew = fullDeck[starterIndex];

                Console.Clear();
                Console.WriteLine("--------------------Your hand--------------------");
                Console.WriteLine("    1         2         3         4      Starter");
                Console.WriteLine("--------- --------- --------- --------- ---------");
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(10));
                }
                Console.Write(starterNew.NameOfCard);
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(10));
                }
                Console.Write(starterNew.SuitOfCard);
                Console.WriteLine();

                //Add points to running total, reshuffle deck and start again (this process will go until all but 4 cards have been used)
                int handPoints = count.CountingCards(handNew, starterNew);
                Console.WriteLine($"Total points for your hand: {handPoints}");
                Console.WriteLine("Press [Enter] to continue.");
                Console.ReadLine();
                Console.Clear();
                runningTotalPoints += handPoints;

                Console.WriteLine("--------------------Your crib--------------------");
                Console.WriteLine("    1         2         3         4      Starter");
                Console.WriteLine("--------- --------- --------- --------- ---------");
                foreach (Card cardHand in crib)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(10));
                }
                Console.Write(starterNew.NameOfCard);
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in crib)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(10));
                }
                Console.Write(starterNew.SuitOfCard);
                Console.WriteLine();

                int cribPoints = count.CountingCards(crib, starterNew);
                Console.WriteLine($"Total points for your crib: {cribPoints}");
                Console.WriteLine("Press [Enter] to continue.");
                Console.ReadLine();
                Console.Clear(); 
                runningTotalPoints += cribPoints;
            }

            //When there's four cards in the deck, treat the remaining four cards as your hand with no starter and add to the running total to make the final total
            List<Card> hand = new List<Card>
            {
                fullDeck[0],
                fullDeck[1],
                fullDeck[2],
                fullDeck[3]
            };
            Card starter = new Card(0, "None");

            Console.Clear();
            Console.WriteLine("-------------Your last hand------------");
            Console.WriteLine("    1         2         3         4    ");
            Console.WriteLine("--------- --------- --------- ---------");
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
            Console.WriteLine();

            //Add points to running total, reshuffle deck and start again (this process will go until all but 4 cards have been used)
            int finalHandPoints = count.CountingCards(hand, starter);
            Console.WriteLine($"Total points for your hand: {finalHandPoints}");
            Console.WriteLine("Press [Enter] to continue.");
            Console.Clear();

            runningTotalPoints += count.CountingCards(hand, starter);

            Console.WriteLine($"Congratulations! Your final score is: {runningTotalPoints}");
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
        }
    }
}

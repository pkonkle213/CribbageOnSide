using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cribbage2021.Classes;

namespace cribbage2021.Classes
{
    public class UserInterface
    {
        public void Run()
        {
            Deck deck = new Deck();
            Points points = new Points();
            CountingPoints count = new CountingPoints();
            const int textPad = 9;
            TestHand test = new TestHand();

            //Create a new deck
            List<Card> fullDeck = deck.CreateDeck();
            //Start a running total
            int runningTotalPoints = 0;

            while (fullDeck.Count > 4)
            {

                fullDeck = deck.ShuffleDeck(fullDeck);
                List<Card> handNew = deck.DealHand(fullDeck);
                List<Card> crib = deck.DealCrib(fullDeck);

                //Remove the dealt cards from the deck
                fullDeck.RemoveRange(0, 8);

                // Sort the hand, thank you google!
                handNew = handNew.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                //Reveal what's in the hand
                Console.WriteLine("   #1       #2       #3       #4       #5       #6");
                Console.WriteLine("-------- -------- -------- -------- -------- --------");

                //Writing the values
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(textPad));
                }
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(textPad));
                }



                test.ShowHands(handNew, fullDeck);



                //Prompt the user to select two cards to send to the crib
                Console.WriteLine();
                bool input = false;
                while (!input)
                {
                    Console.WriteLine();
                    Console.Write("What two cards would you like to discard? (Their associated numbers separated by a space) ");
                    string discardStr = Console.ReadLine();
                    string[] discardStrSplit = discardStr.Split(" ");
                    try
                    {

                        string discardCardOne = discardStrSplit[0];
                        string discardCardTwo = discardStrSplit[1];

                        int discardOne = int.Parse(discardCardOne);
                        int discardTwo = int.Parse(discardCardTwo);

                        crib.Add(handNew[discardOne - 1]);
                        crib.Add(handNew[discardTwo - 1]);
                        handNew.Remove(crib[2]);
                        handNew.Remove(crib[3]);
                        input = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter appropriate values");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Please enter appropriate values");
                    }
                }

                crib = crib.OrderBy(x => x.NumberValue)
                           .ThenBy(x => x.SuitOfCard)
                           .ToList();

                // Randomly select Starter card
                Random rnd = new Random();
                int starterIndex = rnd.Next(fullDeck.Count);
                Card starterNew = fullDeck[starterIndex];

                Console.Clear();
                Console.WriteLine("------------------Your hand----------------");
                Console.WriteLine("   #1       #2       #3       #4    Starter");
                Console.WriteLine("-------- -------- -------- -------- -------");
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(textPad));
                }
                Console.Write(starterNew.NameOfCard);
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in handNew)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(textPad));
                }
                Console.Write(starterNew.SuitOfCard);
                Console.WriteLine();

                //Add points to running total, reshuffle deck and start again (this process will go until all but 4 cards have been used)
                int handPoints = count.CountingCards(handNew, starterNew, false, true);
                Console.WriteLine($"Total points for your hand: {handPoints}");
                runningTotalPoints += handPoints;
                Console.WriteLine();
                Console.WriteLine($"Your current points are: {runningTotalPoints}");
                Console.WriteLine("Press [Enter] to continue.");
                Console.ReadLine();
                Console.Clear();

                Console.WriteLine("-----------------Your crib-----------------");
                Console.WriteLine("   #1       #2       #3       #4    Starter");
                Console.WriteLine("-------- -------- -------- -------- -------");
                foreach (Card cardHand in crib)
                {
                    Console.Write(cardHand.NameOfCard.PadRight(textPad));
                }
                Console.Write(starterNew.NameOfCard);
                Console.WriteLine();

                //Writing the suits
                foreach (Card cardHand in crib)
                {
                    Console.Write(cardHand.SuitOfCard.PadRight(textPad));
                }
                Console.Write(starterNew.SuitOfCard);
                Console.WriteLine();

                int cribPoints = count.CountingCards(crib, starterNew, true, true);
                Console.WriteLine($"Total points for your crib: {cribPoints}");
                runningTotalPoints += cribPoints;
                Console.WriteLine();
                Console.WriteLine($"Your current points are: {runningTotalPoints}");
                Console.WriteLine("Press [Enter] to continue.");
                Console.ReadLine();
                Console.Clear();
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
            Console.WriteLine("-----------Your last hand-----------");
            Console.WriteLine("   #1       #2       #3       #4    ");
            Console.WriteLine("-------- -------- -------- --------");
            foreach (Card cardHand in hand)
            {
                Console.Write(cardHand.NameOfCard.PadRight(textPad));
            }
            Console.WriteLine();

            //Writing the suits
            foreach (Card cardHand in hand)
            {
                Console.Write(cardHand.SuitOfCard.PadRight(textPad));
            }
            Console.WriteLine();

            //Add points to running total, reshuffle deck and start again (this process will go until all but 4 cards have been used)
            int finalHandPoints = count.CountingCards(hand, starter, false, true);
            Console.WriteLine($"Total points for your hand: {finalHandPoints}");
            Console.WriteLine("Press [Enter] to continue.");
            Console.Clear();

            runningTotalPoints += finalHandPoints;

            Console.WriteLine($"Congratulations! Your final score is: {runningTotalPoints}");
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
        }
    }
}
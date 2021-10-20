using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class UserInterface
    {
        public void Run()
        {
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


        }
    }
}

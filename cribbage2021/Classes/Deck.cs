using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class Deck
    {
        public List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
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

                    deck.Add(new Card(cardValue, suit));
                }
            }
            return deck;
        }

        public List<Card> ShuffleDeck(List<Card> deck)
        {
            Random rnd = new Random();

            for (int shufIndex = 0; shufIndex < 1000; shufIndex++)
            {
                int cardAIndex = rnd.Next(deck.Count);
                int cardBIndex = rnd.Next(deck.Count);

                while (cardAIndex == cardBIndex)  // Testing to make sure that the selected cards aren't the same card
                {
                    cardAIndex = rnd.Next(deck.Count);
                    cardBIndex = rnd.Next(deck.Count);
                }

                Card storage;
                storage = deck[cardAIndex];
                deck[cardAIndex] = deck[cardBIndex];
                deck[cardBIndex] = storage;
            }

            return deck;
        }

        public List<Card> DealHand(List<Card> deck)
        {
            //Deal 3 cards to hand, 2 to crib, then 3 to hand
            return new List<Card> {
                deck[0],
                deck[1],
                deck[2],
                deck[5],
                deck[6],
                deck[7]
            };
        }

        public List<Card> DealCrib(List<Card> deck)
        {
            return new List<Card> {
                deck[3],
                deck[4]
            };
        }
    }
}

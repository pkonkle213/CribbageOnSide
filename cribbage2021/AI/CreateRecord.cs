using cribbage2021.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cribbage2021.AI
{
    public class CreateRecord
    {
        public string filePathHandPoints = @"C:\PhilCribbage\LogOfHandPoints.txt";
        public string filePathHandCards = @"C:\PhilCribbage\LogOfCards.txt";
        public string filePathTestHand = @"C:\PhilCribbage\TestHand.txt";

        public void Record(Card cardA, Card cardB, Card cardC, Card cardD, Card starter, int points)
        {
            using (StreamWriter writer = new StreamWriter(filePathHandPoints, true)) // Opens a file to write a log (or begins the file). Notes the date, action taken, the difference, and the new total
            {
                writer.WriteLine(points);
            }
            using (StreamWriter writer = new StreamWriter(filePathHandCards, true))
            {
                writer.WriteLine($"{cardA.NumberValue} {cardA.SuitOfCard} " +
                    $"{cardB.NumberValue} {cardB.SuitOfCard} " +
                    $"{cardC.NumberValue} {cardC.SuitOfCard} " +
                    $"{cardD.NumberValue} {cardD.SuitOfCard} " +
                    $"{starter.NumberValue} {starter.SuitOfCard} ");
            }
        }

        public void TestHand(List<Card> hand, Card starter, int points)
        {
            using (StreamWriter writer = new StreamWriter(filePathTestHand, true))
            {
                writer.WriteLine($"{hand[0].NameOfCard} {hand[0].SuitOfCard} "+
                    $"{hand[1].NameOfCard} {hand[1].SuitOfCard} "+
                    $"{hand[2].NameOfCard} {hand[2].SuitOfCard} "+
                    $"{hand[3].NameOfCard} {hand[3].SuitOfCard} "+
                    $"{starter.NameOfCard} {starter.SuitOfCard} "+
                    $"- {points} ");
            }
        }
    }
}

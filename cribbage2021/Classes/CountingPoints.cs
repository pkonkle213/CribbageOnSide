using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class CountingPoints
    {
        Points points = new Points();
        Output output = new Output();


        public int CountingFiveCards(List<Card> hand, Card starter)
        {
            int tempPoints = 0;

            tempPoints = points.Heels(starter, tempPoints);
            tempPoints = points.Fifteens(hand, starter, tempPoints);
            tempPoints = points.Pairs(hand, starter, tempPoints);
            tempPoints = points.Runs(hand, starter, tempPoints);
            tempPoints = points.Flush(hand, starter, tempPoints);
            return tempPoints;
        }

        public int CountingFourCards(List<Card> hand)
        {

        }
    }
}

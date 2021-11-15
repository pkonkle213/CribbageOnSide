using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class CountingPoints
    {
        Points points = new Points();

        public int CountingCards(List<Card> hand, Card starter, bool crib, bool outWrite)
        {
            int tempPoints = 0;

            tempPoints = points.Heels(starter, tempPoints, outWrite);

            if (starter.AddingValue > 0)
            {
                hand.Add(starter);
            }
            tempPoints = points.Fifteens(hand, tempPoints, outWrite);
            tempPoints = points.Pairs(hand, tempPoints, outWrite);
            tempPoints = points.Runs(hand, tempPoints, outWrite);
            if (starter.AddingValue > 0)
            {
                hand.Remove(starter);
            }

            tempPoints = points.Flush(hand, starter, tempPoints, crib, outWrite);
            tempPoints = points.Knobs(hand, starter, tempPoints, outWrite);

            return tempPoints;
        }
    }
}

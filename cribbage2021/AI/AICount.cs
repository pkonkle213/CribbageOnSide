﻿using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class AICount
    {
        AIPoints points = new AIPoints();

        public int CountingCards(List<Card> hand, Card starter)
        {
            int tempPoints = 0;

            hand.Add(starter);
            tempPoints = points.Fifteens(hand, tempPoints);
            tempPoints = points.Pairs(hand, tempPoints);
            tempPoints = points.Runs(hand, tempPoints);
            hand.Remove(starter);

            tempPoints = points.Flush(hand, starter, tempPoints);
            tempPoints = points.Knobs(hand, starter, tempPoints);

            return tempPoints;
        }
    }
}
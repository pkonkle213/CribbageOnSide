using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class Card
    {
        public Card (string nameOfCard, string suitOfCard, int numberValue)
        {
            this.NameOfCard = nameOfCard;
            this.SuitOfCard = suitOfCard;
            this.NumberValue = numberValue;
        }


        public string NameOfCard { get; }
        public string SuitOfCard { get; }
        public int NumberValue { get; }
        public int AddingValue
        {
            get
            {
                if (NumberValue > 10)
                {
                    return 10;
                }
                return NumberValue;
            }
        }
    }
}

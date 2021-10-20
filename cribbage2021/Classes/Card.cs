using System;
using System.Collections.Generic;
using System.Text;

namespace cribbage2021.Classes
{
    public class Card
    {
        public Card(int numberValue, string suitOfCard)
        {
            this.SuitOfCard = suitOfCard;
            this.NumberValue = numberValue;
        }


        public string NameOfCard
        {
            get
            {
                string name;
                switch (NumberValue)
                {
                    case 0:
                        name = "none";
                        break;

                    case 1:
                        name = "Ace";
                        break;

                    case 11:
                        name = "Jack";
                        break;

                    case 12:
                        name = "Queen";
                        break;

                    case 13:
                        name = "King";
                        break;

                    default:
                        name = NumberValue.ToString();
                        break;
                }
                return name;
            }
        }

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

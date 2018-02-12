using System;
using System.Collections.Generic;
using System.Text;

namespace PokerGuess.Models
{
    public enum Suite
    {
        Clubs=0, Diamonds=1, Hearts=2, Spades=3
    }

    public class Card
    {
        public int Value { get; set; }
        public Suite Suite { get; set; }
        public int Order { get; set; }
        public string ImagePath
        {
            get
            {
                var name = this.ToString();
                var nameLow = name.ToLower();
                var result = nameLow.Replace(" ", "_");
                return "PokerGuess.Resources.CardImages." + result + ".png";
            }
        }

        public override string ToString()
        {
            string value;
            switch (Value)
            {
                case 14:
                    value = "Ace";
                    break;
                case 11:
                    value = "Jack";
                    break;
                case 12:
                    value = "Queen";
                    break;
                case 13:
                    value = "King";
                    break;
                default:
                    value = Value.ToString();
                    break;
            }
            return value + " of " + Suite.ToString();
        }
    }
}

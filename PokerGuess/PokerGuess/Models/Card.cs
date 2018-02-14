using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string SmallImagePath
        {
            get
            {
                var name = this.ToString();
                var nameLow = name.ToLower();
                var result = nameLow.Replace(" ", "_");
                return "PokerGuess.Resources.SmallCardImages." + result + ".png";
            }
        }

        public string ShortName
        {
            get
            {
                string shortName = Value.ToString();
                if (Value > 9)
                {
                    switch (Value)
                    {
                        case 10:
                            shortName = "T";
                            break;
                        case 11:
                            shortName = "J";
                            break;
                        case 12:
                            shortName = "Q";
                            break;
                        case 13:
                            shortName = "K";
                            break;
                        case 14:
                            shortName = "A";
                            break;
                    } 
                }
                     
                return shortName;
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

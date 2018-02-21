using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokerGuess.Models
{
    public enum Suit
    {
        Clubs=0, Diamonds=1, Hearts=2, Spades=3
    }

    public class Card
    {
        public int Value { get; set; }
        public Suit Suit { get; set; }
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

        public string Initial
        {
            get
            {
                string shortName = Value.ToString();
                if (Value > 9)
                {
                    switch (Value)
                    {
                        case 10:
                            shortName = "10 ";
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

        public string FirstName
        {
            get
            {
                string shortName = Value.ToString();
                if (Value > 10)
                {
                    switch (Value)
                    {
                        case 11:
                            shortName = "Jack";
                            break;
                        case 12:
                            shortName = "Queen";
                            break;
                        case 13:
                            shortName = "King";
                            break;
                        case 14:
                            shortName = "Ace";
                            break;
                    }
                }
                return shortName;
            }
        }

        public string FirstNamePlural
        {
            get
            {
                string shortName = Value.ToString() + "'s";
                if (Value > 10)
                {
                    switch (Value)
                    {
                        case 11:
                            shortName = "Jacks";
                            break;
                        case 12:
                            shortName = "Queens";
                            break;
                        case 13:
                            shortName = "Kings";
                            break;
                        case 14:
                            shortName = "Aces";
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
            return value + " of " + Suit.ToString();
        }
    }
}

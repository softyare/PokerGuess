using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PokerGuess.Models
{
    public enum HandType { non_holdem, pair, monster, suited, unsuited, connector, big, middle, small, mixed}

    public class Hand : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int IndexOnTable { get; set; }
        private List<Card> _cards;
        public List<Card> Cards { get { return _cards; }
                                  set {
                                        _cards = value;
                                        OnPropertyChanged(nameof(Cards));
                                      }
        }

        public List<HandType> HandTypes
        {
            get
            {
                List<HandType> result = new List<HandType>();

                // if texas holdem hand
                if (Cards.Count == 2)
                {
                    var card1 = Cards[0];
                    var card2 = Cards[1];

                    // Check if hand pair
                    if (card1.Value == card2.Value)
                    {
                        result.Add(HandType.pair);
                    }

                    // Check if monster
                    if (card1.Value > 11 && card2.Value > 11)
                    {
                        result.Add(HandType.monster);
                    }

                    // Check big, middle, small, mixed
                    if (card1.Value > 9 && card2.Value > 9)
                    {
                        result.Add(HandType.big);
                    }
                    else if (card1.Value > 5 && card1.Value < 10 && card2.Value > 5 && card2.Value < 10)
                    {
                        result.Add(HandType.middle);
                    }
                    else if (card1.Value < 6 && card2.Value < 6)
                    {
                        result.Add(HandType.small);
                    }
                    else
                    {
                        result.Add(HandType.mixed);
                    }

                    // Check suited/unsuited
                    if (card1.Suit == card2.Suit)
                    {
                        result.Add(HandType.suited);
                    }
                    else
                        result.Add(HandType.unsuited);

                    // Check if connector + else if ace case
                    if (Math.Abs(card1.Value - card2.Value) < 5 && Math.Abs(card1.Value - card2.Value) > 0)
                    {
                        result.Add(HandType.connector);
                    }
                    else if
                      (
                          (card1.Value == 14 && (
                              card2.Value == 2 ||
                              card2.Value == 3 ||
                              card2.Value == 4 ||
                              card2.Value == 5)
                          ) ||
                          (card2.Value == 14 && (
                              card1.Value == 2 ||
                              card1.Value == 3 ||
                              card1.Value == 4 ||
                              card1.Value == 5)
                          )
                      )
                    {
                        result.Add(HandType.connector);
                    }
                }
                else
                {
                    result.Add(HandType.non_holdem);
                }

                return result;
            }
        }

        public string TypeDetail 
        {
            get
            {
                // if texas holdem hand
                if (Cards.Count == 2)
                {
                    var card1 = Cards[0];
                    var card2 = Cards[1];

                    // Check if hand pair
                    if (card1.Value == card2.Value)
                    {
                        string cardsPluralName;
                        if (card1.Value > 10)
                        {
                            cardsPluralName = card1.ToString().ToLower().Split(char.Parse(" "))[0] + "s";
                        }
                        else
                        {
                            cardsPluralName = card1.ToString().ToLower().Split(char.Parse(" "))[0] + "'s";
                        }

                        return "A pair of " + cardsPluralName;
                    }

                    // Check if suited
                    if (card1.Suit == card2.Suit)
                    {
                        return "Suited " + card1.Initial + card2.Initial;
                    } else
                    {
                        return card1.Initial + card2.Initial + " off suite";
                    }

                } else
                {
                    return "Not holdem hand";
                }
            }
        }

        public string Types
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (HandTypes.Count > 0)
                {
                    foreach(HandType ht in HandTypes)
                    {
                        if (result.Length > 0)
                            result.Append(", ");
                        result.Append(ht.ToString());
                    }
                }
                return result.ToString();
            }
        }

        public override string ToString()
        {
            return TypeDetail + " [" + Types + "]";
        }
    }
}

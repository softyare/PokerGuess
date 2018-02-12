using System;
using System.Collections.Generic;
using System.Text;

namespace PokerGuess.Models
{
    public class Deck
    {
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            Cards = new List<Card>();
            for (int s = 0; s < 4; s++)
            {
                for (int o = 1; o < 14; o++)
                {
                    int v;
                    if (o == 1)
                        v = 14;
                    else
                        v = o;
                    Card c = new Card
                    {
                        Order = o,
                        Suite = (Suite)s,
                        Value = v
                    };
                    Cards.Add(c);
                }
            }
        }
    }
}

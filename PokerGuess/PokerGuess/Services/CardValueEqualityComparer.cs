using PokerGuess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PokerGuess.Services
{
    public class CardValueEqualityComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card x, Card y)
        {
            return (x.Value == y.Value);
        }

        public int GetHashCode(Card obj)
        {
            return obj.Value.GetHashCode();
        }
    }
}

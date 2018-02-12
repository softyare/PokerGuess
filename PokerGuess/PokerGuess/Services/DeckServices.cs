using System;
using System.Collections.Generic;
using System.Text;
using PokerGuess.Models;

namespace PokerGuess.Services
{
    public static class DeckServices
    {
        public static void Shuffle(Deck deck)
        {
            Random rnd = new Random();
            int numberOfShuffles = rnd.Next(200, 800);

            Card flyingCard;
            int rndIndex;
            for (int i = 0; i < numberOfShuffles; i++)
            {
                rndIndex = rnd.Next(0, deck.Cards.Count);
                flyingCard = deck.Cards[rndIndex];
                deck.Cards.RemoveAt(rndIndex);
                deck.Cards.Add(flyingCard);
            }
        }
    }
}

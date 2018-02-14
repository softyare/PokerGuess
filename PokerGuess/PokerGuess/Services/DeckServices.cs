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

        public static Card DrawCard(Deck deck)
        {
            Card card = deck.Cards[0];
            deck.Cards.Remove(card);
            return card;
        }

        public static void ReturnCard(Card card, Deck deck)
        {
            if (!deck.Cards.Contains(card))
            {
                deck.Cards.Add(card);
            }
        }
        
        public static void ReturnCards(Hand hand, Deck deck)
        {
            foreach(Card c in hand.Cards)
            {
                ReturnCard(c, deck);
            }
        }

        public static Hand DrawHoldemHand(Deck deck)
        {
            Card c1 = DrawCard(deck);
            Card c2 = DrawCard(deck);
            return new Hand
            {
                Cards = new List<Card>() { c1, c2 }
            };
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerGuess.Models
{
    public enum Combination
    {
        UnSet=-1, HighCard=0, APair=1, TwoPairs=2, ThreeOfAKind=4, Straight=5, Flush=6, FullHouse=7, FourOfAKind=8, StraightFlush=9, RoyalFlush=10
    }

    public class PokerCombination
    {
        public List<Card> SortedCards { get; set; }
        public Combination CombinationType { get; set; }
        public Hand Hand { get; set; }
        public CommunityCards Community { get; set; }

        public Card HighCard { get; set; }
        public List<Card> Kickers { get; set; }
        public List<Card> HighGroup { get; set; }
        public List<Card> LowGroup { get; set; }

        public PokerCombination(Hand hand, CommunityCards community)
        {
            this.Hand = hand;
            this.Community = community;
            SortedCards = community.Cards.Concat(hand.Cards).OrderByDescending(c => c.Value).ToList();
            Kickers = new List<Card>();
            HighGroup = new List<Card>();
            LowGroup = new List<Card>();
            CombinationType = Combination.UnSet;
        }

        public List<Card> Clubs
        {
            get
            {
                List<Card> result = new List<Card>();
                if (SortedCards != null)
                {
                    foreach (Card c in SortedCards)
                    {
                        if (c.Suit == Suit.Clubs)
                            result.Add(c);
                    }
                }
                return result;
            }
        }
        public List<Card> Diamonds
        {
            get
            {
                List<Card> result = new List<Card>();
                if (SortedCards != null)
                {
                    foreach (Card c in SortedCards)
                    {
                        if (c.Suit == Suit.Diamonds)
                            result.Add(c);
                    }
                }
                return result;
            }
        }
        public List<Card> Hearts
        {
            get
            {
                List<Card> result = new List<Card>();
                if (SortedCards != null)
                {
                    foreach (Card c in SortedCards)
                    {
                        if (c.Suit == Suit.Hearts)
                            result.Add(c);
                    }
                }
                return result;
            }
        }
        public List<Card> Spades
        {
            get
            {
                List<Card> result = new List<Card>();
                if (SortedCards != null)
                {
                    foreach (Card c in SortedCards)
                    {
                        if (c.Suit == Suit.Spades)
                            result.Add(c);
                    }
                }
                return result;
            }
        }

        public override string ToString()
        {
            var name = new StringBuilder();
            if (CombinationType != Combination.UnSet )
            {
                switch(CombinationType)
                {
                    case Combination.RoyalFlush:
                        name.Append("Royal flush of ");
                        name.Append(HighCard.Suit.ToString().ToLower());
                        return name.ToString();
                    case Combination.StraightFlush:
                        name.Append("Straight flush to ");
                        name.Append(HighCard.ToString().ToLower());
                        return name.ToString();
                    case Combination.FourOfAKind:
                        name.Append("Four of a kind ");
                        name.Append(HighCard.FirstNamePlural.ToLower());
                        name.Append( " with ");
                        name.Append(Kickers[0].Initial);
                        name.Append(" kicker");
                        return name.ToString();
                    case Combination.FullHouse:
                        name.Append("Full house ");
                        name.Append(HighGroup[0].FirstNamePlural);
                        name.Append(" full of ");
                        name.Append(LowGroup[0].FirstNamePlural);
                        return name.ToString();
                    case Combination.Flush:
                        name.Append("Flush of ");
                        name.Append(HighCard.Suit.ToString().ToLower());
                        name.Append(" to ");
                        name.Append(HighCard.FirstName);
                        return name.ToString();
                    case Combination.Straight:
                        name.Append("Straight to ");
                        name.Append(HighCard.FirstName);
                        return name.ToString();
                    case Combination.ThreeOfAKind:
                        name.Append("Three of a kind ");
                        name.Append(HighGroup[0].FirstNamePlural);
                        name.Append(" with ");
                        name.Append(Kickers[0].Initial);
                        name.Append(", ");
                        name.Append(Kickers[1].Initial);
                        name.Append(" kickers");
                        return name.ToString();
                    case Combination.TwoPairs:
                        name.Append("Two pairs ");
                        name.Append(HighGroup[0].FirstNamePlural);
                        name.Append(" and ");
                        name.Append(LowGroup[0].FirstNamePlural);
                        name.Append(" with ");
                        name.Append(Kickers[0].Initial);
                        name.Append(" kicker");
                        return name.ToString();
                    case Combination.APair:
                        name.Append("A pair of ");
                        name.Append(HighGroup[0].FirstNamePlural);
                        name.Append(" with ");
                        name.Append(Kickers[0].Initial);
                        name.Append(", ");
                        name.Append(Kickers[1].Initial);
                        name.Append(", ");
                        name.Append(Kickers[2].Initial);
                        name.Append(" kickers");
                        return name.ToString();
                    case Combination.HighCard:
                        name.Append("High card ");
                        name.Append(HighCard.FirstName);
                        name.Append(" with ");
                        name.Append(Kickers[0].Initial);
                        name.Append(", ");
                        name.Append(Kickers[1].Initial);
                        name.Append(", ");
                        name.Append(Kickers[2].Initial);
                        name.Append(", ");
                        name.Append(Kickers[3].Initial);
                        name.Append(" kickers");
                        return name.ToString();
                }
            }
            return "Not set";
        }
    }
}

using PokerGuess.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PokerGuess.Services
{
    public static class TableServices
    {
        public static bool PutHandsOnTable(Table table, int numberOfHands)
        {
            if ((table.Hands.Count + numberOfHands) <= table.MaxHands)
            {
                for (int i = 0; i < numberOfHands; i++)
                {
                    Hand h = DeckServices.DrawHoldemHand(table.DeckOfCards);
                    h.IndexOnTable = i;
                    h.Table = table;
                    table.Hands.Add(h);
                }
                return true;
            }
            return false;
        }

        public static bool RemoveAllHandsFromTable(Table table)
        {
            if (table.Hands != null)
            {
                foreach (Hand h in table.Hands)
                {
                    DeckServices.ReturnCards(h, table.DeckOfCards);
                }
                DeckServices.ReturnCards(table.Community, table.DeckOfCards);
                table.Hands.Clear();
                DeckServices.Shuffle(table.DeckOfCards);
                return true;
            }
            return false;
        }

        public static bool DealFlop(Table table)
        {
            if (table.Community.Cards.Count < 1)
            {
                table.Community.Cards.Add(DeckServices.DrawCard(table.DeckOfCards));
                table.Community.Cards.Add(DeckServices.DrawCard(table.DeckOfCards));
                table.Community.Cards.Add(DeckServices.DrawCard(table.DeckOfCards));
                return true;
            }
            return false;
        }

        public static bool DealTurn(Table table)
        {
            if (table.Community.Cards.Count == 3)
            {
                table.Community.Cards.Add(DeckServices.DrawCard(table.DeckOfCards));
                return true;
            }
            return false;
        }

        public static bool DealRiver(Table table)
        {
            if (table.Community.Cards.Count == 4)
            {
                table.Community.Cards.Add(DeckServices.DrawCard(table.DeckOfCards));
                return true;
            }
            return false;
        }

        public static void ClearCommunityCards(Table table)
        {
            DeckServices.ReturnCards(table.Community, table.DeckOfCards);
            table.Community.Cards.Clear();
        }

        public static bool GetPokerCombinations(Table table)
        {
            // Ako ima više od 2 handa na stolu i community cards barem flop.
            if (table.Hands.Count > 1 && table.Community.Cards.Count > 2)
            {
                table.PokerCombinations.Clear();
                foreach (Hand h in table.Hands)
                {
                    try
                    {
                        var pc = CreatePokerCombination(h, table.Community);
                        table.PokerCombinations.Add(pc);
                    }
                    catch (Exception e)
                    {
                        Debug.Write("Kreacija poker kombinacija nije uspjela! " + e.Message);
                    }
                }
            }
            return table.PokerCombinations.Count > 0;
        }

        public static PokerCombination CreatePokerCombination(Hand hand, CommunityCards community)
        {
            PokerCombination result = new PokerCombination(hand, community);

            var straightGroups = GetStraightGroups(result);
            var flushGroups = GetFlushGroups(result);
            var sameValueGroups = GetSameValueGroups(result);

            // Check for Straight Flush
            if (straightGroups.Count  > 0 && flushGroups.Count > 0)
            {
                List<List<Card>> sflushs = new List<List<Card>>();
                foreach (var flush in flushGroups)
                {
                    var str = GetStraightGroups(flush);
                    if (str.Count > 0)
                        sflushs.Add(str[0]);
                }
                if(sflushs.Count > 0)
                {
                    if (sflushs[0][0].Value == 14)
                        result.CombinationType = Combination.RoyalFlush;
                    else
                        result.CombinationType = Combination.StraightFlush;
                    result.HighCard = sflushs[0][0];
                    return result;
                }
            }
            // Check for quads
            if(sameValueGroups.Count > 0)
                foreach(var group in sameValueGroups)
                {
                    if (group.Count > 3)
                    {
                        result.CombinationType = Combination.FourOfAKind;
                        result.HighCard = group[0];
                        result.HighGroup = group;
                        var remain = result.SortedCards.Except(group).ToList();
                        result.Kickers.Add(remain[0]);
                        return result;
                    }
                }
            // Check for full house
            if (sameValueGroups.Count > 1)
            {
                var biggest = sameValueGroups.OrderByDescending(g => g.Count).ToList()[0];
                if (biggest.Count > 2)
                {
                    result.CombinationType = Combination.FullHouse;
                    result.HighCard = biggest[0];
                    result.HighGroup = biggest;
                    result.LowGroup = sameValueGroups.OrderByDescending(g => g.Count).ToList()[1];
                    return result;
                }
            }
            // Return if flush
            if (flushGroups.Count > 0)
            {
                result.CombinationType = Combination.Flush;
                result.HighGroup = flushGroups[0];
                result.HighCard = result.HighGroup[0];
                return result;
            }
            // Return if straight
            if (straightGroups.Count > 0)
            {
                result.CombinationType = Combination.Straight;
                result.HighGroup = straightGroups[0];
                result.HighCard = result.HighGroup[0];
                return result;
            }
            // Return if tris
            if (sameValueGroups.Count == 1 && sameValueGroups[0].Count == 3)
            {
                result.CombinationType = Combination.ThreeOfAKind;
                result.HighCard = sameValueGroups[0][0];
                result.HighGroup = sameValueGroups[0];
                var remain = result.SortedCards.Except(result.HighGroup).ToList();
                result.Kickers.Add(remain[0]);
                result.Kickers.Add(remain[1]);
                return result;
            }
            // Return if 2 pairs
            if (sameValueGroups.Count > 1)
            {
                result.CombinationType = Combination.TwoPairs;
                result.HighCard = sameValueGroups[0][0];
                result.HighGroup = sameValueGroups[0];
                result.LowGroup = sameValueGroups[1];
                var remain = result.SortedCards.Except(result.HighGroup).Except(result.LowGroup).ToList();
                result.Kickers.Add(remain[0]);
                return result;
            }
            // Return if a pair
            if (sameValueGroups.Count == 1)
            {
                result.CombinationType = Combination.APair;
                result.HighCard = sameValueGroups[0][0];
                result.HighGroup = sameValueGroups[0];
                var remain = result.SortedCards.Except(result.HighGroup).ToList();
                result.Kickers.Add(remain[0]);
                result.Kickers.Add(remain[1]);
                result.Kickers.Add(remain[2]);
                return result;
            }
            // If nothing else, it's a high card
            result.CombinationType = Combination.HighCard;
            result.HighCard = result.SortedCards[0];
            result.Kickers.Add(result.SortedCards[1]);
            result.Kickers.Add(result.SortedCards[2]);
            result.Kickers.Add(result.SortedCards[3]);
            result.Kickers.Add(result.SortedCards[4]);
            return result;
        }

        static List<List<Card>> GetSameValueGroups(PokerCombination combination)
        {
            List<List<Card>> result = new List<List<Card>>();
            var dist = combination.SortedCards.Distinct(new CardValueEqualityComparer());
            try
            {
                foreach (Card c in dist)
                {
                    List<Card> group = combination.SortedCards.Where(card => card.Value == c.Value).ToList();
                    if (group.Count > 1)
                    {
                        result.Add(group);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Sortiranje po vrijednosti nije uspjelo! " + e.Message);
            }
            return result;
        }

        static List<List<Card>> GetFlushGroups(PokerCombination combination)
        {
            List<List<Card>> result = new List<List<Card>>();
            List<Card> groupClubs = new List<Card>();
            List<Card> groupDiamonds = new List<Card>();
            List<Card> groupHearts = new List<Card>();
            List<Card> groupSpades = new List<Card>();
            foreach (Card c in combination.SortedCards)
            {
                switch(c.Suit)
                {
                    case Suit.Clubs:
                        groupClubs.Add(c);
                        break;
                    case Suit.Diamonds:
                        groupDiamonds.Add(c);
                        break;
                    case Suit.Hearts:
                        groupHearts.Add(c);
                        break;
                    case Suit.Spades:
                        groupSpades.Add(c);
                        break;
                }
            }

            if (groupClubs.Count > 4)
                result.Add(groupClubs);
            if (groupDiamonds.Count > 4)
                result.Add(groupDiamonds);
            if (groupHearts.Count > 4)
                result.Add(groupHearts);
            if (groupSpades.Count > 4)
                result.Add(groupSpades);

            return result;
        }

        static List<List<Card>> GetStraightGroups(PokerCombination combination)
        {
            List<List<Card>> result = new List<List<Card>>();
            List<Card> group = new List<Card>();
            Card lastCard = new Card();
            int count = 1;
            var dist = combination.SortedCards.Distinct(new CardValueEqualityComparer()).ToList();
            foreach (Card c in dist)
            {
                if (count == 1)
                {
                    lastCard = c;
                }
                else
                {
                    if (c.Value == lastCard.Value-1)
                    {
                        if(group.Count == 0)
                            group.Add(lastCard);
                        group.Add(c);
                        lastCard = c;
                    }
                    else
                    {
                        if(group.Count > 4)
                        {
                            result.Add(group);
                        }
                        group = new List<Card>();
                        lastCard = c;
                    }

                    if(count == dist.Count)
                    {
                        if (c.Value == 2 && combination.SortedCards[0].Value == 14)
                        {
                            group.Add(combination.SortedCards[0]);
                        }
                        if (group.Count > 4)
                        {
                            result.Add(group);
                        }
                    }
                }
                count++;
            }
            return result;
        }

        static List<List<Card>> GetStraightGroups(List<Card> cards)
        {
            List<List<Card>> result = new List<List<Card>>();
            List<Card> group = new List<Card>();
            Card lastCard = new Card();
            int count = 1;
            var dist = cards.Distinct(new CardValueEqualityComparer()).ToList();
            foreach (Card c in dist)
            {
                if (count == 1)
                {
                    lastCard = c;
                }
                else
                {
                    if (c.Value == lastCard.Value - 1)
                    {
                        if (group.Count == 0)
                            group.Add(lastCard);
                        group.Add(c);
                        lastCard = c;
                    }
                    else
                    {
                        if (group.Count > 4)
                        {
                            result.Add(group);
                        }
                        group = new List<Card>();
                        lastCard = c;
                    }

                    if (count == dist.Count)
                    {
                        if (c.Value == 2 && cards[0].Value == 14)
                        {
                            group.Add(cards[0]);
                        }
                        if (group.Count > 4)
                        {
                            result.Add(group);
                        }
                    }
                }
                count++;
            }
            return result;
        }

        static List<Card> GetSameSuitCards(List<Card> allCards, Suit suit)
        {
            var result = new List<Card>();
            foreach(Card c in allCards)
            {
                if (c.Suit == suit)
                {
                    result.Add(c);
                }
            }
            return result;
        }

        static List<Card> GetRemainingCards(List<Card> allCards, List<Card> extra)
        {
            var remainingCards = new List<Card>();
            foreach (Card c in allCards)
            {
                if (c.Value != extra[0].Value)
                    remainingCards.Add(c);
            }
            return remainingCards;
        }
    }
}
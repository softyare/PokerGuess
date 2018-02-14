using PokerGuess.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
                foreach(Hand h in table.Hands)
                {
                    DeckServices.ReturnCards(h, table.DeckOfCards);
                }
                table.Hands.Clear();
                DeckServices.Shuffle(table.DeckOfCards);
                return true;
            }
            return false;
        }
    }
}

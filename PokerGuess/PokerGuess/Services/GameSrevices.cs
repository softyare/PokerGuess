using PokerGuess.Models;
using PokerGuess.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerGuess.Services
{
    public static class GameServices
    {
        public static GamePageVM GamePageViewModel { get; set; }
        public static int StartingBet = 10;
        public static Hand SelectedHand { get; set; }
        public static Table MainTable { get; set; }
        public static int Bet { get; set; }
        public static int CurrentScore { get; set; }
        public static int BestScore { get; set; }
    }
}

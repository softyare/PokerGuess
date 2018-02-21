using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PokerGuess.Models;
using PokerGuess.Services;
using Xamarin.Forms;
using System.Windows.Input;

namespace PokerGuess.ViewModels
{
    public class GamePageVM : INotifyPropertyChanged
    {
        public TableViewVM TableVm { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Table MainTable { get { return Services.GameServices.MainTable; }
                                 set { Services.GameServices.MainTable = value; }
        }

        public Command DealNewRandomHandsCommand { get; set; }
        public Command DealTurnCommand { get; set; }
        public Command DealRiverCommand { get; set; }
        public Command DealFlopCommand { get; set; }

        private ImageSource bkgImageSource = ImageSource.FromResource("PokerGuess.Resources.BlueGradBkg.png");
        public ImageSource BkgImageSource
        {
            get { return bkgImageSource; }
            set
            {
                bkgImageSource = value;
                OnPropertyChanged(nameof(BkgImageSource));
            }
        }

        private string currentScoreText;
        public string CurrentScoreText
        {
            get { return currentScoreText; }
            set { currentScoreText = value;
                OnPropertyChanged(nameof(CurrentScoreText));
            }
        }

        private string bestScoreText;
        public string BestScoreText
        {
            get { return bestScoreText; }
            set
            {
                bestScoreText = value;
                OnPropertyChanged(nameof(BestScoreText));
            }
        }

        private string handsInfoText;
        public string HandsInfoText
        {
            get { return handsInfoText; }
            set
            {
                handsInfoText = value;
                OnPropertyChanged(nameof(HandsInfoText));
            }
        }

        private string gameInfoText;
        public string GameInfoText
        {
            get { return gameInfoText; }
            set
            {
                gameInfoText = value;
                OnPropertyChanged(nameof(GameInfoText));
            }
        }

        private string _mainButtonText;
        public string MainButtonText
        {
            get { return _mainButtonText; }
            set
            {
                _mainButtonText = value;
                OnPropertyChanged(nameof(MainButtonText));
            }
        }

        private Command _mainButtonCommand;
        public Command MainButtonCommand
        {
            get { return _mainButtonCommand; }
            set
            {
                _mainButtonCommand = value;
                OnPropertyChanged(nameof(MainButtonCommand));
            }
        }

        private Color _mainButtonBkgColor;
        public Color MainButtonBkgColor
        {
            get { return _mainButtonBkgColor; }
            set
            {
                _mainButtonBkgColor = value;
                OnPropertyChanged(nameof(MainButtonBkgColor));
            }
        }

        public GamePageVM()
        {
            DealNewRandomHandsCommand = new Command(DealNewRandomHands);
            DealTurnCommand = new Command(DealTurn);
            DealRiverCommand = new Command(DealRiver);
            DealFlopCommand = new Command(DealFlop);

            // KREIRANJE TABLE
            var t = new Table(6)
            {
                DeckOfCards = new Deck(),
                State = TableState.Empty,
                HasSelectedHands = true
            };

            Services.GameServices.MainTable = t;
            Services.GameServices.CurrentScore = 0;

            CurrentScoreText = "Current score: " + GameServices.CurrentScore.ToString();
            BestScoreText = "Best score: " + GameServices.BestScore.ToString();
        }

        public void SetMainButton()
        {
            switch(TableVm.MainTable.State)
            {
                case TableState.Empty:
                    MainButtonCommand = DealNewRandomHandsCommand;
                    MainButtonText = "Deal new hands";
                    MainButtonBkgColor = Color.Beige;
                    break;
                case TableState.PreFlop:
                    MainButtonCommand = DealFlopCommand;
                    MainButtonText = "Deal Flop";
                    MainButtonBkgColor = Color.Yellow;
                    break;
                case TableState.Flop:
                    MainButtonCommand = DealTurnCommand;
                    MainButtonText = "Deal Turn";
                    MainButtonBkgColor = Color.GreenYellow;
                    break;
                case TableState.Turn:
                    MainButtonCommand = DealRiverCommand;
                    MainButtonText = "Deal River";
                    MainButtonBkgColor = Color.LightGreen;
                    break;
                case TableState.River:
                    MainButtonCommand = DealNewRandomHandsCommand;
                    MainButtonText = "Deal new hands";
                    MainButtonBkgColor = Color.Beige;
                    break;
            }
        }

        private void DealNewRandomHands()
        {
            Services.GameServices.SelectedHand = null;
            Services.TableServices.RemoveAllHandsFromTable(TableVm.MainTable);
            Services.TableServices.ClearCommunityCards(TableVm.MainTable);
            Services.TableServices.PutHandsOnTable(TableVm.MainTable, new Random().Next(2, TableVm.MainTable.MaxHands +1));
            TableVm.OnPropertyChanged(nameof(TableVm.MainTable));
            TableVm.RefreshHandViews();
            TableVm.CommunityVM.RefreshImageSources();
            MainTable.OnPropertyChanged(nameof(TableVm.MainTable.Hands));
            MainTable.State = TableState.PreFlop;
            MainTable.HasSelectedHands = false;
            GetHandsInfo();
            GameInfoText = "Select your hand on table.";
            SetMainButton();
            Services.GameServices.Bet = Services.GameServices.StartingBet;
        }

        private void ClearInfo()
        {
            HandsInfoText = "";
            GameInfoText = "";
        }

        private void GetCombinationsInfo()
        {
            if (Services.TableServices.GetPokerCombinations(TableVm.MainTable))
            {
                var text = new StringBuilder();
                foreach (PokerCombination pc in TableVm.MainTable.PokerCombinations)
                {
                    text.Append("Hand " + (pc.Hand.IndexOnTable + 1).ToString() + ": " + pc.ToString() + "\n");
                }
                HandsInfoText = text.ToString();
            }  
            else
            {
                HandsInfoText = "";
            }
        }

        private void GetHandsInfo()
        {
            if (TableVm.MainTable.Hands.Count > 0)
            {
                var text = new StringBuilder();
                foreach (Hand h in TableVm.MainTable.Hands)
                {
                    text.Append("Hand " + (h.IndexOnTable +1).ToString() + ": " + h.ToString() + "\n");
                }
                HandsInfoText = text.ToString();
            }
            else
            {
                HandsInfoText = "";
            }
        }

        public void GetGameInfo()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Your hand: ");
            result.Append(Services.GameServices.SelectedHand.TypeDetail + "\n");
            if((int)Services.GameServices.MainTable.State > 1)
            {
                result.Append("Best hands: ");
                foreach (Hand h in GameServices.MainTable.WinningHands)
                {
                    result.Append(MainTable.PokerCombinations[0].Hand.TypeDetail + " | ");
                }
                result.Remove(result.Length - 3, 3);
                result.Append("\n");
            }
            if (GameServices.MainTable.State == TableState.River)
            {
                if (GameServices.CurrentScore > 0)
                {
                    result.Append("Win: $" + Services.GameServices.Bet + "\n");
                    result.Append("Total win: $" + Services.GameServices.CurrentScore);
                }
                else
                {
                    result.Append("Your lost! Game over.");
                }
            } else
            {
                result.Append("Your bet: $" + Services.GameServices.Bet);
            }
            GameInfoText = result.ToString();
        }

        private void DealFlop()
        {
            Services.TableServices.DealFlop(TableVm.MainTable);
            TableVm.CommunityVM.RefreshImageSources();
            TableVm.MainTable.State = TableState.Flop;
            GetCombinationsInfo();
            GetGameInfo();
            SetMainButton();
        }

        private void DealTurn()
        {
            Services.TableServices.DealTurn(TableVm.MainTable);
            TableVm.CommunityVM.RefreshImageSources();
            TableVm.MainTable.State = TableState.Turn;
            GetCombinationsInfo();
            GetGameInfo();
            SetMainButton();
        }
        private void DealRiver()
        {
            Services.TableServices.DealRiver(TableVm.MainTable);
            TableVm.CommunityVM.RefreshImageSources();
            TableVm.MainTable.State = TableState.River;
            GetCombinationsInfo();
            CheckGameEnd();
            GetGameInfo();
            CurrentScoreText = "Current score: " + GameServices.CurrentScore.ToString();
            BestScoreText = "Best score: " + GameServices.BestScore.ToString();
            SetMainButton();
        }

        private void CheckGameEnd()
        {
            bool isWinning = false;
            foreach(Hand h in GameServices.MainTable.WinningHands)
            {
                if (h.TypeDetail == GameServices.SelectedHand.TypeDetail)
                {
                    isWinning = true;
                    break;
                }
            }
            if (isWinning)
            {
                GameServices.CurrentScore = GameServices.CurrentScore + GameServices.Bet;
            } 
            else
            {
                if (GameServices.CurrentScore > GameServices.BestScore)
                    GameServices.BestScore = GameServices.CurrentScore;
                GameServices.CurrentScore = 0;
            }
            
        }
    }
}

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
    class GamePageVM : INotifyPropertyChanged
    {
        private PokerGuess.ViewModels.TableViewVM tableVm;
        public TableViewVM TableVm { get => tableVm; set => tableVm = value; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        }

        public void SetMainButton()
        {
            switch(tableVm.MainTable.State)
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
                    MainButtonBkgColor = Color.Orange;
                    break;
                case TableState.Turn:
                    MainButtonCommand = DealRiverCommand;
                    MainButtonText = "Deal River";
                    MainButtonBkgColor = Color.PaleVioletRed;
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
            Services.TableServices.RemoveAllHandsFromTable(tableVm.MainTable);
            Services.TableServices.ClearCommunityCards(tableVm.MainTable);
            Services.TableServices.PutHandsOnTable(tableVm.MainTable, new Random().Next(2, tableVm.MainTable.MaxHands +1));
            tableVm.OnPropertyChanged(nameof(tableVm.MainTable));
            tableVm.RefreshHandViews();
            tableVm.CommunityVM.RefreshImageSources();
            tableVm.MainTable.OnPropertyChanged(nameof(tableVm.MainTable.Hands));
            tableVm.MainTable.State = TableState.PreFlop;
            GetHandsInfo();
            SetMainButton();
        }

        private void ClearHandsInfo()
        {
            HandsInfoText = "";
        }

        private void GetCombinationsInfo()
        {
            if (Services.TableServices.GetPokerCombinations(tableVm.MainTable))
            {
                var text = new StringBuilder();
                tableVm.MainTable.PokerCombinations.Sort();
                tableVm.MainTable.PokerCombinations.Reverse();
                foreach (PokerCombination pc in tableVm.MainTable.PokerCombinations)
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
            if (tableVm.MainTable.Hands.Count > 0)
            {
                var text = new StringBuilder();
                foreach (Hand h in tableVm.MainTable.Hands)
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

        private void DealFlop()
        {
            Services.TableServices.DealFlop(tableVm.MainTable);
            tableVm.CommunityVM.RefreshImageSources();
            tableVm.MainTable.State = TableState.Flop;
            GetCombinationsInfo();
            SetMainButton();
        }
        private void DealTurn()
        {
            Services.TableServices.DealTurn(tableVm.MainTable);
            tableVm.CommunityVM.RefreshImageSources();
            tableVm.MainTable.State = TableState.Turn;
            GetCombinationsInfo();
            SetMainButton();
        }
        private void DealRiver()
        {
            Services.TableServices.DealRiver(tableVm.MainTable);
            tableVm.CommunityVM.RefreshImageSources();
            tableVm.MainTable.State = TableState.River;
            GetCombinationsInfo();
            SetMainButton();
        }
    }
}

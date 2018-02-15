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

        public GamePageVM()
        {
            DealNewRandomHandsCommand = new Command(DealNewRandomHands);
            DealTurnCommand = new Command(DealTurn);
            DealRiverCommand = new Command(DealRiver);
            DealFlopCommand = new Command(DealFlop);
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
        }

        private void DealFlop()
        {
            Services.TableServices.DealFlop(tableVm.MainTable);
            tableVm.OnPropertyChanged(nameof(tableVm.CommunityVM));
            tableVm.CommunityVM.RefreshImageSources();
        }
        private void DealTurn()
        {
            Services.TableServices.DealTurn(tableVm.MainTable);
            tableVm.OnPropertyChanged(nameof(tableVm.CommunityVM));
            tableVm.CommunityVM.RefreshImageSources();
        }
        private void DealRiver()
        {
            Services.TableServices.DealRiver(tableVm.MainTable);
            tableVm.OnPropertyChanged(nameof(tableVm.CommunityVM));
            tableVm.CommunityVM.RefreshImageSources();
        }
    }
}

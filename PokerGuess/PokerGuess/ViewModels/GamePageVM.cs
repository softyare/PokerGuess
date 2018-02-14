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

        public GamePageVM()
        {
            DealNewRandomHandsCommand = new Command(DealNewRandomHands);
        }

        private void DealNewRandomHands()
        {
            Services.TableServices.RemoveAllHandsFromTable(tableVm.MainTable);
            Services.TableServices.PutHandsOnTable(tableVm.MainTable, new Random().Next(2, tableVm.MainTable.MaxHands));
            tableVm.OnPropertyChanged(nameof(tableVm.MainTable));
            tableVm.RefreshHandViews();
            tableVm.MainTable.OnPropertyChanged(nameof(tableVm.MainTable.Hands));
        }
    }
}

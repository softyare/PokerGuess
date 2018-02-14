using PokerGuess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokerGuess.ViewModels
{
    public class TableViewVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Table MainTable { get; set; }

        public HandViewVM Hand1vm { get; set; }
        public HandViewVM Hand2vm { get; set; }
        public HandViewVM Hand3vm { get; set; }
        public HandViewVM Hand4vm { get; set; }
        public HandViewVM Hand5vm { get; set; }
        public HandViewVM Hand6vm { get; set; }

        public TableViewVM()
        {
            MainTable = new Table(6)
            {
                DeckOfCards = new Deck(),
                State = TableState.Empty
            };
            Services.DeckServices.Shuffle(MainTable.DeckOfCards);
            Services.TableServices.PutHandsOnTable(MainTable, new Random().Next(2, MainTable.MaxHands + 1));
            RefreshHandViews();
        }

        public void RefreshHandViews()
        {
            Hand1vm = null;
            OnPropertyChanged(nameof(Hand1vm));
            Hand2vm = null;
            OnPropertyChanged(nameof(Hand2vm));
            Hand3vm = null;
            OnPropertyChanged(nameof(Hand3vm));
            Hand4vm = null;
            OnPropertyChanged(nameof(Hand4vm));
            Hand5vm = null;
            OnPropertyChanged(nameof(Hand5vm));
            Hand6vm = null;
            OnPropertyChanged(nameof(Hand6vm));
            try
            {
                Hand1vm = new HandViewVM(MainTable.Hands[0]);
                OnPropertyChanged(nameof(Hand1vm));
                Hand2vm = new HandViewVM(MainTable.Hands[1]);
                OnPropertyChanged(nameof(Hand2vm));
                Hand3vm = new HandViewVM(MainTable.Hands[2]);
                OnPropertyChanged(nameof(Hand3vm));
                Hand4vm = new HandViewVM(MainTable.Hands[3]);
                OnPropertyChanged(nameof(Hand4vm));
                Hand5vm = new HandViewVM(MainTable.Hands[4]);
                OnPropertyChanged(nameof(Hand5vm));
                Hand6vm = new HandViewVM(MainTable.Hands[5]);
                OnPropertyChanged(nameof(Hand6vm));
            }
            catch (Exception)
            {
                
            }
        }
    }
}

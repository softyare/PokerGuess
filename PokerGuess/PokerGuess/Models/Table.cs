using PokerGuess.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PokerGuess.Models
{
    public enum TableState { Empty, PreFlop, Flop, Turn, River }

    public class Table : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Deck DeckOfCards { get; set; }
        public int MaxHands { get; private set; }
        private TableState _state;
        public TableState State { get => _state;
                                  set {
                                        _state = value;
                                        OnPropertyChanged(nameof(State));
                                      }
        }

        private List<Hand> _hands;
        public List<Hand> Hands
        {
            get => _hands;
            set
            {
                _hands = value;
                OnPropertyChanged(nameof(Hands));
            }
        }

        public Table(int maxHands)
        {
            Hands = new List<Hand>();
            State = TableState.Empty;
            MaxHands = maxHands;
            DeckOfCards = new Deck();
            DeckServices.Shuffle(DeckOfCards);
        }
    }
}

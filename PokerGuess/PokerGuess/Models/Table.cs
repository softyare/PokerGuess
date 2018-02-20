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
        public CommunityCards Community { get; set; }
        public List<PokerCombination> PokerCombinations { get; set; }
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

        private bool hasSelectedHands;
        public bool HasSelectedHands
        {
            get
            {
                hasSelectedHands = false;
                foreach (Hand h in Hands)
                {
                    if (h.IsSelected)
                    {
                        hasSelectedHands = true;
                    }
                }
                return hasSelectedHands;
            }
            set
            {
                hasSelectedHands = value;
                OnPropertyChanged(nameof(HasSelectedHands));
            }
        }

        public Table(int maxHands)
        {
            Hands = new List<Hand>();
            PokerCombinations = new List<PokerCombination>();
            State = TableState.Empty;
            MaxHands = maxHands;
            DeckOfCards = new Deck();
            Community = new CommunityCards();
            DeckServices.Shuffle(DeckOfCards);
        }
    }
}

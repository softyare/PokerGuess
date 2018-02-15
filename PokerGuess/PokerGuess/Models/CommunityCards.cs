using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PokerGuess.Models
{
    public class CommunityCards : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Card> _cards;
        public List<Card> Cards {
            get { return _cards; }
            set {
                _cards = value;
                OnPropertyChanged(nameof(Cards));
            }
        }

        public CommunityCards()
        {
            Cards = new List<Card>();
        }

        public List<Card> Flop { get {
                if(Cards.Count >= 3)
                    return Cards.GetRange(0, 3);
                return null;
            }
        }

        public Card Turn { get {
                if (Cards.Count >= 4)
                    return Cards[3];
                return null;
            }
        }

        public Card River
        {
            get
            {
                if (Cards.Count >= 5)
                    return Cards[4];
                return null;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PokerGuess.Models;
using PokerGuess.Services;
using Xamarin.Forms;

namespace PokerGuess.ViewModels
{
    class TablePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Deck deck;
        private List<string> cardsByName;

        public List<string> Cards
        {
            get { return cardsByName; }
            set
            {
                cardsByName = value;
                OnPropertyChanged(nameof(Cards));
            }
        }

        public Card FirstCard { get; set; }
        public ImageSource CardImage
        {
            get
            {
                return ImageSource.FromResource(FirstCard.ImagePath);
            }
        }
        
        public TablePageVM()
        {
            deck = new Deck();
            DeckServices.Shuffle(deck);
            Cards = GetCardsByName(deck);
            FirstCard = deck.Cards[0];
            OnPropertyChanged(nameof(FirstCard));
        }

        public List<string> GetCardsByName(Deck d)
        {
            List<string> result = new List<string>();
            foreach (Card c in d.Cards)
            {
                result.Add(c.ToString());
            }
            OnPropertyChanged(nameof(Cards));
            return result;
        }
            
    }
}

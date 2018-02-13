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
        public ImageSource FirstCardImage
        {
            get
            {
                return ImageSource.FromResource(FirstCard.SmallImagePath);
            }
        }

        public Card SecondCard { get; set; }
        public ImageSource SecondCardImage
        {
            get
            {
                return ImageSource.FromResource(SecondCard.SmallImagePath);
            }
        }

        public TablePageVM()
        {
            deck = new Deck();
            DeckServices.Shuffle(deck);
            Cards = GetCardsByName(deck);
            FirstCard = deck.Cards[0];
            SecondCard = deck.Cards[1];
            OnPropertyChanged(nameof(FirstCard));
            OnPropertyChanged(nameof(SecondCard));
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

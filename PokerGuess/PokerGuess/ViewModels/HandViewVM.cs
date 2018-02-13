using System;
using System.Collections.Generic;
using System.ComponentModel;
using PokerGuess.Models;
using Xamarin.Forms;

namespace PokerGuess.ViewModels
{
    public class HandViewVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Hand _hand;
        public Hand CurrnetHand
        {
            get { return _hand; }
            set
            {
                _hand = value;
                OnPropertyChanged(nameof(CurrnetHand));
            }
        }

        public HandViewVM(Hand hand)
        {
            CurrnetHand = hand;
        }

        public ImageSource Card1Image
        {
            get
            {
                if (CurrnetHand.Cards.Count > 0)
                {
                    return ImageSource.FromResource(CurrnetHand.Cards[0].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card2Image
        {
            get
            {
                if (CurrnetHand.Cards.Count > 1)
                {
                    return ImageSource.FromResource(CurrnetHand.Cards[1].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card3Image
        {
            get
            {
                if (CurrnetHand.Cards.Count > 2)
                {
                    return ImageSource.FromResource(CurrnetHand.Cards[2].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card4Image
        {
            get
            {
                if (CurrnetHand.Cards.Count > 3)
                {
                    return ImageSource.FromResource(CurrnetHand.Cards[3].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card5Image
        {
            get
            {
                if (CurrnetHand.Cards.Count > 4)
                {
                    return ImageSource.FromResource(CurrnetHand.Cards[4].SmallImagePath);
                }
                else
                    return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        public Hand CurrentHand
        {
            get { return _hand; }
            set
            {
                _hand = value;
                OnPropertyChanged(nameof(CurrentHand));
                OnPropertyChanged(nameof(Card1Image));
                OnPropertyChanged(nameof(Card2Image));
            }
        }

        public ImageSource HandBkgImageSource { get { return ImageSource.FromResource("PokerGuess.Resources.HandHolder.png"); } }

        public Command HandTapCommand { get; set; }

        public HandViewVM(Hand hand)
        {
            CurrentHand = hand;
            HandTapCommand = new Command(HandTap);
        }

        private void HandTap(object obj)
        {
            if (Services.GameServices.SelectedHand != null)
            {
                if (Services.GameServices.SelectedHand != CurrentHand)
                {
                    Services.GameServices.SelectedHand.IsSelected = false;
                    switch (Services.GameServices.MainTable.State)
                    {
                        case TableState.Flop:
                            Services.GameServices.Bet = 5;
                            break;
                        case TableState.Turn:
                            Services.GameServices.Bet = 2;
                            break;

                    }
                }
            }
            CurrentHand.IsSelected = true;
            Services.GameServices.MainTable.HasSelectedHands = true;
            Services.GameServices.SelectedHand = CurrentHand;
            Services.GameServices.GamePageViewModel.GetGameInfo();
        }

        public ImageSource Card1Image
        {
            get
            {
                if (CurrentHand.Cards.Count > 0)
                {
                    return ImageSource.FromResource(CurrentHand.Cards[0].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card2Image
        {
            get
            {
                if (CurrentHand.Cards.Count > 1)
                {
                    return ImageSource.FromResource(CurrentHand.Cards[1].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card3Image
        {
            get
            {
                if (CurrentHand.Cards.Count > 2)
                {
                    return ImageSource.FromResource(CurrentHand.Cards[2].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card4Image
        {
            get
            {
                if (CurrentHand.Cards.Count > 3)
                {
                    return ImageSource.FromResource(CurrentHand.Cards[3].SmallImagePath);
                }
                else
                    return null;
            }
        }
        public ImageSource Card5Image
        {
            get
            {
                if (CurrentHand.Cards.Count > 4)
                {
                    return ImageSource.FromResource(CurrentHand.Cards[4].SmallImagePath);
                }
                else
                    return null;
            }
        }
    }
}

using PokerGuess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PokerGuess.ViewModels
{
    public class CommunityViewVM : INotifyPropertyChanged
    {
        private ImageSource flop1, flop2, flop3, turn, river;

        public event PropertyChangedEventHandler PropertyChanged;
        public CommunityCards Community { get; set; }
        public ImageSource Flop1 { get { return flop1; } set { flop1 = value; OnPropertyChanged(nameof(Flop1)); } }
        public ImageSource Flop2 { get { return flop2; } set { flop2 = value; OnPropertyChanged(nameof(Flop2)); } }
        public ImageSource Flop3 { get { return flop3; } set { flop3 = value; OnPropertyChanged(nameof(Flop3)); } }
        public ImageSource Turn { get { return turn; } set { turn = value; OnPropertyChanged(nameof(Turn)); } }
        public ImageSource River { get { return river; } set { river = value; OnPropertyChanged(nameof(River)); } }

        private string c1;
        public string C1 { get { return c1; }
            set {
                c1 = value;
                OnPropertyChanged(nameof(C1));
            }
        }

        public CommunityViewVM()
        {
            OnPropertyChanged(nameof(Community));
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void RefreshImageSources()
        {
            if (Community.Cards.Count >= 1)
            {
                Flop1 = ImageSource.FromResource(Community.Cards[0].SmallImagePath);
                C1 = Community.Cards[0].ShortName;
            }
            if (Community.Cards.Count >= 2)
                Flop2 = ImageSource.FromResource(Community.Cards[1].SmallImagePath);
            if (Community.Cards.Count >= 3)
                Flop3 = ImageSource.FromResource(Community.Cards[2].SmallImagePath);
            if (Community.Cards.Count >= 4)
                Turn = ImageSource.FromResource(Community.Cards[3].SmallImagePath);
            if (Community.Cards.Count == 5)
                River = ImageSource.FromResource(Community.Cards[4].SmallImagePath);

            OnPropertyChanged(nameof(Flop1));
            OnPropertyChanged(nameof(Flop2));
            OnPropertyChanged(nameof(Flop3));
            OnPropertyChanged(nameof(Turn));
            OnPropertyChanged(nameof(River));
        }
    }
}

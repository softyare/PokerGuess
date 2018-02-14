using PokerGuess.Models;
using PokerGuess.Services;
using PokerGuess.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokerGuess.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableContentView : ContentView
    {
        public List<GridPosition> GRIDPOSITIONS = new List<GridPosition>()
        {
            new GridPosition() {col=0, row=2 },
            new GridPosition() {col=2, row=0 },
            new GridPosition() {col=5, row=0 },
            new GridPosition() {col=8, row=2 },
            new GridPosition() {col=5, row=4 },
            new GridPosition() {col=2, row=4 }
        };
        public TableViewVM VM { get; set; }
        
        public TableContentView ()
		{
            VM = new TableViewVM();
            BindingContext = VM;
            InitializeComponent ();
		}
	}

    public class GridPosition
    {
        public int col { get; set; }
        public int row { get; set; }
    }
}
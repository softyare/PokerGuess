using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokerGuess.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
        private PokerGuess.ViewModels.GamePageVM vm;
        public GamePage ()
		{
            vm = new PokerGuess.ViewModels.GamePageVM();
            BindingContext = vm;
            InitializeComponent();
            vm.TableVm = TableCV.BindingContext as PokerGuess.ViewModels.TableViewVM;
        }
	}
}
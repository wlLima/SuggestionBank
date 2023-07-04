using SuggestionBank.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuggestionBank.ViewModel
{
    public class MainPageViewModel : Navigation
    {
        public ICommand CreateSuggestionCommand { get; set; }

        public MainPageViewModel()
        {
            CreateSuggestionCommand = new Command(() => Navigate(new CreateSuggestion()));
        }

    }
}

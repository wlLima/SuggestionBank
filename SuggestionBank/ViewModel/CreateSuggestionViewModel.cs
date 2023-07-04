using SuggestionBank.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuggestionBank.ViewModel
{
    public class CreateSuggestionViewModel
    {
        private Suggestions assistant;
        public Suggestions Suggestions { get => assistant; set { assistant = value; NotifyPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public CreateSuggestionViewModel()
        {
            Suggestions = new Suggestions();
            SaveCommand = new Command(() => SaveSuggestion());
        }

        public async void SaveSuggestion()
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Teste", "SaveSuggestion function", "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }

        }
    }
}

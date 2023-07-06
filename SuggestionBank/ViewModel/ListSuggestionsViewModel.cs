using SuggestionBank.DB;
using SuggestionBank.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SuggestionBank.ViewModel
{

    public class ListSuggestionsViewModel: INotifyPropertyChanged
    {
        public Suggestions Suggestions { get; set; }
        private DbAccess _context;
        public List<Suggestions> ListSuggestions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public ListSuggestionsViewModel()
        {
            _context = new DbAccess();
            DisplayList();
        }

        public async void DisplayList()
        {
            try
            {
                ListSuggestions = _context.Suggestions.ToList();

            }catch(Exception ex) {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

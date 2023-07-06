using SuggestionBank.DB;
using SuggestionBank.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuggestionBank.ViewModel
{
    public class CreateSuggestionViewModel: INotifyPropertyChanged
    {
        public Suggestions Suggestions { get; set; }
        public ICommand SaveCommand { get; set; }
        private DbAccess _context;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public CreateSuggestionViewModel()
        {
            _context = new DbAccess();
            Suggestions = new Suggestions();
            SaveCommand = new Command(() => SaveSuggestion());
        }

        public async void SaveSuggestion()
        {
            try
            {
                if (string.IsNullOrEmpty(Suggestions.Collaborator))
                {
                    throw new Exception("O Campo colaborador não pode ser vazio!");
                }

                if (string.IsNullOrEmpty(Suggestions.Departament))
                {
                    throw new Exception("O Campo departamento não pode ser vazio!");
                }

                if (string.IsNullOrEmpty(Suggestions.Suggestion))
                {
                    throw new Exception("O Campo sugestão não pode ser vazio!");
                }

                if (string.IsNullOrEmpty(Suggestions.Justification))
                {
                    throw new Exception("O Campo justificativa não pode ser vazio!");
                }

                _context.Suggestions.Add(Suggestions);
                _context.SaveChanges();

                Suggestions teste = _context.Suggestions.FirstOrDefault();
                await App.Current.MainPage.DisplayAlert("Teste", $"{teste.Justification}", "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }

        }
    }
}

using SuggestionBank.DB;
using SuggestionBank.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Department DepartmentSelected { get; set; }
        public ObservableCollection<Department> ListDepartment { get; set; }
        public ICommand SaveCommand { get; set; }
        private DbAccess _context;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public CreateSuggestionViewModel()
        {
            _context = new DbAccess();
            Suggestions = new Suggestions();
            SaveCommand = new Command(() => SaveSuggestion());
            SearchDepartment();
        }

        public async void SearchDepartment()
        {
            try
            {
                ListDepartment = new ObservableCollection<Department>(_context.Department.ToList());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        public async void SaveSuggestion()
        {
            try
            {
                if (string.IsNullOrEmpty(Suggestions.Collaborator))
                {
                    throw new Exception("O Campo colaborador não pode ser vazio!");
                }
                
                if (string.IsNullOrEmpty(Suggestions.Suggestion))
                {
                    throw new Exception("O Campo sugestão não pode ser vazio!");
                }

                if (string.IsNullOrEmpty(Suggestions.Justification))
                {
                    throw new Exception("O Campo justificativa não pode ser vazio!");
                }

                if(DepartmentSelected == null)
                {
                    throw new Exception("É necessário informar um departamento!");
                }

                Suggestions.Departament = DepartmentSelected.Name;

                _context.Suggestions.Add(Suggestions);
                _context.SaveChanges();

                Suggestions teste = _context.Suggestions.FirstOrDefault();
                await App.Current.MainPage.DisplayAlert("Sugestão", $"Salvo com sucesso!", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }

        }
    }
}

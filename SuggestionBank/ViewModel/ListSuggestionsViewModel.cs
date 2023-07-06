using SuggestionBank.DB;
using SuggestionBank.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;


namespace SuggestionBank.ViewModel
{

    public class ListSuggestionsViewModel : INotifyPropertyChanged
    {
        private Suggestions _suggestionsSelected;
        public Suggestions SuggestionsSelected { get => _suggestionsSelected; set { _suggestionsSelected = value; SelectedItem(value); } }
        private Department _departmentSelected;
        public Department DepartmentSelected { get => _departmentSelected; set { _departmentSelected = value; Search(value); } }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        private DbAccess _context;
        public ObservableCollection<Department> ListDepartment { get; set; }
        public ObservableCollection<Suggestions> ListSuggestions { get; set; }
        private ObservableCollection<Suggestions> listSuggestionsSearch;
    
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public ListSuggestionsViewModel()
        {
            _context = new DbAccess();
            DeleteCommand = new Command((item) => { DeleteSuggestions((Suggestions)item); });
            EditCommand = new Command((item) => { DeleteSuggestions((Suggestions)item); });
            DisplayList();
            ListSearchDepartment();
        }

        private async void ListSearchDepartment()
        {
            try
            {
                ListDepartment = new ObservableCollection<Department>(_context.Department.ToList());
                ListDepartment.Add(new Department() { Id = 0, Name = "Ver todos" });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        private async void Search(Department depart)
        {
            try
            {
                if(depart == null || depart.Name.Equals("Ver todos"))
                {
                    ListSuggestions = listSuggestionsSearch;
                }
                else
                {
                    ListSuggestions = new ObservableCollection<Suggestions>(listSuggestionsSearch.Where(p => p.Departament.Equals(depart.Name)));
                   
                }

            }catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        private async void SelectedItem(Suggestions item)
        {
            try
            {
                if (item != null)
                {
                    await App.Current.MainPage.DisplayAlert("Sugestão", $"Colaborador:\n{item.Collaborator}\n\nDepartamento:\n{item.Departament}\n\nSugestão:\n{item.Suggestion}\n\nJustificativa:\n{item.Justification}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        private async void DeleteSuggestions(Suggestions item)
        {
            try
            {
                bool r = await App.Current.MainPage.DisplayAlert("Sugestão", "Deletar sugestão?", "Deletar", "Cancelar");
                if (r)
                {
                    Suggestions s = _context.Suggestions.FirstOrDefault(s => s.Id == item.Id);
                    if (s != null && s.Id > 0)
                    {
                        _context.Suggestions.Remove(s);
                        ListSuggestions.Remove(item);
                        listSuggestionsSearch.Remove(item);

                        _context.SaveChanges();
                        await App.Current.MainPage.DisplayAlert("Sugestão", "item deletado com sucesso!", "Ok");


                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Sugestão", "Não existe essa sugestão na base de dados!", "Ok");
                    }

                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }


       
        public async void DisplayList()
        {
            try
            {
                ListSuggestions = new ObservableCollection<Suggestions>(_context.Suggestions.ToList());
                listSuggestionsSearch = new ObservableCollection<Suggestions>(_context.Suggestions.ToList());
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

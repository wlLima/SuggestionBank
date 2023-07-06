using SuggestionBank.DB;
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
    public class EditDepartmentViewModel: INotifyPropertyChanged
    {
       public Department Departments { get; set; }
        private DbAccess _context;
        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public EditDepartmentViewModel(Department Departments)
        {
            this.Departments = Departments;
            _context = new DbAccess();
            Departments = new Department();
            SaveCommand = new Command(() => SaveDepartment());
        }

        public async void SaveDepartment()
        {
            try
            {
                if (string.IsNullOrEmpty(Departments.Name))
                {
                    throw new Exception("O Campo nome não pode ser vazio!");
                }

                _context.Department.Update(Departments);
               _context.SaveChanges();

                await App.Current.MainPage.DisplayAlert("Departamento", $"Salvo com sucesso!", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

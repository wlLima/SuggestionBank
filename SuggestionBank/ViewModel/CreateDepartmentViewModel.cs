using SuggestionBank.DB;
using SuggestionBank.Model;
using SuggestionBank.View;
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
    public class CreateDepartmentViewModel: INotifyPropertyChanged
    {
        public Department Departments { get; set; }
        public ICommand SaveCommand { get; set; }
        private DbAccess _context;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public CreateDepartmentViewModel()
        {
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

                _context.Department.Add(Departments);
                _context.SaveChanges();
               // Department teste = _context.Department.FirstOrDefault();
                await App.Current.MainPage.DisplayAlert("Departamento", $"Salvo com sucesso! ", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

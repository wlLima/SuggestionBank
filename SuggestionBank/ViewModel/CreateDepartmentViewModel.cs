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
    public class CreateDepartmentViewModel
    {
        private Department auxiliar;
        public Department Departments { get => auxiliar; set { auxiliar = value; NotifyPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public CreateDepartmentViewModel()
        {
            Departments = new Department();
            SaveCommand = new Command(() => SaveDepartment());
        }

        public async void SaveDepartment()
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Teste", "SaveDepartment function", "Ok");

            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

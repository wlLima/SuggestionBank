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
    public class EditDepartmentViewModel
    {
        private Department assistant;
        public Department Departments { get => assistant; set { assistant = value; NotifyPropertyChanged(); } }

        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public EditDepartmentViewModel()
        {
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

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

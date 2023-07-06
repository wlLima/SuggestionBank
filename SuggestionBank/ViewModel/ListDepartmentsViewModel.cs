using Microsoft.EntityFrameworkCore;
using SuggestionBank.DB;
using SuggestionBank.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SuggestionBank.ViewModel
{
    public class ListDepartmentsViewModel: INotifyPropertyChanged
    {
        public Department Departments { get; set; }
        private DbAccess _context;
        public List<Department> ListDepartments { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public ListDepartmentsViewModel() {
            _context = new DbAccess();
            DisplayList();
        }

        public async void DisplayList()
        {
            try
            {
                ListDepartments = _context.Department.ToList();

            }catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

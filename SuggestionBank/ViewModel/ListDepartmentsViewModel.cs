using Microsoft.EntityFrameworkCore;
using SuggestionBank.DB;
using SuggestionBank.Model;
using SuggestionBank.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SuggestionBank.ViewModel
{
    public class ListDepartmentsViewModel: INotifyPropertyChanged
    {
        private Department _departments;
        public Department Departments { get=> _departments; set { _departments = value; SelectedItem(value); } }
        private DbAccess _context;
        public ObservableCollection<Department> ListDepartments { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        public ListDepartmentsViewModel() {
            _context = new DbAccess();
            DeleteCommand = new Command((item) => { DeleteSuggestions((Department)item); });
            EditCommand = new Command((item) => { EditDepartment((Department)item); });
            DisplayList();
        }

        private async void SelectedItem(Department item)
        {
            try
            {
                if (item != null)
                {
                    await App.Current.MainPage.DisplayAlert("Departamento", $"Nome:\n{item.Name}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        private async void DeleteSuggestions(Department item)
        {
            try
            {
                bool r = await App.Current.MainPage.DisplayAlert("Departamento", "Deletar Departamento?", "Deletar", "Cancelar");
                if (r)
                {
                    Department s = _context.Department.FirstOrDefault(s => s.Id == item.Id);
                    if (s != null && s.Id > 0)
                    {
                        _context.Department.Remove(s);
                        ListDepartments.Remove(item);

                        _context.SaveChanges();
                        await App.Current.MainPage.DisplayAlert("Departamento", "Item deletado com sucesso!", "Ok");


                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Departamento", "Não existe esse Departamento na base de dados!", "Ok");
                    }

                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        private async void EditDepartment(Department item)
        {
            try
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditDepartment() { BindingContext = new EditDepartmentViewModel(item) });

            }catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }

        public async void DisplayList()
        {
            try
            {
                ListDepartments = new ObservableCollection<Department>(_context.Department.ToList());

            }catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
            }
        }
    }
}

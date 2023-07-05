using SuggestionBank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuggestionBank.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListDepartments : ContentPage
	{
		public ListDepartments ()
		{
			InitializeComponent ();
			BindingContext = new ListDepartmentsViewModel();
		}
	}
}
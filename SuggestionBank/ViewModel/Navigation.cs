using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SuggestionBank.ViewModel
{
    public class Navigation
    {
      public async void Navigate(Page page)
        {
            int valor = 10;

            valor = valor / 2;
            
            await App.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}

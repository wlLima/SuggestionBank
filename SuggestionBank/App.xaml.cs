using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuggestionBank
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage()) {BarBackground = Color.FromHex("#B92E1A") };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

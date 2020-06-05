using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTripEver.Views;
namespace AppTripEver
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView())
            {
                BarBackgroundColor = Color.FromHex("#000000"),
                BarTextColor = Color.White
            };
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

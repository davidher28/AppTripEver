using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTripEver.Views;
using Plugin.FirebasePushNotification;

namespace AppTripEver
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView())
            {
                BarTextColor = Color.White
                //IsVisible = false
            };
        }

        protected override void OnStart()
        {
            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                Console.WriteLine("DAVID");
            };
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

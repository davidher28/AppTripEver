using System;
using System.Collections.Generic;
using AppTripEver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserBookingsView : ContentPage
    {
        UserBookingsViewModel context = new UserBookingsViewModel();
        public UserBookingsView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}

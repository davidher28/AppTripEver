using System;
using System.Collections.Generic;
using AppTripEver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HostBookingsView : ContentPage
    {
        HostBookingsViewModel context = new HostBookingsViewModel();
        public HostBookingsView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}

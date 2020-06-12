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

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
        }

        void ListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
        }
    }
}

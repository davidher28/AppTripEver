using System;
using System.Collections.Generic;
using AppTripEver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingUserInfoView
    {
        BookingUserInfoViewModel context = new BookingUserInfoViewModel();
        public BookingUserInfoView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}

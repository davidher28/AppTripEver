using System;
using System.Collections.Generic;
using AppTripEver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingInfoViewPop
    {
        BookingInfoViewModel context = new BookingInfoViewModel();
        public BookingInfoViewPop()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}

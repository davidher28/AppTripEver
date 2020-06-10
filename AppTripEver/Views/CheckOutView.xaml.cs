using System;
using System.Collections.Generic;
using AppTripEver.ViewModels;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckOutView
    {
        CheckOutViewModel context = new CheckOutViewModel();
        public CheckOutView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}

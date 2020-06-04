using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTripEver.ViewModels;
using AppTripEver.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseView : ContentPage
    {
        ChooseViewModel context = new ChooseViewModel();

        public ChooseView()
        {
            InitializeComponent();
            BindingContext = context;

        }
    }
}
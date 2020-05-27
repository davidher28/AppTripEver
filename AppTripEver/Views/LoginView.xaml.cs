using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTripEver.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private async void Choose_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChooseView());
        }
    }
}
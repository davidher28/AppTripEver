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
    public partial class ChooseView : ContentPage
    {
        public ChooseView()
        {
            InitializeComponent();
        }
        private async void User_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserView());
        }
        private async void Host_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HostView());
        }
    }
}
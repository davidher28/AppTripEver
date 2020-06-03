using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppTripEver.ViewModels;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        UsuarioViewModel context = new UsuarioViewModel();

        public LoginView()
        {
            InitializeComponent();
            BindingContext = context;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTripEver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioTabbedView : Xamarin.Forms.TabbedPage
    {
        UsuarioTabbedViewModel context = new UsuarioTabbedViewModel();
        public UsuarioTabbedView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.FromHex("#3D70E0"));
            BindingContext = context;
        }
    }
}
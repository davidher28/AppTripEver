using AppTripEver.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearServicioView : ContentPage
    {
        CrearServicioViewModel context = new CrearServicioViewModel();
        public CrearServicioView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
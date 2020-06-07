using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppTripEver.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioTabbedView : TabbedPage
    {
        UsuarioTabbedViewModel context = new UsuarioTabbedViewModel();
        public UsuarioTabbedView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
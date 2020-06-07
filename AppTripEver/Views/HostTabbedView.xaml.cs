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
    public partial class HostTabbedView : TabbedPage
    {
        HostTabbedViewModel context = new HostTabbedViewModel();
        public HostTabbedView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
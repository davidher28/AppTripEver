using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppTripEver.ViewModels;
using Xamarin.Forms.Xaml;

namespace AppTripEver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceInfoViewPop
    {
        ServiceInformationViewModel context = new ServiceInformationViewModel();
        public ServiceInfoViewPop()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
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
    public partial class PopGeneralView
    {
        PopGeneralViewModel context = new PopGeneralViewModel();
        public PopGeneralView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
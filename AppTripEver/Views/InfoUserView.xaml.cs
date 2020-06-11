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
    public partial class InfoUserView 
    {
        InfoUserViewModel context = new InfoUserViewModel();
        public InfoUserView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
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
    public partial class AddFechasView
    {
        AddFechasViewModel context = new AddFechasViewModel();
        public AddFechasView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
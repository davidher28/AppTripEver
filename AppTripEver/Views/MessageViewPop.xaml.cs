using System;
using System.Collections.Generic;
using AppTripEver.ViewModels;
using Xamarin.Forms;

namespace AppTripEver.Views
{
    public partial class MessageViewPop
    {
        MessageViewModel context = new MessageViewModel();
        public MessageViewPop()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
